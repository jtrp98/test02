using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace FingerprintPayment.import
{
    /// <summary>
    /// Summary description for checkStudent
    /// </summary>
    public class checkStudent : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
               
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                dynamic rss = new JObject();

                var q_user = _db.TB_StudentViews.Where(w => w.SchoolID == nCompany.nCompany && w.cDel == null).AsQueryable().ToList();
                stdCheck stdCheck = new stdCheck();
                List<stdCheck> stdCheckList = new List<stdCheck>();

                string data = fcommon.ReplaceInjection(context.Request["id"]);
                // Split string on spaces (this will separate all the words).
                string[] words = data.Split('~');
                foreach (string word in words)
                {
                    var check = q_user.Where(w => w.sStudentID == word && w.cDel == null).FirstOrDefault();
                    if (check != null)
                    {
                        stdCheck = new stdCheck();
                        stdCheck.status = "พบข้อมูล";
                        stdCheck.stdName = check.sName + " " + check.sLastname;
                        stdCheck.stdCode = check.sStudentID;
                        stdCheck.stdSid = check.sID.ToString();
                    }
                    else
                    {
                        stdCheck = new stdCheck();
                        stdCheck.status = "ไม่พบข้อมูล";
                        stdCheck.stdName = "";
                        stdCheck.stdCode = word;
                        stdCheck.stdSid = "";
                    }
                    stdCheckList.Add(stdCheck);
                }







                rss = new JArray(from a in stdCheckList
                                 select new JObject(
                           new JProperty("stdName", a.stdName),
                           new JProperty("stdCode", a.stdCode),
                           new JProperty("status", a.status),
                           new JProperty("stdSid", a.stdSid)
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

        protected class stdCheck
        {
            public string status { get; set; }
            public string stdName { get; set; }
            public string stdCode { get; set; }
            public string stdSid { get; set; }
        }
    }


}