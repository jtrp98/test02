using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class report07 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            if (!this.IsPostBack)
            {
                DataTable _dt = fcommon.LinqToDataTable(_db.TSubLevels.ToList());
                fcommon.ListDataTableToDropDownList(_dt, ddlcType, "ทั้งหมด", "nTSubLevel", "SubLevel");
            }
            lodareport();
        }
        void Button1_Click(object sender, EventArgs e)
        {
            lodareport();
        }
        private void lodareport()
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            header = string.Empty;
            //Report.cryreport07 reportdocument = new Report.cryreport07();
            string SQL = @"select sName,sLastname,sIdentification,dBirth,TSubLevels.SubLevel,nMax,nMoney
             from TUser inner join TStudentLevel on TUsers.sID = TStudentLevels.sID
             inner join TSubLevel ON TSubLevels.nTSubLevel = TStudentLevels.nTSubLevel
            where cType = '0' AND sName + ' ' + sLastname LIKE '%" + txtSearch.Text + "%' ";
            if (ddlcType.SelectedIndex > 0)
                SQL += " AND TStudentLevels.nTSubLevel =  " + ddlcType.SelectedValue;

            if (ddlSearch.SelectedValue == "1")
                SQL += " AND nMoney = 0";
            else if (ddlSearch.SelectedValue == "2")
                SQL += " AND nMoney > 0";
            
            SQL += " order by SubLevel";
            DataTable _dt = fcommon.Get_Data(_conn, SQL);

            lvCustomers.DataSource = _dt;
            lvCustomers.DataBind();
        }
        private static string CountNumberProduct(string sBarCode, JabJaiEntities _dbs)
        {
            int? nCount = 0;
            int? _ProductNume = _dbs.TProducts.Where(w => w.sBarCode == sBarCode && string.IsNullOrEmpty(w.cDel)).FirstOrDefault().nNumber;
            int? nProductID = _dbs.TProducts.Where(w => w.sBarCode == sBarCode && string.IsNullOrEmpty(w.cDel)).FirstOrDefault().nProductID;
            int? nStock = _dbs.TStockDetails.Where(w => w.nProductID == nProductID).Sum(s => s.nNumber);
            int? nSellNumber = _dbs.TSell_Detail.Where(w => w.nProduct == nProductID && string.IsNullOrEmpty(w.cDel)).Sum(s => s.nNumber);
            nCount = (_ProductNume + (nStock == null ? 0 : nStock)) - ((nSellNumber == null ? 0 : nSellNumber));
            return nCount.ToString();
        }
        protected void ddlcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            lodareport();
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            lodareport();
        }
        protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (lvCustomers.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            this.lodareport();
        }

        public string header = string.Empty;
        public int trHeader = 0;
        public string AddHeader()
        {
            string ans = Eval("SubLevel").ToString();

            if (ans == header)
                ans = "";
            else
                header = ans;

            if (ans == "")
            {
                trHeader = 0;
                return "";
            }
            else
            {
                trHeader = 1;
                return " <tr><td colspan='6' style='text-align: right;'>" + ans + "</td></tr>";
            }
        }

    }
}