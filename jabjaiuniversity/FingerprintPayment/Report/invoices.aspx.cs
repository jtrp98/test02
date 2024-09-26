using FingerprintPayment.Report.Models;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using System.Globalization;
using FluentDateTime;

namespace FingerprintPayment.Report
{
    public partial class invoices : BehaviorGateway
    {
        public TCompany schoolData { get; set; }

        public InvoiceModels Model;
        protected void Page_Load(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(schoolData, ConnectionDB.Read)))
            {
                JWTToken token = new JWTToken();
                if (!token.CheckToken(HttpContext.Current)) Response.Redirect("~/Default.aspx");

                string entities = Session["sEntities"].ToString();

                schoolData = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == UserData.CompanyID);

                StudentLogic logic = new StudentLogic(jabJaiEntities);
                Model = new InvoiceModels();
                string id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(id)) Response.Redirect("~/Default.aspx");
                //var f_sell = jabJaiEntities.TSells.FirstOrDefault(f => f.id == id);
                var f_sell = jabJaiEntities.Database.SqlQuery<TSell>(@"DECLARE @sSellID VARCHAR(40) = '" + id + @"';
SELECT TOP 1 * FROM TSell WHERE id = @sSellID ").FirstOrDefault();

                if (f_sell == null)
                {
                    f_sell = jabJaiEntities.Database.SqlQuery<TSell>(@"DECLARE @sSellID VARCHAR(40) = '" + id + @"';
SELECT TOP 1 * FROM [JabjaiSchoolHistory].[dbo].[TSell] WHERE id = @sSellID ").FirstOrDefault();
                }

                if (f_sell == null)
                {
                    f_sell = jabJaiEntities.Database.SqlQuery<TSell>(@"DECLARE @sSellID VARCHAR(40) = '" + id + @"';
SELECT TOP 1 * FROM [JabjaiSchoolHistory].[dbo].[TSell_Backup] WHERE id = @sSellID ").FirstOrDefault();
                }

                if (f_sell.sID.HasValue && f_sell.sID > 0)
                {
                    var TermId = logic.GetTermId(f_sell.dSell.Value, UserData);
                    var studentData = logic.getById(f_sell.sID.Value, TermId, new JWTToken.userData
                    {
                        CompanyID = schoolData.nCompany
                    });

                    if (studentData == null)
                    {
                        TermId = jabJaiEntities.TB_StudentViews.FirstOrDefault(f => f.sID == f_sell.sID.Value).nTerm;
                        studentData = logic.getById(f_sell.sID.Value, TermId, new JWTToken.userData
                        {
                            CompanyID = schoolData.nCompany
                        });
                    }

                    Model.ClassName = studentData.Class_Name + " / " + studentData.Room_Name;
                    Model.studentName = studentData.student_Title + " " + studentData.student_Name;
                    Model.studentCode = studentData.student_Code;
                }
                else if (f_sell.sID2.HasValue)
                {
                    var q_Title = jabJaiEntities.TTitleLists.Where(w => w.SchoolID == UserData.CompanyID).ToList();
                    var f_Employees = jabJaiEntities.TEmployees.FirstOrDefault(f => f.sEmp == f_sell.sID2);
                    Model.studentName = getTitlte(q_Title, f_Employees?.sTitle) + " " + f_Employees?.sName + " " + f_Employees?.sLastname;
                }

                Model.day = f_sell.dSell.Value;
                Model.TotalMoney = f_sell.nTotal;
                //Model.products = (from a1 in jabJaiEntities.TSell_Detail.Where(w => w.SchoolID == UserData.CompanyID)
                //                  join b1 in jabJaiEntities.TProducts.Where(w => w.SchoolID == UserData.CompanyID) on a1.nProduct equals b1.nProductID
                //                  where a1.nSell == f_sell.sSellID
                //                  select new InvoiceModels.IProduct
                //                  {
                //                      Amount = a1.nNumber ?? 0,
                //                      Name = b1.sProduct,
                //                      Price = a1.nPrice ?? 0
                //                  }).ToList();


                var iProducts = jabJaiEntities.Database.SqlQuery<InvoiceModels.IProduct>(@"DECLARE @nSell INT = " + f_sell.sSellID + @";

SELECT ISNULL(TB.nNumber,0) AS 'Amount',ISNULL(A.sProduct,'') AS 'Name' ,ISNULL(TB.nPrice,0) AS 'Price'
FROM (
SELECT * FROM TSell_Detail WHERE nSell = @nSell AND ISNULL(cDel,'0') = '0') TB 
INNER JOIN TProduct AS A ON A.nProductID = TB.nProduct AND A.SchoolID = TB.SchoolID ").ToList();


                if (iProducts.Count == 0)
                {
                    iProducts = jabJaiEntities.Database.SqlQuery<InvoiceModels.IProduct>(@"DECLARE @nSell INT = " + f_sell.sSellID + @";

SELECT ISNULL(TB.nNumber,0) AS 'Amount',ISNULL(A.sProduct,'') AS 'Name' ,ISNULL(TB.nPrice,0) AS 'Price'
FROM (
SELECT * FROM [JabjaiSchoolHistory].[dbo].[TSell_Detail] WHERE nSell = @nSell AND ISNULL(cDel,'0') = '0' ) TB 
INNER JOIN TProduct AS A ON A.nProductID = TB.nProduct AND A.SchoolID = TB.SchoolID ").ToList();
                }

                if (iProducts.Count == 0)
                {
                    iProducts = jabJaiEntities.Database.SqlQuery<InvoiceModels.IProduct>(@"DECLARE @nSell INT = " + f_sell.sSellID + @";

SELECT ISNULL(TB.nNumber,0) AS 'Amount',ISNULL(A.sProduct,'') AS 'Name' ,ISNULL(TB.nPrice,0) AS 'Price'
FROM (
SELECT * FROM [JabjaiSchoolHistory].[dbo].[TSell_Detail_Backup] WHERE nSell = @nSell AND ISNULL(cDel,'0') = '0' ) TB 
INNER JOIN TProduct AS A ON A.nProductID = TB.nProduct AND A.SchoolID = TB.SchoolID ").ToList();
                }
                Model.products = iProducts;

                int runNumber = 1;
                DateTime date = new DateTime(Model.day.Year, Model.day.Month, Model.day.Day);
                runNumber = jabJaiEntities.TSells.Where(w => w.SchoolID == UserData.CompanyID).Count(c => c.dSell.Value >= date && c.sSellID <= f_sell.sSellID);

                Model.invoiceDay = Model.day.ToString("dd/MM/yyyy", new CultureInfo("th-th"));
                Model.invoiceCode = Model.day.ToString("yyMMdd-", new CultureInfo("th-th")) + string.Format("{0:0000}", runNumber);
                if (f_sell.sEmp.HasValue)
                {
                    var employees = jabJaiEntities.TEmployees.FirstOrDefault(f => f.sEmp == f_sell.sEmp);
                    if (employees != null) Model.employeesName = employees.sName + " " + employees.sLastname;
                    else Model.employeesName = "ซื้อผ่านแอป	";
                }
            }
        }

        private string getTitlte(List<TTitleList> titles, string titlesId)
        {
            int nTitleid = 0;
            int.TryParse((titlesId ?? "0"), out nTitleid);
            var f_titles = titles.FirstOrDefault(f => f.nTitleid == nTitleid);
            if (f_titles == null) return titlesId;
            else return f_titles.titleDescription;
        }
    }
}