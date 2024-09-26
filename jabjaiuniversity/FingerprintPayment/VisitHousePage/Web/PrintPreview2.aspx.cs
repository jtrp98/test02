using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.VisitHousePage.Web
{
    public partial class PrintPreview2 : BehaviorGateway
    {
        protected List<VisitHomeModel.PreviewForm> DataForms { get; set; }
        protected const string DEACTIVE = "deactive";
        protected const string ACTIVE = "active";
        protected const string CODE_CHECK = "&#10004;";
        protected string SCHOOL_NAME = "";
        protected string SCHOOL_AREA = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitPage();
            }
        }

        private void InitPage()
        {
            using (JabJaiMasterEntities mst = Connection.MasterEntities(ConnectionDB.Read))
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new VisitHomeRepository(ctx, mst);

                var q1 = from a in mst.TCompanies.Where(o => o.nCompany == UserData.CompanyID)
                         from b in mst.TSchoolAreas.Where(o => o.Code == a.SchoolAreaCode).DefaultIfEmpty()
                         select new
                         {
                             a.sCompany,
                             b.Area,
                         };
                var school = q1.FirstOrDefault();
                if (school != null)
                {
                    SCHOOL_NAME = school.sCompany;
                    SCHOOL_AREA = school.Area;
                }

                var sid = Request.QueryString["sid"];
                var termid = Request.QueryString["term"];
                var listId = new List<int>();
                if (!string.IsNullOrEmpty(sid))
                {
                    listId.Add(Convert.ToInt32(sid));
                }
                else
                {
                    var classid = Request.QueryString["classid"];
                    var roomid = Request.QueryString["roomid"];

                    int? room = null;
                    if (!string.IsNullOrWhiteSpace(roomid))
                    {
                        room = Convert.ToInt32(roomid);
                    }

                    var result = logic.LoadDataList(UserData.CompanyID, termid, Convert.ToInt32(classid), room, "");

                    listId = result.Select(o => o.sID).ToList();
                }


                var model = logic.LoadPreviewForm(UserData.CompanyID, listId, termid, UserData);

                DataForms = model;

                foreach (var DataForm in DataForms)
                {
                    if (DataForm.Visit != null)
                    {
                        //if (!string.IsNullOrEmpty(DataForm.Visit.StudentSignature))
                        //    DataForm.Visit.StudentSignature = ConvertImageURLToBase64(DataForm.Visit.StudentSignature);
                        //if (!string.IsNullOrEmpty(DataForm.Visit.ParentSignature))
                        //    DataForm.Visit.ParentSignature = ConvertImageURLToBase64(DataForm.Visit.ParentSignature);
                        //if (!string.IsNullOrEmpty(DataForm.Visit.TeacherSignature))
                        //    DataForm.Visit.TeacherSignature = ConvertImageURLToBase64(DataForm.Visit.TeacherSignature);
                        //

                        //if(DataForm.Visit.TeacherId.HasValue)
                        //{
                        //    var q = (from a in ctx.TEmployees.Where(o => o.SchoolID == UserData.CompanyID && o.sEmp == DataForm.Visit.TeacherId)
                        //             from b in ctx.TTitleLists.Where(o => o.SchoolID == UserData.CompanyID && o.nTitleid + "" == a.sTitle).DefaultIfEmpty()
                        //             from c in ctx.TJobLists.Where(o => o.SchoolID == a.SchoolID && o.nJobid  == a.nJobid).DefaultIfEmpty()
                        //             select new
                        //             {
                        //                 title = (b == null ? a.sTitle : b.titleDescription),
                        //                 name = a.sName + " " + a.sLastname,
                        //                 position = c.jobDescription,
                        //             }).FirstOrDefault();
                        //    DataForm.TeacherName = $"{q.title} {q.name}";
                        //    DataForm.TeacherPosition = $"{q.position}";
                        //}

                        if (!string.IsNullOrEmpty(DataForm.Visit.RelationshipMember))
                        {
                            var relation = JsonConvert.DeserializeObject<List<RelationshipMemberModel>>(DataForm.Visit.RelationshipMember);

                            var r = new RelationshipMemberModel();
                            r = relation.FirstOrDefault(o => o.relation == 1);
                            if (r != null)
                                DataForm.visitHouseFatherRelationsLevel[r.relationLevel - 1] = CODE_CHECK;
                            r = relation.FirstOrDefault(o => o.relation == 2);
                            if (r != null)
                                DataForm.visitHouseMotherRelationsLevel[r.relationLevel - 1] = CODE_CHECK;
                            r = relation.FirstOrDefault(o => o.relation == 3);
                            if (r != null)
                                DataForm.visitHouseBrotherRelationsLevel[r.relationLevel - 1] = CODE_CHECK;
                            r = relation.FirstOrDefault(o => o.relation == 4);
                            if (r != null)
                                DataForm.visitHouseSistersRelationsLevel[r.relationLevel - 1] = CODE_CHECK;
                            r = relation.FirstOrDefault(o => o.relation == 5);
                            if (r != null)
                                DataForm.visitHouseGrandparentsRelationsLevel[r.relationLevel - 1] = CODE_CHECK;
                            r = relation.FirstOrDefault(o => o.relation == 6);
                            if (r != null)
                                DataForm.visitHouseRelativeRelationsLevel[r.relationLevel - 1] = CODE_CHECK;
                            r = relation.FirstOrDefault(o => o.relation == 99);
                            if (r != null)
                            {
                                DataForm.visitHouseOtherRelationsLevel[r.relationLevel - 1] = CODE_CHECK;
                                DataForm.visitHouseOtherRelationsLevelSpecify = r.relationText;
                            }
                        }

                        SetCheckboxToArray(DataForm.Visit.ParentWantSchoolsHelp, DataForm.visitHouseHelpFromSchool);
                        //SetCheckboxToArray(DataForm.Visit.ParentWantAgencyHelp, DataForm.visitHouseParentWantAgencyHelp);
                        SetCheckboxToArray(DataForm.Visit.ParentWantAgencyHelp2, DataForm.visitHouseParentWantAgencyHelp2);
                        //3.พฤติกรรมและความเสี่ยง                   
                        SetCheckboxToArray(DataForm.Visit.Health, DataForm.visitHouseHealth);
                        SetCheckboxToArray(DataForm.Visit.WelfareSafety, DataForm.visitHouseWelfare);
                        SetRadioToArray(DataForm.Visit.StudentTravel, DataForm.visitHouseTravelMethod);
                        SetRadioToArray(DataForm.Visit.TravelTime, DataForm.visitHouseTravelTime);
                        SetCheckboxToArray(DataForm.Visit.LivingConditions, DataForm.visitHouseLivingConditions);
                        SetCheckboxToArray(DataForm.Visit.StudentResponsibilities, DataForm.visitHouseStudentWorkFamily);
                        SetCheckboxToArray(DataForm.Visit.Hobbies, DataForm.visitHouseHobby);
                        SetCheckboxToArray(DataForm.Visit.SubstanceAbuseBehavior, DataForm.visitHouseSubstanceAbuseBehavior);
                        SetCheckboxToArray(DataForm.Visit.ViolentBehavior, DataForm.visitHouseViolenceBehavior);
                        SetCheckboxToArray(DataForm.Visit.SexualBehavior, DataForm.visitHouseSexualBehavior);
                        SetCheckboxToArray(DataForm.Visit.GameAddiction, DataForm.visitHouseGameAddiction);

                        SetRadioToArray(DataForm.Visit.AccessComputerInternet, DataForm.visitHouseInternetAccess);
                        SetCheckboxToArray(DataForm.Visit.UseElectronicTools, DataForm.visitHouseUsingElectronicTools);
                        SetRadioToArray(DataForm.Visit.StudentInfoProvider, DataForm.visitHouseInformant);
                    }
                    SetRadioToArray(DataForm.Visit.HouseStyle, DataForm.visitHouseStyle);
                }



            }
        }

        private void SetRadioToArray(int? v, string[] arrToSet)
        {
            try
            {
                if (v.HasValue)
                {
                    if (v == 99)
                    {
                        arrToSet[arrToSet.Length - 1] = ACTIVE;
                    }
                    else
                    {
                        if (v != 0)
                            arrToSet[v.Value - 1] = ACTIVE;
                    }
                }
            }
            catch { }
        }

        private void SetCheckboxToArray(string txt, string[] arrToSet)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                var arr = JsonConvert.DeserializeObject<List<int>>(txt);
                try
                {
                    foreach (var item in arr)
                    {
                        if (item == 99)
                        {
                            arrToSet[arrToSet.Length - 1] = ACTIVE;
                        }
                        else
                        {
                            if (item != 0)
                                arrToSet[item - 1] = ACTIVE;
                        }
                    }
                }
                catch { }
            }
        }

        private String ConvertImageURLToBase64(String url)
        {
            StringBuilder _sb = new StringBuilder();

            Byte[] _byte = this.GetImage(url);

            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

            return _sb.ToString();
        }

        private byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception exp)
            {
                buf = null;
            }

            return (buf);
        }

        public static string GetType(string contentType)
        {
            switch (contentType)
            {
                case "text/plain":
                    return "text";

                case "application/pdf":
                    return "pdf";

                case "application/msword":
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                case "application/vnd.ms-excel":
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                case "application/vnd.ms-powerpoint":
                case "application/vnd.openxmlformats-officedocument.presentationml.presentation":
                    return "office";

                case "image/png":
                case "image/jpeg":
                case "image/gif":
                    return "image";

                case "video/mp4":
                case "video/x-matroska":
                case "video/x-ms-wmv":
                case "video/x-msvideo":
                case "video/quicktime":

                    return "video";

                case "audio/mpeg":
                    return "audio";

                default: return "";
            }
        }

        protected string GetVisitRelation(int? val, string other)
        {
            switch (val)
            {
                case 1:
                    return "บิดา";
                case 2:
                    return "มารดา";
                case 3:
                    return "พี่น้อง";
                case 4:
                    return "ญาติ";
                case 99:
                    return other;
                default:
                    return "";
            }
        }

        protected string GetVisitOccupation(int? val, string other)
        {
            switch (val)
            {
                case 1:
                    return "ธุรกิจส่วนตัว";
                case 2:
                    return "พนักงานบริษัท";
                case 3:
                    return "พนักงานรัฐวิสาหกิจ";
                case 4:
                    return "เกษตรกร";
                case 5:
                    return "ข้าราชการ";
                case 6:
                    return "รับจ้างทั่วไป";
                case 99:
                    return other;
                default:
                    return "";
            }
        }

        protected string GetVisitHighestEducation(int? val)
        {
            switch (val)
            {
                case 1:
                    return "ต่ำกว่าประถมศึกษา";
                case 2:
                    return "ประถมศึกษา";
                case 3:
                    return "มัธยมศึกษา";
                case 4:
                    return "ปวช./ปวส.";
                case 5:
                    return "อนุปริญญา";
                case 6:
                    return "ปริญญาตรี";
                case 7:
                    return "สูงกว่าปริญญาตรี";
                default:
                    return "";
            }
        }

        protected string GetVisitMedianHouseholdIncome(int? val)
        {
            switch (val)
            {
                case 1:
                    return "ต่ำกว่า 15,000";
                case 2:
                    return "15,000-20,000";
                case 3:
                    return "20,001 -30,000";
                case 4:
                    return "มากกว่า 30,000";
                default:
                    return "";
            }
        }
        protected string GetVisitReceiveExpensesFrom(int? val, string other)
        {
            switch (val)
            {
                case 1:
                    return "บิดา";
                case 2:
                    return "มารดา";
                case 3:
                    return "ญาติ";
                case 99:
                    return other;
                default:
                    return "";
            }
        }

        protected string GetVisitStudentWorkIncome(int? val, string other)
        {
            switch (val)
            {
                case 1:
                    return "ไม่มีอาชีพเสริม";
                case 2:
                    return "พาร์ทไทม์";
                case 3:
                    return "ธุรกิจส่วนตัว";
                case 4:
                    return "อาชีพอิสระ";
                case 99:
                    return other;
                default:
                    return "";
            }
        }

        protected string GetVisitDayIncome(int? val)
        {
            switch (val)
            {
                case 1:
                    return "ไม่มีรายได้";
                case 2:
                    return "ต่ำกว่า 100 บาท";
                case 3:
                    return "100 - 300 บาท";
                case 4:
                    return "มากกว่า 300 บาท";
                default:
                    return "";
            }
        }

        protected string GetVisitPaidComeDay(int? val)
        {
            switch (val)
            {
                case 1:
                    return "ต่ำกว่า 50 บาท";
                case 2:
                    return "50 - 100 บาท";
                case 3:
                    return "มากกว่า 100 บาท";
                default:
                    return "";
            }
        }

        protected string GetVisitDistanceHomeToSchool(int? val)
        {
            switch (val)
            {
                case 1:
                    return "1-5 กิโลเมตร";
                case 2:
                    return "6-10 กิโลเมตร";
                case 3:
                    return "11-15 กิโลเมตร";
                case 4:
                    return "16-20 กิโลเมตร";
                case 5:
                    return "20 กิโลเมตรขึ้นไป";
                default:
                    return "";
            }
        }

        protected string GetVisitTravelTime(int? val)
        {
            switch (val)
            {
                case 1:
                    return "ต่ำกว่า 30 นาที";
                case 2:
                    return "30 นาที - 1 ชั่วโมง";
                case 3:
                    return "มากกว่า 1 ชั่วโมง";
                default:
                    return "";
            }
        }

        protected class RelationshipMemberModel
        {
            public byte relation { get; set; }
            public byte relationLevel { get; set; }
            public string relationText { get; set; }
        }
    }

}

