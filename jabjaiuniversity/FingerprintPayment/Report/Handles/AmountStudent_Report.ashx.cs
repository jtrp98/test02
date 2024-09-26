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
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;
using static FingerprintPayment.Report.Handles.ReportStudent_Handler;
using System.Data.Entity;

namespace FingerprintPayment.Report.Handles
{
    /// <summary>
    /// Summary description for Reports03_exportHandler
    /// </summary>
    public class AmountStudent_Report : IHttpHandler, IRequiresSessionState
    {
        JWTToken.userData userData;
        TCompany school;

        List<ModelData1> dataList;
        int? year;
        public void ProcessRequest(HttpContext context)
        {

            year = ToNullableInt(context.Request["year"] + "");
            string rtype = (context.Request["rtype"] + "");
            var term = (context.Request["term"] + "");
            List<int> lvl1 = (context.Request["lvl1"] + "")?.Split(',')
                .Where( o => !string.IsNullOrEmpty(o))
                .Select(o => int.Parse(o))
                .ToList();
            var type = ToNullableInt(context.Request["type"] + "");

            //var name = context.Request["name"] + "";
            //var _cols = (context.Request["cols"] + "");
            //var cols = new List<string>();
            //if (!string.IsNullOrEmpty(_cols))
            //{
            //    cols = _cols.Split(',').ToList();
            //}

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
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                school = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
            }

            using (var masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {

                var tTuser = db.Database.SqlQuery<JabjaiEntity.DB.TUser>($"SELECT * FROM TUser WHERE SchoolID = {userData.CompanyID} AND TUser.cDel IS NULL").ToList();

                var q_title = db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                var nation = db.TMasterDatas.Where(w => w.MasterType == "3").ToList();
                var religion = db.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                var race = db.TMasterDatas.Where(w => w.MasterType == "9").ToList();

                var qryTSL = db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID);

                if (lvl1.Count > 0)
                {
                    qryTSL = qryTSL.Where(o => lvl1.Contains(o.nTSubLevel));
                }
                var thisDay = DateTime.Today;

                var tterm = db.TTerms.Where(o => o.SchoolID == userData.CompanyID && o.nTerm == term).FirstOrDefault();

                var qrySV = db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == term && w.cDel != "1");

                if (thisDay >= tterm.dStart && thisDay <= tterm.dEnd)
                {
                    qrySV = qrySV.Where(w => (
                                       (((w.nStudentStatus ?? 0) == 0 || w.nStudentStatus == 4) && DbFunctions.TruncateTime(w.moveInDate ?? thisDay) <= thisDay)
                                        || (w.nStudentStatus == 2 && thisDay < DbFunctions.TruncateTime(w.MoveOutDate ?? thisDay))
                                        || (w.nStudentStatus == 1 && thisDay < DbFunctions.TruncateTime(w.MoveOutDate ?? thisDay))
                                        ));
                }
                else
                {
                    qrySV = qrySV.Where(w => ((w.nStudentStatus ?? 0) == 0 || w.nStudentStatus == 4));
                }

                var sHistory = (from a in qrySV

                                from e1 in db.TClassMembers
                                .Where(o => o.SchoolID == userData.CompanyID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2).DefaultIfEmpty()

                                from e2 in db.TEmployees
                                .Where(o => o.SchoolID == userData.CompanyID && o.sEmp == e1.nTeacherHeadid).DefaultIfEmpty()

                                select new
                                {
                                    a.sID,
                                    a.nTermSubLevel2,
                                    a.nTerm,
                                    teacher = e2.sName + " " + e2.sLastname,
                                }).ToList();

                var ListData = (from a in tTuser
                                    //join b in db.TStudentClassroomHistories
                                    //    .Where(w => w.SchoolID == userData.CompanyID && w.cDel == false && (w.nStudentStatus ?? 0) == 0 && w.nTerm == term) on a.sID equals b.sID
                                join b in sHistory on a.sID equals b.sID

                                join c in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nWorkingStatus == 1) on b.nTermSubLevel2 equals c.nTermSubLevel2
                                join d in qryTSL on c.nTSubLevel equals d.nTSubLevel
                                join g in db.TTerms.Where(w => w.SchoolID == userData.CompanyID) on b.nTerm.Trim() equals g.nTerm.Trim()
                                join h in db.TLevels.Where(w => w.SchoolID == userData.CompanyID) on d.nTLevel equals h.LevelID

                                join tP in masterEntities.provinces on a.sStudentProvince equals tP.PROVINCE_ID.ToString() into JAP
                                from JtP in JAP.DefaultIfEmpty()
                                join tA in masterEntities.amphurs on a.sStudentAumpher equals tA.AMPHUR_ID.ToString() into JAA
                                from JtA in JAA.DefaultIfEmpty()
                                join tD in masterEntities.districts on a.sStudentTumbon equals tD.DISTRICT_ID.ToString() into JAD
                                from JtD in JAD.DefaultIfEmpty()

                                join tF in db.TFamilyProfiles.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals tF.sID into JAF
                                from JtF in JAF.DefaultIfEmpty()

                                where g.nTerm == term

