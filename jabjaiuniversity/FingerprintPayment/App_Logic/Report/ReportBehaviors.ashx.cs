using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.App_Logic.Report
{
    /// <summary>
    /// Summary description for ReportBehaviors
    /// </summary>
    public class ReportBehaviors : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current.Session["sEntities"] == null) context.Response.Redirect("/Default.aspx");
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";

            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
       
                DateTime dStart = DateTime.Today; //DateTime.Today.AddDays(-120);
                DateTime dEnd = DateTime.Today; //DateTime.Today.AddDays(-90);
                dynamic rss;
                if (!string.IsNullOrEmpty(context.Request["day"]))
                {
                    string day = context.Request["day"].ToString();
                    dStart = DateTime.ParseExact(day, "dd/MM/yyyy", new CultureInfo("en-us"));
                    if (!string.IsNullOrEmpty(context.Request["end"]))
                    {
                        string end = context.Request["end"].ToString();
                        dEnd = DateTime.ParseExact(end, "dd/MM/yyyy", new CultureInfo("en-us"));
                    }
                    else
                    {
                        dEnd = dStart;
                    }
                }

                var lstudent = (from d in db.TBehaviorHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).ToList()
                                join e in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList() on d.UserAdd equals e.sEmp
                                join a in db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList() on d.StudentId equals a.sID
                                join b in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).ToList() on a.nTermSubLevel2 equals b.nTermSubLevel2
                                join c in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList() on b.nTSubLevel equals c.nTSubLevel
                                orderby d.dAdd descending
                                where a.cDel == null
                                select new
                                {
                                    studentid = a.sID,
                                    studentname = a.sName,
                                    studentlastname = a.sLastname,
                                    classname = c.SubLevel.Trim() + "/" + b.nTSubLevel2,
                                    b.nTermSubLevel2,
                                    c.nTSubLevel,
                                    teachername = e.sName,
                                    teacherlastname = e.sLastname,
                                    d.dAdd,
                                    d.Score,
                                    d.ResidualScore,
                                    d.Type,
                                    d.Note,
                                    d.BehaviorName,
                                    d.dAdd.Value
                                }).ToList();

                if (!string.IsNullOrEmpty(context.Request["level2"]))
                {
                    int level2id = int.Parse(context.Request["level2"]);
                    lstudent = lstudent.Where(w => w.nTermSubLevel2 == level2id).ToList();
                }
                else if (!string.IsNullOrEmpty(context.Request["level"]))
                {
                    int levelid = int.Parse(context.Request["level"]);
                    lstudent = lstudent.Where(w => w.nTSubLevel == levelid).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request["studentid"]))
                {
                    int studentid = int.Parse(context.Request["studentid"]);
                    lstudent = lstudent.Where(w => w.studentid == studentid).ToList();
                }

                rss = new JArray(from a in lstudent.ToList()
                                 select new JObject(
                                     new JProperty("studentname", a.studentname),
                                     new JProperty("day", a.dAdd.Value.ToString("dd/MM/yyyy HH:mm:ss")),
                                     new JProperty("studentlastname", a.studentlastname),
                                     new JProperty("behaviorname", a.BehaviorName),
                                     new JProperty("score", a.Score),
                                     new JProperty("residualscore", a.ResidualScore),
                                     new JProperty("teachername", a.teachername),
                                     new JProperty("teacherlastname", a.teacherlastname),
                                     new JProperty("note", a.Note),
                                     new JProperty("behaviortype", (a.Type == "0" ? "เพิ่ม" : "ลด"))
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
    }
}