using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using JabjaiEntity.DB;
using System.Globalization;
using JabjaiMainClass;
using MasterEntity;
using System.Web.Services;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class leavelist : System.Web.UI.Page
    {
        [WebMethod]
        public static string GetPermission()
        {
            return mp.Permission_Page.permission;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);
            //btnSearch.Click += new EventHandler(btnSearch_Click);
            if (!IsPostBack)
            {
                SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
                OpenData();
                int sEmpID = int.Parse(Session["sEmpID"] + "");
                fcommon.ListDBToDropDownList(_conn, ddlsublevel, "select * from TSubLevel", "- ทั้งหมด -", "nTSubLevel", "SubLevel");
            }
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            OpenData();
            //JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            //dgd.DataSource = _db.TUsers.Where(w => w.cType == "0" && (w.sName + " " + w.sLastname).Contains(txtSearch.Text) && string.IsNullOrEmpty(w.cDel)).ToList();
            //dgd.CurrentPageIndex = 0;
            //dgd.DataBind();
        }
        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                //DataRowView yourDataSource = (DataRowView)e.Item.DataItem;
                LinkButton _btnStdManage = e.Item.FindControl("btnEdit") as LinkButton;
                _btnStdManage.Attributes.Add("onclick", "modalLeave('" + e.Item.Cells[0].Text + "'); return false;");
                _btnStdManage.Attributes.Add("data-toggle", "modal");
                _btnStdManage.Attributes.Add("data-target", "#modalLeave");
                LinkButton _btnData = e.Item.FindControl("btnData") as LinkButton;
                _btnData.Attributes.Add("onclick", "modalLeaveData('" + e.Item.Cells[0].Text + "','" + e.Item.Cells[1].Text + " " + e.Item.Cells[2].Text + "'); return false;");
                _btnData.Attributes.Add("data-toggle", "modal");
                _btnData.Attributes.Add("data-target", "#modalLeaveData");
            }
        }
        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            sbyte sID = fcommon.FindIndexColumnOfDataFieldInGrid(dgd, "sID");
        }
        private void OpenData()
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            string SQL = "", SQLComm = "";
            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string sname = Request.QueryString["sname"];
            string lsittoday = Request.QueryString["lsittoday"];
            string type = Request.QueryString["type"];

            if (!string.IsNullOrEmpty(idlv)) SQLComm += " AND nTSubLevel = " + idlv;
            if (!string.IsNullOrEmpty(idlv2)) SQLComm += " AND nTSubLevel2 = " + idlv2;

            SQL = @"select *  from (select CONVERT(varchar, sID)+'U' AS sID,sName,sLastName,sIdentification,cDel,'U' AS sType
                    from TUser AS TB1 left join TTermSubLevel2 AS TB2 on TB1.nTermSubLevel2 = TB2.nTermSubLevel2
                    where cDel IS NULL " + SQLComm + @"
                    union
                    select CONVERT(varchar,sEmp)+'E' AS sID,sName,sLastName,sIdentification,cDel,'E' AS sType
                    from TEmployees) tb1
                    where cDel IS NULL and sType like '%" + type + "' ";

            if (!string.IsNullOrEmpty(sname)) SQL += " AND sName + ' ' + sLastname LIKE '%" + sname + "%'";
            if (!string.IsNullOrEmpty(lsittoday)) SQL += @" AND sID in( SELECT CONVERT(varchar, sID)+''+cTypeID
            FROM TLeave
            where '" + DateTime.Today.ToString("MM/dd/yyyy", new CultureInfo("en-us")) + "' between dLeaveStart and dLeaveStart)";

            dgd.DataSource = fcommon.Get_Data(_conn, SQL);
            dgd.DataBind();
        }
        protected void ddlcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            txtSearch.Text = "";
            dgd.DataSource = _db.TUsers.Where(w => w.cType == "0" && string.IsNullOrEmpty(w.cDel)).ToList();
            dgd.CurrentPageIndex = 0;
            dgd.DataBind();
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            dgd.DataSource = _db.TUsers.Where(w => w.cType == "0" && (w.sName + " " + w.sLastname).Contains(txtSearch.Text) && string.IsNullOrEmpty(w.cDel)).ToList();
            dgd.CurrentPageIndex = 0;
            dgd.DataBind();
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            JabJaiEntities _db2 = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            DataTable dtName = fcommon.LinqToDataTable(_db2.TUsers.Where(w => w.cDel == null && w.cType == "0" && (w.sName.Contains(prefixText) || w.sLastname.Contains(prefixText) || w.sIdentification.Contains(prefixText))));
            List<string> customers = new List<string>();
            if (dtName != null)
            {
                foreach (DataRow dr in dtName.Rows)
                {
                    customers.Add(dr["sName"] + " " + dr["sLastname"]);
                }
            }
            return customers;
        }
    }
}