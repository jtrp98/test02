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

namespace FingerprintPayment.studentCard
{
    public partial class studentCardPrintiframe2 : System.Web.UI.Page
    {
        public int schoolID { get; set; }
        public string BackgroundCard { get; set; } = "";
        public float Height { get; set; } = 128f;
        public float Width { get; set; } = 82f;
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
                int idn = int.Parse(id);
                string term = Request.QueryString["term"];

                var Q_title = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();
                var company = dbmaster.TCompanies.FirstOrDefault(o => o.nCompany == schoolID);
                var studentView = _db.TB_StudentViews.Where(o => o.SchoolID == schoolID && o.sID == idn && o.nTerm == term).FirstOrDefault();
                var userValue = _db.TUser.Where(w => w.SchoolID == schoolID && w.sID == idn).FirstOrDefault();
                var subLevel = _db.TSubLevels
                    .Where(w => w.SchoolID == schoolID && w.nTSubLevel == studentView.nTSubLevel)
                    .FirstOrDefault();
                var level = _db.TLevels
                    .Where(w => w.SchoolID == schoolID && w.LevelID == subLevel.nTLevel)
                    .FirstOrDefault();
                var healtProfile = _db.TStudentHealthInfoes
                  .Where(w => w.SchoolID == schoolID && w.sID == idn)
                  .FirstOrDefault();
                var familyProfile = _db.TFamilyProfiles
                 .Where(w => w.SchoolID == schoolID && w.sID == idn)
                 .FirstOrDefault();
                var cardInfo = _db.TStudentCardInfoes.Where(w => w.SchoolID == schoolID).ToList();

                var cardType = cardInfo.Where(w => w.elementName == "NewCardType").FirstOrDefault();
                var card = cardInfo.Where(w => w.elementName == "CardCover").FirstOrDefault();
                var printDate = cardInfo.Where(w => w.elementName == "CardPrintDate").FirstOrDefault();
                var enddate = cardInfo.Where(w => w.elementName == "CardExpireDate").FirstOrDefault();

                if (card != null && card.elementValue != "")
                {
                    BackgroundCard = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm"));
                }

                if (printDate != null && printDate.date.HasValue)
                {
                    //if (printDate.elementValue != "")
                    PrintDate = printDate.date?.ToString("dd/MM/yy", new CultureInfo("th-TH"));
                }

                if (enddate != null && enddate.date.HasValue)
                {
                    //if (enddate.elementValue != "")
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

                    var dict = new Dictionary<string, string>();
                    var imgProfile = "";
                    if (!string.IsNullOrEmpty(userValue.sStudentPicture))
                    {
                        if (userValue.sStudentPicture.Contains("?x-image-process=image/resize,m_fill,h_300,w_270"))
                        {
                            imgProfile = userValue.sStudentPicture + "&d=" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                        }
                        else
                        {
                            imgProfile = userValue.sStudentPicture + "?d=" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                        }
                    }
                    else
                    {
                        imgProfile = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";
                    }
                    dict.Add("<ชื่อโรงเรียน>", $"{company.sCompany}");
                    dict.Add("<ชื่อโรงเรียนEng>", $"{company.sNameEN}");
                    dict.Add("<โลโก้โรงเรียน>", $"{company.sImage}");
                    dict.Add("<เทอม>", $"{studentView.sTerm}");
                    dict.Add("<ปีการศึกษา>", $"{studentView.numberYear}");
                    dict.Add("<ระดับชั้น(เต็มไม่ระบุต้น-ปลาย)>", level.LevelName);
                    dict.Add("<ระดับชั้น(เต็มระบุต้น-ปลาย)>", subLevel.SubLevelNameTH);
                    dict.Add("<ระดับชั้น(ย่อ)>", subLevel.SubLevel);
                    dict.Add("<ห้อง>", $"{studentView.SubLevel}/{studentView.nTSubLevel2}");
                    dict.Add("<รูปนักเรียน>", $"{imgProfile}");
                    dict.Add("<คำนำหน้านักเรียน>", $"{Common.geTitelName(Q_title, userValue.sStudentTitle)}");
                    //dict.Add("<คำนำหน้า(ร)>", $"{Common.geTitelName(Q_title, userValue.sStudentTitle)}");
                    dict.Add("<ชื่อนักเรียน>", $"{userValue.sName}");
                    dict.Add("<นามสกุลนักเรียน>", $"{userValue.sLastname}");
                    dict.Add("<ชื่อ-สกุลนักเรียน>", Common.geTitelName(Q_title, userValue.sStudentTitle) + userValue.sName + " " + userValue.sLastname);
                    dict.Add("<ชื่อ-สกุลนักเรียนEng>", Common.geTitelNameEN(Q_title, userValue.sStudentTitle) + userValue.sStudentNameEN + " " + userValue.sStudentLastEN);
                    dict.Add("<ชื่อเล่นนักเรียน>", $"{userValue.sNickName}");
                    dict.Add("<ชื่อเล่นนักเรียนEng>", $"{userValue.sNickNameEN}");
                    dict.Add("<รหัสนักเรียน>", userValue.sStudentID);
                    dict.Add("<สถานะการศึกษา>", $"{GetStudentStatus(studentView.nStudentStatus)}");
                    dict.Add("<IDCardนักเรียน>", $"{userValue.sIdentification}");
                    dict.Add("<วันเกิดนักเรียน>", $"{userValue.dBirth?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))}");
                    dict.Add("<เบอร์โทรนักเรียน>", $"{userValue.sPhone}");
                    dict.Add("<กรุ๊ปเลือด>", $"{healtProfile?.sBlood}");
                    dict.Add("<ประเภทการเดินทาง>", $"{(userValue.JourneyType == 1 ? "ไปกลับ" : "ประจำ")}");
                    dict.Add("<ชื่อ-นามสกุลบิดา>", $"{familyProfile?.sFatherFirstName} {familyProfile?.sFatherLastName}");
                    dict.Add("<ชื่อ-นามสกุลมารดา>", $"{familyProfile?.sMotherFirstName} {familyProfile?.sMotherLastName}");
                    dict.Add("<วันที่ออกบัตร>", PrintDate);
                    dict.Add("<วันที่หมดอายุบัตร>", ExpireDate);
                    dict.Add("<QRCodeรหัสนักเรียน>", $"{QRCodeFunction.Create(userValue.sStudentID, QRCoder.QRCodeGenerator.ECCLevel.H, 1, false)}");
                    dict.Add("<Barcodeรหัสนักเรียน>", $"{BarCodeFunction.Create(userValue.sStudentID, BarCodeFunction.BarcodeTYPE.CODE128, false, BarCodeFunction.Alignment.CENTER, 193, 43)}");

                    var templateHead = context.TblReportTemplate.FirstOrDefault(o => o.nTemplateID == form.nHeaderTemplateID);
                    var templateBody = context.TblReportTemplate.FirstOrDefault(o => o.nTemplateID == form.nBodyTemplateID);
                    var templateFoot = context.TblReportTemplate.FirstOrDefault(o => o.nTemplateID == form.nFooterTemplateID);

                    ltrCard.Text = ReplaceVariable(templateHead.sValue + "", dict);
                    ltrCard.Text += ReplaceVariable(templateBody.sValue + "", dict);
                    ltrCard.Text += ReplaceVariable(templateFoot.sValue + "", dict);
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