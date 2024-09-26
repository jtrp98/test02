
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FingerprintPayment.Leave
{
    public partial class TeacherConfig : Page
    {
        public class Model2Save
        {
            public int PKID { get; set; }
            public Nullable<byte> YearStart { get; set; }
            public Nullable<byte> YearEnd { get; set; }
            public List<Model2SaveSub> List { get; set; }

            public class Model2SaveSub
            {
                public int TypeID { get; set; }
                public Nullable<short> Day { get; set; }

            }
        }

        public class EmpJob
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Job { get; set; }
        }

        public List<EmpJob> EmpJobList { get; set; }
        public List<TLeave_Type> TypeList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var qryEmp = from a in db.TEmployees.Where(o => o.SchoolID == userData.CompanyID && o.cDel == null)
                             from b in db.TJobLists.Where(o => o.SchoolID == userData.CompanyID && o.nJobid == a.nJobid)
                             select new EmpJob
                             {
                                 Id = a.sEmp,
                                 Name = a.sName + " " + a.sLastname,
                                 Job = b.jobDescription,
                             };

                EmpJobList = qryEmp.ToList();

                var ddlEmpList = EmpJobList.Select(o => new ListItem
                {
                    Value = o.Id + "",
                    Text = o.Name,
                }).OrderBy(o => o.Text).ToList();
                ddlEmpList.Insert(0, new ListItem() { Value = "", Text = "เลือก" });

                selectEmpList.DataSource = ddlEmpList;
                selectEmpList.DataBind();

                ddlNotiEmp.DataSource = ddlEmpList;
                ddlNotiEmp.DataBind();

                TypeList = db.TLeave_Type
                   .Where(o => o.SchoolID == userData.CompanyID && o.Type == "T" && o.IsDel == false && o.IsActive == true)
                  .OrderBy(o => o.Created)
                  .ToList();

            }
        }

        [WebMethod(EnableSession = true)]
        public static object SaveData11(byte? num)
        {
            var userData = GetUserData();
            var logs = new List<FormLog>();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var data = db.TLeave_ConfigTeacher.Where(o => o.SchoolID == userData.CompanyID).FirstOrDefault();

                var log = new TLeave_Log();
                log.Date = DateTime.Now;
                log.Type = "2";
                log.SchoolID = userData.CompanyID;
                log.ByUser = userData.Name;
                log.Text = $"แก้จำนวนผู้ตรวจสอบเป็น {num}";
                db.TLeave_Log.Add(log);

                if (data != null)
                {
                    data.ApproveNum = num;
                    data.ModifyBy = userData.UserID;
                    data.Modified = DateTime.Now;
                }
                else
                {
                    data = new TLeave_ConfigTeacher();
                    data.ApproveNum = num;
                    data.IsDel = false;
                    data.SchoolID = userData.CompanyID;
                    data.CreateBy = userData.UserID;
                    data.Created = DateTime.Now;
                    data.ModifyBy = userData.UserID;
                    data.Modified = DateTime.Now;

                    db.TLeave_ConfigTeacher.Add(data);
                }

                db.SaveChanges();

                return new
                {
                    text = "success"
                };
            }

        }

        [WebMethod(EnableSession = true)]
        public static object SaveData12(int depId, int? user1, string name1, int? user2, string name2, int? user3, string name3)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var data = db.TDepartments.Where(o => o.SchoolID == userData.CompanyID && o.DepID == depId).FirstOrDefault();

                if (data != null)
                {
                    data.userHeadId = user1;
                    data.userApproveOne = user2;
                    data.userApproveTwo = user3;
                    data.UpdatedDate = DateTime.Now;
                    data.UpdatedBy = userData.UserID;

                    var log = new TLeave_Log();
                    log.Date = DateTime.Now;
                    log.Type = "2";
                    log.SchoolID = userData.CompanyID;
                    log.ByUser = userData.Name;
                    log.Text = $"แก้ไข แผนก{data.departmentName} ผู้อนุมัติคนที่1เป็น {(!string.IsNullOrEmpty(name1) ? name1 : "-")} ,ผู้อนุมัติคนที่2เป็น {(!string.IsNullOrEmpty(name2) ? name2 : "-")} ,ผู้อนุมัติคนที่3เป็น {(!string.IsNullOrEmpty(name3) ? name3 : "-")}";
                    db.TLeave_Log.Add(log);

                    db.SaveChanges();
                }

                return new
                {
                    text = "success"
                };
            }

        }

        [WebMethod(EnableSession = true)]
        public static object SaveData2(List<Model2Save> models, string delId)
        {
            var userData = GetUserData();
            var logs = new List<FormLog>();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var arrDelId = delId?.Split(',').Where(o => !string.IsNullOrEmpty(o)).Select(o => int.Parse(o));
                var listData = db.TLeave_ConfigTeacher_Day.Where(o => o.SchoolID == userData.CompanyID).ToList();
                var listDataDayList = db.TLeave_ConfigTeacher_DayList.Where(o => o.SchoolID == userData.CompanyID).ToList();
                var leaveType = db.TLeave_Type.Where(o => o.SchoolID == userData.CompanyID && o.IsDel == false).ToList();

                foreach (var i in models)
                {
                    var obj = listData.FirstOrDefault(o => o.PKID == i.PKID);

                    var remark = "";
                    if (obj == null)
                    {
                        remark += "เพิ่ม";
                        obj = new TLeave_ConfigTeacher_Day();
                        obj.SchoolID = userData.CompanyID;
                        obj.YearStart = i.YearStart;
                        obj.YearEnd = i.YearEnd;
                        //obj.Goverment = i.Goverment;
                        //obj.Maternity = i.Maternity;
                        //obj.Sick = i.Sick;
                        //obj.Military = i.Military;
                        //obj.Personal = i.Personal;
                        //obj.Ordination = i.Ordination;
                        //obj.Vacation = i.Vacation;
                        //obj.Training = i.Training;
                        obj.Created = DateTime.Now;
                        obj.CreateBy = userData.UserID;
                        obj.Modified = DateTime.Now;
                        obj.ModifyBy = userData.UserID;
                        obj.IsDel = false;

                        db.TLeave_ConfigTeacher_Day.Add(obj);
                    }
                    else
                    {
                        remark += "แก้ไข";
                        obj.YearStart = i.YearStart;
                        obj.YearEnd = i.YearEnd;
                        //obj.Goverment = i.Goverment;
                        //obj.Maternity = i.Maternity;
                        //obj.Sick = i.Sick;
                        //obj.Military = i.Military;
                        //obj.Personal = i.Personal;
                        //obj.Ordination = i.Ordination;
                        //obj.Vacation = i.Vacation;
                        //obj.Training = i.Training;
                        obj.Modified = DateTime.Now;
                        obj.ModifyBy = userData.UserID;
                    }

                    db.SaveChanges();

                    remark += $@"การตั้งค่าจำนวนการลาประจำปีเป็น จำนวนปีทำงาน {i.YearStart}-{i.YearEnd}ปี";

                    var count = 1;
                    foreach (var day in i.List)
                    {
                        var dataDay = listDataDayList.Where(o => o.DayID == obj.PKID && o.TypeID == day.TypeID).FirstOrDefault();
                        var type = leaveType.Where(o => o.TypeID == day.TypeID).FirstOrDefault();

                        if (dataDay == null)
                        {
                            dataDay = new TLeave_ConfigTeacher_DayList();
                            dataDay.SchoolID = userData.CompanyID;
                            dataDay.TypeID = day.TypeID;
                            dataDay.DayID = obj.PKID;
                            dataDay.Day = day.Day;
                            dataDay.IsDel = false;

                            db.TLeave_ConfigTeacher_DayList.Add(dataDay);
                        }
                        else
                        {
                            dataDay.Day = day.Day;
                        }

                        remark += $",{count++} {type?.TypeName} :{day.Day} วันต่อปี";
                    }

                    var log = new TLeave_Log();
                    log.Date = DateTime.Now;
                    log.Type = "2";
                    log.SchoolID = userData.CompanyID;
                    log.ByUser = userData.Name;
                    log.Text = remark;
                    db.TLeave_Log.Add(log);
                }

                foreach (var id in arrDelId)
                {
                    var i = listData.FirstOrDefault(o => o.PKID == id);
                    i.IsDel = true;
                    i.Modified = DateTime.Now;
                    i.ModifyBy = userData.UserID;

                    var remark = $@"ลบการตั้งค่าจำนวนการลาประจำปี จำนวนปีทำงาน {i.YearStart}-{i.YearEnd}ปี";
                    var dayList = listDataDayList.Where(o => o.DayID == id).ToList();

                    foreach (var d in dayList)
                    {
                        d.IsDel = true;
                    }

                    var log = new TLeave_Log();
                    log.Date = DateTime.Now;
                    log.Type = "2";
                    log.SchoolID = userData.CompanyID;
                    log.ByUser = userData.Name;
                    log.Text = remark;
                    db.TLeave_Log.Add(log);
                }

                db.SaveChanges();

                return new
                {
                    text = "success"
                };
            }

        }

        [WebMethod(EnableSession = true)]
        public static object SaveData4(List<TLeave_ConfigTeacher_Notify> models, string delId, string addId44, string delId44)
        {
            var userData = GetUserData();
            var logs = new List<FormLog>();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var arrDelId = delId?.Split(',').Where(o => !string.IsNullOrEmpty(o)).Select(o => int.Parse(o));
                var listData = db.TLeave_ConfigTeacher_Notify.Where(o => o.SchoolID == userData.CompanyID).ToList();


                foreach (var i in models)
                {
                    var obj = listData.FirstOrDefault(o => o.NotiID == i.NotiID);

                    var remark = "";
                    if (obj == null)
                    {
                        remark += "เพิ่ม";
                        obj = new TLeave_ConfigTeacher_Notify();
                        obj.SchoolID = userData.CompanyID;
                        obj.NotiType = i.NotiType;
                        obj.No = i.No;
                        obj.Time1 = i.Time1;
                        //obj.Time2 = i.Time2;
                        obj.Created = DateTime.Now;
                        obj.CreateBy = userData.UserID;
                        obj.Modified = DateTime.Now;
                        obj.ModifyBy = userData.UserID;
                        obj.IsDel = false;

                        db.TLeave_ConfigTeacher_Notify.Add(obj);
                    }
                    else
                    {
                        if (obj.No == i.No && obj.NotiType == i.NotiType && obj.Time1 == i.Time1)
                        {
                            continue;
                        }

                        remark += "แก้ไข";
                        obj.NotiType = i.NotiType;
                        obj.No = i.No;
                        obj.Time1 = i.Time1;
                        //obj.Time2 = i.Time2;
                        obj.Modified = DateTime.Now;
                        obj.ModifyBy = userData.UserID;
                    }

                    switch (i.NotiType)
                    {
                        case 1:
                            remark += $@" 4.1 แจ้งเตือนจำนวนการมาสาย เตือนครั้งที่ {i.No}, สาย {i.Time1} ครั้ง";
                            break;
                        case 2:
                            remark += $@" 4.2 การแจ้งเตือนวันหมดอายุ VISA เตือนครั้งที่ {i.No}, ก่อนครบกำหนด {i.Time1} วัน";
                            break;
                        case 3:
                            remark += $@" 4.3 การแจ้งเตือนวันหมดอายุ Work Permit เตือนครั้งที่ {i.No}, ก่อนครบกำหนด {i.Time1} วัน";
                            break;
                        default:
                            break;
                    }

                    var log = new TLeave_Log();
                    log.Date = DateTime.Now;
                    log.Type = "2";
                    log.SchoolID = userData.CompanyID;
                    log.ByUser = userData.Name;
                    log.Text = remark;
                    db.TLeave_Log.Add(log);
                }

                foreach (var id in arrDelId)
                {
                    var i = listData.FirstOrDefault(o => o.NotiID == id);
                    i.IsDel = true;
                    i.Modified = DateTime.Now;
                    i.ModifyBy = userData.UserID;
                    var remark = "";
                    switch (i.NotiType)
                    {
                        case 1:
                            remark += $@"ลบการตั้งค่า 4.1 แจ้งเตือนจำนวนการมาสาย เตือนครั้งที่ {i.No}, สาย {i.Time1} ครั้ง";
                            break;
                        case 2:
                            remark += $@"ลบการตั้งค่า 4.2 การแจ้งเตือนวันหมดอายุ VISA เตือนครั้งที่ {i.No}, ก่อนครบกำหนด {i.Time1} วัน";
                            break;
                        case 3:
                            remark += $@"ลบการตั้งค่า 4.3 การแจ้งเตือนวันหมดอายุ Work Permit เตือนครั้งที่ {i.No}, ก่อนครบกำหนด {i.Time1} วัน";
                            break;
                        default:
                            break;
                    }

                    var log = new TLeave_Log();
                    log.Date = DateTime.Now;
                    log.Type = "2";
                    log.SchoolID = userData.CompanyID;
                    log.ByUser = userData.Name;
                    log.Text = remark;
                    db.TLeave_Log.Add(log);
                }

                var listDataNotiUser = db.TLeave_ConfigTeacher_NotifyUser.Where(o => o.SchoolID == userData.CompanyID).ToList();
                var empList = (from a in db.TEmployees.Where(o => o.SchoolID == userData.CompanyID && o.cDel == null)
                               from b in db.TTitleLists.Where(o => o.nTitleid + "" == a.sTitle && o.SchoolID == a.SchoolID).DefaultIfEmpty()
                               select new
                               {
                                   a.sEmp,
                                   teachTitle = (b == null ? a.sTitle : b.titleDescription),
                                   teacher = a.sName + " " + a.sLastname,
                               }).ToList();

                var arrAdd44 = addId44?.Split(',').Where(o => !string.IsNullOrEmpty(o)).Select(o => int.Parse(o));
                var arrDel44 = delId44?.Split(',').Where(o => !string.IsNullOrEmpty(o)).Select(o => int.Parse(o));

                foreach (var id in arrAdd44)
                {
                    var emp = empList.FirstOrDefault(o => o.sEmp == id);
                    var notiUser = listDataNotiUser.FirstOrDefault(o => o.TeacherID == id);
                    if (notiUser == null)
                    {
                        var obj = new TLeave_ConfigTeacher_NotifyUser();
                        obj.SchoolID = userData.CompanyID;
                        obj.TeacherID = id;
                        obj.Created = DateTime.Now;
                        obj.CreateBy = userData.UserID;
                        obj.Modified = DateTime.Now;
                        obj.ModifyBy = userData.UserID;
                        obj.IsDel = false;
                        db.TLeave_ConfigTeacher_NotifyUser.Add(obj);

                        var log = new TLeave_Log();
                        log.Date = DateTime.Now;
                        log.Type = "2";
                        log.SchoolID = userData.CompanyID;
                        log.ByUser = userData.Name;
                        log.Text = $"เพิ่มรายชื่อ 4.4 ผู้มีสิทธิ์ในการรับการแจ้งเตือน {emp?.teachTitle} {emp?.teacher}";
                        db.TLeave_Log.Add(log);

                    }

                }

                foreach (var id in arrDel44)
                {
                    var notiUser = listDataNotiUser.FirstOrDefault(o => o.TeacherID == id);
                    if (notiUser != null)
                    {
                        notiUser.IsDel = true;
                        notiUser.Modified = DateTime.Now;
                        notiUser.ModifyBy = userData.UserID;

                        var emp = empList.FirstOrDefault(o => o.sEmp == id);
                        var log = new TLeave_Log();
                        log.Date = DateTime.Now;
                        log.Type = "2";
                        log.SchoolID = userData.CompanyID;
                        log.ByUser = userData.Name;
                        log.Text = $"ลบรายชื่อ 4.4 ผู้มีสิทธิ์ในการรับการแจ้งเตือน {emp?.teachTitle} {emp?.teacher}";
                        db.TLeave_Log.Add(log);
                    }
                }

                db.SaveChanges();

                return new
                {
                    text = "success"
                };
            }

        }

        [WebMethod(EnableSession = true)]
        public static object OnEnableOrDisable(bool isEnable)
        {
            var userData = GetUserData();
            var logs = new List<FormLog>();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var data = db.TLeave_ConfigTeacher.Where(o => o.SchoolID == userData.CompanyID).FirstOrDefault();

                var log = new TLeave_Log();
                log.Date = DateTime.Now;
                log.Type = "2";
                log.SchoolID = userData.CompanyID;
                log.ByUser = userData.Name;
                log.Text = $"{(isEnable ? "เปิด" : "ปิด")}การใช้งาน ตั้งค่าจำนวนการลาประจำปี";
                db.TLeave_Log.Add(log);

                if (data != null)
                {
                    data.IsEnable = isEnable;
                    data.ModifyBy = userData.UserID;
                    data.Modified = DateTime.Now;
                }
                else
                {
                    data = new TLeave_ConfigTeacher();
                    data.IsEnable = isEnable;
                    data.IsDel = false;
                    data.SchoolID = userData.CompanyID;
                    data.CreateBy = userData.UserID;
                    data.Created = DateTime.Now;
                    data.ModifyBy = userData.UserID;
                    data.Modified = DateTime.Now;

                    db.TLeave_ConfigTeacher.Add(data);
                }

                var empList = new List<string>();
                if (isEnable)
                {
                    var qry = from a in db.TEmployees.Where(o => o.SchoolID == userData.CompanyID && o.cDel != "1")
                              from b in db.TEmpSalaries.Where(o => o.SchoolID == a.SchoolID && o.sEmp == a.sEmp && o.WorkStatus == 1).DefaultIfEmpty()
                              select new { a.sEmp, a.sName, a.sLastname, b.WorkInEducationDate };
                    var lst = qry.ToList();

                    var c = 1;
                    foreach (var i in lst.Where(o => !o.WorkInEducationDate.HasValue))
                    {
                        empList.Add($"{c++}.{i.sName} {i.sLastname}");
                    }
                }

                if(empList.Count == 0)
                {
                    db.SaveChanges();
                }

                return new
                {
                    text = "success",
                    employeeList = string.Join("<br>", empList),
                };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object SaveCutOffDay(DateTime day)
        {
            var userData = GetUserData();
            var logs = new List<FormLog>();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var data = db.TLeave_ConfigTeacher.Where(o => o.SchoolID == userData.CompanyID).FirstOrDefault();

                var log = new TLeave_Log();
                log.Date = DateTime.Now;
                log.Type = "2";
                log.SchoolID = userData.CompanyID;
                log.ByUser = userData.Name;
                log.Text = $"บันทึกวันตัดรอบปีการทำงานเป็นวันที่ {day.ToString("dd/MM", new CultureInfo("th-TH"))}";
                db.TLeave_Log.Add(log);

                if (data != null)
                {
                    data.CutOffDay = day;
                    data.ModifyBy = userData.UserID;
                    data.Modified = DateTime.Now;
                }
                else
                {
                    data = new TLeave_ConfigTeacher();
                    data.CutOffDay = day;
                    data.IsDel = false;
                    data.SchoolID = userData.CompanyID;
                    data.CreateBy = userData.UserID;
                    data.Created = DateTime.Now;
                    data.ModifyBy = userData.UserID;
                    data.Modified = DateTime.Now;

                    db.TLeave_ConfigTeacher.Add(data);
                }

                db.SaveChanges();

                return new
                {
                    text = "success"
                };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object GetData()
        {
            var userData = GetUserData();
            using (var dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {

                //var empList = db.TEmployees.Where(o => o.SchoolID == userData.CompanyID && o.cDel == null)
                //    .Select(o => new
                //    {
                //        user = o.sEmp,
                //        name = o.sName + " " + o.sLastname,
                //    }).OrderBy(o => o.name).ToList();

                var data1 = db.TLeave_ConfigTeacher.Where(o => o.SchoolID == userData.CompanyID && o.IsDel == false)
                     .AsEnumerable()
                     .Select(o => new
                     {
                         id = o.SetID,
                         approveNum = o.ApproveNum,
                         isEnable = o.IsEnable,
                         cutOffDay = o.CutOffDay?.ToString("dd/MM", new CultureInfo("th-TH")),
                     })
                    .FirstOrDefault();

                var data12 = (from a in db.TDepartments.Where(o => o.SchoolID == userData.CompanyID && o.cDel == false)
                                  //from b1 in db.TEmployees.Where(o => o.SchoolID == userData.CompanyID && o.sEmp == a.userHeadId).DefaultIfEmpty()
                                  //from b2 in db.TEmployees.Where(o => o.SchoolID == userData.CompanyID && o.sEmp == a.userApproveOne).DefaultIfEmpty()
                                  //from b3 in db.TEmployees.Where(o => o.SchoolID == userData.CompanyID && o.sEmp == a.userApproveTwo).DefaultIfEmpty()

                              select new
                              {
                                  a.DepID,
                                  a.departmentName,
                                  user1 = a.userHeadId,
                                  //name1 = b1.sName + " " + b1.sLastname,
                                  user2 = a.userApproveOne,
                                  //name2 = b2.sName + " " + b2.sLastname,
                                  user3 = a.userApproveTwo,
                                  //name3 = b3.sName + " " + b3.sLastname,
                              }).OrderBy(o => o.departmentName).ToList();

                var data2 = (from a in db.TLeave_ConfigTeacher_Day.Where(o => o.SchoolID == userData.CompanyID && o.IsDel == false)
                             select new
                             {
                                 id = a.PKID,
                                 year1 = a.YearStart,
                                 year2 = a.YearEnd,
                                 list = db.TLeave_ConfigTeacher_DayList.Where(o => o.SchoolID == a.SchoolID && o.DayID == a.PKID)
                                 .Select(o => new
                                 {
                                     dayId = o.DayID,
                                     day = o.Day,
                                     typeId = o.TypeID,
                                 }),
                                 //sick = o.Sick,
                                 //personal = o.Personal,
                                 //vacation = o.Vacation,
                                 //military = o.Military,
                                 //maternity = o.Maternity,
                                 //ordination = o.Ordination,
                                 //goverment = o.Goverment,
                                 //training = o.Training,
                             }).OrderBy(o => o.id).ToList();

                //var emp = new List<Model1>();
                //var company = dbMaster.TCompanies.FirstOrDefault(o => o.nCompany == userData.CompanyID);
                //emp.Add(new Model1 { Order = 1, Id = company.nSchoolHeadid, Type = "ผู้อำนวยการโรงเรียน" });
                //emp.Add(new Model1 { Order = 2, Id = company.nRegistraDirectorid, Type = "หัวหน้านายทะเบียน/หัวหน้าฝ่ายวัดผลและประเมินผล" });
                //emp.Add(new Model1 { Order = 3, Id = company.nAcademicDirectorid, Type = "รองผู้อำนวยการฝ่ายวิชาการ/ผู้ช่วยผู้อำนวยการฝ่ายวิชาการ/หัวหน้าฝ่ายวิชาการ" });
                ////arrEmp.Add(new Model1 { Id = company.nAcademicSubDirectorid, Type = "รองผู้อำนวยการฝ่ายวิชาการ/ผู้ช่วยผู้อำนวยการฝ่ายวิชาการ" });
                //emp.Add(new Model1 { Order = 4, Id = company.nAccountingDirectorid, Type = "รองผู้อำนวยการฝ่ายการเงิน/หัวหน้าฝ่ายการเงิน" });
                //emp.Add(new Model1 { Order = 5, Id = company.nPersonnel, Type = "รองผู้อำนวยการฝ่ายบุคคล/หัวหน้าฝ่ายบุคคล" });
                //emp.Add(new Model1 { Order = 6, Id = company.nStudentDevelopmentDirectorid, Type = "รองผู้อำนวยการฝ่ายพัฒนานักเรียน/หัวหน้าฝ่ายปกครอง" });
                //emp.Add(new Model1 { Order = 7, Id = company.nGM, Type = "รองผู้อำนวยการฝ่ายบริหารทั่วไป/หัวหน้าฝ่ายบริหารทั่วไป" });
                //emp.Add(new Model1 { Order = 8, Id = company.nWebAdminid, Type = "ผู้ดูแลระบบ" });
                //var arrEmp = emp.Select(o => o.Id).ToArray();
                //var empList = (from a in db.TEmployees.Where(o => o.SchoolID == userData.CompanyID && arrEmp.Contains(o.sEmp))
                //             .Select(o => new { o.sEmp, o.sName, o.sLastname })
                //             .AsEnumerable()
                //               from b in emp.Where(o => o.Id == a.sEmp)
                //               select new
                //               {
                //                   Order = b.Order,
                //                   Id = a.sEmp,
                //                   Name = a.sName + " " + a.sLastname,
                //                   Position = b.Type,
                //               }).OrderBy(o => o.Order).ToList();

                //var logs = db.TLeave_Log.Where(o => o.SchoolID == userData.CompanyID && o.Type == "2")
                // .OrderByDescending(o => o.Date)
                // .Take(20)
                // .ToList();

                //var sbLogs = new StringBuilder();
                //if (logs != null)
                //{
                //    foreach (var item in logs)
                //    {
                //        sbLogs.Append($"<strong>{item.Date?.ToString("dd/MM/yyyy HH:mm ", new CultureInfo("th-TH"))}</strong> {item.Text}, ผู้ทำรายการ : {item.ByUser}<br/>");
                //    }
                //}

                var notify = db.TLeave_ConfigTeacher_Notify.Where(o => o.SchoolID == userData.CompanyID && o.IsDel == false)
                    .Select(o => new
                    {
                        id = o.NotiID,
                        type = o.NotiType,
                        no = o.No,
                        time1 = o.Time1,
                        time2 = o.Time2,
                    })
                    .ToList();

                var notiUserList = (from a in db.TLeave_ConfigTeacher_NotifyUser.Where(o => o.SchoolID == userData.CompanyID && o.IsDel == false)
                                    from b in db.TEmployees.Where(o => o.sEmp == a.TeacherID && o.SchoolID == userData.CompanyID).DefaultIfEmpty()
                                    from c in db.TJobLists.Where(o => o.SchoolID == userData.CompanyID && o.nJobid == b.nJobid).DefaultIfEmpty()
                                    select new EmpJob
                                    {
                                        Id = a.TeacherID,
                                        Name = b.sName + " " + b.sLastname,
                                        Job = c.jobDescription + "",
                                    }).ToList();

                return new
                {
                    data1 = data1,
                    data12 = data12,
                    data2 = data2,
                    data41 = notify.Where(o => o.type == 1).OrderBy(o => o.no),
                    data42 = notify.Where(o => o.type == 2).OrderBy(o => o.no),
                    data43 = notify.Where(o => o.type == 3).OrderBy(o => o.no),
                    data44 = notify.Where(o => o.type == 4).OrderBy(o => o.no),
                    data45 = notiUserList,
                    //emp = empList,
                    //logs = sbLogs.ToString(),
                    text = "success"
                };
            }

        }

        public static JWTToken.userData GetUserData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Default.aspx");
            }

            return userData;
        }

        public class Search
        {
            public string text { get; set; }
        }
        public class FormLog
        {
            public DateTime Date { get; set; }
            public string Text { get; set; }
            public string By { get; set; }

        }

        private class Model1
        {
            public int Order { get; set; }

            public int? Id { get; set; }
            public string Type { get; set; }
            public string EmpName { get; set; }
        }
    }


}