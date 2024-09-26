using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using FingerprintPayment.Report.Models;
using FingerprintPayment.Report.Functions.Reports_03;

namespace FingerprintPayment.Report
{
    public partial class Reportmobile03 : BehaviorGateway
    {
        //internal JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            if (!this.IsPostBack)
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                //fcommon.ListDBToDropDownList(_conn, ddlyear, "select * from TYear where SchoolID = " + userData.CompanyID + " order by numberYear desc", "", "nYear", "numberYear");
                //ddlyear.SelectedValue = DateTime.Now.Year.ToString();
                //using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                //{
                //    //string sEntities = Session["sEntities"].ToString();
                //    //var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                //    //var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);
                //    //fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");
                //    //hdfschoolname.Value = tCompany.sCompany;
                //}
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object returnlist(Search search)
        {
            var userData = GetUserData();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    StudentLogic logic = new StudentLogic(dbschool);
                    DateTime dStart = DateTime.Today, dEnd = DateTime.Today;
                    if (search.sort_type == 0)
                    {
                        search.term_id = logic.GetTermId(search.dStart ?? DateTime.Today, userData);
                        dStart = search.dStart.Value;
                        dEnd = search.dStart.Value;
                    }
                    else if (search.sort_type == 1)
                    {
                        search.term_id = logic.GetTermId(search.dStart ?? DateTime.Today, userData);
                        dStart = search.dStart.Value;
                        dEnd = search.dStart.Value.AddMonths(1).AddDays(-1);
                    }
                    else if (search.sort_type == 2)
                    {
                        var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm.Trim() == search.term_id.Trim());
                        dStart = f_term.dStart.Value;
                        dEnd = f_term.dEnd.Value;
                    }
                    else if (search.sort_type == 3)
                    {
                        dStart = search.dStart.Value;
                        if (search.dEnd.HasValue) dEnd = search.dEnd.Value;
                        else dEnd = search.dStart.Value;

                        search.term_id = logic.GetTermId(dStart, userData);
                    }
                    else
                    {
                        search.term_id = logic.GetTermId(search.dStart ?? DateTime.Today, userData);
                        dStart = search.dStart.Value;
                        dEnd = search.dEnd.Value;
                    }

