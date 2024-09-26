using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Message.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;

namespace FingerprintPayment.Message
{
    public partial class GroupForm : MessageGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                //// Get current year
                //StudentLogic studentLogic = new StudentLogic(en);
                //string currentTerm = studentLogic.GetTermId(UserData);

                var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                foreach (var l in listLevel)
                {
                    this.ltrLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);
                }

            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadTermSubLevel2(string subLevelID)
        {
            List<EntityDropdown> result = null;

            if (!string.IsNullOrEmpty(subLevelID))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    int slID = Convert.ToInt32(subLevelID);

                    string query = string.Format(@"
SELECT r.id, r.name
FROM
(
	SELECT CAST(t.nTermSubLevel2 AS VARCHAR(10)) 'id', s.SubLevel + ' / ' + t.nTSubLevel2 'name', s.SubLevel 'sort1', (CASE WHEN ISNUMERIC(t.nTSubLevel2) = 1 THEN RIGHT('0000' + t.nTSubLevel2, 5) ELSE t.nTSubLevel2 END) 'sort2'
	FROM TTermSubLevel2 t 
	LEFT JOIN TSubLevel s ON t.nTSubLevel = s.nTSubLevel 
	WHERE t.SchoolID = {0} AND t.nTSubLevel = {1} AND t.nTermSubLevel2Status = '1' AND t.nWorkingStatus = 1
) r
ORDER BY r.sort1, r.sort2", schoolID, slID);
                    result = dbschool.Database.SqlQuery<EntityDropdown>(query).ToList();
                }
            }

            return result;
        }

        [WebMethod]
        public static object SearchData(SearchOption searchOption)
        {
            bool success = true;
            string message = "Success";

            List<UserList> result = null;

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    if (searchOption.type == "") throw new Exception(string.Format(@"กรุณาระบุประเภท"));

                    string condition = "";
                    string query = "";
                    if (searchOption.type == "0")
                    {
                        // Get current year
                        StudentLogic studentLogic = new StudentLogic(en);
                        string currentTerm = studentLogic.GetTermId(userData);

                        condition = "";
                        if (searchOption.levelID != null)
                        {
                            condition += " AND nTSubLevel = " + searchOption.levelID;
                        }
                        if (searchOption.roomIDs.Count() > 0)
                        {
                            condition += string.Format(@" AND nTermSubLevel2 IN ({0})", string.Join(", ", searchOption.roomIDs));
                        }
                        if (!string.IsNullOrEmpty(searchOption.name))
                        {
                            condition += string.Format(@" AND (sStudentID LIKE N'%{0}%' OR sName LIKE N'%{0}%' OR sLastname LIKE N'%{0}%')", searchOption.name);
                        }

                        // Student
                        query = string.Format(@"
SELECT sID 'ID', nStudentNumber 'Number', sStudentID 'Code', titleDescription 'Title', sName 'Name', sLastname 'Lastname', SubLevel 'Level', nTSubLevel2 'Room', 'student' 'Type'
FROM TB_StudentViews
WHERE SchoolID = {0} AND nTerm = '{1}' AND ISNULL(cDel, '0') = '0' {2}
ORDER BY SubLevel, nTSubLevel2, sStudentID", schoolID, currentTerm, condition);

                    }
                    else if (searchOption.type == "1")
                    {
                        condition = "";
                        if (!string.IsNullOrEmpty(searchOption.name))
                        {
                            condition += string.Format(@" AND (ei.Code LIKE N'%{0}%' OR e.sName LIKE N'%{0}%' OR e.sLastname LIKE N'%{0}%')", searchOption.name);
                        }

                        // Employee
                        query = string.Format(@"
SELECT e.sEmp 'ID', NULL 'Number', ei.Code 'Code', t.titleDescription 'Title', e.sName 'Name', e.sLastname 'Lastname', NULL 'Level', NULL 'Room', 'employee' 'Type'
FROM TEmployees e
LEFT JOIN TEmployeeInfo ei ON e.SchoolID = ei.SchoolID AND e.sEmp = ei.sEmp
LEFT JOIN TTitleList t ON e.sTitle = CAST(t.nTitleid AS VARCHAR(10))
WHERE e.SchoolID = {0} AND e.cDel IS NULL {1}
ORDER BY ei.Code, e.sName, e.sLastname", schoolID, condition);

                    }

                    result = en.Database.SqlQuery<UserList>(query).ToList();
                }
                catch (Exception err)
                {
                    success = false;
                    message = err.Message;
                }
            }

            return JsonConvert.SerializeObject(new { success, message, searchOption, data = result });
        }

        [WebMethod(EnableSession = true)]
        public static object GetData(string groupID)
        {
            bool success = true;
            string message = "Success";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;

            GroupData groupData = new GroupData();

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    if (!int.TryParse(groupID, out int iGroupID)) { iGroupID = 0; }

                    TSMSGroup smsGroup = en.TSMSGroup.Where(w => w.SchoolID == schoolID && w.SMSGroupID == iGroupID).FirstOrDefault();
                    if (smsGroup != null)
                    {
                        groupData.GroupID = smsGroup.SMSGroupID;
                        groupData.GroupName = smsGroup.SMSGroupName;
                        groupData.GroupNameEn = smsGroup.SMSGroupNameEn;
                        groupData.GroupDefault = smsGroup.GroupDefault;

                        groupData.UserData = new List<UserList>();

                        List<int> userIDs = en.TSMSGroupUser.Where(w => w.SchoolID == schoolID && w.SMSGroupID == iGroupID && w.cDel == false).Select(s => s.UserID).ToList();
                        if (userIDs.Count > 0)
                        {
                            // Get current year
                            StudentLogic studentLogic = new StudentLogic(en);
                            string currentTerm = studentLogic.GetTermId(userData);

                            List<UserList> userList = null;

                            string query = string.Format(@"
SELECT A.*
FROM
(
	SELECT sID 'ID', nStudentNumber 'Number', sStudentID 'Code', titleDescription 'Title', sName 'Name', sLastname 'Lastname', SubLevel 'Level', nTSubLevel2 'Room', 'student' 'Type'
	FROM TB_StudentViews
	WHERE SchoolID = {0} AND nTerm = '{1}' AND sID IN ({2})
	--ORDER BY SubLevel, nTSubLevel2, sStudentID
	UNION
	SELECT e.sEmp 'ID', NULL 'Number', ei.Code 'Code', t.titleDescription 'Title', e.sName 'Name', e.sLastname 'Lastname', NULL 'Level', NULL 'Room', 'employee' 'Type'
	FROM TEmployees e
	LEFT JOIN TEmployeeInfo ei ON e.SchoolID = ei.SchoolID AND e.sEmp = ei.sEmp
	LEFT JOIN TTitleList t ON e.sTitle = CAST(t.nTitleid AS VARCHAR(10))
	WHERE e.SchoolID = {0} AND e.cDel IS NULL AND e.sEmp IN ({2})
	--ORDER BY ei.Code, e.sName, e.sLastname
) A
ORDER BY A.Type, A.Code, A.Title, A.Name, A.Lastname", schoolID, currentTerm, string.Join<int>(",", userIDs));
                            userList = en.Database.SqlQuery<UserList>(query).ToList();

                            groupData.UserData = userList;
                        }
                    }
                    else
                    {
                        success = false;
                        message = "Data not found";
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    message = err.Message;
                }
            }

            return JsonConvert.SerializeObject(new { success, message, groupData });
        }

        [WebMethod(EnableSession = true)]
        public static object SaveGroup(GroupData groupData)
        {
            bool success = true;
            string message = "Success";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;


            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
               
                  

                    try
                    {
                        // Check exist group name in school
                        int existGroupName = en.TSMSGroup.Where(w => w.SchoolID == schoolID && w.cDel == false && w.SMSGroupID != groupData.GroupID && (w.SMSGroupName == groupData.GroupName.Trim() || w.SMSGroupNameEn == groupData.GroupNameEn.Trim())).Count();

                        if (existGroupName == 0)
                        {
                            if (groupData.GroupID == 0)
                            {
                                // Insert
                                // Save group
                                TSMSGroup g = new TSMSGroup
                                {
                                    SMSGroupName = groupData.GroupName,
                                    SMSGroupNameEn = groupData.GroupNameEn,
                                    Status = 1,
                                    SchoolID = schoolID,
                                    cDel = false,

                                    CreatedBy = userData.UserID,
                                    CreatedDate = DateTime.Now
                                };
                                en.TSMSGroup.Add(g);
                                en.SaveChanges();

                                // Save users
                                foreach (var u in groupData.Users)
                                {
                                    TSMSGroupUser gu = new TSMSGroupUser
                                    {
                                        SMSGroupID = g.SMSGroupID,
                                        UserID = u,
                                        SchoolID = schoolID,
                                        cDel = false,

                                        CreatedBy = userData.UserID,
                                        CreatedDate = DateTime.Now
                                    };
                                    en.TSMSGroupUser.Add(gu);
                                }
                                en.SaveChanges();

                                //transaction.Commit();
                            }
                            else
                            {
                                // Modify
                                TSMSGroup smsGroup = en.TSMSGroup.Where(w => w.SchoolID == schoolID && w.SMSGroupID == groupData.GroupID).FirstOrDefault();
                                if (smsGroup != null)
                                {
                                    // Save group
                                    smsGroup.SMSGroupName = groupData.GroupName;
                                    smsGroup.SMSGroupNameEn = groupData.GroupNameEn;
                                    smsGroup.UpdatedBy = userData.UserID;
                                    smsGroup.UpdatedDate = DateTime.Now;

                                    // Save users
                                    // Remove not contain current save
                                    en.TSMSGroupUser.Where(w => w.SchoolID == schoolID && w.SMSGroupID == groupData.GroupID && !groupData.Users.Contains(w.UserID)).ToList().ForEach(e => { e.cDel = true; e.UpdatedBy = userData.UserID; e.UpdatedDate = DateTime.Now; });

                                    // Save new userID
                                    foreach (var u in groupData.Users)
                                    {
                                        TSMSGroupUser smsGroupUser = en.TSMSGroupUser.Where(w => w.SchoolID == schoolID && w.SMSGroupID == groupData.GroupID && w.UserID == u).FirstOrDefault();
                                        if (smsGroupUser == null)
                                        {
                                            // Add new user
                                            smsGroupUser = new TSMSGroupUser
                                            {
                                                SMSGroupID = groupData.GroupID,
                                                UserID = u,
                                                SchoolID = schoolID,
                                                cDel = false,

                                                CreatedBy = userData.UserID,
                                                CreatedDate = DateTime.Now
                                            };
                                            en.TSMSGroupUser.Add(smsGroupUser);
                                        }
                                        else
                                        {
                                            // update user
                                            if ((bool)smsGroupUser.cDel)
                                            {
                                                smsGroupUser.cDel = false;
                                                smsGroup.UpdatedBy = userData.UserID;
                                                smsGroup.UpdatedDate = DateTime.Now;
                                            }
                                        }
                                    }
                                    en.SaveChanges();

                                    //transaction.Commit();
                                }
                                else
                                {
                                    success = false;
                                    message = "Data not found";
                                }
                            }
                        }
                        else
                        {
                            success = false;
                            message = "Duplicate Group name : " + groupData.GroupName;
                        }
                    }
                    catch (Exception err)
                    {
                        success = false;
                        message = err.Message;

                        //transaction.Rollback();
                    }
                
            }

            return JsonConvert.SerializeObject(new { success, message });
        }

        public class SearchOption
        {
            public int groupID { get; set; }
            public string type { get; set; }
            public int? levelID { get; set; }
            public int[] roomIDs { get; set; }
            public string name { get; set; }
        }

        public class UserList
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "number")]
            public int? Number { get; set; }

            [JsonProperty(PropertyName = "code")]
            public string Code { get; set; }

            [JsonProperty(PropertyName = "title")]
            public string Title { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "lastname")]
            public string Lastname { get; set; }

            [JsonProperty(PropertyName = "level")]
            public string Level { get; set; }

            [JsonProperty(PropertyName = "room")]
            public string Room { get; set; }

            [JsonProperty(PropertyName = "type")]
            public string Type { get; set; }
        }

        public class GroupData
        {
            [JsonProperty(PropertyName = "groupID")]
            public int GroupID { get; set; }

            [JsonProperty(PropertyName = "groupName")]
            public string GroupName { get; set; }

            [JsonProperty(PropertyName = "groupNameEn")]
            public string GroupNameEn { get; set; }

            [JsonProperty(PropertyName = "groupDefault")]
            public string GroupDefault { get; set; }

            [JsonProperty(PropertyName = "users")]
            public List<int> Users { get; set; }

            [JsonProperty(PropertyName = "userData")]
            public List<UserList> UserData { get; set; }
        }


    }
}