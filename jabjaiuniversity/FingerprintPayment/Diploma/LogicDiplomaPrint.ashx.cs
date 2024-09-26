using FingerprintPayment.App_Logic;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.Diploma
{
    /// <summary>
    /// Summary description for LogicDiplomaPrint
    /// </summary>

    public class LogicDiplomaPrint : IHttpHandler, IRequiresSessionState
    {

        //JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read);

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string _mode = fcommon.ReplaceInjection(context.Request["Mode"]);
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, object> row;

                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                BP2 lGetData = new BP2();

                switch (_mode)
                {
                    #region GetData
                    case "GetData":

                        lGetData = GetDate(fcommon.ReplaceInjection(context.Request["sID"]));

                        var noRow = 0;
                        //foreach (var std in lGetData)
                        //{
                        //    row = new Dictionary<string, object>();
                        //    //noRow = noRow + 1;
                        //    //row.Add("noRow", noRow);
                        //    row.Add("fullName", std.fullName);
                        //    rows.Add(row);
                        //}

                        break;
                        #endregion
                }
                context.Response.Expires = -1;
                context.Response.ContentType = "application/json";

                context.Response.Write(serializer.Serialize(lGetData));

                context.Response.End();
            }
        }

        private class BP2
        {

            public int sID { get; set; }
            public string fullName { get; set; }
            public DateTime date { get; set; }
            public string day { get; set; }
            public string month { get; set; }
            public string year { get; set; }

            public DateTime? dateGrad { get; set; }
            public string dayGrad { get; set; }
            public string monthGrad { get; set; }
            public string yearGrad { get; set; }

            public string schoolName { get; set; }
            public string schoolDistrict { get; set; }
            public string schoolProvince { get; set; }



        }



        private BP2 GetDate(string sID)
        {

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

               
                var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                dataJSON dataJSON = new dataJSON();

                var q_title = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                var sIDint = Int32.Parse(sID);

                //var stdGrad = from sh in _db.TStudentHIstories
                //              where sh.nStudentId == sIDint
                //              select sh;

                var user = (from std in _db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID)
                            join u in _db.TUser.Where(w => w.SchoolID == userData.CompanyID) on std.sID equals u.sID
                            join g in _db.TStudentHIstories.Where(w => w.SchoolID == userData.CompanyID) on u.sID equals g.sID into g2
                            where std.sID == sIDint
                            from g in g2.DefaultIfEmpty()

                            select new BP2
                            {
                                fullName = std.titleDescription + u.sName + " " + u.sLastname,
                                date = u.dBirth.Value,
                                dateGrad = g.DayAdd.Value

                            }).AsQueryable().FirstOrDefault();



                user.day = user.date.Day.ToString();
                user.month = dataJSON.monthConv(user.date.Month.ToString());
                user.year = (user.date.Year + 543).ToString();

                if (user.dateGrad != null)
                {
                    user.dayGrad = user.dateGrad.Value.Day.ToString();
                    user.monthGrad = dataJSON.monthConv(user.dateGrad.Value.Month.ToString());
                    user.yearGrad = (user.dateGrad.Value.Year + 543).ToString();
                }
                else
                {
                    user.dayGrad = "-";
                    user.monthGrad = "-";
                    user.yearGrad = "-";
                }
                var school = qcompany.sCompany.Split(new string[] { "โรงเรียน" }, StringSplitOptions.None);

                user.schoolName = school[1];
                user.schoolDistrict = qcompany.sAumpher;
                user.schoolProvince = qcompany.sProvince;



                return user;
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