                    //if (search.report_type == 1) return ReportsType_01.getData(search, dbschool, dStart, dEnd);
                    //else if (search.report_type == 0) return ReportsType_02.getData(search, dbschool, dStart, dEnd);
                    //else return ReportsType_03.getData(search, dbschool, dStart, dEnd);
                    if (search.report_type == 0) return ReportsType_02.getData(search, dbschool, dStart, dEnd, userData);
                    else if (search.report_type == 1) return ReportsType_01.getData(search, dbschool, dStart, dEnd, true, userData, true);
                    else if (search.report_type == 2) return ReportsType_03.getData(search, dbschool, dStart, dEnd, userData);
                    else
                    {
                        var data = ReportsType_0303.getData(search, dbschool, dStart, dEnd, search.term_id, userData);
                        return data;
                    }

                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void export_data(Search search)
        {
            var userData = GetUserData();

            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();

                var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany, ConnectionDB.Read)))
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานการมาโรงเรียน");

                    var worksheet = excel.Workbook.Worksheets["รายงานการมาโรงเรียน"];
                   

                    DateTime dayStart = DateTime.Today, dayEnd = DateTime.Today;


                    StudentLogic logic = new StudentLogic(dbschool);
                    if (search.sort_type == 0)
                    {
                        search.term_id = logic.GetTermId(search.dStart ?? DateTime.Today, userData);
                        dayStart = search.dStart.Value;
                        dayEnd = search.dStart.Value.AddDays(1);
                    }
                    else if (search.sort_type == 1)
                    {
                        search.term_id = logic.GetTermId(search.dStart ?? DateTime.Today, userData);
                        dayStart = search.dStart.Value;
                        dayEnd = search.dStart.Value.AddMonths(1).AddDays(-1);
                    }
                    else if (search.sort_type == 2)
                    {
                        var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm.Trim() == search.term_id.Trim());
                        dayStart = f_term.dStart.Value;
                        dayEnd = f_term.dEnd.Value;
                    }
                    else
                    {
                        search.term_id = logic.GetTermId(search.dStart ?? DateTime.Today, userData);
                        dayStart = search.dStart.Value;
                        dayEnd = search.dEnd.Value;
                    }

                    string class_name = "ทั้งหมด", classRoom_name = "ทั้งหมด";
                    if (search.level_id.HasValue)
                    {
                        var f_level = QueryDataBases.SubLevel_Query.GetData(dbschool, userData).FirstOrDefault(f => f.class_id == search.level_id);
                        class_name = f_level.class_name;
                    }
                    if (search.level2_id.HasValue)
                    {
                        var f_class = QueryDataBases.SubLevel2_Query.GetRoom(dbschool, search.level2_id.Value, userData);
                        classRoom_name = f_class.classRoom_name;
                    }

                    int rowsStart = 9, Index = 1;
                    //int Status_0 = 0, Status_1 = 0, Status_2 = 0, Status_3 = 0;
                    //int DayLength = report.headerReports.Sum(s => s.weeks.Sum(c1 => c1.days.Count()));
                    List<string> arraryString = new List<string>() { "3", "1", "11", "10" };

                    if (search.report_type == 2)
                    {

                        SetHeader(worksheet, "A1:H1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                        SetHeader(worksheet, "A2:H2", true, "รายงานสรุปสถิติการมาโรงเรียน", 14, ExcelHorizontalAlignment.Center);

                        SetHeader(worksheet, "A3:H3", true, string.Format("ประจำวันที่ {0:dd/MM/yyyy} ถึงวันที่ {1:dd/MM/yyyy} ", dayStart, dayEnd), 14, ExcelHorizontalAlignment.Center);
                        SetHeader(worksheet, "A4:H4", true, "พิมพ์วันที่ : " + DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Right);
                        SetHeader(worksheet, "A5:H5", true, "เวลา : " + DateTime.Now.ToString("HH:mm:ss", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Right);

                        SetHeader(worksheet, "A6:B6", true, "ระดับชั้นเรียน : ", null, ExcelHorizontalAlignment.Right);
                        SetHeader(worksheet, "C6:C6", true, class_name, null, ExcelHorizontalAlignment.Right);
                        SetHeader(worksheet, "D6:F6", true, "ชั้นเรียน : ", null, ExcelHorizontalAlignment.Right);
                        SetHeader(worksheet, "G6:H6", true, classRoom_name, null, ExcelHorizontalAlignment.Right);



                        var report = ReportsType_03.getData(search, dbschool, dayStart, dayEnd, userData);
                        string[] strHeader = { "ลำดับ", "เลขประจำตัว", "ชื่อ-นามสกุล", "วัน/เดือน/ปี", "สาย", "ขาด", "ลากิจ", "ลาป่วย" };
                        int Columuns = 1;
                        foreach (string str in strHeader)
                        {
                            SetTableHeader(worksheet.Cells[8, Columuns++], false, str, ExcelHorizontalAlignment.Center);
                        }


                        foreach (var Classdata in report)
                        {
                            SetTableRows(worksheet.Cells[string.Format("A{0}:H{0}", rowsStart)], true, "ห้อง " + Classdata.level2name, ExcelHorizontalAlignment.Left);
                            rowsStart += 1;
                            foreach (var studentData in Classdata.studentDatas)
                            {
                                int DayLength = studentData.scanDatas.Count(c => arraryString.Contains(c.Scan_Status));
                                int rowsEnd = rowsStart + ((DayLength == 0 ? 1 : DayLength) - 1);

                                int Sum_Status_0 = 0, Sum_Status_1 = 0, Sum_Status_2 = 0, Sum_Status_3 = 0;
                                SetTableRows(worksheet.Cells[rowsStart, 1, rowsEnd, 1], true, Index + ".", ExcelHorizontalAlignment.Center);
                                SetTableRows(worksheet.Cells[rowsStart, 2, rowsEnd, 2], true, studentData.Student_Id, ExcelHorizontalAlignment.Center);
                                SetTableRows(worksheet.Cells[rowsStart, 3, rowsEnd, 3], true, studentData.Student_Name, ExcelHorizontalAlignment.Left);

                                if (DayLength == 0)
                                {
                                    SetTableRows(worksheet.Cells[rowsStart, 4], false, "", ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[rowsStart, 5], false, "", ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[rowsStart, 6], false, "", ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[rowsStart, 7], false, "", ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[rowsStart, 8], false, "", ExcelHorizontalAlignment.Center);
                                    rowsStart++;
                                }
                                else
                                {
                                    foreach (var logscanData in (from a in studentData.scanDatas
                                                                     //orderby a.Year, a.DayOfYear
                                                                 where arraryString.Contains(a.Scan_Status)
                                                                 select a))
                                    {
                                        SetTableRows(worksheet.Cells[rowsStart, 4], false, logscanData.Scan_Date, ExcelHorizontalAlignment.Center);
                                        SetTableRows(worksheet.Cells[rowsStart, 5], false, logscanData.Scan_Status == "1" ? "1" : "", ExcelHorizontalAlignment.Center);
                                        SetTableRows(worksheet.Cells[rowsStart, 6], false, logscanData.Scan_Status == "3" ? "1" : "", ExcelHorizontalAlignment.Center);
                                        SetTableRows(worksheet.Cells[rowsStart, 7], false, logscanData.Scan_Status == "10" ? "1" : "", ExcelHorizontalAlignment.Center);
                                        SetTableRows(worksheet.Cells[rowsStart, 8], false, logscanData.Scan_Status == "11" ? "1" : "", ExcelHorizontalAlignment.Center);
                                        switch (logscanData.Scan_Status)
                                        {
                                            case "1": Sum_Status_0 += 1; break;
                                            case "3": Sum_Status_1 += 1; break;
                                            case "10": Sum_Status_2 += 1; break;
                                            case "11": Sum_Status_3 += 1; break;
                                        }

                                        rowsStart++;
                                    }
                                }

                                SetTableFooter(worksheet.Cells[string.Format("A{0}:D{0}", rowsStart)], true, "รวม", ExcelHorizontalAlignment.Right);
                                SetTableFooter(worksheet.Cells[rowsStart, 5], false, Sum_Status_0 + "", ExcelHorizontalAlignment.Center);
                                SetTableFooter(worksheet.Cells[rowsStart, 6], false, Sum_Status_1 + "", ExcelHorizontalAlignment.Center);
                                SetTableFooter(worksheet.Cells[rowsStart, 7], false, Sum_Status_2 + "", ExcelHorizontalAlignment.Center);
                                SetTableFooter(worksheet.Cells[rowsStart, 8], false, Sum_Status_3 + "", ExcelHorizontalAlignment.Center);

                                rowsStart += 1;
                                Index++;
                            }

                        }

                    }
                    else/* if (search.report_type == 3)*/
                    {

                        SetHeader(worksheet, "A1:K1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                        SetHeader(worksheet, "A2:K2", true, "รายงานสรุปสถิติการมาโรงเรียน", 14, ExcelHorizontalAlignment.Center);

                        SetHeader(worksheet, "A3:K3", true, string.Format("ประจำวันที่ {0:dd/MM/yyyy} ถึงวันที่ {1:dd/MM/yyyy} ", dayStart, dayEnd), 14, ExcelHorizontalAlignment.Center);
                        SetHeader(worksheet, "A4:K4", true, "พิมพ์วันที่ : " + DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Right);
                        SetHeader(worksheet, "A5:K5", true, "เวลา : " + DateTime.Now.ToString("HH:mm:ss", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Right);

                        SetHeader(worksheet, "A6:B6", true, "ระดับชั้นเรียน : ", null, ExcelHorizontalAlignment.Right);
                        SetHeader(worksheet, "C6:C6", true, class_name, null, ExcelHorizontalAlignment.Right);
                        SetHeader(worksheet, "D6:F6", true, "ชั้นเรียน : ", null, ExcelHorizontalAlignment.Right);
                        SetHeader(worksheet, "G6:K6", true, classRoom_name, null, ExcelHorizontalAlignment.Right);


                        var report = ReportsType_0303.getData(search, dbschool, dayStart, dayEnd, search.term_id, userData);
                        string[] strHeader = { "ลำดับ", "เลขประจำตัว", "ชื่อ-นามสกุล", "สาย", "ขาด", "ขาดครึ่งวัน", "ลากิจ", "ลาป่วย", "ไม่ได้เช็คชื่อ", "ตัดคะแนนสาย/ขาด", "ตัดคะแนนอื่นๆ", "รวมคะแนน" };
                        int Columuns = 1;
                        foreach (string str in strHeader)
                        {
                            SetTableHeader(worksheet.Cells[8, Columuns++], false, str, ExcelHorizontalAlignment.Center);
                        }


                        foreach (var Classdata in report)
                        {
                            SetTableRows(worksheet.Cells[string.Format("A{0}:H{0}", rowsStart)], true, "ห้อง " + Classdata.level2name, ExcelHorizontalAlignment.Left);
                            rowsStart += 1;
                            foreach (var studentData in Classdata.studentDatas)
                            {
                                int DayLength = studentData.scanDatas.Count(c => arraryString.Contains(c.Scan_Status));
                                int rowsEnd = rowsStart + ((DayLength == 0 ? 1 : DayLength) - 1);

                                int Sum_Status_0 = 0, Sum_Status_1 = 0, Sum_Status_1_1 = 0, Sum_Status_2 = 0, Sum_Status_3 = 0, Sum_Status_4 = 0;
                                decimal behavAuto = studentData.behavAuto;
                                decimal behavManual = studentData.behavManual;
                                decimal behav = studentData.behaviorScore;
                                int _Late = 0, _Absence = 0, _Absence_Half = 0, _Errand = 0, _Sick = 0, _UncheckName = 0;

                                //SetTableRows(worksheet.Cells[rowsStart, 1, rowsEnd, 1], true, Index + ".", ExcelHorizontalAlignment.Center);
                                //SetTableRows(worksheet.Cells[rowsStart, 2, rowsEnd, 2], true, studentData.Student_Id, ExcelHorizontalAlignment.Left);
                                //SetTableRows(worksheet.Cells[rowsStart, 3, rowsEnd, 3], true, studentData.Student_Name, ExcelHorizontalAlignment.Left);

                                SetTableRows(worksheet.Cells[rowsStart, 1], false, Index + ".", ExcelHorizontalAlignment.Center);
                                SetTableRows(worksheet.Cells[rowsStart, 2], false, studentData.Student_Id, ExcelHorizontalAlignment.Center);
                                SetTableRows(worksheet.Cells[rowsStart, 3], false, studentData.Student_Name, ExcelHorizontalAlignment.Left);

                                if (DayLength == 0)
                                {
                                    SetTableRows(worksheet.Cells[rowsStart, 4], false, "0" + "/(" + _Late + ")", ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[rowsStart, 5], false, "0" + "/(" + _Absence + ")", ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[rowsStart, 6], false, "0" + "/(" + _Absence_Half + ")", ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[rowsStart, 7], false, "0" + "/(" + _Errand + ")", ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[rowsStart, 8], false, "0" + "/(" + _Sick + ")", ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[rowsStart, 9], false, "0" + "/(" + _UncheckName + ")", ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[rowsStart, 10], false, "0", ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[rowsStart, 11], false, "0", ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[rowsStart, 12], false, "0", ExcelHorizontalAlignment.Center);
                                }
                                else
                                {
                                    foreach (var logscanData in (from a in studentData.scanDatas
                                                                     //orderby a.Year, a.DayOfYear
                                                                 where arraryString.Contains(a.Scan_Status)
                                                                 select a))
                                    {
                                        //SetTableRows(worksheet.Cells[rowsStart, 4], false, logscanData.Scan_Date, ExcelHorizontalAlignment.Center);
                                        //SetTableRows(worksheet.Cells[rowsStart, 5], false, logscanData.Scan_Status == "1" ? "1" : "", ExcelHorizontalAlignment.Center);
                                        //SetTableRows(worksheet.Cells[rowsStart, 6], false, logscanData.Scan_Status == "3" ? "1" : "", ExcelHorizontalAlignment.Center);
                                        //SetTableRows(worksheet.Cells[rowsStart, 7], false, logscanData.Scan_Status == "10" ? "1" : "", ExcelHorizontalAlignment.Center);
                                        //SetTableRows(worksheet.Cells[rowsStart, 8], false, logscanData.Scan_Status == "11" ? "1" : "", ExcelHorizontalAlignment.Center);
                                        switch (logscanData.Scan_Status)
                                        {
                                            case "1": Sum_Status_0 += 1; break;
                                            //case "3": Sum_Status_1 += 1; break;
                                            case "3":
                                                {
                                                    if (logscanData.timeIn == "")
                                                    {
                                                        Sum_Status_1 += 1;
                                                    }
                                                    else Sum_Status_1_1 += 1;
                                                    break;
                                                }
                                            case "10": Sum_Status_2 += 1; break;
                                            case "11": Sum_Status_3 += 1; break;
                                            case "99": Sum_Status_4 += 1; break;
                                        }

                                        _Late += logscanData.Late;
                                        _Absence += logscanData.Absence;
                                        _Absence_Half += logscanData.Absence_Half;
                                        _Errand += logscanData.Errand;
                                        _Sick += logscanData.Sick;
                                        _UncheckName += logscanData.UncheckName;

                                        //rowsStart++;
                                    }

                                    //SetTableFooter(worksheet.Cells[string.Format("A{0}:D{0}", rowsStart)], true, "รวม", ExcelHorizontalAlignment.Right);
                                    SetTableFooter(worksheet.Cells[rowsStart, 4], false, Sum_Status_0 + "/(" + _Late + ")", ExcelHorizontalAlignment.Center);
                                    SetTableFooter(worksheet.Cells[rowsStart, 5], false, Sum_Status_1 + "/(" + _Absence + ")", ExcelHorizontalAlignment.Center);
                                    SetTableFooter(worksheet.Cells[rowsStart, 6], false, Sum_Status_1_1 + "/(" + _Absence_Half + ")", ExcelHorizontalAlignment.Center);
                                    SetTableFooter(worksheet.Cells[rowsStart, 7], false, Sum_Status_2 + "/(" + _Errand + ")", ExcelHorizontalAlignment.Center);
                                    SetTableFooter(worksheet.Cells[rowsStart, 8], false, Sum_Status_3 + "/(" + _Sick + ")", ExcelHorizontalAlignment.Center);
                                    SetTableFooter(worksheet.Cells[rowsStart, 9], false, Sum_Status_4 + "/(" + _UncheckName + ")", ExcelHorizontalAlignment.Center);
                                    SetTableFooter(worksheet.Cells[rowsStart, 10], false, behavAuto + "", ExcelHorizontalAlignment.Center);
                                    SetTableFooter(worksheet.Cells[rowsStart, 11], false, behavManual + "", ExcelHorizontalAlignment.Center);
                                    SetTableFooter(worksheet.Cells[rowsStart, 12], false, behav + "", ExcelHorizontalAlignment.Center);
                                }

                                if ((studentData.StudentStatus ?? 0) == 2)
                                {
                                    worksheet.Cells[string.Format("A{0}:L{0}", rowsStart)].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                    worksheet.Cells[string.Format("A{0}:L{0}", rowsStart)].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[string.Format("A{0}:L{0}", rowsStart)].Style.Fill.BackgroundColor.SetColor(0, 190, 190, 190);
                                    //using (ExcelRange rng = worksheet.Cells[$"A{rowsStart}:K{rowsStart}"])
                                    //{
                                    //    rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
                                    //    rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                                    //}
                                }
                                else if (studentData.Alert)
                                {
                                    worksheet.Cells[string.Format("A{0}:L{0}", rowsStart)].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                    worksheet.Cells[string.Format("A{0}:L{0}", rowsStart)].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[string.Format("A{0}:L{0}", rowsStart)].Style.Fill.BackgroundColor.SetColor(0, 238, 213, 210);
                                }
                                else
                                {
                                    worksheet.Cells[string.Format("A{0}:L{0}", rowsStart)].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                    worksheet.Cells[string.Format("A{0}:L{0}", rowsStart)].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[string.Format("A{0}:L{0}", rowsStart)].Style.Fill.BackgroundColor.SetColor(0, 255, 255, 255);
                                }

                                rowsStart += 1;
                                Index++;
                            }

                        }
                    }


                    worksheet.Cells.AutoFitColumns();
                    worksheet.Column(1).Width = 10;
                    worksheet.Column(2).Width = 15;
                    worksheet.Column(3).Width = 24;
                    worksheet.Column(4).Width = 14;
                    worksheet.Column(5).Width = 10;
                    worksheet.Column(6).Width = 10;
                    worksheet.Column(7).Width = 10;
                    worksheet.Column(8).Width = 10;


                    if (search.report_type == 3)
                    {

                    }

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                    HttpContext.Current.Response.ContentType = "application/text";
                    HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                    HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
                }
            }
        }

        private static void SetHeader(ExcelWorksheet excelWorksheet, string Cells, bool Merge, string strValues, int? fontSize, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = excelWorksheet.Cells[Cells])
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.Font.Size = fontSize ?? 10;
            }
        }

        private static void SetTableHeader(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
                rng.Style.Fill.BackgroundColor.SetColor(0, 51, 122, 183);
            }
        }

        private static void SetTableRows(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            }
        }

        private static void SetTableFooter(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(0, 217, 217, 217);
            }
        }

        private static string ShotDayName(int DayNumber)
        {
            switch (DayNumber)
            {
                case 0: return "อา.";
                case 1: return "จ.";
                case 2: return "อ.";
                case 3: return "พ.";
                case 4: return "พฤ.";
                case 5: return "ศ.";
                default: return "ส.";
            }
        }

        private static string MonthName(int DayNumber)
        {
            switch (DayNumber)
            {
                case 1: return "มกราคม";
                case 2: return "กุมภาพันธ์";
                case 3: return "มีนาคม";
                case 4: return "เมษายน";
                case 5: return "พฤษภาคม";
                case 6: return "มิถุนายน";
                case 7: return "กรกฎาคม";
                case 8: return "สิงหาคม";
                case 9: return "กันยายน";
                case 10: return "ตุลาคม";
                case 11: return "พฤศจิกายน";
                default: return "ธันวาคม";
            }
        }

    }

    internal class StudentList
    {
        public string sName { get; set; }
        public string sLastname { get; set; }
        public string nStudentStatus { get; set; }
        public string sStudentID { get; set; }
        public DateTime? DayQuit { get; set; }
        public DateTime? moveInDate { get; set; }
        public DateTime? MoveOutDate { get; set; }
        public int DayIn { get; set; }
        public int DayOut { get; set; }
        public int StudentId { get; set; }
        public int? nStudentNumber { get; set; }
        public string NoteIn { get; set; }
        public string NoteOut { get; set; }
        public int G_Day { get; set; }
        public int nStudentStatus2 { get; internal set; }
    }
}