using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.SessionState;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using AccountingDB;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for dataGenericListData
    /// </summary>
    public class dataGenericListData : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string _mode = fcommon.ReplaceInjection(context.Request["Mode"]);
            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //Dictionary<string, object> row;
            // List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                throw new Exception();
            }
            var result = "";
            switch (_mode)
            {
                case "liststudent":
                    result = GetListstudent(userData, fcommon.ReplaceInjection(context.Request["nelevel"]), fcommon.ReplaceInjection(context.Request["nsublevel"]));
                    break;
                case "liststudentv2":
                    result = GetListstudentV2(userData, fcommon.ReplaceInjection(context.Request["nelevel"]), fcommon.ReplaceInjection(context.Request["nsublevel"]));
                    break;
                case "liststudentv3":
                    result = GetListstudentV3(userData);
                    break;
                case "listproduct":
                    result = GetProductList(userData, fcommon.ReplaceInjection(context.Request["id"]));
                    break;
            }
            context.Response.Expires = -1;
            context.Response.ContentType = "application/json";
            //context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(result);
            context.Response.End();
        }

        private string GetProductList(JWTToken.userData userData, string Id)
        {
            int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(Id));
            using (var accountContext = new AccountingDBContext())
            using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
            {
                var unitList = accountContext.AccountMeasure
                    .Where(w => (w.SchoolId == userData.CompanyID || w.SchoolId == null) && w.DeleteDate == null)
                    .Select(o => new { o.AccountMeasureId, o.NameThai })
                    .ToList();

                var data = (from a in dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID)
                         from b in dbschool.TTypes.Where(w => w.SchoolID == userData.CompanyID && w.nTypeID == a.nType)
                             //orderby new { a.sBarCode.Length, a.sBarCode }
                         where a.cDel == null && b.shop_id == shop_id
                         select new
                         {
                             id = a.nProductID,
                             name = a.sProduct,
                             code = a.sBarCode,
                             unit = a.UnitID,
                         })
                         .ToList();

                var result = from a in data
                             from b in unitList.Where(o => o.AccountMeasureId == a.unit).DefaultIfEmpty()
                             select new { 
                                a.id,
                                a.code,
                                a.name,
                                unit = b?.NameThai
                             };

                return JsonConvert.SerializeObject(result);
                //rss.DATA = new JArray(from a in q
                //                      select new JObject
                //                          {
                //                              new JProperty("value", a.nProductID ),
                //                              new JProperty("label", a.sProduct),
                //                              new JProperty("barcode",a.sBarCode),
                //                          });

                //return rss.ToString();
            }
        }

        private string GetListstudent(JWTToken.userData userData, string nLevel, string nSubLevel)
        {
            int schoolID = userData.CompanyID;
            SqlConnection _conn = fcommon.ConfigSqlConnection(schoolID);
            string _comm = "";
            if (!string.IsNullOrEmpty(nLevel))
                _comm += " AND nTSubLevel = " + nLevel;
            if (!string.IsNullOrEmpty(nSubLevel) && nSubLevel != "undefined")
                _comm += " AND nTermSubLevel2 = " + nSubLevel;

            if (string.IsNullOrEmpty(nSubLevel))
            {
                using (var entities = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {
                    StudentLogic logic = new StudentLogic(entities);
                    var term_0 = logic.GetTermDATA(DateTime.Now, new JWTToken.userData { CompanyID = schoolID });

                    var term_1 = entities.TTerms.OrderBy(o => o.dStart).FirstOrDefault(f => f.dStart >= term_0.dStart && f.cDel == null);
                    if (term_1 != null)
                    {
                        _comm += $" AND nTerm in ( '{term_1.nTerm}','{term_0.nTerm}')";
                    }
                    else
                    {
                        _comm += $" AND nTerm = '{term_0.nTerm}'";
                    }
                }

            }

            //string SQL = @"select sID,sName + ' ' + sLastname AS sName,TB2.nTermSubLevel2,TB2.nTSubLevel,TB2.nTSubLevel2,sStudentID as studentid
            //               from TUser AS TB1 left join TTermSubLevel2 AS TB2 on TB1.nTermSubLevel2 = TB2.nTermSubLevel2
            //               where TB1.cDel IS NULL AND TB1.SchoolID = " + schoolID + " " + _comm;

            string SQL = @"SELECT DISTINCT sID,sName + ' ' + sLastname AS sName,nTermSubLevel2,nTSubLevel,nTSubLevel2,sStudentID as studentid 
                            FROM [JabjaiSchoolSingleDB].[dbo].[TB_StudentViews]
                           where cDel IS NULL AND isnull(nStudentStatus,0) in (0,4) AND SchoolID = " + schoolID + " " + _comm;

            DataTable dtUser = fcommon.Get_Data(_conn, SQL);
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dtUser == null) return JsonConvert.SerializeObject(rows);
            foreach (DataRow dr in dtUser.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dtUser.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return JsonConvert.SerializeObject(rows);
        }

        private string GetListstudentV2(JWTToken.userData userData, string nLevel, string nSubLevel)
        {
            int schoolID = userData.CompanyID;
            SqlConnection _conn = fcommon.ConfigSqlConnection(schoolID);
            string _comm = "";
            if (!string.IsNullOrEmpty(nLevel))
                _comm += " AND nTSubLevel = " + nLevel;
            if (!string.IsNullOrEmpty(nSubLevel) && nSubLevel != "undefined")
                _comm += " AND nTermSubLevel2 = " + nSubLevel;

            if (string.IsNullOrEmpty(nSubLevel))
            {
                using (var entities = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {
                    StudentLogic logic = new StudentLogic(entities);
                    var term_0 = logic.GetTermDATA(DateTime.Now, new JWTToken.userData { CompanyID = schoolID });

                    var term_1 = entities.TTerms.OrderBy(o => o.dStart).FirstOrDefault(f => f.dStart >= term_0.dStart && f.cDel == null);
                    if (term_1 != null)
                    {
                        _comm += $" AND nTerm in ( '{term_1.nTerm}','{term_0.nTerm}')";
                    }
                    else
                    {
                        _comm += $" AND nTerm = '{term_0.nTerm}'";
                    }
                }
            }

            string SQL = @"SELECT DISTINCT sID,sName + ' ' + sLastname AS sName,nTermSubLevel2,nTSubLevel,nTSubLevel2,sStudentID as studentid 
                            FROM [JabjaiSchoolSingleDB].[dbo].[TB_StudentViews]
                           where cDel IS NULL AND isnull(nStudentStatus,0) in (0,4) AND SchoolID = " + schoolID + " " + _comm;

            DataTable dtUser = fcommon.Get_Data(_conn, SQL);
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dtUser == null) return JsonConvert.SerializeObject(rows);
            foreach (DataRow dr in dtUser.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dtUser.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return JsonConvert.SerializeObject(rows);
        }

        private string GetListstudentV3(JWTToken.userData userData)
        {
            int schoolID = userData.CompanyID;
            SqlConnection _conn = fcommon.ConfigSqlConnection(schoolID);

            string SQL = @"SELECT DISTINCT sID,sName + ' ' + sLastname AS sName,sStudentID as studentid 
                            FROM [JabjaiSchoolSingleDB].[dbo].[TUser]
                           where cDel is null and ISNULL(nStudentStatus,0) = 0 AND SchoolID = " + schoolID;

            DataTable dtUser = fcommon.Get_Data(_conn, SQL);
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dtUser == null) return JsonConvert.SerializeObject(rows);
            foreach (DataRow dr in dtUser.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dtUser.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return JsonConvert.SerializeObject(rows);
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}