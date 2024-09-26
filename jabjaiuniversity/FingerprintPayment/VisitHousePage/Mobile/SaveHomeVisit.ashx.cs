using OBS;
using OBS.Model;
using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace FingerprintPayment.VisitHousePage.Mobile
{
    /// <summary>
    /// Summary description for SaveHomeVisit
    /// </summary>
    public class SaveHomeVisit : IHttpHandler
    {
        private string endpoint = "obs.ap-southeast-2.myhuaweicloud.com";
        private string AK = "UVIKYLYS2BECK7RGLFVN";
        private string SK = "LCQcPRii1eSYVHInqMvJU0TklY5lH6VrUeikwhdK";

        public void ProcessRequest(HttpContext context)
        {
            bool success = true;
            string message = "Success to save data.";

            try
            {
                string data = context.Request.Form["data"];
                VisitHouseForm.HomeVisitData homeVisitData = JsonConvert.DeserializeObject<VisitHouseForm.HomeVisitData>(data);
                int schoolID = homeVisitData.SchoolID;

                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        StudentLogic studentLogic = new StudentLogic(dbSchool);
                        string currentTermID = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });

                        int? currentYearID = null;
                        var termObj = dbSchool.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();
                        if (termObj != null)
                        {
                            currentYearID = termObj.nYear;
                        }

                        ObsConfig config = new ObsConfig();
                        config.Endpoint = endpoint;
                        ObsClient client = new ObsClient(AK, SK, config);

                        // Signature manage & upload file image to cloud
                        string parentSignatureUrl = UploadImageToCloudFromBase64(client, schoolID, homeVisitData.StudentID, "ParentSignature.png", homeVisitData.ParentSignature);
                        string studentSignatureUrl = UploadImageToCloudFromBase64(client, schoolID, homeVisitData.StudentID, "StudentSignature.png", homeVisitData.StudentSignature);
                        string teacherSignatureUrl = UploadImageToCloudFromBase64(client, schoolID, homeVisitData.StudentID, "TeacherSignature.png", homeVisitData.TeacherSignature);

                        // Find teacher id in classroom
                        int? teacherID = null;
                        var classMember = dbSchool.TClassMembers.Where(w => w.SchoolID == schoolID && w.nTerm == currentTermID && w.nTermSubLevel2 == homeVisitData.RoomID).FirstOrDefault();
                        if (classMember != null)
                        {
                            teacherID = classMember.nTeacherHeadid;
                        }

                        THomeVisit homeVisit = dbSchool.THomeVisit.Where(w => w.SchoolID == schoolID && w.StudentID == homeVisitData.StudentID && w.TermID == currentTermID && w.IsDel == false).FirstOrDefault();
                        if (homeVisit == null)
                        {
                            if (homeVisitData.Status == 3)
                            {
                                // 3 = เยื่ยมแต่ไม่พบ
                                homeVisit = new THomeVisit
                                {
                                    SchoolID = homeVisitData.SchoolID,
                                    RoomID = homeVisitData.RoomID,
                                    StudentID = homeVisitData.StudentID,
                                    TermID = currentTermID,
                                    YearID = currentYearID,
                                    Status = homeVisitData.Status,
                                    LINE = homeVisitData.LINE,
                                    Facebook = homeVisitData.Facebook,
                                    TeacherId = teacherID,
                                    IsDel = false,
                                    CreatedDate = DateTime.Now.FixSecond(15).FixMillisecond(1)
                                };
                            }
                            else
                            {
                                homeVisit = new THomeVisit
                                {
                                    SchoolID = homeVisitData.SchoolID,
                                    RoomID = homeVisitData.RoomID,
                                    StudentID = homeVisitData.StudentID,
                                    TermID = currentTermID,
                                    YearID = currentYearID,
                                    Status = homeVisitData.Status,
                                    LINE = homeVisitData.LINE,
                                    Facebook = homeVisitData.Facebook,

                                    // 1. ข้อมูลผู้ปกครอง
                                    HaveParents = homeVisitData.HaveParents,
                                    Relationship = homeVisitData.Relationship,
                                    RelationshipOther = homeVisitData.RelationshipOther,
                                    Fullname = homeVisitData.Fullname,
                                    PhoneNumber = homeVisitData.PhoneNumber,
                                    IDCardNumber = homeVisitData.IDCardNumber,
                                    Occupation = homeVisitData.Occupation,
                                    OccupationOther = homeVisitData.OccupationOther,
                                    HighestEducation = homeVisitData.HighestEducation,
                                    WelfareRegistersPoor = homeVisitData.WelfareRegistersPoor,
                                    // 2. ความสัมพันธ์ในครอบครัว
                                    ResidentialHouse = homeVisitData.ResidentialHouse,
                                    DormitoryLivingWith = homeVisitData.DormitoryLivingWith,
                                    ResidentialHouseOther = homeVisitData.ResidentialHouseOther,
                                    OwnHome = homeVisitData.OwnHome,
                                    Cleanliness = homeVisitData.Cleanliness,
                                    CleanlinessOther = homeVisitData.CleanlinessOther,
                                    UtilitiesElectricity = homeVisitData.UtilitiesElectricity,
                                    WaterForConsumption = homeVisitData.WaterForConsumption,
                                    Toilet = homeVisitData.Toilet,
                                    LivingEnvironment = homeVisitData.LivingEnvironment,
                                    StudentFamilyMembersAmount = homeVisitData.StudentFamilyMembersAmount,
                                    StudentFamilyMembersMale = homeVisitData.StudentFamilyMembersMale,
                                    StudentFamilyMembersFemale = homeVisitData.StudentFamilyMembersFemale,
                                    SiblingsBornSameParentsAmount = homeVisitData.SiblingsBornSameParentsAmount,
                                    SiblingsBornSameParentsMale = homeVisitData.SiblingsBornSameParentsMale,
                                    SiblingsBornSameParentsFemale = homeVisitData.SiblingsBornSameParentsFemale,
                                    SiblingsBornDifferentParentsAmount = homeVisitData.SiblingsBornDifferentParentsAmount,
                                    SiblingsBornDifferentParentsMale = homeVisitData.SiblingsBornDifferentParentsMale,
                                    SiblingsBornDifferentParentsFemale = homeVisitData.SiblingsBornDifferentParentsFemale,
                                    FamiliesNeedSpecialAssistance = homeVisitData.FamiliesNeedSpecialAssistance,
                                    FamiliesNeedSpecialAssistanceTotal = homeVisitData.FamiliesNeedSpecialAssistanceTotal,
                                    FamilyRelationship = homeVisitData.FamilyRelationship,
                                    FamilyRelationshipOther = homeVisitData.FamilyRelationshipOther,
                                    RelationshipMember = homeVisitData.RelationshipMember,
                                    SpendTimeWithFamily = homeVisitData.SpendTimeWithFamily,
                                    WorkloadTheirFamilies = homeVisitData.WorkloadTheirFamilies,
                                    LeisureActivities = homeVisitData.LeisureActivities,
                                    LeaveStudent = homeVisitData.LeaveStudent,
                                    LeaveStudentOther = homeVisitData.LeaveStudentOther,
                                    MedianHouseholdIncome = homeVisitData.MedianHouseholdIncome,
                                    ReceiveExpensesFrom = homeVisitData.ReceiveExpensesFrom,
                                    ReceiveExpensesFromOther = homeVisitData.ReceiveExpensesFromOther,
                                    StudentWorkIncome = homeVisitData.StudentWorkIncome,
                                    StudentWorkIncomeOther = homeVisitData.StudentWorkIncomeOther,
                                    DailyIncome = homeVisitData.DailyIncome,
                                    PaidComeDay = homeVisitData.PaidComeDay,
                                    ParentWantAgencyHelp2 = homeVisitData.ParentWantAgencyHelp,
                                    ParentWantAgencyHelp2Other = homeVisitData.ParentWantAgencyHelpOther,
                                    ParentConcerns = homeVisitData.ParentConcerns,
                                    ParentWantSchoolsHelp = homeVisitData.ParentWantSchoolsHelp,
                                    ParentWantSchoolsHelpOther = homeVisitData.ParentWantSchoolsHelpOther,
                                    // 3. พฤติกรรมและความเสี่ยง
                                    Health = homeVisitData.Health,
                                    WelfareSafety = homeVisitData.WelfareSafety,
                                    DistanceHomeToSchool = homeVisitData.DistanceHomeToSchool,
                                    TravelTime = homeVisitData.TravelTime,
                                    StudentTravel = homeVisitData.StudentTravel,
                                    StudentTravelOther = homeVisitData.StudentTravelOther,
                                    LivingConditions = homeVisitData.LivingConditions,
                                    LivingConditionsOther = homeVisitData.LivingConditionsOther,
                                    StudentResponsibilities = homeVisitData.StudentResponsibilities,
                                    StudentResponsibilitiesOther = homeVisitData.StudentResponsibilitiesOther,
                                    Hobbies = homeVisitData.Hobbies,
                                    HobbiesOther = homeVisitData.HobbiesOther,
                                    SubstanceAbuseBehavior = homeVisitData.SubstanceAbuseBehavior,
                                    ViolentBehavior = homeVisitData.ViolentBehavior,
                                    ViolentBehaviorOther = homeVisitData.ViolentBehaviorOther,
                                    SexualBehavior = homeVisitData.SexualBehavior,
                                    GameAddiction = homeVisitData.GameAddiction,
                                    GameAddictionOther = homeVisitData.GameAddictionOther,
                                    AccessComputerInternet = homeVisitData.AccessComputerInternet,
                                    UseElectronicTools = homeVisitData.UseElectronicTools,
                                    StudentInfoProvider = homeVisitData.StudentInfoProvider,
                                    // 4. บันทึกเยี่ยมบ้าน
                                    Note = homeVisitData.Note,
                                    HouseStyle = homeVisitData.HouseStyle,
                                    ParentSignature = !string.IsNullOrEmpty(parentSignatureUrl) ? parentSignatureUrl : null,
                                    StudentSignature = !string.IsNullOrEmpty(studentSignatureUrl) ? studentSignatureUrl : null,
                                    TeacherSignature = !string.IsNullOrEmpty(teacherSignatureUrl) ? teacherSignatureUrl : null,
                                    TeacherId = teacherID,

                                    IsDel = false,
                                    CreatedDate = DateTime.Now.FixSecond(15).FixMillisecond(5)
                                };
                            }

                            dbSchool.THomeVisit.Add(homeVisit);
                        }
                        else
                        {
                            if (homeVisitData.Status == 3)
                            {
                                homeVisit.YearID = currentYearID;

                                // 3 = เยื่ยมแต่ไม่พบ
                                homeVisit.Status = homeVisitData.Status;
                                homeVisit.TeacherId = teacherID;
                                homeVisit.UpdatedDate = DateTime.Now.FixSecond(15).FixMillisecond(10);
                            }
                            else
                            {
                                homeVisit.YearID = currentYearID;
                                homeVisit.LINE = homeVisitData.LINE;
                                homeVisit.Facebook = homeVisitData.Facebook;

                                // 1. ข้อมูลผู้ปกครอง
                                homeVisit.HaveParents = homeVisitData.HaveParents;
                                homeVisit.Relationship = homeVisitData.Relationship;
                                homeVisit.RelationshipOther = homeVisitData.RelationshipOther;
                                homeVisit.Fullname = homeVisitData.Fullname;
                                homeVisit.PhoneNumber = homeVisitData.PhoneNumber;
                                homeVisit.IDCardNumber = homeVisitData.IDCardNumber;
                                homeVisit.Occupation = homeVisitData.Occupation;
                                homeVisit.OccupationOther = homeVisitData.OccupationOther;
                                homeVisit.HighestEducation = homeVisitData.HighestEducation;
                                homeVisit.WelfareRegistersPoor = homeVisitData.WelfareRegistersPoor;
                                // 2. ความสัมพันธ์ในครอบครัว
                                homeVisit.ResidentialHouse = homeVisitData.ResidentialHouse;
                                homeVisit.DormitoryLivingWith = homeVisitData.DormitoryLivingWith;
                                homeVisit.ResidentialHouseOther = homeVisitData.ResidentialHouseOther;
                                homeVisit.OwnHome = homeVisitData.OwnHome;
                                homeVisit.Cleanliness = homeVisitData.Cleanliness;
                                homeVisit.CleanlinessOther = homeVisitData.CleanlinessOther;
                                homeVisit.UtilitiesElectricity = homeVisitData.UtilitiesElectricity;
                                homeVisit.WaterForConsumption = homeVisitData.WaterForConsumption;
                                homeVisit.Toilet = homeVisitData.Toilet;
                                homeVisit.LivingEnvironment = homeVisitData.LivingEnvironment;
                                homeVisit.StudentFamilyMembersAmount = homeVisitData.StudentFamilyMembersAmount;
                                homeVisit.StudentFamilyMembersMale = homeVisitData.StudentFamilyMembersMale;
                                homeVisit.StudentFamilyMembersFemale = homeVisitData.StudentFamilyMembersFemale;
                                homeVisit.SiblingsBornSameParentsAmount = homeVisitData.SiblingsBornSameParentsAmount;
                                homeVisit.SiblingsBornSameParentsMale = homeVisitData.SiblingsBornSameParentsMale;
                                homeVisit.SiblingsBornSameParentsFemale = homeVisitData.SiblingsBornSameParentsFemale;
                                homeVisit.SiblingsBornDifferentParentsAmount = homeVisitData.SiblingsBornDifferentParentsAmount;
                                homeVisit.SiblingsBornDifferentParentsMale = homeVisitData.SiblingsBornDifferentParentsMale;
                                homeVisit.SiblingsBornDifferentParentsFemale = homeVisitData.SiblingsBornDifferentParentsFemale;
                                homeVisit.FamiliesNeedSpecialAssistance = homeVisitData.FamiliesNeedSpecialAssistance;
                                homeVisit.FamiliesNeedSpecialAssistanceTotal = homeVisitData.FamiliesNeedSpecialAssistanceTotal;
                                homeVisit.FamilyRelationship = homeVisitData.FamilyRelationship;
                                homeVisit.FamilyRelationshipOther = homeVisitData.FamilyRelationshipOther;
                                homeVisit.RelationshipMember = homeVisitData.RelationshipMember;
                                homeVisit.SpendTimeWithFamily = homeVisitData.SpendTimeWithFamily;
                                homeVisit.WorkloadTheirFamilies = homeVisitData.WorkloadTheirFamilies;
                                homeVisit.LeisureActivities = homeVisitData.LeisureActivities;
                                homeVisit.LeaveStudent = homeVisitData.LeaveStudent;
                                homeVisit.LeaveStudentOther = homeVisitData.LeaveStudentOther;
                                homeVisit.MedianHouseholdIncome = homeVisitData.MedianHouseholdIncome;
                                homeVisit.ReceiveExpensesFrom = homeVisitData.ReceiveExpensesFrom;
                                homeVisit.ReceiveExpensesFromOther = homeVisitData.ReceiveExpensesFromOther;
                                homeVisit.StudentWorkIncome = homeVisitData.StudentWorkIncome;
                                homeVisit.StudentWorkIncomeOther = homeVisitData.StudentWorkIncomeOther;
                                homeVisit.DailyIncome = homeVisitData.DailyIncome;
                                homeVisit.PaidComeDay = homeVisitData.PaidComeDay;
                                homeVisit.ParentWantAgencyHelp2 = homeVisitData.ParentWantAgencyHelp;
                                homeVisit.ParentWantAgencyHelp2Other = homeVisitData.ParentWantAgencyHelpOther;
                                homeVisit.ParentConcerns = homeVisitData.ParentConcerns;
                                homeVisit.ParentWantSchoolsHelp = homeVisitData.ParentWantSchoolsHelp;
                                homeVisit.ParentWantSchoolsHelpOther = homeVisitData.ParentWantSchoolsHelpOther;
                                // 3. พฤติกรรมและความเสี่ยง
                                homeVisit.Health = homeVisitData.Health;
                                homeVisit.WelfareSafety = homeVisitData.WelfareSafety;
                                homeVisit.DistanceHomeToSchool = homeVisitData.DistanceHomeToSchool;
                                homeVisit.TravelTime = homeVisitData.TravelTime;
                                homeVisit.StudentTravel = homeVisitData.StudentTravel;
                                homeVisit.StudentTravelOther = homeVisitData.StudentTravelOther;
                                homeVisit.LivingConditions = homeVisitData.LivingConditions;
                                homeVisit.LivingConditionsOther = homeVisitData.LivingConditionsOther;
                                homeVisit.StudentResponsibilities = homeVisitData.StudentResponsibilities;
                                homeVisit.StudentResponsibilitiesOther = homeVisitData.StudentResponsibilitiesOther;
                                homeVisit.Hobbies = homeVisitData.Hobbies;
                                homeVisit.HobbiesOther = homeVisitData.HobbiesOther;
                                homeVisit.SubstanceAbuseBehavior = homeVisitData.SubstanceAbuseBehavior;
                                homeVisit.ViolentBehavior = homeVisitData.ViolentBehavior;
                                homeVisit.ViolentBehaviorOther = homeVisitData.ViolentBehaviorOther;
                                homeVisit.SexualBehavior = homeVisitData.SexualBehavior;
                                homeVisit.GameAddiction = homeVisitData.GameAddiction;
                                homeVisit.GameAddictionOther = homeVisitData.GameAddictionOther;
                                homeVisit.AccessComputerInternet = homeVisitData.AccessComputerInternet;
                                homeVisit.UseElectronicTools = homeVisitData.UseElectronicTools;
                                homeVisit.StudentInfoProvider = homeVisitData.StudentInfoProvider;
                                // 4. บันทึกเยี่ยมบ้าน
                                homeVisit.Note = homeVisitData.Note;
                                homeVisit.HouseStyle = homeVisitData.HouseStyle;
                                if (!string.IsNullOrEmpty(parentSignatureUrl)) homeVisit.ParentSignature = parentSignatureUrl;
                                if (!string.IsNullOrEmpty(studentSignatureUrl)) homeVisit.StudentSignature = studentSignatureUrl;
                                if (!string.IsNullOrEmpty(teacherSignatureUrl)) homeVisit.TeacherSignature = teacherSignatureUrl;
                                homeVisit.TeacherId = teacherID;

                                homeVisit.UpdatedDate = DateTime.Now.FixSecond(15).FixMillisecond(15);
                            }
                        }
                        dbSchool.SaveChanges();

                        if (context.Request.Files.Count > 0 && homeVisitData.Status != 3)
                        {
                            for (int i = 0; i < context.Request.Files.Count; i++)
                            {
                                HttpPostedFile file = context.Request.Files[i];

                                string fileNameKey = context.Request.Files.AllKeys[i];
                                string[] preFileName = fileNameKey.Split('-'); // [0]: type, [1]: id, [2]: file name

                                int typeID = preFileName[0] == "out" ? 1 : 2; // 1 = out, 2 = in
                                int ID = Convert.ToInt32(preFileName[1]);

                                VisitHouseForm.HomeVisitFileData homeVisitFileData;
                                if (typeID == 1)
                                {
                                    // 1 = ภาพถ่ายสภาพบ้านนักเรียน
                                    homeVisitFileData = homeVisitData.HousePictureOutSide.Where(w => w.InDB == false && w.Status == "new" && w.Type == typeID && w.ID == ID).FirstOrDefault();
                                }
                                else
                                {
                                    // 2 = ภาพถ่ายภายในบ้านนักเรียน
                                    homeVisitFileData = homeVisitData.HousePictureInSide.Where(w => w.InDB == false && w.Status == "new" && w.Type == typeID && w.ID == ID).FirstOrDefault();
                                }

                                if (homeVisitFileData != null)
                                {
                                    string imageUrl = UploadImageToCloudFromHttpPostedFile(client, schoolID, homeVisitData.StudentID, file);

                                    THomeVisitFile homeVisitFile = new THomeVisitFile
                                    {
                                        Type = homeVisitFileData.Type,
                                        HomeVisitID = homeVisit.ID,
                                        SchoolID = schoolID,
                                        ContentType = homeVisitFileData.ContentType,
                                        FileName = homeVisitFileData.FileName,
                                        Path = imageUrl,

                                        IsDel = false,
                                        CreatedDate = DateTime.Now.FixSecond(16).FixMillisecond(1)
                                    };
                                    dbSchool.THomeVisitFiles.Add(homeVisitFile);
                                    dbSchool.SaveChanges();
                                }
                            }
                        }

                        // 1 = ภาพถ่ายสภาพบ้านนักเรียน
                        List<VisitHouseForm.HomeVisitFileData> homeVisitFileDatas = homeVisitData.HousePictureOutSide.Where(w => w.InDB == true && w.Status == "delete" && w.Type == 1).ToList();
                        foreach (var file in homeVisitFileDatas)
                        {
                            THomeVisitFile homeVisitFile = dbSchool.THomeVisitFiles.Where(w => w.SchoolID == schoolID && w.HomeVisitID == homeVisit.ID && w.Type == 1 && w.ID == file.ID && w.IsDel == false).FirstOrDefault();
                            if (homeVisitFile != null)
                            {
                                homeVisitFile.IsDel = true;
                                homeVisitFile.UpdatedDate = DateTime.Now.FixSecond(16).FixMillisecond(10);
                                dbSchool.SaveChanges();
                            }
                        }

                        // 2 = ภาพถ่ายภายในบ้านนักเรียน
                        homeVisitFileDatas = homeVisitData.HousePictureInSide.Where(w => w.InDB == true && w.Status == "delete" && w.Type == 2).ToList();
                        foreach (var file in homeVisitFileDatas)
                        {
                            THomeVisitFile homeVisitFile = dbSchool.THomeVisitFiles.Where(w => w.SchoolID == schoolID && w.HomeVisitID == homeVisit.ID && w.Type == 2 && w.ID == file.ID && w.IsDel == false).FirstOrDefault();
                            if (homeVisitFile != null)
                            {
                                homeVisitFile.IsDel = true;
                                homeVisitFile.UpdatedDate = DateTime.Now.FixSecond(16).FixMillisecond(15);
                                dbSchool.SaveChanges();
                            }
                        }

                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            //Send File details in a JSON Response.
            string json = new JavaScriptSerializer().Serialize(new { success, message });
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "text/json";
            context.Response.Write(json);
            context.Response.End();
        }

        public bool IsReusable { get { return false; } }

        public string UploadImageToCloudFromBase64(ObsClient client, int schoolID, int studentID, string fileName, string base64Image)
        {
            string urlImageCloud = "";

            try
            {
                if (!string.IsNullOrEmpty(base64Image))
                {
                    //data:image/gif;base64,
                    //this image is a single pixel (black)
                    byte[] bytes = Convert.FromBase64String(base64Image);

                    Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(ms);
                    }

                    string tempPath = HttpContext.Current.Server.MapPath("~/Images/" + Guid.NewGuid().ToString());
                    Directory.CreateDirectory(tempPath);

                    string fullFilePath = tempPath + "/" + fileName;

                    image.Save(fullFilePath, System.Drawing.Imaging.ImageFormat.Png);

                    PutObjectRequest request = new PutObjectRequest()
                    {
                        BucketName = "homevisit",
                        ObjectKey = schoolID + "/" + studentID + "/" + fileName,
                        FilePath = fullFilePath,
                    };

                    PutObjectResponse response = client.PutObject(request);
                    urlImageCloud = response.ObjectUrl;

                    if (Directory.Exists(tempPath))
                    {
                        Directory.Delete(tempPath, true);
                    }
                }
            }
            catch { }

            return urlImageCloud;
        }

        public string UploadImageToCloudFromHttpPostedFile(ObsClient client, int schoolID, int studentID, HttpPostedFile httpPostedFile)
        {
            string urlImageCloud = "";

            try
            {
                string tempPath = HttpContext.Current.Server.MapPath("~/Images/" + Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempPath);

                string fileName = Path.GetFileName(httpPostedFile.FileName);
                string guidFileName = Guid.NewGuid().ToString() + Path.GetExtension(httpPostedFile.FileName);

                string fullFilePath = tempPath + "/" + fileName;

                httpPostedFile.SaveAs(fullFilePath);

                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = "homevisit",
                    ObjectKey = schoolID + "/" + studentID + "/" + guidFileName,
                    FilePath = fullFilePath,
                };

                PutObjectResponse response = client.PutObject(request);
                urlImageCloud = response.ObjectUrl;

                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
            }
            catch { }

            return urlImageCloud;
        }


    }
}