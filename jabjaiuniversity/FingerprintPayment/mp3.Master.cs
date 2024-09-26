using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class mp3 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _page = HttpContext.Current.Request.CurrentExecutionFilePath.ToString();
            if (_page.IndexOf("useraddmoney") == -1 && _page.IndexOf("pagesellproduct") == -1)
            {

            }
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            if (_db.TEmployees.Count() == 0)
            {

            }
            else if (HttpContext.Current.Request.Url.ToString().IndexOf("checkmoney.aspx") != -1)
            {

            }
            else if (string.IsNullOrEmpty(Session["sEmpID"] + ""))
            {
                Response.Redirect("/Default.aspx");
            }
            else
            {
                //int sEmpID = int.Parse(Session["sEmpID"] + "");
                //foreach (var _data in _db.TEmployees.Where(w => w.sEmp == sEmpID))
                //{
                //    lbllogin.Text = _data.sName;
                //    //lbllogin.Text = "ยินดีต้อนรับคุณ " + _data.sName + " " + _data.sLastname + " เข้าสู่ระบบ";
                //}
            }
            if (!IsPostBack)
            {
                //fcommon.LED8("0", 2);            
            }
        }
        public HtmlGenericControl SetBody
        {
            get { return this.from2; }
        }
    }
}