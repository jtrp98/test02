using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace FingerprintPayment.Modules.Invoices
{
    public partial class CreatebyGroup : StudentGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitialControl();
        }

        private void InitialControl()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                // Get current year
                StudentLogic studentLogic = new StudentLogic(en);
                string currentTerm = studentLogic.GetTermId(UserData);
                int yearID = 0;
                var term = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == currentTerm && w.cDel == null).FirstOrDefault();
                if (term != null)
                {
                    yearID = term.nYear.Value;
                }

                var listYear = en.TYears.Where(w => w.SchoolID == schoolID && w.cDel == false).OrderByDescending(x => x.numberYear).ToList();
                foreach (var l in listYear)
                {
                    if (l.nYear == yearID)
                    {
                        this.ltrYear.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                    }
                    else
                    {
                        this.ltrYear.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                    }

                    if (yearID == 0) yearID = l.nYear;
                }

                if (yearID != 0)
                {
                    var listTerm = en.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == yearID && w.cDel == null).OrderByDescending(o => o.nTerm).ToList();
                    foreach (var l in listTerm)
                    {
                        if (l.nTerm.Trim() == currentTerm)
                        {
                            this.ltrTerm.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nTerm, l.sTerm);
                        }
                        else
                        {
                            this.ltrTerm.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nTerm, l.sTerm);
                        }
                    }
                }

                var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                foreach (var l in listLevel)
                {
                    this.ltrLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);
                }
                //this.ltrSortLevel.Text = this.ltrLevel.Text;
            }

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadStudent(string TermId, List<int> RoomId)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                var result = (from a in en.TB_StudentViews.Where(w => w.SchoolID == schoolID && (w.cDel ?? "0") == "0" && (w.nStudentStatus ?? 0) != 2)
                              where a.nTerm == TermId && RoomId.Contains(a.nTermSubLevel2)
                              group a by new { a.nTermSubLevel2, a.SubLevel, a.nTSubLevel2 } into gb
                              select new
                              {
                                  lable = gb.Key.SubLevel + " / " + gb.Key.nTSubLevel2,
                                  key = gb.Key.nTermSubLevel2,
                                  item = (from a1 in gb
                                          orderby new { a1.nStudentNumber, a1.sStudentID }
                                          select new
                                          {
                                              a1.sID,
                                              a1.sName,
                                              a1.sLastname,
                                              a1.sStudentID,
                                              a1.nStudentNumber,
                                              a1.SubLevel,
                                              a1.nTSubLevel2
                                          })
                              }).AsQueryable().ToList();

                return JsonConvert.SerializeObject(result);

            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadGropInvoices()
        {
            int schoolID = GetUserData().CompanyID;
            using (PeakengineEntities en = Connection.PeakengineEntities(ConnectionDB.Read))
            {
                var result = (from a in en.Product_Group
                              where a.school_id == schoolID && a.Del != true
                              select new
                              {
                                  a.PaymentGroupID,
                                  a.GroupName,
                                  Item = (from b in en.Product_Group_List
                                          join c in en.Products on b.nPaymentID equals c.nPaymentID
                                          where a.PaymentGroupID == b.PaymentGroupID && c.Del != true
                                          select new
                                          {
                                              c.nPaymentID,
                                              c.sPayment,
                                              b.Price
                                          }).ToList()
                              }).ToList();

                var result_1 = (from a in en.Products
                                where a.school_id == schoolID && (a.Del ?? false) == false
                                select new
                                {
                                    a.nPaymentID,
                                    a.Pirce,
                                    a.sPayment
                                }).ToList();


                var data = new { byGroup = result, byItem = result_1 };

                return JsonConvert.SerializeObject(data);
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string InsertInvoices(InvoicesData data)
        {
            int schoolID = GetUserData().CompanyID;
            using (PeakengineEntities en = Connection.PeakengineEntities(ConnectionDB.Read))
            {
                using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    List<int> StudentId = data.iStudents.Select(s => s.StudentId).ToList();
                    var iStudents = (from a in jabJaiEntities.TB_StudentViews.Where(w => w.SchoolID == schoolID && StudentId.Contains(w.sID) && w.nTerm == data.TermId)
                                     select a).AsQueryable().ToList();

                    decimal paymentAmount = data.invoicesItems.Sum(s => s.Price) ?? 0;
                    decimal TotalDiscount = data.invoicesItems.Sum(s => s.Discount) ?? 0;
                    DateTime DueDate = DateTime.ParseExact(data.DueDate, "dd/MM/yyyy", new CultureInfo("th-th"));
                    DateTime issuedDate = DateTime.ParseExact(data.issuedDate, "dd/MM/yyyy", new CultureInfo("th-th"));

                    string invoicesStatus = "Approve";
                    if (data.invoicesStatus != invoicesStatus) invoicesStatus = "Draft";

                    List<IStudent> studentsInfo = new List<IStudent>();

                    foreach (var studentDATA in data.iStudents)
                    {
                        var f_iStudents = iStudents.FirstOrDefault(f => f.sID == studentDATA.StudentId);
                        DateTime dateTime = new DateTime(issuedDate.Year, 1, 1);
                        int Invoice_count = en.TInvoices.Count(w => w.dueDate >= dateTime && !string.IsNullOrEmpty(w.code) && w.school_id == schoolID) + 1;
                        decimal ManualDiscount = studentDATA.Discount ?? 0;
                        decimal OutstandingAmount = paymentAmount - (ManualDiscount + TotalDiscount);
                        var invoices_Code = Guid.NewGuid().ToString();

                        TInvoice f_invoices = new TInvoice
                        {
                            code = string.Format("IV-{0:yyyyMM}{1:00000}", DateTime.Today, Invoice_count),
                            //contactId = invoice.contactId,
                            //tuitionfeeDetail_id = invoice.tuitionfeeDetail_id,
                            invoices_status = invoicesStatus,
                            jabjaiUpdate = DateTime.Now,
                            dueDate = DueDate,
                            issuedDate = issuedDate,
                            school_id = f_iStudents.SchoolID,
                            paymentAmount = paymentAmount,
                            OutstandingAmount = OutstandingAmount,
                            TotalDiscount = TotalDiscount,
                            ManualDiscount = ManualDiscount,
                            GrandTotal = OutstandingAmount,
                            Description = "",
                            student_id = f_iStudents.sID,
                            StudentName = f_iStudents.sName + " " + f_iStudents.sLastname,
                            StudentCode = f_iStudents.sStudentID,
                            nTerm = f_iStudents.nTerm,
                            //nTLevel = invoice.nTLevel,
                            nTSubLevel = f_iStudents.nTLevel,
                            paid_status = false,
                            invoices_Code = invoices_Code,

                            SubLevel = f_iStudents.SubLevel,
                            SubLevel2 = f_iStudents.nTSubLevel2,
                            nTermSubLevel2 = f_iStudents.nTermSubLevel2,
                            //Level = f_iStudents.Level,
                            Term = f_iStudents.sTerm,
                            TermYear = f_iStudents.numberYear + "",
                        };

                        en.TInvoices.Add(f_invoices);

                        en.SaveChanges();

                        studentsInfo.Add(new IStudent
                        {
                            invoices_Id = f_invoices.invoices_Id,
                            ClassName = f_iStudents.SubLevel + " / " + f_iStudents.nTSubLevel2,
                            StudentId = f_iStudents.sID,
                            Discount = studentDATA.Discount,
                            StudentName = f_iStudents.sName + " " + f_iStudents.sLastname
                        });

                        var q_add = new List<TInvoices_Detail>();
                        foreach (var ItemData in data.invoicesItems)
                        {
                            q_add.Add(new TInvoices_Detail
                            {
                                invoices_Id = f_invoices.invoices_Id,
                                nPaymentID = ItemData.ProductId,
                                //product_id = data.product_id,
                                quantity = 1,
                                price = ItemData.Price,
                                Discount = ItemData.Discount,
                                OutstandingAmount = ItemData.Price - ItemData.Discount,
                                GrandTotal = ItemData.Price - ItemData.Discount,
                                ProductAmount = ItemData.Price,
                                IsDelete = false
                            });
                        }

                        en.TInvoices_Detail.AddRange(q_add);
                        en.SaveChanges();
                    }

                    #region Insert Log

                    dynamic json = new { StudentInfo = studentsInfo, ProductInfo = data.invoicesItems, dueDate = data.DueDate, issuedDate = data.issuedDate };
                    string Fd_HistoryID = "", Fd_JSON = JsonConvert.SerializeObject(json);

                    int Fd_LevelID = data.Fd_LevelID;
                    var TermData = (from a in jabJaiEntities.TTerms.Where(w => w.SchoolID == schoolID)
                                    join b in jabJaiEntities.TYears.Where(w => w.SchoolID == schoolID && w.cDel == false) on a.nYear equals b.nYear
                                    select new
                                    {
                                        a.sTerm,
                                        numberYear = (b.numberYear ?? 0).ToString(),
                                        b.nYear,
                                        a.nTerm
                                    }).FirstOrDefault();

                    var classData = jabJaiEntities.TSubLevels.FirstOrDefault(f => f.nTSubLevel == data.Fd_LevelID);

                    Fd_HistoryID = string.Format("{0:yyyyMMdd}-{1:0000}", DateTime.Today, en.TB_CreateGroupHistory.Count(c => c.Fd_SchoolID == schoolID && c.Fd_CreateDate >= DateTime.Today) + 1);


                    TB_CreateGroupHistory _CreateGroupHistory = new TB_CreateGroupHistory()
                    {
                        Fd_CreateDate = DateTime.Now,
                        Fd_CreateBy = GetUserData().UserID,
                        Fd_UID = Guid.NewGuid(),

                        Fd_Amount = paymentAmount - TotalDiscount,
                        Fd_HistoryID = Fd_HistoryID,
                        Fd_InvoiceStatus = data.invoicesStatus,
                        Fd_SchoolID = schoolID,
                        Fd_JSON = Fd_JSON,
                        Fd_LevelID = classData.nTSubLevel,
                        Fd_LevelName = classData.SubLevel,
                        Fd_TermId = data.TermId,
                        Fd_TermNumber = TermData.sTerm,
                        Fd_YearNumber = TermData.numberYear,
                        Fd_dueDate = DueDate,
                        Fd_issuedDate = issuedDate,
                    };

                    en.TB_CreateGroupHistory.Add(_CreateGroupHistory);
                    en.SaveChanges();

                    #endregion
                }

                return JsonConvert.SerializeObject(data);
            }
        }

        public class IStudent
        {
            public int StudentId { get; set; }
            public int invoices_Id { get; set; }
            public string ClassName { get; set; }
            public string StudentName { get; set; }
            public decimal? Discount { get; set; } = 0;
        }

        public class InvoicesItem
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal? Price { get; set; } = 0;
            public decimal? Discount { get; set; } = 0;
        }

        public class InvoicesData
        {
            public string DueDate { get; set; }
            public string issuedDate { get; set; }
            public string TermId { get; set; }
            public string TermNumber { get; set; }
            public int? YearNumber { get; set; }
            public string invoicesStatus { get; set; }
            public List<InvoicesItem> invoicesItems { get; set; }
            public List<IStudent> iStudents { get; set; }
            public int Fd_LevelID { get; set; }
            public string Fd_LevelName { get; set; }
        }
    }
}