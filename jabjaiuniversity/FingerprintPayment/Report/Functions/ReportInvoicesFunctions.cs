using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Report.Functions
{
    public class ReportInvoicesFunctions
    {
        public Reports_data Init(ReportInvoice_Search search, int CompanyID)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(CompanyID, ConnectionDB.Read)))
                {
                    using (PeakengineEntities peakengineEntities = Connection.PeakengineEntities(ConnectionDB.Read))
                    {
                        //search.term_id = string.Format("TS{0:0000000}", int.Parse(search.term_id));
                        var q_student = (from a in dbschool.TB_StudentViews
                                         where a.SchoolID == CompanyID &&   search.level2_id == a.nTermSubLevel2 && (a.cDel ?? "0") != "1"
                                         //&& b.cType == "0" && b.nCompany == tCompany.nCompany
                                         && a.nTerm.Trim() == search.term_id && (a.nStudentStatus ?? 0) != 2
                                         orderby new { a.nStudentNumber, a.sStudentID }
                                         select new
                                         {
                                             a.sName,
                                             a.sLastname,
                                             a.sStudentID,
                                             a.sID,
                                             nStudentNumber = a.nStudentNumber ?? 0,
                                             a.titleDescription
                                         }).AsQueryable().ToList();

                        var f_class = dbschool.TTermSubLevel2.Where(w => w.SchoolID == CompanyID).FirstOrDefault(f => f.nTermSubLevel2 == search.level2_id);

                        string _commUser = "";
                        q_student.ForEach(f =>
                        {
                            _commUser += (string.IsNullOrEmpty(_commUser) ? "" : ",") + "'" + f.sID + "'";
                        });

                        string SQL = "", Condition = "";

                        if (search.report_type == "1") Condition += " AND A.tuitionfeeDetail_id IS NOT NULL";
                        if (!string.IsNullOrEmpty(_commUser)) Condition += string.Format(" AND A.student_id IN ({0})", _commUser);

                        SQL = string.Format(@"DECLARE @SCHOOLID INT = {0}
DECLARE @TERMID VARCHAR(100) = '{1}'

SELECT student_id,StudentCode,SUM(paymentAmount) AS paymentAmount,SUM(ManualDiscount) AS ManualDiscount,0 AS invoices_Id,
SUM(TotalDiscount + ManualDiscount) AS TotalDiscount,SUM(SUMPAYMENT) AS SUMPAYMENT,SUM(SUMPRICE) AS SUMPRICE,
SUM(SUMDEDT) AS SUMDEDT
FROM (
SELECT student_id,A.StudentCode AS student_number,StudentCode,a.invoices_Id,
ManualDiscount + GrandTotal + TotalDiscount AS paymentAmount,ManualDiscount,TotalDiscount,ISNULL(SUM(D1.amount),0) AS SUMPAYMENT,
ISNULL(SUM(B.price),0) - (TotalDiscount + ManualDiscount) AS SUMPRICE,
A.OutstandingAmount AS SUMDEDT
FROM TInvoices AS A 
INNER JOIN (SELECT invoices_Id,SUM(price) AS price FROM TInvoices_Detail GROUP BY invoices_Id) AS B ON A.invoices_Id = B.invoices_Id
LEFT OUTER JOIN (SELECT A1.invoices_Id,SUM(A2.amount) -  ISNULL(MAX(paid_Discount),0)  AS amount FROM  Paid_Payment AS A1
LEFT JOIN Payment AS A2 ON A1.paidpayment_id = A2.paidpayment_id AND A1.status IS NULL
WHERE ISNULL(A2.isDel, 0) = 0
GROUP BY A1.invoices_id,A1.paid_Discount) AS D1 ON A.invoices_Code = D1.invoices_Id
WHERE A.school_id = @SCHOOLID AND nTerm = @TERMID AND 'void,draft' NOT LIKE '%'+LOWER(invoices_status)+'%'  
AND ISNULL(A.isDel, 0) = 0 {2}
GROUP BY student_id, paymentAmount, ManualDiscount, TotalDiscount, A.StudentCode,a.invoices_Id,GrandTotal,OutstandingAmount) TB
GROUP BY student_id, StudentCode ", CompanyID, search.term_id, Condition);

                        var q1 = peakengineEntities.Database.SqlQuery<ET_Invoice>(SQL).ToList();

                        var q_invoices = (from a in q_student
                                          join b in q1 on a.sID equals b.student_id into jab

                                          from jb in jab.DefaultIfEmpty()

                                          orderby a.nStudentNumber, a.sStudentID

                                          select new Invoice_data
                                          {
                                              student_id = a.sStudentID,
                                              student_number = a.nStudentNumber,
                                              InvoiceId = jb == null ? 0 : jb.invoices_Id,
                                              student_name = a.titleDescription + " " + a.sName + " " + a.sLastname,
                                              paymentAmount = string.Format("{0:#,##0.00}", jb == null ? 0 : (jb.paymentAmount)),
                                              payment = string.Format("{0:#,##0.00}", jb == null ? 0 : jb.SUMPAYMENT),
                                              debt = string.Format("{0:#,##0.00}", jb == null ? 0 : jb.SUMDEDT),
                                              TotalDiscount = jb == null || jb.TotalDiscount == 0 ? "" : string.Format("ส่วนลด : {0:#,##0.00}", jb.TotalDiscount),
                                          }).ToList();

                        var f_classmember = (from a in dbschool.TClassMembers.Where(w => w.SchoolID == CompanyID)
                                             join b in dbschool.TEmployees.Where(w => w.SchoolID == CompanyID) on a.nTeacherHeadid equals b.sEmp
                                             join c in dbschool.TTerms.Where(w => w.SchoolID == CompanyID) on a.nTerm equals c.nTerm
                                             where (c.nYear == search.year_id || c.nTerm == search.term_id) && a.nTermSubLevel2 == search.level2_id
                                             select new
                                             {
                                                 a.nTeacherHeadid,
                                                 name = b.sName + " " + b.sLastname
                                             }).FirstOrDefault();

                        var q_reports = new Reports_data();

                        if (f_classmember != null) q_reports.teacher_name = f_classmember == null ? "" : (string.IsNullOrEmpty(f_classmember.name) ? "" : f_classmember.name);
                        else q_reports.teacher_name = "";
                        q_reports.invoice_Datas = q_invoices;

                        return q_reports;
                    }
                }
            }

        }
    }

    public class ReportInvoice_Search
    {
        public string term_id { get; set; }
        public int year_id { get; set; }
        public int? level2_id { get; set; }
        public Nullable<int> student_id { get; set; }
        public string report_type { get; set; }
    }

    public class Reports_data
    {
        public string teacher_name { get; set; }
        public List<Invoice_data> invoice_Datas { get; set; }
        public string errorMessage { get; set; }
    }

    public class Invoice_data
    {
        public string student_id { get; set; }
        public int? student_number { get; set; }
        public string paymentAmount { get; set; }
        public string payment { get; set; }
        public string debt { get; set; }
        public string student_name { get; set; }
        public int InvoiceId { get; set; }
        public string TotalDiscount { get; set; }
    }

    internal class ET_Invoice
    {
        public int student_id { get; set; }
        public string StudentCode { get; set; }
        public decimal SUMPAYMENT { get; set; }
        public decimal SUMPRICE { get; set; }
        public decimal SUMDEDT { get; set; }
        public decimal paymentAmount { get; set; }
        public decimal ManualDiscount { get; set; }
        public decimal TotalDiscount { get; set; }
        public int invoices_Id { get; set; }
    }
}