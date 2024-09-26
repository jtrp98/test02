using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using FingerprintPayment.Card.CsCode;

namespace FingerprintPayment.Card.PermissionCard
{
    public partial class Main : CardGateway
    {

        public class Search
        {
            public int? sID { get; set; }
            public int? typeID { get; set; }
            public string term { get; set; }
            public int? level1 { get; set; }
            public int? level2 { get; set; }
            public string name { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object LoadData(Search search)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new CardsRepository(ctx);

                var result = logic.LoadPermissionCard(userData.CompanyID, search.term, search.level1, search.level2, search.name);

                return new
                {
                    data = result.Select((o, i) => new
                    {
                        Index = i + 1,
                        o.sID,
                        o.Number,
                        o.Code,
                        o.FullName,
                        o.Room,
                        o.Time,
                        o.Term,
                    })
                };

            }

        }

        private static T1 CloneObject<T1, T2>(T2 source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null)) return default;

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<T1>(JsonConvert.SerializeObject(source), deserializeSettings);
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object OpenDialog(Search search)
        {
            var userData = GetUserData();
            var schoolId = userData.CompanyID;
            var now = DateTime.Now;
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolId,ConnectionDB.Read)))
            {
                var model = new UCDialog.Model();

                var student = ctx.TB_StudentViews
                    .Where(o => o.SchoolID == schoolId && o.sID == search.sID && o.nTerm == search.term)
                    .Select(o => new
                    {
                        code = o.sStudentID,
                        name = o.titleDescription + " " + o.sName + " " + o.sLastname,
                        level = o.SubLevel + "/" + o.nTSubLevel2
                    })
                    .FirstOrDefault();

                model.Term = search.term;
                model.StudentID = search.sID.Value;
                model.No = (ctx.TPermissionCard.Where(o => o.SchoolID == schoolId).Max(o => o.RefNo) ?? 0) + 1;
                model.Date = now;
                model.StudentCode = student.code;
                model.StudentName = student.name;
                model.Class = student.level;

                var countType = ctx.TPermissionCardType.Where(o => o.SchoolID == schoolId && o.IsDel == false).Count();

                #region init type
                if (countType == 0)
                {
                    var lst2Add = new List<TPermissionCardType>();
                    var temp = new TPermissionCardType()
                    {
                        Permission = "",
                        SchoolID = schoolId,
                        IsDel = false,
                        Created = now,
                        Modified = now,
                        Creator = userData.UserID,
                        Modifier = userData.UserID,
                    };
                    var add = CloneObject<TPermissionCardType, TPermissionCardType>(temp);
                    add.Permission = "1. เข้าห้องเรียน";
                    lst2Add.Add(add);

                    add = CloneObject<TPermissionCardType, TPermissionCardType>(temp);
                    add.Permission = "2. ออกนอกห้องเรียน";
                    lst2Add.Add(add);

                    add = CloneObject<TPermissionCardType, TPermissionCardType>(temp);
                    add.Permission = "3. ออกนอกบริเวณโรงเรียน";
                    lst2Add.Add(add);

                    add = CloneObject<TPermissionCardType, TPermissionCardType>(temp);
                    add.Permission = "4. ขออนุญาตอื่นๆ";
                    lst2Add.Add(add);

                    add = CloneObject<TPermissionCardType, TPermissionCardType>(temp);
                    add.Permission = "5. นอนห้องพยาบาล";
                    lst2Add.Add(add);

                    add = CloneObject<TPermissionCardType, TPermissionCardType>(temp);
                    add.Permission = "6. ผู้ปกครองมารับก่อนกำหนด";
                    lst2Add.Add(add);

                    ctx.TPermissionCardType.AddRange(lst2Add);
                    ctx.SaveChanges();
                }

                #endregion

                var listType = ctx.TPermissionCardType.Where(o => o.SchoolID == schoolId && o.IsDel == false).AsEnumerable()
                    .Select(o => new UCDialog.Model.SelectModel
                    {
                        Value = o.ID,
                        Text = o.Permission,
                    })
                    .ToList();
                model.TypeList = listType;

                var html = RenderPartialToString("~/Card/PermissionCard/UCDialog.ascx", model);
                return new { html = html };
            }
        }
        public static string RenderPartialToString(string controlName, UCDialog.Model viewData)
        {
            ViewDataDictionary vd = new ViewDataDictionary(viewData);
            ViewPage vp = new ViewPage { ViewData = vd };
            var control = vp.LoadControl(controlName) as UCDialog;
            control.ModelData = viewData;
            vp.Controls.Add(control);

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                {
                    vp.RenderControl(tw);
                }
            }

            return sb.ToString();
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object CheckHistoryCountTime(Search search)
        {
            var userData = GetUserData();
            var schoolId = userData.CompanyID;

            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolId,ConnectionDB.Read)))
            {
                var count = ctx.TPermissionCard
                    .Where(o => o.SchoolID == schoolId && o.StudentID == search.sID && o.TypeID == search.typeID && o.IsDel == false)
                    .Count();

                return new { count = count + 1 };
            }
        }



        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //[WebMethod(EnableSession = true)]
        //public static void ExportExcel(Search search)
        //{
        //    var userData = GetUserData();
        //    using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
        //    {
        //        var logic = new SDQRepository(ctx);

        //        var result = logic.LoadReportStudent(userData.CompanyID, term, level1, level2, name, type);

        //        var data = result.Select((o, i) => new
        //        {
        //            Index = i + 1,
        //            o.sId,
        //            o.Number,
        //            o.Code,
        //            o.FullName,
        //            o.Room,
        //            o.Score1,
        //            o.Score2,
        //            o.Score3,
        //            o.Score4,
        //            o.Score5,
        //        });

        //        using (ExcelPackage excel = new ExcelPackage())
        //        {
        //            excel.Workbook.Worksheets.Add("รายงานสรุปผลรายบุคคล");

        //            var worksheet = excel.Workbook.Worksheets["รายงานสรุปผลรายบุคคล"];
        //            string entities = HttpContext.Current.Session["sEntities"].ToString();
        //            var row = 1;

        //            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
        //            {
        //                var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

        //                SetCell(worksheet.Cells[row, 1, row++, 10]
        //                  , isMerge: true
        //                  , text: school.sCompany
        //                  , fontSize: 14
        //                  , isBold: true);

        //                SetCell(worksheet.Cells[row, 1, row++, 10]
        //                , isMerge: true
        //                , text: $"รายงานสรุปผลรายบุคคล"
        //                );

        //                SetCell(worksheet.Cells[row, 1, row++, 10]
        //                 , isMerge: true
        //                 , text: $"ปีการศึกษา {yearNo} ภาคเรียนที่ {termNo}"
        //                 );
        //            }

        //            SetCell(worksheet.Cells[row, 1], isHeader: true, text: "ลำดับ");
        //            SetCell(worksheet.Cells[row, 2], isHeader: true, text: "รหัสนักเรียน");
        //            SetCell(worksheet.Cells[row, 3], isHeader: true, text: "ชั้นเรียน");
        //            SetCell(worksheet.Cells[row, 4], isHeader: true, text: "ชื่อ-นามสกุล");
        //            SetCell(worksheet.Cells[row, 5], isHeader: true, text: "อารมณ์");
        //            SetCell(worksheet.Cells[row, 6], isHeader: true, text: "ความพฤติ");
        //            SetCell(worksheet.Cells[row, 7], isHeader: true, text: "สมาธิ");
        //            SetCell(worksheet.Cells[row, 8], isHeader: true, text: "ความสัมพันธ์กับเพื่อน");
        //            SetCell(worksheet.Cells[row, 9], isHeader: true, text: "สังคม");
        //            SetCell(worksheet.Cells[row++, 10], isHeader: true, text: "ผลประเมิน");

        //            foreach (var r in data)
        //            {

        //                SetCell(worksheet.Cells[row, 1], text: r.Index + "");
        //                SetCell(worksheet.Cells[row, 2], text: r.Code + "");
        //                SetCell(worksheet.Cells[row, 3], text: r.Room + "");
        //                SetCell(worksheet.Cells[row, 4], text: r.FullName + "");

        //                var c = CalcTypeScore(type, 1, r.Score1);
        //                SetCell(worksheet.Cells[row, 5], text: c.Item2, color: c.Item1);
        //                c = CalcTypeScore(type, 2, r.Score2);
        //                SetCell(worksheet.Cells[row, 6], text: c.Item2, color: c.Item1);
        //                c = CalcTypeScore(type, 3, r.Score3);
        //                SetCell(worksheet.Cells[row, 7], text: c.Item2, color: c.Item1);
        //                c = CalcTypeScore(type, 4, r.Score4);
        //                SetCell(worksheet.Cells[row, 8], text: c.Item2, color: c.Item1);
        //                c = CalcTypeScore(type, 5, r.Score5);
        //                SetCell(worksheet.Cells[row, 9], text: c.Item2, color: c.Item1);

        //                int? sumAll = 0;
        //                switch (type)
        //                {
        //                    case 1:
        //                        sumAll = r.Score1 + r.Score2 + r.Score3 + r.Score4;
        //                        break;

        //                    case 2:
        //                    case 3:
        //                        sumAll = r.Score1 + r.Score2 + r.Score3 + r.Score4 + r.Score5;
        //                        break;

        //                    default:
        //                        break;
        //                }

        //                c = CalcTypeScore(type, 6, sumAll);
        //                SetCell(worksheet.Cells[row, 10], text: c.Item2, color: c.Item1);

        //                row++;
        //            }

        //            worksheet.Cells.AutoFitColumns(20, 40);

        //            HttpContext.Current.Response.Clear();
        //            HttpContext.Current.Response.ContentType = "application/text;";
        //            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
        //            HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
        //            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        //            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        //            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**

        //        }
        //    }
        //}

    }
}