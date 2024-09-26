using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiMainClass;
using System.Globalization;
using MasterEntity;
using JabjaiEntity.DB;
using System.Data.Entity;

namespace FingerprintPayment
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnLogin.Click += BtnLogin_Click;
            //JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            //hlkRegister.Visible = _db.TEmployees.ToList().Count == 0;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbsmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var tUser = dbsmaster.TUsers;
                var tCompanies = dbsmaster.TCompanies;
                var q1 = await (from a in tUser
                          join b in tCompanies on a.nCompany equals b.nCompany
                          where a.sIdentification == txtsid.Text && a.cType == "1" && a.cDel == null
                          select new
                          {
                              a.nSystemID,
                              a.nCompany,
                              b.sEntities,
                              a.dBirth
                          }).ToListAsync();

                if (q1.Count() > 0)
                {
                    try
                    {
                        DateTime pass = DateTime.ParseExact(txtspass.Text, "ddMMyyyy", new CultureInfo("en-us"));
                        if (pass.Year > 2400) pass = DateTime.ParseExact(txtspass.Text, "ddMMyyyy", new CultureInfo("th-th"));
                        var quser = q1.Where(w => w.dBirth == pass).FirstOrDefault();
                        if (quser != null)
                        {
                            string sID = quser.nSystemID.ToString();
                            Session["sEntities"] = quser.sEntities;
                            Session["nCompany"] = quser.nCompany;
                            string sEntities = quser.sEntities;
                            try
                            {
                                int nid = int.Parse(sID);
                                Session["sEmpID"] = sID;
                                database.InsertLog(sID, "Login เข้าระบบ", quser.sEntities,
                                    Request, 999, 0, 0);
                                Response.Redirect("AdminMain.aspx");
                            }
                            catch (Exception ex)
                            {
                                Response.Write(ex.StackTrace.ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.StackTrace.ToString());
                    }
                }

            }

        }
    }
}
