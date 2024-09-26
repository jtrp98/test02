using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment.studentCard
{
    public partial class studentCardPrintiframe : System.Web.UI.Page
    {
        public int schoolID { get; set; }
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
                string sEntities = Session["sEntities"].ToString();
                string modeall = Request.QueryString["modeall"];

                string id = Request.QueryString["id"];
                int idn = int.Parse(id);

                string idlv = Request.QueryString["idlv"];
                int sublevel = int.Parse(idlv);

                string idlv2 = Request.QueryString["idlv2"];
                int className = int.Parse(idlv2);

                string term = Request.QueryString["term"];

                //textwordEng
                string wE = Request.QueryString["wE"];
                //typeCard
                //string tC = Request.QueryString["tC"];
                //typeBarQue
                string tBQ = Request.QueryString["tBQ"];
                //typeClassRoom
                string tCL = Request.QueryString["tCL"];
                //typeExpDate
                string tEXP = Request.QueryString["tEXP"];
                //typeNickName
                string tNik = Request.QueryString["tNik"];

                //typecolorNickName
                //string colorNik = Request.QueryString["colorNik"];

                var Q_title = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                string sqlCondition = "";

                if (!string.IsNullOrEmpty(idn.ToString())) { sqlCondition += string.Format(@" AND a.sid = {0}", idn); }
                if (!string.IsNullOrEmpty(term)) { sqlCondition += string.Format(@" AND a.nTerm = '{0}'", term); }
                if (!string.IsNullOrEmpty(className.ToString())) { sqlCondition += string.Format(@" AND a.nTermSubLevel2 = {0}", className); }


                string sqlQueryFilter = string.Format(@"
SELECT *
FROM TB_StudentViews a
WHERE a.cDel IS NULL AND ISNULL(a.nStudentStatus, 0) = 0 AND a.SchoolID = {0} {1}
", schoolID, sqlCondition);


                List<TB_StudentViews> tB_StudentViews = _db.Database.SqlQuery<TB_StudentViews>(sqlQueryFilter).ToList();

                var user = tB_StudentViews.FirstOrDefault();

                var TUserList = _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.sID == user.sID).FirstOrDefault();
                var TSubLevel = _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == user.nTSubLevel).FirstOrDefault();
                var TLevel = _db.TLevels.Where(w => w.SchoolID == userData.CompanyID && w.LevelID == TSubLevel.nTLevel).FirstOrDefault();

                var NAMETH = Common.geTitelName(Q_title, TUserList.sStudentTitle) + TUserList.sName;
                var LASTNAMETH = TUserList.sLastname;

                var NAMEENG = TUserList.sStudentNameEN;
                var LASTNAMEENG = TUserList.sStudentLastEN;

                var FULLNAMETH = Common.geTitelName(Q_title, TUserList.sStudentTitle) + TUserList.sName + " " + TUserList.sLastname;
                var FULLNAMEEN = Common.geTitelNameEN(Q_title, TUserList.sStudentTitle) + TUserList.sStudentNameEN + " " + TUserList.sStudentLastEN;

                var NICKNAMETH = TUserList.sNickName;
                var NICKNAMEEN = TUserList.sNickNameEN;

                var STUDENTID = TUserList.sStudentID;
                var STUDENTIDENTITY = TUserList.sIdentification;
                var STUDENTPICTURE = TUserList.sStudentPicture;

                int? STUDENTJOURNEY = TUserList.JourneyType;
                var STUDENTDORMITORYName = TUserList.DormitoryName;


                var studentInfoList = _db.TStudentCardInfoes.Where(w => w.SchoolID == userData.CompanyID).ToList();

                string tC = studentInfoList.Where(w => w.elementName == "CardType").Select(o => o.elementValue).FirstOrDefault();

                var educationType = studentInfoList.Where(w => w.elementName == "EducationType").FirstOrDefault();
                if (educationType != null)
                {
                    tCL = educationType.elementValue;
                }

                if (tC == "5")
                {
                    mainPage1.Attributes["hidden"] = "hidden";
                    mainPageTC6.Attributes["hidden"] = "hidden";
                    mainPageTC7.Attributes["hidden"] = "hidden";
                    mainPageTC8.Attributes["hidden"] = "hidden";
                    mainPageTC9.Attributes["hidden"] = "hidden";
                    mainPageTC10.Attributes["hidden"] = "hidden";
                    mainPageTC11.Attributes["hidden"] = "hidden";
                    mainPageTC12.Attributes["hidden"] = "hidden";
                    mainPageTC13.Attributes["hidden"] = "hidden";
                    mainPageTC15.Attributes["hidden"] = "hidden";
                    mainPageTC16.Attributes["hidden"] = "hidden";
                    if (modeall == "1")
                        printbutton.Attributes["class"] = "example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall hidden";


                    var card = studentInfoList.Where(w => w.elementName == "CardCover").FirstOrDefault();
                    if (card != null)
                    {
                        if (card.elementValue == "")
                            BG5.Attributes["style"] = "";
                        else
                            BG5.Src = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm")); ;
                    }
                    else BG5.Attributes["style"] = "";


                    if (TUserList.sStudentPicture != null && TUserList.sStudentPicture != "")
                        ImgProfileTC5.Src = PictureUrl(TUserList);
                    else ImgProfileTC5.Src = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";

                    var ProfileTopAndBottom = 0;
                    var ProfileLeftAndRight = 0;

                    var Ptopbotton = studentInfoList.Where(w => w.elementName == "PicProfileUpDown").FirstOrDefault();
                    if (Ptopbotton != null)
                        ProfileTopAndBottom = int.Parse(Ptopbotton.elementValue);

                    var Pleftright = studentInfoList.Where(w => w.elementName == "PicProfileLeftRight").FirstOrDefault();
                    if (Pleftright != null)
                        ProfileLeftAndRight = int.Parse(Pleftright.elementValue);

                    MoveImgProfileTC5.Attributes["style"] = "top: " + ProfileTopAndBottom + "px; left: " + ProfileLeftAndRight + "px; ";

                    if (FULLNAMETH.Length <= 29)
                    {
                        FullNameTC5.InnerHtml = FULLNAMETH;
                        FullNameTC5.Attributes["style"] = "font-size: 22px;";
                    }
                    else
                    {
                        FullNameTC5.InnerHtml = FULLNAMETH;
                        FullNameTC5.Attributes["style"] = "font-size: 18px;";
                    }

                    var PatternContent = studentInfoList
                        .Where(w => w.elementName == "PatternContent")
                        .FirstOrDefault();

                    var pidPosition = studentInfoList
                       .Where(w => w.elementName == "PIDPosition")
                       .Select(o => o.elementValue)
                       .FirstOrDefault();

                    if (PatternContent != null)
                    {
                        if (PatternContent.elementValue == "0")
                        {
                            tagDivNameTC5.Attributes["style"] = "padding-left: 95px; height: 27px;";
                            tagDivClassTC5.Attributes["style"] = "padding-left: 95px; height: 28px;";
                            tagDivStudentIDTC5.Attributes["style"] = "padding-left: 95px; height: 28px;" + (string.IsNullOrEmpty(pidPosition) ? "" : $"margin-left:{pidPosition}px !important;");
                        }
                        else if (PatternContent.elementValue == "1")
                        {
                            tagDivNameTC5.Attributes["style"] = "text-align: center; height: 27px;";
                            tagDivClassTC5.Attributes["style"] = "text-align: center; height: 28px;";
                            tagDivStudentIDTC5.Attributes["style"] = "text-align: center; height: 28px;" + (string.IsNullOrEmpty(pidPosition) ? "" : $"margin-left:{pidPosition}px !important;");
                        }
                        else if (PatternContent.elementValue == "2")
                        {
                            tagDivNameTC5.Attributes["style"] = "text-align: right; height: 27px;";
                            tagDivClassTC5.Attributes["style"] = "text-align: right; height: 28px;";
                            tagDivStudentIDTC5.Attributes["style"] = "text-align: right; height: 28px;" + (string.IsNullOrEmpty(pidPosition) ? "" : $"margin-left:{pidPosition}px !important;");
                        }
                    }

                    var PIDDisplay = studentInfoList.Where(w => w.elementName == "PIDDisplay").FirstOrDefault();

                    if (PIDDisplay == null || PIDDisplay?.elementValue == "0")
                    {
                        MainidTC5.Visible = true;
                    }
                    else
                    {
                        MainidTC5.Visible = false;
                    }

                    var TopAndBottom = 0;
                    var topbottom = studentInfoList.Where(w => w.elementName == "ContentAlignmentTopBottom").FirstOrDefault();
                    if (topbottom != null)
                        TopAndBottom = topbottom.elementValue == null ? 0 : int.Parse(topbottom.elementValue);

                    var LeftAndRight = 0;
                    var leftright = studentInfoList.Where(w => w.elementName == "ContentAlignmentLeftRight").FirstOrDefault();
                    if (leftright != null)
                        LeftAndRight = leftright.elementValue == null ? 0 : int.Parse(leftright.elementValue);

                    MoveContentTC5.Attributes["style"] = "top: " + TopAndBottom + "px; margin-left: " + LeftAndRight + "px; ";

                    if (tCL == "0")
                        ClassTC5.InnerText = TSubLevel.SubLevelNameTH;
                    else if (tCL == "1")
                        ClassTC5.InnerText = TLevel.LevelName;
                    else if (tCL == "2")
                        ClassTC5.InnerText = TLevel.LevelNameEng?.Replace("School", "");
                    else if (tCL == "3")
                        ClassTC5.InnerText = "";


                    MainidTC5.InnerText = (TUserList.sIdentification == null || TUserList.sIdentification == string.Empty) ? "ไม่พบข้อมูล" : AddDash(TUserList.sIdentification);

                    string code = string.Format(TUserList.sStudentID);
                    string imgBase655 = QRCodeFunction.Create(code, QRCoder.QRCodeGenerator.ECCLevel.H, 1, false);
                    QR5.Src = imgBase655;


                    var BarcodeQrTopBottom = 0;
                    var positionTopBottom = studentInfoList.Where(w => w.elementName == "BarcodeQRTopBottom").FirstOrDefault();
                    if (positionTopBottom != null)
                        BarcodeQrTopBottom = int.Parse(positionTopBottom.elementValue);

                    var BarcodeQrLeftRight = 0;
                    var positionLeftRigh = studentInfoList.Where(w => w.elementName == "BarcodeQRLeftRight").FirstOrDefault();
                    if (positionLeftRigh != null)
                        BarcodeQrLeftRight = int.Parse(positionLeftRigh.elementValue);

                    MoveBarcodeQRTC5.Attributes["style"] = "top:" + BarcodeQrTopBottom + "px; left: " + BarcodeQrLeftRight + "px; ";

                    StudentidTC5.InnerText = TUserList.sStudentID;


                }
                else if (tC == "6")
                {
                    mainPage1.Attributes["hidden"] = "hidden";
                    mainPageTC5.Attributes["hidden"] = "hidden";
                    mainPageTC7.Attributes["hidden"] = "hidden";
                    mainPageTC8.Attributes["hidden"] = "hidden";
                    mainPageTC9.Attributes["hidden"] = "hidden";
                    mainPageTC10.Attributes["hidden"] = "hidden";
                    mainPageTC11.Attributes["hidden"] = "hidden";
                    mainPageTC12.Attributes["hidden"] = "hidden";
                    mainPageTC13.Attributes["hidden"] = "hidden";
                    mainPageTC15.Attributes["hidden"] = "hidden";
                    mainPageTC16.Attributes["hidden"] = "hidden";

                    var card = studentInfoList.Where(w => w.elementName == "CardCover").FirstOrDefault();
                    if (card != null)
                    {
                        if (card.elementValue == "")
                            BG3.Attributes["style"] = "";
                        else
                            BG3.Src = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm")); ;
                    }
                    else BG3.Attributes["style"] = "";


                    if (STUDENTPICTURE != null && STUDENTPICTURE != "")
                        ImgProfile3.Src = PictureUrl(TUserList);
                    else ImgProfile3.Src = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";


                    studentID3.InnerText = STUDENTID;
                    FullName3.InnerText = FULLNAMETH;
                    FullNameEng3.InnerText = FULLNAMEEN;

                    if (FULLNAMETH.Length <= 26)
                        FullName3.Attributes["style"] = "font-size: 170%";
                    else
                        FullName3.Attributes["style"] = "font-size: 140%";


                    if (FULLNAMEEN.Length <= 26)
                        FullNameEng3.Attributes["style"] = "font-size: 150%";
                    else
                        FullNameEng3.Attributes["style"] = "font-size: 130%";


                    MainId3.InnerText = AddDash(STUDENTIDENTITY);


                    string code = string.Format(TUserList.sStudentID);
                    //string imgBase655 = QRCodeFunction.Create(code, QRCoder.QRCodeGenerator.ECCLevel.H);
                    string imgBase655 = QRCodeFunction.Create(code, QRCoder.QRCodeGenerator.ECCLevel.H, 1, false);
                    QR3.Src = imgBase655;

                    if (modeall == "1")
                        printbutton.Attributes["class"] = "example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall hidden";

                }
                else if (tC == "7")
                {
                    mainPage1.Attributes["hidden"] = "hidden";
                    mainPageTC5.Attributes["hidden"] = "hidden";
                    mainPageTC6.Attributes["hidden"] = "hidden";
                    mainPageTC8.Attributes["hidden"] = "hidden";
                    mainPageTC9.Attributes["hidden"] = "hidden";
                    mainPageTC10.Attributes["hidden"] = "hidden";
                    mainPageTC11.Attributes["hidden"] = "hidden";
                    mainPageTC12.Attributes["hidden"] = "hidden";
                    mainPageTC13.Attributes["hidden"] = "hidden";
                    mainPageTC15.Attributes["hidden"] = "hidden";
                    mainPageTC16.Attributes["hidden"] = "hidden";

                    var card = studentInfoList.Where(w => w.elementName == "CardCover").FirstOrDefault();
                    if (card != null)
                    {
                        if (card.elementValue == "")
                            BG7.Attributes["style"] = "";
                        else
                            BG7.Src = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm")); ;
                    }
                    else BG7.Attributes["style"] = "";

                    var ProfileTopAndBottom = 0;
                    var ProfileLeftAndRight = 0;
                    var ContentTopAndBottom = 0;
                    var ContentLeftAndRight = 0;

                    var Ctopbottom = studentInfoList.Where(w => w.elementName == "ContentAlignmentTopBottom").FirstOrDefault();
                    if (Ctopbottom != null)
                        ContentTopAndBottom = Ctopbottom.elementValue == null ? 0 : int.Parse(Ctopbottom.elementValue);

                    var Cleftright = studentInfoList.Where(w => w.elementName == "ContentAlignmentLeftRight").FirstOrDefault();
                    if (Cleftright != null)
                        ContentLeftAndRight = Cleftright.elementValue == null ? 0 : int.Parse(Cleftright.elementValue);

                    int ContentTopDown = (3 + (ContentTopAndBottom));
                    int ContentLeftRight = (0 + (ContentLeftAndRight));
                    MoveConTentTC7.Attributes["style"] = "top: " + ContentTopDown + "px; margin-left: " + ContentLeftRight + "px; ";

                    if (STUDENTPICTURE != null && STUDENTPICTURE != "")
                        ImgProfileTC7.Src = PictureUrl(TUserList);
                    else ImgProfileTC7.Src = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";

                    var Ptopbotton = studentInfoList.Where(w => w.elementName == "PicProfileUpDown").FirstOrDefault();
                    if (Ptopbotton != null)
                        ProfileTopAndBottom = int.Parse(Ptopbotton.elementValue);

                    var Pleftright = studentInfoList.Where(w => w.elementName == "PicProfileLeftRight").FirstOrDefault();
                    if (Pleftright != null)
                        ProfileLeftAndRight = int.Parse(Pleftright.elementValue);

                    MoveImgProfileTC7.Attributes["style"] = "top: " + ProfileTopAndBottom + "px; left: " + ProfileLeftAndRight + "px; ";


                    if (FULLNAMETH.Length <= 28)
                    {
                        FullNameTC7.InnerText = FULLNAMETH;
                        FullNameTC7.Attributes["style"] = "font-size: 130%;";
                    }
                    else
                    {
                        FullNameTC7.InnerText = FULLNAMETH;
                        FullNameTC7.Attributes["style"] = "font-size: 120%;";
                    }

                    FullNameEngTC7.InnerText = FULLNAMEEN;
                    FullNameEngTC7.Attributes["style"] = "font-size: 130%;";

                    if (wE != "0")
                    {
                        DivHidNameEng7.Attributes["hidden"] = "hidden";
                    }


                    if (tCL == "0")
                        ClassTC7.InnerText = TSubLevel.SubLevelNameTH;
                    else if (tCL == "1")
                        ClassTC7.InnerText = TLevel.LevelName;
                    else if (tCL == "2")
                        ClassTC7.InnerText = TLevel.LevelNameEng.Replace("School", "");
                    else if (tCL == "3")
                        ClassTC7.InnerText = "";


                    MainIdTC7.InnerText = AddDash(STUDENTIDENTITY);
                    StudentIdTC7.InnerText = STUDENTID;

                    if (modeall == "1")
                        printbutton.Attributes["class"] = "example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall hidden";

                }
                else if (tC == "8")
                {
                    mainPage1.Attributes["hidden"] = "hidden";
                    mainPageTC5.Attributes["hidden"] = "hidden";
                    mainPageTC6.Attributes["hidden"] = "hidden";
                    mainPageTC7.Attributes["hidden"] = "hidden";
                    mainPageTC9.Attributes["hidden"] = "hidden";
                    mainPageTC10.Attributes["hidden"] = "hidden";
                    mainPageTC11.Attributes["hidden"] = "hidden";
                    mainPageTC12.Attributes["hidden"] = "hidden";
                    mainPageTC13.Attributes["hidden"] = "hidden";
                    mainPageTC15.Attributes["hidden"] = "hidden";
                    mainPageTC16.Attributes["hidden"] = "hidden";

                    if (modeall == "1")
                        printbutton.Attributes["class"] = "example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall hidden";

                    if (STUDENTPICTURE != null && STUDENTPICTURE != "")
                        IMGProfile8.Src = PictureUrl(TUserList);
                    else IMGProfile8.Src = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";

                    var card = studentInfoList.Where(w => w.elementName == "CardCover").FirstOrDefault();
                    if (card != null)
                    {
                        if (card.elementValue == "")
                            BG8.Attributes["style"] = "";
                        else
                            BG8.Src = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm")); ;
                        BG8.Attributes["style"] = "position: fixed; height: 55mm;";
                    }
                    else BG8.Attributes["style"] = "";


                    var UpDown = 0;
                    var LeftRight = 0;

                    var PositionUpDown = studentInfoList.Where(w => w.elementName == "PicProfileUpDown").FirstOrDefault();
                    if (PositionUpDown != null)
                        UpDown = int.Parse(PositionUpDown.elementValue);
                    var PositionLeftRight = studentInfoList.Where(w => w.elementName == "PicProfileLeftRight").FirstOrDefault();
                    if (PositionLeftRight != null)
                        LeftRight = int.Parse(PositionLeftRight.elementValue);

                    int PicUpDown = (0 + (UpDown));
                    int PicLeftRight = (13 + (LeftRight));
                    IMGProfile8.Attributes["style"] = "margin-left: " + PicLeftRight + "px; display:block; width:80px; height:101px; margin-top: " + PicUpDown + "px;";


                    if (wE == "0")
                    {

                        NameTH8.Text = NAMETH;
                        NameTH8.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 12px;";
                        LastNameTH8.Text = LASTNAMETH;
                        LastNameTH8.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 12px;";

                        NameENG8.Text = NAMEENG;
                        NameENG8.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 16px;";
                        LastNameENG8.Text = LASTNAMEENG;
                        LastNameENG8.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 12px;";

                    }
                    else if (wE == "1")
                    {

                        NameTH8.Text = NAMETH;
                        NameTH8.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 12px;";
                        LastNameTH8.Text = LASTNAMETH;
                        LastNameTH8.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 12px;";

                        NameENG8.Text = "";
                        NameENG8.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 16px;";
                        LastNameENG8.Text = "";
                        LastNameENG8.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 12px;";

                    }
                    else if (wE == "2")
                    {

                        NameTH8.Text = NAMEENG;
                        NameTH8.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 12px;";
                        LastNameTH8.Text = LASTNAMEENG;
                        LastNameTH8.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 12px;";

                        NameENG8.Text = "";
                        NameENG8.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 16px;";
                        LastNameENG8.Text = "";
                        LastNameENG8.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 12px;";

                    }



                    StudentID8.Text = STUDENTID;
                    StudentID8.Attributes["style"] = "margin-left: 95px; font-size: 90%; font-weight: bold;";

                    DateTime now = DateTime.Now;
                    string month = now.Month.ToString();
                    if (month.Length == 1)
                        month = "0" + month;
                    string year = now.AddYears(543).Year.ToString();
                    year = year.Remove(0, 2);
                    string today = now.Day.ToString();
                    if (today.Length == 1)
                        today = "0" + today;

                    DateTimeNow8.Text = today + "/" + month + "/" + year;
                    DateTimeNow8.Attributes["style"] = "margin-left: 45px; font-size: 90%; font-weight: bold;";

                    var BarcodeUpDown = 0;
                    var Barcode = BarCodeFunction.Create(STUDENTID, BarCodeFunction.BarcodeTYPE.CODE128, false, BarCodeFunction.Alignment.CENTER, 193, 43);

                    var positionTop = studentInfoList.Where(w => w.elementName == "BarcodeQRTopBottom").FirstOrDefault();
                    if (positionTop != null)
                        BarcodeUpDown = int.Parse(positionTop.elementValue);

                    BarCode8.Src = Barcode;
                    BarCode8.Attributes["style"] = "margin-left: 23px; display: block; width: 4.1cm; height: 40px; margin-top: " + BarcodeUpDown + "px;";

                }
                else if (tC == "9")
                {
                    mainPage1.Attributes["hidden"] = "hidden";
                    mainPageTC5.Attributes["hidden"] = "hidden";
                    mainPageTC6.Attributes["hidden"] = "hidden";
                    mainPageTC7.Attributes["hidden"] = "hidden";
                    mainPageTC8.Attributes["hidden"] = "hidden";
                    mainPageTC10.Attributes["hidden"] = "hidden";
                    mainPageTC11.Attributes["hidden"] = "hidden";
                    mainPageTC12.Attributes["hidden"] = "hidden";
                    mainPageTC13.Attributes["hidden"] = "hidden";
                    mainPageTC15.Attributes["hidden"] = "hidden";
                    mainPageTC16.Attributes["hidden"] = "hidden";

                    if (modeall == "1")
                        printbutton.Attributes["class"] = "example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall hidden";


                    //1=กลับบ้าน, 2=อยู่หอ
                    if (STUDENTJOURNEY == 2)
                    {
                        var card = studentInfoList
                            .Where(w => w.elementName == "CardCoverSchoolT2")
                            .FirstOrDefault();
                        if (card != null)
                        {
                            if (card.elementValue == "")
                                BG9.Attributes["style"] = "";
                            BG9.Src = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm")); ;
                        }

                        var jType = studentInfoList
                         .Where(w => w.elementName == ("JourneyType2"))
                         .FirstOrDefault();

                        if (jType != null && jType.elementValue == "1")
                        {
                            JournneyTypeTC9.InnerText = STUDENTDORMITORYName;
                        }
                    }
                    else
                    {
                        var card = studentInfoList
                       .Where(w => w.elementName == "CardCoverSchoolT1")
                       .FirstOrDefault();
                        if (card != null)
                        {
                            if (card.elementValue == "")
                                BG9.Attributes["style"] = "";
                            BG9.Src = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm")); ;
                        }

                        var jType = studentInfoList
                        .Where(w => w.elementName == ("JourneyType1"))
                        .FirstOrDefault();

                        if (jType != null && jType.elementValue == "1")
                        {
                            JournneyTypeTC9.InnerText = "ไป - กลับ";
                        }
                    }

                    var UpDown = 0;
                    var LeftRight = 0;
                    var PositionUpDown = studentInfoList.Where(w => w.elementName == "PicProfileUpDown").FirstOrDefault();
                    if (PositionUpDown != null)
                        UpDown = int.Parse(PositionUpDown.elementValue);

                    var PositionLeftRight = studentInfoList.Where(w => w.elementName == "PicProfileLeftRight").FirstOrDefault();
                    if (PositionLeftRight != null)
                        LeftRight = int.Parse(PositionLeftRight.elementValue);
                    MoveImgProfileTC9.Attributes["style"] = "margin-left: " + LeftRight + "px; margin-top: " + UpDown + "px;";


                    if (STUDENTPICTURE != null && STUDENTPICTURE != "")
                        ImgProfileTC9.Src = PictureUrl(TUserList);
                    else ImgProfileTC9.Src = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";


                    var BarcodeUpDown = 0;
                    var positionTop = studentInfoList.Where(w => w.elementName == "BarcodeQRTopBottom").FirstOrDefault();
                    if (positionTop != null)
                        BarcodeUpDown = int.Parse(positionTop.elementValue);
                    MoveBarcodeTC9.Attributes["style"] = "margin-top:" + BarcodeUpDown + "px;";


                    var PatternContent = studentInfoList.Where(w => w.elementName == "PatternContent").FirstOrDefault();
                    if (PatternContent != null)
                    {
                        if (PatternContent.elementValue == "0")
                        {
                            tagDivNameTC9.Attributes["style"] = "text-align: left; height: 28px; padding-left: 86px; top: 2px;";
                            tagDivNameENTC9.Attributes["style"] = "text-align: left; height: 28px; padding-left: 86px; top: 2px;";
                            tagDivClassTC9.Attributes["style"] = "text-align: left; height: 27px; padding-left: 50px; top: 2px;";
                            tagDivStudentIDTC9.Attributes["style"] = "text-align: left; height: 27px; padding-left: 106px; top: 4px;";
                        }
                        else if (PatternContent.elementValue == "1")
                        {
                            tagDivNameTC9.Attributes["style"] = "text-align: center; height: 28px; top: 2px;";
                            tagDivNameENTC9.Attributes["style"] = "text-align: center; height: 28px; top: 2px;";
                            tagDivClassTC9.Attributes["style"] = "text-align: center; height: 27px; top: 2px;";
                            tagDivStudentIDTC9.Attributes["style"] = "text-align: center; height: 27px; top: 4px;";
                        }
                        else if (PatternContent.elementValue == "2")
                        {
                            tagDivNameTC9.Attributes["style"] = "text-align: right; height: 28px; top: 2px;";
                            tagDivNameENTC9.Attributes["style"] = "text-align: right; height: 28px; top: 2px;";
                            tagDivClassTC9.Attributes["style"] = "text-align: right; height: 27px; top: 2px;";
                            tagDivStudentIDTC9.Attributes["style"] = "text-align: right; height: 27px; top: 4px;";
                        }
                    }

                    var showName = studentInfoList.Where(w => w.elementName == "ShowName").Select(o => o.elementValue).FirstOrDefault() + "";
                    switch (showName)
                    {
                        case "0":
                            tagDivNameTC9.Visible = true;
                            tagDivNameENTC9.Visible = true;
                            break;

                        case "2":
                            tagDivNameTC9.Visible = false;
                            break;

                        default:
                            tagDivNameENTC9.Visible = false;
                            break;
                    }

                    FullNameTC9.InnerText = FULLNAMETH;
                    FullNameENTC9.InnerText = FULLNAMEEN;

                    //if (FULLNAMETH.Length <= 28)
                    //{
                    //    FullNameTC9.InnerText = FULLNAMETH;
                    //    FullNameTC9.Attributes["style"] = "font-size: 130%;";
                    //}
                    //else if (FULLNAMETH.Length < 35)
                    //{
                    //    FullNameTC9.InnerText = FULLNAMETH;
                    //    FullNameTC9.Attributes["style"] = "font-size: 124%;";
                    //}
                    //else if (FULLNAMETH.Length < 42)
                    //{
                    //    FullNameTC9.InnerText = FULLNAMETH;
                    //    FullNameTC9.Attributes["style"] = "font-size: 116%;";
                    //}
                    //else
                    //{
                    //    FullNameTC9.InnerText = FULLNAMETH;
                    //    FullNameTC9.Attributes["style"] = "font-size: 110%;";
                    //}

                    if (tCL == "0")
                        ClassTC9.InnerText = TSubLevel.SubLevelNameTH;
                    else if (tCL == "1")
                        ClassTC9.InnerText = TLevel.LevelName;
                    else if (tCL == "2")
                        ClassTC9.InnerText = TLevel.LevelNameEng.Replace("School", "");
                    else if (tCL == "3")
                        ClassTC9.InnerText = "";


                    StudentIdTC9.InnerText = STUDENTID;

                    var Barcode = BarCodeFunction.Create(STUDENTID, BarCodeFunction.BarcodeTYPE.CODE128, false, BarCodeFunction.Alignment.CENTER, 193, 43);
                    BarCodeTC9.Src = Barcode;

                }
                else if (tC == "10")
                {
                    mainPage1.Attributes["hidden"] = "hidden";
                    mainPageTC5.Attributes["hidden"] = "hidden";
                    mainPageTC6.Attributes["hidden"] = "hidden";
                    mainPageTC7.Attributes["hidden"] = "hidden";
                    mainPageTC8.Attributes["hidden"] = "hidden";
                    mainPageTC9.Attributes["hidden"] = "hidden";
                    mainPageTC11.Attributes["hidden"] = "hidden";
                    mainPageTC12.Attributes["hidden"] = "hidden";
                    mainPageTC13.Attributes["hidden"] = "hidden";
                    mainPageTC15.Attributes["hidden"] = "hidden";
                    mainPageTC16.Attributes["hidden"] = "hidden";

                    if (modeall == "1")
                        printbutton.Attributes["class"] = "example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall hidden";

                    var card = studentInfoList.Where(w => w.elementName == "CardCover").FirstOrDefault();
                    if (card != null)
                    {
                        if (card.elementValue == "")
                            BG10.Attributes["style"] = "";
                        else
                            BG10.Src = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm")); ;
                        BG10.Attributes["style"] = "position: fixed; height: 55mm;";
                    }
                    else BG10.Attributes["style"] = "";


                    NameTH10.Text = NAMETH;
                    NameTH10.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 18px;";

                    LastNameTH10.Text = LASTNAMETH;
                    LastNameTH10.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 18px;";

                    NickNameTH10.Text = NICKNAMETH;
                    NickNameTH10.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 18px;";

                    MainId10.Text = AddDash(STUDENTIDENTITY);
                    MainId10.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 18px;";

                    StudentID10.Text = STUDENTID;
                    StudentID10.Attributes["style"] = "font-size: 90%; font-weight: bold; height: 18px;";


                    var TopAndBottom = 0;
                    var LeftAndRight = 0;
                    var topbottom = studentInfoList.Where(w => w.elementName == "ContentAlignmentTopBottom").FirstOrDefault();
                    if (topbottom != null)
                        TopAndBottom = topbottom.elementValue == null ? 0 : int.Parse(topbottom.elementValue);
                    var leftright = studentInfoList.Where(w => w.elementName == "ContentAlignmentLeftRight").FirstOrDefault();
                    if (leftright != null)
                        LeftAndRight = leftright.elementValue == null ? 0 : int.Parse(leftright.elementValue);

                    MoveContent10.Attributes["style"] = "margin-top: " + TopAndBottom + "px; margin-left: " + LeftAndRight + "px; ";

                }
                else if (tC == "11")
                {
                    mainPage1.Attributes["hidden"] = "hidden";
                    mainPageTC5.Attributes["hidden"] = "hidden";
                    mainPageTC6.Attributes["hidden"] = "hidden";
                    mainPageTC7.Attributes["hidden"] = "hidden";
                    mainPageTC8.Attributes["hidden"] = "hidden";
                    mainPageTC9.Attributes["hidden"] = "hidden";
                    mainPageTC10.Attributes["hidden"] = "hidden";
                    mainPageTC12.Attributes["hidden"] = "hidden";
                    mainPageTC13.Attributes["hidden"] = "hidden";
                    mainPageTC15.Attributes["hidden"] = "hidden";
                    mainPageTC16.Attributes["hidden"] = "hidden";

                    if (modeall == "1")
                        printbutton.Attributes["class"] = "example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall hidden";

                    var card = studentInfoList.Where(w => w.elementName == "CardCover").FirstOrDefault();
                    if (card != null)
                    {
                        if (card.elementValue == "")
                            BG11.Attributes["style"] = "";
                        else
                            BG11.Src = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm")); ;
                    }
                    else BG11.Attributes["style"] = "";

                    var ProfileTopAndBottom = 0;
                    var ProfileLeftAndRight = 0;
                    var ContentTopAndBottom = 0;
                    var ContentLeftAndRight = 0;

                    var Ctopbottom = studentInfoList.Where(w => w.elementName == "ContentAlignmentTopBottom").FirstOrDefault();
                    if (Ctopbottom != null)
                        ContentTopAndBottom = Ctopbottom.elementValue == null ? 0 : int.Parse(Ctopbottom.elementValue);

                    var Cleftright = studentInfoList.Where(w => w.elementName == "ContentAlignmentLeftRight").FirstOrDefault();
                    if (Cleftright != null)
                        ContentLeftAndRight = Cleftright.elementValue == null ? 0 : int.Parse(Cleftright.elementValue);

                    int ContentTopDown = (3 + (ContentTopAndBottom));
                    int ContentLeftRight = (0 + (ContentLeftAndRight));
                    MoveConTentTC11.Attributes["style"] = "top: " + ContentTopDown + "px; margin-left: " + ContentLeftRight + "px; ";


                    if (STUDENTPICTURE != null && STUDENTPICTURE != "")
                        ImgProfileTC11.Src = PictureUrl(TUserList);
                    else ImgProfileTC11.Src = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";

                    var Ptopbotton = studentInfoList.Where(w => w.elementName == "PicProfileUpDown").FirstOrDefault();
                    if (Ptopbotton != null)
                        ProfileTopAndBottom = int.Parse(Ptopbotton.elementValue);

                    var Pleftright = studentInfoList.Where(w => w.elementName == "PicProfileLeftRight").FirstOrDefault();
                    if (Pleftright != null)
                        ProfileLeftAndRight = int.Parse(Pleftright.elementValue);

                    MoveImgProfileTC11.Attributes["style"] = "top: " + ProfileTopAndBottom + "px; left: " + ProfileLeftAndRight + "px; ";

                    if (FULLNAMETH.Length <= 28)
                    {
                        FullNameTC11.InnerText = FULLNAMETH;
                        FullNameTC11.Attributes["style"] = "font-size: 135%;";
                    }
                    else
                    {
                        FullNameTC11.InnerText = FULLNAMETH;
                        FullNameTC11.Attributes["style"] = "font-size: 130%;";
                    }

                    if (tCL == "0")
                        ClassTC11.InnerText = TSubLevel.SubLevelNameTH ?? TLevel.LevelName;
                    else if (tCL == "1")
                        ClassTC11.InnerText = TLevel.LevelName;
                    else if (tCL == "2")
                        ClassTC11.InnerText = TLevel.LevelNameEng.Replace("School", "");
                    else if (tCL == "3")
                        ClassTC11.InnerText = "";

                    var pidDisplay = studentInfoList
                    .Where(w => w.elementName == "PIDDisplay")
                    .Select(o => o.elementValue)
                    .FirstOrDefault();

                    if ((pidDisplay ?? "0") == "0")
                    {
                        divPID11.Visible = true;
                        MainIdTC11.InnerText = AddDash(STUDENTIDENTITY);
                    }
                    else
                    {
                        divPID11.Visible = false;
                    }

                    StudentIdTC11.InnerText = STUDENTID;

                    if (modeall == "1")
                        printbutton.Attributes["class"] = "example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall hidden";

                }
                else if (tC == "12")
                {
                    mainPage1.Attributes["hidden"] = "hidden";
                    mainPageTC5.Attributes["hidden"] = "hidden";
                    mainPageTC6.Attributes["hidden"] = "hidden";
                    mainPageTC7.Attributes["hidden"] = "hidden";
                    mainPageTC8.Attributes["hidden"] = "hidden";
                    mainPageTC9.Attributes["hidden"] = "hidden";
                    mainPageTC10.Attributes["hidden"] = "hidden";
                    mainPageTC11.Attributes["hidden"] = "hidden";
                    mainPageTC13.Attributes["hidden"] = "hidden";
                    mainPageTC15.Attributes["hidden"] = "hidden";
                    mainPageTC16.Attributes["hidden"] = "hidden";

                    var card = studentInfoList.Where(w => w.elementName == "CardCover").FirstOrDefault();
                    if (card != null)
                    {
                        if (card.elementValue == "")
                            BG12.Attributes["style"] = "";
                        else
                            BG12.Src = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm")); ;
                    }
                    else BG12.Attributes["style"] = "";

                    if (STUDENTPICTURE != null && STUDENTPICTURE != "")
                        ImgProfile12.Src = PictureUrl(TUserList);
                    else ImgProfile12.Src = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";

                    studentID12.InnerText = STUDENTID;
                    FullNameTC12.InnerText = FULLNAMETH;

                    if (tCL == "0")
                        ClassTC12.InnerText = TSubLevel.SubLevelNameTH;
                    else if (tCL == "1")
                        ClassTC12.InnerText = TLevel.LevelName;
                    else if (tCL == "2")
                        ClassTC12.InnerText = TLevel.LevelNameEng.Replace("School", "");
                    else if (tCL == "3")
                        ClassTC12.InnerText = "";

                    if (FULLNAMETH.Length <= 26)
                        FullNameTC12.Attributes["style"] = "font-size: 170%";
                    else
                        FullNameTC12.Attributes["style"] = "font-size: 139%";

                    MainId12.InnerText = AddDash(STUDENTIDENTITY);



                    var fontColor = studentInfoList.Where(w => w.elementName == "FontColor").FirstOrDefault();
                    if (fontColor != null)
                    {
                        if (fontColor.elementValue == "White")
                            BodyT12.Attributes["class"] = "col-xs-12 pad0 FwhiteOnly12";
                        else if (fontColor.elementValue == "Black")
                            BodyT12.Attributes["class"] = "col-xs-12 pad0 FblackOnly12";
                        else if (fontColor.elementValue == "Blue")
                            BodyT12.Attributes["class"] = "col-xs-12 pad0 FblueOnly12";
                    }
                    else
                        BodyT12.Attributes["class"] = "col-xs-12 pad0 FblackOnly12";



                    string code = string.Format(TUserList.sStudentID);
                    string imgBase655 = QRCodeFunction.Create(code, QRCoder.QRCodeGenerator.ECCLevel.H, 1, false);
                    //string imgBase655 = QRCodeFunction.Create(code, QRCoder.QRCodeGenerator.ECCLevel.H);
                    QR12.Src = imgBase655;

                    if (modeall == "1")
                        printbutton.Attributes["class"] = "example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall hidden";

                }
                else if (tC == "13")
                {
                    mainPage1.Attributes["hidden"] = "hidden";
                    mainPageTC5.Attributes["hidden"] = "hidden";
                    mainPageTC6.Attributes["hidden"] = "hidden";
                    mainPageTC7.Attributes["hidden"] = "hidden";
                    mainPageTC8.Attributes["hidden"] = "hidden";
                    mainPageTC9.Attributes["hidden"] = "hidden";
                    mainPageTC10.Attributes["hidden"] = "hidden";
                    mainPageTC11.Attributes["hidden"] = "hidden";
                    mainPageTC12.Attributes["hidden"] = "hidden";
                    mainPageTC15.Attributes["hidden"] = "hidden";
                    mainPageTC16.Attributes["hidden"] = "hidden";

                    if (modeall == "1")
                        printbutton.Attributes["class"] = "example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall hidden";


                    var card = studentInfoList.Where(w => w.elementName == "CardCover").FirstOrDefault();
                    if (card != null)
                    {
                        if (card.elementValue == "")
                            BG13.Attributes["style"] = "";
                        else
                            BG13.Src = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm")); ;
                    }
                    else BG13.Attributes["style"] = "";

                    if (TUserList.sStudentPicture != null && TUserList.sStudentPicture != "")
                        ImgProfileTC13.Src = PictureUrl(TUserList);
                    else ImgProfileTC13.Src = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";

                    var ProfileTopAndBottom = 0;
                    var Ptopbotton = studentInfoList.Where(w => w.SchoolID == userData.CompanyID && w.elementName == "PicProfileUpDown").FirstOrDefault();
                    if (Ptopbotton != null)
                        ProfileTopAndBottom = int.Parse(Ptopbotton.elementValue);

                    var ProfileLeftAndRight = 0;
                    var Pleftright = studentInfoList.Where(w => w.SchoolID == userData.CompanyID && w.elementName == "PicProfileLeftRight").FirstOrDefault();
                    if (Pleftright != null)
                        ProfileLeftAndRight = int.Parse(Pleftright.elementValue);

                    MoveImgProfileTC13.Attributes["style"] = "top: " + ProfileTopAndBottom + "px; left: " + ProfileLeftAndRight + "px; ";

                    var TopAndBottom = 0;
                    var topbottom = studentInfoList.Where(w => w.elementName == "ContentAlignmentTopBottom").FirstOrDefault();
                    if (topbottom != null)
                        TopAndBottom = topbottom.elementValue == null ? 0 : int.Parse(topbottom.elementValue);

                    var LeftAndRight = 0;
                    var leftright = studentInfoList.Where(w => w.elementName == "ContentAlignmentLeftRight").FirstOrDefault();
                    if (leftright != null)
                        LeftAndRight = leftright.elementValue == null ? 0 : int.Parse(leftright.elementValue);

                    MoveContentTC13.Attributes["style"] = "top: " + TopAndBottom + "px; margin-left: " + LeftAndRight + "px; ";


                    if (FULLNAMETH.Length <= 29)
                    {
                        FullNameThTC13.InnerHtml = FULLNAMETH;
                        FullNameThTC13.Attributes["style"] = "font-size: 26px;";
                    }
                    else
                    {
                        FullNameThTC13.InnerHtml = FULLNAMETH;
                        FullNameThTC13.Attributes["style"] = "font-size: 22px;";
                    }


                    FullNameEnTC13.InnerText = TUserList.sStudentNameEN + " " + TUserList.sStudentLastEN;
                    FullNameEnTC13.Attributes["style"] = "font-size: 21px;";

                    MainidTC13.InnerText = (TUserList.sIdentification == null || TUserList.sIdentification == string.Empty) ? "ไม่พบข้อมูล" : AddDash(TUserList.sIdentification);

                    var BarcodeQrTopBottom = 0;
                    var positionTopBottom = studentInfoList.Where(w => w.elementName == "BarcodeQRTopBottom").FirstOrDefault();
                    if (positionTopBottom != null)
                        BarcodeQrTopBottom = int.Parse(positionTopBottom.elementValue);

                    var BarcodeQrLeftRight = 0;
                    var positionLeftRigh = studentInfoList.Where(w => w.elementName == "BarcodeQRLeftRight").FirstOrDefault();
                    if (positionLeftRigh != null)
                        BarcodeQrLeftRight = int.Parse(positionLeftRigh.elementValue);

                    MoveBarcodeQRTC13.Attributes["style"] = "top:" + BarcodeQrTopBottom + "px; left: " + BarcodeQrLeftRight + "px; ";


                    string code = string.Format(TUserList.sStudentID);
                    string imgBase655 = QRCodeFunction.Create(code, QRCoder.QRCodeGenerator.ECCLevel.H, 1, false);
                    QR13.Src = imgBase655;

                    StudentidTC13.InnerText = TUserList.sStudentID;

                }
                else if (tC == "15")
                {
                    mainPage1.Attributes["hidden"] = "hidden";
                    mainPageTC5.Attributes["hidden"] = "hidden";
                    mainPageTC6.Attributes["hidden"] = "hidden";
                    mainPageTC7.Attributes["hidden"] = "hidden";
                    mainPageTC8.Attributes["hidden"] = "hidden";
                    mainPageTC9.Attributes["hidden"] = "hidden";
                    mainPageTC10.Attributes["hidden"] = "hidden";
                    mainPageTC11.Attributes["hidden"] = "hidden";
                    mainPageTC12.Attributes["hidden"] = "hidden";
                    mainPageTC13.Attributes["hidden"] = "hidden";
                    mainPageTC16.Attributes["hidden"] = "hidden";

                    var card = studentInfoList.Where(w => w.elementName == "CardCover").FirstOrDefault();
                    if (card != null)
                    {
                        if (card.elementValue == "")
                            BG15.Attributes["style"] = "";
                        else
                            BG15.Src = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm")); ;
                        BG15.Attributes["style"] = "position: fixed; height: 55mm;";
                    }
                    else BG15.Attributes["style"] = "";

                    if (modeall == "1")
                        printbutton.Attributes["class"] = "example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall hidden";

                    if (STUDENTPICTURE != null && STUDENTPICTURE != "")
                        ImgProfile15.Src = PictureUrl(TUserList);
                    else ImgProfile15.Src = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";

                    if (wE == "0")
                    {
                        if (FULLNAMETH.Length <= 28)
                        {
                            LblName15.Text = FULLNAMETH;
                            LblName15.Attributes["style"] = "font-size: 90%; font-weight: bold;";
                        }
                        else
                        {
                            LblName15.Text = FULLNAMETH;
                            LblName15.Attributes["style"] = "font-size: 82%; font-weight: bold;";
                        }

                        if (FULLNAMEEN.Length <= 24)
                        {
                            LblEngname15.Text = FULLNAMEEN;
                            LblEngname15.Attributes["style"] = "font-size: 90%; font-weight: bold;";
                        }
                        else
                        {
                            LblEngname15.Text = FULLNAMEEN;
                            LblEngname15.Attributes["style"] = "font-size: 77%; font-weight: bold;";
                        }

                    }
                    else if (wE == "1")
                    {

                        if (FULLNAMETH.Length <= 28)
                        {
                            LblName15.Text = FULLNAMETH;
                            LblName15.Attributes["style"] = "font-size: 90%; font-weight: bold;";
                        }
                        else
                        {
                            LblName15.Text = FULLNAMETH;
                            LblName15.Attributes["style"] = "font-size: 82%; font-weight: bold;";
                        }
                        LblEngname15.Text = "";
                        LblEngname15.Attributes["style"] = "font-size: 90%; font-weight: bold;";

                    }
                    else if (wE == "2")
                    {

                        if (FULLNAMEEN.Length <= 24)
                        {
                            LblName15.Text = FULLNAMEEN;
                            LblName15.Attributes["style"] = "font-size: 90%; font-weight: bold;";
                        }
                        else
                        {
                            LblName15.Text = FULLNAMEEN;
                            LblName15.Attributes["style"] = "font-size: 82%; font-weight: bold;";
                        }
                        LblEngname15.Text = "";
                        LblEngname15.Attributes["style"] = "font-size: 90%; font-weight: bold;";
                    }

                    var pidPosition = studentInfoList
                      .Where(w => w.elementName == "PIDPosition")
                      .Select(o => o.elementValue)
                      .FirstOrDefault();

                    LblStudentID15.Text = STUDENTID;//AddDash(STUDENTIDENTITY);
                    LblStudentID15.Attributes["style"] = "font-size: 90%; font-weight: bold;" + (string.IsNullOrEmpty(pidPosition) ? "" : $"padding-left:{pidPosition}px !important;");

                    if (schoolID == 229)
                    {
                        if (tCL == "0")
                            LblClass15.Text = TSubLevel.SubLevelNameEN;
                        else if (tCL == "1")
                        {
                            LblClass15.Text = TLevel.LevelName;
                            LblClassEng15.Text = TLevel.LevelNameEng.Replace("School", "");
                        }
                        else if (tCL == "2")
                        {
                            LblClass15.Text = TLevel.LevelNameEng;
                            LblClassEng15.Text = TLevel.LevelNameEng.Replace("School", "");
                        }
                        else if (tCL == "3")
                        {
                            LblClass15.Text = "";
                            LblClassEng15.Text = "";
                        }
                    }
                    else
                    {
                        if (tCL == "0")
                            LblClass15.Text = TSubLevel.SubLevelNameTH;
                        else if (tCL == "1")
                            LblClass15.Text = TLevel.LevelName;
                        else if (tCL == "2")
                        {
                            LblClass15.Text = TLevel.LevelName;
                            LblClassEng15.Text = TLevel.LevelNameEng.Replace("School", "");
                        }
                        else if (tCL == "3")
                        {
                            LblClass15.Text = "";
                            LblClassEng15.Text = "";
                        }
                    }


                    LblClass15.Attributes["style"] = "padding-left: 45px; padding-right: 0px; font-size: 90%; font-weight: bold;";
                    LblClassEng15.Attributes["style"] = "padding-left: 41px; padding-right: 0px; font-size: 90%; font-weight: bold;margin-top: -5px;";
                    var Barcode = BarCodeFunction.Create(STUDENTID, BarCodeFunction.BarcodeTYPE.CODE128, false, BarCodeFunction.Alignment.CENTER, 193, 43);

                    var UpDown = 0;
                    var LeftRight = 0;
                    var BarcodeUpDown = 0;
                    var BarcodeLeftRight = 0;

                    var PositionUpDown = studentInfoList.Where(w => w.elementName == "PicProfileUpDown").FirstOrDefault();
                    if (PositionUpDown != null)
                        UpDown = int.Parse(PositionUpDown.elementValue);

                    var PositionLeftRight = studentInfoList.Where(w => w.elementName == "PicProfileLeftRight").FirstOrDefault();
                    if (PositionLeftRight != null)
                        LeftRight = int.Parse(PositionLeftRight.elementValue);

                    int PicUpDown = (0 + UpDown);
                    int PicLeftRight = (10 + LeftRight);

                    if (tC == "1")
                        ImgProfile15.Attributes["style"] = "margin-left:" + PicLeftRight + "px; display:block; width:87.25px; height:107.5px; margin-top:" + PicUpDown + "px;";
                    else
                        ImgProfile15.Attributes["style"] = "margin-left: " + PicLeftRight + "px; display:block; width:80px; height:101px; margin-top: " + PicUpDown + "px;";


                    var positionTop = studentInfoList.Where(w => w.elementName == "BarcodeQRTopBottom").FirstOrDefault();
                    if (positionTop != null)
                        BarcodeUpDown = int.Parse(positionTop.elementValue);

                    var positionLeftRight = studentInfoList.Where(w => w.elementName == "BarcodeQRLeftRight").FirstOrDefault();
                    if (positionLeftRight != null)
                        BarcodeLeftRight = int.Parse(positionLeftRight.elementValue);

                    ImgBarcode15.Src = Barcode;
                    ImgBarcode15.Attributes["style"] = "margin-left:" + (18 + (BarcodeLeftRight)) + "px; display:block; width: 4.1cm; height: 40px; margin-top:" + (12 + BarcodeUpDown) + "px;";

                    //1=กลับบ้าน, 2=อยู่หอ
                    if (STUDENTJOURNEY == 2)
                    {
                        LblDorm15.Text = STUDENTDORMITORYName;
                    }
                    else
                    {
                        LblDorm15.Text = "ไป - กลับ";
                    }
                    LblDorm15.Attributes["style"] = "margin-left:" + (18 + (BarcodeLeftRight)) + "px; display:block; width: 4.1cm;";
                }
                else if (tC == "16")
                {
                    mainPage1.Attributes["hidden"] = "hidden";
                    mainPageTC5.Attributes["hidden"] = "hidden";
                    mainPageTC6.Attributes["hidden"] = "hidden";
                    mainPageTC7.Attributes["hidden"] = "hidden";
                    mainPageTC8.Attributes["hidden"] = "hidden";
                    mainPageTC9.Attributes["hidden"] = "hidden";
                    mainPageTC10.Attributes["hidden"] = "hidden";
                    mainPageTC11.Attributes["hidden"] = "hidden";
                    mainPageTC12.Attributes["hidden"] = "hidden";
                    mainPageTC13.Attributes["hidden"] = "hidden";
                    mainPageTC15.Attributes["hidden"] = "hidden";

                    var card = studentInfoList.Where(w => w.elementName == "CardCover").FirstOrDefault();
                    var printDate = studentInfoList.Where(w => w.elementName == "CardPrintDate").FirstOrDefault();
                    var isReleaseShow = studentInfoList.Where(w => w.elementName == "ReleaseDate2Display").FirstOrDefault();
                    var enddate = studentInfoList.Where(w => w.elementName == "CardExpireDate").FirstOrDefault();
                    var isExpireShow = studentInfoList.Where(w => w.elementName == "ExpDate2Display").FirstOrDefault();

                    if (card != null)
                    {
                        if (card.elementValue == "")
                            bg16.Attributes["style"] = "";
                        else
                            bg16.Src = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm")); ;
                    }
                    else bg16.Attributes["style"] = "";

                    if (STUDENTPICTURE != null && STUDENTPICTURE != "")
                        avatar16.Src = PictureUrl(TUserList);
                    else avatar16.Src = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";

                    code16.InnerText = STUDENTID;
                    nameTh16.InnerText = FULLNAMETH;
                    nameEn16.InnerText = FULLNAMEEN;


                    if (isReleaseShow == null || isReleaseShow.elementValue == "0")
                    {
                        dateRelease16.InnerText = "" + printDate?.date?.ToString("dd/MM/yy", new CultureInfo("th-TH")); ;
                    }

                    if (isExpireShow == null || isExpireShow.elementValue == "0")
                    {
                        dateExpire16.InnerText = "" + enddate?.date?.ToString("dd/MM/yy", new CultureInfo("th-TH")); ;
                    }

                    var UpDown = 0;
                    var LeftRight = 0;
                    var BarcodeUpDown = 0;
                    var BarcodeLeftRight = 0;

                    var PositionUpDown = studentInfoList.Where(w => w.elementName == "PicProfileUpDown" && w.elementValue != "undefied").FirstOrDefault();
                    if (PositionUpDown != null)
                        UpDown = int.Parse(PositionUpDown.elementValue);

                    var PositionLeftRight = studentInfoList.Where(w => w.elementName == "PicProfileLeftRight").FirstOrDefault();
                    if (PositionLeftRight != null)
                        LeftRight = int.Parse(PositionLeftRight.elementValue);

                    avatar16.Attributes["style"] += "margin-left: " + LeftRight + "px;margin-top: " + UpDown + "px;";

                    string code = string.Format(TUserList.sStudentID);
                    string imgBase655 = QRCodeFunction.Create(code, QRCoder.QRCodeGenerator.ECCLevel.H, 1, false);
                    //string imgBase655 = QRCodeFunction.Create(code, QRCoder.QRCodeGenerator.ECCLevel.H);
                    qrCode16.Src = imgBase655;
                    var positionTop = studentInfoList.Where(w => w.elementName == "BarcodeQRTopBottom").FirstOrDefault();
                    if (positionTop != null)
                        BarcodeUpDown = int.Parse(positionTop.elementValue);

                    var positionLeftRight = studentInfoList.Where(w => w.elementName == "BarcodeQRLeftRight").FirstOrDefault();
                    if (positionLeftRight != null)
                        BarcodeLeftRight = int.Parse(positionLeftRight.elementValue);

                    qrCode16.Attributes["style"] += "margin-left:" + BarcodeLeftRight + "px; margin-top:" + BarcodeUpDown + "px;";

                    if (modeall == "1")
                        printbutton.Attributes["class"] = "example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall hidden";
                }
                else
                {
                    mainPageTC5.Attributes["hidden"] = "hidden";
                    mainPageTC6.Attributes["hidden"] = "hidden";
                    mainPageTC7.Attributes["hidden"] = "hidden";
                    mainPageTC8.Attributes["hidden"] = "hidden";
                    mainPageTC9.Attributes["hidden"] = "hidden";
                    mainPageTC10.Attributes["hidden"] = "hidden";
                    mainPageTC11.Attributes["hidden"] = "hidden";
                    mainPageTC12.Attributes["hidden"] = "hidden";
                    mainPageTC13.Attributes["hidden"] = "hidden";
                    mainPageTC15.Attributes["hidden"] = "hidden";
                    mainPageTC16.Attributes["hidden"] = "hidden";

                    if (modeall == "1")
                        printbutton.Attributes["class"] = "example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall hidden";

                    if (STUDENTPICTURE != null && STUDENTPICTURE != "")
                        Img1.Src = PictureUrl(TUserList);
                    else Img1.Src = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";


                    if (wE == "0")
                    {

                        if (FULLNAMETH.Length <= 28)
                        {
                            Label2.Text = FULLNAMETH;
                            Label2.Attributes["style"] = "font-size: 90%; font-weight: bold;";
                        }
                        else
                        {
                            Label2.Text = FULLNAMETH;
                            Label2.Attributes["style"] = "font-size: 82%; font-weight: bold;";
                        }

                        if (FULLNAMEEN.Length <= 24)
                        {
                            LabelEngName.Text = FULLNAMEEN;
                            LabelEngName.Attributes["style"] = "font-size: 90%; font-weight: bold;";
                        }
                        else
                        {
                            LabelEngName.Text = FULLNAMEEN;
                            LabelEngName.Attributes["style"] = "font-size: 77%; font-weight: bold;";
                        }

                    }
                    else if (wE == "1")
                    {

                        if (FULLNAMETH.Length <= 28)
                        {
                            Label2.Text = FULLNAMETH;
                            Label2.Attributes["style"] = "font-size: 90%; font-weight: bold;";
                        }
                        else
                        {
                            Label2.Text = FULLNAMETH;
                            Label2.Attributes["style"] = "font-size: 82%; font-weight: bold;";
                        }
                        LabelEngName.Text = "";
                        LabelEngName.Attributes["style"] = "font-size: 90%; font-weight: bold;";

                    }
                    else if (wE == "2")
                    {

                        if (FULLNAMEEN.Length <= 24)
                        {
                            Label2.Text = FULLNAMEEN;
                            Label2.Attributes["style"] = "font-size: 90%; font-weight: bold;";
                        }
                        else
                        {
                            Label2.Text = FULLNAMEEN;
                            Label2.Attributes["style"] = "font-size: 82%; font-weight: bold;";
                        }
                        LabelEngName.Text = "";
                        LabelEngName.Attributes["style"] = "font-size: 90%; font-weight: bold;";

                    }

                    var pidDisplay = studentInfoList
                       .Where(w => w.elementName == "PIDDisplay")
                       .Select(o => o.elementValue)
                       .FirstOrDefault();

                    if ((pidDisplay ?? "0") == "0")
                    {
                        var pidPosition = studentInfoList
                        .Where(w => w.elementName == "PIDPosition")
                        .Select(o => o.elementValue)
                        .FirstOrDefault();

                        Label3.Text = AddDash(STUDENTIDENTITY);
                        Label3.Attributes["style"] = "font-size: 90%; font-weight: bold;" + (string.IsNullOrEmpty(pidPosition) ? "" : $"padding-left:{pidPosition}px !important;");
                    }
                    else
                    {
                        Label3.Text = "-";
                    }


                    if (tCL == "0")
                        Class.Text = TSubLevel.SubLevelNameTH;
                    else if (tCL == "1")
                        Class.Text = TLevel.LevelName;
                    else if (tCL == "2")
                        Class.Text = TLevel.LevelNameEng.Replace("School", "");
                    else if (tCL == "3")
                        Class.Text = "";

                    Class.Attributes["style"] = "padding-left: 45px; padding-right: 0px; font-size: 90%; font-weight: bold;";

                    if (tC == "14")
                    {
                        Blood.Visible = false;
                    }
                    else
                    {
                        Blood.Text = "";
                        Blood.Attributes["style"] = "font-size: 90%; font-weight: bold; padding-right: 0px; padding-left: 5px;";
                    }

                    //string dayToday = DateTime.Today.ToString("dd");
                    //string monthToday = DateTime.Today.ToString("MM");
                    //string yearToday = DateTime.Today.AddYears(543).Year.ToString();
                    //yearToday = yearToday.Remove(0, 2);

                    //switch (userData.CompanyID)
                    //{
                    //    case 1163:
                    //        DateNow.Text = "25/01/66";
                    //        break;

                    //    case 1189:
                    //        DateNow.Text = "8/6/66";
                    //        break;

                    //    default:
                    //        DateNow.Text = DateTime.Now.ToString("dd/MM/yy", new CultureInfo("th-TH"));
                    //        break;
                    //}
                    var isShowPrintDate = studentInfoList
                      .Where(w => w.elementName == "ReleaseDate2Display")
                      .FirstOrDefault();
                    var printDate = studentInfoList
                        .Where(w => w.elementName == "CardPrintDate")
                        .FirstOrDefault();
                    // DateNow.Text = //dayToday + "/" + monthToday + "/" + yearToday;

                    if (isShowPrintDate == null || isShowPrintDate.elementValue == "0")
                    {
                        if (printDate != null)
                        {
                            DateNow.Text = printDate.date?.ToString("dd/MM/yy", new CultureInfo("th-TH"));
                            DateNow.Attributes["style"] = "padding-left: 49px; padding-right: 0px; font-size: 90%; font-weight: bold;";
                        }
                    }

                    studentID.Text = STUDENTID;

                    var fontColor = studentInfoList.Where(w => w.elementName == "FontColor").FirstOrDefault();
                    if (fontColor != null)
                    {
                        if (fontColor.elementValue == "White")
                            studentID.Attributes["class"] = "sarabun Fwhite";
                        else if (fontColor.elementValue == "Black")
                            studentID.Attributes["class"] = "sarabun Fblack";
                        else if (fontColor.elementValue == "Blue")
                            studentID.Attributes["class"] = "sarabun Fblue";
                    }
                    else
                        studentID.Attributes["class"] = "sarabun Fblack";


                    var card = studentInfoList.Where(w => w.elementName == "CardCover").FirstOrDefault();
                    if (card != null)
                    {
                        if (card.elementValue == "")
                            BG.Attributes["style"] = "";
                        else
                            BG.Src = card.elementValue + "?d=" + (DateTime.Now.ToString("ddMMyyHHmm"));

                        BG.Attributes["style"] = "position: fixed; height: 55mm;";
                    }
                    else
                        BG.Attributes["style"] = "";

                    var Barcode = BarCodeFunction.Create(STUDENTID, BarCodeFunction.BarcodeTYPE.CODE128, false, BarCodeFunction.Alignment.CENTER, 193, 43);

                    var UpDown = 0;
                    var LeftRight = 0;
                    var BarcodeUpDown = 0;
                    var BarcodeLeftRight = 0;

                    var PositionUpDown = studentInfoList.Where(w => w.elementName == "PicProfileUpDown" && w.elementValue != "undefied").FirstOrDefault();
                    if (PositionUpDown != null)
                        UpDown = int.Parse(PositionUpDown.elementValue);

                    var PositionLeftRight = studentInfoList.Where(w => w.elementName == "PicProfileLeftRight").FirstOrDefault();
                    if (PositionLeftRight != null)
                        LeftRight = int.Parse(PositionLeftRight.elementValue);

                    int PicUpDown = (-3 + UpDown);
                    int PicLeftRight = (7 + LeftRight);

                    if (tC == "1")
                        Img1.Attributes["style"] = "margin-left:" + PicLeftRight + "px; display:block; width:87.25px; height:107.5px; margin-top:" + PicUpDown + "px;";
                    else
                        Img1.Attributes["style"] = "margin-left: " + PicLeftRight + "px; display:block; width:80px; height:101px; margin-top: " + PicUpDown + "px;";


                    var positionTop = studentInfoList.Where(w => w.elementName == "BarcodeQRTopBottom").FirstOrDefault();
                    if (positionTop != null)
                        BarcodeUpDown = int.Parse(positionTop.elementValue);

                    var positionLeftRight = studentInfoList.Where(w => w.elementName == "BarcodeQRLeftRight").FirstOrDefault();
                    if (positionLeftRight != null)
                        BarcodeLeftRight = int.Parse(positionLeftRight.elementValue);

                    BarCode.Src = Barcode;
                    BarCode.Attributes["style"] = "margin-left:" + (24 + (BarcodeLeftRight)) + "px; display:block; width: 4.1cm; height: 40px; margin-top:" + BarcodeUpDown + "px;";

                    //barcode tBQ 0 = show, 1 = not show 
                    if (tBQ == "1")
                    {
                        HideQRCode.Attributes["hidden"] = "hidden";
                    }

                    //string dayExp = "";
                    //string monthExp = "";
                    //string yearExp = "";

                    var isShowExpireDate = studentInfoList
                     .Where(w => w.elementName == "ShowExpireDate" && w.cDel == null)
                     .FirstOrDefault();

                    var enddate = studentInfoList
                        .Where(w => w.elementName == "CardExpireDate" && w.cDel == null)
                        .FirstOrDefault();

                    if (isShowExpireDate == null || isShowExpireDate.elementValue == "0")
                    {
                        if (enddate != null)
                        {
                            ExpDate.Text = enddate.date?.ToString("dd/MM/yy", new CultureInfo("th-TH"));
                            //dayExp + "/" + monthExp + "/" + yearExp;
                            ExpDate.Attributes["style"] = "padding-left: 59px; padding-right: 0px; font-size: 88%; font-weight: bold;";
                        }
                        else
                        {
                            ExpDate.Text = "";
                        }
                    }

                    if (schoolID == 1173)
                    {
                        ExpDate.Text = "";
                    }

                    var cardType = studentInfoList.Where(w => w.elementName == "CardType").FirstOrDefault();
                    if (cardType != null)
                    {
                        if (cardType.elementValue == "3" || cardType.elementValue == "14")
                        {
                            if (TUserList.sStudentNameOther != null && TUserList.sStudentNameOther != "")
                            {
                                if (FULLNAMETH.Length <= 28)
                                {
                                    Label2.Text = FULLNAMETH;
                                    Label2.Attributes["style"] = "font-size: 90%; font-weight: bold;";
                                }
                                else
                                {
                                    Label2.Text = FULLNAMETH;
                                    Label2.Attributes["style"] = "font-size: 82%; font-weight: bold;";
                                }

                                if (wE == "0")
                                {
                                    if (FULLNAMEEN.Length <= 24)
                                    {
                                        LabelEngName.Text = FULLNAMEEN;
                                        LabelEngName.Attributes["style"] = "font-size: 90%; font-weight: bold;";
                                    }
                                    else
                                    {
                                        LabelEngName.Text = FULLNAMEEN;
                                        LabelEngName.Attributes["style"] = "font-size: 77%; font-weight: bold;";
                                    }
                                }
                                else
                                {
                                    LabelEngName.Text = "";
                                    LabelEngName.Attributes["style"] = "font-size: 90%; font-weight: bold;";
                                }
                            }


                            //ExpDate.Text = "";
                            var Phone = studentInfoList.Where(w => w.elementName == "NumberPhone").FirstOrDefault();
                            if (Phone != null)
                            {
                                if (Phone.elementValue == "0")//show phone
                                {
                                    if (TUserList.sPhone != null || TUserList.sPhone != string.Empty)
                                    {
                                        ExpDate.Text = TUserList.sPhone;
                                    }
                                    ExpDate.Attributes["style"] = "padding-left: 55px; padding-right: 0px; font-size: 88%; font-weight: bold;";
                                }
                                //else if (Phone.elementValue == "1")//not show
                                //{
                                //    ExpDate.Text = "";
                                //}

                            }


                            //DateNow.Text = "";
                            var Brithday = studentInfoList.Where(w => w.elementName == "Brithday").FirstOrDefault();
                            if (Brithday != null)
                            {
                                if (Brithday.elementValue == "0")//show birth day
                                {
                                    DateNow.Text = TUserList.dBirth?.ToString("dd/MM/yyyy", new CultureInfo("th-th"));
                                }
                                else if (Brithday.elementValue == "1")
                                {
                                    if (schoolID == 1173)
                                    {
                                        DateNow.Text = enddate?.date?.ToString("dd/MM/yy", new CultureInfo("th-TH"));
                                    }
                                    else
                                    {
                                        DateNow.Text = printDate?.date?.ToString("dd/MM/yy", new CultureInfo("th-TH"));
                                    }
                                }
                                else
                                {
                                    DateNow.Text = "";
                                }
                            }
                            DateNow.Attributes["style"] = "padding-left: 49px; padding-right: 0px; font-size: 90%; font-weight: bold;";

                            var health = _db.TStudentHealthInfoes.Where(w => w.sID == TUserList.sID && w.SchoolID == userData.CompanyID).FirstOrDefault();
                            if (health != null)
                            {
                                if (health.sBlood != null)
                                    Blood.Text = health.sBlood;
                            }
                            Blood.Attributes["style"] = "font-size: 90%; font-weight: bold; padding-right: 0px; padding-left: 5px;";

                        }
                        else if (cardType.elementValue == "4")
                        {
                            Label3.Text = STUDENTID;
                            Label3.Attributes["style"] = "font-size: 90%; font-weight: bold;padding-left: 14px !important;";

                            //NICKNAME 0 = show, 1 = notshow
                            if (tNik == "0")
                            {
                                studentID.Text = NICKNAMETH;
                            }
                            else if (tNik == "1")
                            {
                                studentID.Text = "";
                            }


                            ////SB-2688
                            //if (schoolID == 349)
                            //{
                            //    if (enddate != null)
                            //    {
                            //        dayExp = enddate.date.Value.Day.ToString();
                            //        if (dayExp.Length == 1)
                            //            dayExp = "0" + dayExp;

                            //        monthExp = enddate.date.Value.Month.ToString();
                            //        if (monthExp.Length == 1)
                            //            monthExp = "0" + monthExp;

                            //        yearExp = enddate.date.Value.Year.ToString();
                            //        yearExp = yearExp.Remove(0, 2);

                            //        //ExpDate tEXP 0 = show, 1 = notshow
                            //        if (tEXP == "0")
                            //        {
                            //            ExpDate.Text = dayExp + "/" + monthExp + "/" + yearExp;
                            //            ExpDate.Attributes["style"] = "padding-left: 59px; padding-right: 0px; font-size: 88%; font-weight: bold;";
                            //        }
                            //        else if (tEXP == "1")
                            //        {
                            //            ExpDate.Text = "";
                            //        }
                            //    }

                            //    yearToday = DateTime.Today.Year.ToString();
                            //    yearToday = yearToday.Remove(0, 2);


                            //    if (tCL == "0")
                            //        Class.Text = TSubLevel.SubLevelNameEN;
                            //    else
                            //        Class.Text = TLevel.LevelNameEng.Replace("School","");
                            //    Class.Attributes["style"] = "padding-left: 45px; padding-right: 0px; font-size: 90%; font-weight: bold;";


                            //    DateNow.Text = dayToday + "/" + monthToday + "/" + yearToday;
                            //    DateNow.Attributes["style"] = "padding-left: 49px; padding-right: 0px; font-size: 90%; font-weight: bold;";
                            //}


                        }
                        else if (cardType.elementValue == "1")
                        {
                            //var Brithday = studentInfoList.Where(w => w.SchoolID == userData.CompanyID && w.elementName == "Brithday").FirstOrDefault();
                            //if (Brithday != null)
                            //{
                            //    if (Brithday.elementValue == "0")
                            //    {
                            //        DateNow.Text = TUserList.dBirth.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th"));
                            //    }
                            //    else
                            //    {
                            //        //if (dayExp != "")
                            //        //    DateNow.Text = dayExp + "/" + monthExp + "/" + yearExp;
                            //    }
                            //}
                            //DateNow.Attributes["style"] = "padding-left: 49px; padding-right: 0px; font-size: 90%; font-weight: bold;";
                        }
                    }
                }

            }
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

        public string PictureUrl(JabjaiEntity.DB.TUser user)
        {
            string sStudentPicture = user.sStudentPicture;
            if (sStudentPicture.IndexOf("?x-image-process=image/resize,m_fill,h_300,w_270") != -1)
            {
                if (string.IsNullOrEmpty(sStudentPicture.Replace("?x-image-process=image/resize,m_fill,h_300,w_270", "")))
                {
                    return "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";
                }

                return user.sStudentPicture + "&d=" + (user.dPicUpdate ?? DateTime.Now).ToString("ddMMyyyyHHmmss");
            }
            else
            {
                return user.sStudentPicture + "?d=" + (user.dPicUpdate ?? DateTime.Now).ToString("ddMMyyyyHHmmss");
            }
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