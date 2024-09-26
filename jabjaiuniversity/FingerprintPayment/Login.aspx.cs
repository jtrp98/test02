using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OpenData(Request.QueryString["id"] + "");
            // OpenData("2");
        }

        private void OpenData(string sID)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                int UserId = int.Parse(sID);
                var tUser = dbmaster.TUsers.Find(UserId);
                var tCompany = dbmaster.TCompanies.Find(tUser.nCompany);
                sID = tUser.nSystemID.ToString();
                Session["sEntities"] = tCompany.sEntities;
                Session["nCompany"] = tCompany.nCompany;
                HttpContext.Current.Session["sEntities"] = tCompany.sEntities;
                try
                {
                    JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"] + ""));
                    int nid = int.Parse(sID);
                    Session["sEmpID"] = sID;
                    _db.SaveChanges();
                    database.InsertLog(sID + "", "Login เข้าระบบ",
                        HttpContext.Current.Session["sEntities"].ToString(),
                        Request, 999, 0, 0);
                    Response.Redirect("AdminMain.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.StackTrace.ToString());
                    //Response.Redirect("Default.aspx");
                }
            }
            //    DataTable _dt = fcommon.Get_Data(fcommon.connMaster, @"SELECT * FROM TUser INNER JOIN TCompany ON TCompanies.nCompany = TUsers.nCompany
            //    WHERE sID = " + sID);
            //foreach (DataRow _dr in _dt.Rows)
            //{


            //    //HttpCookie aCookie = new HttpCookie("userInfo");
            //    //aCookie.Values["sEntities"] = _dr["sEntities"] + "";
            //    //aCookie.Values["nCompany"] = _dr["nCompany"] + "";
            //    //aCookie.Expires = DateTime.Now.AddDays(900);
            //    //HttpContext.Current.Response.Cookies.Add(aCookie);

            //    //Response.Cookies["sEntities"].Value = _dr["sEntities"] + "";
            //}

        }
    }
}