using JabjaiMainClass;
using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using MasterEntity;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for deleteDataJSON
    /// </summary>
    public class deleteDataJSON : IHttpHandler, IRequiresSessionState
    {

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

          
            //string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            int schoolID = Convert.ToInt32(HttpContext.Current.Session["nCompany"]);
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {

                switch (context.Request["mode"])
                {
                    case "delschedule":
                        try
                        {
                            int id = int.Parse(context.Request["temp"]);
                            var Schedule = db.TSchedules.AsQueryable().Where(w => w.SchoolID == userData.CompanyID && w.sScheduleID == id).FirstOrDefault();
                            Schedule.cDel = true;
                            Schedule.UpdatedDate = DateTime.Now;
                            Schedule.UpdatedBy = userData.UserID;
                            db.SaveChanges();
                            context.Response.Write("Success");
                            var qplane = db.TPlanes.FirstOrDefault(f => f.SchoolID == schoolID && f.sPlaneID == Schedule.sPlaneID);

                            database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                                "ทำการลบข้อมูลตารางสอนวิชา " + qplane.sPlaneName + " รหัสตารางสอน : " + Schedule.sScheduleID,
                                HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 1, 4, 0);
                            context.Response.StatusCode = 200;
                        }
                        catch (Exception ex)
                        {
                            context.Response.Write("error : " + ex.Message);
                            context.Response.StatusCode = 500;
                        }
                        break;

                    case "delfinger":
                        var session = HttpContext.Current.Session;
                        using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                        {


                            //int nCompany = dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault().nCompany;
                            string sPassword = RandomNumber();
                            string type = context.Request["type"];
                            int userid = int.Parse(context.Request["userid"]);
                            string sName = "";
                            if (type == "0")
                            {
                                foreach (var data in dbMaster.TUsers.Where(w => w.cType == "0" && w.nSystemID == userid && w.nCompany == schoolID))
                                {
                                    if (data.sFinger == null && data.sFinger == null && data.sFinger == null)
                                    {
                                        sPassword = data.sPassword;
                                    }
                                    else
                                    {
                                        data.sFinger = null;
                                        data.sFinger2 = null;
                                        data.sFinger3 = null;
                                        data.sPassword = sPassword;
                                    }
                                    sName = data.sName + " " + data.sLastname;
                                }
                                database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                                 "ทำการลบลายนิ้วมือ " + sName,
                                 HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 14, 4, 0);


                            }
                            else
                            {
                                foreach (var data in dbMaster.TUsers.Where(w => w.cType != "0" && w.nSystemID == userid && w.nCompany == schoolID))
                                {
                                    if (data.sPassword == "000000")
                                    {
                                        data.sPassword = sPassword;
                                    }
                                    else if (data.sFinger == null && data.sFinger == null && data.sFinger == null)
                                    {
                                        sPassword = data.sPassword;
                                    }
                                    else
                                    {
                                        data.sFinger = null;
                                        data.sFinger2 = null;
                                        data.sFinger3 = null;
                                        data.sPassword = sPassword;
                                    }
                                    sName = data.sName + " " + data.sLastname;
                                }
                                database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                                    "ทำการลบลายนิ้วมือ " + sName,
                                    HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 13, 4, 0);
                            }

                            dbMaster.SaveChanges();
                            context.Response.Write(sPassword);
                            context.Response.StatusCode = 200;

                        }
                        break;
                }

                context.Response.ContentType = "text/plain";
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string RandomNumber()
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                Random rand = new Random((int)DateTime.Now.Ticks);
                int numIterations = 0;
                do
                {
                    numIterations = rand.Next(100000, 999999);

                } while (_dbMaster.TUsers.Where(w => w.sPassword == numIterations.ToString() && string.IsNullOrEmpty(w.sFinger)).ToList().Count > 0);

                return numIterations.ToString();
            }
        }
    }
}