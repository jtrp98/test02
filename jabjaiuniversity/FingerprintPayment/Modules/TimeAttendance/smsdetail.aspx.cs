using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using System.Configuration;
using System.IO;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class smsdetail : SMSGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + ""))
            {
                Response.Redirect("~/Default.aspx");
            }

            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnDelete.Click += BtnDelete_Click;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string _queryString = Request.QueryString["id"] ?? "0";
                int nSMSId = int.Parse(_queryString);
                var f_SMS = entities.TSMS.FirstOrDefault(f => f.nSMS == nSMSId);
                var messageBox = entities.TMessageBoxes.FirstOrDefault(f => f.push_id == nSMSId);
                if (f_SMS != null)
                {
                    f_SMS.isDel = true;
                    f_SMS.cDel = true;
                    f_SMS.UpdatedDate = DateTime.Now;
                    f_SMS.UpdatedBy = UserData.UserID;
                    entities.SaveChanges();
                }
                if (messageBox != null)
                {
                    messageBox.cDel = true;
                    messageBox.UpdatedDate = DateTime.Now;
                    messageBox.UpdatedBy = UserData.UserID;
                }
                Response.Redirect("smssettings.aspx");
            }

        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("smssettings.aspx");
        }
    }
}