//[System.Web.Script.Services.ScriptMethod()]
//[System.Web.Services.WebMethod(EnableSession = true)]
//public static object SaveData()//(Dictionary<string,object> data , object file)
//{          
//    return new { };
//    //var userData = GetUserData();
//    //using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
//    //{
//    //    var logic = new VisitHomeLogic(ctx);

//    //    var result = logic.LoadDataList(userData.CompanyID, search.term, search.level2, search.name);

//    //    return new
//    //    {
//    //        data = result.Select((o, i) => new
//    //        {
//    //            Index = i + 1,
//    //            o.Code,
//    //            o.sID,
//    //            o.FullName,
//    //            o.Status1,
//    //            o.Status2,
//    //        })
//    //    };
//    //}
//    //var result = GetData(term);

//    //return result.GroupBy(o => o.Room)
//    //    .Select(o => new
//    //    {
//    //        Room = o.Key,
//    //        Count = o.Count(),
//    //        Data = o.Select(i => new
//    //        {
//    //            i.sID,
//    //            i.title,
//    //            i.FullName,
//    //            moveInDate = i.moveInDate.HasValue ? i.moveInDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : "-",
//    //            i.sStudentID,
//    //            oldSchoolName = string.IsNullOrWhiteSpace(i.oldSchoolName) ? "-" : i.oldSchoolName,

//    //        })
//    //    })
//    //    .ToList();
//}
