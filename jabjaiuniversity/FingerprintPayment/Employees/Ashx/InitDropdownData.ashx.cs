using FingerprintPayment.Employees.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Employees.Ashx
{
    /// <summary>
    /// Summary description for InitDropdownData
    /// </summary>
    public class InitDropdownData : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string table = HttpContext.Current.Request.QueryString["table"];

            var json = "";
            List<object> result = new List<object>();

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
            {
                switch (table)
                {
                    case "WorkStatus":
                        //result = en.TWorkStatus.Select(s => new { id = s.WORKSTATUS_ID, value = s.WORKSTATUS_NAME }).ToList<object>();
                        break;
                    case "Title":
                        result = en.TTitleLists.Select(s => new { id = s.nTitleid, value = s.titleDescription }).ToList<object>();
                        break;
                    case "Department":
                        result = en.TDepartments.Select(s => new { id = s.DepID, value = s.departmentName }).ToList<object>();
                        break;
                    case "BloodType":
                        //using (JabJaiMasterEntities dbsmaster = Connection.MasterEntities())
                        //{
                        //    result = dbsmaster.TBloodGroups.Select(s => new { id = s.BLOODGROUP_ID, value = s.BLOODGROUP_NAME }).ToList<object>();
                        //}
                        break;
                    case "OfficialCategory":
                        result.Add(new { id = "1", value = "ประเภทราชการ 1" });
                        result.Add(new { id = "2", value = "ประเภทราชการ 2" });
                        result.Add(new { id = "3", value = "ประเภทราชการ 3" });
                        result.Add(new { id = "4", value = "ประเภทราชการ 4" });
                        break;
                    case "Nationality":
                        //result = en.TNationalities.Select(s => new { id = s.NATIONALITY_ID, value = s.NATIONALITY_NAME }).ToList<object>();
                        break;
                    case "Race":
                        //result = en.Traces.Select(s => new { id = s.RACE_ID, value = s.RACE_NAME }).ToList<object>();
                        break;
                    case "Religion":
                        //using (JabJaiMasterEntities dbsmaster = Connection.MasterEntities())
                        //{
                        //    result = dbsmaster.TReligions.Select(s => new { id = s.RELIGION_ID, value = s.RELIGION_NAME }).ToList<object>();
                        //}
                        break;
                    case "PersonalStatus":
                    case "MaritalStatus":
                        //using (JabJaiMasterEntities dbsmaster = Connection.MasterEntities())
                        //{
                        //    result = dbsmaster.TMaritalStatus.Select(s => new { id = s.MARITALSTATUS_ID, value = s.MARITALSTATUS_NAME }).ToList<object>();
                        //}
                        break;
                    case "GPFMember":
                        result.Add(new { id = "1", value = "กบข. 1" });
                        result.Add(new { id = "2", value = "กบข. 2" });
                        result.Add(new { id = "3", value = "กบข. 3" });
                        result.Add(new { id = "4", value = "กบข. 4" });
                        break;
                    case "GPFAccumulation":
                        result.Add(new { id = "1", value = "กบข. 1" });
                        result.Add(new { id = "2", value = "กบข. 2" });
                        result.Add(new { id = "3", value = "กบข. 3" });
                        result.Add(new { id = "4", value = "กบข. 4" });
                        break;
                    case "District":
                        using (JabJaiMasterEntities dbsmaster = Connection.MasterEntities())
                        {
                            result = dbsmaster.districts.Select(s => new { id = s.DISTRICT_ID, value = s.DISTRICT_NAME }).ToList<object>();
                        }
                        break;
                    case "Amphur":
                        using (JabJaiMasterEntities dbsmaster = Connection.MasterEntities())
                        {
                            result = dbsmaster.amphurs.Select(s => new { id = s.AMPHUR_ID, value = s.AMPHUR_NAME }).ToList<object>();
                        }
                        break;
                    case "Province":
                        using (JabJaiMasterEntities dbsmaster = Connection.MasterEntities())
                        {
                            result = dbsmaster.provinces.Select(s => new { id = s.PROVINCE_ID, value = s.PROVINCE_NAME }).ToList<object>();
                        }
                        break;
                    case "Year":
                        for (int i = 2502; i < 2602; i++)
                        {
                            result.Add(new { id = i, value = i });
                        }
                        break;
                    case "Level":
                        result = en.TLevels.Select(s => new { id = s.LevelID, value = s.LevelName }).ToList<object>();
                        break;
                    case "Bursary":
                        //result = en.TBursaries.Select(s => new { id = s.BURSARY_ID, value = s.BURSARY_NAME }).ToList<object>();
                        break;
                    case "Qualification":
                        //result = en.TQualifications.Select(s => new { id = s.QUALIFI_ID, value = s.QUALIFI_NAME }).ToList<object>();
                        break;
                    case "Country":
                        using (JabJaiMasterEntities dbsmaster = Connection.MasterEntities())
                        {
                            result = dbsmaster.TCountries.Select(s => new { id = s.COUNTRY_ID, value = s.COUNTRY_NAME }).ToList<object>();
                        }
                        break;
                    case "Rank":
                        //result = en.TRanks.Select(s => new { id = s.RANK_ID, value = s.RANK_NAME }).ToList<object>();
                        break;
                    case "Job":
                        result = en.TJobLists.Select(s => new { id = s.nJobid, value = s.jobDescription }).ToList<object>();
                        break;
                    case "FamilyRelationship":
                        //using (JabJaiMasterEntities dbsmaster = Connection.MasterEntities())
                        //{
                        //    result = dbsmaster.TFamilyRelationships.Select(s => new { id = s.FAMILYRELATION_ID, value = s.FAMILYRELATION_NAME }).ToList<object>();
                        //}
                        break;
                    case "Redemption":
                        //result = en.TRedemptions.Select(s => new { id = s.REDEM_ID, value = s.REDEM_NAME }).ToList<object>();
                        break;
                    case "RedemptionType":
                        //result = en.TRedemptionTypes.Select(s => new { id = s.REDEMTYPE_ID, value = s.REDEMTYPE_NAME }).ToList<object>();
                        break;
                    case "EducationRight":
                        //result = en.TEducationRights.Select(s => new { id = s.EDURIGHT_ID, value = s.EDURIGHT_NAME }).ToList<object>();
                        break;
                    case "LiveStatus":
                        //using (JabJaiMasterEntities dbsmaster = Connection.MasterEntities())
                        //{
                        //    result = dbsmaster.TLiveStatus.Select(s => new { id = s.LIVESTATUS_ID, value = s.LIVESTATUS_NAME }).ToList<object>();
                        //}
                        break;
                    case "MedicalRight":
                        //result = en.TMedicalRights.Select(s => new { id = s.MEDICRIGHT_ID, value = s.MEDICRIGHT_NAME }).ToList<object>();
                        break;
                    case "LicenseExtension":
                        result.Add(new { id = "1", value = "การต่อใบอนุญาติ 1" });
                        result.Add(new { id = "2", value = "การต่อใบอนุญาติ 2" });
                        result.Add(new { id = "3", value = "การต่อใบอนุญาติ 3" });
                        result.Add(new { id = "4", value = "การต่อใบอนุญาติ 4" });
                        result.Add(new { id = "5", value = "การต่อใบอนุญาติ 5" });
                        result.Add(new { id = "6", value = "การต่อใบอนุญาติ 6" });
                        break;
                    case "Term":
                        result = en.TTerms.Select(s => new { id = s.nTerm, value = s.sTerm }).ToList<object>();
                        break;
                    case "DirectTeaching":
                    case "CompetentTeaching":
                    case "Subject":
                    case "WantTrain":
                        //result = en.TSubjects.Select(s => new { id = s.SUBJECT_ID, value = s.SUBJECT_NAME }).ToList<object>();
                        break;
                    case "CourseType":
                        result = en.TCourseTypes.Select(s => new { id = s.courseTypeId, value = s.Description }).ToList<object>();
                        break;
                    case "Class":
                        result = en.TClasses.Select(s => new { id = s.sClassID, value = s.sClass }).ToList<object>();
                        break;
                    case "Room":
                        result = en.TRooms.Select(s => new { id = s.sRoomID, value = s.sRoomName }).ToList<object>();
                        break;

                }
            }

            json = JsonConvert.SerializeObject(result);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}