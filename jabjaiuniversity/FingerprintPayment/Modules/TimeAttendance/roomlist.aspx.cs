using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class roomlist1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string updatedata(TClass Rooms)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    if (string.IsNullOrEmpty(Rooms.sClassID))
                    {
                        Rooms.sClassID = dbschool.TClasses.Where(w => w.SchoolID == userData.CompanyID).Select(s => s.sClassID).DefaultIfEmpty("0").Max();
                        string FormatID = string.Format("CL{0:000}", userData.CompanyID);
                        if (Rooms.sClassID.StartsWith(string.Format("{0:000}CL", userData.CompanyID)))
                        {
                            Rooms.sClassID = string.Format("{0}0{1:0000}", FormatID, int.Parse(Rooms.sClassID.Substring(0, 3).Replace(FormatID, "")) + 1);
                        }
                        else if (Rooms.sClassID.StartsWith(string.Format("CL{0:000}", userData.CompanyID)))
                        {
                            if (userData.CompanyID > 999) Rooms.sClassID = string.Format("{0}0{1:000}", FormatID, int.Parse(Rooms.sClassID.Replace(FormatID, "")) + 1);
                            else Rooms.sClassID = string.Format("{0}0{1:0000}", FormatID, int.Parse(Rooms.sClassID.Replace(FormatID, "")) + 1);
                        }
                        else
                        {
                            if (userData.CompanyID > 999) Rooms.sClassID = string.Format("{0}0{1:000}", FormatID, 1);
                            else Rooms.sClassID = string.Format("{0}0{1:0000}", FormatID, 1);
                        }
                        Rooms.SchoolID = userData.CompanyID;
                        Rooms.CreatedDate = DateTime.Now;
                        Rooms.CreatedBy = userData.UserID;

                        dbschool.TClasses.Add(Rooms);

                        database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                            "ทำการเพิ่มข้อมูลห้องเรียน " + Rooms.sClass,
                            HttpContext.Current.Session["sEntities"].ToString(),
                            HttpContext.Current.Request, 4, 2, 0);
                    }
                    else
                    {
                        var q = dbschool.TClasses.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.sClassID == Rooms.sClassID);
                        Rooms.UpdatedDate = DateTime.Now;
                        Rooms.UpdatedBy = userData.UserID;

                        q.sClass = Rooms.sClass;

                        database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                            "ทำการแก้ไขข้อมูลข้อมูลห้องเรียน " + Rooms.sClass,
                            HttpContext.Current.Session["sEntities"].ToString(),
                            HttpContext.Current.Request, 4, 2, 0);
                    }

                    dbschool.SaveChanges();
                    return "Success";
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<RoomsData> getdata(string Rooms_Id)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    var rss = (from a in dbschool.TClasses.Where(w => w.SchoolID == userData.CompanyID)
                               where a.sClassID == Rooms_Id
                               select new RoomsData
                               {
                                   SchoolID = userData.CompanyID,
                                   Rooms_Id = a.sClassID,
                                   Rooms_Name = a.sClass,
                               }).ToList();

                    return rss;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string deletedata(string Rooms_Id)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    var q = dbschool.TClasses.First(f => f.sClassID == Rooms_Id);
                    q.cDel = "1";
                    dbschool.SaveChanges();
                    //database.InsertLog(HttpContext.Current.Session["sEmpID"] + "", "ทำการลบข้อมูลความประพฤติ " + q.BehaviorName,
                    //   HttpContext.Current.Session["sEntities"].ToString(),
                    //   HttpContext.Current.Request, 4, 4, 0);
                    return "Success";
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static Search_data returnlist(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == entities).FirstOrDefault();
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
                {
                    var tTerm = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).Where(w => w.dStart <= DateTime.Today && w.dEnd >= DateTime.Today).FirstOrDefault();
                    var tBehaviorSettings = dbschool.TBehaviorSettings.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault();
                    DateTime _dstart = DateTime.Today;
                    DateTime _dEnd = DateTime.Today.AddDays(1);
                    int Rows = 1;
                    var q1 = (from a in dbschool.TClasses.Where(w => w.SchoolID == userData.CompanyID)
                              where a.cDel == null
                              select new RoomsData
                              {
                                  Rooms_Id = a.sClassID,
                                  Rooms_Name = a.sClass,
                              }).ToList();

                    q1.ToList().ForEach(f => f.Row = Rows++);

                    var q2 = new Search_data();
                    int pageSize = 0;
                    if (q1.Count() > 0) pageSize = (q1.Count() / search.pageSize) + (q1.Count() % search.pageSize > 0 ? 1 : 0);
                    q2.FOOT = new FOOT
                    {
                        pageSize = pageSize
                    };
                    q2.DATA = q1.ToPagedList(search.pageNumber, search.pageSize).ToList();

                    return q2;
                }
            }
        }

        public class RoomsData
        {
            public string Rooms_Id { get; set; }
            public string Rooms_Name { get; set; }
            public int Row { get; set; }
            public int SchoolID { get; set; }
        }

        public class Search
        {
            public int pageSize { get; set; }
            public int pageNumber { get; set; }
            public string wording { get; set; }
        }

        public class Search_data
        {
            public FOOT FOOT { get; set; }
            public List<RoomsData> DATA { get; set; }
        }

        public class FOOT
        {
            public int? pageSize { get; set; }
        }
    }
}