using FingerprintPayment.Class;
using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterStart : System.Web.UI.Page
    {
        // All session use in module
        // Session["RegisterOnlineEntities"]
        // Session["RegisterOnlineSchoolID"]
        // Session["RegisterOnlineFileBase64"]
        // Session["ActivePage"]
        // Session["RegisterID"]
        // Session["RegisterPrintID"]
        // Session["RegisterF"]

        protected string enSourceId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            enSourceId = Request.QueryString["id"];
            string f = (string)Request.QueryString["f"]; // f = from
            string rid = (string)Request.QueryString["rid"]; // registerID
            int.TryParse(rid, out int preRegisterId);

            if (!string.IsNullOrEmpty(enSourceId))
            {
                string entities = ComFunction.Rot13Transform(enSourceId);

                bool success = Int32.TryParse(entities.Replace(RegisterGateway.GetLettersShuffle(), ""), out int schoolID);
                if (success)
                {
                    Session.Timeout = 60;
                    Session["RegisterOnlineEntities"] = entities;
                    Session["RegisterOnlineSchoolID"] = schoolID;

                    //string schoolEntities = (string)Session["RegisterOnlineEntities"];

                    
                    using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        var company = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();

                        if (company != null)
                        {
                            //Session["RegisterOnlineSchoolID"] = company.nCompany;
                            Session["RegisterOnlineSchoolLogo"] = company.sImage;
                            Session["RegisterOnlineSchoolName"] = company.sCompany;

                            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                            {
                                if (string.IsNullOrEmpty(f) && string.IsNullOrEmpty(rid))
                                {
                                    // Get year from register settings
                                    string sqlQuery = string.Format(@"SELECT DISTINCT [Year] FROM TRegisterSetup WHERE SchoolID = {0} AND DATEADD(ms, -3, CONVERT(VARCHAR(10), DATEADD(d, 1, SubmitDocumentDate), 111)) <= GETDATE() AND GETDATE() <= DATEADD(ms, -3, CONVERT(VARCHAR(10), DATEADD(d, 1, EndDate), 111)) ORDER BY [Year] DESC", schoolID);
                                    int year = en.Database.SqlQuery<int>(sqlQuery).FirstOrDefault();

                                    Session["RegisterOnlineYear"] = year - 543;
                                    Session["RegisterOnlineYearBE"] = year;
                                }
                                else
                                {
                                    // Get year from register data
                                    var preRegister = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == preRegisterId).FirstOrDefault();
                                    if (preRegister != null)
                                    {
                                        Session["RegisterOnlineYear"] = preRegister.registerYear;
                                        Session["RegisterOnlineYearBE"] = preRegister.registerYear + 543;
                                    }
                                }
                            }

                            Session["ActivePage"] = 0;
                        }
                        else
                        {
                            enSourceId = "";
                            this.ltrMessage.Text = "กรุณาเข้า url จากหน้าเว็บโรงเรียนเท่านั้น!";
                        }
                    }
                }
                else
                {
                    enSourceId = "";
                    this.ltrMessage.Text = "กรุณาเข้า url จากหน้าเว็บโรงเรียนเท่านั้น!";
                }
            }
            else
            {
                this.ltrMessage.Text = "กรุณาเข้า url จากหน้าเว็บโรงเรียนเท่านั้น!";
            }

            if (!string.IsNullOrEmpty(rid))
            {
                Session["RegisterID"] = preRegisterId;

                if (string.IsNullOrEmpty(f))
                {
                    // If ever the recording is complete
                    Session["RegisterF"] = null;
                }
                else if (f == "sendmail")
                {
                    // From send mail section
                    Session["RegisterF"] = f;
                }

                Response.Redirect("RegisterOnline01.aspx");
            }
            else
            {
                Session["RegisterID"] = null;
                Session["RegisterF"] = null;
            }
            //
        }
    }
}