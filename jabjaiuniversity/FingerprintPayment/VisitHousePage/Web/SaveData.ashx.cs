using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using TUser = JabjaiEntity.DB.TUser;

namespace FingerprintPayment.VisitHousePage.Web
{
    /// <summary>
    /// Summary description for SaveData
    /// </summary>
    public class SaveData : IHttpHandler , IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            context.Response.ContentType = "text/plain";
            var sId = context.Request.QueryString["sid"];
         
            var uploadedFile = context.Request.Files; //only uploading one file
            var delfile = context.Request.Form["delfile"];
            var data = context.Request.Form["data"];
            //var sig1 = context.Request.Form["sig1"];
            //var sig2 = context.Request.Form["sig2"];
            //var sig3 = context.Request.Form["sig3"];

            var model = JsonConvert.DeserializeObject<THomeVisit>(data);
            int id;
            string status;
            string msg;

            TUser user;
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
               
                var repo = new VisitHomeRepository(ctx);

                model.StudentID = Convert.ToInt32(sId);
                model.SchoolID = userData.CompanyID;

                user = ctx.TUser.FirstOrDefault(o => o.sID == model.StudentID);

                (id, status, msg) = repo.SaveFormDataSystem(model
                            , uploadedFile.GetMultiple("attFile1")
                            , uploadedFile.GetMultiple("attFile2")
                            , userData
                            , delfile.Split(',').Where(o => !string.IsNullOrWhiteSpace(o)).Select( o => Convert.ToInt32(o)).ToArray());
            }

            if (status != "ok")
            {
                var err = new
                {
                    module = "VisitHousePage.Web/SaveData.ashx",
                    message = msg,
                    data = model,
                };
                var line = new LINENotify();
                line.LineSBErrorSend(LINENotify.TOKEN_VISITHOME, JsonConvert.SerializeObject(err));
            }
            else
            {
                database.InsertLog(userData.UserID + "", $"บันทึกการเยี่ยมบ้านของนักเรียน {user.sName + " " + user.sLastname}", HttpContext.Current.Request, 27, 0, 0, userData.CompanyID);
            }

            context.Response.Expires = -1;
            //context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            context.Response.Write(JsonConvert.SerializeObject(new { status = status, id = id , msg = msg }));
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
}