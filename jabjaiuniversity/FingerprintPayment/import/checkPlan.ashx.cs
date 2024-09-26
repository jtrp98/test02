using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace FingerprintPayment.import
{
    /// <summary>
    /// Summary description for checkPlan
    /// </summary>
    public class checkPlan : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                dynamic rss = new JObject();

                string term = fcommon.ReplaceInjection(context.Request["term"]);
                //int termn;
                //bool isTermNumeric = int.TryParse(term, out termn);
                //int? termn = int.Parse(term);
                string year = fcommon.ReplaceInjection(context.Request["numberYear"]);
                int? numberYear = int.Parse(year);

                //var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                planCheck planCheck = new planCheck();
                List<planCheck> planCheckList = new List<planCheck>();

                //var q_plan = _db.TPlanes.Where(w => w.cDel == null).ToList();

                string data = fcommon.ReplaceInjection(context.Request["id"]);
                string idlv = fcommon.ReplaceInjection(context.Request["idlv"]);
                var planId = int.Parse(fcommon.ReplaceInjection(context.Request["planId"]));
                int idlvn = int.Parse(idlv);

                var terms = (from p in _db.TYears.Where(w => w.SchoolID == nCompany.nCompany && w.numberYear == numberYear && w.cDel == false)
                             join t in _db.TTerms.Where(w => w.SchoolID == nCompany.nCompany && w.sTerm == term && w.cDel == null) on p.nYear equals t.nYear
                             select new { p.nYear, t.nTerm }).FirstOrDefault();

                //var planCourseDTO = ServiceHelper.GetPlanCourses(idlvn, 0, terms.nTerm, terms.nYear, nCompany.nCompany, _db);

                // Split string on spaces (this will separate all the words).
                string[] words = data.Split('~');
                foreach (string word in words)
                {
                    //var checkplan = (from p in planCourseDTO.Where(w => w.SchoolID == nCompany.nCompany && w.NTSubLevel == idlv && w.CourseCode == word)
                    ////join pc in _db.TPlanCourses.Where(w => w.SchoolID == nCompany.nCompany && w.PlanId == planId) on p.sPlaneID equals pc.sPlaneID
                    //select p).FirstOrDefault();
                    //var checkplan = _db.TPlanes.Where(w => w.courseCode == word && w.cDel == null && w.SchoolID == nCompany.nCompany && w.nTSubLevel == idlv).FirstOrDefault();
                    var checkplan = (from p in _db.TPlanes.Where(w => w.SchoolID == nCompany.nCompany && w.nTSubLevel == idlv && w.courseCode == word && w.cDel == null)
                                     join pc in _db.TPlanCourses.Where(w => w.SchoolID == nCompany.nCompany && w.PlanId == planId && w.CourseStatus == 1 && w.IsActive == true) on p.sPlaneID equals pc.sPlaneID
                                     join r in _db.TPlanCourseTerms.Where(w => w.SchoolID == nCompany.nCompany && w.nTerm == terms.nTerm && w.IsActive == true) on pc.PlanCourseId equals r.PlanCourseId
                                     select p).FirstOrDefault();

                    if (checkplan != null)
                    {
                        planCheck = new planCheck();
                        planCheck.status = "พบข้อมูล";
                        planCheck.planName = checkplan.sPlaneName;
                        planCheck.planCode = checkplan.courseCode;
                        planCheckList.Add(planCheck);
                    }
                    else
                    {
                        planCheck = new planCheck();
                        planCheck.status = "ไม่พบข้อมูล";
                        planCheck.planName = "";
                        planCheck.planCode = word;
                        planCheckList.Add(planCheck);
                    }
                }

                rss = new JArray(from a in planCheckList
                                 select new JObject(
                       new JProperty("planName", a.planName),
                       new JProperty("status", a.status),
                       new JProperty("planCode", a.planCode)
                    ));

                //context.Response.Expires = -1;
                //context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                //context.Response.ContentType = "application/json";
                ////context.Response.ContentEncoding = Encoding.UTF8;
                //context.Response.Write(rss);
                //context.Response.End();

                context.Response.Expires = -1;
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";
                //context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(rss);
                context.Response.Flush(); // Sends all currently buffered output to the client.
                context.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                context.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected class planCheck
        {
            public string status { get; set; }
            public string planName { get; set; }
            public string planCode { get; set; }
        }

    }


}