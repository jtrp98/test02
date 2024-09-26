using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterEntity;
using JabjaiEntity.DB;
using System.Globalization;
using System.Web.DynamicData;
using JabjaiMainClass;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace FingerprintPayment
{
    public partial class leaveSetting : System.Web.UI.Page
    {
        public TM_LeaveSettings f_data = new TM_LeaveSettings();
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            Button1.Click += new EventHandler(Button1_Click);
            Button2.Click += new EventHandler(Button2_Click);
            btnSave.Click += new EventHandler(btnSave_Click);

            f_data = getLeaveData();
            if (!Page.IsPostBack)
            {
                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"] + "";
                    using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                    {
                        int sEmpID = int.Parse(Session["sEmpID"] + "");
                        int? index = 0;
                        var companydata = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                        if (companydata.checker == null)
                        {
                            companydata.checker = 3;
                            _dbMaster.SaveChanges();
                        }

                        index = companydata.checker;

                        List<checkerList> checkerlist = new List<checkerList>();
                        checkerList checker = new checkerList();
                        int count = 1;

                        var emp = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).ToList();
                        var newList = emp.OrderBy(x => x.sName).ToList();

                        foreach (var t in newList)
                        {
                            var item = new ListItem
                            {
                                Text = t.sName + " " + t.sLastname,
                                Value = t.sEmp.ToString()
                            };
                            assistOne.Items.Add(item);
                        }
                        foreach (var t in newList)
                        {
                            var item = new ListItem
                            {
                                Text = t.sName + " " + t.sLastname,
                                Value = t.sEmp.ToString()
                            };
                            assistTwo.Items.Add(item);
                        }

                        var q = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).ToList();

                        foreach (var a in _db.TDepartments.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.deleted != 1))
                        {
                            checker = new checkerList();
                            checker.number = count;
                            count = count + 1;
                            checker.departmentName = a.departmentName;
                            var check1 = q.Where(w => w.sEmp == a.userHeadId).FirstOrDefault();
                            if (check1 != null)
                                checker.departmentHead = check1.sName + " " + check1.sLastname;
                            else checker.departmentHead = "";

                            if (a.userApproveOne != null)
                            {
                                var check2 = q.Where(w => w.sEmp == a.userApproveOne).FirstOrDefault();
                                if (check2 != null)
                                    checker.approveOne = check2.sName + " " + check2.sLastname;
                                else checker.approveOne = "";
                            }

                            if (a.userApproveTwo != null)
                            {
                                var check3 = q.Where(w => w.sEmp == a.userApproveTwo).FirstOrDefault();
                                if (check3 != null)
                                    checker.approveTwo = check3.sName + " " + check3.sLastname;
                                else checker.approveTwo = "";
                            }

                            checker.departmentId = a.DepID;
                            checker.toWebservice = "0" + "~" + a.DepID;
                            checkerlist.Add(checker);
                        }

                        dgd.DataSource = checkerlist;
                        dgd.DataBind();
                        requestPeople.Text = index.ToString();
                    }
                }
            }
        }


        void btnSave_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                int id = 0;
                Int32.TryParse(departid.Text, out id);

                var data = _db.TDepartments.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.DepID == id).FirstOrDefault();

                var serializer = new JavaScriptSerializer();
                serializer.RegisterConverters(new[] { new DateTimeConverter() });
                string json = serializer.Serialize(data);

                TB_HistorySetting _HistorySetting = new TB_HistorySetting()
                {
                    Fd_SettingData = json,
                    Fd_UpdatedDate = DateTime.Now,
                    Fd_FunctionName = "Departments Settings",
                    Fd_HistoryID = Guid.NewGuid(),
                    Fd_SchoolID = userData.CompanyID,
                    Fd_UpdatedBy = userData.UserID
                };

                _db.TB_HistorySetting.Add(_HistorySetting);

                int? ApproveTwo = Convert.ToInt32(assistOne.SelectedValue);
                int? ApproveTree = Convert.ToInt32(assistTwo.SelectedValue);


                if (ApproveTwo == ApproveTree)
                {
                    Response.Write("<script>alert('ไม่สามารถบันทึกได้ เนื่องจากผู้อนุมัติ 2 กับผู้อนุมัติ 3 ซ้ำกัน'); window.location.replace('leaveSetting.aspx');</script>");
                }
                else if (data.userHeadId == ApproveTwo)
                {
                    Response.Write("<script>alert('ไม่สามารถบันทึกได้ เนื่องจากหัวหน้าแผนกซ้ำกับผู้อนุมัติ 2'); window.location.replace('leaveSetting.aspx');</script>");
                }
                else if (data.userHeadId == ApproveTree)
                {
                    Response.Write("<script>alert('ไม่สามารถบันทึกได้ เนื่องจากหัวหน้าแผนกซ้ำกับผู้อนุมัติ 3'); window.location.replace('leaveSetting.aspx');</script>");
                }
                else
                {
                    if (assistOne.SelectedValue != "-1")
                    {
                        int x1 = Int32.Parse(assistOne.SelectedValue);
                        data.userApproveOne = x1;
                    }
                    else
                    {
                        data.userApproveOne = null;
                    }

                    if (assistTwo.SelectedValue != "-2")
                    {
                        int x2 = Int32.Parse(assistTwo.SelectedValue);
                        data.userApproveTwo = x2;
                    }
                    else
                    {
                        data.userApproveTwo = null;
                    }

                    _db.SaveChanges();
                    Response.Redirect("leaveSetting.aspx");

                }
            }
        }

        void btnCancle_Click(object sender, EventArgs e)
        {

            Response.Redirect("leaveSetting.aspx");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("leaveSetting-checker.aspx");
        }
        private void Button2_Click(object sender, EventArgs e)
        {

            Response.Redirect("/classmember/classmember.aspx");
        }

        [ScriptMethod()]
        [WebMethod(EnableSession = true)]
        public static TM_LeaveSettings Update(TM_LeaveSettings leaveSettings)
        {
            using (JabJaiMasterEntities entities = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                {
                    var f1 = jabJaiEntities.TLeaveSettings.FirstOrDefault(f => f.schoolId == userData.CompanyID);
                    if (f1 == null) f1 = new TLeaveSetting();
                    else
                    {
                        var serializer = new JavaScriptSerializer();
                        serializer.RegisterConverters(new[] { new DateTimeConverter() });
                        string json = serializer.Serialize(f1);

                        TB_HistorySetting _HistorySetting = new TB_HistorySetting()
                        {
                            Fd_SettingData = json,
                            Fd_UpdatedDate = DateTime.Now,
                            Fd_FunctionName = "Leave Settings",
                            Fd_HistoryID = Guid.NewGuid(),
                            Fd_SchoolID = userData.CompanyID,
                            Fd_UpdatedBy = userData.UserID
                        };

                        jabJaiEntities.TB_HistorySetting.Add(_HistorySetting);
                    }

                    f1.nLimit = leaveSettings.nLimit ?? 0;
                    f1.nType = leaveSettings.nType ?? 1;

                    if (f1.schoolId == 0)
                    {
                        f1.CreatedDate = DateTime.Now;
                        f1.CreatedBy = userData.UserID;
                        f1.schoolId = userData.CompanyID;
                        jabJaiEntities.TLeaveSettings.Add(f1);
                    }
                    else
                    {
                        f1.UpdatedDate = DateTime.Now;
                        f1.UpdatedBy = userData.UserID;
                    }

                    jabJaiEntities.SaveChanges();

                    return new TM_LeaveSettings
                    {
                        nLimit = f1.nLimit,
                        nType = f1.nType
                    };
                }
            }
        }

        [ScriptMethod()]
        [WebMethod(EnableSession = true)]
        public static TM_LeaveSettings getLeaveData()
        {
            using (JabJaiMasterEntities entities = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                {
                    var f1 = jabJaiEntities.TLeaveSettings.Select(s => new
                    {
                        schoolId = s.schoolId,
                        nLimit = s.nLimit,
                        nType = s.nType
                    }).FirstOrDefault(f => f.schoolId == userData.CompanyID);

                    if (f1 == null)
                    {
                        return new TM_LeaveSettings
                        {
                            nLimit = 0,
                            nType = 1
                        };
                    }
                    else
                    {
                        return new TM_LeaveSettings
                        {
                            nLimit = f1.nLimit,
                            nType = f1.nType
                        };
                    }
                }
            }
        }

        public class TM_LeaveSettings
        {
            public int? nLimit { get; set; }
            public int? nType { get; set; }
        }


        protected class checkerList
        {
            public int number { get; set; }
            public int departmentId { get; set; }

            public string departmentHead { get; set; }
            public string approveOne { get; set; }
            public string approveTwo { get; set; }
            public string departmentName { get; set; }
            public string toWebservice { get; set; }
        }

    }
}