                                select new
                                {
                                    aID = a.sID,
                                    aName = a.sName,
                                    aLasname = a.sLastname,
                                    teacher = b.teacher,
                                    aSex = a.cSex,
                                    cDateMoveIn = a.moveInDate.HasValue ? a.moveInDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : "ไม่พบข้อมูล",
                                    cBirthDay = a.dBirth == null ? DateTime.Now : a.dBirth,
                                    cReligion = a.sStudentReligion,
                                    cRace = a.sStudentRace,
                                    cChildren = a.nSonNumber.ToString() == null ? "" : a.nSonNumber.ToString(),

                                    groupclassid = h.LevelID,
                                    groupsupername = h.LevelName,
                                    groupsortvalue = h.sortValue,

                                    classid = d.nTSubLevel,
                                    classname = d.SubLevel,

                                    roomid = c.nTermSubLevel2,
                                    roomname = c.nTSubLevel2,

                                    cProvinceID = JtP == null ? "9999" : JtP.PROVINCE_ID.ToString(),
                                    cProvinceNAME = JtP == null ? "ไม่ระบุข้อมูล" : JtP.PROVINCE_NAME.Trim(),
                                    cAmphurID = JtA == null ? "99999" : JtA.AMPHUR_ID.ToString(),
                                    cAmphurNAME = JtA == null ? "ไม่ระบุข้อมูล" : JtA.AMPHUR_NAME.Trim(),
                                    cDistrictID = JtD == null ? "999999" : JtD.DISTRICT_ID.ToString(),
                                    cDistrictNAME = JtD == null ? "ไม่ระบุข้อมูล" : JtD.DISTRICT_NAME.Trim(),

                                    cFamilyJob = JtF == null ? "" : JtF.sFamilyJob,
                                    cFamilyIncome = JtF == null ? "ไม่พบข้อมูล" : JtF.nFamilyIncome.ToString(),
                                    cFamilyEducation = JtF == null ? "ไม่พบข้อมูล" : JtF.sFamilyGraduated.ToString(),
                                    cFamilyReligion = JtF == null ? "99" : JtF.sFamilyReligion,
                                    cFamilyStatus = JtF == null ? "ไม่พบข้อมูล" : JtF.familyStatus.ToString(),

                                    cFatherIncome = JtF == null ? null : JtF.nFatherIncome,
                                    cFatherEducation = JtF == null ? null : JtF.sFatherGraduated,
                                    cFatherReligion = JtF == null ? "999" : JtF.sFatherReligion
                                }).ToList();


