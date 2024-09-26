using MasterEntity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.AssetManagement.CsCode
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
                rng.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                rng.Style.Fill.BackgroundColor.SetColor(0, 255, 255, 0);
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

        public static ExcelPackage ExportReport01Excel(string sortIndex, string orderDir, string searchYear, string searchCategory)
        {
            ExcelPackage excel = new ExcelPackage();

            string worksheetName = "รายงานคงเหลือตามกลุ่ม";
            excel.Workbook.Worksheets.Add(worksheetName);
            var worksheet = excel.Workbook.Worksheets[worksheetName];

            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            {
                var company = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                //Header topic
                SetHeader(worksheet, "A1:H1", true, "โรงเรียน " + company.sCompany, null, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A2:H2", true, "กลุ่มงาน/สาระ ", null, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A3:H3", true, "แบบสำรวจครุภัณฑ์ ปีการศึกษา " + searchYear, null, ExcelHorizontalAlignment.Center);

                // Query Data
                List<EntityReport01> listData = QueryEngine.LoadReport01ListData(sortIndex, orderDir, searchYear, searchCategory);

                int tableRow = 4;

                //Header of table  
                //  
                int tableColumun = 1;
                string[] headerColumnName = { "ลำดับ", "รหัสประเภทครุภัณฑ์", "ประเภทครุภัณฑ์", "สินค้า", "จำนวน", "หน่วยนับ", "แผนก/หน่วยงาน", "ผู้รับผิดชอบ" };
                foreach (var h in headerColumnName)
                {
                    SetTableHeader(worksheet.Cells[tableRow, tableColumun++], false, h, ExcelHorizontalAlignment.Center);
                }

                //Body of table  
                //  
                tableRow = 5;
                int rowIndex = 1;
                foreach (var data in listData)
                {
                    SetTableRows(worksheet.Cells[tableRow, 1], false, rowIndex++ + ".", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 2], false, data.Code, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 3], false, data.Category, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 4], false, data.Product, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 5], false, data.Amount.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 6], false, data.Unit, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 7], false, data.Department, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow++, 8], false, "", ExcelHorizontalAlignment.Center);
                }

                for (int i = 1; i <= headerColumnName.Length; i++) worksheet.Column(i).AutoFit();

                return excel;
            }
        }

        public static ExcelPackage ExportReport02Excel(string sortIndex, string orderDir, string searchYear, string searchCategory)
        {
            ExcelPackage excel = new ExcelPackage();

            string worksheetName = "รายงานสรุปการเบิกใช้";
            excel.Workbook.Worksheets.Add(worksheetName);
            var worksheet = excel.Workbook.Worksheets[worksheetName];

            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            {
                var company = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                //Header topic
                SetHeader(worksheet, "A1:J1", true, "โรงเรียน " + company.sCompany, null, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A2:J2", true, "กลุ่มงาน/สาระ ", null, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A3:J3", true, "แบบสรุปการเบิกใช้ ปีการศึกษา " + searchYear, null, ExcelHorizontalAlignment.Center);

                // Query Data
                List<EntityReport02> listData = QueryEngine.LoadReport02ListData(sortIndex, orderDir, searchYear, searchCategory);

                int tableRow = 4;

                //Header of table  
                //  
                int tableColumun = 1;
                string[] headerColumnName = { "ลำดับ", "รหัสประเภทครุภัณฑ์", "ประเภทครุภัณฑ์", "สินค้า", "จำนวน", "หน่วยนับ", "แผนก/หน่วยงาน", "ผู้รับผิดชอบ", "ผู้เบิก", "เลขที่เอกสาร" };
                foreach (var h in headerColumnName)
                {
                    SetTableHeader(worksheet.Cells[tableRow, tableColumun++], false, h, ExcelHorizontalAlignment.Center);
                }

                //Body of table  
                //  
                tableRow = 5;
                int rowIndex = 1;
                foreach (var data in listData)
                {
                    SetTableRows(worksheet.Cells[tableRow, 1], false, rowIndex++ + ".", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 2], false, data.Code, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 3], false, data.Category, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 4], false, data.Product, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 5], false, data.Amount.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 6], false, data.Unit, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 7], false, data.Department, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 8], false, data.ResponsibleBy, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow, 9], false, data.Receiver, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[tableRow++, 10], false, data.DocumentNo, ExcelHorizontalAlignment.Center);
                }

                for (int i = 1; i <= headerColumnName.Length; i++) worksheet.Column(i).AutoFit();

                return excel;
            }
        }


    }
}