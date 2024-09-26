using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System.Data;
using FingerprintPayment.Helper;


namespace FingerprintPayment.TeacherCard
{
    public partial class TeacherCardPrint : System.Web.UI.Page
    {

        public List<Teacherlist> teacherlists { get; set; } = new List<Teacherlist>();
        public List<TTeacherCardInfo> CardProp { get; set; } = new List<TTeacherCardInfo>();

        public bool IsShowNameEng { get; set; }
        public bool isShowPosition { get; set; }
        public bool isShowIdentity { get; set; }
        public bool isShowImgProfile { get; set; }

        public string PictureSize { get; set; }

        public static string BGCard = "";
        public static int UpDown = 0;
        public static int LeftRight = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                var userMasterList = new List<UserMasterModel>();
                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = HttpContext.Current.Session["sEntities"].ToString();

                    var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var schooldata = _dbMaster.TCompanies.Where(w => w.nCompany == nCompany.nCompany).FirstOrDefault();
                    userMasterList = _dbMaster.TUsers.Where(w => w.nCompany == nCompany.nCompany && w.cDel == null && w.cType == "1").Select(o => new UserMasterModel { sID = o.sID, sPicture = o.sPicture })
                        .ToList();
                }
                var Title = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();
                //var Employee = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).ToList();
                //var EmployeeInFo = _db.TEmployeeInfoes.Where(w => w.SchoolID == userData.CompanyID).ToList();

                //var JobList = _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID && w.deleted != "1").ToList();


                var EMP_TYPE = Request.QueryString["type"];
                var EMP_ID = Request.QueryString["EmpID"];

                var OptionUpDown = Request.QueryString["OUpDown"];
                var OptionLeftRight = Request.QueryString["OLeftRight"];
                //UpDown = Convert.ToInt32(OptionUpDown);
                //LeftRight = Convert.ToInt32(OptionLeftRight);

                string aEMP_TYPE = "";
                int? aEMP_ID = null;

                if (EMP_TYPE == "" && EMP_ID == null)
                {
                    aEMP_TYPE = null;
                    aEMP_ID = null;
                }
                else if (string.IsNullOrEmpty(EMP_ID))
                {
                    aEMP_TYPE = EMP_TYPE;
                }
                else if (string.IsNullOrEmpty(EMP_TYPE))
                {
                    aEMP_ID = int.Parse(EMP_ID);
                }

                CardProp = _db.TTeacherCardInfoes.AsNoTracking().Where(w => w.SchoolID == userData.CompanyID).ToList();

                var CardContentUpdown = CardProp.Where(w => w.elementName == "ContentAlignmentTopBottom").FirstOrDefault();
                if (CardContentUpdown != null)
                {
                    var CardUpdown = CardContentUpdown.elementValue == null ? "0" : CardContentUpdown.elementValue;
                    UpDown = Convert.ToInt32(CardUpdown);
                }

                var CardContentLeftRight = CardProp.Where(w => w.elementName == "ContentAlignmentLeftRight").FirstOrDefault();
                if (CardContentLeftRight != null)
                {
                    var CardLeftRight = CardContentLeftRight.elementValue == null ? "0" : CardContentLeftRight.elementValue;
                    LeftRight = Convert.ToInt32(CardLeftRight);
                }

                var card = CardProp.Where(w => w.elementName == "CardCover" && w.SchoolID == userData.CompanyID).FirstOrDefault();
                if (card != null)
                    BGCard = card.elementValue;
                else
                    BGCard = "";

                //List<TypeList> typeLists = new List<TypeList>
                //{
                //    new TypeList {cTypeID = "1",cTypeName = "บุคลากรทั่วไป"},
                //    new TypeList {cTypeID = "2",cTypeName = "ครูประจำการ"},
                //    new TypeList {cTypeID = "3",cTypeName = "บุคลากรทางการศึกษา"},
                //    new TypeList {cTypeID = "4",cTypeName = "ครูพิเศษ"},
                //    new TypeList {cTypeID = "5",cTypeName = "ครูพี่เลี้ยง"},
                //    new TypeList {cTypeID = "6",cTypeName = "ผู้บริหาร"},
                //};

                //var typeLists = _db.TEmployeeTypes
                //           .Where(o => o.SchoolID == userData.CompanyID && o.IsActive == true && o.IsDel == false)
                //           .Select(o => new TypeList
                //           {
                //               cTypeID = (o.nTypeId2 ?? o.nTypeId) + "",
                //               cTypeName = o.Title,
                //           })
                //           .OrderBy(o => o.cTypeID)
                //           .ToList();


                var CardType = CardProp.Where(w => w.elementName == "CardType").FirstOrDefault();

