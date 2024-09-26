using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Web.DynamicData;
using System.Drawing;
using System.IO;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for branchEdit
    /// </summary>
    public class branchEdit : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
               

                    string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                dynamic rss = new JObject();
                string id = fcommon.ReplaceInjection(context.Request["id"]);
                int idn = int.Parse(id);
                string mode = fcommon.ReplaceInjection(context.Request["mode"]);

                var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                List<branchDetail> branchDetailList = new List<branchDetail>();
                branchDetail branchDetail = new branchDetail();

                if (mode == "1")
                {
                    var data1 = _db.TBranches.Where(w => w.BranchId == idn).FirstOrDefault();

                    branchDetail = new branchDetail();
                    branchDetail.branchId = data1.BranchId.ToString();
                    branchDetail.name = data1.BranchName;
                    branchDetail.nTLevel = data1.nTLevel.ToString();
                }

                if (mode == "2")
                {
                    var data2 = _db.TBranchSubjects.Where(w => w.BranchSubjectId == idn && w.SchoolID == userData.CompanyID).FirstOrDefault();
                    var data1 = _db.TBranches.Where(w => w.BranchId == data2.BranchId && w.SchoolID == userData.CompanyID).FirstOrDefault();
                    branchDetail = new branchDetail();
                    branchDetail.branchId = data1.BranchId.ToString();
                    branchDetail.branchName = data1.BranchName.ToString();
                    branchDetail.name = data2.BranchSubjectName;
                    branchDetail.nTLevel = data1.nTLevel.ToString();
                    branchDetail.subjectId = data2.BranchSubjectId.ToString();
                }

                if (mode == "3")
                {
                    var data3 = _db.TBranchSpecs.Where(w => w.BranchSpecId == idn && w.SchoolID == userData.CompanyID).FirstOrDefault();
                    var data2 = _db.TBranchSubjects.Where(w => w.BranchSubjectId == data3.BranchSubjectId && w.SchoolID == userData.CompanyID).FirstOrDefault();
                    var data1 = _db.TBranches.Where(w => w.BranchId == data2.BranchId && w.SchoolID == userData.CompanyID).FirstOrDefault();
                    branchDetail = new branchDetail();
                    branchDetail.branchId = data1.BranchId.ToString();
                    branchDetail.branchName = data1.BranchName.ToString();
                    branchDetail.name = data3.BranchSpecName;
                    branchDetail.nTLevel = data1.nTLevel.ToString();
                    branchDetail.subjectId = data2.BranchSubjectId.ToString();
                    branchDetail.subjectName = data2.BranchSubjectName;
                }

                branchDetailList.Add(branchDetail);


                rss = new JArray(from a in branchDetailList
                                 select new JObject(
                       new JProperty("branchId", a.branchId),
                      new JProperty("branchName", a.branchName),
                     new JProperty("name", a.name),
                     new JProperty("nTLevel", a.nTLevel),
                     new JProperty("subjectId", a.subjectId),
                     new JProperty("subjectName", a.subjectName)
                    ));
                context.Response.Expires = -1;
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";
                //context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(rss);
                context.Response.End();
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected class branchDetail
        {            
            public string name { get; set; }
            public string branchName { get; set; }
            public string branchId { get; set; }
            public string subjectName { get; set; }
            public string subjectId { get; set; }
            public string nTLevel { get; set; }            
        }


    }


}