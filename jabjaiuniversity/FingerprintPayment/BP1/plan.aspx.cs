using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.BP1
{
    public partial class plan : System.Web.UI.Page
    {
        public string selectOption = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }
            if (!this.IsPostBack)
            {
                using (JabJaiMasterEntities db = Connection.MasterEntities())
                {
                    string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    selectOption += $"<option value=''>- ทั้งหมด -</option>";
                    using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(tCompany)))
                    {
                        foreach (var data in QueryDataBases.SubLevel_Query.GetData(entities))
                        {
                            selectOption += $"<option value='{ data.class_id }'>{data.class_name}</option>";
                        }
                    }
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object updateData(Subject subject)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }
            using (JabJaiMasterEntities db = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var user_id = HttpContext.Current.Session["sEmpID"].ToString();
                using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(tCompany)))
                {
                    var tb_Subject = entities.TB_Subject.FirstOrDefault(f => f.SubjectID == subject.SubjectID);
                    if (tb_Subject == null) tb_Subject = new TB_Subject();
                    tb_Subject.SubjectName = subject.SubjectName;
                    tb_Subject.nCredit = subject.nCredit;
                    tb_Subject.courseCode = subject.courseCode;
                    tb_Subject.courseGroup = subject.courseGroup;
                    tb_Subject.courseType = subject.courseType;
                    tb_Subject.courseHour = subject.courseHour;
                    tb_Subject.courseTotalHour = subject.courseTotalHour;
                    tb_Subject.nTSubLevel = subject.nTSubLevel;

                    if (tb_Subject.SubjectID == 0)
                    {
                        tb_Subject.SubjectID = entities.TB_Subject.Select(s => s.SubjectID).DefaultIfEmpty(0).Max() + 1;
                        tb_Subject.DateCreate = DateTime.Now;
                        tb_Subject.UserCreate = int.Parse(user_id);
                        tb_Subject.Delete = false;
                        entities.TB_Subject.Add(tb_Subject);
                    }
                    else
                    {
                        tb_Subject.DateUpdate = DateTime.Now;
                        tb_Subject.UserUpdate = int.Parse(user_id);
                    }

                    entities.SaveChanges();
                    return "Success";
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object listData(Search search)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }
            using (JabJaiMasterEntities db = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var user_id = HttpContext.Current.Session["sEmpID"].ToString();
                using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(tCompany)))
                {
                    var tPlanes = entities.TB_Subject.ToList();
                    var q = (from a in tPlanes
                             join b in entities.TSubLevels on a.nTSubLevel equals b.nTSubLevel

                             where a.Delete == false && (!search.levelId.HasValue || a.nTSubLevel == search.levelId)
                    select new Subject
                    {
                        SubjectName = a.SubjectName,
                        nCredit = a.nCredit,
                        courseCode = a.courseCode,
                        courseGroup = a.courseGroup,
                        courseType = a.courseType,
                        courseHour = a.courseHour,
                        courseTotalHour = a.courseTotalHour,
                        nTSubLevel = a.nTSubLevel,
                        SubjectID = a.SubjectID,
                        subllevelName = b.SubLevel
                    }).ToList();

                    ///TODO ==> SET PAGED
                    int rowsIndex = 1, pageSize = 1;
                    pageSize = (q.Count() / (search.pageSize ?? 1)) + (q.Count() % search.pageSize == 0 ? 0 : 1);

                    if (!string.IsNullOrEmpty(search.sortName))
                    {
                        var sortExpressions = new List<Tuple<string, string>>();
                        sortExpressions.Add(new Tuple<string, string>(search.sortName, search.sorting ? "desc" : "asc"));

                        q = q.MultipleSort(sortExpressions).ToList();
                    }

                    q = q.ToPagedList(search.pageNumber.Value, search.pageSize.Value).ToList();
                    q.ToList().ForEach(f =>
                    {
                        f.Index = (((search.pageNumber ?? 1) - 1) * (search.pageSize ?? 20)) + rowsIndex++;
                        switch (f.courseGroup)
                        {
                            case 1: f.courseGroupName = "รายวิชาพื้นฐาน"; break;
                            case 2: f.courseGroupName = "รายวิชาเพิ่มเติม"; break;
                            case 3: f.courseGroupName = "กิจกรรมพัฒนาผู้เรียน"; break;
                            case 4: f.courseGroupName = "รายวิชาเสริมไม่คิดหน่วยกิต"; break;
                        }
                    });

                    var result = new Result
                    {
                        DATA = q,
                        FOOT = new Result.foot
                        {
                            pageSize = pageSize
                        }
                    };

                    return result;
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object deleteData(int id)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }
            using (JabJaiMasterEntities db = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var user_id = HttpContext.Current.Session["sEmpID"].ToString();
                using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(tCompany)))
                {

                    var tb_Subject = entities.TB_Subject.FirstOrDefault(f => f.SubjectID == id);

                    tb_Subject.Delete = true;
                    entities.SaveChanges();

                    return "Success";
                }
            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object getData(int id)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }
            using (JabJaiMasterEntities db = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var user_id = HttpContext.Current.Session["sEmpID"].ToString();
                using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(tCompany)))
                {

                    return entities.TB_Subject.FirstOrDefault(f => f.SubjectID == id);

                }
            }
        }

        public class Subject
        {
            public int Index { get; set; }
            public int SubjectID { get; set; }
            public string SubjectName { get; set; }
            public Nullable<double> nCredit { get; set; }
            public string courseCode { get; set; }
            public Nullable<int> courseGroup { get; set; }
            public Nullable<int> courseType { get; set; }
            public Nullable<double> courseHour { get; set; }
            public Nullable<double> courseTotalHour { get; set; }
            public Nullable<int> nTSubLevel { get; set; }
            public string subllevelName { get; set; }
            public string courseGroupName { get; set; }
        }

        public class Search
        {
            public string wording { get; set; }
            public Nullable<int> levelId { get; set; }
            public Nullable<int> pageSize { get; set; }
            public Nullable<int> pageNumber { get; set; }
            public string sortName { get; set; }
            public bool sorting { get; set; }
        }

        public class Result
        {
            public foot FOOT { get; set; }
            public object DATA { get; set; }

            public class foot
            {
                public int pageSize { get; set; }
            }
        }
    }
}