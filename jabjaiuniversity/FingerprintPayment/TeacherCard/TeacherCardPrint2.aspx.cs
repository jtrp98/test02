using EllipticCurve.Utils;
using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using ScottPlot.Drawing.Colormaps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using TemplateBuilder;

namespace FingerprintPayment.TeacherCard
{
    public partial class TeacherCardPrint2 : System.Web.UI.Page
    {
        public int schoolID { get; set; }
        public string BackgroundCard { get; set; } = "";
        public float Height { get; set; } = 8.5f;
        public float Width { get; set; } = 5.4f;
        public string PrintDate { get; set; }
        public string ExpireDate { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            schoolID = userData.CompanyID;

            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");



            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string modeall = Request.QueryString["modeall"];
                string id = Request.QueryString["id"];
                int? idn = null;
                if (!string.IsNullOrEmpty(id))
                {
                    idn = int.Parse(id);
                }

                string term = Request.QueryString["term"];

                var Q_title = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();
                var company = dbmaster.TCompanies.FirstOrDefault(o => o.nCompany == schoolID);

                var qryEmp = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null);
                var qryUser = dbmaster.TUsers.Where(w => w.nCompany == userData.CompanyID && w.cDel == null && w.cType == "1");

                if (idn.HasValue)
                {
                    qryEmp = qryEmp.Where(o => o.sEmp == idn);
                    qryUser = qryUser.Where(o => o.sID == idn);
                }

                var teacherData = (from a in qryEmp
                                   from b in _db.TEmployeeInfoes.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == a.sEmp).DefaultIfEmpty()
                                   from c in _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID && w.deleted != "1" && w.nJobid == a.nJobid).DefaultIfEmpty()
                                   from d in _db.TDepartments.Where(w => w.SchoolID == userData.CompanyID && w.DepID == a.nDepartmentId).DefaultIfEmpty()
                                   from f in _db.TEmployeeTypes.Where(o => o.nTypeId + "" == a.cType).DefaultIfEmpty()

