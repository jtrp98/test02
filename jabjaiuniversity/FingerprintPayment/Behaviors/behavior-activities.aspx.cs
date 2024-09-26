using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using WebGrease.Css.Extensions;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;
using Newtonsoft.Json.Linq;
using System.Web.Script.Services;
using PagedList;

namespace FingerprintPayment
{
    public partial class behavior_activities : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

        }

        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            int schoolID = UserData.CompanyID;
            int behaviorID = int.Parse(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect("behavior-activities-edit.aspx?id=" + e.CommandArgument);
                    break;
                case "Del":
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
                    {
                        var tBehavior = dbschool.TBehaviors.Where(w => w.SchoolID == schoolID && w.BehaviorId == behaviorID).FirstOrDefault();
                        tBehavior.Status = false;
                        dbschool.SaveChanges();
                        database.InsertLog(UserData.UserID.ToString(),
                           "ทำการลบข้อมูลความประพฤติ " + tBehavior.BehaviorName,
                           UserData.Entities,
                           HttpContext.Current.Request, 4, 4, 0);
                    }
                    Response.Redirect("behavior-activities.aspx");
                    break;
                case "Page":
                    break;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string updatedata(TBehavior tBehavior)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    if (tBehavior.BehaviorId == 0)
                    {
                        //int BehaviorId = 1;
                        //if (dbschool.TBehaviors.Count() > 0) BehaviorId = dbschool.TBehaviors.Max(max => max.BehaviorId + 1);
                        //tBehavior.BehaviorId = BehaviorId;
                        tBehavior.BehaviorName = tBehavior.BehaviorName;
                        tBehavior.BehaviorNameEN = tBehavior.BehaviorNameEN;
                        tBehavior.dAdd = DateTime.Now;
                        tBehavior.UserAdd = userData.UserID;
                        tBehavior.Status = true;
                        tBehavior.SchoolID = schoolID;
                        tBehavior.CreatedBy = userData.UserID;
                        tBehavior.CreatedDate = DateTime.Now;

                        dbschool.TBehaviors.Add(tBehavior);

                        database.InsertLog(userData.UserID.ToString(),
                            "ทำการเพิ่มข้อมูลความประพฤติ " + tBehavior.BehaviorName,
                            userData.Entities,
                            HttpContext.Current.Request, 4, 2, 0);
                    }
                    else
                    {
                        var q = dbschool.TBehaviors.Find(tBehavior.BehaviorId, schoolID);
                        if (q.UserAdd.HasValue) q.BehaviorName = tBehavior.BehaviorName;
                        q.BehaviorNameEN = tBehavior.BehaviorNameEN;
                        q.dUpdate = DateTime.Now;
                        q.UserUpdate = userData.UserID;
                        q.Score = tBehavior.Score;
                        q.UpdatedBy = userData.UserID;
                        q.UpdatedDate = DateTime.Now;

                        database.InsertLog(userData.UserID.ToString(),
                            "ทำการแก้ไขข้อมูลความประพฤติ " + tBehavior.BehaviorName,
                            userData.Entities,
                            HttpContext.Current.Request, 4, 3, 0);
                    }
                    dbschool.SaveChanges();
                    return "Success";
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<Behaviordata> getdata(int behavior_id)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    var rss = (from a in dbschool.TBehaviors
                               where a.SchoolID == schoolID && a.BehaviorId == behavior_id
                               select new Behaviordata
                               {
                                   BehaviorId = a.BehaviorId,
                                   BehaviorName = a.BehaviorName,
                                   BehaviorNameEN = a.BehaviorNameEN,
                                   Type = a.Type,
                                   Score = a.Score,
                                   CreatedBy = a.UserAdd
                               }).ToList();
                    return rss;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string deletedata(int behavior_id)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    var q = dbschool.TBehaviors.Find(behavior_id, schoolID);
                    q.Status = false;
                    q.cDel = true;
                    q.UpdatedBy = userData.UserID;
                    q.UpdatedDate = DateTime.Now;
                    dbschool.SaveChanges();
                    database.InsertLog(userData.UserID.ToString(), "ทำการลบข้อมูลความประพฤติ " + q.BehaviorName,
                       userData.Entities,
                       HttpContext.Current.Request, 4, 4, 0);
                    return "Success";
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static Search_data returnlist(Search search)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    var tTerm = dbschool.TTerms.Where(w => w.SchoolID == schoolID && w.dStart <= DateTime.Today && w.dEnd >= DateTime.Today).FirstOrDefault();
                    var tBehaviorSettings = dbschool.TBehaviorSettings.Where(w => w.SchoolID == schoolID).FirstOrDefault();
                    DateTime _dstart = DateTime.Today;
                    DateTime _dEnd = DateTime.Today.AddDays(1);
                    int Rows = 1;
                    var q1 = (from a in dbschool.TBehaviors
                              where a.SchoolID == schoolID && a.Status == true && a.Type == search.BehaviorType
                              select new Behaviordata
                              {
                                  BehaviorId = a.BehaviorId,
                                  BehaviorName = a.BehaviorName,
                                  BehaviorNameEN = a.BehaviorNameEN,
                                  Score = a.Score,
                                  Type = a.Type,
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

        public class Behaviordata
        {
            public int BehaviorId { get; set; }
            public string BehaviorName { get; set; }
            public string BehaviorNameEN { get; set; }
            public decimal? Score { get; set; }
            public int? Type { get; set; }
            public int Row { get; set; }
            public int? CreatedBy { get; set; }
        }

        public class Search
        {
            public int pageSize { get; set; }
            public int pageNumber { get; set; }
            public string wording { get; set; }
            public int? BehaviorType { get; set; }
        }

        public class Search_data
        {
            public FOOT FOOT { get; set; }
            public List<Behaviordata> DATA { get; set; }
        }

        public class FOOT
        {
            public int? pageSize { get; set; }
        }
    }
}