using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class macadd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //timecode.Tick += Timecode_Tick;
            if (!this.IsPostBack)
            {
                if (string.IsNullOrEmpty(Session["sAverment"] + ""))
                {
                    Session["sAverment"] = "";
                    //CheckNumber(RandomNumber());
                    //Session["sTime"] = DateTime.Now;
                }
                else
                {
                    //if (CheckNumber(Session["sAverment"] + ""))
                    //{
                    //    CheckNumber(RandomNumber());
                    //    Session["sTime"] = DateTime.Now;
                    //}
                }
                //lbltop.Text = Session["sAverment"] + "";
            }
        }

        private void Timecode_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sAverment"] + ""))
            {
                CheckNumber(RandomNumber());
                Session["sTime"] = DateTime.Now;
            }
            else
            {
                if (CheckNumber(Session["sAverment"] + ""))
                {
                    CheckNumber(RandomNumber());
                    Session["sTime"] = DateTime.Now;
                }
            }
            lbltop.Text = Session["sAverment"] + "";         
            DateTime _dateStart = DateTime.Parse(Session["sTime"] + "");
            Double _time = (_dateStart.AddMinutes(5) - DateTime.Now).TotalSeconds;
            lblTime.Text = string.Format("{0:00}:{1:00}", _time / 60, _time % 60);

        }

        private string RandomNumber()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            numIterations = rand.Next(100000, 999999);
            lbltop.Text = numIterations.ToString();
            Session["sAverment"] = numIterations.ToString();

            return numIterations.ToString();
        }

        public static string DetermineCompName(string IP)
        {
            IPAddress myIP = IPAddress.Parse(IP);
            IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
            List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
            return compName.First();
        }

        private bool CheckNumber(string sAverment)
        {
            DataTable _dt = fcommon.Get_Data(fcommon.connMaster, @"SELECT * FROM TAverment where sAverment = '" + sAverment + "' AND DATEADD(MINUTE,-5,GETDATE()) < dAdd AND sMac IS NULL ");

            if (_dt.Rows.Count == 0)
            {
                int nAvermentID = 1;
                if (fcommon.Get_Data(fcommon.connMaster, "SELECT * FROM TAverment").Rows.Count > 0) nAvermentID = int.Parse(fcommon.Get_Value(fcommon.connMaster, "SELECT top 1 nAvermentID FROM TAverment order by nAvermentID desc")) + 1;
                string SQL = "INSERT INTO [TAverment] ([nAvermentID],[sAverment],[nCompany],[dAdd])VALUES (" + nAvermentID + ",'" + sAverment + "','" + Session["sEntities"] + "',GETDATE())";
                fcommon.ExecuteNonQuery(fcommon.connMaster, SQL);
                Session["nAvermentID"] = nAvermentID;
            }

            return _dt.Rows.Count == 0;
        }
    }
}