                if (CardType.elementValue == "1")
                {
                    Page2.Attributes["class"] = "col-xs-12 page2 pagecut hidden";
                    Page3.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page4.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page5.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page6.Attributes["class"] = "col-xs-12 page2 pagecut hidden";
                }
                else if (CardType.elementValue == "2")
                {
                    Page1.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page3.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page4.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page5.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page6.Attributes["class"] = "col-xs-12 page2 pagecut hidden";
                }
                else if (CardType.elementValue == "3")
                {
                    Page1.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page2.Attributes["class"] = "col-xs-12 page2 pagecut hidden";
                    Page4.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page5.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page6.Attributes["class"] = "col-xs-12 page2 pagecut hidden";

                    PictureSize = CardProp.Where(w => w.elementName == "PictureSize").Select(o => o.elementValue).FirstOrDefault() + "";
                    
                }
                else if (CardType.elementValue == "4")
                {
                    Page1.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page2.Attributes["class"] = "col-xs-12 page2 pagecut hidden";
                    Page3.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page5.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page6.Attributes["class"] = "col-xs-12 page2 pagecut hidden";
                }
                else if (CardType.elementValue == "5")
                {
                    Page1.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page2.Attributes["class"] = "col-xs-12 page2 pagecut hidden";
                    Page3.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page4.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page6.Attributes["class"] = "col-xs-12 page2 pagecut hidden";
                }
                else if (CardType.elementValue == "6")
                {
                    Page1.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page2.Attributes["class"] = "col-xs-12 page2 pagecut hidden";
                    Page3.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page4.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                    Page5.Attributes["class"] = "col-xs-12 page1 pagecut hidden";
                }

                var q = (from a in _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null)

                         join b in _db.TEmployeeInfoes.Where(w => w.SchoolID == userData.CompanyID) on a.sEmp equals b.sEmp into JAB
                         from b in JAB.DefaultIfEmpty()

                         join c in _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID && w.deleted != "1") on a.nJobid equals c.nJobid into JAC
                         from c in JAC.DefaultIfEmpty()

                         join d in _db.TDepartments.Where(w => w.SchoolID == userData.CompanyID) on a.nDepartmentId equals d.DepID into JAD
                         from d in JAD.DefaultIfEmpty()

                         where (!aEMP_ID.HasValue || a.sEmp == aEMP_ID)
                         && ((string.IsNullOrEmpty(aEMP_TYPE)) || a.cType == aEMP_TYPE)
                         orderby b.Code, b.Code.Length
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
                             EmpType = c == null ? string.Empty : c.jobDescription,
                             EmpDepartment = d == null ? string.Empty : d.departmentName
                         }).ToList();

                teacherlists = (from a in q
                                from b in userMasterList.Where(o => o.sID == a.sEmp).DefaultIfEmpty()
                                select new Teacherlist
                                {
                                    EmpID = a.sEmp,
                                    Id = (a.Code == null || a.Code == string.Empty) ? "ไม่พบข้อมูล" : AddDash(a.Code),
                                    Identification = (a.sIdentification == null || a.sIdentification == string.Empty) ? "ไม่พบข้อมูล" : AddDash(a.sIdentification),
                                    EmpFullName = Common.geTitelName(Title, a.sTitle) + a.sName + " " + a.sLastname,
                                    EmpFullNameEN = a.FirstNameEn + " " + a.LastNameEn,
                                    EmpType = (a.EmpType == null || a.EmpType == string.Empty) ? "ไม่พบข้อมูล" : a.EmpType,
                                    EmpPic = b?.sPicture ?? a?.sPicture ?? "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png",

                                    QRCode = QRCodeFunction.Create(a.Code, QRCoder.QRCodeGenerator.ECCLevel.H, 1, false),
                                    Barcode = BarCodeFunction.Create(a.Code, BarCodeFunction.BarcodeTYPE.CODE128, false, BarCodeFunction.Alignment.CENTER, 193, 43),
                                    EmpDepartment = (a.EmpDepartment == null || a.EmpDepartment == string.Empty) ? "ไม่พบข้อมูล" : a.EmpDepartment
                                }).ToList();

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


        public static string gEtTypeEmploYee(List<TypeList> typeLists, string TypelId)
        {
            var f_type = typeLists.FirstOrDefault(f => f.cTypeID == TypelId);
            return f_type == null ? TypelId : f_type.cTypeName;
        }


        public class TypeList
        {
            public string cTypeID { get; set; }
            public string cTypeName { get; set; }
        }



        public class Teacherlist
        {
            public string Id { get; set; }
            public int EmpID { get; set; }
            public string EmpType { get; set; }
            public string EmpFullName { get; set; }
            public string EmpFullNameEN { get; set; }
            public string EmpPic { get; set; }
            public string QRCode { get; set; }
            public string Barcode { get; set; }
            public string Identification { get; set; }
            public string EmpDepartment { get; set; }

        }

        private class UserMasterModel
        {
            public int sID { get; set; }
            public string sPicture { get; set; }
        }
    }
}