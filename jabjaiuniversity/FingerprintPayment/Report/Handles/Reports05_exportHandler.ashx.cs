using FingerprintPayment.Report.Functions.Report_05;
using FingerprintPayment.Report.Models;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Globalization;
using JabjaiMainClass;

namespace FingerprintPayment.Report.Handles
{
    /// <summary>
    /// Summary description for Reports05_exportHandler
    /// </summary>
    public class Reports05_exportHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }

            List<Reports_05Model> models = new List<Reports_05Model>();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";// context.Session["sEntities"].ToString();
                string entities = context.Session["sEntities"].ToString();
                var f_company = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(f_company,ConnectionDB.Read)))
                {
                    var jsonString = new StreamReader(context.Request.InputStream).ReadToEnd();
                    SearchEmployees search = JsonConvert.DeserializeObject<SearchEmployees>(jsonString);

                    DateTime dStart = DateTime.Today, dEnd = DateTime.Today;
                    if (search.sort_type == 1)
                    {
                        dStart = search.dStart;
                        search.dEnd = search.dStart.AddMonths(1).AddDays(-1);
                    }

                    reportsType_01 reports = new reportsType_01();
                    Export_excel(reports.getReports(dbschool, search), search);
                }
            }
        }

        private void Export_excel(Reports_05Model models, SearchEmployees search)
        {
            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("รายงานการมาทำงาน");

                var worksheet = excel.Workbook.Worksheets["รายงานการมาทำงาน"];
                string entities = HttpContext.Current.Session["sEntities"].ToString();
        
                var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                worksheet.Cells.AutoFitColumns();
                int rowsIndex = 1;
                int colMonth = 4 + models.headerReports.Sum(s => s.weeks.Sum(s1 => s1.days.Count()));

                SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, colMonth + 8], true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);

                SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, colMonth + 8], true, search.dStart.ToString("รายงานการเข้าทำงาน ประจำเดือน MMMM ปี yyyy", new CultureInfo("th-th")), 15, ExcelHorizontalAlignment.Center);

                SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex, 3], true, "ประเภทบุคลากร : ", null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet.Cells[rowsIndex, 4, rowsIndex, 8], true, getUserType(search.user_type, tCompany.nCompany), null, ExcelHorizontalAlignment.Left);

                SetHeader(worksheet.Cells[rowsIndex, colMonth + 4 + 6, rowsIndex, colMonth + 4 + 7], true, "พิมพ์วันที่ :", null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet.Cells[rowsIndex, colMonth + 4 + 8, rowsIndex, colMonth + 4 + 9], true, DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Left);
                SetHeader(worksheet.Cells[++rowsIndex, 1, rowsIndex, 3], true, "แผนก : ", null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet.Cells[rowsIndex, colMonth + 4 + 6, rowsIndex, colMonth + 4 + 7], true, "เวลา :", null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet.Cells[rowsIndex, colMonth + 4 + 8, rowsIndex, colMonth + 4 + 9], true, DateTime.Now.ToString("HH:mm:ss น.", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Left);

                string deparment = "ทั้งหมด";
                if (search.department_id.HasValue)
                {
                    JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read));
                    var f_deparment = dbschool.TDepartments.FirstOrDefault(f => f.DepID == search.department_id.Value);
                    if (f_deparment != null) deparment = f_deparment.departmentName;
                }

                SetHeader(worksheet.Cells[rowsIndex, 4, rowsIndex++, 8], true, deparment, null, ExcelHorizontalAlignment.Left);

                worksheet.Row(rowsIndex).Height = 15;
                worksheet.Column(3).Width = 80;

                SetTableHeader(worksheet.Cells[rowsIndex, 1, rowsIndex + 2, 1], true, "ลำดับ", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, 2, rowsIndex + 2, 2], true, "รหัส", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, 3, rowsIndex + 2, 3], true, "ชื่อพนักงาน", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, 5, rowsIndex, colMonth++], true, models.headerReports.FirstOrDefault().Month_Name, ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, colMonth, rowsIndex + 2, colMonth++], true, "สาย", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, colMonth, rowsIndex + 2, colMonth++], true, "เวลาสาย", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, colMonth, rowsIndex + 2, colMonth++], true, "ขาด", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, colMonth, rowsIndex + 2, colMonth++], true, "ลาป่วย", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, colMonth, rowsIndex + 2, colMonth++], true, "ลากิจ", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, colMonth, rowsIndex + 2, colMonth++], true, "ราชการ", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, colMonth, rowsIndex + 2, colMonth++], true, "อบรม ดูงาน", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, colMonth, rowsIndex + 2, colMonth++], true, "คลอดบุตร", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, colMonth, rowsIndex + 2, colMonth++], true, "พักผ่อน", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, colMonth, rowsIndex + 2, colMonth++], true, "อุปสมบท", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, colMonth, rowsIndex + 2, colMonth++], true, "เตรียมพล", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, colMonth, rowsIndex + 2, colMonth++], true, "รวม", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[rowsIndex, colMonth, rowsIndex + 2, colMonth++], true, "หมายเหตุ", ExcelHorizontalAlignment.Center);

                worksheet.Row(rowsIndex).Height = 24;
                SetTableHeader(worksheet.Cells[rowsIndex++, 4], true, "เดือน", ExcelHorizontalAlignment.Center);
                worksheet.Row(rowsIndex).Height = 24;
                SetTableHeader(worksheet.Cells[rowsIndex++, 4], true, "สัปดาห์", ExcelHorizontalAlignment.Center);
                worksheet.Row(rowsIndex).Height = 30;
                SetTableHeader(worksheet.Cells[rowsIndex++, 4], true, "วันที่", ExcelHorizontalAlignment.Center);

                int colWeek = 4;
                int colDay = 5;

                foreach (var headerData in models.headerReports)
                {
                    foreach (var weekData in headerData.weeks)
                    {
                        SetTableHeader(worksheet.Cells[rowsIndex - 2, colWeek + 1, rowsIndex - 2, colWeek + weekData.days.Count()], true, weekData.Weeks_Id.ToString(), ExcelHorizontalAlignment.Center);
                        foreach (var dayData in weekData.days)
                        {
                            worksheet.Column(colDay).Width = 6;
                            SetTableHeader(worksheet.Cells[rowsIndex - 1, colDay++], true, dayData.Days_Name.Replace("<br/>", "\r\n"), ExcelHorizontalAlignment.Center);
                        }
                        colWeek += weekData.days.Count();
                    }

                }

                int dataIndex = 1;
                foreach (var employeesData in models.reportsDatas)
                {
                    int dataColumns = 1;
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, string.Format("{0}", dataIndex++), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, employeesData.Code, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++, rowsIndex, dataColumns++], true, employeesData.Name, ExcelHorizontalAlignment.Left);
                    //SetTableRows(worksheet.Cells[rowsIndex, dataColumns++, rowsIndex, dataColumns++], true, employeesData.Code, ExcelHorizontalAlignment.Center);
                    //SetTableRows(worksheet.Cells[rowsIndex, dataColumns++, rowsIndex, dataColumns++], true, employeesData.Name, ExcelHorizontalAlignment.Center);
                    foreach (var scanData in employeesData.scanDatas)
                    {
                        var rng = worksheet.Cells[rowsIndex, dataColumns++];
                        ExcelRichText text1;
                        switch (scanData.Scan_StatusIn.Trim())
                        {
                            case "0":
                                text1 = rng.RichText.Add(scanData.Scan_TimeIn);
                                text1.Color = Color.Black;
                                break;
                            case "1":
                                text1 = rng.RichText.Add(scanData.Scan_TimeIn);
                                text1.Color = ColorTranslator.FromHtml("#e4922f");
                                break;
                            case "3":
                                text1 = rng.RichText.Add("ข");
                                text1.Color = Color.Red;
                                break;
                            case "11": text1 = rng.RichText.Add("ป"); break;
                            case "10": text1 = rng.RichText.Add("ล"); break;
                            case "12": text1 = rng.RichText.Add("ก"); break;
                            case "8": text1 = rng.RichText.Add(((scanData.Scan_TimeIn ?? "-").Trim() == "-" ? "หยุด" : scanData.Scan_TimeIn)); break;
                            case "9": text1 = rng.RichText.Add(((scanData.Scan_TimeIn ?? "-").Trim() == "-" ? "หยุด" : scanData.Scan_TimeIn)); break;
                            case "21": text1 = rng.RichText.Add("ค"); break;
                            case "22": text1 = rng.RichText.Add("พ"); break;
                            case "23": text1 = rng.RichText.Add("ศ"); break;
                            case "24": text1 = rng.RichText.Add("ท"); break;
                            case "25": text1 = rng.RichText.Add("อ"); break;
                            case "26": text1 = rng.RichText.Add("ร"); break;
                            //default: text1 = rng.RichText.Add(scanData.Scan_TimeIn.Trim() == "-" ? "หยุด" : scanData.Scan_TimeIn); break;
                            //default: text1 = rng.RichText.Add(""); break;
                            default:
                                text1 = rng.RichText.Add("ข");
                                text1.Color = Color.Red;
                                break;
                        }

                        ExcelRichText text2;
                        switch (scanData.Scan_StatusOut.Trim())
                        {
                            case "0": text2 = rng.RichText.Add("\r\n" + scanData.Scan_TimeOut); break;
                            case "2":
                                text2 = rng.RichText.Add("\r\n" + scanData.Scan_TimeOut);
                                text2.Color = ColorTranslator.FromHtml("#e4922f");
                                break;
                            case "3":
                                string richText = "";
                                //if (scanData.Scan_TimeIn != "-")
                                //{
                                //    //richText = scanData.Scan_TimeOut;
                                //}
                                //else
                                //{
                                if ((scanData.Scan_TimeOut ?? "-") != "-")
                                {
                                    richText = scanData.Scan_TimeOut;
                                }
                                else
                                {
                                    switch (scanData.Scan_StatusIn.Trim())
                                    {
                                        case "11": richText = "ป"; break;
                                        case "10": richText = "ล"; break;
                                        case "12": richText = "ก"; break;
                                        case "8": richText = "หยุด"; break;
                                        case "9": richText = "หยุด"; break;
                                        case "21": richText = "ค"; break;
                                        case "22": richText = "พ"; break;
                                        case "23": richText = "ศ"; break;
                                        case "24": richText = "ท"; break;
                                        case "25": richText = "อ"; break;
                                        case "26": richText = "ร"; break;
                                        default: richText = "ข"; break;
                                    }
                                }

                                //}
                                text2 = rng.RichText.Add("\r\n" + richText);
                                if (richText == "ข") text2.Color = Color.Red;
                                break;
                            case "11": text2 = rng.RichText.Add("\r\n" + "ป"); break;
                            case "10": text2 = rng.RichText.Add("\r\n" + "ล"); break;
                            case "12": text2 = rng.RichText.Add("\r\n" + "ก"); break;
                            case "8": text2 = rng.RichText.Add("\r\n" + ((scanData.Scan_TimeOut ?? "-").Trim() == "-" ? "หยุด" : scanData.Scan_TimeOut)); break;
                            case "9": text2 = rng.RichText.Add("\r\n" + ((scanData.Scan_TimeOut ?? "-").Trim() == "-" ? "หยุด" : scanData.Scan_TimeOut)); break;
                            default: text2 = rng.RichText.Add("\r\n" + ((scanData.Scan_TimeOut ?? "-").Trim() == "-" ? ((scanData.Scan_StatusIn ?? "3").Trim() == "3" ? "ข" : "หยุด") : scanData.Scan_TimeOut)); break;
                        }

                        text1.Size = 8;
                        text2.Size = 8;
                        rng.Style.WrapText = true;
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    }
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, employeesData.Sum_Status_1.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, employeesData.Time_Late.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, employeesData.Sum_Status_2.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, employeesData.Sum_Status_3.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, employeesData.Sum_Status_4.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, employeesData.Sum_Status_26.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, employeesData.Sum_Status_25.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, employeesData.Sum_Status_21.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, employeesData.Sum_Status_22.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, employeesData.Sum_Status_23.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, employeesData.Sum_Status_24.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false
                        , (employeesData.Sum_Status_1 +
                        employeesData.Sum_Status_2 +
                        employeesData.Sum_Status_3 +
                        employeesData.Sum_Status_4 +
                        employeesData.Sum_Status_26 +
                        employeesData.Sum_Status_25 +
                        employeesData.Sum_Status_21 +
                        employeesData.Sum_Status_22 +
                        employeesData.Sum_Status_23 +
                        employeesData.Sum_Status_24) + ""
                        , ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, dataColumns++], false, "", ExcelHorizontalAlignment.Center);

                    worksheet.Row(rowsIndex).Height = 30;
                    rowsIndex++;
                }

                worksheet.Column(1).Width = 5;
                worksheet.Column(2).Width = 10;
                worksheet.Column(3).Width = 8;
                worksheet.Column(colMonth++).Width = 8;
                worksheet.Column(colMonth++).Width = 8;
                worksheet.Column(colMonth++).Width = 8;
                worksheet.Column(colMonth++).Width = 8;

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

        private static void SetHeader(ExcelRange Cells, bool Merge, string strValues, int? fontSize, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
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
                rng.Style.WrapText = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Font.Color.SetColor(Color.White);
                rng.Style.Fill.BackgroundColor.SetColor(0, 51, 122, 183);
                rng.Style.Font.Size = 8;
            }
        }

        private static void SetTableRows(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.WrapText = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rng.Style.Font.Size = 8;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string getUserType(string userType, int schoolid)
        {
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(schoolid,ConnectionDB.Read)))
            {
                var type = db.TEmployeeTypes
                     .Where(o => o.SchoolID == schoolid && o.IsActive == true && o.IsDel == false && ((o.nTypeId2 ?? o.nTypeId) + "") == userType)
                     .Select(o => o.Title)
                     .FirstOrDefault();

                if (string.IsNullOrEmpty(type))
                {
                    return "ทั้งหมด";
                }
                else
                {
                    return type;
                }
            }
            //switch (userType)
            //{
            //    case "1": return "บุคลากรทั่วไป";
            //    case "2": return "ครูประจำการ";
            //    case "3": return "บุคลากรทางการศึกษา";
            //    case "4": return "ครูพิเศษ";
            //    case "5": return "ครูพี่เลี้ยง";
            //    case "6": return "ผู้บริหาร";
            //    default: return "ทั้งหมด";
            //}
        }
    }
}