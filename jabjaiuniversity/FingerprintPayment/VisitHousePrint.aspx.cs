using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class VisitHousePrint : VisitHouseGateway
    {
        // 1
        protected string studentName = "";
        protected string studentLastName = "";
        protected string studentLevel = "";
        protected string[] studentIdentification = new string[] { "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;" };
        // 2
        protected string parentName = "";
        protected string parentLastName = "";
        protected string parentTel = "";
        protected string noParent = "deactive";
        protected string parentRelationship = "";
        protected string parentCareer = "";
        protected string parentEducation = "";
        protected string[] parentIdentification = new string[] { "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;", "&nbsp;" };
        protected string noCard = "deactive";
        protected string poorRegister = "deactive";
        // 3
        // 3.1
        protected string visitHouseTimeTogether = "";
        // 3.2
        protected string[] visitHouseFatherRelationsLevel = new string[] { "", "", "", "", "" };
        protected string[] visitHouseMotherRelationsLevel = new string[] { "", "", "", "", "" };
        protected string[] visitHouseBrotherRelationsLevel = new string[] { "", "", "", "", "" };
        protected string[] visitHouseSistersRelationsLevel = new string[] { "", "", "", "", "" };
        protected string[] visitHouseGrandparentsRelationsLevel = new string[] { "", "", "", "", "" };
        protected string[] visitHouseRelativeRelationsLevel = new string[] { "", "", "", "", "" };
        protected string[] visitHouseOtherRelationsLevel = new string[] { "", "", "", "", "" };
        protected string visitHouseOtherRelationsLevelSpecify = "";
        // 3.3
        protected string[] visitHouseTakeCareChildren = new string[] { "deactive", "deactive", "deactive", "deactive" };
        protected string visitHouseTakeCareChildrenOther = "";
        // 3.4
        protected string visitHouseHouseholdIncome = "";
        // 3.5
        protected string visitHouseExpensesFrom = "";
        protected string visitHouseExtraWork = "";
        protected string visitHouseExtraWorkIncome = "";
        protected string visitHouseCarryMoneySchool = "";
        // 3.6
        protected string[] visitHouseHelpFromSchool = new string[] { "deactive", "deactive", "deactive", "deactive" };
        protected string visitHouseHelpFromSchoolOther = "";
        // 3.7
        protected string[] visitHouseHelpFamilyReceived = new string[] { "deactive", "deactive", "deactive" };
        protected string visitHouseHelpFamilyReceivedOther = "";
        // 3.8
        protected string[] visitHouseParentsConcerns = new string[] { "", "", "" };
        // 4
        // 4.1
        protected string[] visitHouseHealth = new string[] { "deactive", "deactive", "deactive", "deactive", "deactive" };
        // 4.2
        protected string[] visitHouseWelfare = new string[] { "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive" };
        // 4.3
        protected string visitHouseDistanceSchool = "";
        protected string visitHouseTimeSchoolHour = "";
        protected string visitHouseTimeSchoolMinute = "";
        protected string[] visitHouseTravelMethod = new string[] { "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive" };
        protected string visitHouseTravelMethodOther = "";
        // 4.4
        protected string[] visitHouseLivingConditions = new string[] { "deactive", "deactive" };
        // 4.5
        protected string[] visitHouseStudentWorkFamily = new string[] { "deactive", "deactive", "deactive", "deactive", "deactive", "deactive" };
        protected string visitHouseStudentWorkFamilyOther = "";
        // 4.6
        protected string[] visitHouseHobby = new string[] { "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive" };
        protected string visitHouseHobbyOther = "";
        // 4.7
        protected string[] visitHouseSubstanceAbuseBehavior = new string[] { "deactive", "deactive", "deactive", "deactive", "deactive" };
        // 4.8
        protected string[] visitHouseViolenceBehavior = new string[] { "deactive", "deactive", "deactive", "deactive", "deactive" };
        // 4.9
        protected string[] visitHouseSexualBehavior = new string[] { "deactive", "deactive", "deactive", "deactive", "deactive", "deactive" };
        // 4.10
        protected string[] visitHouseGameAddiction = new string[] { "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive" };
        protected string visitHouseGameAddictionOther = "";
        // 4.11
        protected string[] visitHouseInternetAccess = new string[] { "deactive", "deactive" };
        // 4.12
        protected string[] visitHouseUsingElectronicTools = new string[] { "deactive", "deactive", "deactive", "deactive" };

        protected string[] visitHouseInformant = new string[] { "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive", "deactive" };

        protected string visitHousePhotosOutsideHome = "data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs%3D";
        protected string visitHousePhotosInsideHome = "data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs%3D";

        protected void Page_Load(object sender, EventArgs e)
        {
            string year = Request.QueryString["year"];
            string vid = Request.QueryString["vid"];

            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                int iYear = 0;
                if (!int.TryParse(year, out iYear)) { iYear = 0; }
                int iID = 0;
                if (!int.TryParse(vid, out iID)) { iID = 0; }

                TVisitHouse v = en.TVisitHouses.Where(w => w.SchoolID == schoolID && w.Year == iYear && w.VisitHouseID == iID).FirstOrDefault();
                if (v != null)
                {
                    if (v.sID != null)
                    {
                        JabjaiEntity.DB.TUser u = en.TUser.Where(w => w.sID == v.sID).FirstOrDefault();
                        if (u != null)
                        {
                            studentName = u.sName;
                            studentLastName = u.sLastname;

                            TYear y = en.TYears.Where(w => w.numberYear == iYear && w.cDel == false).FirstOrDefault();
                            if (y != null)
                            {
                                TTerm t = en.TTerms.Where(w => w.nYear == y.nYear).FirstOrDefault();
                                if (t != null)
                                {
                                    TStudentClassroomHistory sc = en.TStudentClassroomHistories.Where(w => w.sID == v.sID && w.nTerm == t.nTerm).FirstOrDefault();
                                    if (sc != null)
                                    {
                                        TTermSubLevel2 ts = en.TTermSubLevel2.Where(w => w.nTermSubLevel2 == sc.nTermSubLevel2).FirstOrDefault();
                                        if (ts != null)
                                        {
                                            TSubLevel sl = en.TSubLevels.Where(w => w.nTSubLevel == ts.nTSubLevel).FirstOrDefault();
                                            if (sl != null)
                                            {
                                                studentLevel = sl.SubLevel;
                                            }
                                        }
                                    }
                                }
                            }

                            for (int i = 0; i < u.sIdentification.Length; i++)
                            {
                                studentIdentification[i] = u.sIdentification[i].ToString();
                            }

                        }
                    }

                    visitHouseTimeTogether = (v.TimeTogether == null ? null : v.TimeTogether.Value.ToString());
                    if (v.FatherRelationsLevel != null) visitHouseFatherRelationsLevel[v.FatherRelationsLevel.Value - 1] = "&#10004;";
                    if (v.MotherRelationsLevel != null) visitHouseMotherRelationsLevel[v.MotherRelationsLevel.Value - 1] = "&#10004;";
                    if (v.BrotherRelationsLevel != null) visitHouseBrotherRelationsLevel[v.BrotherRelationsLevel.Value - 1] = "&#10004;";
                    if (v.SistersRelationsLevel != null) visitHouseSistersRelationsLevel[v.SistersRelationsLevel.Value - 1] = "&#10004;";
                    if (v.RelativeRelationsLevel != null) visitHouseRelativeRelationsLevel[v.RelativeRelationsLevel.Value - 1] = "&#10004;";

                    if (v.TakeCareChildren != null) visitHouseTakeCareChildren[v.TakeCareChildren.Value - 1] = "active";
                    visitHouseTakeCareChildrenOther = v.TakeCareChildrenOther;

                    visitHouseHouseholdIncome = (v.HouseholdIncome == null ? null : v.HouseholdIncome.Value.ToString("#,0.00"));

                    visitHouseExpensesFrom = v.ExpensesFrom;
                    visitHouseExtraWork = v.ExtraWork;
                    visitHouseExtraWorkIncome = (v.ExtraWorkIncome == null ? null : v.ExtraWorkIncome.Value.ToString("#,0.00"));
                    visitHouseCarryMoneySchool = (v.CarryMoneySchool == null ? null : v.CarryMoneySchool.Value.ToString("#,0.00"));

                    if (v.HelpFromSchool != null) visitHouseHelpFromSchool[v.HelpFromSchool.Value - 1] = "active";
                    visitHouseHelpFromSchoolOther = v.HelpFromSchoolOther;

                    if (v.HelpFamilyReceived != null) visitHouseHelpFamilyReceived[v.HelpFamilyReceived.Value - 1] = "active";
                    visitHouseHelpFamilyReceivedOther = v.HelpFamilyReceivedOther;

                    if (!string.IsNullOrEmpty(v.ParentsConcerns))
                    {
                        string[] result = Regex.Split(v.ParentsConcerns, "\n\\s*");
                        for (int i = 0; i < result.Length && i < 3; i++)
                        {
                            visitHouseParentsConcerns[i] = result[i];
                        }
                    }

                    if (v.Health != null) visitHouseHealth[v.Health.Value - 1] = "active";

                    if (v.Welfare != null) visitHouseWelfare[v.Welfare.Value - 1] = "active";

                    visitHouseDistanceSchool = (v.DistanceSchool == null ? null : v.DistanceSchool.Value.ToString("0"));
                    visitHouseTimeSchoolHour = (v.TimeSchoolHour == null ? null : v.TimeSchoolHour.Value.ToString("0"));
                    visitHouseTimeSchoolMinute = (v.TimeSchoolMinute == null ? null : v.TimeSchoolMinute.Value.ToString("0"));
                    if (v.TravelMethod != null) visitHouseTravelMethod[v.TravelMethod.Value - 1] = "active";
                    visitHouseTravelMethodOther = v.TravelMethodOther;

                    if (v.LivingConditions != null) visitHouseLivingConditions[v.LivingConditions.Value - 1] = "active";

                    if (v.StudentWorkFamily != null) visitHouseStudentWorkFamily[v.StudentWorkFamily.Value - 1] = "active";
                    visitHouseStudentWorkFamilyOther = v.StudentWorkFamilyOther;

                    if (v.Hobby != null) visitHouseHobby[v.Hobby.Value - 1] = "active";
                    visitHouseHobbyOther = v.HobbyOther;

                    if (v.SubstanceAbuseBehavior != null) visitHouseSubstanceAbuseBehavior[v.SubstanceAbuseBehavior.Value - 1] = "active";

                    if (v.ViolenceBehavior != null) visitHouseViolenceBehavior[v.ViolenceBehavior.Value - 1] = "active";

                    if (v.SexualBehavior != null) visitHouseSexualBehavior[v.SexualBehavior.Value - 1] = "active";

                    if (v.GameAddiction != null) visitHouseGameAddiction[v.GameAddiction.Value - 1] = "active";
                    visitHouseGameAddictionOther = v.GameAddictionOther;

                    if (v.InternetAccess != null) visitHouseInternetAccess[v.InternetAccess.Value - 1] = "active";

                    if (v.UsingElectronicTools != null) visitHouseUsingElectronicTools[v.UsingElectronicTools.Value - 1] = "active";

                    if (v.Informant != null) visitHouseInformant[v.Informant.Value - 1] = "active";

                    if (!string.IsNullOrEmpty(v.PhotosOutsideHome))
                    {
                        visitHousePhotosOutsideHome = string.Format(@"/Handles/GetImageHandler.ashx?cl=visit-out&im={0}&year={1}&id={2}", v.PhotosOutsideHome, year, vid);
                    }

                    if (!string.IsNullOrEmpty(v.PhotosInsideHome))
                    {
                        visitHousePhotosInsideHome = string.Format(@"/Handles/GetImageHandler.ashx?cl=visit-in&im={0}&year={1}&id={2}", v.PhotosInsideHome, year, vid);
                    }

                }
            }
        }
    }
}