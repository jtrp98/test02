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
    /// Summary description for branchList
    /// </summary>
    public class branchList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            dynamic rss = new JObject();

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                var q_usermaster = _dbMaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();

                List<String> branchList = new List<string>();
                List<branchSpec> SpecList1 = new List<branchSpec>();
                branchSpec Spec1 = new branchSpec();

                SpecList1.AddRange((from a in _db.TBranches.Where(w => w.SchoolID == userData.CompanyID)
                                    join b in _db.TBranchSubjects.Where(w => w.SchoolID == userData.CompanyID) on a.BranchId equals b.BranchId
                                    join c in _db.TBranchSpecs.Where(w => w.SchoolID == userData.CompanyID) on b.BranchSubjectId equals c.BranchSubjectId
                                    where a.cDel == null && b.cDel == null && c.cDel == null

                                    select new branchSpec
                                    {
                                        branchSpecId = c.BranchSpecId,
                                        name = c.BranchSpecName,
                                    }
                                 ).ToList());

                foreach (var t in SpecList1)
                {
                    branchList.Add(t.name);
                }

                branchList = branchList.Distinct().ToList();


                var newSortList4 = branchList.OrderBy(x => x).ToList();


                rss = new JArray(from a in newSortList4
                                 select new JObject(

                       new JProperty("name", a)

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



        class branchSpec
        {

            public int branchSpecId { get; set; }
            public string name { get; set; }
        }
    }


}