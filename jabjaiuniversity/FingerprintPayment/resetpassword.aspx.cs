using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiMainClass;

namespace FingerprintPayment
{
    public partial class resetpassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GenOTP(Request.QueryString["id"].ToString());
            }
        }

        private void GenOTP(string repassid)
        {
            foreach (DataRow _dr in fcommon.Get_Data(fcommon.connMaster, "SELECT * FROM TResetPassword WHERE ID = " + repassid).Rows)
            {
                DateTime dExpActive = DateTime.Parse(_dr["dExpActive"] + "");
                string OTP = _dr["OTP"] + "";
                if (DateTime.Now > dExpActive)
                {
                    Response.Write("");
                }
                else {
                    if (string.IsNullOrEmpty(OTP))
                    {
                        OTP = RandomNumber();
                        fcommon.ExecuteNonQuery(fcommon.connMaster, "UPDATE TResetPassword SET OTP = '" + OTP + "',dExpUpdate = '" + DateTime.Now.AddHours(6).ToString("MM/dd/yyyy HH:mm") + "' WHERE ID = " + repassid);
                        Response.Write(OTP);
                    }
                    else {
                        DateTime dExpUpdate = DateTime.Parse(_dr["dExpUpdate"] + "");
                        if (DateTime.Now > dExpUpdate)
                        {
                            Response.Write("");
                        }
                        else {
                            Response.Write(OTP);
                        }
                    }
                }
            }
        }

        private string RandomNumber()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            numIterations = rand.Next(1000, 999999);
            if (fcommon.Get_Data(fcommon.connMaster, "SELECT * FROM TResetPassword WHERE OTP = '" + numIterations + "'  AND (cStatus = '1' OR dExpUpdate < '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "')").Rows.Count > 0)
            {
                RandomNumber();
            }
            return numIterations.ToString();
        }
    }
}