using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FingerprintPayment.PreRegister.CsCode
{
    public class ReportEngine
    {
        public ReportEngine() { }

        private static void SetHeader(ExcelWorksheet excelWorksheet, string Cells, bool Merge, string strValues, int? fontSize, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = excelWorksheet.Cells[Cells])
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.Font.Size = fontSize ?? 11;
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

        public static ExcelPackage ExportReportPreRegisterExcel(string sortIndex, string orderDir, int schoolID, string searchYear, string searchRegStatus, string searchOptLevel, string searchCouType, string searchCouTime, string searchBranch, string searchStdName, string searchPlan)
        {
            ExcelPackage excel = new ExcelPackage();

            string worksheetName = "ข้อมูลผู้สมัครเข้าเรียน";
            excel.Workbook.Worksheets.Add(worksheetName);
            var worksheet = excel.Workbook.Worksheets[worksheetName];


            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            {
                var company = db.TCompanies.FirstOrDefault(w => w.nCompany == schoolID);

                //Header topic
                SetHeader(worksheet, "A1:EJ1", true, "ข้อมูลผู้สมัครเข้าเรียน " + company.sCompany, null, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A2:EJ2", true, "", null, ExcelHorizontalAlignment.Center);

                // Query Data
                List<EntityReportPreRegister> listData = QueryEngine.LoadReportPreRegisterListData(sortIndex, orderDir, schoolID, searchYear, searchRegStatus, searchOptLevel, searchCouType, searchCouTime, searchBranch, searchStdName, searchPlan);

                int tableRow = 3;

                //Header of table  
                //  
                int tableColumun = 1;
                string[] headerColumnName = { "ลำดับ", "วันที่ลงทะเบียน", "ปีการศึกษา", "ประเภทนักเรียน", "สาขา", "รอบการเรียน", "หลักสูตร", "แผน", "ระดับ", "รหัสนักเรียน", "เลขที่สอบ", "ผลสอบ", "ข้อมูล/เอกสาร", "รายละเอียด", "เพศ", "คำนำหน้า", "ชื่อ", "นามสกุล", "ชื่อ (อังกฤษ)", "นามสกุล (อังกฤษ)"
                    ,"ชื่อเล่น", "ชื่อเล่น (อังกฤษ)", "รหัสประจำตัวประชาชน", "วันเกิด", "เชื้อชาติ", "สัญชาติ", "ศาสนา", "เป็นบุตรคนที่", "บ้านเลขที่", "ซอย", "หมู่", "ถนน", "จังหวัด", "อำเภอ", "ตำบล", "รหัสไปรษณีย์", "เบอร์โทรศัพท์", "อีเมล์"
                    ,"บ้านเลขที่", "ซอย", "หมู่", "ถนน", "จังหวัด", "อำเภอ", "ตำบล", "รหัสไปรษณีย์", "เบอร์โทรศัพท์"
                    ,"น้ำหนัก", "ส่วนสูง", "กรุ๊ปเลือด", "แพ้อาหาร", "แพ้ยา", "แพ้อื่นๆ", "โรคประจำตัว", "โรคร้ายแรง", "วันที่สมัคร", "วันที่ย้ายเข้า", "รหัสนักเรียน", "ย้ายไปยังห้อง", "สถานะการจ่ายเงิน", "ชื่อสถานศึกษาเดิม", "จังหวัด"
                    ,"อำเภอ", "ตำบล", "คะแนน GPA", "วุฒิการศึกษา", "คำนำหน้า (ผู้ปกครอง)", "ชื่อ (ผู้ปกครอง)", "นามสกุล (ผู้ปกครอง)", "รหัสประจำตัวประชาชน (ผู้ปกครอง)", "เชื้อชาติ (ผู้ปกครอง)", "สัญชาติ (ผู้ปกครอง)", "ศาสนา (ผู้ปกครอง)", "บ้านเลขที่ (ผู้ปกครอง)"
                    ,"ซอย (ผู้ปกครอง)", "หมู่ (ผู้ปกครอง)", "ถนน (ผู้ปกครอง)", "จังหวัด (ผู้ปกครอง)", "อำเถอ (ผู้ปกครอง)", "ตำบล (ผู้ปกครอง)", "รหัสไปรษณีย์ (ผู้ปกครอง)", "เบอร์โทรศัพท์ 1 (ผู้ปกครอง)", "เบอร์โทรศัพท์ 2 (ผู้ปกครอง)"
                    ,"เบอร์โทรศัพท์ 3 (ผู้ปกครอง)", "e-mail (ผู้ปกครอง)", "รายได้ต่อเดือน (ผู้ปกครอง)", "วุฒิการศึกษา (ผู้ปกครอง)", "สถานที่ทำงาน (ผู้ปกครอง)", "ว/ด/ป เกิด (ผู้ปกครอง)", "อายุ (ผู้ปกครอง)", "อาชีพ (ผู้ปกครอง)", "ความสัมพันธ์ (ผู้ปกครอง)", "คำนำหน้า (บิดา)", "ชื่อ (บิดา)", "นามสกุล (บิดา)", "รหัสประจำตัวประชาชน (บิดา)", "เชื้อชาติ (บิดา)", "สัญชาติ (บิดา)", "ศาสนา (บิดา)", "บ้านเลขที่ (บิดา)", "ซอย (บิดา)", "หมู่ (บิดา)", "ถนน (บิดา)"
                    ,"จังหวัด (บิดา)", "อำเภอ (บิดา)", "ตำบล (บิดา)", "รหัสไปรษณีย์ (บิดา)", "เบอร์โทรศัพท์ (บิดา)", "e-mail (บิดา)", "รายได้ต่อเดือน (บิดา)", "วุฒิการศึกษา (บิดา)", "สถานที่ทำงาน (บิดา)", "ว/ด/ป เกิด (บิดา)", "อายุ (บิดา)", "อาชีพ (บิดา)", "คำนำหน้า (มารดา)", "ชื่อ (มารดา)", "นามสกุล (มารดา)", "รหัสประจำตัวประชาชน (มารดา)", "เชื้อชาติ (มารดา)", "สัญชาติ (มารดา)", "ศาสนา (มารดา)"
                    ,"บ้านเลขที่ (มารดา)", "ซอย (มารดา)", "หมู่ (มารดา)", "ถนน (มารดา)", "จังหวัด (มารดา)", "อำเภอ (มารดา)", "ตำบล (มารดา)", "รหัสไปรษณีย์ (มารดา)", "เบอร์โทรศัพท์ (มารดา)", "e-mail (มารดา)", "รายได้ต่อเดือน (มารดา)", "วุฒิการศึกษา (มารดา)", "สถานที่ทำงาน (มารดา)", "ว/ด/ป เกิด (มารดา)", "อายุ (มารดา)", "อาชีพ (มารดา)"
                    ,"รู้จักสถานศึกษาจาก บิดา-มารดา", "รู้จักสถานศึกษาจาก ญาติ", "รู้จักสถานศึกษาจาก พี่/น้อง กำลังศึกษา", "รู้จักสถานศึกษาจาก เอกสารแนะแนว", "รู้จักสถานศึกษาจาก ป้ายโฆษณา", "รู้จักสถานศึกษาจาก ป้ายรถเมล์", "รู้จักสถานศึกษาจาก งานแนะแนว"
                    ,"รู้จักสถานศึกษาจาก มีผู้แนะนำ", "รู้จักสถานศึกษาจาก สื่อออนไลน์ (website)", "รู้จักสถานศึกษาจาก สื่อออนไลน์ (google)", "รู้จักสถานศึกษาจาก สื่อออนไลน์ (facebook)" };

                foreach (var h in headerColumnName)
                {
                    SetTableHeader(worksheet.Cells[tableRow, tableColumun++], false, h, ExcelHorizontalAlignment.Center);
                }

                //Body of table  
                //  

                List<province> listProvince = new List<province>();
                List<amphur> listAmphurs = new List<amphur>();
                List<district> listDistricts = new List<district>();

                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    listProvince = dbMaster.provinces.ToList();
                    listAmphurs = dbMaster.amphurs.ToList();
                    listDistricts = dbMaster.districts.ToList();
                }

                tableRow = 4;
                int rowIndex = 1;
                foreach (var data in listData)
                {
                    //data.StudentProvince
                    if (!string.IsNullOrEmpty(data.StudentProvince))
                    {
                        var objProvince = listProvince.Where(w => w.PROVINCE_ID.ToString() == data.StudentProvince).FirstOrDefault();
                        if (objProvince != null)
                        {
                            data.StudentProvince = objProvince.PROVINCE_NAME;
                        }
                    }
                    //data.StudentAumpher
                    if (!string.IsNullOrEmpty(data.StudentAumpher))
                    {
                        var objAumpher = listAmphurs.Where(w => w.AMPHUR_ID.ToString() == data.StudentAumpher).FirstOrDefault();
                        if (objAumpher != null)
                        {
                            data.StudentAumpher = objAumpher.AMPHUR_NAME;
                        }
                    }
                    //data.StudentTumbon
                    if (!string.IsNullOrEmpty(data.StudentTumbon))
                    {
                        var objTumbon = listDistricts.Where(w => w.DISTRICT_ID.ToString() == data.StudentTumbon).FirstOrDefault();
                        if (objTumbon != null)
                        {
                            data.StudentTumbon = objTumbon.DISTRICT_NAME;
                        }
                    }

                    //data.HouseRegistrationProvince
                    if (!string.IsNullOrEmpty(data.HouseRegistrationProvince))
                    {
                        var objProvince = listProvince.Where(w => w.PROVINCE_ID.ToString() == data.HouseRegistrationProvince).FirstOrDefault();
                        if (objProvince != null)
                        {
                            data.HouseRegistrationProvince = objProvince.PROVINCE_NAME;
                        }
                    }
                    //data.HouseRegistrationAumpher
                    if (!string.IsNullOrEmpty(data.HouseRegistrationAumpher))
                    {
                        var objAumpher = listAmphurs.Where(w => w.AMPHUR_ID.ToString() == data.HouseRegistrationAumpher).FirstOrDefault();
                        if (objAumpher != null)
                        {
                            data.HouseRegistrationAumpher = objAumpher.AMPHUR_NAME;
                        }
                    }
                    //data.HouseRegistrationTumbon
                    if (!string.IsNullOrEmpty(data.HouseRegistrationTumbon))
                    {
                        var objTumbon = listDistricts.Where(w => w.DISTRICT_ID.ToString() == data.HouseRegistrationTumbon).FirstOrDefault();
                        if (objTumbon != null)
                        {
                            data.HouseRegistrationTumbon = objTumbon.DISTRICT_NAME;
                        }
                    }

                    //data.OldSchoolProvince
                    if (!string.IsNullOrEmpty(data.OldSchoolProvince))
                    {
                        var objProvince = listProvince.Where(w => w.PROVINCE_ID.ToString() == data.OldSchoolProvince).FirstOrDefault();
                        if (objProvince != null)
                        {
                            data.OldSchoolProvince = objProvince.PROVINCE_NAME;
                        }
                    }
                    //data.OldSchoolAumpher
                    if (!string.IsNullOrEmpty(data.OldSchoolAumpher))
                    {
                        var objAumpher = listAmphurs.Where(w => w.AMPHUR_ID.ToString() == data.OldSchoolAumpher).FirstOrDefault();
                        if (objAumpher != null)
                        {
                            data.OldSchoolAumpher = objAumpher.AMPHUR_NAME;
                        }
                    }
                    //data.OldSchoolTumbon
                    if (!string.IsNullOrEmpty(data.OldSchoolTumbon))
                    {
                        var objTumbon = listDistricts.Where(w => w.DISTRICT_ID.ToString() == data.OldSchoolTumbon).FirstOrDefault();
                        if (objTumbon != null)
                        {
                            data.OldSchoolTumbon = objTumbon.DISTRICT_NAME;
                        }
                    }

                    //data.FamilyProvince
                    if (!string.IsNullOrEmpty(data.FamilyProvince))
                    {
                        var objProvince = listProvince.Where(w => w.PROVINCE_ID.ToString() == data.FamilyProvince).FirstOrDefault();
                        if (objProvince != null)
                        {
                            data.FamilyProvince = objProvince.PROVINCE_NAME;
                        }
                    }
                    //data.FamilyAumpher
                    if (!string.IsNullOrEmpty(data.FamilyAumpher))
                    {
                        var objAumpher = listAmphurs.Where(w => w.AMPHUR_ID.ToString() == data.FamilyAumpher).FirstOrDefault();
                        if (objAumpher != null)
                        {
                            data.FamilyAumpher = objAumpher.AMPHUR_NAME;
                        }
                    }
                    //data.FamilyTumbon
                    if (!string.IsNullOrEmpty(data.FamilyTumbon))
                    {
                        var objTumbon = listDistricts.Where(w => w.DISTRICT_ID.ToString() == data.FamilyTumbon).FirstOrDefault();
                        if (objTumbon != null)
                        {
                            data.FamilyTumbon = objTumbon.DISTRICT_NAME;
                        }
                    }

                    //data.FatherProvince
                    if (!string.IsNullOrEmpty(data.FatherProvince))
                    {
                        var objProvince = listProvince.Where(w => w.PROVINCE_ID.ToString() == data.FatherProvince).FirstOrDefault();
                        if (objProvince != null)
                        {
                            data.FatherProvince = objProvince.PROVINCE_NAME;
                        }
                    }
                    //data.FatherAumpher
                    if (!string.IsNullOrEmpty(data.FatherAumpher))
                    {
                        var objAumpher = listAmphurs.Where(w => w.AMPHUR_ID.ToString() == data.FatherAumpher).FirstOrDefault();
                        if (objAumpher != null)
                        {
                            data.FatherAumpher = objAumpher.AMPHUR_NAME;
                        }
                    }
                    //data.FatherTumbon
                    if (!string.IsNullOrEmpty(data.FatherTumbon))
                    {
                        var objTumbon = listDistricts.Where(w => w.DISTRICT_ID.ToString() == data.FatherTumbon).FirstOrDefault();
                        if (objTumbon != null)
                        {
                            data.FatherTumbon = objTumbon.DISTRICT_NAME;
                        }
                    }

                    //data.MotherProvince
                    if (!string.IsNullOrEmpty(data.MotherProvince))
                    {
                        var objProvince = listProvince.Where(w => w.PROVINCE_ID.ToString() == data.MotherProvince).FirstOrDefault();
                        if (objProvince != null)
                        {
                            data.MotherProvince = objProvince.PROVINCE_NAME;
                        }
                    }
                    //data.MotherAumpher
                    if (!string.IsNullOrEmpty(data.MotherAumpher))
                    {
                        var objAumpher = listAmphurs.Where(w => w.AMPHUR_ID.ToString() == data.MotherAumpher).FirstOrDefault();
                        if (objAumpher != null)
                        {
                            data.MotherAumpher = objAumpher.AMPHUR_NAME;
                        }
                    }
                    //data.MotherTumbon
                    if (!string.IsNullOrEmpty(data.MotherTumbon))
                    {
                        var objTumbon = listDistricts.Where(w => w.DISTRICT_ID.ToString() == data.MotherTumbon).FirstOrDefault();
                        if (objTumbon != null)
                        {
                            data.MotherTumbon = objTumbon.DISTRICT_NAME;
                        }
                    }

                    int colIndex = 1;
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, rowIndex++ + ".", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.RegisterDate == null ? "" : data.RegisterDate.Value.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Year.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentCategory, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.BranchName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Time, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Course, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.PlanName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SubLevel, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentID, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.ExamCode, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.ExamResults, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.CompleteDocuments, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.CompleteDocumentsInfo, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Sex, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentTitle, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentFirstName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentLastName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentFirstNameEn, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentLastNameEn, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.NickName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.NickNameEn, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentIdentityCard, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentBirth == null ? "" : data.StudentBirth.Value.ToString("d MMM yyyy", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentRace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentNation, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentReligion, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SonNumber == null ? "0" : data.SonNumber.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentHomeNumber, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentSoy, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentMuu, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentRoad, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentProvince, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentAumpher, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentTumbon, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentPost, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Phone, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Email, ExcelHorizontalAlignment.Center);

                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.HouseRegistrationHomeNumber, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.HouseRegistrationSoy, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.HouseRegistrationMuu, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.HouseRegistrationRoad, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.HouseRegistrationProvince, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.HouseRegistrationAumpher, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.HouseRegistrationTumbon, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.HouseRegistrationPost, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.HouseRegistrationPhone, ExcelHorizontalAlignment.Center);

                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Weight == null ? "" : data.Weight.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Height == null ? "" : data.Height.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Blood, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SickFood, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SickDrug, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SickOther, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SickNormal, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SickDanger, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.AddDate == null ? "" : data.AddDate.Value.ToString("d MMM yyyy", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MoveInDate == null ? "" : data.MoveInDate.Value.ToString("d MMM yyyy", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SaveAsSID == null ? "" : data.SaveAsSID.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.ClassRoom, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.PaymentStatus, ExcelHorizontalAlignment.Center);

                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.OldSchoolName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.OldSchoolProvince, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.OldSchoolAumpher, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.OldSchoolTumbon, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.OldSchoolGPA == null ? "" : data.OldSchoolGPA.Value.ToString("0.00"), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.OldSchoolGraduated, ExcelHorizontalAlignment.Center);

                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyTitle, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyFirstName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyLastName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyIdentityCard, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyRace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyNation, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyReligion, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyHomeNumber, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilySoy, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyMuu, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyRoad, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyProvince, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyAumpher, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyTumbon, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyPost, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.PhoneOne, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.PhoneTwo, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.PhoneThree, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.ParentEmail, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyIncome?.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyGraduated, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyWorkPlace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyBirthDay == null ? "" : data.FamilyBirthDay.Value.ToString("d MMM yyyy", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyAge == null ? "" : data.FamilyAge.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyJob, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyRelation, ExcelHorizontalAlignment.Center);

                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherTitle, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherFirstName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherLastName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherIdentityCard, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherRace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherNation, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherReligion, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherHomeNumber, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherSoy, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherMuu, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherRoad, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherProvince, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherAumpher, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherTumbon, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherPost, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherPhone, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherEmail, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherIncome?.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherGraduated, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherWorkPlace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherBirthDay == null ? "" : data.FatherBirthDay.Value.ToString("d MMM yyyy", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherAge == null ? "" : data.FatherAge.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherJob, ExcelHorizontalAlignment.Center);

                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherTitle, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherFirstName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherLastName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherIdentityCard, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherRace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherNation, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherReligion, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherHomeNumber, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherSoy, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherMuu, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherRoad, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherProvince, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherAumpher, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherTumbon, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherPost, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherPhone, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherEmail, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherIncome?.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherGraduated, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherWorkPlace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherBirthDay == null ? "" : data.MotherBirthDay.Value.ToString("d MMM yyyy", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherAge == null ? "" : data.MotherAge.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherJob, ExcelHorizontalAlignment.Center);

                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom1, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom2, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom3, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom4, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom5, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom6, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom7, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom8, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom9, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom10, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow++, colIndex++], false, data.KnowFrom11, ExcelHorizontalAlignment.Center);
                }

                for (int i = 1; i <= headerColumnName.Length; i++) worksheet.Column(i).AutoFit();

                return excel;
            }
        }

        public static ExcelPackage ExportReportPreRegisterBackupPlansExcel(string sortIndex, string orderDir, int schoolID, string searchYear, string searchRegStatus, string searchOptLevel, string searchCouType, string searchCouTime, string searchBranch, string searchStdName, string searchPlan)
        {
            ExcelPackage excel = new ExcelPackage();

            string worksheetName = "ข้อมูลผู้สมัครเข้าเรียน";
            excel.Workbook.Worksheets.Add(worksheetName);
            var worksheet = excel.Workbook.Worksheets[worksheetName];



            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var company = dbMaster.TCompanies.FirstOrDefault(w => w.nCompany == schoolID);

                // Query Data
                List<EntityReportPreRegister> listData = QueryEngine.LoadReportPreRegisterListData(sortIndex, orderDir, schoolID, searchYear, searchRegStatus, searchOptLevel, searchCouType, searchCouTime, searchBranch, searchStdName, searchPlan);

                // Calculate column excel
                int alphaIndex = 8;
                char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

                // Find new alpha index
                int? maxOrderPlans = listData.Max(m => m.OrderPlans);
                maxOrderPlans = (int)(maxOrderPlans == null ? 0 : maxOrderPlans);
                alphaIndex = alphaIndex + (int)maxOrderPlans;

                // Header topic
                SetHeader(worksheet, "A1:E" + alpha[alphaIndex] + "1", true, "ข้อมูลผู้สมัครเข้าเรียน " + company.sCompany, null, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A2:E" + alpha[alphaIndex] + "2", true, "", null, ExcelHorizontalAlignment.Center);

                int tableRow = 3;

                //Header of table  
                //  
                int tableColumun = 1;
                List<string> headerColumnName = new List<string> { "ลำดับ", "ปีการศึกษา", "ประเภทนักเรียน", "สาขา", "รอบการเรียน", "หลักสูตร", "แผน", "ระดับ", "รหัสนักเรียน", "เลขที่สอบ", "ผลสอบ", "ข้อมูล/เอกสาร", "รายละเอียด", "เพศ", "คำนำหน้า", "ชื่อ", "นามสกุล", "ชื่อ (อังกฤษ)", "นามสกุล (อังกฤษ)"
                ,"ชื่อเล่น", "ชื่อเล่น (อังกฤษ)", "รหัสประจำตัวประชาชน", "วันเกิด", "เชื้อชาติ", "สัญชาติ", "ศาสนา", "เป็นบุตรคนที่", "บ้านเลขที่", "ซอย", "หมู่", "ถนน", "จังหวัด", "อำเภอ", "ตำบล", "รหัสไปรษณีย์", "เบอร์โทรศัพท์", "อีเมล์"
                ,"น้ำหนัก", "ส่วนสูง", "กรุ๊ปเลือด", "แพ้อาหาร", "แพ้ยา", "แพ้อื่นๆ", "โรคประจำตัว", "โรคร้ายแรง", "วันที่สมัคร", "วันที่ย้ายเข้า", "รหัสนักเรียน", "ย้ายไปยังห้อง", "สถานะการจ่ายเงิน", "ชื่อสถานศึกษาเดิม", "จังหวัด"
                ,"อำเภอ", "ตำบล", "คะแนน GPA", "วุฒิการศึกษา", "คำนำหน้า (ผู้ปกครอง)", "ชื่อ (ผู้ปกครอง)", "นามสกุล (ผู้ปกครอง)", "รหัสประจำตัวประชาชน (ผู้ปกครอง)", "เชื้อชาติ (ผู้ปกครอง)", "สัญชาติ (ผู้ปกครอง)", "ศาสนา (ผู้ปกครอง)", "บ้านเลขที่ (ผู้ปกครอง)"
                ,"ซอย (ผู้ปกครอง)", "หมู่ (ผู้ปกครอง)", "ถนน (ผู้ปกครอง)", "จังหวัด (ผู้ปกครอง)", "อำเถอ (ผู้ปกครอง)", "ตำบล (ผู้ปกครอง)", "รหัสไปรษณีย์ (ผู้ปกครอง)", "เบอร์โทรศัพท์ 1 (ผู้ปกครอง)", "เบอร์โทรศัพท์ 2 (ผู้ปกครอง)"
                ,"เบอร์โทรศัพท์ 3 (ผู้ปกครอง)", "e-mail (ผู้ปกครอง)", "รายได้ต่อเดือน (ผู้ปกครอง)", "วุฒิการศึกษา (ผู้ปกครอง)", "สถานที่ทำงาน (ผู้ปกครอง)", "ว/ด/ป เกิด (ผู้ปกครอง)", "อายุ (ผู้ปกครอง)", "อาชีพ (ผู้ปกครอง)", "ความสัมพันธ์ (ผู้ปกครอง)", "คำนำหน้า (บิดา)", "ชื่อ (บิดา)", "นามสกุล (บิดา)", "รหัสประจำตัวประชาชน (บิดา)", "เชื้อชาติ (บิดา)", "สัญชาติ (บิดา)", "ศาสนา (บิดา)", "บ้านเลขที่ (บิดา)", "ซอย (บิดา)", "หมู่ (บิดา)", "ถนน (บิดา)"
                ,"จังหวัด (บิดา)", "อำเภอ (บิดา)", "ตำบล (บิดา)", "รหัสไปรษณีย์ (บิดา)", "เบอร์โทรศัพท์ (บิดา)", "e-mail (บิดา)", "รายได้ต่อเดือน (บิดา)", "วุฒิการศึกษา (บิดา)", "สถานที่ทำงาน (บิดา)", "ว/ด/ป เกิด (บิดา)", "อายุ (บิดา)", "อาชีพ (บิดา)", "คำนำหน้า (มารดา)", "ชื่อ (มารดา)", "นามสกุล (มารดา)", "รหัสประจำตัวประชาชน (มารดา)", "เชื้อชาติ (มารดา)", "สัญชาติ (มารดา)", "ศาสนา (มารดา)"
                ,"บ้านเลขที่ (มารดา)", "ซอย (มารดา)", "หมู่ (มารดา)", "ถนน (มารดา)", "จังหวัด (มารดา)", "อำเภอ (มารดา)", "ตำบล (มารดา)", "รหัสไปรษณีย์ (มารดา)", "เบอร์โทรศัพท์ (มารดา)", "e-mail (มารดา)", "รายได้ต่อเดือน (มารดา)", "วุฒิการศึกษา (มารดา)", "สถานที่ทำงาน (มารดา)", "ว/ด/ป เกิด (มารดา)", "อายุ (มารดา)", "อาชีพ (มารดา)"
                ,"รู้จักสถานศึกษาจาก บิดา-มารดา", "รู้จักสถานศึกษาจาก ญาติ", "รู้จักสถานศึกษาจาก พี่/น้อง กำลังศึกษา", "รู้จักสถานศึกษาจาก เอกสารแนะแนว", "รู้จักสถานศึกษาจาก ป้ายโฆษณา", "รู้จักสถานศึกษาจาก ป้ายรถเมล์", "รู้จักสถานศึกษาจาก งานแนะแนว"
                ,"รู้จักสถานศึกษาจาก มีผู้แนะนำ", "รู้จักสถานศึกษาจาก สื่อออนไลน์ (website)", "รู้จักสถานศึกษาจาก สื่อออนไลน์ (google)", "รู้จักสถานศึกษาจาก สื่อออนไลน์ (facebook)" };

                // Insert max backup plans to headerColumnName
                // Insert backup plans column - insert at position 9
                for (int i = (int)maxOrderPlans; i > 0; i--)
                {
                    if (i == 1)
                    {
                        headerColumnName.Insert(10, "แผนหลัก");
                    }
                    else
                    {
                        headerColumnName.Insert(10, "แผนสำรอง " + (i - 1));
                    }
                }

                foreach (var h in headerColumnName)
                {
                    SetTableHeader(worksheet.Cells[tableRow, tableColumun++], false, h, ExcelHorizontalAlignment.Center);
                }

                //Body of table  
                //  


                List<province> listProvince = dbMaster.provinces.ToList();
                List<amphur> listAmphurs = dbMaster.amphurs.ToList();
                List<district> listDistricts = dbMaster.districts.ToList();

                tableRow = 4;
                int rowIndex = 1;
                foreach (var data in listData)
                {
                    //data.StudentProvince
                    if (!string.IsNullOrEmpty(data.StudentProvince))
                    {
                        var objProvince = listProvince.Where(w => w.PROVINCE_ID.ToString() == data.StudentProvince).FirstOrDefault();
                        if (objProvince != null)
                        {
                            data.StudentProvince = objProvince.PROVINCE_NAME;
                        }
                    }
                    //data.StudentAumpher
                    if (!string.IsNullOrEmpty(data.StudentAumpher))
                    {
                        var objAumpher = listAmphurs.Where(w => w.AMPHUR_ID.ToString() == data.StudentAumpher).FirstOrDefault();
                        if (objAumpher != null)
                        {
                            data.StudentAumpher = objAumpher.AMPHUR_NAME;
                        }
                    }
                    //data.StudentTumbon
                    if (!string.IsNullOrEmpty(data.StudentTumbon))
                    {
                        var objTumbon = listDistricts.Where(w => w.DISTRICT_ID.ToString() == data.StudentTumbon).FirstOrDefault();
                        if (objTumbon != null)
                        {
                            data.StudentTumbon = objTumbon.DISTRICT_NAME;
                        }
                    }

                    //data.OldSchoolProvince
                    if (!string.IsNullOrEmpty(data.OldSchoolProvince))
                    {
                        var objProvince = listProvince.Where(w => w.PROVINCE_ID.ToString() == data.OldSchoolProvince).FirstOrDefault();
                        if (objProvince != null)
                        {
                            data.OldSchoolProvince = objProvince.PROVINCE_NAME;
                        }
                    }
                    //data.OldSchoolAumpher
                    if (!string.IsNullOrEmpty(data.OldSchoolAumpher))
                    {
                        var objAumpher = listAmphurs.Where(w => w.AMPHUR_ID.ToString() == data.OldSchoolAumpher).FirstOrDefault();
                        if (objAumpher != null)
                        {
                            data.OldSchoolAumpher = objAumpher.AMPHUR_NAME;
                        }
                    }
                    //data.OldSchoolTumbon
                    if (!string.IsNullOrEmpty(data.OldSchoolTumbon))
                    {
                        var objTumbon = listDistricts.Where(w => w.DISTRICT_ID.ToString() == data.OldSchoolTumbon).FirstOrDefault();
                        if (objTumbon != null)
                        {
                            data.OldSchoolTumbon = objTumbon.DISTRICT_NAME;
                        }
                    }

                    //data.FamilyProvince
                    if (!string.IsNullOrEmpty(data.FamilyProvince))
                    {
                        var objProvince = listProvince.Where(w => w.PROVINCE_ID.ToString() == data.FamilyProvince).FirstOrDefault();
                        if (objProvince != null)
                        {
                            data.FamilyProvince = objProvince.PROVINCE_NAME;
                        }
                    }
                    //data.FamilyAumpher
                    if (!string.IsNullOrEmpty(data.FamilyAumpher))
                    {
                        var objAumpher = listAmphurs.Where(w => w.AMPHUR_ID.ToString() == data.FamilyAumpher).FirstOrDefault();
                        if (objAumpher != null)
                        {
                            data.FamilyAumpher = objAumpher.AMPHUR_NAME;
                        }
                    }
                    //data.FamilyTumbon
                    if (!string.IsNullOrEmpty(data.FamilyTumbon))
                    {
                        var objTumbon = listDistricts.Where(w => w.DISTRICT_ID.ToString() == data.FamilyTumbon).FirstOrDefault();
                        if (objTumbon != null)
                        {
                            data.FamilyTumbon = objTumbon.DISTRICT_NAME;
                        }
                    }

                    //data.FatherProvince
                    if (!string.IsNullOrEmpty(data.FatherProvince))
                    {
                        var objProvince = listProvince.Where(w => w.PROVINCE_ID.ToString() == data.FatherProvince).FirstOrDefault();
                        if (objProvince != null)
                        {
                            data.FatherProvince = objProvince.PROVINCE_NAME;
                        }
                    }
                    //data.FatherAumpher
                    if (!string.IsNullOrEmpty(data.FatherAumpher))
                    {
                        var objAumpher = listAmphurs.Where(w => w.AMPHUR_ID.ToString() == data.FatherAumpher).FirstOrDefault();
                        if (objAumpher != null)
                        {
                            data.FatherAumpher = objAumpher.AMPHUR_NAME;
                        }
                    }
                    //data.FatherTumbon
                    if (!string.IsNullOrEmpty(data.FatherTumbon))
                    {
                        var objTumbon = listDistricts.Where(w => w.DISTRICT_ID.ToString() == data.FatherTumbon).FirstOrDefault();
                        if (objTumbon != null)
                        {
                            data.FatherTumbon = objTumbon.DISTRICT_NAME;
                        }
                    }

                    //data.MotherProvince
                    if (!string.IsNullOrEmpty(data.MotherProvince))
                    {
                        var objProvince = listProvince.Where(w => w.PROVINCE_ID.ToString() == data.MotherProvince).FirstOrDefault();
                        if (objProvince != null)
                        {
                            data.MotherProvince = objProvince.PROVINCE_NAME;
                        }
                    }
                    //data.MotherAumpher
                    if (!string.IsNullOrEmpty(data.MotherAumpher))
                    {
                        var objAumpher = listAmphurs.Where(w => w.AMPHUR_ID.ToString() == data.MotherAumpher).FirstOrDefault();
                        if (objAumpher != null)
                        {
                            data.MotherAumpher = objAumpher.AMPHUR_NAME;
                        }
                    }
                    //data.MotherTumbon
                    if (!string.IsNullOrEmpty(data.MotherTumbon))
                    {
                        var objTumbon = listDistricts.Where(w => w.DISTRICT_ID.ToString() == data.MotherTumbon).FirstOrDefault();
                        if (objTumbon != null)
                        {
                            data.MotherTumbon = objTumbon.DISTRICT_NAME;
                        }
                    }

                    int colIndex = 1;
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, rowIndex++ + ".", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Year.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentCategory, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.BranchName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Time, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Course, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.PlanName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SubLevel, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentID, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.ExamCode, ExcelHorizontalAlignment.Center);

                    // Insert backup plans
                    if (string.IsNullOrEmpty(data.BackupPlans))
                    {
                        for (int i = 0; i < maxOrderPlans; i++)
                        {
                            SetTableRows(worksheet.Cells[tableRow, colIndex++], false, "", ExcelHorizontalAlignment.Center);
                        }
                    }
                    else
                    {
                        List<RegisterOnline03.BackupPlans> backupPlans = JsonConvert.DeserializeObject<List<RegisterOnline03.BackupPlans>>(data.BackupPlans);
                        int missingColumn = (int)maxOrderPlans - backupPlans.Count;

                        foreach (var b in backupPlans)
                        {
                            SetTableRows(worksheet.Cells[tableRow, colIndex++], false, b.PlanName, ExcelHorizontalAlignment.Center);
                        }

                        for (int i = 0; i < missingColumn; i++)
                        {
                            SetTableRows(worksheet.Cells[tableRow, colIndex++], false, "", ExcelHorizontalAlignment.Center);
                        }
                    }

                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.ExamResults, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.CompleteDocuments, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.CompleteDocumentsInfo, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Sex, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentTitle, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentFirstName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentLastName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentFirstNameEn, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentLastNameEn, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.NickName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.NickNameEn, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentIdentityCard, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentBirth == null ? "" : data.StudentBirth.Value.ToString("d MMM yyyy", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentRace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentNation, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentReligion, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SonNumber == null ? "0" : data.SonNumber.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentHomeNumber, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentSoy, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentMuu, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentRoad, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentProvince, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentAumpher, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentTumbon, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.StudentPost, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Phone, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Email, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Weight == null ? "" : data.Weight.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Height == null ? "" : data.Height.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.Blood, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SickFood, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SickDrug, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SickOther, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SickNormal, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SickDanger, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.AddDate == null ? "" : data.AddDate.Value.ToString("d MMM yyyy", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MoveInDate == null ? "" : data.MoveInDate.Value.ToString("d MMM yyyy", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SaveAsSID == null ? "" : data.SaveAsSID.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.ClassRoom, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.PaymentStatus, ExcelHorizontalAlignment.Center);

                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.OldSchoolName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.OldSchoolProvince, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.OldSchoolAumpher, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.OldSchoolTumbon, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.OldSchoolGPA == null ? "" : data.OldSchoolGPA.Value.ToString("0.00"), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.OldSchoolGraduated, ExcelHorizontalAlignment.Center);

                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyTitle, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyFirstName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyLastName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyIdentityCard, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyRace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyNation, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyReligion, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyHomeNumber, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilySoy, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyMuu, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyRoad, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyProvince, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyAumpher, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyTumbon, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyPost, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.PhoneOne, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.PhoneTwo, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.PhoneThree, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.ParentEmail, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyIncome?.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyGraduated, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyWorkPlace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyBirthDay == null ? "" : data.FamilyBirthDay.Value.ToString("d MMM yyyy", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyAge == null ? "" : data.FamilyAge.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyJob, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FamilyRelation, ExcelHorizontalAlignment.Center);

                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherTitle, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherFirstName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherLastName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherIdentityCard, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherRace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherNation, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherReligion, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherHomeNumber, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherSoy, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherMuu, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherRoad, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherProvince, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherAumpher, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherTumbon, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherPost, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherPhone, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherEmail, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherIncome?.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherGraduated, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherWorkPlace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherBirthDay == null ? "" : data.FatherBirthDay.Value.ToString("d MMM yyyy", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherAge == null ? "" : data.FatherAge.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.FatherJob, ExcelHorizontalAlignment.Center);

                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherTitle, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherFirstName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherLastName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherIdentityCard, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherRace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherNation, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherReligion, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherHomeNumber, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherSoy, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherMuu, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherRoad, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherProvince, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherAumpher, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherTumbon, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherPost, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherPhone, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherEmail, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherIncome?.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherGraduated, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherWorkPlace, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherBirthDay == null ? "" : data.MotherBirthDay.Value.ToString("d MMM yyyy", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherAge == null ? "" : data.MotherAge.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.MotherJob, ExcelHorizontalAlignment.Center);

                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom1, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom2, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom3, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom4, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom5, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom6, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom7, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom8, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom9, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.KnowFrom10, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow++, colIndex++], false, data.KnowFrom11, ExcelHorizontalAlignment.Center);
                }

                for (int i = 1; i <= (headerColumnName.Count + maxOrderPlans); i++) worksheet.Column(i).AutoFit();

                return excel;
            }
        }

        public static ExcelPackage ExportPreRegisterExcelStudentAmount(int schoolID, string searchYear, string searchRegStatus, string searchOptLevel, string searchCouType, string searchCouTime, string searchBranch, string searchStdName, string searchPlan)
        {
            ExcelPackage excel = new ExcelPackage();

            string worksheetName = "จำนวนผู้สมัครเข้าเรียน";
            excel.Workbook.Worksheets.Add(worksheetName);
            var worksheet = excel.Workbook.Worksheets[worksheetName];

            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            {
                var company = db.TCompanies.FirstOrDefault(w => w.nCompany == schoolID);

                //Header topic
                SetHeader(worksheet, "A1:F1", true, "สรุปจำนวนนักเรียนสมัครเรียนออนไลน์", null, ExcelHorizontalAlignment.Center);
                //SetHeader(worksheet, "A2:F1", true, "", null, ExcelHorizontalAlignment.Center);

                // Query Data
                List<EntityReportPreRegisterStudentAmount> listData = QueryEngine.LoadReportPreRegisterStudentAmount(schoolID, searchYear, searchRegStatus, searchOptLevel, searchCouType, searchCouTime, searchBranch, searchStdName, searchPlan);

                int tableRow = 2;

                //Header of table  
                //  
                int tableColumun = 1;
                string[] headerColumnName = { "วันที่สมัคร", "ระดับชั้น", "แผน", "ชาย", "หญิง", "รวม" };

                foreach (var h in headerColumnName)
                {
                    SetTableHeader(worksheet.Cells[tableRow, tableColumun++], false, h, ExcelHorizontalAlignment.Center);
                }

                //Body of table  
                //  

                int totalSumMale = 0, totalSumFemale = 0, totalCountAll = 0;

                tableRow = 3;
                int colIndex = 1;
                foreach (var data in listData)
                {
                    colIndex = 1;
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.RegisterDate == null ? "" : data.RegisterDate.ToString("d MMM yyyy", new CultureInfo("th-TH")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.LevelName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.PlanName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SumMale.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, colIndex++], false, data.SumFemale.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow++, colIndex++], false, data.CountAll.ToString(), ExcelHorizontalAlignment.Center);

                    totalSumMale += data.SumMale;
                    totalSumFemale += data.SumFemale;
                    totalCountAll += data.CountAll;
                }

                //Summary
                colIndex = 4;
                SetTableRows(worksheet.Cells[string.Format(@"A{0}:C{0}", tableRow)], true, "รวม", ExcelHorizontalAlignment.Center);
                SetTableRows(worksheet.Cells[tableRow, colIndex++], false, totalSumMale.ToString(), ExcelHorizontalAlignment.Center);
                SetTableRows(worksheet.Cells[tableRow, colIndex++], false, totalSumFemale.ToString(), ExcelHorizontalAlignment.Center);
                SetTableRows(worksheet.Cells[tableRow++, colIndex++], false, totalCountAll.ToString(), ExcelHorizontalAlignment.Center);

                for (int i = 1; i <= headerColumnName.Length; i++) worksheet.Column(i).AutoFit();

                return excel;
            }
        }

    }
}