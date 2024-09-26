using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Web; 

namespace FingerprintPayment.GraduationReport.CsFile
{
    public class QueryEngine
    {
        public QueryEngine()
        {
            //
            // Add constructor logic here
            //
            /*
             http://localhost:50465//Home/AjaxGetJsonData?draw=1&columns[0][data]=Name&columns[0][name]=&columns[0][searchable]=true&columns[0][orderable]=true&columns[0][search][value]=&columns[0][search][regex]=false&columns[1][data]=Age&columns[1][name]=&columns[1][searchable]=true&columns[1][orderable]=false&columns[1][search][value]=&columns[1][search][regex]=false&columns[2][data]=DoB&columns[2][name]=&columns[2][searchable]=true&columns[2][orderable]=true&columns[2][search][value]=&columns[2][search][regex]=false&order[0][column]=0&order[0][dir]=asc&start=0&length=10&search[value]=&search[regex]=false&_=1437225574923
             */
        }

        public static string LoadGraduationReportJsonData(int draw, int pageIndex, int pageSize, string sortBy, string ddlYear, string ddlTerm, string ddlSubLV)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataStdGraduation> listData;

          

            string sEntities = (string)HttpContext.Current.Session["sEntities"];

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                var company = dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(ddlYear) && !string.IsNullOrEmpty(ddlTerm))
                {
                    var year = Int32.Parse(ddlYear);

                    var codeTerm = (from t in en.TTerms 
                                    where t.nYear == year && t.sTerm == ddlTerm 
                                    select t.nTerm).FirstOrDefault().Trim();

                    if (!string.IsNullOrEmpty(codeTerm))
                    {
                        sqlCondition += string.Format(@" AND (sh.nTerm = '{0}')", codeTerm);
                    }
                }

                if (!string.IsNullOrEmpty(ddlSubLV))
                {
                    sqlCondition += string.Format(@" AND (sv.nTSubLevel = {0})", ddlSubLV);
                }

                string sqlQuery = string.Format(@"
SELECT sh.sID 'ID', sv.sStudentID 'StudentId', sv.titleDescription 'Title', sv.sName 'Name', sv.sLastname 'Lastname', sv.nTSubLevel 'TSubLevel', sv.nTSubLevel2 'nTSubLevel2'
, CONCAT(sv.SubLevel,' / ',sv.nTSubLevel2) 'ClassDisplay', u.sIdentification 'Identification', sh.nTerm, sv.numberYear 'numberYear'
FROM TStudentHIstory sh 
LEFT JOIN TB_StudentViews sv ON sh.SchoolID = sv.SchoolID AND sh.nTerm = sv.nTerm AND sh.nTermSubLevel2_OLD = sv.nTermSubLevel2 AND sh.sID = sv.sID
LEFT JOIN TUser u ON sh.SchoolID = u.SchoolID AND sh.sID = u.sID
WHERE sh.SchoolID = {0} AND (u.nStudentStatus <> 2 OR sv.nStudentStatus <> 2) {1}", company.nCompany, sqlCondition);
                List<ListDataStdGraduation> resultCount = en.Database.SqlQuery<ListDataStdGraduation>(sqlQuery).ToList();

                sqlQuery = string.Format(@"
SELECT A.*
FROM 
    (
    SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM ( 
        {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3} order by nTSubLevel2 , StudentId", sortBy, sqlQuery, lowerBound, upperBound);
                List<ListDataStdGraduation> resultFilter = en.Database.SqlQuery<ListDataStdGraduation>(sqlQuery).ToList();

                totalData = resultCount.Count();
                filterData = resultFilter.Count();

                listData = new List<ListDataStdGraduation>();
                ListDataStdGraduation item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataStdGraduation();
                    item.no = "";
                    item.StudentId = i.StudentId;
                    item.Identification = i.Identification;
                    item.Title = i.Title;
                    item.Name = i.Name;
                    item.Lastname = i.Lastname;
                    item.ClassDisplay = i.ClassDisplay;
                    listData.Add(item);
                }
            }

            CollectionData<ListDataStdGraduation> data = new CollectionData<ListDataStdGraduation>();
            data.draw = draw;
            data.pageIndex = pageIndex;
            data.pageSize = pageSize;
            data.pageCount = (totalData / pageSize) + 1;
            data.recordsTotal = totalData;
            data.recordsFiltered = filterData;
            data.data = listData;

            var json = JsonConvert.SerializeObject(data);

            return json;
        }
    }
}

