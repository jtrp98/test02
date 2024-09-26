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
    public partial class RegisterExamResult : System.Web.UI.Page
    {
        protected string registerYearBE = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string entities = ComFunction.Rot13Transform(Request.QueryString["id"]);
            int schoolID = Convert.ToInt32(entities.Replace(RegisterGateway.GetLettersShuffle(), ""));
            
            Session["RegisterOnlineEntities"] = entities;
            Session["RegisterOnlineSchoolID"] = schoolID;

            //string schoolEntities = (string)Session["RegisterOnlineEntities"];

          
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                var company = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();

                if (company != null)
                {
                    //Session["RegisterOnlineSchoolID"] = company.nCompany;
                    Session["RegisterOnlineSchoolLogo"] = company.sImage;
                    Session["RegisterOnlineSchoolName"] = company.sCompany;
                }

                string sqlQuery = string.Format(@"SELECT DISTINCT [Year] FROM TRegisterSetup WHERE SchoolID = {0} ORDER BY [Year] DESC", schoolID);
                int year = en.Database.SqlQuery<int>(sqlQuery).FirstOrDefault();

                Session["RegisterOnlineYear"] = year - 543;
                Session["RegisterOnlineYearBE"] = year;

                registerYearBE = Convert.ToString(Session["RegisterOnlineYearBE"]);
            }

            Response.Redirect("RegisterExamResultCheck.aspx");
        }
    }
}