                                   select new
                                   {
                                       a.sEmp,
                                       Code = b == null ? string.Empty : b.Code,
                                       a.sIdentification,
                                       a.sTitle,
                                       a.sName,
                                       a.sLastname,
                                       a.sPicture,
                                       FirstNameEn = b == null ? string.Empty : b.FirstNameEn,
                                       LastNameEn = b == null ? string.Empty : b.LastNameEn,
                                       EmpType = f == null ? string.Empty : f.Title,
                                       EmpDepartment = d == null ? string.Empty : d.departmentName,
                                       EmpJob = c == null ? "" : c.jobDescription,
                                   }).ToList();

                var teacherMaster = qryUser.Select(o => new { o.sID, o.sPicture }).ToList();

                var teacherList = (from a in teacherData
                                   from b in teacherMaster.Where(o => o.sID == a.sEmp).DefaultIfEmpty()
                                   select new
                                   {
                                       EmpID = a.sEmp,
                                       Code = string.IsNullOrEmpty(a.Code) ? "ไม่พบข้อมูล" : AddDash(a.Code),
                                       Identification = string.IsNullOrEmpty(a.sIdentification) ? "ไม่พบข้อมูล" : AddDash(a.sIdentification),
                                       Title = Common.geTitelName(Q_title, a.sTitle),
                                       EmpFullName = a.sName + " " + a.sLastname,
                                       EmpFullNameEN = a.FirstNameEn + " " + a.LastNameEn,
                                       EmpType = string.IsNullOrEmpty(a.EmpType) ? "ไม่พบข้อมูล" : a.EmpType,
                                       EmpJob = string.IsNullOrEmpty(a.EmpJob) ? "ไม่พบข้อมูล" : a.EmpJob,
                                       EmpPic = b?.sPicture ?? a?.sPicture ?? "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png",

                                       QRCode = QRCodeFunction.Create(a.Code, QRCoder.QRCodeGenerator.ECCLevel.H, 1, false),
                                       Barcode = BarCodeFunction.Create(a.Code, BarCodeFunction.BarcodeTYPE.CODE128, false, BarCodeFunction.Alignment.CENTER, 193, 43),
                                       EmpDepartment = string.IsNullOrEmpty(a.EmpDepartment) ? "ไม่พบข้อมูล" : a.EmpDepartment
                                   }).ToList();

                var cardInfo = _db.TTeacherCardInfoes.Where(w => w.SchoolID == schoolID).ToList();

                var cardType = cardInfo.Where(w => w.elementName == "NewCardType").FirstOrDefault();
                var card = cardInfo.Where(w => w.elementName == "CardCover").FirstOrDefault();
                var printDate = cardInfo.Where(w => w.elementName == "CardPrintDate").FirstOrDefault();
                var enddate = cardInfo.Where(w => w.elementName == "CardExpireDate").FirstOrDefault();

                if (card != null && card.elementValue != "")
                {
                    BackgroundCard = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm"));
                }

                if (printDate != null && printDate.elementValue != "")
                {
                    if (printDate.elementValue != "")
                        PrintDate = printDate.date?.ToString("dd/MM/yy", new CultureInfo("th-TH"));
                }

                if (enddate != null && enddate.elementValue != "")
                {
                    if (enddate.elementValue != "")
                        ExpireDate = enddate.date?.ToString("dd/MM/yy", new CultureInfo("th-TH"));
                }

                using (var context = new TemplateBuilderContext())
                {
                    var form = context.TblReportForm
                        .Where(o => o.nFormID + "" == cardType.elementValue)
                        .FirstOrDefault();

                    if (form != null)
                    {
                        if (form.sPaperSize == "Card-Portrait")
                        {
                            Height = 128f;
                            Width = 82f;
                        }
                        else if (form.sPaperSize == "Card-Landscape")
                        {
                            Width = 128f;
                            Height = 82f;
                        }
                    }

                    var templateHead = context.TblReportTemplate.FirstOrDefault(o => o.nTemplateID == form.nHeaderTemplateID);
                    var templateBody = context.TblReportTemplate.FirstOrDefault(o => o.nTemplateID == form.nBodyTemplateID);
                    var templateFoot = context.TblReportTemplate.FirstOrDefault(o => o.nTemplateID == form.nFooterTemplateID);

                    foreach (var teacher in teacherList)
                    {
                        var dict = new Dictionary<string, string>();
                        var imgProfile = "";

                        if (!string.IsNullOrEmpty(teacher?.EmpPic))
                        {
                            if (teacher.EmpPic.Contains("?x-image-process=image/resize,m_fill,h_300,w_270"))
                            {
                                imgProfile = teacher?.EmpPic + "&d=" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                            }
                            else
                            {
                                imgProfile = teacher?.EmpPic + "?d=" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                            }
                        }
                        //else
                        //{
                        //    imgProfile = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";
                        //}

                        dict.Add("<ชื่อโรงเรียน>", $"{company.sCompany}");
                        dict.Add("<ชื่อโรงเรียนEng>", $"{company.sNameEN}");
                        dict.Add("<โลโก้โรงเรียน>", $"{company.sImage}");
                        dict.Add("<รูปบุคลากร>", imgProfile);
                        dict.Add("<แผนก>", teacher.EmpDepartment);
                        dict.Add("<เลขบัตรประชาชนบุคลากร>", AddDash(teacher.Identification));
                        dict.Add("<ประเภทบุคลากร>", teacher.EmpType);
                        dict.Add("<ตำแหน่ง>", teacher.EmpJob);
                        dict.Add("<ชื่อพนักงาน>", teacher.EmpFullName);
                        dict.Add("<รหัสพนักงาน>", AddDash(teacher.Code));
                        dict.Add("<ชื่อพนักงานEng>", teacher.EmpFullNameEN);
                        dict.Add("<คำนำหน้าบุคลากร>", $"{Common.geTitelName(Q_title, teacher.Title)}");
                        dict.Add("<วันที่ออกบัตร>", PrintDate);
                        dict.Add("<วันที่หมดอายุบัตร>", ExpireDate);
                        dict.Add("<QRCodeรหัสพนักงาน>", $"{QRCodeFunction.Create(teacher.Code, QRCoder.QRCodeGenerator.ECCLevel.H, 1, false)}");
                        dict.Add("<Barcode รหัสพนักงาน>", $"{BarCodeFunction.Create(teacher.Code, BarCodeFunction.BarcodeTYPE.CODE128, false, BarCodeFunction.Alignment.CENTER, 193, 43)}");

                        ltrCard.Text += @"<div id='card'>";
                        ltrCard.Text += ReplaceVariable(templateHead.sValue + "", dict);
                        ltrCard.Text += ReplaceVariable(templateBody.sValue + "", dict);
                        ltrCard.Text += ReplaceVariable(templateFoot.sValue + "", dict);
                        ltrCard.Text += @"</div>";
                    }

                }
            }
        }

        private object GetStudentStatus(int? nStudentStatus)
        {

            switch (nStudentStatus)
            {

                case 1:
                    return "จำหน่าย";
                case 2:
                    return "ลาออก";
                case 3:
                    return "พักการเรียน";
                case 4:
                    return "สำเร็จการศึกษา";
                case 5:
                    return "ขาดการติดต่อ";
                case 6:
                    return "พ้นสภาพ";
                case 7:
                    return "นักเรียนไปโครงการ";

                case 0:
                default:
                    return "กำลังศึกษา";
            }

        }

        private string ReplaceVariable(string value, Dictionary<string, string> dict)
        {
            if (string.IsNullOrEmpty(value)) return "";

            var html = value.Replace("&quot;", "'");
            html = HttpUtility.HtmlDecode(html);

            foreach (var d in dict)
            {
                html = html.Replace(d.Key, d.Value);
            }

            return html;
        }


        public static string AddDash(string Identification)
        {
            var citizencard = "";

            if (Identification.Trim().Length == 13)
            {
                citizencard = Identification.Trim();
                citizencard = citizencard.Insert(1, "-");
                citizencard = citizencard.Insert(6, "-");
                citizencard = citizencard.Insert(12, "-");
                citizencard = citizencard.Insert(15, "-");
            }
            else
            {
                citizencard = Identification;
            }

            return citizencard;
        }

        public static string AddClass(string sClass)
        {
            var Class = "";

            if (sClass == "อ.1" || sClass == "อ.2" || sClass == "อ.3")
                Class = "อนุบาล";
            else if (sClass == "ป.1" || sClass == "ป.2" || sClass == "ป.3")
                Class = "ประถมศึกษาตอนต้น";
            else if (sClass == "ป.4" || sClass == "ป.5" || sClass == "ป.6")
                Class = "ประถมศึกษาตอนปลาย";
            else if (sClass == "ม.1" || sClass == "ม.2" || sClass == "ม.3")
                Class = "มัธยมศึกษาตอนต้น";
            else if (sClass == "ม.4" || sClass == "ม.5" || sClass == "ม.6")
                Class = "มัธยมศึกษาตอนปลาย";
            else if (sClass == "ปวช. 1" || sClass == "ปวช. 2" || sClass == "ปวช. 3")
                Class = "ประกาศนียบัตรวิชาชีพ";
            else if (sClass == "ปวส. 1" || sClass == "ปวส. 2")
                Class = "ประกาศนียบัตรวิชาชีพชั้นสูง";
            else Class = sClass;

            return Class;
        }

        public static string AddClass2(string sClass)
        {
            var Class = "";

            if (sClass == "อ.1" || sClass == "อ.2" || sClass == "อ.3")
                Class = "อนุบาล";
            else if (sClass == "ป.1" || sClass == "ป.2" || sClass == "ป.3")
                Class = "ประถมศึกษา";
            else if (sClass == "ป.4" || sClass == "ป.5" || sClass == "ป.6")
                Class = "ประถมศึกษา";
            else if (sClass == "ม.1" || sClass == "ม.2" || sClass == "ม.3")
                Class = "มัธยมศึกษาตอนต้น";
            else if (sClass == "ม.4" || sClass == "ม.5" || sClass == "ม.6")
                Class = "มัธยมศึกษาตอนปลาย";
            else if (sClass == "ปวช. 1" || sClass == "ปวช. 2" || sClass == "ปวช. 3")
                Class = "ประกาศนียบัตรวิชาชีพ";
            else if (sClass == "ปวส. 1" || sClass == "ปวส. 2")
                Class = "ประกาศนียบัตรวิชาชีพชั้นสูง";
            else Class = sClass;

            return Class;
        }

    }
}