using FingerprintPayment.Report.Models;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using FingerprintPayment.Report.Functions.Reports_06;
using OfficeOpenXml.Style;
using System.Globalization;
using JabjaiMainClass;
using FingerprintPayment.grade;
using System.Drawing;
using System.Net;
using FingerprintPayment.Helper;

namespace FingerprintPayment.Report.Handles
{
    /// <summary>
    /// Summary description for Reports03_exportHandler
    /// </summary>
    public class ReportEmployee_Handler : IHttpHandler, IRequiresSessionState
    {
        JWTToken.userData userData;
        TCompany school;
        List<MasterEntity.TUser> tuser;
        public void ProcessRequest(HttpContext context)
        {
            string etype = (context.Request["etype"] + "");
            string name = (context.Request["name"] + "");
            string rtype = (context.Request["rtype"] + "");

            JWTToken token = new JWTToken();
            userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                throw new Exception();
            }

            school = new TCompany();
            tuser = new List<MasterEntity.TUser>();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                school = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
                tuser = dbmaster.TUsers.Where(w => w.cType == "1" && w.nCompany == school.nCompany).ToList();
            }

            ExcelPackage excel = new ExcelPackage();

            switch (rtype)
            {
                case "1":
                    excel = Report1(etype, name);
                    break;

                case "2":
                    excel = Report2(etype, name);
                    break;


                default:
                    break;
            }

            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment; filename=รายงาน_รายชื่อพนักงาน_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
            context.Response.ContentType = "application/text";
            context.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            context.Response.BinaryWrite(excel.GetAsByteArray());
            context.Response.Flush(); // Sends all currently buffered output to the client.
            context.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            context.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
        }

        private ExcelPackage Report1(string etype, string name)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                List<Report1VM> dList = GetDataReport1(etype, name, dbschool);

                ExcelPackage excel = new ExcelPackage();

                string worksheetName = $@"รายชื่อพนักงาน";
                var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                SetSheetHeaderT1(worksheet, new List<string>(), 6, "รายชื่อพนักงงาน");

                //SetCell(worksheet.Cells[6, 1], text: "ลำดับ");
                SetCell(worksheet.Cells[6, 1], text: "ลำดับ");
                SetCell(worksheet.Cells[6, 2], text: "ประเภทพนักงาน");
                SetCell(worksheet.Cells[6, 3], text: "เบอร์โทรศัพท์");
                SetCell(worksheet.Cells[6, 4], text: "ชื่อ - นามสกุล");
                SetCell(worksheet.Cells[6, 5], text: "ตารางเวลา");
                SetCell(worksheet.Cells[6, 6], text: "รหัสลายนิ้วมือ");

                for (int row = 0; row < dList.Count; row++)
                {
                    var s = dList[row];
                    //SetCell(worksheet.Cells[7 + row, 1], text: (row + 1) + "");
                    SetCell(worksheet.Cells[7 + row, 1], text: row + 1 + "");
                    SetCell(worksheet.Cells[7 + row, 2], text: s.type);
                    SetCell(worksheet.Cells[7 + row, 3], text: s.phone);
                    SetCell(worksheet.Cells[7 + row, 4], text: s.employessname, horizotal: ExcelHorizontalAlignment.Left);
                    SetCell(worksheet.Cells[7 + row, 5], text: s.timetable);
                    SetCell(worksheet.Cells[7 + row, 6], text: s.password);

                }

                worksheet.Cells.AutoFitColumns(15, 30);

                if (dList.Count == 0)
                {
                    worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                }

                return excel;
            };
        }

        private ExcelPackage Report2(string etype, string name)
        {
            var color1 = "#ecf9ff";
            var color2 = "#fcfff5";

            List<Report2VM> dList = GetDataReport2(etype, name);

            ExcelPackage excel = new ExcelPackage();

            string worksheetName = $@"รายชื่อพนักงาน";
            var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
            SetSheetHeaderT1(worksheet, new List<string>(), 10, "รายชื่อพนักงงาน");

            int _col = 1;

            SetCell(worksheet.Cells[6, _col, 7, _col], text: "ลำดับ", isMerge: true); _col = _col + 1;
            SetCell(worksheet.Cells[6, _col, 6, _col + 26], text: "ประวัติส่วนตัว", isMerge: true, bgColor: ColorTranslator.FromHtml(color1)); _col = _col + 26;
            SetCell(worksheet.Cells[6, _col + 1, 6, _col + 10], text: "ที่อยู่ตามทะเบียนบ้าน", isMerge: true, bgColor: ColorTranslator.FromHtml(color2)); _col = _col + 10;
            SetCell(worksheet.Cells[6, _col + 1, 6, _col + 10], text: "ที่อยู่ที่ติดต่อได้", isMerge: true, bgColor: ColorTranslator.FromHtml(color1)); _col = _col + 10;
            SetCell(worksheet.Cells[6, _col + 1, 6, _col + 11], text: "ตำแหน่งงาน", isMerge: true, bgColor: ColorTranslator.FromHtml(color2)); _col = _col + 11;
            SetCell(worksheet.Cells[6, _col + 1, 6, _col + 7], text: "ข้อมูลครอบครัว", isMerge: true, bgColor: ColorTranslator.FromHtml(color1)); _col = _col + 7;
            SetCell(worksheet.Cells[6, _col + 1, 6, _col + 6], text: "ข้อมูลการศึกษา", isMerge: true, bgColor: ColorTranslator.FromHtml(color2)); _col = _col + 6;
            SetCell(worksheet.Cells[6, _col + 1, 6, _col + 4], text: "ข้อมูลเกียรติคุณ", isMerge: true, bgColor: ColorTranslator.FromHtml(color1)); _col = _col + 4;
            SetCell(worksheet.Cells[6, _col + 1, 6, _col + 5], text: "ประวัติการศึกษา อบรม ดูงาน", isMerge: true, bgColor: ColorTranslator.FromHtml(color2)); _col = _col + 5;
            SetCell(worksheet.Cells[6, _col + 1, 6, _col + 6], text: "ใบอนุญาตประกอบวิชาชีพ", isMerge: true, bgColor: ColorTranslator.FromHtml(color1)); _col = _col + 6;
            SetCell(worksheet.Cells[6, _col + 1, 6, _col + 5], text: "ประวัติการรับเครื่องราชฯ", isMerge: true, bgColor: ColorTranslator.FromHtml(color2));

            _col = 2;
            SetCell(worksheet.Cells[7, _col++], text: "ประเภทบุคลากร", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "รหัสพนักงาน", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ตำแหน่ง", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "แผนก", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "เพศ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "คำนำหน้านาม", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ชื่อ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "นามสกุล", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ชื่อ(ภาษาอังกฤษ)", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "นามสกุล(ภาษาอังกฤษ)", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ยอดเงินคงเหลือ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "เลขบัตรประชาชน", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "เลขที่หนังสือเดินทาง", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ประเทศหนังสือเดินทาง", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "วันเกิด", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "กรุ๊ปเลือด", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "สัญชาติ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "เชื้อชาติ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ศาสนา", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "สถานภาพสมรส", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ชื่อคู่สมรส", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "นามสกุลคู่สมรส", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "หมายเลขโทรศัพท์", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "อีเมล์", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ตารางเวลา", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ใช้ระบบ Biometric", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "รหัสลายนิ้วมือ", bgColor: ColorTranslator.FromHtml(color1));

            SetCell(worksheet.Cells[7, _col++], text: "บ้านเลขที่", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "หมู่ที่", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "หมู่บ้าน", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "ซอย", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "อาคาร/ชั้น", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "ถนน", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "จังหวัด", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "เขต/อำเภอ", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "แขวง/ตำบล", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "รหัสไปรษณีย์", bgColor: ColorTranslator.FromHtml(color2));

            SetCell(worksheet.Cells[7, _col++], text: "บ้านเลขที่", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "หมู่ที่", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "หมู่บ้าน", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ซอย", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "อาคาร/ชั้น", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ถนน", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "จังหวัด", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "เขต/อำเภอ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "แขวง/ตำบล", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "รหัสไปรษณีย์", bgColor: ColorTranslator.FromHtml(color1));

            SetCell(worksheet.Cells[7, _col++], text: "สถานภาพการทำงาน", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "ระดับ", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "วันที่ปฏิบัติงานในสถานศึกษา", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "วันสั่งบรรจุ", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "เงินเดือน", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "วันเริ่มปฏิบัติราชการ", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "เงินประจำตำแหน่ง", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "จำนวนเงินวิทยฐานะ", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "วันครบเกษียณอายุ", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "รายได้สุทธิรวม", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "อายุงานเหลือ", bgColor: ColorTranslator.FromHtml(color2));

            SetCell(worksheet.Cells[7, _col++], text: "ลำดับ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ความสัมพันธ์", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "คำนำหน้า", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ชื่อ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "นามสกุล", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "วันเกิด", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "สถานภาพ", bgColor: ColorTranslator.FromHtml(color1));

            SetCell(worksheet.Cells[7, _col++], text: "ลำดับ", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "ปีที่", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "ปีที่", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "ระดับการศึกษา", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "วิชาเอก", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "สถาบันการศึกษา", bgColor: ColorTranslator.FromHtml(color2));

            SetCell(worksheet.Cells[7, _col++], text: "ลำดับ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ประเภทเกียรติคุณ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "หน่วยงาน", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ปีที่ได้รับ", bgColor: ColorTranslator.FromHtml(color1));

            SetCell(worksheet.Cells[7, _col++], text: "ลำดับ", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "โครงการ", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "ชื่อหลักสูตรอบรม", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "ว/ด/ป ที่เริ่ม", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "ว/ด/ป ที่สิ้น", bgColor: ColorTranslator.FromHtml(color2));

            SetCell(worksheet.Cells[7, _col++], text: "ลำดับ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ประเภทใบอนุญาต", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "เลขที่ใบประกอบ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ชื่อใบประกอบ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ว/ด/ป ออกใบประกอบ", bgColor: ColorTranslator.FromHtml(color1));
            SetCell(worksheet.Cells[7, _col++], text: "ว/ด/ป หมดอายุ", bgColor: ColorTranslator.FromHtml(color1));

            SetCell(worksheet.Cells[7, _col++], text: "ลำดับ", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "ปีที่ได้รับ", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "ชั้นเครื่องราชฯ", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "ตำแหน่ง", bgColor: ColorTranslator.FromHtml(color2));
            SetCell(worksheet.Cells[7, _col++], text: "ลงวันที่", bgColor: ColorTranslator.FromHtml(color2));

            //            Dictionary<int, string> myDietFavorites = new Dictionary<int, string>()
            //{
            //    { 1, "Burger"},
            //    { 2, "Fries"},
            //    { 3, "Donuts"}
            //};

            var personalStatus = new Dictionary<string, string>() {
                    { "1", "โสด"},
                    { "2", "สมรส"},
                    { "3", "หม้าย"},
                    { "4", "หย่า"},
                    { "5", "แยกกันอยู่"},
                    { "", ""}
                };

            var workStatus = new Dictionary<string, string>(){
                    { "1", "ทำงาน"},
                    { "2", "ลาออก"},
                    { "3", "พักงาน"},
                    { "", ""}
                };

            var licenseType = new Dictionary<string, string>(){
                    { "1", "ครู" },
                    { "2", "ผู้บริหารสถานศึกษา" },
                    { "3", "ผู้บริหารการศึกษา" },
                    { "4", "ศึกษานิเทศก์" },
                    { "", ""}
                };
            //dList = dList.OrderBy(o => o.type).ToList();

            var startRow = 8;

            for (int row = 0; row < dList.Count; row++)
            {
                var c = 1;
                var r = dList[row];

                var _info = r.info.ElementAtOrDefault(0) ?? new Report2VMInfo();
                var _address = r.address.ElementAtOrDefault(0) ?? new Report2VMAddress();
                var _salary = r.salary.ElementAtOrDefault(0) ?? new Report2VMSalary();

                //SetCell(worksheet.Cells[7 + row, 1], text: (row + 1) + "");
                //SetCell(worksheet.Cells[7 + row, 1], text: row + 1 + "");
                //SetCell(worksheet.Cells[7 + row, 2], text: s.type);
                //SetCell(worksheet.Cells[7 + row, 3], text: s.phone);
                //SetCell(worksheet.Cells[7 + row, 4], text: s.employessname, horizotal: ExcelHorizontalAlignment.Left);
                //SetCell(worksheet.Cells[7 + row, 5], text: s.timetable);
                //SetCell(worksheet.Cells[7 + row, 6], text: s.password);

                var maxRow = new List<int>() {
                    r.address.Count(),
                    r.education.Count(),
                    r.family.Count(),
                    r.honor.Count(),
                    r.insignia.Count(),
                    r.info.Count(),
                    r.license.Count(),
                    r.teaching.Count(),
                    r.training.Count(),
                    r.salary.Count(),
                }.Max();

                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (row + 1) + "");
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.type));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_info.Code));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.job));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.dept));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.cSex == "0" ? "ชาย" : "หญิง"));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.title));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.sName));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.sLastname));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_info.FirstNameEn));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_info.LastNameEn));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: ((r.emp.nMoney ?? 0).ToString("#,0.##")));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.sIdentification));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_info.PassportNumber));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_info.PassportCountry));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.dob));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_info.BloodType));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_info.Nationality));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_info.Ethnicity));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_info.Religion));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (personalStatus[_info.PersonalStatus + ""]));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_info.SpouseFirstName));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_info.SpouseLastName));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: r.emp.sPhone);
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: r.emp.sEmail);
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: r.timetype);
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: r.bio);
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: r.password);

                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.sHomeNumber));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.sMuu));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.Village));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.sSoy));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.Building));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.sRoad));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.sProvince));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.sAumpher));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.sTumbon));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (r.emp.sPost));

                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_address.No));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_address.VillageNo));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_address.Village));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_address.Alley));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_address.Building));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_address.Road));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_address.Province));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_address.Amphur));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_address.Tubmon));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_address.Postcode));

                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (workStatus[_salary.WorkStatus + ""]));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_salary.Degree));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_salary.WorkInEducationDate));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_salary.GovernmentOrderDate));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_salary.Salary) + "");
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_salary.WorkStartDate));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_salary.PositionMoney) + "");
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_salary.AcademicStandingMoney) + "");
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_salary.RetirementDate));
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_salary.NetSalary) + "");
                SetCell(worksheet.Cells[startRow + row, c, startRow + row, c++], text: (_salary.RemainGovernmentDay) + "");


                for (int i = 0; i < maxRow + 1; i++)
                {
                    var _c = c;
                    var _family = r.family.ElementAtOrDefault(i) ?? new Report2VMFamily();

                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_family.ID) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_family.FamilyRelation) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_family.Title) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_family.FirstName) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_family.LastName) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_family.dob) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (string.IsNullOrEmpty(_family.LiveStatus) ? _family.DeathStatus : _family.LiveStatus) + "");

                    var _edu = r.education.ElementAtOrDefault(i) ?? new Report2VMEdu();
                    //var _eduDesc = r.educationDesc[i] || {};

                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_edu.ID) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_edu.StudyYear) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_edu.GraduationYear) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_edu.Level) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_edu.Major) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_edu.Institution) + "");

                    var _honor = r.honor.ElementAtOrDefault(i) ?? new Report2VMHonor();
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_honor.ID) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_honor.Type) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_honor.Department) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_honor.Year) + "");

                    var _training = r.training.ElementAtOrDefault(i) ?? new Report2VMTrain();
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_training.ID) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_training.ProjectName) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_training.TrainingName) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_training.StartDate) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_training.EndDate) + "");

                    var _license = r.license.ElementAtOrDefault(i) ?? new Report2VMLicense();
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_license.ID) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (licenseType[_license.LicenseType + ""]) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_license.LicenseNo) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_license.LicenseName) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_license.IssuedDate) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_license.ExpireDate) + "");

                    var _insignia = r.insignia.ElementAtOrDefault(i) ?? new Report2VMInsig();
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_insignia.ID) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_insignia.Year) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_insignia.Grade) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_insignia.Position) + "");
                    SetCell(worksheet.Cells[startRow + row + i, _c++], text: (_insignia.Date) + "");

                }

                startRow = startRow + maxRow - 1;
            }

            worksheet.Cells.AutoFitColumns(15, 30);

            if (dList.Count == 0)
            {
                worksheetName = $"ไม่มีข้อมูล";
                excel.Workbook.Worksheets.Add(worksheetName);
            }

            return excel;

        }

        private List<Report1VM> GetDataReport1(string etype, string name, JabJaiEntities dbschool)
        {
            var qry1 = dbschool.TEmployees.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID);

            if (!string.IsNullOrEmpty(etype))
            {
                qry1 = qry1.Where(o => o.cType == etype);
            }

            var q1 = (from a in qry1
                      from c in dbschool.TTimetypes.Where(w => w.SchoolID == userData.CompanyID && a.nTimeType == w.nTimeType).DefaultIfEmpty()
                      from d in dbschool.TEmployeeTypes.Where(o => o.SchoolID == userData.CompanyID && (o.nTypeId2 ?? o.nTypeId) + "" == a.cType).DefaultIfEmpty()
                      from m in dbschool.TEmpSalaries.Where(o => o.SchoolID == a.SchoolID && o.cDel == false && o.sEmp == a.sEmp).DefaultIfEmpty()
                      from f in dbschool.TTitleLists.Where(o => o.nTitleid + "" == a.sTitle && o.SchoolID == a.SchoolID).DefaultIfEmpty()

                      where  (a.sName + " " + a.sLastname).Contains(name) && m.WorkStatus != 2

                      select new
                      {
                          a.sEmp,
                          a.nTimeType,
                          cType = a.cType ?? "-",
                          title = f == null ? a.sTitle : f.titleDescription,
                          a.sName,
                          a.sLastname,
                          a.sPhone,
                          type = d.Title,//a.cType.Trim() == "2" ? "อาจารย์" : "พนักงาน",
                          c.sTimeType
                      })
            .ToList();

            var d1 = (from a in q1
                      from b in tuser.Where(w => a.sEmp == w.nSystemID.Value).DefaultIfEmpty()

                      orderby a.cType, a.sName

                      select new Report1VM
                      {
                          employessname = a.title + " " +  a.sName + " " + a.sLastname,
                          phone = a.sPhone,
                          type = a.type,
                          password = string.IsNullOrEmpty(b.sFinger) ? b.sPassword : "",
                          timetable = a.sTimeType ?? ""
                      });

            return d1.OrderBy(o => o.type).ToList();
        }

        private List<Report2VM> GetDataReport2(string etype, string name)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var schoolID = userData.CompanyID;

                var qry1 = dbschool.TEmployees.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID);

                if (!string.IsNullOrEmpty(etype))
                {
                    qry1 = qry1.Where(o => o.cType == etype);
                }

                var q1 =


                              (from a in qry1

                               from b1 in dbschool.TEmployeeTypes.Where(o => o.SchoolID == schoolID && (o.nTypeId2 ?? o.nTypeId) + "" == a.cType).DefaultIfEmpty()

                               from b2 in dbschool.TJobLists.Where(o => o.nJobid == a.nJobid && o.SchoolID == schoolID && o.cDel == false).DefaultIfEmpty()

                               from b3 in dbschool.TDepartments.Where(o => o.DepID == a.nDepartmentId && o.SchoolID == schoolID && o.cDel == false).DefaultIfEmpty()

                               from j in dbschool.TEmpSalaries.Where(o => o.SchoolID == schoolID && o.sEmp == a.sEmp && o.cDel == false).DefaultIfEmpty()
                                   // from b4 in dbschool.TTitleLists.Where(o => o.nTitleid + "" == a.sTitle && o.SchoolID == schoolID && o.cDel == false)
                                   //.DefaultIfEmpty()

                               join c in dbschool.TEmpAddresses.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals c.sEmp into addressList
                               join d in dbschool.TEmpEducationInfoes.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals d.sEmp into eduList
                               join e in dbschool.TEmpFamilies.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals e.sEmp into famList
                               join f in dbschool.TEmpHonors.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals f.sEmp into honorList
                               join g in dbschool.TEmpInsignias.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals g.sEmp into insiList
                               join h in dbschool.TEmployeeInfoes.Where(o => o.SchoolID == schoolID) on a.sEmp equals h.sEmp into infoList
                               join i in dbschool.TEmpProfessionalLicenses.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals i.sEmp into licensList
                               //from j in dbschool.TEmpSalaries.Where(o => o.SchoolID == schoolID && o.sEmp == a.sEmp).DefaultIfEmpty()
                               join k in dbschool.TEmpTeachings.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals k.sEmp into teachList
                               join l in dbschool.TEmpTrainings.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals l.sEmp into trainList
                               join m in dbschool.TEmpSalaries.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals m.sEmp into salaryList

                               where  (a.sName + " " + a.sLastname).Contains(name) && j.WorkStatus != 2

                               select new
                               {
                                   a,

                                   type = b1.Title,
                                   job = b2.jobDescription,
                                   dept = b3.departmentName,
                                   //title = b4.titleDescription,
                                   //dob = a.dBirth.Value.ToString("dd/MM/yyyy" , new CultureInfo("th-TH")),

                                   address = addressList.Select(i => i).ToList(),
                                   //addressDesc = addressList.Select(i => new Report2ModelAddress
                                   //{
                                   //    ID = i.ID,
                                   //    Tubmon = "",
                                   //    Amphur = "",
                                   //    Province = "",
                                   //}).ToList(),

                                   education = eduList.Select(i => i).ToList(),
                                   family = famList.Select(i => i).ToList(),
                                   honor = honorList.Select(i => i).ToList(),
                                   training = trainList.Select(i => i).ToList(),
                                   insignia = insiList.Select(i => i).ToList(),
                                   info = infoList.Select(i => i).ToList(),
                                   license = licensList.Select(i => i).ToList(),
                                   teaching = teachList.Select(i => i).ToList(),
                                   salary = salaryList.Select(i => i).ToList(),
                               });


                var _d1 = q1.ToList();
                //var master = dbschool.TMasterDatas.ToList();
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    //var usermaster = dbmaster.TUsers.Where(w => w.nCompany == userData.CompanyID && w.cType == "1").ToList();

                    var d_provinces = dbmaster.provinces.Select(s => new ModelItem1 { Id = s.PROVINCE_ID + "", Name = s.PROVINCE_NAME }).ToList();
                    var d_districts = dbmaster.districts.Select(s => new ModelItem1 { Id = s.DISTRICT_ID + "", Name = s.DISTRICT_NAME }).ToList();
                    var d_amphurs = dbmaster.amphurs.Select(s => new ModelItem1 { Id = s.AMPHUR_ID + "", Name = s.AMPHUR_NAME }).ToList();

                    var q_titles = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                    var nation = dbschool.TMasterDatas.Where(w => w.MasterType == "3").ToList();
                    var religion = dbschool.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                    var race = dbschool.TMasterDatas.Where(w => w.MasterType == "9").ToList();
                    var timetypes = dbschool.TTimetypes.Where(w => w.SchoolID == userData.CompanyID && w.cUserType == "2").ToList();
                    //var levels = dbschool.TLevels.Where(w => w.SchoolID == schoolID).ToList();
                    var levels = new List<EduLevel>() {
                        new EduLevel {
                            LevelID = 1 ,
                            LevelName ="ต่ำกว่าประถมศึกษา"
                        },
                        new EduLevel {
                            LevelID = 2 ,
                            LevelName ="ประถมศึกษา"
                        },
                        new EduLevel {
                            LevelID = 3 ,
                            LevelName ="มัธยมศึกษาหรือเทียบเท่า"
                        },
                        new EduLevel {
                            LevelID = 4 ,
                            LevelName ="ปริญญาตรี หรือเทียบเท่า"
                        },
                         new EduLevel {
                            LevelID = 5 ,
                            LevelName ="ปริญญาโท"
                        },
                         new EduLevel {
                            LevelID = 6 ,
                            LevelName ="ปริญญาเอก"
                        },
                            new EduLevel {
                            LevelID = 7 ,
                            LevelName ="ประกาศนียบัตรวิชาชีพ(ปวช.)"
                        },
                        new EduLevel {
                            LevelID = 8 ,
                            LevelName ="ประกาศนียบัตรวิชาชีพขั้นสูง(ปวส.)"
                        },
                        new EduLevel {
                            LevelID = 9 ,
                            LevelName ="ประกาศนียบัตรบัณฑิตวิชาชีพครู(ป.บัณฑิต)"
                        },
                    };

                    var d1 = from o in _d1
                             from b in tuser.Where(i => i.sID == o.a.sEmp).DefaultIfEmpty()

                             select new Report2VM
                             {

                                 emp = new Report2VMEmp
                                 {
                                     cSex = o.a.cSex,
                                     sName = o.a.sName,
                                     sLastname = o.a.sLastname,
                                     sIdentification = o.a.sIdentification,
                                     sHomeNumber = o.a.sHomeNumber,
                                     sMuu = o.a.sMuu,
                                     Village = o.a.Village,
                                     sSoy = o.a.sSoy,
                                     Building = o.a.Building,
                                     sRoad = o.a.sRoad,
                                     nMoney = o.a.nMoney,
                                     //sProvince = o.a.sProvince,
                                     //sAumpher = o.a.sAumpher,
                                     //sTumbon = o.a.sTumbon,

                                     sTumbon = getAddress(d_districts, o.a.sTumbon + ""),
                                     sAumpher = getAddress(d_amphurs, o.a.sAumpher + ""),
                                     sProvince = getAddress(d_provinces, o.a.sProvince + ""),

                                     sPost = o.a.sPost,
                                     sEmail = o.a.sEmail,
                                     sPhone = o.a.sPhone,
                                 },

                                 type = o.type,
                                 job = o.job,
                                 dept = o.dept,

                                 dob = o.a.dBirth?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                 title = getTitlte(q_titles, o.a.sTitle),
                                 timetype = getTimeType(timetypes, o.a.nTimeType),
                                 bio = b.UseBiometric ? "ใช้" : "ไม่ใช้",
                                 password = string.IsNullOrEmpty(b.sFinger) ? b.sPassword : "",

                                 sTumbon = getAddress(d_districts, o.a.sTumbon + ""),
                                 sAumpher = getAddress(d_amphurs, o.a.sAumpher + ""),
                                 sProvince = getAddress(d_provinces, o.a.sProvince + ""),

                                 address = o.address.Select((i, j) => new Report2VMAddress
                                 {
                                     ID = j + 1,
                                     No = i.No,
                                     VillageNo = i.VillageNo,
                                     Village = i.Village,
                                     Building = i.Building,
                                     Alley = i.Alley,
                                     Road = i.Road,
                                     Postcode = i.Postcode,

                                     Tubmon = getAddress(d_districts, i.SubdistrictID + ""),
                                     Amphur = getAddress(d_amphurs, i.DistrictID + ""),
                                     Province = getAddress(d_provinces, i.ProvinceID + ""),
                                 }).OrderBy(y => y.ID),

                                 education = o.education.Select((i, j) => new Report2VMEdu
                                 {
                                     ID = j + 1,
                                     Institution = i.Institution,
                                     StudyYear = i.StudyYear,
                                     GraduationYear = i.GraduationYear,
                                     //i.LevelID,
                                     Major = i.Major,
                                     MinorSubject = i.MinorSubject,

                                     Level = getLevel(levels, i.LevelID),
                                 }).OrderBy(y => y.ID),

                                 family = o.family.Select((i, j) => new Report2VMFamily
                                 {
                                     ID = j + 1,
                                     FamilyRelation = i.FamilyRelation,
                                     //i.TitleID,
                                     FirstName = i.FirstName,
                                     LastName = i.LastName,
                                     //i.Birthday,
                                     PersonalStatus = i.PersonalStatus,
                                     LiveStatus = i.LiveStatus,
                                     DeathStatus = i.DeathStatus,
                                     FamilyCareer = i.FamilyCareer,
                                     //i.LevelID,

                                     Title = getTitlte(q_titles, i.TitleID + ""),
                                     Level = getLevel(levels, i.LevelID),
                                     dob = i.Birthday?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                 }).OrderBy(y => y.ID),

                                 honor = o.honor.Select((i, j) => new Report2VMHonor
                                 {
                                     ID = j + 1,
                                     Type = i.Type,
                                     Department = i.Department,
                                     Year = i.Year,
                                 }).OrderBy(y => y.ID),

                                 insignia = o.insignia.Select((i, j) => new Report2VMInsig
                                 {
                                     ID = j + 1,
                                     Year = i.Year,
                                     Grade = i.Grade,
                                     Position = i.Position,
                                     BookNumber = i.BookNumber,
                                     Part = i.Part,
                                     Duty = i.Duty,
                                     Number = i.Number,
                                     Date = i.Date?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                 }).OrderBy(y => y.ID),

                                 info = o.info.Select((i, j) => new Report2VMInfo
                                 {
                                     ID = j + 1,
                                     Code = i.Code,
                                     FirstNameEn = i.FirstNameEn,
                                     LastNameEn = i.LastNameEn,
                                     PassportNumber = i.PassportNumber,
                                     PassportCountry = i.PassportCountry,
                                     BloodType = i.BloodType,
                                     //i.Nationality,
                                     //i.Ethnicity,
                                     //i.Religion,
                                     PersonalStatus = i.PersonalStatus,
                                     SpouseFirstName = i.SpouseFirstName,
                                     SpouseLastName = i.SpouseLastName,

                                     Religion = Common.geTReligion(religion, i.Religion),
                                     Ethnicity = Common.geTRace(race, i.Ethnicity),
                                     Nationality = Common.geTNation(nation, i.Nationality),
                                 }).OrderBy(y => y.ID),

                                 license = o.license.Select((i, j) => new Report2VMLicense
                                 {
                                     ID = j + 1,
                                     LicenseType = i.LicenseType,
                                     LicenseNo = i.LicenseNo,
                                     LicenseName = i.LicenseName,
                                     IssuedDate = i.IssuedDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     ExpireDate = i.ExpireDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     AgencyIssued = i.AgencyIssued,
                                 }).OrderBy(y => y.ID),

                                 teaching = o.teaching.Select((i, j) => new Report2VMTeaching
                                 {
                                     ID = j + 1,
                                     nYear = i.nYear,
                                     nTerm = i.nTerm,
                                     courseTypeId = i.courseTypeId,
                                     SUBJECT_ID = i.SUBJECT_ID,
                                     sClassID = i.sClassID,
                                     sRoomID = i.sRoomID,
                                     HoursPerWeek = i.HoursPerWeek,
                                     DirectTeaching = i.DirectTeaching,
                                     CompetentTeaching = i.CompetentTeaching,
                                     WantTrain = i.WantTrain,
                                 }).OrderBy(y => y.ID),

                                 training = o.training.Select((i, j) => new Report2VMTrain
                                 {
                                     ID = j + 1,
                                     ProjectName = i.ProjectName,
                                     TrainingName = i.TrainingName,
                                     //i.StartDate,
                                     //i.EndDate,
                                     Place = i.Place,
                                     Country = i.Country,
                                     Expenses = i.Expenses,
                                     Province = i.Province,
                                     TrainingType = i.TrainingType,
                                     TrainingHours = i.TrainingHours,

                                     StartDate = i.StartDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     EndDate = i.EndDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                 }).OrderBy(y => y.ID),

                                 salary = o.salary.Select((i, j) => new Report2VMSalary
                                 {
                                     ID = j + 1,
                                     WorkStatus = i.WorkStatus,
                                     WorkInEducationDate = i.WorkInEducationDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     Salary = i.Salary,
                                     PositionMoney = i.PositionMoney,
                                     RetirementDate = i.RetirementDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     RemainGovernmentYear = i.RemainGovernmentYear,
                                     RemainGovernmentMonth = i.RemainGovernmentMonth,
                                     RemainGovernmentDay = i.RemainGovernmentDay,
                                     Degree = i.Degree,
                                     GovernmentOrderDate = i.GovernmentOrderDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     WorkStartDate = i.WorkStartDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     AcademicStandingMoney = i.AcademicStandingMoney,
                                     NetSalary = i.NetSalary,
                                     DayQuit = i.DayQuit,
                                 }).OrderBy(y => y.ID),
                             };

                    return d1.OrderBy(o => o.type).ToList();


                }
            }
        }

        //private void SetCell(ExcelWorksheet excelWorksheet
        //    , string Cells
        //    , bool isMerge = false
        //    , string text = ""
        //    , int fontSize = 11
        //    , bool isBold = false
        //    , ExcelHorizontalAlignment horizotal = ExcelHorizontalAlignment.Center
        //    , ExcelVerticalAlignment vetical = ExcelVerticalAlignment.Center
        //    )
        //{
        //    using (ExcelRange rng = excelWorksheet.Cells[Cells])
        //    {
        //        rng.Merge = isMerge;
        //        rng.Value = text;
        //        rng.Style.Font.Bold = isBold;
        //        rng.Style.HorizontalAlignment = horizotal;
        //        rng.Style.VerticalAlignment = vetical;
        //        rng.Style.Font.Size = fontSize;
        //    }
        //}

        private void SetSheetHeaderT1(ExcelWorksheet worksheet
          , List<string> addedCol
          , int tableCol
          , string reportName)
        {
            if (!string.IsNullOrEmpty(school.sImage))
            {
                var request = WebRequest.Create(school.sImage);

                Image logo;

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    logo = Image.FromStream(stream);
                }

                using (logo)
                {
                    var excelImage = worksheet.Drawings.AddPicture("School Logo", logo);

                    excelImage.SetPosition(0, 5, 0, 5);
                    excelImage.SetSize(100, 100);
                }
            }


            tableCol = tableCol - 1;
            var addColLength = addedCol.Count;

            SetCell(worksheet.Cells[1, 1, 5, 1], true);

            SetCell(worksheet.Cells[1, 2, 1, tableCol + addColLength]
                , isMerge: true
                , text: school.sCompany
                , horizotal: ExcelHorizontalAlignment.Left
                , fontSize: 14
                , isBold: true);

            SetCell(worksheet.Cells[2, 2, 2, tableCol + addColLength]
                , isMerge: true
                , text: school.sAddress
                , horizotal: ExcelHorizontalAlignment.Left
                , fontSize: 12
                , isBold: true);

            SetCell(worksheet.Cells[3, 2, 3, tableCol + addColLength]
                , isMerge: true
                , text: $"โทรศัพท์ : {school.sPhoneOne + (string.IsNullOrEmpty(school.sPhoneTwo) ? "" : " ," + school.sPhoneTwo)}  โทรสาร : {school.sFax}"
                , horizotal: ExcelHorizontalAlignment.Left
                , fontSize: 12
                , isBold: true);


            SetCell(worksheet.Cells[4, 2, 4, tableCol + addColLength]
               , isMerge: true
               , text: $"{school.sWebsite}  อีเมล์ : {school.sEmailOne + (string.IsNullOrEmpty(school.sEmailTwo) ? "" : " ," + school.sEmailTwo)}"
               , horizotal: ExcelHorizontalAlignment.Left
               , fontSize: 12
               , isBold: true);


            //SetCell(worksheet.Cells[5, 2, 5, tableCol + addColLength]
            //  , isMerge: true
            //  , text: summary
            //  , horizotal: ExcelHorizontalAlignment.Left
            //  , fontSize: 12
            //  , isBold: true);

            SetCell(worksheet.Cells[1, tableCol + 1 + addColLength, 5, tableCol + 1 + addColLength]
              , isMerge: true
              , text: reportName
              , horizotal: ExcelHorizontalAlignment.Center
              , fontSize: 12
              , isBold: true);
        }

        private void SetCell(ExcelRange xrange
            , bool isMerge = false
            , string text = ""
            , int fontSize = 11
            , bool isBold = false
            , ExcelHorizontalAlignment horizotal = ExcelHorizontalAlignment.Center
            , ExcelVerticalAlignment vetical = ExcelVerticalAlignment.Center
            , Color? bgColor = null
            )
        {

            using (xrange)
            {
                xrange.Merge = isMerge;
                xrange.Value = text;
                xrange.Style.Font.Bold = isBold;
                xrange.Style.HorizontalAlignment = horizotal;
                xrange.Style.VerticalAlignment = vetical;
                xrange.Style.Font.Size = fontSize;
                if (bgColor.HasValue)
                {
                    xrange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    xrange.Style.Fill.BackgroundColor.SetColor(bgColor.Value);
                }
                xrange.AutoFitColumns();

            }
        }

        private static string getLevel(List<EduLevel> levels, int? id)
        {
            return levels.FirstOrDefault(o => o.LevelID == id)?.LevelName + "";
        }

        private static string getTimeType(List<TTimetype> timetypes, int? nTimeType)
        {
            return timetypes.FirstOrDefault(o => o.nTimeType == nTimeType)?.sTimeType + "";
        }

        private string getTitlte(List<TTitleList> titles, string titlesId)
        {
            int nTitleid = 0;
            int.TryParse((titlesId ?? "0"), out nTitleid);
            var f_titles = titles.FirstOrDefault(f => f.nTitleid == nTitleid);
            if (f_titles == null) return titlesId;
            else return f_titles.titleDescription;
        }

        private string getAddress(List<ModelItem1> lst, string id)
        {
            return lst.FirstOrDefault(o => o.Id == id)?.Name + "";
        }

        private int? ToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }




        private class Report1VM
        {
            public string employessname { get; set; }
            public string phone { get; set; }
            public string type { get; set; }
            public string password { get; set; }
            public string timetable { get; set; }
        }

        private class Report2VMInfo
        {
            public int? ID { get; set; }
            public string Code { get; set; }
            public string FirstNameEn { get; set; }
            public string LastNameEn { get; set; }
            public string PassportNumber { get; set; }
            public string PassportCountry { get; set; }
            public string BloodType { get; set; }
            public int? PersonalStatus { get; set; }
            public string SpouseFirstName { get; set; }
            public string SpouseLastName { get; set; }
            public string Religion { get; set; }
            public string Ethnicity { get; set; }
            public string Nationality { get; set; }
        }

        private class Report2VMSalary
        {
            public int? ID { get; set; }
            public int? WorkStatus { get; set; }
            public string WorkInEducationDate { get; set; }
            public decimal? Salary { get; set; }
            public decimal? PositionMoney { get; set; }
            public string RetirementDate { get; set; }
            public int? RemainGovernmentYear { get; set; }
            public int? RemainGovernmentMonth { get; set; }
            public int? RemainGovernmentDay { get; set; }
            public string Degree { get; set; }
            public string GovernmentOrderDate { get; set; }
            public string WorkStartDate { get; set; }
            public decimal? AcademicStandingMoney { get; set; }
            public decimal? NetSalary { get; set; }
            public DateTime? DayQuit { get; set; }
        }

        private class Report2VMTrain
        {
            public int? ID { get; set; }
            public string ProjectName { get; set; }
            public string TrainingName { get; set; }
            public string Place { get; set; }
            public string Country { get; set; }
            public decimal? Expenses { get; set; }
            public string Province { get; set; }
            public string TrainingType { get; set; }
            public decimal? TrainingHours { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }

        private class Report2VMTeaching
        {
            public int? ID { get; set; }
            public int? nYear { get; set; }
            public string nTerm { get; set; }
            public int? courseTypeId { get; set; }
            public int? SUBJECT_ID { get; set; }
            public string sClassID { get; set; }
            public string sRoomID { get; set; }
            public int? HoursPerWeek { get; set; }
            public int? DirectTeaching { get; set; }
            public int? CompetentTeaching { get; set; }
            public int? WantTrain { get; set; }
        }

        private class Report2VMLicense
        {
            public int? ID { get; set; }
            public int? LicenseType { get; set; }
            public string LicenseNo { get; set; }
            public string LicenseName { get; set; }
            public string IssuedDate { get; set; }
            public string ExpireDate { get; set; }
            public string AgencyIssued { get; set; }
        }

        private class Report2VMInsig
        {
            public int? ID { get; set; }
            public int? Year { get; set; }
            public string Grade { get; set; }
            public string Position { get; set; }
            public string BookNumber { get; set; }
            public string Part { get; set; }
            public string Duty { get; set; }
            public string Number { get; set; }
            public string Date { get; set; }
        }

        private class Report2VMHonor
        {
            public int? ID { get; set; }
            public string Type { get; set; }
            public string Department { get; set; }
            public int? Year { get; set; }
        }

        private class Report2VMFamily
        {
            public int? ID { get; set; }
            public string FamilyRelation { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int? PersonalStatus { get; set; }
            public string LiveStatus { get; set; }
            public string DeathStatus { get; set; }
            public string FamilyCareer { get; set; }
            public string Title { get; set; }
            public string Level { get; set; }
            public string dob { get; set; }
        }

        private class Report2VMEdu
        {
            public int? ID { get; set; }
            public string Institution { get; set; }
            public int? StudyYear { get; set; }
            public int? GraduationYear { get; set; }
            public string Major { get; set; }
            public string MinorSubject { get; set; }
            public string Level { get; set; }
        }

        private class Report2VMAddress
        {
            public int? ID { get; set; }
            public string No { get; set; }
            public string VillageNo { get; set; }
            public string Village { get; set; }
            public string Building { get; set; }
            public string Alley { get; set; }
            public string Road { get; set; }
            public string Postcode { get; set; }
            public string Tubmon { get; set; }
            public string Amphur { get; set; }
            public string Province { get; set; }
        }

        private class Report2VMEmp
        {
            public string cSex { get; set; }
            public string sName { get; set; }
            public string sLastname { get; set; }
            public string sIdentification { get; set; }
            public string sHomeNumber { get; set; }
            public string sMuu { get; set; }
            public string Village { get; set; }
            public string sSoy { get; set; }
            public string Building { get; set; }
            public string sRoad { get; set; }
            public string sProvince { get; set; }
            public string sAumpher { get; set; }
            public string sTumbon { get; set; }
            public string sPost { get; set; }
            public string sEmail { get; set; }
            public string sPhone { get; set; }
            public decimal? nMoney { get; internal set; }
        }

        private class Report2VM
        {
            public Report2VMEmp emp { get; set; }
            public string type { get; set; }
            public string job { get; set; }
            public string dept { get; set; }
            public string dob { get; set; }
            public string title { get; set; }
            public string timetype { get; set; }
            public string bio { get; set; }
            public string password { get; set; }
            public string sTumbon { get; set; }
            public string sAumpher { get; set; }
            public string sProvince { get; set; }
            public IEnumerable<Report2VMAddress> address { get; set; }
            public IEnumerable<Report2VMEdu> education { get; set; }
            public IEnumerable<Report2VMFamily> family { get; set; }
            public IEnumerable<Report2VMHonor> honor { get; set; }
            public IEnumerable<Report2VMInsig> insignia { get; set; }
            public IEnumerable<Report2VMInfo> info { get; set; }
            public IEnumerable<Report2VMLicense> license { get; set; }
            public IEnumerable<Report2VMTeaching> teaching { get; set; }
            public IEnumerable<Report2VMTrain> training { get; set; }
            public IEnumerable<Report2VMSalary> salary { get; set; }
            public int id { get; internal set; }
        }

        private class EduLevel
        {
            public int LevelID { get; set; }
            public string LevelName { get; set; }
        }
    }


}