                dataList = (from a in ListData
                            orderby a.aID
                            select new ModelData1
                            {
                                studentID = a.aID,
                                studentName = a.aName,
                                studentlastName = a.aLasname,
                                teacher = a.teacher,
                                AGE = DateTime.Now.Year - a.cBirthDay.Value.Year,
                                Sex = a.aSex,
                                DateMoveIn = a.cDateMoveIn,
                                Religion = Common.geTReligion(religion, (a.cReligion == null || a.cReligion == "") ? "999" : a.cReligion),
                                Race = Common.geTRace(race, (a.cRace == null || a.cRace == "") ? "000" : a.cRace),
                                Children = a.cChildren,
                                ProvinceId = a.cProvinceID,
                                ProvinceName = a.cProvinceNAME,
                                AmphurId = a.cAmphurID,
                                AmphurName = a.cAmphurNAME,
                                DistrictId = a.cDistrictID,
                                DistrictName = a.cDistrictNAME,

                                GroupClassId = a.groupclassid,
                                GroupClassname = a.groupsupername,
                                GroupClassvalue = a.groupsortvalue,
                                ClassId = a.classid,
                                ClassName = a.classname,
                                RoomId = a.roomid,
                                RoomName = a.roomname,

                                FamilyJob = a.cFamilyJob == "" ? "ไม่ระบุข้อมูล" : a.cFamilyJob,
                                FamilyIncome = a.cFamilyIncome,
                                FamilyEducation = a.cFamilyEducation,
                                FamilyReligion = Common.geTReligion(religion, a.cFamilyReligion),
                                FamilyStatus = a.cFamilyStatus,

                                FatherIncome = a.cFatherIncome == null ? 0 : (a.cFatherIncome * 12),
                                FatherEducation = a.cFatherEducation == null ? 0 : a.cFatherEducation,
                                FatherReligion = Common.geTReligion(religion, (a.cFatherReligion == null || a.cFatherReligion == "") ? "999" : a.cFatherReligion)
                            }).ToList();
            }



            if (rtype == "excel")
            {
                //ExcelPackage excel = new ExcelPackage();

                //switch (type)
                //{
                //    case 1:
                //        excel = Report1(year, term, lvl1, lvl2, name, cols);
                //        break;

                //    case 2:
                //        excel = Report2(year, term, lvl1, lvl2, name, cols);
                //        break;

                //    case 3:
                //        excel = Report3(year, term, lvl1, lvl2, name, cols);
                //        break;

                //    case 4:
                //        excel = Report4(year, term, lvl1, lvl2, name, cols);
                //        break;

                //    case 5:
                //        excel = Report5(year, term, lvl1, lvl2, name, cols);
                //        break;

                //    case 6:
                //        excel = Report6(year, term, lvl1, lvl2, name, cols);
                //        break;

                //    case 7:
                //        excel = Report7(year, term, lvl1, lvl2, name, cols);
                //        break;

                //    case 8:
                //        excel = Report8(year, term, lvl1, lvl2, name, cols);
                //        break;

                //    case 9:
                //        excel = Report9(year, term, lvl1, lvl2, name, cols);
                //        break;

                //    default:
                //        break;
                //}


                //context.Response.Clear();
                //context.Response.AddHeader("content-disposition", "attachment; filename=รายงาน_รายชื่อนักเรียน_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                //context.Response.ContentType = "application/text";
                //context.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                //context.Response.BinaryWrite(excel.GetAsByteArray());
                //context.Response.Flush(); // Sends all currently buffered output to the client.
                //context.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                //context.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
            }
            else if (rtype == "pdf")
            {
                var path = context.Server.MapPath("~/Fonts/THSarabun.ttf");
                FontFactory.Register(path, "thsarabun1");


                string filename = "";
                byte[] doc = null;

                switch (type)
                {
                    case 11:
                        filename = $"รายงานจำนวนนักเรียน_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
                        doc = ReportPDF11();
                        break;

                    case 12:
                        filename = $"รายงานจำนวนนักเรียน(แสดงชั้นห้อง)_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
                        doc = ReportPDF12();
                        break;

                    case 13:
                        filename = $"รายงานจำนวนนักเรียนแยกแผนการเรียน_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
                        doc = ReportPDF13();
                        break;
                    //case 9:
                    //    excel = Report9(year, term, lvl1, lvl2, name, cols);
                    //    break;

                    default:
                        break;
                }


                context.Response.Clear();
                context.Response.AddHeader("content-disposition", "attachment; filename=รายงาน_รายชื่อนักเรียน_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
                context.Response.ContentType = "application/pdf";
                context.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                context.Response.BinaryWrite(doc);
                context.Response.Flush(); // Sends all currently buffered output to the client.
                context.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                context.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**

            }
        }


        private void SetPdfHeaderType1(PdfWriter writer1, Document doc1, string reportName)
        {
            writer1.PageEvent = new ITextEvents(school, " ", " ", reportName);

            //PdfPTable table = new PdfPTable(3);
            //table.WidthPercentage = 99f;
            //table.SetTotalWidth(new float[] { 20, 55, 25 });
            //table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            //if (!string.IsNullOrEmpty(school.sImage))
            //{
            //    var jpg = iTextSharp.text.Image.GetInstance(school.sImage);
            //    jpg.Alignment = Element.ALIGN_CENTER;
            //    jpg.PaddingTop = 10;
            //    jpg.ScaleAbsolute(70, 70);
            //    PdfPCell cell = new PdfPCell(jpg);
            //    cell.Rowspan = 5;
            //    cell.Border = 0;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //    table.AddCell(cell);
            //}

            ////tableCol = tableCol - 1;
            ////var addColLength = addedCol.Count;

            //table.AddCell(SetCellPDF(
            //    text: school.sCompany,
            //    horizotal: Element.ALIGN_LEFT,
            //    fontSize: 14,
            //    border: iTextSharp.text.Rectangle.NO_BORDER
            //    ));

            //table.AddCell(SetCellPDF(
            //  text: reportName,
            //  fontSize: 16,
            //  horizotal: Element.ALIGN_CENTER,
            //  vetical: Element.ALIGN_TOP,
            //  fontStyle: iTextSharp.text.Font.BOLD,
            //   border: iTextSharp.text.Rectangle.NO_BORDER,
            //  rowspan: 5
            //  ));

            //table.AddCell(SetCellPDF(
            //   text: school.sAddress,
            //   horizotal: Element.ALIGN_LEFT,
            //    border: iTextSharp.text.Rectangle.NO_BORDER
            //   ));

            //table.AddCell(SetCellPDF(
            //  text: $"โทรศัพท์ : {school.sPhoneOne + (string.IsNullOrEmpty(school.sPhoneTwo) ? "" : " ," + school.sPhoneTwo)}  โทรสาร : {school.sFax}",
            //  horizotal: Element.ALIGN_LEFT,
            //   border: iTextSharp.text.Rectangle.NO_BORDER
            //  ));

            //table.AddCell(SetCellPDF(
            //  text: $"{school.sWebsite}  อีเมล์ : {school.sEmailOne + (string.IsNullOrEmpty(school.sEmailTwo) ? "" : " ," + school.sEmailTwo)}",
            //  horizotal: Element.ALIGN_LEFT,
            //   border: iTextSharp.text.Rectangle.NO_BORDER
            //  ));

            //table.AddCell(SetCellPDF(
            //text: " ",
            //colspan: 3,
            //border: iTextSharp.text.Rectangle.NO_BORDER
            //));

            //doc1.Add(table);

            //doc1.Add(new Paragraph("\n"));
        }

        #region PDF Report

        private PdfPCell SetCellPDF(
          string text = ""
         , int fontSize = 12
         , int fontStyle = iTextSharp.text.Font.NORMAL
         , int horizotal = Element.ALIGN_CENTER
         , int vetical = Element.ALIGN_MIDDLE
         , int border = iTextSharp.text.Rectangle.BOX
         , int colspan = 1
         , int rowspan = 1
         , BaseColor bgColor = null
         )
        {
            var font = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, fontSize, fontStyle);
            var p = new Phrase(text, font);
            var cell = new PdfPCell(p);
            if (bgColor != null)
                cell.BackgroundColor = bgColor;
            //cell.UseAscender = true;
            cell.VerticalAlignment = vetical;
            cell.HorizontalAlignment = horizotal;
            cell.Border = border;
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;
            cell.PaddingBottom = 2f;
            cell.PaddingTop = 1f;
            return cell;
        }


        private byte[] ReportPDF11()
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                //List<Report3VM> studentList = GetDataReport3(year, term, lvl1, lvl2, name, dbschool);

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (dataList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        var d1 = dataList
                           .GroupBy(o => new
                           {
                               o.ClassId,
                               o.ClassName,
                               o.RoomName,
                           })
                           .Select(o => new
                           {
                               ClassId = o.Key.ClassId,
                               ClassName = $"{o.Key.ClassName}",
                               Room = $"{o.Key.ClassName}/{o.Key.RoomName}",
                               //RoomCount = mEmList.Where(i => i.ClassName == o.Key.ClassName).Count(),
                               Male = o.Count(i => i.Sex == "0"),
                               Female = o.Count(i => i.Sex == "1"),
                               All = o.Count(),
                               GroupClassvalue = o.Min(i => i.GroupClassvalue),
                           })
                           .GroupBy(o => new
                           {
                               o.ClassName
                           })
                           .Select(o => new
                           {
                               ClassId = o.Min(i => i.ClassId),
                               o.Key.ClassName,
                               RoomCount = o.Count(),
                               Male = o.Sum(i => i.Male),
                               Female = o.Sum(i => i.Female),
                               All = o.Sum(i => i.Male + i.Female),
                               GroupClassvalue = o.Max(i => i.GroupClassvalue),
                               Type = 0,
                           })
                           .ToList();

                        var d2 = d1
                            .GroupBy(o => new
                            {
                                o.GroupClassvalue,
                            })
                            .Select(o => new
                            {
                                ClassId = o.Max(i => i.ClassId) + 1,
                                ClassName = $"รวม",
                                RoomCount = d1.Where(i => i.GroupClassvalue == o.Key.GroupClassvalue)
                                .Sum(i => i.RoomCount),
                                Male = o.Sum(i => i.Male),
                                Female = o.Sum(i => i.Female),
                                All = o.Sum(i => i.Male + i.Female),
                                GroupClassvalue = o.Key.GroupClassvalue,
                                Type = 1
                            })
                            .ToList();

                        var d3 = d2
                         .GroupBy(o => new
                         {
                             o.ClassName,
                         })
                         .Select(o => new
                         {
                             ClassId = o.Max(i => i.ClassId) + 1,
                             ClassName = $"รวมทั้งหมด",
                             RoomCount = d2.Sum(i => i.RoomCount),
                             Male = o.Sum(i => i.Male),
                             Female = o.Sum(i => i.Female),
                             All = o.Sum(i => i.Male + i.Female),
                             GroupClassvalue = o.Max(i => i.GroupClassvalue) + 1,
                             Type = 2
                         })
                         .ToList();

                        var data = d1.Concat(d2).Concat(d3)
                            .ToList()
                            .OrderBy(o => o.GroupClassvalue)
                            .ThenBy(o => o.ClassId)
                            .ThenBy(o => o.Type)
                            .ToList();

                        using (MemoryStream ms1 = new MemoryStream())
                        {
                            Document doc1 = new Document(PageSize.A4, 20, 20, 100, 20);

                            using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                            {
                                writer1.CloseStream = false;

                                writer1.PageEvent = new ITextEvents(school, " ", " ", "รายงานจำนวนนักเรียน");

                                doc1.Open();

                                PdfPTable table1 = new PdfPTable(5);
                                table1.HeaderRows = 2;
                                table1.WidthPercentage = 99;

                                table1.AddCell(SetCellPDF(
                                      text: "ระดับชั้น",
                                      rowspan: 2,
                                      fontSize: 14,
                                      //bgColor: new BaseColor(51, 122, 183),
                                      fontStyle: iTextSharp.text.Font.BOLD,
                                      vetical: Element.ALIGN_MIDDLE,
                                      horizotal: Element.ALIGN_CENTER,
                                      border: iTextSharp.text.Rectangle.BOX
                                      ));

                                table1.AddCell(SetCellPDF(
                                       text: "จำนวนห้องเรียน",
                                       //bgColor: new BaseColor(51, 122, 183),
                                       rowspan: 2,
                                       fontSize: 14,
                                       fontStyle: iTextSharp.text.Font.BOLD,
                                       vetical: Element.ALIGN_MIDDLE,
                                       horizotal: Element.ALIGN_CENTER,
                                       border: iTextSharp.text.Rectangle.BOX
                                       ));

                                table1.AddCell(SetCellPDF(
                                      text: "รายงานสถิติแสดงจำนวนนักเรียนชาย-หญิง",
                                      //bgColor: new BaseColor(51, 122, 183),
                                      colspan: 2,
                                      fontSize: 14,
                                      fontStyle: iTextSharp.text.Font.BOLD,
                                      vetical: Element.ALIGN_MIDDLE,
                                      horizotal: Element.ALIGN_CENTER,
                                      border: iTextSharp.text.Rectangle.BOX
                                      ));

                                table1.AddCell(SetCellPDF(
                                      text: "รวมนักเรียน",
                                      //bgColor: new BaseColor(51, 122, 183),
                                      rowspan: 2,
                                      fontSize: 14,
                                      fontStyle: iTextSharp.text.Font.BOLD,
                                      vetical: Element.ALIGN_MIDDLE,
                                      horizotal: Element.ALIGN_CENTER,
                                      border: iTextSharp.text.Rectangle.BOX
                                      ));

                                table1.AddCell(SetCellPDF(
                                     text: "ชาย",
                                     //bgColor: new BaseColor(51, 122, 183),
                                     fontSize: 14,
                                     fontStyle: iTextSharp.text.Font.BOLD,
                                     vetical: Element.ALIGN_MIDDLE,
                                     horizotal: Element.ALIGN_CENTER,
                                     border: iTextSharp.text.Rectangle.BOX
                                     ));

                                table1.AddCell(SetCellPDF(
                                     text: "หญิง",
                                     //bgColor: new BaseColor(51, 122, 183),
                                     fontSize: 14,
                                     fontStyle: iTextSharp.text.Font.BOLD,
                                     vetical: Element.ALIGN_MIDDLE,
                                     horizotal: Element.ALIGN_CENTER,
                                     border: iTextSharp.text.Rectangle.BOX
                                     ));

                                //doc1.Add(table1);

                                //PdfPTable table2 = new PdfPTable(5);
                                //table1.WidthPercentage = 99;
                                //row data
                                for (int row = 0; row < data.Count(); row++)
                                {
                                    var s = data[row];

                                    table1.AddCell(SetCellPDF(text: s.ClassName + ""));
                                    table1.AddCell(SetCellPDF(text: s.RoomCount + ""));
                                    table1.AddCell(SetCellPDF(text: s.Male + ""));
                                    table1.AddCell(SetCellPDF(text: s.Female + ""));
                                    table1.AddCell(SetCellPDF(text: s.All + ""));
                                }

                                doc1.Add(table1);

                                doc1.Close();
                                //  writer1.Close();
                            }
                            //

                            ms1.Position = 0;
                            var x = new PdfReader(ms1);
                            copy.AddDocument(x);
                            ms1.Dispose();

                        }



                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }

                // return doc;
            };
        }

        private byte[] ReportPDF12()
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (dataList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        var d1 = dataList
                             .GroupBy(o => new
                             {
                                 o.ClassId,
                                 o.ClassName,
                                 o.RoomId,
                                 o.RoomName,
                             })
                            .OrderBy(o => o.Key.ClassId).ThenBy(o => o.Key.RoomId)
                            .Select(o => new
                            {
                                ClassId = o.Key.ClassId + 0.0,
                                SortKey = Regex.Replace($"{o.Key.ClassName}/{o.Key.RoomName}", "[^0-9]", ""),
                                SortKey2 = Regex.Replace($"{o.Key.ClassName}/{o.Key.RoomName}", "[^0-9]", "").Length,
                                ClassName = o.Key.ClassName,
                                RoomName = $"{o.Key.ClassName}/{o.Key.RoomName}",
                                Male = o.Count(i => i.Sex == "0"),
                                Female = o.Count(i => i.Sex == "1"),
                                All = o.Count(),
                                GroupClassvalue = o.Max(i => i.GroupClassvalue),
                                Teacher = o.Max(i => i.teacher),
                                Type = 0,
                            })
                            .ToList();

                        var d2 = d1
                            .GroupBy(o => new
                            {
                                o.ClassName,
                            })
                            .Select(o => new
                            {
                                ClassId = o.Max(i => i.ClassId) + 0.5,
                                SortKey = $"{o.Max(i => i.SortKey)}",
                                SortKey2 = 99,
                                ClassName = $"รวม",
                                RoomName = $"รวม",
                                Male = o.Sum(i => i.Male),
                                Female = o.Sum(i => i.Female),
                                All = o.Sum(i => i.Male + i.Female),
                                GroupClassvalue = o.Max(i => i.GroupClassvalue),
                                Teacher = "",
                                Type = 1,
                            })
                            .ToList();

                        var d3 = d2
                        .GroupBy(o => new
                        {
                            o.ClassName,
                        })
                        .Select(o => new
                        {
                            ClassId = o.Max(i => i.ClassId) + 1.0,
                            SortKey = $"รวมทั้งหมด",
                            SortKey2 = 999,
                            ClassName = $"รวมทั้งหมด",
                            RoomName = $"รวมทั้งหมด",
                            Male = o.Sum(i => i.Male),
                            Female = o.Sum(i => i.Female),
                            All = o.Sum(i => i.Male + i.Female),
                            GroupClassvalue = o.Max(i => i.GroupClassvalue) + 100,
                            Teacher = "",
                            Type = 2
                        })
                        .ToList();


                        var data = d1.Concat(d2).Concat(d3)
                         .OrderBy(o => o.GroupClassvalue)
                         .ThenBy(o => o.ClassId)
                         .ThenBy(o => o.SortKey2)
                         .ThenBy(o => o.SortKey)
                         .ThenBy(o => o.Type)
                         .ToList();


                        using (MemoryStream ms1 = new MemoryStream())
                        {
                            Document doc1 = new Document(PageSize.A4, 20, 20, 100, 20);

                            using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                            {
                                writer1.CloseStream = false;

                                SetPdfHeaderType1(writer1, doc1, "รายงานจำนวนนักเรียน (แสดงชั้นห้อง)");

                                doc1.Open();

                                PdfPTable table1 = new PdfPTable(5);
                                table1.HeaderRows = 2;
                                table1.WidthPercentage = 99;

                                table1.AddCell((SetCellPDF(
                                      text: "ระดับชั้น",
                                      //bgColor: new BaseColor(51, 122, 183),
                                      rowspan: 2,
                                      fontSize: 14,
                                      fontStyle: iTextSharp.text.Font.BOLD,
                                      vetical: Element.ALIGN_MIDDLE,
                                      horizotal: Element.ALIGN_CENTER,
                                      border: iTextSharp.text.Rectangle.BOX
                                      )));


                                table1.AddCell(SetCellPDF(
                                      text: "รายงานสถิติแสดงจำนวนนักเรียนชาย-หญิง",
                                      //bgColor: new BaseColor(51, 122, 183),
                                      colspan: 2,
                                      fontSize: 14,
                                      fontStyle: iTextSharp.text.Font.BOLD,
                                      vetical: Element.ALIGN_MIDDLE,
                                      horizotal: Element.ALIGN_CENTER,
                                      border: iTextSharp.text.Rectangle.BOX
                                      ));

                                table1.AddCell(SetCellPDF(
                                      text: "รวมนักเรียน",
                                      //bgColor: new BaseColor(51, 122, 183),
                                      rowspan: 2,
                                      fontSize: 14,
                                      fontStyle: iTextSharp.text.Font.BOLD,
                                      vetical: Element.ALIGN_MIDDLE,
                                      horizotal: Element.ALIGN_CENTER,
                                      border: iTextSharp.text.Rectangle.BOX
                                      ));

                                table1.AddCell(SetCellPDF(
                                       text: "ครูประจำชั้น",
                                       //bgColor: new BaseColor(51, 122, 183),
                                       rowspan: 2,
                                       fontSize: 14,
                                       fontStyle: iTextSharp.text.Font.BOLD,
                                       vetical: Element.ALIGN_MIDDLE,
                                       horizotal: Element.ALIGN_CENTER,
                                       border: iTextSharp.text.Rectangle.BOX
                                       ));

                                table1.AddCell(SetCellPDF(
                                     text: "ชาย",
                                     //bgColor: new BaseColor(51, 122, 183),
                                     fontSize: 14,
                                     fontStyle: iTextSharp.text.Font.BOLD,
                                     vetical: Element.ALIGN_MIDDLE,
                                     horizotal: Element.ALIGN_CENTER,
                                     border: iTextSharp.text.Rectangle.BOX
                                     ));

                                table1.AddCell(SetCellPDF(
                                     text: "หญิง",
                                     //bgColor: new BaseColor(51, 122, 183),
                                     fontSize: 14,
                                     fontStyle: iTextSharp.text.Font.BOLD,
                                     vetical: Element.ALIGN_MIDDLE,
                                     horizotal: Element.ALIGN_CENTER,
                                     border: iTextSharp.text.Rectangle.BOX
                                     ));

                                //doc1.Add(table1);

                                //PdfPTable table2 = new PdfPTable(4);
                                //row data
                                for (int row = 0; row < data.Count(); row++)
                                {
                                    var s = data[row];

                                    table1.AddCell(SetCellPDF(text: s.RoomName + ""));
                                    table1.AddCell(SetCellPDF(text: s.Male + ""));
                                    table1.AddCell(SetCellPDF(text: s.Female + ""));
                                    table1.AddCell(SetCellPDF(text: s.All + ""));
                                    table1.AddCell(SetCellPDF(text: s.Teacher));
                                }



                                doc1.Add(table1);

                                doc1.Close();
                                //  writer1.Close();
                            }
                            //

                            ms1.Position = 0;
                            var x = new PdfReader(ms1);
                            copy.AddDocument(x);
                            ms1.Dispose();

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }

        private byte[] ReportPDF13()
        {
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (dataList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        var q1 = from a in db.TCurriculums.Where(o => o.SchoolId == userData.CompanyID && o.IsActive == true && o.cDel == false && o.nYear == year)
                                 from b in db.TPlans.Where(o => o.SchoolID == userData.CompanyID && o.IsActive == true && o.cDel == false && o.CurriculumId == a.CurriculumId)
                                 from c in db.TPlanTermSubLevel2.Where(o => o.SchoolID == userData.CompanyID && o.IsActive == true && o.cDel == false && o.PlanId == b.PlanId)
                                 select new
                                 {
                                     b.PlanId,
                                     b.PlanName,
                                     b.nTSubLevel,
                                     c.nTermSubLevel2,
                                 };
                        var plan = q1.Distinct().ToList();

                        var d1 = (from a in dataList
                                  from b in plan.Where(o => o.nTermSubLevel2 == a.RoomId)
                                  select new
                                  {
                                      a.ClassId,
                                      a.ClassName,
                                      a.RoomId,
                                      a.RoomName,
                                      b.PlanId,
                                      b.PlanName,
                                      a.Sex,
                                      a.GroupClassvalue,
                                  })
                                   .GroupBy(o => new
                                   {
                                       o.ClassId,
                                       o.ClassName,
                                       o.RoomId,
                                       o.RoomName,
                                       o.PlanId,
                                       o.PlanName,
                                   })
                                    .Select(o => new
                                    {
                                        ClassId = o.Key.ClassId + 0.0,
                                        ClassName = $"{o.Key.ClassName}",
                                        Room = $"{o.Key.ClassName}/{o.Key.RoomName}",
                                        Plan = o.Key.PlanName,
                                        Male = o.Count(i => i.Sex == "0"),
                                        Female = o.Count(i => i.Sex == "1"),
                                        All = o.Count(),
                                        GroupClassvalue = o.Min(i => i.GroupClassvalue),
                                        Type = 0,
                                        SortKey = Regex.Replace($"{o.Key.ClassName}/{o.Key.RoomName}", "[^0-9]", ""),
                                        SortKey2 = Regex.Replace($"{o.Key.ClassName}/{o.Key.RoomName}", "[^0-9]", "").Length,
                                    })
                                    .ToList();

                        var d2 = d1
                       .GroupBy(o => new
                       {
                           o.GroupClassvalue,
                       })
                       .Select(o => new
                       {
                           ClassId = o.Max(i => i.ClassId) + 0.5,
                           ClassName = $"",
                           Room = $"",
                           Plan = $"รวม",
                           Male = o.Sum(i => i.Male),
                           Female = o.Sum(i => i.Female),
                           All = o.Sum(i => i.Male + i.Female),
                           GroupClassvalue = o.Max(i => i.GroupClassvalue),
                           Type = 1,
                           SortKey = $"{o.Max(i => i.SortKey)}",
                           SortKey2 = 99,
                       })
                       .ToList();

                        var d3 = d2
                           .GroupBy(o => new
                           {
                               o.ClassName,
                           })
                           .Select(o => new
                           {
                               ClassId = o.Max(i => i.ClassId) + 1.0,
                               ClassName = $"",
                               Room = $"",
                               Plan = $"รวมทั้งหมด",
                               Male = o.Sum(i => i.Male),
                               Female = o.Sum(i => i.Female),
                               All = o.Sum(i => i.Male + i.Female),
                               GroupClassvalue = o.Max(i => i.GroupClassvalue) + 100,
                               Type = 2,
                               SortKey = $"รวมทั้งหมด",
                               SortKey2 = 999,
                           })
                           .ToList();


                        var data = d1.Concat(d2).Concat(d3)
                             .OrderBy(o => o.GroupClassvalue)
                             .ThenBy(o => o.ClassId)
                             .ThenBy(o => o.SortKey2)
                             .ThenBy(o => o.SortKey)
                             .ThenBy(o => o.Type)
                             .ToList();

                        using (MemoryStream ms1 = new MemoryStream())
                        {
                            Document doc1 = new Document(PageSize.A4, 20, 20, 100, 20);

                            using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                            {
                                writer1.CloseStream = false;

                                SetPdfHeaderType1(writer1, doc1, "รายงานจำนวนนักเรียนแยกแผนการเรียน");

                                doc1.Open();
                                PdfPTable table1 = new PdfPTable(5);
                                table1.HeaderRows = 2;
                                table1.WidthPercentage = 99;

                                table1.AddCell(SetCellPDF(
                                      text: "ระดับชั้น",
                                      //bgColor: new BaseColor(51, 122, 183),
                                      rowspan: 2,
                                      fontSize: 14,
                                      fontStyle: iTextSharp.text.Font.BOLD,
                                      vetical: Element.ALIGN_MIDDLE,
                                      horizotal: Element.ALIGN_CENTER,
                                      border: iTextSharp.text.Rectangle.BOX
                                      ));

                                table1.AddCell(SetCellPDF(
                                     text: "แผนการเรียน",
                                     //bgColor: new BaseColor(51, 122, 183),
                                     rowspan: 2,
                                     fontSize: 14,
                                     fontStyle: iTextSharp.text.Font.BOLD,
                                     vetical: Element.ALIGN_MIDDLE,
                                     horizotal: Element.ALIGN_CENTER,
                                     border: iTextSharp.text.Rectangle.BOX
                                     ));

                                table1.AddCell(SetCellPDF(
                                      text: "จำนวนนักเรียนชาย-หญิง",
                                      //bgColor: new BaseColor(51, 122, 183),
                                      colspan: 2,
                                      fontSize: 14,
                                      fontStyle: iTextSharp.text.Font.BOLD,
                                      vetical: Element.ALIGN_MIDDLE,
                                      horizotal: Element.ALIGN_CENTER,
                                      border: iTextSharp.text.Rectangle.BOX
                                      ));

                                table1.AddCell(SetCellPDF(
                                      text: "รวมนักเรียน",
                                      //bgColor: new BaseColor(51, 122, 183),
                                      rowspan: 2,
                                      fontSize: 14,
                                      fontStyle: iTextSharp.text.Font.BOLD,
                                      vetical: Element.ALIGN_MIDDLE,
                                      horizotal: Element.ALIGN_CENTER,
                                      border: iTextSharp.text.Rectangle.BOX
                                      ));

                                table1.AddCell(SetCellPDF(
                                     text: "ชาย",
                                     //bgColor: new BaseColor(51, 122, 183),
                                     fontSize: 14,
                                     fontStyle: iTextSharp.text.Font.BOLD,
                                     vetical: Element.ALIGN_MIDDLE,
                                     horizotal: Element.ALIGN_CENTER,
                                     border: iTextSharp.text.Rectangle.BOX
                                     ));

                                table1.AddCell(SetCellPDF(
                                     text: "หญิง",
                                     //bgColor: new BaseColor(51, 122, 183),
                                     fontSize: 14,
                                     fontStyle: iTextSharp.text.Font.BOLD,
                                     vetical: Element.ALIGN_MIDDLE,
                                     horizotal: Element.ALIGN_CENTER,
                                     border: iTextSharp.text.Rectangle.BOX
                                     ));

                                //doc1.Add(table1);

                                //PdfPTable table2 = new PdfPTable(4);
                                //row data
                                for (int row = 0; row < data.Count(); row++)
                                {
                                    var s = data[row];

                                    table1.AddCell(SetCellPDF(text: s.Room + ""));
                                    table1.AddCell(SetCellPDF(text: s.Plan + ""));
                                    table1.AddCell(SetCellPDF(text: s.Male + ""));
                                    table1.AddCell(SetCellPDF(text: s.Female + ""));
                                    table1.AddCell(SetCellPDF(text: s.All + ""));
                                }

                                doc1.Add(table1);

                                doc1.Close();
                                //  writer1.Close();
                            }
                            //

                            ms1.Position = 0;
                            var x = new PdfReader(ms1);
                            copy.AddDocument(x);
                            ms1.Dispose();

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }

        #endregion

        #region Excel Report

        private void SetCell(ExcelRange xrange
          , bool isMerge = false
          , string text = ""
          , int fontSize = 11
          , bool isBold = false
          , ExcelHorizontalAlignment horizotal = ExcelHorizontalAlignment.Center
          , ExcelVerticalAlignment vetical = ExcelVerticalAlignment.Center
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

                xrange.AutoFitColumns();

            }
        }


        #endregion

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

        //private string getTitlte(List<TTitleList> titles, string titlesId)
        //{
        //    int nTitleid = 0;
        //    int.TryParse((titlesId ?? "0"), out nTitleid);
        //    var f_titles = titles.FirstOrDefault(f => f.nTitleid == nTitleid);
        //    if (f_titles == null) return titlesId;
        //    else return f_titles.titleDescription;
        //}

        //private string GetAddress(List<ModelItem1> lst, string id)
        //{
        //    return lst.FirstOrDefault(o => o.Id == id)?.Name + "";
        //}



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

        //internal class Report1StudentVM
        //{
        //    public int levelId { get; set; }
        //    public int level2Id { get; set; }
        //    public string tterm { get; set; }
        //    public string levelname { get; set; }
        //    public string level2name { get; set; }
        //    public int? student_number { get; set; }
        //    public int? studentStatus { get; set; }
        //    public string sidentification { get; set; }
        //    public string studentId { get; set; }
        //    public string studentPassWord { get; set; }
        //    public string stMoveIn { get; set; }
        //    public string studentsex { get; set; }
        //    public string titleDes { get; set; }
        //    public string studentname { get; set; }
        //    public string studentlastname { get; set; }
        //    public string stNickName { get; set; }
        //    public string studentnameEN { get; set; }
        //    public string studentlastnameEN { get; set; }
        //    public string stNickNameEN { get; set; }
        //    public string birth { get; set; }
        //    public string stReligion { get; set; }
        //    public string stRace { get; set; }
        //    public string stNation { get; set; }
        //    public int? stSonTotal { get; set; }
        //    public int? stSonNumber { get; set; }
        //    public int? stRelativeHere { get; set; }
        //    public string phone { get; set; }
        //    public string stEmail { get; set; }
        //    public string money { get; set; }
        //    public string stHomeRegistCode { get; set; }
        //    public string homeNumber { get; set; }
        //    public string muu { get; set; }
        //    public string soy { get; set; }
        //    public string road { get; set; }
        //    public string tumbon { get; set; }
        //    public string aumpher { get; set; }
        //    public string provin { get; set; }
        //    public string post { get; set; }
        //    public string stHousePhone { get; set; }
        //    public string ststayWithName { get; set; }
        //    public string ststayWithLast { get; set; }
        //    public int? ststayHomeType { get; set; }
        //    public string ststayWithEmail { get; set; }
        //    public string ststayWithEmergency { get; set; }
        //    public string friNearHomename { get; set; }
        //    public string friNearHomelast { get; set; }
        //    public string friNearHomephone { get; set; }
        //    public string stOldSchoolName { get; set; }
        //    public string stOldSchoolTumbon { get; set; }
        //    public string stOldSchoolAumpher { get; set; }
        //    public string stOldSchoolProvince { get; set; }
        //    public string stOldSchoolGraduated { get; set; }
        //    public decimal? stOldSchoolGPA { get; set; }
        //    public string stmoveOutReason { get; set; }
        //    public string stOldhome { get; set; }
        //    public string stOldmuu { get; set; }
        //    public string stOldsoy { get; set; }
        //    public string stOldroad { get; set; }
        //    public string stOldtumbon { get; set; }
        //    public string stOldaumper { get; set; }
        //    public string stOldprovince { get; set; }
        //    public string stOldpostcode { get; set; }
        //    public string stOldphone { get; set; }
        //    public string famRelate { get; set; }
        //    public string famTitle { get; set; }
        //    public string famName { get; set; }
        //    public string famlastname { get; set; }
        //    public string famNameEN { get; set; }
        //    public string famlastnameEN { get; set; }
        //    public string famBirday { get; set; }
        //    public string famReligion { get; set; }
        //    public string famRace { get; set; }
        //    public string famNation { get; set; }
        //    public string famhome { get; set; }
        //    public string fammuu { get; set; }
        //    public string famsoy { get; set; }
        //    public string famroad { get; set; }
        //    public string famtumbon { get; set; }
        //    public string famaumper { get; set; }
        //    public string famprovince { get; set; }
        //    public string fampostcode { get; set; }
        //    public string famphone1 { get; set; }
        //    public string famphone2 { get; set; }
        //    public string famphone3 { get; set; }
        //    public int? famstatus { get; set; }
        //    public int? fameducation { get; set; }
        //    public string famJob { get; set; }
        //    public string famJobTower { get; set; }
        //    public string famJobSalary { get; set; }
        //    public int? famWithdrawMoney { get; set; }
        //    public string faterTitle { get; set; }
        //    public string faterName { get; set; }
        //    public string faterLastname { get; set; }
        //    public string faterNameEN { get; set; }
        //    public string faterLastnameEN { get; set; }
        //    public string faterBirday { get; set; }
        //    public string faterReligion { get; set; }
        //    public string faterRace { get; set; }
        //    public string faterNation { get; set; }
        //    public string faterhome { get; set; }
        //    public string fatermuu { get; set; }
        //    public string fatersoy { get; set; }
        //    public string faterroad { get; set; }
        //    public string fatertumbon { get; set; }
        //    public string fateraumper { get; set; }
        //    public string faterprovince { get; set; }
        //    public string faterpostcode { get; set; }
        //    public string faterphone1 { get; set; }
        //    public string faterphone2 { get; set; }
        //    public string faterphone3 { get; set; }
        //    public int? fatereducation { get; set; }
        //    public string faterJob { get; set; }
        //    public string faterJobTower { get; set; }
        //    public string faterJobSalary { get; set; }
        //    public string moterTitle { get; set; }
        //    public string moterName { get; set; }
        //    public string moterLastname { get; set; }
        //    public string moterNameEN { get; set; }
        //    public string moterLastnameEN { get; set; }
        //    public string moterBirday { get; set; }
        //    public string moterReligion { get; set; }
        //    public string moterRace { get; set; }
        //    public string moterNation { get; set; }
        //    public string moterhome { get; set; }
        //    public string motermuu { get; set; }
        //    public string motersoy { get; set; }
        //    public string moterroad { get; set; }
        //    public string motertumbon { get; set; }
        //    public string moteraumper { get; set; }
        //    public string moterprovince { get; set; }
        //    public string moterpostcode { get; set; }
        //    public string moterphone1 { get; set; }
        //    public string moterphone2 { get; set; }
        //    public string moterphone3 { get; set; }
        //    public int? motereducation { get; set; }
        //    public string moterJob { get; set; }
        //    public string moterJobTower { get; set; }
        //    public string moterJobSalary { get; set; }
        //    public decimal? stdWeight { get; set; }
        //    public decimal? stdHeight { get; set; }
        //    public string stdBlood { get; set; }
        //    public string stdSickFood { get; set; }
        //    public string stdSickDruq { get; set; }
        //    public string stdSickOther { get; set; }
        //    public string stdSickNormal { get; set; }
        //    public string stdSickDanger { get; set; }
        //}

        //internal class Report1VM
        //{
        //    internal int male;
        //    internal int female;

        //    public string teacher { get; set; }
        //    public string level1 { get; set; }
        //    public string level2 { get; set; }
        //    public List<Report1StudentVM> students { get; set; }
        //}

        //internal class Report6StudentVM
        //{
        //    public int? no { get; set; }
        //    public string code { get; set; }
        //    public string name { get; set; }
        //    public string id { get; set; }
        //    public string familyid { get; set; }
        //    public string family { get; set; }
        //    public string motherid { get; set; }
        //    public string mother { get; set; }
        //    public string fatherid { get; set; }
        //    public string father { get; set; }
        //}

        //internal class Report6VM
        //{
        //    public string teacher { get; set; }
        //    public int? year { get; set; }
        //    public string term { get; set; }
        //    public string level1 { get; set; }
        //    public string level2 { get; set; }
        //    public int male { get; set; }
        //    public int female { get; set; }
        //    public List<Report6StudentVM> students { get; set; }
        //}

        //internal class Report3VM
        //{
        //    public string teacher { get; set; }
        //    public int? year { get; set; }
        //    public string term { get; set; }
        //    public string level1 { get; set; }
        //    public string level2 { get; set; }
        //    public int male { get; set; }
        //    public int female { get; set; }
        //    public List<Report3StudentVM> students { get; set; }
        //}

        //internal class Report3StudentVM
        //{
        //    public int? no { get; set; }
        //    public string code { get; set; }
        //    public string name { get; set; }
        //    public string nick { get; set; }
        //    public string id { get; set; }
        //    public string date { get; set; }
        //    public string picture { get; set; }
        //}
    }

    internal class ModelData1
    {
        public int studentID { get; set; }
        public string studentName { get; set; }
        public string studentlastName { get; set; }
        public int AGE { get; set; }
        public string Sex { get; set; }
        public string DateMoveIn { get; set; }
        public string Religion { get; set; }
        public string Race { get; set; }
        public string Children { get; set; }
        public string ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public string AmphurId { get; set; }
        public string AmphurName { get; set; }
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int GroupClassId { get; set; }
        public string GroupClassname { get; set; }
        public int? GroupClassvalue { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string FamilyJob { get; set; }
        public string FamilyIncome { get; set; }
        public string FamilyEducation { get; set; }
        public string FamilyReligion { get; set; }
        public string FamilyStatus { get; set; }
        public double? FatherIncome { get; set; }
        public int? FatherEducation { get; set; }
        public string FatherReligion { get; set; }
        public string teacher { get; internal set; }
    }


}