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
using JabjaiSchoolGradeEntity;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for bp5GradeRegister
    /// </summary>
    public class bp5GradeRegister : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade());
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

            dynamic rss = new JObject();
            string userterm = fcommon.ReplaceInjection(context.Request["term"]);
            string year = fcommon.ReplaceInjection(context.Request["year"]);
            string id = fcommon.ReplaceInjection(context.Request["id"]);
            string idlv2 = fcommon.ReplaceInjection(context.Request["idlv2"]);
            int idlv2n = int.Parse(idlv2);

            int? useryear = Int32.Parse(year);
            int nyear = 0;
            string nterm = "";

            foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear && userData.CompanyID == w.SchoolID))
            {
                nyear = ff.nYear;
            }

            foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && userData.CompanyID == w.SchoolID && w.cDel == null))
            {
                nterm = ee.nTerm;
            }

            var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
            var data = _dbGrade.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();

            bp5register bp5register = new bp5register();
            List<bp5register> bp5registerList = new List<bp5register>();

            int number = 1;
            if (data != null)
            {
                var tGradeDetails = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == data.nGradeId).ToList();
                foreach (var data2 in tGradeDetails.Where(w => w.cDel == false))
                {
                    bp5register = new bp5register();
                    bp5register.number = number;
                    bp5register.sID = data2.sID ;
                    bp5register.ddlother = data2.getSpecial;
                    bp5registerList.Add(bp5register);
                    number = number + 1;
                }
            }



            rss = new JArray(from a in bp5registerList
                             select new JObject(
                   new JProperty("number", a.number),
                  new JProperty("ddlother", a.ddlother),
                 new JProperty("sID", a.sID)
                ));


            context.Response.Expires = -1;
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            //context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(rss);
            context.Response.End();
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    class bp5register
    {
        public int number { get; set; }
        public string ddlother { get; set; }
        public int sID { get; set; }

    }

}