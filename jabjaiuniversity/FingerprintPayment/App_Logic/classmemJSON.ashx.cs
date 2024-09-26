using AutoMapper;
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

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for dataJSON1
    /// </summary>
    public class classmemJSON : IHttpHandler, IRequiresSessionState
    {
        //[Inject]
        //public ICommonService CommonService { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                    var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                    dynamic rss = new JObject();
                    string sMode = fcommon.ReplaceInjection(context.Request["mode"]);
                    string id = fcommon.ReplaceInjection(context.Request["id"]);
                    string term = fcommon.ReplaceInjection(context.Request["term"]);
                    switch (sMode)
                    {

                        case "classmember":
                            #region
                            int idlv2 = int.Parse(id);
                            var check = db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2 && w.nTerm == term).FirstOrDefault();

                            rss = new JArray(from a in db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2 && w.nTerm == term).ToList()
                                             join b in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null) on a.nTeacherHeadid equals b.sEmp into jab
                                             from jb in jab.DefaultIfEmpty()

                                             join c in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null) on a.nTeacherAssistOne equals c.sEmp into jac
                                             from jc in jac.DefaultIfEmpty()

                                             join d in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null) on a.nTeacherAssistTwo equals d.sEmp into jad
                                             from jd in jad.DefaultIfEmpty()

                                             select new JObject(
                                                 new JProperty("head1", jb == null ? "" : jb.sName + " " + jb.sLastname),
                                                 new JProperty("headid", a.nTeacherHeadid),
                                                 new JProperty("one", jc == null ? "" : jc.sName + " " + jc.sLastname),
                                                 new JProperty("oneid", a.nTeacherAssistOne),
                                                 new JProperty("two", jd == null ? "" : jd.sName + " " + jd.sLastname),
                                                 new JProperty("twoid", a.nTeacherAssistTwo)
                                             ));

                            #endregion
                            break;

                        case "department":
                            int departid = int.Parse(id);

                            var q = (from a in db.TDepartments.Where(w => w.SchoolID == userData.CompanyID)
                                     join b in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.userHeadId equals b.sEmp into jab
                                     from jb in jab.DefaultIfEmpty()

                                     join c in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.userApproveOne equals c.sEmp into jac
                                     from jc in jac.DefaultIfEmpty()

                                     join d in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.userApproveTwo equals d.sEmp into jad
                                     from jd in jad.DefaultIfEmpty()

                                     where a.DepID == departid
                                     select new
                                     {
                                         departname = a.departmentName,
                                         head1 = jb == null ? "" : jb.sName + " " + jb.sLastname,
                                         headid = a.userHeadId,
                                         one = jc == null ? "" : jc.sName + " " + jc.sLastname,
                                         oneid = a.userApproveOne,
                                         two = jd == null ? "" : jd.sName + " " + jd.sLastname,
                                         twoid = a.userApproveTwo
                                     }).ToList();

                            rss = new JArray(from a in q

                                             select new JObject(
                                                 new JProperty("departname", a.departname),
                                                 new JProperty("head1", a.head1),
                                                 new JProperty("headid", a.headid),
                                                 new JProperty("one", a.one),
                                                 new JProperty("oneid", a.oneid),
                                                 new JProperty("two", a.two),
                                                 new JProperty("twoid", a.twoid)
                                             ));

                            break;

                        case "teacherlist":
                            var tMaster = _dbMaster.TUsers.Where(w => w.nCompany == nCompany.nCompany && w.cType == "1" && w.cDel == null).ToList();
                            var emp = db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cType == "1" && w.cDel != "1").ToList();
                            rss = new JArray(from a in emp
                                             select new JObject(
                                    new JProperty("value", a.sEmp),
                                    new JProperty("label", a.sName + " " + a.sLastname)
                                ));

                            break;

                       
                    }

                    context.Response.Expires = -1;
                    context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                    context.Response.ContentType = "application/json";
                    //context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.Write(rss);
                    context.Response.End();
                }
            }
        }

        private string GetDayName(int nPlaneDay)
        {
            switch (nPlaneDay)
            {
                case 0: return "su";
                case 1: return "mo";
                case 2: return "tu";
                case 3: return "we";
                case 4: return "th";
                case 5: return "fr";
                case 6: return "sa";
                default: return "";
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    class ratio
    {
        public int? sID { get; set; }
        public string grade40 { get; set; }
        public string grade35 { get; set; }
        public string grade30 { get; set; }
        public string grade25 { get; set; }
        public string grade20 { get; set; }
        public string grade15 { get; set; }
        public string grade10 { get; set; }
        public string grade0 { get; set; }
        public string grade40per { get; set; }
        public string grade35per { get; set; }
        public string grade30per { get; set; }
        public string grade25per { get; set; }
        public string grade20per { get; set; }
        public string grade15per { get; set; }
        public string grade10per { get; set; }
        public string grade0per { get; set; }
        public string xbar { get; set; }
        public string sd { get; set; }
        public string cv { get; set; }
        public string totalstudent { get; set; }
        public string ror { get; set; }
        public string MS { get; set; }
        public string MK { get; set; }
        public string P { get; set; }
        public string MP { get; set; }
        public string other { get; set; }
        public string rorper { get; set; }
        public string MSper { get; set; }
        public string MKper { get; set; }
        public string Pper { get; set; }
        public string MPper { get; set; }
        public string otherper { get; set; }
        public string B0 { get; set; }
        public string B1 { get; set; }
        public string B2 { get; set; }
        public string B3 { get; set; }
        public string R0 { get; set; }
        public string R1 { get; set; }
        public string R2 { get; set; }
        public string R3 { get; set; }
        public string header { get; set; }
        public string header2 { get; set; }
        public string header3 { get; set; }
    }
}
