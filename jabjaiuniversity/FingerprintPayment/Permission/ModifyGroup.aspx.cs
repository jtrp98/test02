using JabjaiEntity.DB;
using MasterEntity;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Permission
{
    public partial class ModifyGroup : BasePermissionPage
    {
        public int? GroupID { get; set; }
        public bool IsAdmin { get; set; }

        public List<MenuGroupModel> ListMenu { get; set; }
        public List<MenuGroupModel> ListMenuGroupless { get; set; }

        public List<UserGroupModel> ListSelectedUser { get; set; }
        public List<UserGroupModel> ListAllUser { get; set; }

        //public ModifyGroup()
        //{
        //    ListMenu = new List<MenuGroupModel>();
        //    ListSelectedUser = new List<UserGroupModel>();
        //    ListAllUser = new List<UserGroupModel>();
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GroupID = ToNullableInt(Request.QueryString["gid"] + "");

                OnInitData();
            }
        }

        private void OnInitData()
        {
            var schoolID = UserData.CompanyID;
            var isDev = ConfigurationManager.AppSettings["Menu_demo"] + "" == "1";
            var q1 = $@"
SELECT A.groupmenuid 'GroupID', A.groupmenu 'GroupMenu', B.ID 'SegmentID', B.Name 'SegmentName', C.MenuId , C.MenuName  , D.Role , 'W' 'Type'

FROM TGroupMenu A 
LEFT JOIN TSegmentMenu B ON A.groupmenuid = B.GroupMenuID
LEFT JOIN TMenu C ON C.SegmentID = B.ID
LEFT JOIN TGroupPermissionMenu D ON C.MenuId = D.MenuID AND D.type = 'W' AND D.SchoolID = {schoolID} {(GroupID.HasValue ? " AND D.GroupID  = " + GroupID : " AND D.GroupID  IS NULL ")} 

WHERE  C.MenuId is not null AND A.groupmenuid not in (27) and ISNULL(C.IsExceptAuth , 0) = 0 AND A.active = 1 AND B.Active = 1 AND C.active = 1 
{(isDev ? "" : " AND C.demo = 0 ")} 
ORDER by A.new_order , B.nOrder , C.nMenuOrder2 
";

            var q2 = $@"
SELECT A.Menu_Id 'GroupID' , A.Menu_Name 'GroupMenu' , B.Menu_Id 'MenuID' , B.Menu_Name 'MenuName'  , C.Role ,'M' 'Type' ,  A.OrderNo 'OrderNo1' , B.OrderNo 'OrderNo2'

FROM TMobileMenu A
LEFT JOIN TMobileMenu B ON A.Menu_Id = B.SubMenu_Id 
LEFT JOIN TGroupPermissionMenu C ON C.MenuID = B.Menu_Id  AND C.Type = 'M' AND C.SchoolID =  {schoolID} {(GroupID.HasValue ? " AND C.GroupID  = " + GroupID : " AND C.GroupID  IS NULL")} 

WHERE  A.SubMenu_Id is null AND B.SubMenu_Id is not null and ISNULL(A.IsExceptAuth , 0) = 0 
ORDER by A.Menu_Id 
";

            var q3 = $@"
SELECT 0 'GroupID', '' 'GroupMenu',0 'SegmentID', '' 'SegmentName', A.MenuId ,A.MenuName  , B.Role , 'W' 'Type'
FROM  TMenu A 
LEFT JOIN TGroupPermissionMenu B ON A.MenuID = B.MenuID  AND B.Type = 'W' AND B.SchoolID =  {schoolID} {(GroupID.HasValue ? " AND B.GroupID  = " + GroupID : " AND B.GroupID  IS NULL")} 

WHERE  A.MenuId in (209)  and A.IsExceptAuth = 0  AND A.active = 1 {(isDev ? "" : " AND A.demo = 0 ")}

";

            var _selectedUser = new List<int>();
            var _doneUser = new List<ModelDoneUser>();

            using (var db = Connection.MasterEntities(ConnectionDB.Read))
            {
                if (GroupID.HasValue)
                {
                    var group = db.TGroupPermission
                        .FirstOrDefault(o => o.GroupID == this.GroupID && o.SchoolID == schoolID && o.IsDel == false && o.IsActive == true);

                    if (group != null)
                    {
                        txtGroupName.Text = group.GroupName;
                        lblGroupName.Text = group.GroupName;

                        if (group.GroupName == "แอดมิน/ผู้บริหาร" && group.IsEditable == false)
                        {
                            IsAdmin = true;
                        }

                        if (group.IsEditable == false)
                        {
                            txtGroupName.Attributes["style"] = "display:none;";
                        }
                        else
                        {
                            lblGroupName.Attributes["style"] = "display:none;";
                        }
                    }

                    _selectedUser = db.TGroupPermissionUser
                        .Where(o => o.SchoolID == schoolID && o.GroupID == GroupID && o.IsDel == false && o.IsActive == true)
                        .Select(o => o.UserID)
                        .ToList();
                }
                else
                {
                    lblGroupName.Attributes["style"] = "display:none;";
                }
                var d1 = db.Database.SqlQuery<MenuGroupModel>(q1).ToList();

                var d2 = db.Database.SqlQuery<MenuGroupModel>(q2).ToList();

                var d3 = db.Database.SqlQuery<MenuGroupModel>(q3).ToList();

                ListMenu = d1.Concat(d2).ToList();

                ListMenuGroupless = d3;

                _doneUser = (from a in db.TGroupPermissionUser
                    .Where(o => o.SchoolID == schoolID && o.GroupID != GroupID && o.IsDel == false && o.IsActive == true)
                             from b in db.TGroupPermission.Where(o => o.SchoolID == a.SchoolID && o.IsDel == false && o.IsActive == true && o.GroupID == a.GroupID)

                             select new ModelDoneUser
                             {
                                 UserID = a.UserID,
                                 GroupName = b.GroupName
                             })
                    .Distinct()
                    .ToList();
            }

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                var dateNow = DateTime.Today;
                var qryEmp = from a in db.TEmployees.Where(o => o.SchoolID == schoolID && o.cDel == null)
                             from b in db.TEmployeeInfoes.Where(o => o.sEmp == a.sEmp && o.SchoolID == a.SchoolID).DefaultIfEmpty()
                             from c in db.TEmpSalaries.Where(w => w.SchoolID == a.SchoolID && w.sEmp == a.sEmp).DefaultIfEmpty()

                             where c == null || ((c.WorkStatus == 1 && (dateNow >= c.WorkInEducationDate || c.WorkInEducationDate == null)) || (c.WorkStatus == 2 && dateNow <= c.DayQuit))

                             select new UserGroupModel
                             {
                                 UserID = a.sEmp,
                                 FullName = a.sName + " " + a.sLastname,
                                 Code = b.Code,
                             };

                ListAllUser = (from a in qryEmp.AsEnumerable()
                               from b in _doneUser.Where(o => o.UserID == a.UserID).DefaultIfEmpty()

                               select new UserGroupModel
                               {
                                   UserID = a.UserID,
                                   FullName = a.FullName,
                                   Code = a.Code,
                                   IsSelectable = (b == null),
                                   Remark = b?.GroupName,
                               })
                               .OrderBy(o => o.Code)
                               .ToList();

                ListSelectedUser = new List<UserGroupModel>();

                if (_selectedUser.Count() > 0)
                {
                    ListSelectedUser = ListAllUser
                        .Where(o => _selectedUser.Contains(o.UserID))
                        .Select(o => new UserGroupModel
                        {
                            UserID = o.UserID,
                            FullName = o.FullName,
                            Code = o.Code,
                        })
                        .ToList();

                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object AddOrModifyGroup(AddOrModifyGroupModel model)
        {
            var isSuccess = false;
            var msg = "";
            var userData = GetUserData();
            var logText = "";

            using (var db = Connection.MasterEntities(ConnectionDB.Read))
            {
                
                    try
                    {
                        var log = new TGroupPermission_Log();
                        log.Date = DateTime.Now;
                        log.ByUser = userData.Name;
                        log.SchoolID = userData.CompanyID;
                        
                        TGroupPermission group;

                        if (model.GroupID.HasValue)//modify
                        {
                            group = db.TGroupPermission.FirstOrDefault(o => o.GroupID == model.GroupID && o.SchoolID == userData.CompanyID);
                            group.GroupName = model.GroupName;
                            group.Modified = DateTime.Now;
                            group.ModifyBy = userData.UserID;

                            db.TGroupPermissionMenu.RemoveRange(db.TGroupPermissionMenu.Where(o => o.GroupID == model.GroupID));
                            db.TGroupPermissionUser.RemoveRange(db.TGroupPermissionUser.Where(o => o.GroupID == model.GroupID));

                            logText += $"แก้ไขกลุ่ม : {group.GroupName}";
                        }
                        else
                        {
                            group = new TGroupPermission()
                            {
                                GroupName = model.GroupName,
                                SchoolID = userData.CompanyID,
                                IsActive = true,
                                IsDel = false,
                                Created = DateTime.Now,
                                CreateBy = userData.UserID,
                                Modified = DateTime.Now,
                                ModifyBy = userData.UserID,
                                IsEditable = true
                            };

                            db.TGroupPermission.Add(group);

                            logText += $"เพื่มกลุ่ม : {group.GroupName}";
                        }

                        db.SaveChanges();

                        foreach (int u in model.SelectedUser.Where(o => o.HasValue))
                        {
                            db.TGroupPermissionUser.Add(new TGroupPermissionUser()
                            {
                                GroupID = group.GroupID,
                                UserID = u,
                                IsDel = false,
                                IsActive = true,
                                SchoolID = userData.CompanyID
                            });
                        }

                        foreach (var m in model.SelectedMenu)
                        {
                            db.TGroupPermissionMenu.Add(new TGroupPermissionMenu()
                            {
                                GroupID = group.GroupID,
                                MenuID = m.MenuId,
                                Role = m.Role,
                                Type = m.Type,
                                IsDel = false,
                                IsActive = true,
                                SchoolID = userData.CompanyID
                            });
                        }

                        db.SaveChanges();

                        log.Text = logText;
                        db.TGroupPermission_Log.Add(log);
                        db.SaveChanges();

                     
                       
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                       

                        isSuccess = false;

                        msg = ex.Message + ex.StackTrace;
                    }
                
            }
            return new { status = isSuccess, msg = msg };
        }

        private class ModelDoneUser
        {
            public int UserID { get; set; }
            public string GroupName { get; set; }
        }
    }
}