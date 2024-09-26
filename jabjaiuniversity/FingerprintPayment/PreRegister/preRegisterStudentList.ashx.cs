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

namespace FingerprintPayment.PreRegister
{
    /// <summary>
    /// Summary description for preRegisterStudentList
    /// </summary>
    public class preRegisterStudentList : IHttpHandler, IRequiresSessionState
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

                var q_title = _db.TTitleLists.ToList();

                List<stdDetail> stdDetailList = new List<stdDetail>();
                stdDetail stdDetail = new stdDetail();


                foreach (var data1 in _db.TPreRegisters.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel != 1))
                {



                    stdDetail = new stdDetail();
                    stdDetail.fullName = data1.sName + " " + data1.sLastname;
                    stdDetail.firstName = data1.sName;
                    stdDetail.preRegisterId = data1.preRegisterId;
                    stdDetailList.Add(stdDetail);
                }

                var newSortList4 = stdDetailList.OrderBy(x => x.firstName).ToList();


                rss = new JArray(from a in newSortList4
                                 select new JObject(

                       new JProperty("fullName", a.fullName),
                       new JProperty("preRegisterId", a.preRegisterId)

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



        protected class stdDetail
        {

            public int preRegisterId { get; set; }
            public string fullName { get; set; }
            public string firstName { get; set; }
        }
    }


}