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
    public partial class ProductBalanceReport : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            if (!this.IsPostBack)
            {
                var userData = GetUserData();
                DataTable _dt = fcommon.LinqToDataTable(_db.TTypes.Where(w => string.IsNullOrEmpty(w.cDel) && w.SchoolID == userData.CompanyID).ToList());
                fcommon.ListDataTableToDropDownList(_dt, ddlcType, "ทั้งหมด", "nTypeID", "sType");
                //lodareport();
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    hdfschoolname.Value = tCompany.sCompany;
                }
            }
        }
        void Button1_Click(object sender, EventArgs e)
        {
            lodareport();
        }

        private void lodareport()
        {
            var userData = GetUserData();
            string sEntities = Session["sEntities"] + "";
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                var tproduct = dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).ToList();
                if (!string.IsNullOrEmpty(txtSearch.Text)) tproduct = tproduct.Where(w => w.sBarCode.Contains(txtSearch.Text) || w.sProduct.Contains(txtSearch.Text)).ToList();

                if (ddlcType.SelectedIndex > 0)
                {
                    int nType = int.Parse(ddlcType.SelectedValue.ToString());
                    tproduct = tproduct.Where(w => w.nType == nType).ToList();
                }

                var reports = (from a in tproduct
                               join b in dbschool.TSell_Detail.Where(w => w.SchoolID == userData.CompanyID && (w.cDel ?? "0") == "0") on a.nProductID equals b.nProduct
                               join c in dbschool.TStockDetails.Where(w => w.SchoolID == userData.CompanyID) on a.nProductID equals c.nProductID
                               group new { a, b, c } by new { a.nNumber, a.sBarCode, a.nProductID, a.sProduct } into gb
                               select new
                               {
                                   Balance = (gb.Key.nNumber + gb.Sum(s => s.c.nNumber)) - gb.Sum(s => s.b.nNumber),
                                   gb.Key.sBarCode,
                                   gb.Key.sProduct,
                               }).ToList();

                ltrHtml.Text = @"<table class='table table-condensed' style='width: 100%; font-size:20px;'>";
                string _sHtml = "";
                _sHtml += @"<tr class='warning'>
                                <td style='width: 50%;'>BarCode</td>
                                <td style='width: 25%;'>ชื่อสินค้า</td>
                                <td style='width: 25%;'>คงเหลือ</td>
                            </tr>";

                foreach (var data in reports)
                {
                    _sHtml += @" <tr class='active'>
                                    <td style='text-align:left;'>" + data.sBarCode + @"</td>
                                    <td style='text-align:left;'>" + data.sProduct + @"</td>
                                    <td style='text-align:center;'>" + data.Balance + @"</td>
                                </tr>";
                }
                ltrHtml.Text += _sHtml + "</table>";
            }
        }
        public static string CountNumberProduct_Reprot04(string sBarCode)
        {
            var userData = GetUserData();
            JabJaiEntities _dbs = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));

            int? nCount = 0;
            int? _ProductNume = _dbs.TProducts.Where(w => w.sBarCode == sBarCode && string.IsNullOrEmpty(w.cDel) && w.SchoolID == userData.CompanyID).FirstOrDefault().nNumber;
            int? nProductID = _dbs.TProducts.Where(w => w.sBarCode == sBarCode && string.IsNullOrEmpty(w.cDel) && w.SchoolID == userData.CompanyID).FirstOrDefault().nProductID;
            int? nStock = _dbs.TStockDetails.Where(w => w.nProductID == nProductID && w.SchoolID == userData.CompanyID).Sum(s => s.nNumber);
            int? nSellNumber = _dbs.TSell_Detail.Where(w => w.nProduct == nProductID && string.IsNullOrEmpty(w.cDel) && w.SchoolID == userData.CompanyID).Sum(s => s.nNumber);
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

        protected void lvCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}