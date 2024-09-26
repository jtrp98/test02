using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using System.Globalization;
using System.Web.DynamicData;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using System.Drawing;
using System.IO;
using System.Transactions;
using System.Data.Entity.Validation;

namespace FingerprintPayment.grade
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancle.Click += new EventHandler(btnCancle_Click);

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities));

            dgd.RowDataBound += new GridViewRowEventHandler(dgd_RowDataBound);



            string id = Request.QueryString["id"];
            string idlv2 = Request.QueryString["idlv2"];
            string idlv = Request.QueryString["idlv"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];
            string mode = Request.QueryString["mode"];
            int nidlv2 = int.Parse(idlv2);
            var data = _db.TPlanes.Where(w => w.sPlaneID.ToString() == id).FirstOrDefault();
            var room = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == nidlv2).FirstOrDefault();
            var sub = _db.TSubLevels.Where(w => w.nTSubLevel == room.nTSubLevel).FirstOrDefault();

            var lc_emp = _db.TEmployees.ToList();

            Year.Text = Request.QueryString["term"] + "/" + Request.QueryString["year"];



            if (mode != "EN")
            {
                txtclass.Text = sub.SubLevel + " / " + room.nTSubLevel2;
                planname.Text = data.courseCode + " " + data.sPlaneName;
                headertxtclass.Text = "ชั้นเรียน";
                headerplanname.Text = "วิชา";
                headerteacher1.Text = "ครูประจำชั้น";
                headerteacher2.Text = "ครูผู้สอน";
                headertxtyear.Text = "ปีการศึกษา";
                tablestudentname.Text = "ชื่อ - นามสกุล";
                tablenumber.Text = "ลำดับ";
                tablefullscore.Text = "คะแนนเต็ม";
                tablescore.Text = "คะแนนสอบ";
                tablescoresum.Text = "ผลรวมคะแนนทั้งหมด";
                tablemid.Text = "กลางภาค";
                tablefinal.Text = "ปลายภาค";
                tablebehavior.Text = "คุณลักษณะ อันพึงประสงค์";
                tablereading.Text = "อ่าน คิด วิเคราะห์และเขียน";
                tablescore100.Text = "รวม 100%";
                tablegrade.Text = "เกรด";
                tableother.Text = "ผลประเมินอื่นๆ";
                tabscore.Text = "คะแนนเก็บ 1-20";
                tabbehaveior.Text = "คุณลักษณะฯ";
                tabchewat.Text = "คะแนนเก็บ 21-40";
                configaccumulate.Text = "คะแนนเก็บ";
                configaddbehavior.Text = "เพิ่มหัวข้อคะแนนหน่วยย่อยของคุณลักษณะอันพึงประสงค์";
                configaddchewat.Text = "เพิ่มหัวข้อคะแนนเก็บ/หน่วยชี้วัดอีก 20 ช่อง";
                configautobehavior.Text = "เปิดระบบคำนวนคะแนนคุณลักษณะอันพึงประสงค์ แบบอัตโนมัติ";
                configautoread.Text = "ปิดระบบคำนวนคะแนนอ่าน คิด วิเคราะห์และเขียน แบบอัตโนมัติ ";
                configclosegrade.Text = "ปิดหัวข้อเกรด";
                configclosesamattana.Text = "ปิดหัวข้อสมรรถนะ";
                configdicimal.Text = "แสดงจุดทศนิยม";
                configdisableshare.Text = "เปิดการใช้ข้อมูลชื่อและคะแนนเต็มของหน่วยต่างๆร่วมกับห้องอื่น";
                configfinal.Text = "ปลายภาค";
                configformedit.Text = "แก้ไขฟอร์ม";
                configlosetwotab.Text = "ปิดหัวข้อคุณลักษณะฯ และอ่าน คิด วิเคราะห์ฯ";
                configmid.Text = "กลางภาค";
                configpasspersent.Text = "% ตัดผ่านของคะแนนเก็บแต่ละจุด";
                configratio.Text = "สัดส่วนคะแนน";
                config.Text = "ตั้งค่า";
                popuptxt.Text = "ตัวอย่างการใส่ข้อมูล";
                tabSamattana.Text = "คะแนนสมรรถนะ";
                configaddmid.Text = "เพิ่มหัวข้อคะแนนกลางภาค";
                configaddfinal.Text = "เพิ่มหัวข้อคะแนนปลายภาค";
                tabmid.Text = "กลางภาค";
                tabfinal.Text = "ปลายภาค";
                submitPeriodheader.Text = "ช่วงเวลาที่เปิดให้บันทึกคะแนน";
                Label2.Text = "แชร์ข้อมูลไปยังห้อง";
            }
            else
            {
                Label2.Text = "Sharing to";
                txtclass.Text = sub.SubLevelEN + " / " + room.nTSubLevel2;
                planname.Text = data.courseCode + " " + data.sPlaneName;
                headertxtclass.Text = "Classroom";
                headerplanname.Text = "Course";
                headerteacher1.Text = "Homeroom's teacher";
                headerteacher2.Text = "Lecturer's teacher";
                headertxtyear.Text = "Academic year";
                tablestudentname.Text = "Student's name";
                tablenumber.Text = "No.";
                tablefullscore.Text = "Full score";
                tablescore.Text = "Score";
                tablescoresum.Text = "Score SUM";
                tablemid.Text = "Midterm";
                tablefinal.Text = "Final";
                tablebehavior.Text = "Behavior";
                tablereading.Text = "Reading, Thinking, Writing score";
                tablescore100.Text = "Total 100%";
                tablegrade.Text = "Grade";
                tableother.Text = "Special evaluation";
                tabscore.Text = "Exercise";
                tabbehaveior.Text = "Behavior";
                tabchewat.Text = "Measure Unit";
                configaccumulate.Text = "Exercise";
                configaddbehavior.Text = "Add Additional Behavior tab.";
                configaddchewat.Text = "Add Measure Unit score tab.";
                configautobehavior.Text = "Enable automatic calculation on Behavior tab.";
                configautoread.Text = "Disable automatic calculation on Reading, Thinking tab.";
                configclosegrade.Text = "Close Grade tab.";
                configclosesamattana.Text = "Close Capability tab.";
                configdicimal.Text = "Show dicimal.";
                configdisableshare.Text = "Enable automatic sharing course's information to others.";
                configfinal.Text = "Final term";
                configformedit.Text = "Edit form";
                configlosetwotab.Text = "Close Behavior and Reading, Writing, Thinking tab.";
                configmid.Text = "Mid term";
                configpasspersent.Text = "% Cut off for passing any Exercise";
                configratio.Text = "Score Ratio";
                config.Text = "Setting";
                popuptxt.Text = "example form";
                tabSamattana.Text = "Capability Score";
                configaddmid.Text = "Add Additional Midterm score tab.";
                configaddfinal.Text = "Add Additional Finalterm score tab.";
                tabmid.Text = "Midterm";
                tabfinal.Text = "Final";
                submitPeriodheader.Text = "Score Submit Period";
            }



            if (!IsPostBack)
            {


                OpenData();
                nomax.Text = "0";
                var item2 = new ListItem
                {
                    Text = "---- %",
                    Value = "-1"
                };

                scoreRatio.Items.Add(item2);
                midRatio.Items.Add(item2);
                lastRatio.Items.Add(item2);
                passRatio.Items.Add(item2);

                for (int x = 0; x < 101; x = x + 5)
                {
                    var item = new ListItem
                    {
                        Text = x.ToString() + " %",
                        Value = x.ToString()
                    };
                    scoreRatio.Items.Add(item);
                    midRatio.Items.Add(item);
                    lastRatio.Items.Add(item);
                    passRatio.Items.Add(item);
                }

                if ((year != "" && year != null) && (idlv2 != "" && idlv2 != null))
                {
                    int? idlv2n = Int32.Parse(idlv2);
                    int? idlvn = Int32.Parse(idlv);
                    int? useryear = Int32.Parse(year);
                    int nyear = 0;
                    string nterm = "";
                    int ntermtable = 0;
                    List<string> planIdlist = new List<string>();
                    List<string> copyplanIdlist = new List<string>();
                    List<string> teacherListOwner = new List<string>();
                    List<string> teacherList2 = new List<string>();



                    foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear))
                    {
                        nyear = ff.nYear;
                    }

                    foreach (var ee in _db.TTerms.Where(w => w.sTerm == term && w.nYear == nyear))
                    {
                        nterm = ee.nTerm;
                    }

                    var check = _db.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.nTermSubLevel2 == idlv2n).FirstOrDefault();

                    idlv2txt.Text = idlv2;
                    planidtxt.Text = id;
                    termtxt.Text = nterm.Trim();
                    yeartxt.Text = year;

                    foreach (var dd in _db.TTermTimeTables.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == idlv2n))
                    {
                        ntermtable = dd.nTermTable;
                    }



                    var lc_TSchedules = _db.TSchedules.Where(w => w.nTermTable == ntermtable && w.cDel != true).ToList();
                    var lc_TPlanOwners = _db.TPlanOwners.ToList();
                    foreach (var hh in lc_TSchedules.Where(w => w.nTermTable == ntermtable && w.sPlaneID.ToString() == id && w.cDel != true))
                    {
                        string planId = "";
                        planId = hh.sPlaneID.ToString();
                        planIdlist.Add(planId);
                        foreach (var gg in lc_TPlanOwners.Where(w => w.sPlaneID == hh.sPlaneID))
                        {
                            teacherListOwner.Add(gg.sEMP.ToString());
                        }
                        teacherList2.Add(hh.sEmp.ToString());
                    }

                    foreach (var hh2 in lc_TSchedules.Where(w => w.nTermTable == ntermtable && w.cDel != true))
                    {
                        string planId2 = "";
                        if (hh2.sPlaneID != null)
                        {
                            planId2 = hh2.sPlaneID.ToString();
                            copyplanIdlist.Add(planId2);
                        }
                    }
                    var q_title = _db.TTitleLists.ToList();

                    teacher1.Text = "";
                    var teacherdata = _db.TClassMembers.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == idlv2n).FirstOrDefault();

                    if (teacherdata != null && teacherdata.nTeacherHeadid != null)
                    {
                        var teacher = _db.TEmployees.Where(w => w.sEmp == teacherdata.nTeacherHeadid).FirstOrDefault();
                        if (teacher != null)
                        {
                            int n;
                            bool isNumeric = int.TryParse(teacher.sTitle, out n);
                            string title = "";
                            if (isNumeric == true)
                            {
                                var f_title = q_title.FirstOrDefault(f => f.nTitleid == n);
                                title = f_title.titleDescription;
                            }
                            teacher1.Text = title + teacher.sName + " " + teacher.sLastname;
                        }
                    }

                    List<string> unique3 = copyplanIdlist.Distinct().ToList();
                    List<string> unique = planIdlist.Distinct().ToList();
                    List<string> uniqueOwner = teacherListOwner.Distinct().ToList();
                    List<string> uniqueSchedule = teacherList2.Distinct().ToList();
                    teacher2.Text = "";
                    string teach = "";
                    int count2 = 0;
                    int countOwner = 0;
                    int countSchedule = 0;

                    foreach (var teacherid in uniqueSchedule)
                    {
                        if (teacherid != "" && teacherid != null)
                        {
                            countSchedule = countSchedule + 1;
                        }
                    }

                    foreach (var teacherid in uniqueOwner)
                    {
                        if (teacherid != "" && teacherid != null)
                        {
                            countOwner = countOwner + 1;
                        }
                    }

                    if (countSchedule > 1)
                    {
                        foreach (var teacherid in uniqueSchedule)
                        {
                            if (teacherid != "" && teacherid != null)
                            {
                                int nemp = int.Parse(teacherid);
                                var tdata = _db.TEmployees.Where(w => w.sEmp == nemp).FirstOrDefault();
                                if (tdata != null)
                                {
                                    count2 = count2 + 1;

                                    int n;
                                    bool isNumeric = int.TryParse(tdata.sTitle, out n);
                                    string title = "";
                                    if (isNumeric == true)
                                    {
                                        var f_title = q_title.FirstOrDefault(f => f.nTitleid == n);
                                        title = f_title.titleDescription;
                                    }

                                    teach = teach + count2 + "." + title + tdata.sName + " " + tdata.sLastname + " ";
                                }
                            }
                        }
                    }
                    else if (countSchedule == 1)
                    {
                        foreach (var teacherid2 in uniqueSchedule)
                        {
                            if (teacherid2 != "" && teacherid2 != null)
                            {
                                int nemp = int.Parse(teacherid2);
                                var tdata = _db.TEmployees.Where(w => w.sEmp == nemp).FirstOrDefault();
                                if (tdata != null)
                                {
                                    count2 = count2 + 1;
                                    int n;
                                    bool isNumeric = int.TryParse(tdata.sTitle, out n);
                                    string title = "";
                                    if (isNumeric == true)
                                    {
                                        var f_title = q_title.FirstOrDefault(f => f.nTitleid == n);
                                        title = f_title.titleDescription;
                                    }
                                    teach = teach + count2 + "." + title + tdata.sName + " " + tdata.sLastname + " ";
                                }
                            }
                        }
                    }
                    else if (countSchedule == 0)
                    {
                        if (countOwner > 1)
                        {
                            foreach (var teacherid in uniqueOwner)
                            {
                                if (teacherid != "" && teacherid != null)
                                {
                                    int nemp = int.Parse(teacherid);
                                    var tdata = _db.TEmployees.Where(w => w.sEmp == nemp).FirstOrDefault();
                                    if (tdata != null)
                                    {
                                        count2 = count2 + 1;
                                        int n;
                                        bool isNumeric = int.TryParse(tdata.sTitle, out n);
                                        string title = "";
                                        if (isNumeric == true)
                                        {
                                            var f_title = q_title.FirstOrDefault(f => f.nTitleid == n);
                                            title = f_title.titleDescription;
                                        }
                                        teach = teach + count2 + "." + title + tdata.sName + " " + tdata.sLastname + " ";
                                    }
                                }
                            }
                        }
                        else if (countOwner == 1)
                        {
                            foreach (var teacherid2 in uniqueOwner)
                            {
                                if (teacherid2 != "" && teacherid2 != null)
                                {
                                    int nemp = int.Parse(teacherid2);
                                    var tdata = _db.TEmployees.Where(w => w.sEmp == nemp).FirstOrDefault();
                                    if (tdata != null)
                                    {
                                        count2 = count2 + 1;

                                        int n;
                                        bool isNumeric = int.TryParse(tdata.sTitle, out n);
                                        string title = "";
                                        if (isNumeric == true)
                                        {
                                            var f_title = q_title.FirstOrDefault(f => f.nTitleid == n);
                                            title = f_title.titleDescription;
                                        }
                                        teach = teach + count2 + "." + title + tdata.sName + " " + tdata.sLastname + " ";
                                    }
                                }
                            }
                        }
                    }


                    teacher2.Text = teach;
                    int count = 0;

                    var q1 = _db.TGrades.Where(w => w.nTerm == nterm).ToList();
                    foreach (var planid in unique3)
                    {
                        var grade = q1.Where(w => w.sPlaneID.ToString() == planid).FirstOrDefault();
                        if (grade != null)
                        {
                            count = count + 1;
                            var plan = _db.TPlanes.Where(w => w.sPlaneID == grade.sPlaneID).FirstOrDefault();
                            var item = new ListItem
                            {
                                Text = plan.sPlaneName,
                                Value = plan.sPlaneID.ToString()
                            };
                            ddlcopy1.Items.Add(item);
                            ddlcopy2.Items.Add(item);
                            ddlcopy3.Items.Add(item);
                        }


                    }
                    if (count == 0)
                    {
                        var item = new ListItem
                        {
                            Text = "ยังไม่มีวิชาที่ลงทะเบียนเกรด",
                            Value = "-1"
                        };
                        ddlcopy1.Items.Add(item);
                        ddlcopy2.Items.Add(item);
                        ddlcopy3.Items.Add(item);
                    }

                }

            }
        }
        protected void dgd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (DataBinder.Eval(e.Row.DataItem, "number") == " ")
            {
                e.Row.CssClass = "righttext3";
                e.Row.Cells[2].CssClass = "righttext2";
                e.Row.Cells[1].CssClass = "hid3";
                e.Row.Cells[25].CssClass = "hid2";
                e.Row.Cells[26].CssClass = "hid2";
                e.Row.Cells[27].CssClass = "hid2";
            }


        }
        void btnSave_Click(object sender, EventArgs e)
        {

            string nterm = "";
            int nyear = 0;
            string id = Request.QueryString["id"];
            string idlv2 = Request.QueryString["idlv2"];
            int? idlv2n = Int32.Parse(idlv2);
            string yyy = Request.QueryString["year"];
            int? useryear = Int32.Parse(yyy);
            string userterm = Request.QueryString["term"];

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities));

            string LogFormat = "บันทึกเกรดวิชา {0} เรียน ห้อง {1} ปีการศึกษา : {2} ภาคเรียนที่ : {3}";
            var f_plane = _db.TPlanes.FirstOrDefault(f => f.sPlaneID.ToString() == id);
            var roomData = QueryDataBases.SubLevel2_Query.GetRoom(_db, (idlv2n ?? 0), userData);
            database.InsertLog(HttpContext.Current.Session["sEmpID"] + "", string.Format(LogFormat, f_plane.sPlaneName, roomData.classRoom_name, useryear, userterm), Session["sEntities"].ToString(), HttpContext.Current.Request, 25, 2, 0);

            try
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromSeconds(10000)))
                //using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromSeconds(10000)))
                {



                    DateTime? daystart = DateTime.Now;
                    DateTime? dayend = DateTime.Now;
                    var holiday = _db.THolidays.Where(w => w.cDel == null && w.sHolidayType == "0").ToList();

                    foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear))
                    {
                        nyear = ff.nYear;
                    }

                    foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear))
                    {
                        nterm = ee.nTerm;
                        daystart = ee.dStart;
                        dayend = ee.dEnd;
                    }

                    foreach (var data1 in _db.TGradeShareInfoes.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.from_nTSubLevel2 == idlv2n))
                    {
                        data1.cDel = 1;
                    }

                    if (check8.Checked == false)
                        editmulticlass4.Text = "";

                    if (editmulticlass4.Text != "")
                    {
                        string editmulti2 = editmulticlass4.Text;
                        string[] words2 = editmulti2.Split('/');
                        int count = _db.TGradeShareInfoes.Select(s => s.nGradeShareInfoId).DefaultIfEmpty(0).Max();

                        foreach (string word in words2)
                        {
                            if (word != "" && word != null)
                            {
                                int wordn = int.Parse(word);
                                var check = _db.TGradeShareInfoes.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.from_nTSubLevel2 == idlv2n && w.to_nTSubLevel2 == wordn).FirstOrDefault();

                                if (check == null)
                                {
                                    count = count + 1;
                                    TGradeShareInfo info = new TGradeShareInfo();
                                    info.nGradeShareInfoId = count;
                                    info.nTerm = nterm;
                                    //info.sPlaneID = id;
                                    info.from_nTSubLevel2 = (int)idlv2n;
                                    info.to_nTSubLevel2 = wordn;
                                    info.cDel = null;
                                    _db.TGradeShareInfoes.Add(info);

                                }
                                else
                                {
                                    check.cDel = null;
                                }
                            }
                        }

                        _db.SaveChanges();
                    }

                    TGradeAttendance student = new TGradeAttendance();
                    List<TGradeAttendance> studentlist = new List<TGradeAttendance>();



                    int countRecord = _db.TGrades.Select(s => s.nGradeId).DefaultIfEmpty(0).Max() + 1;
                    var checkGrades = _db.TGrades.FirstOrDefault(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.nTermSubLevel2 == idlv2n);
                    int countAtt = _db.TGradeAttendances.Select(s => s.nGradeAttendanceId).DefaultIfEmpty(0).Max() + 1;

                    TGrade grade = new TGrade();
                    List<TGradeDetail> detail_list = new List<TGradeDetail>();
                    TGradeDetail detail = new TGradeDetail();
                    int sEmpID = int.Parse(Session["sEmpID"] + "");

                    double lastratio = double.Parse(lastRatio.SelectedValue);
                    double midratio = double.Parse(midRatio.SelectedValue);
                    double scoreratio = double.Parse(scoreRatio.SelectedValue);
                    double passratio = double.Parse(passRatio.SelectedValue);

                    int dicimal = 0;
                    if (dicimalCheck.Text == "0")
                        dicimal = 2;
                    else if (dicimalCheck.Text == "1")
                        dicimal = 0;

                    double maxquiz1 = 0;
                    double maxquiz2 = 0;
                    double maxquiz3 = 0;
                    double maxquiz4 = 0;
                    double maxquiz5 = 0;
                    double maxquiz6 = 0;
                    double maxquiz7 = 0;
                    double maxquiz8 = 0;
                    double maxquiz9 = 0;
                    double maxquiz10 = 0;
                    double maxquiz11 = 0;
                    double maxquiz12 = 0;
                    double maxquiz13 = 0;
                    double maxquiz14 = 0;
                    double maxquiz15 = 0;
                    double maxquiz16 = 0;
                    double maxquiz17 = 0;
                    double maxquiz18 = 0;
                    double maxquiz19 = 0;
                    double maxquiz20 = 0;
                    double maxbehavior1 = 0;
                    double maxbehavior2 = 0;
                    double maxbehavior3 = 0;
                    double maxbehavior4 = 0;
                    double maxbehavior5 = 0;
                    double maxbehavior6 = 0;
                    double maxbehavior7 = 0;
                    double maxbehavior8 = 0;
                    double maxbehavior9 = 0;
                    double maxbehavior10 = 0;
                    double maxcheewat1 = 0;
                    double maxcheewat2 = 0;
                    double maxcheewat3 = 0;
                    double maxcheewat4 = 0;
                    double maxcheewat5 = 0;
                    double maxcheewat6 = 0;
                    double maxcheewat7 = 0;
                    double maxcheewat8 = 0;
                    double maxcheewat9 = 0;
                    double maxcheewat10 = 0;
                    double maxcheewat11 = 0;
                    double maxcheewat12 = 0;
                    double maxcheewat13 = 0;
                    double maxcheewat14 = 0;
                    double maxcheewat15 = 0;
                    double maxcheewat16 = 0;
                    double maxcheewat17 = 0;
                    double maxcheewat18 = 0;
                    double maxcheewat19 = 0;
                    double maxcheewat20 = 0;

                    if (maxCW1.Text != "")
                        maxcheewat1 = Double.Parse(maxCW1.Text);
                    if (maxCW2.Text != "")
                        maxcheewat2 = Double.Parse(maxCW2.Text);
                    if (maxCW3.Text != "")
                        maxcheewat3 = Double.Parse(maxCW3.Text);
                    if (maxCW4.Text != "")
                        maxcheewat4 = Double.Parse(maxCW4.Text);
                    if (maxCW5.Text != "")
                        maxcheewat5 = Double.Parse(maxCW5.Text);
                    if (maxCW6.Text != "")
                        maxcheewat6 = Double.Parse(maxCW6.Text);
                    if (maxCW7.Text != "")
                        maxcheewat7 = Double.Parse(maxCW7.Text);
                    if (maxCW8.Text != "")
                        maxcheewat8 = Double.Parse(maxCW8.Text);
                    if (maxCW9.Text != "")
                        maxcheewat9 = Double.Parse(maxCW9.Text);
                    if (maxCW10.Text != "")
                        maxcheewat10 = Double.Parse(maxCW10.Text);
                    if (maxCW11.Text != "")
                        maxcheewat11 = Double.Parse(maxCW11.Text);
                    if (maxCW12.Text != "")
                        maxcheewat12 = Double.Parse(maxCW12.Text);
                    if (maxCW13.Text != "")
                        maxcheewat13 = Double.Parse(maxCW13.Text);
                    if (maxCW14.Text != "")
                        maxcheewat14 = Double.Parse(maxCW14.Text);
                    if (maxCW15.Text != "")
                        maxcheewat15 = Double.Parse(maxCW15.Text);
                    if (maxCW16.Text != "")
                        maxcheewat16 = Double.Parse(maxCW16.Text);
                    if (maxCW17.Text != "")
                        maxcheewat17 = Double.Parse(maxCW17.Text);
                    if (maxCW18.Text != "")
                        maxcheewat18 = Double.Parse(maxCW18.Text);
                    if (maxCW19.Text != "")
                        maxcheewat19 = Double.Parse(maxCW19.Text);
                    if (maxCW20.Text != "")
                        maxcheewat20 = Double.Parse(maxCW20.Text);

                    double cheewatmax = maxcheewat1 + maxcheewat2 + maxcheewat3 + maxcheewat4 + maxcheewat5 + maxcheewat6 + maxcheewat7 +
                        maxcheewat8 + maxcheewat9 + maxcheewat10 + maxcheewat11 + maxcheewat12 + maxcheewat13 + maxcheewat14 + maxcheewat15 +
                        maxcheewat16 + maxcheewat17 + maxcheewat18 + maxcheewat19 + maxcheewat20;

                    if (maxS1.Text != "")
                        maxquiz1 = Double.Parse(maxS1.Text);
                    if (maxS2.Text != "")
                        maxquiz2 = Double.Parse(maxS2.Text);
                    if (maxS3.Text != "")
                        maxquiz3 = Double.Parse(maxS3.Text);
                    if (maxS4.Text != "")
                        maxquiz4 = Double.Parse(maxS4.Text);
                    if (maxS5.Text != "")
                        maxquiz5 = Double.Parse(maxS5.Text);
                    if (maxS6.Text != "")
                        maxquiz6 = Double.Parse(maxS6.Text);
                    if (maxS7.Text != "")
                        maxquiz7 = Double.Parse(maxS7.Text);
                    if (maxS8.Text != "")
                        maxquiz8 = Double.Parse(maxS8.Text);
                    if (maxS9.Text != "")
                        maxquiz9 = Double.Parse(maxS9.Text);
                    if (maxS10.Text != "")
                        maxquiz10 = Double.Parse(maxS10.Text);
                    if (maxS11.Text != "")
                        maxquiz11 = Double.Parse(maxS11.Text);
                    if (maxS12.Text != "")
                        maxquiz12 = Double.Parse(maxS12.Text);
                    if (maxS13.Text != "")
                        maxquiz13 = Double.Parse(maxS13.Text);
                    if (maxS14.Text != "")
                        maxquiz14 = Double.Parse(maxS14.Text);
                    if (maxS15.Text != "")
                        maxquiz15 = Double.Parse(maxS15.Text);
                    if (maxS16.Text != "")
                        maxquiz16 = Double.Parse(maxS16.Text);
                    if (maxS17.Text != "")
                        maxquiz17 = Double.Parse(maxS17.Text);
                    if (maxS18.Text != "")
                        maxquiz18 = Double.Parse(maxS18.Text);
                    if (maxS19.Text != "")
                        maxquiz19 = Double.Parse(maxS19.Text);
                    if (maxS20.Text != "")
                        maxquiz20 = Double.Parse(maxS20.Text);

                    double quizmax = maxquiz1 + maxquiz2 + maxquiz3 + maxquiz4 + maxquiz5 + maxquiz6 + maxquiz7 +
                        maxquiz8 + maxquiz9 + maxquiz10 + maxquiz11 + maxquiz12 + maxquiz13 + maxquiz14 + maxquiz15 +
                        maxquiz16 + maxquiz17 + maxquiz18 + maxquiz19 + maxquiz20;
                    quizmax = quizmax + cheewatmax;

                    if (maxb1.Text != "")
                        maxbehavior1 = Double.Parse(maxb1.Text);
                    if (maxb2.Text != "")
                        maxbehavior2 = Double.Parse(maxb2.Text);
                    if (maxb3.Text != "")
                        maxbehavior3 = Double.Parse(maxb3.Text);
                    if (maxb4.Text != "")
                        maxbehavior4 = Double.Parse(maxb4.Text);
                    if (maxb5.Text != "")
                        maxbehavior5 = Double.Parse(maxb5.Text);
                    if (maxb6.Text != "")
                        maxbehavior6 = Double.Parse(maxb6.Text);
                    if (maxb7.Text != "")
                        maxbehavior7 = Double.Parse(maxb7.Text);
                    if (maxb8.Text != "")
                        maxbehavior8 = Double.Parse(maxb8.Text);
                    if (maxb9.Text != "")
                        maxbehavior9 = Double.Parse(maxb9.Text);
                    if (maxb10.Text != "")
                        maxbehavior10 = Double.Parse(maxb10.Text);
                    double bahavemax = maxbehavior1 + maxbehavior2 + maxbehavior3 + maxbehavior4
                        + maxbehavior5 + maxbehavior6 + maxbehavior7 + maxbehavior8 + maxbehavior9
                        + maxbehavior10;
                    if (bahavemax == 0)
                        bahavemax = 3;

                    if (checkGrades == null)
                    {
                        checkGrades = new TGrade();
                        checkGrades.dAdd = DateTime.Now;
                        checkGrades.dUpdate = DateTime.Now;
                        checkGrades.fRatioLateTerm = lastratio;
                        checkGrades.fRatioQuizPass = passratio;
                        checkGrades.fRatioMidTerm = midratio;
                        checkGrades.fRatioQuiz = scoreratio;
                        checkGrades.maxMidTerm = maxSmid.Text;
                        checkGrades.maxFinalTerm = maxSlate.Text;
                        //checkGrades.maxBehavior1 = maxb1.Text;
                        //checkGrades.maxBehavior10 = maxb10.Text;
                        //checkGrades.maxBehavior2 = maxb2.Text;
                        //checkGrades.maxBehavior3 = maxb3.Text;
                        //checkGrades.maxBehavior4 = maxb4.Text;
                        //checkGrades.maxBehavior5 = maxb5.Text;
                        //checkGrades.maxBehavior6 = maxb6.Text;
                        //checkGrades.maxBehavior7 = maxb7.Text;
                        //checkGrades.maxBehavior8 = maxb8.Text;
                        //checkGrades.maxBehavior9 = maxb9.Text;
                        //checkGrades.maxGrade1 = maxS1.Text;
                        //checkGrades.maxGrade2 = maxS2.Text;
                        //checkGrades.maxGrade3 = maxS3.Text;
                        //checkGrades.maxGrade4 = maxS4.Text;
                        //checkGrades.maxGrade5 = maxS5.Text;
                        //checkGrades.maxGrade6 = maxS6.Text;
                        //checkGrades.maxGrade7 = maxS7.Text;
                        //checkGrades.maxGrade8 = maxS8.Text;
                        //checkGrades.maxGrade9 = maxS9.Text;
                        //checkGrades.maxGrade10 = maxS10.Text;
                        //checkGrades.maxGrade11 = maxS11.Text;
                        //checkGrades.maxGrade12 = maxS12.Text;
                        //checkGrades.maxGrade13 = maxS13.Text;
                        //checkGrades.maxGrade14 = maxS14.Text;
                        //checkGrades.maxGrade15 = maxS15.Text;
                        //checkGrades.maxGrade16 = maxS16.Text;
                        //checkGrades.maxGrade17 = maxS17.Text;
                        //checkGrades.maxGrade18 = maxS18.Text;
                        //checkGrades.maxGrade19 = maxS19.Text;
                        //checkGrades.maxGrade20 = maxS20.Text;
                        //checkGrades.maxCheewat1 = maxCW1.Text;
                        //checkGrades.maxCheewat2 = maxCW2.Text;
                        //checkGrades.maxCheewat3 = maxCW3.Text;
                        //checkGrades.maxCheewat4 = maxCW4.Text;
                        //checkGrades.maxCheewat5 = maxCW5.Text;
                        //checkGrades.maxCheewat6 = maxCW6.Text;
                        //checkGrades.maxCheewat7 = maxCW7.Text;
                        //checkGrades.maxCheewat8 = maxCW8.Text;
                        //checkGrades.maxCheewat9 = maxCW9.Text;
                        //checkGrades.maxCheewat10 = maxCW10.Text;
                        //checkGrades.maxCheewat11 = maxCW11.Text;
                        //checkGrades.maxCheewat12 = maxCW12.Text;
                        //checkGrades.maxCheewat13 = maxCW13.Text;
                        //checkGrades.maxCheewat14 = maxCW14.Text;
                        //checkGrades.maxCheewat15 = maxCW15.Text;
                        //checkGrades.maxCheewat16 = maxCW16.Text;
                        //checkGrades.maxCheewat17 = maxCW17.Text;
                        //checkGrades.maxCheewat18 = maxCW18.Text;
                        //checkGrades.maxCheewat19 = maxCW19.Text;
                        //checkGrades.maxCheewat20 = maxCW20.Text;
                        //checkGrades.nameBehavior1 = setnameb1.Text;
                        //checkGrades.nameBehavior2 = setnameb2.Text;
                        //checkGrades.nameBehavior3 = setnameb3.Text;
                        //checkGrades.nameBehavior4 = setnameb4.Text;
                        //checkGrades.nameBehavior5 = setnameb5.Text;
                        //checkGrades.nameBehavior6 = setnameb6.Text;
                        //checkGrades.nameBehavior7 = setnameb7.Text;
                        //checkGrades.nameBehavior8 = setnameb8.Text;
                        //checkGrades.nameBehavior9 = setnameb9.Text;
                        //checkGrades.nameBehavior10 = setnameb10.Text;
                        //checkGrades.nameGrade1 = setname1.Text;
                        //checkGrades.nameGrade2 = setname2.Text;
                        //checkGrades.nameGrade3 = setname3.Text;
                        //checkGrades.nameGrade4 = setname4.Text;
                        //checkGrades.nameGrade5 = setname5.Text;
                        //checkGrades.nameGrade6 = setname6.Text;
                        //checkGrades.nameGrade7 = setname7.Text;
                        //checkGrades.nameGrade8 = setname8.Text;
                        //checkGrades.nameGrade9 = setname9.Text;
                        //checkGrades.nameGrade10 = setname10.Text;
                        //checkGrades.nameGrade11 = setname11.Text;
                        //checkGrades.nameGrade12 = setname12.Text;
                        //checkGrades.nameGrade13 = setname13.Text;
                        //checkGrades.nameGrade14 = setname14.Text;
                        //checkGrades.nameGrade15 = setname15.Text;
                        //checkGrades.nameGrade16 = setname16.Text;
                        //checkGrades.nameGrade17 = setname17.Text;
                        //checkGrades.nameGrade18 = setname18.Text;
                        //checkGrades.nameGrade19 = setname19.Text;
                        //checkGrades.nameGrade20 = setname20.Text;
                        //checkGrades.nameCheewat1 = chewatname1.Text;
                        //checkGrades.nameCheewat2 = chewatname2.Text;
                        //checkGrades.nameCheewat3 = chewatname3.Text;
                        //checkGrades.nameCheewat4 = chewatname4.Text;
                        //checkGrades.nameCheewat5 = chewatname5.Text;
                        //checkGrades.nameCheewat6 = chewatname6.Text;
                        //checkGrades.nameCheewat7 = chewatname7.Text;
                        //checkGrades.nameCheewat8 = chewatname8.Text;
                        //checkGrades.nameCheewat9 = chewatname9.Text;
                        //checkGrades.nameCheewat10 = chewatname10.Text;
                        //checkGrades.nameCheewat11 = chewatname11.Text;
                        //checkGrades.nameCheewat12 = chewatname12.Text;
                        //checkGrades.nameCheewat13 = chewatname13.Text;
                        //checkGrades.nameCheewat14 = chewatname14.Text;
                        //checkGrades.nameCheewat15 = chewatname15.Text;
                        //checkGrades.nameCheewat16 = chewatname16.Text;
                        //checkGrades.nameCheewat17 = chewatname17.Text;
                        //checkGrades.nameCheewat18 = chewatname18.Text;
                        //checkGrades.nameCheewat19 = chewatname19.Text;
                        //checkGrades.nameCheewat20 = chewatname20.Text;
                        //checkGrades.maxMid1 = midmax1.Text;
                        //checkGrades.maxMid2 = midmax2.Text;
                        //checkGrades.maxMid3 = midmax3.Text;
                        //checkGrades.maxMid4 = midmax4.Text;
                        //checkGrades.maxMid5 = midmax5.Text;
                        //checkGrades.maxMid6 = midmax6.Text;
                        //checkGrades.maxMid7 = midmax7.Text;
                        //checkGrades.maxMid8 = midmax8.Text;
                        //checkGrades.maxMid9 = midmax9.Text;
                        //checkGrades.maxMid10 = midmax10.Text;
                        //checkGrades.nameMid1 = midmodalname1.Text;
                        //checkGrades.nameMid2 = midmodalname2.Text;
                        //checkGrades.nameMid3 = midmodalname3.Text;
                        //checkGrades.nameMid4 = midmodalname4.Text;
                        //checkGrades.nameMid5 = midmodalname5.Text;
                        //checkGrades.nameMid6 = midmodalname6.Text;
                        //checkGrades.nameMid7 = midmodalname7.Text;
                        //checkGrades.nameMid8 = midmodalname8.Text;
                        //checkGrades.nameMid9 = midmodalname9.Text;
                        //checkGrades.nameMid10 = midmodalname10.Text;
                        //checkGrades.maxFinal1 = finalmax1.Text;
                        //checkGrades.maxFinal2 = finalmax2.Text;
                        //checkGrades.maxFinal3 = finalmax3.Text;
                        //checkGrades.maxFinal4 = finalmax4.Text;
                        //checkGrades.maxFinal5 = finalmax5.Text;
                        //checkGrades.maxFinal6 = finalmax6.Text;
                        //checkGrades.maxFinal7 = finalmax7.Text;
                        //checkGrades.maxFinal8 = finalmax8.Text;
                        //checkGrades.maxFinal9 = finalmax9.Text;
                        //checkGrades.maxFinal10 = finalmax10.Text;
                        //checkGrades.nameFinal1 = finalmodalname1.Text;
                        //checkGrades.nameFinal2 = finalmodalname2.Text;
                        //checkGrades.nameFinal3 = finalmodalname3.Text;
                        //checkGrades.nameFinal4 = finalmodalname4.Text;
                        //checkGrades.nameFinal5 = finalmodalname5.Text;
                        //checkGrades.nameFinal6 = finalmodalname6.Text;
                        //checkGrades.nameFinal7 = finalmodalname7.Text;
                        //checkGrades.nameFinal8 = finalmodalname8.Text;
                        //checkGrades.nameFinal9 = finalmodalname9.Text;
                        //checkGrades.nameFinal10 = finalmodalname10.Text;
                        checkGrades.nGradeId = countRecord;
                        checkGrades.nTerm = nterm;
                        checkGrades.nUserAdd = sEmpID;
                        checkGrades.nUserUpdate = sEmpID;
                        checkGrades.sNote = "";
                        //checkGrades.sPlaneID = id;
                        checkGrades.maxBehaviorTotal = bahavemax.ToString();
                        checkGrades.maxGradeTotal = quizmax.ToString();

                        checkGrades.GradeDicimal = ddl1.Text;
                        checkGrades.GradeAutoReadScore = ddl2.Text;
                        checkGrades.GradeAddBehavior = ddl3.Text;
                        checkGrades.GradeAutoBehaviorScore = ddl4.Text;
                        checkGrades.GradeAddCheewat = ddl5.Text;
                        checkGrades.GradeCloseBehaviorReadWrite = ddl6.Text;
                        checkGrades.GradeShareData = ddl7.Text;
                        checkGrades.GradeCloseGrade = ddl8.Text;
                        checkGrades.GradeCloseSamattana = ddl9.Text;
                        checkGrades.optionMid = ddl11.Text;
                        checkGrades.optionFinal = ddl12.Text;
                        checkGrades.nTermSubLevel2 = int.Parse(idlv2);
                        _db.TGrades.Add(checkGrades);
                    }
                    else
                    {
                        checkGrades.dUpdate = DateTime.Now;
                        checkGrades.fRatioLateTerm = lastratio;
                        checkGrades.fRatioQuizPass = passratio;
                        checkGrades.fRatioMidTerm = midratio;
                        checkGrades.fRatioQuiz = scoreratio;
                        checkGrades.maxMidTerm = maxSmid.Text;
                        checkGrades.maxFinalTerm = maxSlate.Text;
                        //checkGrades.maxBehavior1 = maxb1.Text;
                        //checkGrades.maxBehavior10 = maxb10.Text;
                        //checkGrades.maxBehavior2 = maxb2.Text;
                        //checkGrades.maxBehavior3 = maxb3.Text;
                        //checkGrades.maxBehavior4 = maxb4.Text;
                        //checkGrades.maxBehavior5 = maxb5.Text;
                        //checkGrades.maxBehavior6 = maxb6.Text;
                        //checkGrades.maxBehavior7 = maxb7.Text;
                        //checkGrades.maxBehavior8 = maxb8.Text;
                        //checkGrades.maxBehavior9 = maxb9.Text;
                        //checkGrades.maxGrade1 = maxS1.Text;
                        //checkGrades.maxGrade2 = maxS2.Text;
                        //checkGrades.maxGrade3 = maxS3.Text;
                        //checkGrades.maxGrade4 = maxS4.Text;
                        //checkGrades.maxGrade5 = maxS5.Text;
                        //checkGrades.maxGrade6 = maxS6.Text;
                        //checkGrades.maxGrade7 = maxS7.Text;
                        //checkGrades.maxGrade8 = maxS8.Text;
                        //checkGrades.maxGrade9 = maxS9.Text;
                        //checkGrades.maxGrade10 = maxS10.Text;
                        //checkGrades.maxGrade11 = maxS11.Text;
                        //checkGrades.maxGrade12 = maxS12.Text;
                        //checkGrades.maxGrade13 = maxS13.Text;
                        //checkGrades.maxGrade14 = maxS14.Text;
                        //checkGrades.maxGrade15 = maxS15.Text;
                        //checkGrades.maxGrade16 = maxS16.Text;
                        //checkGrades.maxGrade17 = maxS17.Text;
                        //checkGrades.maxGrade18 = maxS18.Text;
                        //checkGrades.maxGrade19 = maxS19.Text;
                        //checkGrades.maxGrade20 = maxS20.Text;
                        //checkGrades.nameBehavior1 = setnameb1.Text;
                        //checkGrades.nameBehavior2 = setnameb2.Text;
                        //checkGrades.nameBehavior3 = setnameb3.Text;
                        //checkGrades.nameBehavior4 = setnameb4.Text;
                        //checkGrades.nameBehavior5 = setnameb5.Text;
                        //checkGrades.nameBehavior6 = setnameb6.Text;
                        //checkGrades.nameBehavior7 = setnameb7.Text;
                        //checkGrades.nameBehavior8 = setnameb8.Text;
                        //checkGrades.nameBehavior9 = setnameb9.Text;
                        //checkGrades.nameBehavior10 = setnameb10.Text;
                        //checkGrades.nameGrade1 = setname1.Text;
                        //checkGrades.nameGrade2 = setname2.Text;
                        //checkGrades.nameGrade3 = setname3.Text;
                        //checkGrades.nameGrade4 = setname4.Text;
                        //checkGrades.nameGrade5 = setname5.Text;
                        //checkGrades.nameGrade6 = setname6.Text;
                        //checkGrades.nameGrade7 = setname7.Text;
                        //checkGrades.nameGrade8 = setname8.Text;
                        //checkGrades.nameGrade9 = setname9.Text;
                        //checkGrades.nameGrade10 = setname10.Text;
                        //checkGrades.nameGrade11 = setname11.Text;
                        //checkGrades.nameGrade12 = setname12.Text;
                        //checkGrades.nameGrade13 = setname13.Text;
                        //checkGrades.nameGrade14 = setname14.Text;
                        //checkGrades.nameGrade15 = setname15.Text;
                        //checkGrades.nameGrade16 = setname16.Text;
                        //checkGrades.nameGrade17 = setname17.Text;
                        //checkGrades.nameGrade18 = setname18.Text;
                        //checkGrades.nameGrade19 = setname19.Text;
                        //checkGrades.nameGrade20 = setname20.Text;
                        //checkGrades.nameCheewat1 = chewatname1.Text;
                        //checkGrades.nameCheewat2 = chewatname2.Text;
                        //checkGrades.nameCheewat3 = chewatname3.Text;
                        //checkGrades.nameCheewat4 = chewatname4.Text;
                        //checkGrades.nameCheewat5 = chewatname5.Text;
                        //checkGrades.nameCheewat6 = chewatname6.Text;
                        //checkGrades.nameCheewat7 = chewatname7.Text;
                        //checkGrades.nameCheewat8 = chewatname8.Text;
                        //checkGrades.nameCheewat9 = chewatname9.Text;
                        //checkGrades.nameCheewat10 = chewatname10.Text;
                        //checkGrades.nameCheewat11 = chewatname11.Text;
                        //checkGrades.nameCheewat12 = chewatname12.Text;
                        //checkGrades.nameCheewat13 = chewatname13.Text;
                        //checkGrades.nameCheewat14 = chewatname14.Text;
                        //checkGrades.nameCheewat15 = chewatname15.Text;
                        //checkGrades.nameCheewat16 = chewatname16.Text;
                        //checkGrades.nameCheewat17 = chewatname17.Text;
                        //checkGrades.nameCheewat18 = chewatname18.Text;
                        //checkGrades.nameCheewat19 = chewatname19.Text;
                        //checkGrades.nameCheewat20 = chewatname20.Text;
                        //checkGrades.maxCheewat1 = maxCW1.Text;
                        //checkGrades.maxCheewat2 = maxCW2.Text;
                        //checkGrades.maxCheewat3 = maxCW3.Text;
                        //checkGrades.maxCheewat4 = maxCW4.Text;
                        //checkGrades.maxCheewat5 = maxCW5.Text;
                        //checkGrades.maxCheewat6 = maxCW6.Text;
                        //checkGrades.maxCheewat7 = maxCW7.Text;
                        //checkGrades.maxCheewat8 = maxCW8.Text;
                        //checkGrades.maxCheewat9 = maxCW9.Text;
                        //checkGrades.maxCheewat10 = maxCW10.Text;
                        //checkGrades.maxCheewat11 = maxCW11.Text;
                        //checkGrades.maxCheewat12 = maxCW12.Text;
                        //checkGrades.maxCheewat13 = maxCW13.Text;
                        //checkGrades.maxCheewat14 = maxCW14.Text;
                        //checkGrades.maxCheewat15 = maxCW15.Text;
                        //checkGrades.maxCheewat16 = maxCW16.Text;
                        //checkGrades.maxCheewat17 = maxCW17.Text;
                        //checkGrades.maxCheewat18 = maxCW18.Text;
                        //checkGrades.maxCheewat19 = maxCW19.Text;
                        //checkGrades.maxCheewat20 = maxCW20.Text;
                        //checkGrades.maxMid1 = midmax1.Text;
                        //checkGrades.maxMid2 = midmax2.Text;
                        //checkGrades.maxMid3 = midmax3.Text;
                        //checkGrades.maxMid4 = midmax4.Text;
                        //checkGrades.maxMid5 = midmax5.Text;
                        //checkGrades.maxMid6 = midmax6.Text;
                        //checkGrades.maxMid7 = midmax7.Text;
                        //checkGrades.maxMid8 = midmax8.Text;
                        //checkGrades.maxMid9 = midmax9.Text;
                        //checkGrades.maxMid10 = midmax10.Text;
                        //checkGrades.nameMid1 = midmodalname1.Text;
                        //checkGrades.nameMid2 = midmodalname2.Text;
                        //checkGrades.nameMid3 = midmodalname3.Text;
                        //checkGrades.nameMid4 = midmodalname4.Text;
                        //checkGrades.nameMid5 = midmodalname5.Text;
                        //checkGrades.nameMid6 = midmodalname6.Text;
                        //checkGrades.nameMid7 = midmodalname7.Text;
                        //checkGrades.nameMid8 = midmodalname8.Text;
                        //checkGrades.nameMid9 = midmodalname9.Text;
                        //checkGrades.nameMid10 = midmodalname10.Text;
                        //checkGrades.maxFinal1 = finalmax1.Text;
                        //checkGrades.maxFinal2 = finalmax2.Text;
                        //checkGrades.maxFinal3 = finalmax3.Text;
                        //checkGrades.maxFinal4 = finalmax4.Text;
                        //checkGrades.maxFinal5 = finalmax5.Text;
                        //checkGrades.maxFinal6 = finalmax6.Text;
                        //checkGrades.maxFinal7 = finalmax7.Text;
                        //checkGrades.maxFinal8 = finalmax8.Text;
                        //checkGrades.maxFinal9 = finalmax9.Text;
                        //checkGrades.maxFinal10 = finalmax10.Text;
                        //checkGrades.nameFinal1 = finalmodalname1.Text;
                        //checkGrades.nameFinal2 = finalmodalname2.Text;
                        //checkGrades.nameFinal3 = finalmodalname3.Text;
                        //checkGrades.nameFinal4 = finalmodalname4.Text;
                        //checkGrades.nameFinal5 = finalmodalname5.Text;
                        //checkGrades.nameFinal6 = finalmodalname6.Text;
                        //checkGrades.nameFinal7 = finalmodalname7.Text;
                        //checkGrades.nameFinal8 = finalmodalname8.Text;
                        //checkGrades.nameFinal9 = finalmodalname9.Text;
                        //checkGrades.nameFinal10 = finalmodalname10.Text;
                        checkGrades.nTerm = nterm;
                        checkGrades.nUserUpdate = sEmpID;
                        checkGrades.sNote = "";
                        //checkGrades.sPlaneID = id;
                        checkGrades.maxBehaviorTotal = bahavemax.ToString();
                        checkGrades.maxGradeTotal = quizmax.ToString();

                        checkGrades.GradeDicimal = ddl1.Text;
                        checkGrades.GradeAutoReadScore = ddl2.Text;
                        checkGrades.GradeAddBehavior = ddl3.Text;
                        checkGrades.GradeAutoBehaviorScore = ddl4.Text;
                        checkGrades.GradeAddCheewat = ddl5.Text;
                        checkGrades.GradeCloseBehaviorReadWrite = ddl6.Text;
                        checkGrades.GradeShareData = ddl7.Text;
                        checkGrades.GradeCloseGrade = ddl8.Text;
                        checkGrades.GradeCloseSamattana = ddl9.Text;
                        checkGrades.optionMid = ddl11.Text;
                        checkGrades.optionFinal = ddl12.Text;
                    }



                    int countRecord2 = _db.TGradeLocks.Select(s => s.nGradeLock).DefaultIfEmpty(0).Max() + 1;
                    TGradeLock l = new TGradeLock();
                    List<TGradeLock> llist = new List<TGradeLock>();

                    int periodnow = int.Parse(periodSubmit.Text);
                    int n;


                    int gradeid = 0;
                    if (checkGrades == null)
                    {
                        gradeid = countRecord;
                    }
                    else
                    {
                        gradeid = checkGrades.nGradeId;
                    }

                    var dup = _db.TGradeLocks.Where(w => w.nGradeId == gradeid).FirstOrDefault();
                    if (dup == null)
                    {
                        l = new TGradeLock();
                        l.lastUpdate = DateTime.Now;
                        l.nGradeId = gradeid;
                        l.nGradeLock = countRecord2;
                        bool isNumeric = int.TryParse(maxCW1.Text, out n); if (isNumeric == true) l.scoreCheewat1 = periodnow;
                        isNumeric = int.TryParse(maxCW2.Text, out n); if (isNumeric == true) l.scoreCheewat2 = periodnow;
                        isNumeric = int.TryParse(maxCW3.Text, out n); if (isNumeric == true) l.scoreCheewat3 = periodnow;
                        isNumeric = int.TryParse(maxCW4.Text, out n); if (isNumeric == true) l.scoreCheewat4 = periodnow;
                        isNumeric = int.TryParse(maxCW5.Text, out n); if (isNumeric == true) l.scoreCheewat5 = periodnow;
                        isNumeric = int.TryParse(maxCW6.Text, out n); if (isNumeric == true) l.scoreCheewat6 = periodnow;
                        isNumeric = int.TryParse(maxCW7.Text, out n); if (isNumeric == true) l.scoreCheewat7 = periodnow;
                        isNumeric = int.TryParse(maxCW8.Text, out n); if (isNumeric == true) l.scoreCheewat8 = periodnow;
                        isNumeric = int.TryParse(maxCW9.Text, out n); if (isNumeric == true) l.scoreCheewat9 = periodnow;
                        isNumeric = int.TryParse(maxCW10.Text, out n); if (isNumeric == true) l.scoreCheewat10 = periodnow;
                        isNumeric = int.TryParse(maxCW11.Text, out n); if (isNumeric == true) l.scoreCheewat11 = periodnow;
                        isNumeric = int.TryParse(maxCW12.Text, out n); if (isNumeric == true) l.scoreCheewat12 = periodnow;
                        isNumeric = int.TryParse(maxCW13.Text, out n); if (isNumeric == true) l.scoreCheewat13 = periodnow;
                        isNumeric = int.TryParse(maxCW14.Text, out n); if (isNumeric == true) l.scoreCheewat14 = periodnow;
                        isNumeric = int.TryParse(maxCW15.Text, out n); if (isNumeric == true) l.scoreCheewat15 = periodnow;
                        isNumeric = int.TryParse(maxCW16.Text, out n); if (isNumeric == true) l.scoreCheewat16 = periodnow;
                        isNumeric = int.TryParse(maxCW17.Text, out n); if (isNumeric == true) l.scoreCheewat17 = periodnow;
                        isNumeric = int.TryParse(maxCW18.Text, out n); if (isNumeric == true) l.scoreCheewat18 = periodnow;
                        isNumeric = int.TryParse(maxCW19.Text, out n); if (isNumeric == true) l.scoreCheewat19 = periodnow;
                        isNumeric = int.TryParse(maxCW20.Text, out n); if (isNumeric == true) l.scoreCheewat20 = periodnow;
                        isNumeric = int.TryParse(finalmax1.Text, out n); if (isNumeric == true) l.scoreFinal1 = periodnow;
                        isNumeric = int.TryParse(finalmax2.Text, out n); if (isNumeric == true) l.scoreFinal2 = periodnow;
                        isNumeric = int.TryParse(finalmax3.Text, out n); if (isNumeric == true) l.scoreFinal3 = periodnow;
                        isNumeric = int.TryParse(finalmax4.Text, out n); if (isNumeric == true) l.scoreFinal4 = periodnow;
                        isNumeric = int.TryParse(finalmax5.Text, out n); if (isNumeric == true) l.scoreFinal5 = periodnow;
                        isNumeric = int.TryParse(finalmax6.Text, out n); if (isNumeric == true) l.scoreFinal6 = periodnow;
                        isNumeric = int.TryParse(finalmax7.Text, out n); if (isNumeric == true) l.scoreFinal7 = periodnow;
                        isNumeric = int.TryParse(finalmax8.Text, out n); if (isNumeric == true) l.scoreFinal8 = periodnow;
                        isNumeric = int.TryParse(finalmax9.Text, out n); if (isNumeric == true) l.scoreFinal9 = periodnow;
                        isNumeric = int.TryParse(finalmax10.Text, out n); if (isNumeric == true) l.scoreFinal10 = periodnow;
                        isNumeric = int.TryParse(maxSlate.Text, out n); if (isNumeric == true) l.scoreFinalTerm = periodnow;
                        isNumeric = int.TryParse(maxS1.Text, out n); if (isNumeric == true) l.scoreGrade1 = periodnow;
                        isNumeric = int.TryParse(maxS2.Text, out n); if (isNumeric == true) l.scoreGrade2 = periodnow;
                        isNumeric = int.TryParse(maxS3.Text, out n); if (isNumeric == true) l.scoreGrade3 = periodnow;
                        isNumeric = int.TryParse(maxS4.Text, out n); if (isNumeric == true) l.scoreGrade4 = periodnow;
                        isNumeric = int.TryParse(maxS5.Text, out n); if (isNumeric == true) l.scoreGrade5 = periodnow;
                        isNumeric = int.TryParse(maxS6.Text, out n); if (isNumeric == true) l.scoreGrade6 = periodnow;
                        isNumeric = int.TryParse(maxS7.Text, out n); if (isNumeric == true) l.scoreGrade7 = periodnow;
                        isNumeric = int.TryParse(maxS8.Text, out n); if (isNumeric == true) l.scoreGrade8 = periodnow;
                        isNumeric = int.TryParse(maxS9.Text, out n); if (isNumeric == true) l.scoreGrade9 = periodnow;
                        isNumeric = int.TryParse(maxS10.Text, out n); if (isNumeric == true) l.scoreGrade10 = periodnow;
                        isNumeric = int.TryParse(maxS11.Text, out n); if (isNumeric == true) l.scoreGrade11 = periodnow;
                        isNumeric = int.TryParse(maxS12.Text, out n); if (isNumeric == true) l.scoreGrade12 = periodnow;
                        isNumeric = int.TryParse(maxS13.Text, out n); if (isNumeric == true) l.scoreGrade13 = periodnow;
                        isNumeric = int.TryParse(maxS14.Text, out n); if (isNumeric == true) l.scoreGrade14 = periodnow;
                        isNumeric = int.TryParse(maxS15.Text, out n); if (isNumeric == true) l.scoreGrade15 = periodnow;
                        isNumeric = int.TryParse(maxS16.Text, out n); if (isNumeric == true) l.scoreGrade16 = periodnow;
                        isNumeric = int.TryParse(maxS17.Text, out n); if (isNumeric == true) l.scoreGrade17 = periodnow;
                        isNumeric = int.TryParse(maxS18.Text, out n); if (isNumeric == true) l.scoreGrade18 = periodnow;
                        isNumeric = int.TryParse(maxS19.Text, out n); if (isNumeric == true) l.scoreGrade19 = periodnow;
                        isNumeric = int.TryParse(maxS20.Text, out n); if (isNumeric == true) l.scoreGrade20 = periodnow;
                        isNumeric = int.TryParse(midmax1.Text, out n); if (isNumeric == true) l.scoreMid1 = periodnow;
                        isNumeric = int.TryParse(midmax2.Text, out n); if (isNumeric == true) l.scoreMid2 = periodnow;
                        isNumeric = int.TryParse(midmax3.Text, out n); if (isNumeric == true) l.scoreMid3 = periodnow;
                        isNumeric = int.TryParse(midmax4.Text, out n); if (isNumeric == true) l.scoreMid4 = periodnow;
                        isNumeric = int.TryParse(midmax5.Text, out n); if (isNumeric == true) l.scoreMid5 = periodnow;
                        isNumeric = int.TryParse(midmax6.Text, out n); if (isNumeric == true) l.scoreMid6 = periodnow;
                        isNumeric = int.TryParse(midmax7.Text, out n); if (isNumeric == true) l.scoreMid7 = periodnow;
                        isNumeric = int.TryParse(midmax8.Text, out n); if (isNumeric == true) l.scoreMid8 = periodnow;
                        isNumeric = int.TryParse(midmax9.Text, out n); if (isNumeric == true) l.scoreMid9 = periodnow;
                        isNumeric = int.TryParse(midmax10.Text, out n); if (isNumeric == true) l.scoreMid10 = periodnow;
                        isNumeric = int.TryParse(maxSmid.Text, out n); if (isNumeric == true) l.scoreMidTerm = periodnow;
                        l.updateByEMP = sEmpID;
                        _db.TGradeLocks.Add(l);
                    }
                    else
                    {
                        if (periodnow != -1)
                        {
                            dup.lastUpdate = DateTime.Now;
                            bool isNumeric2;

                            if (checkcw1.Text == "0") { isNumeric2 = int.TryParse(maxCW1.Text, out n); if (isNumeric2 == true && dup.scoreCheewat1 == null) dup.scoreCheewat1 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat1 = null; }
                            if (checkcw2.Text == "0") { isNumeric2 = int.TryParse(maxCW2.Text, out n); if (isNumeric2 == true && dup.scoreCheewat2 == null) dup.scoreCheewat2 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat2 = null; }
                            if (checkcw3.Text == "0") { isNumeric2 = int.TryParse(maxCW3.Text, out n); if (isNumeric2 == true && dup.scoreCheewat3 == null) dup.scoreCheewat3 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat3 = null; }
                            if (checkcw4.Text == "0") { isNumeric2 = int.TryParse(maxCW4.Text, out n); if (isNumeric2 == true && dup.scoreCheewat4 == null) dup.scoreCheewat4 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat4 = null; }
                            if (checkcw5.Text == "0") { isNumeric2 = int.TryParse(maxCW5.Text, out n); if (isNumeric2 == true && dup.scoreCheewat5 == null) dup.scoreCheewat5 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat5 = null; }
                            if (checkcw6.Text == "0") { isNumeric2 = int.TryParse(maxCW6.Text, out n); if (isNumeric2 == true && dup.scoreCheewat6 == null) dup.scoreCheewat6 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat6 = null; }
                            if (checkcw7.Text == "0") { isNumeric2 = int.TryParse(maxCW7.Text, out n); if (isNumeric2 == true && dup.scoreCheewat7 == null) dup.scoreCheewat7 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat7 = null; }
                            if (checkcw8.Text == "0") { isNumeric2 = int.TryParse(maxCW8.Text, out n); if (isNumeric2 == true && dup.scoreCheewat8 == null) dup.scoreCheewat8 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat8 = null; }
                            if (checkcw9.Text == "0") { isNumeric2 = int.TryParse(maxCW9.Text, out n); if (isNumeric2 == true && dup.scoreCheewat9 == null) dup.scoreCheewat9 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat9 = null; }
                            if (checkcw10.Text == "0") { isNumeric2 = int.TryParse(maxCW10.Text, out n); if (isNumeric2 == true && dup.scoreCheewat10 == null) dup.scoreCheewat10 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat10 = null; }
                            if (checkcw11.Text == "0") { isNumeric2 = int.TryParse(maxCW11.Text, out n); if (isNumeric2 == true && dup.scoreCheewat11 == null) dup.scoreCheewat11 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat11 = null; }
                            if (checkcw12.Text == "0") { isNumeric2 = int.TryParse(maxCW12.Text, out n); if (isNumeric2 == true && dup.scoreCheewat12 == null) dup.scoreCheewat12 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat12 = null; }
                            if (checkcw13.Text == "0") { isNumeric2 = int.TryParse(maxCW13.Text, out n); if (isNumeric2 == true && dup.scoreCheewat13 == null) dup.scoreCheewat13 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat13 = null; }
                            if (checkcw14.Text == "0") { isNumeric2 = int.TryParse(maxCW14.Text, out n); if (isNumeric2 == true && dup.scoreCheewat14 == null) dup.scoreCheewat14 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat14 = null; }
                            if (checkcw15.Text == "0") { isNumeric2 = int.TryParse(maxCW15.Text, out n); if (isNumeric2 == true && dup.scoreCheewat15 == null) dup.scoreCheewat15 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat15 = null; }
                            if (checkcw16.Text == "0") { isNumeric2 = int.TryParse(maxCW16.Text, out n); if (isNumeric2 == true && dup.scoreCheewat16 == null) dup.scoreCheewat16 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat16 = null; }
                            if (checkcw17.Text == "0") { isNumeric2 = int.TryParse(maxCW17.Text, out n); if (isNumeric2 == true && dup.scoreCheewat17 == null) dup.scoreCheewat17 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat17 = null; }
                            if (checkcw18.Text == "0") { isNumeric2 = int.TryParse(maxCW18.Text, out n); if (isNumeric2 == true && dup.scoreCheewat18 == null) dup.scoreCheewat18 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat18 = null; }
                            if (checkcw19.Text == "0") { isNumeric2 = int.TryParse(maxCW19.Text, out n); if (isNumeric2 == true && dup.scoreCheewat19 == null) dup.scoreCheewat19 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat19 = null; }
                            if (checkcw20.Text == "0") { isNumeric2 = int.TryParse(maxCW20.Text, out n); if (isNumeric2 == true && dup.scoreCheewat20 == null) dup.scoreCheewat20 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat20 = null; }
                            isNumeric2 = int.TryParse(finalmax1.Text, out n); if (isNumeric2 == true && dup.scoreFinal1 == null) dup.scoreFinal1 = periodnow; else if (isNumeric2 != true) dup.scoreFinal1 = null;
                            isNumeric2 = int.TryParse(finalmax2.Text, out n); if (isNumeric2 == true && dup.scoreFinal2 == null) dup.scoreFinal2 = periodnow; else if (isNumeric2 != true) dup.scoreFinal2 = null;
                            isNumeric2 = int.TryParse(finalmax3.Text, out n); if (isNumeric2 == true && dup.scoreFinal3 == null) dup.scoreFinal3 = periodnow; else if (isNumeric2 != true) dup.scoreFinal3 = null;
                            isNumeric2 = int.TryParse(finalmax4.Text, out n); if (isNumeric2 == true && dup.scoreFinal4 == null) dup.scoreFinal4 = periodnow; else if (isNumeric2 != true) dup.scoreFinal4 = null;
                            isNumeric2 = int.TryParse(finalmax5.Text, out n); if (isNumeric2 == true && dup.scoreFinal5 == null) dup.scoreFinal5 = periodnow; else if (isNumeric2 != true) dup.scoreFinal5 = null;
                            isNumeric2 = int.TryParse(finalmax6.Text, out n); if (isNumeric2 == true && dup.scoreFinal6 == null) dup.scoreFinal6 = periodnow; else if (isNumeric2 != true) dup.scoreFinal6 = null;
                            isNumeric2 = int.TryParse(finalmax7.Text, out n); if (isNumeric2 == true && dup.scoreFinal7 == null) dup.scoreFinal7 = periodnow; else if (isNumeric2 != true) dup.scoreFinal7 = null;
                            isNumeric2 = int.TryParse(finalmax8.Text, out n); if (isNumeric2 == true && dup.scoreFinal8 == null) dup.scoreFinal8 = periodnow; else if (isNumeric2 != true) dup.scoreFinal8 = null;
                            isNumeric2 = int.TryParse(finalmax9.Text, out n); if (isNumeric2 == true && dup.scoreFinal9 == null) dup.scoreFinal9 = periodnow; else if (isNumeric2 != true) dup.scoreFinal9 = null;
                            isNumeric2 = int.TryParse(finalmax10.Text, out n); if (isNumeric2 == true && dup.scoreFinal10 == null) dup.scoreFinal10 = periodnow; else if (isNumeric2 != true) dup.scoreFinal10 = null;
                            isNumeric2 = int.TryParse(maxSlate.Text, out n); if (isNumeric2 == true && dup.scoreFinalTerm == null) dup.scoreFinalTerm = periodnow; else if (isNumeric2 != true) dup.scoreFinalTerm = null;
                            if (checkg1.Text == "0") { isNumeric2 = int.TryParse(maxS1.Text, out n); if (isNumeric2 == true && dup.scoreGrade1 == null) dup.scoreGrade1 = periodnow; else if (isNumeric2 != true) dup.scoreGrade1 = null; }
                            if (checkg2.Text == "0") { isNumeric2 = int.TryParse(maxS2.Text, out n); if (isNumeric2 == true && dup.scoreGrade2 == null) dup.scoreGrade2 = periodnow; else if (isNumeric2 != true) dup.scoreGrade2 = null; }
                            if (checkg3.Text == "0") { isNumeric2 = int.TryParse(maxS3.Text, out n); if (isNumeric2 == true && dup.scoreGrade3 == null) dup.scoreGrade3 = periodnow; else if (isNumeric2 != true) dup.scoreGrade3 = null; }
                            if (checkg4.Text == "0") { isNumeric2 = int.TryParse(maxS4.Text, out n); if (isNumeric2 == true && dup.scoreGrade4 == null) dup.scoreGrade4 = periodnow; else if (isNumeric2 != true) dup.scoreGrade4 = null; }
                            if (checkg5.Text == "0") { isNumeric2 = int.TryParse(maxS5.Text, out n); if (isNumeric2 == true && dup.scoreGrade5 == null) dup.scoreGrade5 = periodnow; else if (isNumeric2 != true) dup.scoreGrade5 = null; }
                            if (checkg6.Text == "0") { isNumeric2 = int.TryParse(maxS6.Text, out n); if (isNumeric2 == true && dup.scoreGrade6 == null) dup.scoreGrade6 = periodnow; else if (isNumeric2 != true) dup.scoreGrade6 = null; }
                            if (checkg7.Text == "0") { isNumeric2 = int.TryParse(maxS7.Text, out n); if (isNumeric2 == true && dup.scoreGrade7 == null) dup.scoreGrade7 = periodnow; else if (isNumeric2 != true) dup.scoreGrade7 = null; }
                            if (checkg8.Text == "0") { isNumeric2 = int.TryParse(maxS8.Text, out n); if (isNumeric2 == true && dup.scoreGrade8 == null) dup.scoreGrade8 = periodnow; else if (isNumeric2 != true) dup.scoreGrade8 = null; }
                            if (checkg9.Text == "0") { isNumeric2 = int.TryParse(maxS9.Text, out n); if (isNumeric2 == true && dup.scoreGrade9 == null) dup.scoreGrade9 = periodnow; else if (isNumeric2 != true) dup.scoreGrade9 = null; }
                            if (checkg10.Text == "0") { isNumeric2 = int.TryParse(maxS10.Text, out n); if (isNumeric2 == true && dup.scoreGrade10 == null) dup.scoreGrade10 = periodnow; else if (isNumeric2 != true) dup.scoreGrade10 = null; }
                            if (checkg11.Text == "0") { isNumeric2 = int.TryParse(maxS11.Text, out n); if (isNumeric2 == true && dup.scoreGrade11 == null) dup.scoreGrade11 = periodnow; else if (isNumeric2 != true) dup.scoreGrade11 = null; }
                            if (checkg12.Text == "0") { isNumeric2 = int.TryParse(maxS12.Text, out n); if (isNumeric2 == true && dup.scoreGrade12 == null) dup.scoreGrade12 = periodnow; else if (isNumeric2 != true) dup.scoreGrade12 = null; }
                            if (checkg13.Text == "0") { isNumeric2 = int.TryParse(maxS13.Text, out n); if (isNumeric2 == true && dup.scoreGrade13 == null) dup.scoreGrade13 = periodnow; else if (isNumeric2 != true) dup.scoreGrade13 = null; }
                            if (checkg14.Text == "0") { isNumeric2 = int.TryParse(maxS14.Text, out n); if (isNumeric2 == true && dup.scoreGrade14 == null) dup.scoreGrade14 = periodnow; else if (isNumeric2 != true) dup.scoreGrade14 = null; }
                            if (checkg15.Text == "0") { isNumeric2 = int.TryParse(maxS15.Text, out n); if (isNumeric2 == true && dup.scoreGrade15 == null) dup.scoreGrade15 = periodnow; else if (isNumeric2 != true) dup.scoreGrade15 = null; }
                            if (checkg16.Text == "0") { isNumeric2 = int.TryParse(maxS16.Text, out n); if (isNumeric2 == true && dup.scoreGrade16 == null) dup.scoreGrade16 = periodnow; else if (isNumeric2 != true) dup.scoreGrade16 = null; }
                            if (checkg17.Text == "0") { isNumeric2 = int.TryParse(maxS17.Text, out n); if (isNumeric2 == true && dup.scoreGrade17 == null) dup.scoreGrade17 = periodnow; else if (isNumeric2 != true) dup.scoreGrade17 = null; }
                            if (checkg18.Text == "0") { isNumeric2 = int.TryParse(maxS18.Text, out n); if (isNumeric2 == true && dup.scoreGrade18 == null) dup.scoreGrade18 = periodnow; else if (isNumeric2 != true) dup.scoreGrade18 = null; }
                            if (checkg19.Text == "0") { isNumeric2 = int.TryParse(maxS19.Text, out n); if (isNumeric2 == true && dup.scoreGrade19 == null) dup.scoreGrade19 = periodnow; else if (isNumeric2 != true) dup.scoreGrade19 = null; }
                            if (checkg20.Text == "0") { isNumeric2 = int.TryParse(maxS20.Text, out n); if (isNumeric2 == true && dup.scoreGrade20 == null) dup.scoreGrade20 = periodnow; else if (isNumeric2 != true) dup.scoreGrade20 = null; }
                            isNumeric2 = int.TryParse(midmax1.Text, out n); if (isNumeric2 == true && dup.scoreMid1 == null) dup.scoreMid1 = periodnow; else if (isNumeric2 != true) dup.scoreMid1 = null;
                            isNumeric2 = int.TryParse(midmax2.Text, out n); if (isNumeric2 == true && dup.scoreMid2 == null) dup.scoreMid2 = periodnow; else if (isNumeric2 != true) dup.scoreMid2 = null;
                            isNumeric2 = int.TryParse(midmax3.Text, out n); if (isNumeric2 == true && dup.scoreMid3 == null) dup.scoreMid3 = periodnow; else if (isNumeric2 != true) dup.scoreMid3 = null;
                            isNumeric2 = int.TryParse(midmax4.Text, out n); if (isNumeric2 == true && dup.scoreMid4 == null) dup.scoreMid4 = periodnow; else if (isNumeric2 != true) dup.scoreMid4 = null;
                            isNumeric2 = int.TryParse(midmax5.Text, out n); if (isNumeric2 == true && dup.scoreMid5 == null) dup.scoreMid5 = periodnow; else if (isNumeric2 != true) dup.scoreMid5 = null;
                            isNumeric2 = int.TryParse(midmax6.Text, out n); if (isNumeric2 == true && dup.scoreMid6 == null) dup.scoreMid6 = periodnow; else if (isNumeric2 != true) dup.scoreMid6 = null;
                            isNumeric2 = int.TryParse(midmax7.Text, out n); if (isNumeric2 == true && dup.scoreMid7 == null) dup.scoreMid7 = periodnow; else if (isNumeric2 != true) dup.scoreMid7 = null;
                            isNumeric2 = int.TryParse(midmax8.Text, out n); if (isNumeric2 == true && dup.scoreMid8 == null) dup.scoreMid8 = periodnow; else if (isNumeric2 != true) dup.scoreMid8 = null;
                            isNumeric2 = int.TryParse(midmax9.Text, out n); if (isNumeric2 == true && dup.scoreMid9 == null) dup.scoreMid9 = periodnow; else if (isNumeric2 != true) dup.scoreMid9 = null;
                            isNumeric2 = int.TryParse(midmax10.Text, out n); if (isNumeric2 == true && dup.scoreMid10 == null) dup.scoreMid10 = periodnow; else if (isNumeric2 != true) dup.scoreMid10 = null;
                            isNumeric2 = int.TryParse(maxSmid.Text, out n); if (isNumeric2 == true && dup.scoreMidTerm == null) dup.scoreMidTerm = periodnow; else if (isNumeric2 != true) dup.scoreMidTerm = null;
                            dup.updateByEMP = sEmpID;
                            _db.SaveChanges();
                        }
                        else
                        {
                            dup.lastUpdate = DateTime.Now;
                            bool isNumeric2;
                            if (checkcw1.Text == "0") { isNumeric2 = int.TryParse(maxCW1.Text, out n); if (isNumeric2 == true) dup.scoreCheewat1 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat1 = null; }
                            if (checkcw2.Text == "0") { isNumeric2 = int.TryParse(maxCW2.Text, out n); if (isNumeric2 == true) dup.scoreCheewat2 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat2 = null; }
                            if (checkcw3.Text == "0") { isNumeric2 = int.TryParse(maxCW3.Text, out n); if (isNumeric2 == true) dup.scoreCheewat3 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat3 = null; }
                            if (checkcw4.Text == "0") { isNumeric2 = int.TryParse(maxCW4.Text, out n); if (isNumeric2 == true) dup.scoreCheewat4 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat4 = null; }
                            if (checkcw5.Text == "0") { isNumeric2 = int.TryParse(maxCW5.Text, out n); if (isNumeric2 == true) dup.scoreCheewat5 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat5 = null; }
                            if (checkcw6.Text == "0") { isNumeric2 = int.TryParse(maxCW6.Text, out n); if (isNumeric2 == true) dup.scoreCheewat6 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat6 = null; }
                            if (checkcw7.Text == "0") { isNumeric2 = int.TryParse(maxCW7.Text, out n); if (isNumeric2 == true) dup.scoreCheewat7 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat7 = null; }
                            if (checkcw8.Text == "0") { isNumeric2 = int.TryParse(maxCW8.Text, out n); if (isNumeric2 == true) dup.scoreCheewat8 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat8 = null; }
                            if (checkcw9.Text == "0") { isNumeric2 = int.TryParse(maxCW9.Text, out n); if (isNumeric2 == true) dup.scoreCheewat9 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat9 = null; }
                            if (checkcw10.Text == "0") { isNumeric2 = int.TryParse(maxCW10.Text, out n); if (isNumeric2 == true) dup.scoreCheewat10 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat10 = null; }
                            if (checkcw11.Text == "0") { isNumeric2 = int.TryParse(maxCW11.Text, out n); if (isNumeric2 == true) dup.scoreCheewat11 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat11 = null; }
                            if (checkcw12.Text == "0") { isNumeric2 = int.TryParse(maxCW12.Text, out n); if (isNumeric2 == true) dup.scoreCheewat12 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat12 = null; }
                            if (checkcw13.Text == "0") { isNumeric2 = int.TryParse(maxCW13.Text, out n); if (isNumeric2 == true) dup.scoreCheewat13 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat13 = null; }
                            if (checkcw14.Text == "0") { isNumeric2 = int.TryParse(maxCW14.Text, out n); if (isNumeric2 == true) dup.scoreCheewat14 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat14 = null; }
                            if (checkcw15.Text == "0") { isNumeric2 = int.TryParse(maxCW15.Text, out n); if (isNumeric2 == true) dup.scoreCheewat15 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat15 = null; }
                            if (checkcw16.Text == "0") { isNumeric2 = int.TryParse(maxCW16.Text, out n); if (isNumeric2 == true) dup.scoreCheewat16 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat16 = null; }
                            if (checkcw17.Text == "0") { isNumeric2 = int.TryParse(maxCW17.Text, out n); if (isNumeric2 == true) dup.scoreCheewat17 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat17 = null; }
                            if (checkcw18.Text == "0") { isNumeric2 = int.TryParse(maxCW18.Text, out n); if (isNumeric2 == true) dup.scoreCheewat18 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat18 = null; }
                            if (checkcw19.Text == "0") { isNumeric2 = int.TryParse(maxCW19.Text, out n); if (isNumeric2 == true) dup.scoreCheewat19 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat19 = null; }
                            if (checkcw20.Text == "0") { isNumeric2 = int.TryParse(maxCW20.Text, out n); if (isNumeric2 == true) dup.scoreCheewat20 = periodnow; else if (isNumeric2 != true) dup.scoreCheewat20 = null; }
                            isNumeric2 = int.TryParse(finalmax1.Text, out n); if (isNumeric2 == true) dup.scoreFinal1 = periodnow; else if (isNumeric2 != true) dup.scoreFinal1 = null;
                            isNumeric2 = int.TryParse(finalmax2.Text, out n); if (isNumeric2 == true) dup.scoreFinal2 = periodnow; else if (isNumeric2 != true) dup.scoreFinal2 = null;
                            isNumeric2 = int.TryParse(finalmax3.Text, out n); if (isNumeric2 == true) dup.scoreFinal3 = periodnow; else if (isNumeric2 != true) dup.scoreFinal3 = null;
                            isNumeric2 = int.TryParse(finalmax4.Text, out n); if (isNumeric2 == true) dup.scoreFinal4 = periodnow; else if (isNumeric2 != true) dup.scoreFinal4 = null;
                            isNumeric2 = int.TryParse(finalmax5.Text, out n); if (isNumeric2 == true) dup.scoreFinal5 = periodnow; else if (isNumeric2 != true) dup.scoreFinal5 = null;
                            isNumeric2 = int.TryParse(finalmax6.Text, out n); if (isNumeric2 == true) dup.scoreFinal6 = periodnow; else if (isNumeric2 != true) dup.scoreFinal6 = null;
                            isNumeric2 = int.TryParse(finalmax7.Text, out n); if (isNumeric2 == true) dup.scoreFinal7 = periodnow; else if (isNumeric2 != true) dup.scoreFinal7 = null;
                            isNumeric2 = int.TryParse(finalmax8.Text, out n); if (isNumeric2 == true) dup.scoreFinal8 = periodnow; else if (isNumeric2 != true) dup.scoreFinal8 = null;
                            isNumeric2 = int.TryParse(finalmax9.Text, out n); if (isNumeric2 == true) dup.scoreFinal9 = periodnow; else if (isNumeric2 != true) dup.scoreFinal9 = null;
                            isNumeric2 = int.TryParse(finalmax10.Text, out n); if (isNumeric2 == true) dup.scoreFinal10 = periodnow; else if (isNumeric2 != true) dup.scoreFinal10 = null;
                            isNumeric2 = int.TryParse(maxSlate.Text, out n); if (isNumeric2 == true) dup.scoreFinalTerm = periodnow; else if (isNumeric2 != true) dup.scoreFinalTerm = null;
                            if (checkg1.Text == "0") { isNumeric2 = int.TryParse(maxS1.Text, out n); if (isNumeric2 == true) dup.scoreGrade1 = periodnow; else if (isNumeric2 != true) dup.scoreGrade1 = null; }
                            if (checkg2.Text == "0") { isNumeric2 = int.TryParse(maxS2.Text, out n); if (isNumeric2 == true) dup.scoreGrade2 = periodnow; else if (isNumeric2 != true) dup.scoreGrade2 = null; }
                            if (checkg3.Text == "0") { isNumeric2 = int.TryParse(maxS3.Text, out n); if (isNumeric2 == true) dup.scoreGrade3 = periodnow; else if (isNumeric2 != true) dup.scoreGrade3 = null; }
                            if (checkg4.Text == "0") { isNumeric2 = int.TryParse(maxS4.Text, out n); if (isNumeric2 == true) dup.scoreGrade4 = periodnow; else if (isNumeric2 != true) dup.scoreGrade4 = null; }
                            if (checkg5.Text == "0") { isNumeric2 = int.TryParse(maxS5.Text, out n); if (isNumeric2 == true) dup.scoreGrade5 = periodnow; else if (isNumeric2 != true) dup.scoreGrade5 = null; }
                            if (checkg6.Text == "0") { isNumeric2 = int.TryParse(maxS6.Text, out n); if (isNumeric2 == true) dup.scoreGrade6 = periodnow; else if (isNumeric2 != true) dup.scoreGrade6 = null; }
                            if (checkg7.Text == "0") { isNumeric2 = int.TryParse(maxS7.Text, out n); if (isNumeric2 == true) dup.scoreGrade7 = periodnow; else if (isNumeric2 != true) dup.scoreGrade7 = null; }
                            if (checkg8.Text == "0") { isNumeric2 = int.TryParse(maxS8.Text, out n); if (isNumeric2 == true) dup.scoreGrade8 = periodnow; else if (isNumeric2 != true) dup.scoreGrade8 = null; }
                            if (checkg9.Text == "0") { isNumeric2 = int.TryParse(maxS9.Text, out n); if (isNumeric2 == true) dup.scoreGrade9 = periodnow; else if (isNumeric2 != true) dup.scoreGrade9 = null; }
                            if (checkg10.Text == "0") { isNumeric2 = int.TryParse(maxS10.Text, out n); if (isNumeric2 == true) dup.scoreGrade10 = periodnow; else if (isNumeric2 != true) dup.scoreGrade10 = null; }
                            if (checkg11.Text == "0") { isNumeric2 = int.TryParse(maxS11.Text, out n); if (isNumeric2 == true) dup.scoreGrade11 = periodnow; else if (isNumeric2 != true) dup.scoreGrade11 = null; }
                            if (checkg12.Text == "0") { isNumeric2 = int.TryParse(maxS12.Text, out n); if (isNumeric2 == true) dup.scoreGrade12 = periodnow; else if (isNumeric2 != true) dup.scoreGrade12 = null; }
                            if (checkg13.Text == "0") { isNumeric2 = int.TryParse(maxS13.Text, out n); if (isNumeric2 == true) dup.scoreGrade13 = periodnow; else if (isNumeric2 != true) dup.scoreGrade13 = null; }
                            if (checkg14.Text == "0") { isNumeric2 = int.TryParse(maxS14.Text, out n); if (isNumeric2 == true) dup.scoreGrade14 = periodnow; else if (isNumeric2 != true) dup.scoreGrade14 = null; }
                            if (checkg15.Text == "0") { isNumeric2 = int.TryParse(maxS15.Text, out n); if (isNumeric2 == true) dup.scoreGrade15 = periodnow; else if (isNumeric2 != true) dup.scoreGrade15 = null; }
                            if (checkg16.Text == "0") { isNumeric2 = int.TryParse(maxS16.Text, out n); if (isNumeric2 == true) dup.scoreGrade16 = periodnow; else if (isNumeric2 != true) dup.scoreGrade16 = null; }
                            if (checkg17.Text == "0") { isNumeric2 = int.TryParse(maxS17.Text, out n); if (isNumeric2 == true) dup.scoreGrade17 = periodnow; else if (isNumeric2 != true) dup.scoreGrade17 = null; }
                            if (checkg18.Text == "0") { isNumeric2 = int.TryParse(maxS18.Text, out n); if (isNumeric2 == true) dup.scoreGrade18 = periodnow; else if (isNumeric2 != true) dup.scoreGrade18 = null; }
                            if (checkg19.Text == "0") { isNumeric2 = int.TryParse(maxS19.Text, out n); if (isNumeric2 == true) dup.scoreGrade19 = periodnow; else if (isNumeric2 != true) dup.scoreGrade19 = null; }
                            if (checkg20.Text == "0") { isNumeric2 = int.TryParse(maxS20.Text, out n); if (isNumeric2 == true) dup.scoreGrade20 = periodnow; else if (isNumeric2 != true) dup.scoreGrade20 = null; }
                            isNumeric2 = int.TryParse(midmax1.Text, out n); if (isNumeric2 == true) dup.scoreMid1 = periodnow; else if (isNumeric2 != true) dup.scoreMid1 = null;
                            isNumeric2 = int.TryParse(midmax2.Text, out n); if (isNumeric2 == true) dup.scoreMid2 = periodnow; else if (isNumeric2 != true) dup.scoreMid2 = null;
                            isNumeric2 = int.TryParse(midmax3.Text, out n); if (isNumeric2 == true) dup.scoreMid3 = periodnow; else if (isNumeric2 != true) dup.scoreMid3 = null;
                            isNumeric2 = int.TryParse(midmax4.Text, out n); if (isNumeric2 == true) dup.scoreMid4 = periodnow; else if (isNumeric2 != true) dup.scoreMid4 = null;
                            isNumeric2 = int.TryParse(midmax5.Text, out n); if (isNumeric2 == true) dup.scoreMid5 = periodnow; else if (isNumeric2 != true) dup.scoreMid5 = null;
                            isNumeric2 = int.TryParse(midmax6.Text, out n); if (isNumeric2 == true) dup.scoreMid6 = periodnow; else if (isNumeric2 != true) dup.scoreMid6 = null;
                            isNumeric2 = int.TryParse(midmax7.Text, out n); if (isNumeric2 == true) dup.scoreMid7 = periodnow; else if (isNumeric2 != true) dup.scoreMid7 = null;
                            isNumeric2 = int.TryParse(midmax8.Text, out n); if (isNumeric2 == true) dup.scoreMid8 = periodnow; else if (isNumeric2 != true) dup.scoreMid8 = null;
                            isNumeric2 = int.TryParse(midmax9.Text, out n); if (isNumeric2 == true) dup.scoreMid9 = periodnow; else if (isNumeric2 != true) dup.scoreMid9 = null;
                            isNumeric2 = int.TryParse(midmax10.Text, out n); if (isNumeric2 == true) dup.scoreMid10 = periodnow; else if (isNumeric2 != true) dup.scoreMid10 = null;
                            isNumeric2 = int.TryParse(maxSmid.Text, out n); if (isNumeric2 == true) dup.scoreMidTerm = periodnow; else if (isNumeric2 != true) dup.scoreMidTerm = null;
                            dup.updateByEMP = sEmpID;
                            _db.SaveChanges();
                        }
                    }

                    int dgdrow = dgd.Rows.Count;

                    string[] dgd6 = new string[dgdrow];
                    string[] dgd7 = new string[dgdrow];
                    string[] dgd8 = new string[dgdrow];
                    string[] dgd9 = new string[dgdrow];
                    string[] dgd10 = new string[dgdrow];
                    string[] dgd11 = new string[dgdrow];
                    string[] dgd12 = new string[dgdrow];
                    string[] dgd13 = new string[dgdrow];
                    string[] dgd14 = new string[dgdrow];
                    string[] dgd15 = new string[dgdrow];
                    string[] dgd16 = new string[dgdrow];
                    string[] dgd17 = new string[dgdrow];
                    string[] dgd18 = new string[dgdrow];
                    string[] dgd19 = new string[dgdrow];
                    string[] dgd20 = new string[dgdrow];

                    string[] dgdcw1 = new string[dgdrow];
                    string[] dgdcw2 = new string[dgdrow];
                    string[] dgdcw3 = new string[dgdrow];
                    string[] dgdcw4 = new string[dgdrow];
                    string[] dgdcw5 = new string[dgdrow];
                    string[] dgdcw6 = new string[dgdrow];
                    string[] dgdcw7 = new string[dgdrow];
                    string[] dgdcw8 = new string[dgdrow];
                    string[] dgdcw9 = new string[dgdrow];
                    string[] dgdcw10 = new string[dgdrow];
                    string[] dgdcw11 = new string[dgdrow];
                    string[] dgdcw12 = new string[dgdrow];
                    string[] dgdcw13 = new string[dgdrow];
                    string[] dgdcw14 = new string[dgdrow];
                    string[] dgdcw15 = new string[dgdrow];
                    string[] dgdcw16 = new string[dgdrow];
                    string[] dgdcw17 = new string[dgdrow];
                    string[] dgdcw18 = new string[dgdrow];
                    string[] dgdcw19 = new string[dgdrow];
                    string[] dgdcw20 = new string[dgdrow];

                    string[] behave1 = new string[dgdrow];
                    string[] behave2 = new string[dgdrow];
                    string[] behave3 = new string[dgdrow];
                    string[] behave4 = new string[dgdrow];
                    string[] behave5 = new string[dgdrow];
                    string[] behave6 = new string[dgdrow];
                    string[] behave7 = new string[dgdrow];
                    string[] behave8 = new string[dgdrow];
                    string[] behave9 = new string[dgdrow];
                    string[] behave10 = new string[dgdrow];

                    string[] dgdmid = new string[dgdrow];
                    string[] dgdlate = new string[dgdrow];
                    string[] dgdbehave = new string[dgdrow];
                    string[] dgdreading = new string[dgdrow];
                    string[] dgdsamattana = new string[dgdrow];
                    string[] dgdother = new string[dgdrow];
                    string[] dgdTotalScore = new string[dgdrow];
                    string[] dgdTotalGrade = new string[dgdrow];
                    string[] dgdmid1 = new string[dgdrow];
                    string[] dgdmid2 = new string[dgdrow];
                    string[] dgdmid3 = new string[dgdrow];
                    string[] dgdmid4 = new string[dgdrow];
                    string[] dgdmid5 = new string[dgdrow];
                    string[] dgdmid6 = new string[dgdrow];
                    string[] dgdmid7 = new string[dgdrow];
                    string[] dgdmid8 = new string[dgdrow];
                    string[] dgdmid9 = new string[dgdrow];
                    string[] dgdmid10 = new string[dgdrow];
                    string[] dgdfinal1 = new string[dgdrow];
                    string[] dgdfinal2 = new string[dgdrow];
                    string[] dgdfinal3 = new string[dgdrow];
                    string[] dgdfinal4 = new string[dgdrow];
                    string[] dgdfinal5 = new string[dgdrow];
                    string[] dgdfinal6 = new string[dgdrow];
                    string[] dgdfinal7 = new string[dgdrow];
                    string[] dgdfinal8 = new string[dgdrow];
                    string[] dgdfinal9 = new string[dgdrow];
                    string[] dgdfinal10 = new string[dgdrow];

                    int row = 0;
                    for (row = 0; row < dgdrow; row++)
                    {
                        //dgdp2.Rows
                        var dgdp2Rows = dgdp2.Rows[row];
                        TextBox txtGrade6 = (TextBox)dgdp2Rows.FindControl("txtGrade6");
                        dgd6[row] = txtGrade6.Text;
                        TextBox txtGrade7 = (TextBox)dgdp2Rows.FindControl("txtGrade7");
                        dgd7[row] = txtGrade7.Text;
                        TextBox txtGrade8 = (TextBox)dgdp2Rows.FindControl("txtGrade8");
                        dgd8[row] = txtGrade8.Text;
                        TextBox txtGrade9 = (TextBox)dgdp2Rows.FindControl("txtGrade9");
                        dgd9[row] = txtGrade9.Text;
                        TextBox txtGrade10 = (TextBox)dgdp2Rows.FindControl("txtGrade10");
                        dgd10[row] = txtGrade10.Text;
                        //dgdp3.Rows
                        var dgdp3Rows = dgdp3.Rows[row];
                        TextBox txtGrade11 = (TextBox)dgdp3Rows.FindControl("txtGrade11");
                        dgd11[row] = txtGrade11.Text;
                        TextBox txtGrade12 = (TextBox)dgdp3Rows.FindControl("txtGrade12");
                        dgd12[row] = txtGrade12.Text;
                        TextBox txtGrade13 = (TextBox)dgdp3Rows.FindControl("txtGrade13");
                        dgd13[row] = txtGrade13.Text;
                        TextBox txtGrade14 = (TextBox)dgdp3Rows.FindControl("txtGrade14");
                        dgd14[row] = txtGrade14.Text;
                        TextBox txtGrade15 = (TextBox)dgdp3Rows.FindControl("txtGrade15");
                        dgd15[row] = txtGrade15.Text;

                        //dgd3.Rows
                        var dgd3Rows = dgd3.Rows[row];
                        TextBox txtSID = (TextBox)dgd3Rows.FindControl("sID");
                        int sID = Int32.Parse(txtSID.Text);
                        TextBox txtMidScore = (TextBox)dgd3Rows.FindControl("txtMidScore");
                        dgdmid[row] = txtMidScore.Text;
                        TextBox txtLateScore = (TextBox)dgd3Rows.FindControl("txtLateScore");
                        dgdlate[row] = txtLateScore.Text;
                        TextBox txtGoodBehavior = (TextBox)dgd3Rows.FindControl("txtGoodBehavior");
                        dgdbehave[row] = txtGoodBehavior.Text;
                        TextBox txtGoodReading = (TextBox)dgd3Rows.FindControl("txtGoodReading");
                        dgdreading[row] = txtGoodReading.Text;
                        TextBox txtSamattana = (TextBox)dgd3Rows.FindControl("txtSamattana");
                        dgdsamattana[row] = txtSamattana.Text;
                        TextBox txtTotalScore = (TextBox)dgd3Rows.FindControl("txtTotalScore");
                        dgdTotalScore[row] = txtTotalScore.Text;
                        TextBox txtTotalGrade = (TextBox)dgd3Rows.FindControl("txtTotalGrade");
                        dgdTotalGrade[row] = txtTotalGrade.Text;
                        DropDownList ddlOther = (DropDownList)dgd3Rows.FindControl("ddlOther");
                        dgdother[row] = ddlOther.SelectedValue;

                        //dgdp4.Rows
                        var dgdp4Rows = dgdp4.Rows[row];
                        TextBox txtGrade16 = (TextBox)dgdp4Rows.FindControl("txtGrade16");
                        dgd16[row] = txtGrade16.Text;
                        TextBox txtGrade17 = (TextBox)dgdp4Rows.FindControl("txtGrade17");
                        dgd17[row] = txtGrade17.Text;
                        TextBox txtGrade18 = (TextBox)dgdp4Rows.FindControl("txtGrade18");
                        dgd18[row] = txtGrade18.Text;
                        TextBox txtGrade19 = (TextBox)dgdp4Rows.FindControl("txtGrade19");
                        dgd19[row] = txtGrade19.Text;
                        TextBox txtGrade20 = (TextBox)dgdp4Rows.FindControl("txtGrade20");
                        dgd20[row] = txtGrade20.Text;
                        //dgdp5.Rows
                        var dgdp5Rows = dgdp5.Rows[row];
                        TextBox behavior1 = (TextBox)dgdp5Rows.FindControl("behave1");
                        behave1[row] = behavior1.Text;
                        TextBox behavior2 = (TextBox)dgdp5Rows.FindControl("behave2");
                        behave2[row] = behavior2.Text;
                        TextBox behavior3 = (TextBox)dgdp5Rows.FindControl("behave3");
                        behave3[row] = behavior3.Text;
                        TextBox behavior4 = (TextBox)dgdp5Rows.FindControl("behave4");
                        behave4[row] = behavior4.Text;
                        TextBox behavior5 = (TextBox)dgdp5Rows.FindControl("behave5");
                        behave5[row] = behavior5.Text;
                        //dgdp6.Rows
                        var dgdp6Rows = dgdp6.Rows[row];
                        TextBox behavior6 = (TextBox)dgdp6Rows.FindControl("behave6");
                        behave6[row] = behavior6.Text;
                        TextBox behavior7 = (TextBox)dgdp6Rows.FindControl("behave7");
                        behave7[row] = behavior7.Text;
                        TextBox behavior8 = (TextBox)dgdp6Rows.FindControl("behave8");
                        behave8[row] = behavior8.Text;
                        TextBox behavior9 = (TextBox)dgdp6Rows.FindControl("behave9");
                        behave9[row] = behavior9.Text;
                        TextBox behavior10 = (TextBox)dgdp6Rows.FindControl("behave10");
                        behave10[row] = behavior10.Text;
                        //dgdp7.Rows
                        var dgdp7Rows = dgdp7.Rows[row];
                        TextBox chewat1 = (TextBox)dgdp7Rows.FindControl("chewat1");
                        dgdcw1[row] = chewat1.Text;
                        TextBox chewat2 = (TextBox)dgdp7Rows.FindControl("chewat2");
                        dgdcw2[row] = chewat2.Text;
                        TextBox chewat3 = (TextBox)dgdp7Rows.FindControl("chewat3");
                        dgdcw3[row] = chewat3.Text;
                        TextBox chewat4 = (TextBox)dgdp7Rows.FindControl("chewat4");
                        dgdcw4[row] = chewat4.Text;
                        TextBox chewat5 = (TextBox)dgdp7Rows.FindControl("chewat5");
                        dgdcw5[row] = chewat5.Text;
                        //dgdp8.Rows
                        var dgdp8Rows = dgdp8.Rows[row];
                        TextBox chewat6 = (TextBox)dgdp8Rows.FindControl("chewat6");
                        dgdcw6[row] = chewat6.Text;
                        TextBox chewat7 = (TextBox)dgdp8Rows.FindControl("chewat7");
                        dgdcw7[row] = chewat7.Text;
                        TextBox chewat8 = (TextBox)dgdp8Rows.FindControl("chewat8");
                        dgdcw8[row] = chewat8.Text;
                        TextBox chewat9 = (TextBox)dgdp8Rows.FindControl("chewat9");
                        dgdcw9[row] = chewat9.Text;
                        TextBox chewat10 = (TextBox)dgdp8Rows.FindControl("chewat10");
                        dgdcw10[row] = chewat10.Text;
                        //dgdp9.Rows
                        var dgdp9Rows = dgdp9.Rows[row];
                        TextBox chewat11 = (TextBox)dgdp9Rows.FindControl("chewat11");
                        dgdcw11[row] = chewat11.Text;
                        TextBox chewat12 = (TextBox)dgdp9Rows.FindControl("chewat12");
                        dgdcw12[row] = chewat12.Text;
                        TextBox chewat13 = (TextBox)dgdp9Rows.FindControl("chewat13");
                        dgdcw13[row] = chewat13.Text;
                        TextBox chewat14 = (TextBox)dgdp9Rows.FindControl("chewat14");
                        dgdcw14[row] = chewat14.Text;
                        TextBox chewat15 = (TextBox)dgdp9Rows.FindControl("chewat15");
                        dgdcw15[row] = chewat15.Text;
                        //dgdp10.Rows
                        var dgdp10Rows = dgdp10.Rows[row];
                        TextBox chewat16 = (TextBox)dgdp10Rows.FindControl("chewat16");
                        dgdcw16[row] = chewat16.Text;
                        TextBox chewat17 = (TextBox)dgdp10Rows.FindControl("chewat17");
                        dgdcw17[row] = chewat17.Text;
                        TextBox chewat18 = (TextBox)dgdp10Rows.FindControl("chewat18");
                        dgdcw18[row] = chewat18.Text;
                        TextBox chewat19 = (TextBox)dgdp10Rows.FindControl("chewat19");
                        dgdcw19[row] = chewat19.Text;
                        TextBox chewat20 = (TextBox)dgdp10Rows.FindControl("chewat20");
                        dgdcw20[row] = chewat20.Text;
                        //dgdmidterm1.Rows
                        var dgdmidterm1Rows = dgdmidterm1.Rows[row];
                        TextBox midscore1 = (TextBox)dgdmidterm1Rows.FindControl("midscore1");
                        dgdmid1[row] = midscore1.Text;
                        TextBox midscore2 = (TextBox)dgdmidterm1Rows.FindControl("midscore2");
                        dgdmid2[row] = midscore2.Text;
                        TextBox midscore3 = (TextBox)dgdmidterm1Rows.FindControl("midscore3");
                        dgdmid3[row] = midscore3.Text;
                        TextBox midscore4 = (TextBox)dgdmidterm1Rows.FindControl("midscore4");
                        dgdmid4[row] = midscore4.Text;
                        TextBox midscore5 = (TextBox)dgdmidterm1Rows.FindControl("midscore5");
                        dgdmid5[row] = midscore5.Text;
                        //dgdmidterm2.Rows
                        var dgdmidterm2Rows = dgdmidterm2.Rows[row];
                        TextBox midscore6 = (TextBox)dgdmidterm2Rows.FindControl("midscore6");
                        dgdmid6[row] = midscore6.Text;
                        TextBox midscore7 = (TextBox)dgdmidterm2Rows.FindControl("midscore7");
                        dgdmid7[row] = midscore7.Text;
                        TextBox midscore8 = (TextBox)dgdmidterm2Rows.FindControl("midscore8");
                        dgdmid8[row] = midscore8.Text;
                        TextBox midscore9 = (TextBox)dgdmidterm2Rows.FindControl("midscore9");
                        dgdmid9[row] = midscore9.Text;
                        TextBox midscore10 = (TextBox)dgdmidterm2Rows.FindControl("midscore10");
                        dgdmid10[row] = midscore10.Text;
                        //dgdfinalterm1.Rows
                        var dgdfinalterm1Rows = dgdfinalterm1.Rows[row];
                        TextBox finalscore1 = (TextBox)dgdfinalterm1Rows.FindControl("finalscore1");
                        dgdfinal1[row] = finalscore1.Text;
                        TextBox finalscore2 = (TextBox)dgdfinalterm1Rows.FindControl("finalscore2");
                        dgdfinal2[row] = finalscore2.Text;
                        TextBox finalscore3 = (TextBox)dgdfinalterm1Rows.FindControl("finalscore3");
                        dgdfinal3[row] = finalscore3.Text;
                        TextBox finalscore4 = (TextBox)dgdfinalterm1Rows.FindControl("finalscore4");
                        dgdfinal4[row] = finalscore4.Text;
                        TextBox finalscore5 = (TextBox)dgdfinalterm1Rows.FindControl("finalscore5");
                        dgdfinal5[row] = finalscore5.Text;
                        //dgdfinalterm2.Rows
                        var dgdfinalterm2Rows = dgdfinalterm2.Rows[row];
                        TextBox finalscore6 = (TextBox)dgdfinalterm2Rows.FindControl("finalscore6");
                        dgdfinal6[row] = finalscore6.Text;
                        TextBox finalscore7 = (TextBox)dgdfinalterm2Rows.FindControl("finalscore7");
                        dgdfinal7[row] = finalscore7.Text;
                        TextBox finalscore8 = (TextBox)dgdfinalterm2Rows.FindControl("finalscore8");
                        dgdfinal8[row] = finalscore8.Text;
                        TextBox finalscore9 = (TextBox)dgdfinalterm2Rows.FindControl("finalscore9");
                        dgdfinal9[row] = finalscore9.Text;
                        TextBox finalscore10 = (TextBox)dgdfinalterm2Rows.FindControl("finalscore10");
                        dgdfinal10[row] = finalscore10.Text;
                    }

                    row = 0;

                    var check2local = _db.TGradeDetails.Where(w => checkGrades.nGradeId == w.nGradeId).ToList();
                    var max_id = _db.TGradeDetails.Select(s => s.nGradeDetailId).DefaultIfEmpty(0).Max();

                    foreach (GridViewRow dgItem in dgd.Rows)
                    {
                        TextBox txtSID = (TextBox)dgItem.FindControl("oneSID");
                        int sID = Int32.Parse(txtSID.Text);
                        TextBox txtGrade1 = (TextBox)dgItem.FindControl("txtGrade1");
                        string str1 = txtGrade1.Text;
                        TextBox txtGrade2 = (TextBox)dgItem.FindControl("txtGrade2");
                        string str2 = txtGrade2.Text;
                        TextBox txtGrade3 = (TextBox)dgItem.FindControl("txtGrade3");
                        string str3 = txtGrade3.Text;
                        TextBox txtGrade4 = (TextBox)dgItem.FindControl("txtGrade4");
                        string str4 = txtGrade4.Text;
                        TextBox txtGrade5 = (TextBox)dgItem.FindControl("txtGrade5");
                        string str5 = txtGrade5.Text;

                        string str6 = dgd6[row];
                        string str7 = dgd7[row];
                        string str8 = dgd8[row];
                        string str9 = dgd9[row];
                        string str10 = dgd10[row];
                        string str11 = dgd11[row];
                        string str12 = dgd12[row];
                        string str13 = dgd13[row];
                        string str14 = dgd14[row];
                        string str15 = dgd15[row];
                        string str16 = dgd16[row];
                        string str17 = dgd17[row];
                        string str18 = dgd18[row];
                        string str19 = dgd19[row];
                        string str20 = dgd20[row];

                        string cwscore1 = dgdcw1[row];
                        string cwscore2 = dgdcw2[row];
                        string cwscore3 = dgdcw3[row];
                        string cwscore4 = dgdcw4[row];
                        string cwscore5 = dgdcw5[row];
                        string cwscore6 = dgdcw6[row];
                        string cwscore7 = dgdcw7[row];
                        string cwscore8 = dgdcw8[row];
                        string cwscore9 = dgdcw9[row];
                        string cwscore10 = dgdcw10[row];
                        string cwscore11 = dgdcw11[row];
                        string cwscore12 = dgdcw12[row];
                        string cwscore13 = dgdcw13[row];
                        string cwscore14 = dgdcw14[row];
                        string cwscore15 = dgdcw15[row];
                        string cwscore16 = dgdcw16[row];
                        string cwscore17 = dgdcw17[row];
                        string cwscore18 = dgdcw18[row];
                        string cwscore19 = dgdcw19[row];
                        string cwscore20 = dgdcw20[row];

                        string mid1 = dgdmid1[row];
                        string mid2 = dgdmid2[row];
                        string mid3 = dgdmid3[row];
                        string mid4 = dgdmid4[row];
                        string mid5 = dgdmid5[row];
                        string mid6 = dgdmid6[row];
                        string mid7 = dgdmid7[row];
                        string mid8 = dgdmid8[row];
                        string mid9 = dgdmid9[row];
                        string mid10 = dgdmid10[row];
                        string final1 = dgdfinal1[row];
                        string final2 = dgdfinal2[row];
                        string final3 = dgdfinal3[row];
                        string final4 = dgdfinal4[row];
                        string final5 = dgdfinal5[row];
                        string final6 = dgdfinal6[row];
                        string final7 = dgdfinal7[row];
                        string final8 = dgdfinal8[row];
                        string final9 = dgdfinal9[row];
                        string final10 = dgdfinal10[row];

                        string totalMid = dgdmid[row];
                        string totalLate = dgdlate[row];
                        string totalBehave = dgdbehave[row];
                        string totalReading = dgdreading[row];
                        string totalSamattana = dgdsamattana[row];
                        string totalOther = dgdother[row];
                        string totalScore = dgdTotalScore[row];
                        string totalGrade = dgdTotalGrade[row];


                        student = new TGradeAttendance();

                        double quiz1 = Double.Parse(!string.IsNullOrEmpty(str1) ? str1 : "0");
                        double quiz2 = Double.Parse(!string.IsNullOrEmpty(str2) ? str2 : "0");
                        double quiz3 = Double.Parse(!string.IsNullOrEmpty(str3) ? str3 : "0");
                        double quiz4 = Double.Parse(!string.IsNullOrEmpty(str4) ? str4 : "0");
                        double quiz5 = Double.Parse(!string.IsNullOrEmpty(str5) ? str5 : "0");
                        double quiz6 = Double.Parse(!string.IsNullOrEmpty(dgd6[row]) ? dgd6[row] : "0");
                        double quiz7 = Double.Parse(!string.IsNullOrEmpty(dgd7[row]) ? dgd7[row] : "0");
                        double quiz8 = Double.Parse(!string.IsNullOrEmpty(dgd8[row]) ? dgd8[row] : "0");
                        double quiz9 = Double.Parse(!string.IsNullOrEmpty(dgd9[row]) ? dgd9[row] : "0");
                        double quiz10 = Double.Parse(!string.IsNullOrEmpty(dgd10[row]) ? dgd10[row] : "0");
                        double quiz11 = Double.Parse(!string.IsNullOrEmpty(dgd11[row]) ? dgd11[row] : "0");
                        double quiz12 = Double.Parse(!string.IsNullOrEmpty(dgd12[row]) ? dgd12[row] : "0");
                        double quiz13 = Double.Parse(!string.IsNullOrEmpty(dgd13[row]) ? dgd13[row] : "0");
                        double quiz14 = Double.Parse(!string.IsNullOrEmpty(dgd14[row]) ? dgd14[row] : "0");
                        double quiz15 = Double.Parse(!string.IsNullOrEmpty(dgd15[row]) ? dgd15[row] : "0");
                        double quiz16 = Double.Parse(!string.IsNullOrEmpty(dgd16[row]) ? dgd16[row] : "0");
                        double quiz17 = Double.Parse(!string.IsNullOrEmpty(dgd17[row]) ? dgd17[row] : "0");
                        double quiz18 = Double.Parse(!string.IsNullOrEmpty(dgd18[row]) ? dgd18[row] : "0");
                        double quiz19 = Double.Parse(!string.IsNullOrEmpty(dgd19[row]) ? dgd19[row] : "0");
                        double quiz20 = Double.Parse(!string.IsNullOrEmpty(dgd20[row]) ? dgd20[row] : "0");

                        double chewatscore1 = Double.Parse(!string.IsNullOrEmpty(dgdcw1[row]) ? dgdcw1[row] : "0");
                        double chewatscore2 = Double.Parse(!string.IsNullOrEmpty(dgdcw2[row]) ? dgdcw2[row] : "0");
                        double chewatscore3 = Double.Parse(!string.IsNullOrEmpty(dgdcw3[row]) ? dgdcw3[row] : "0");
                        double chewatscore4 = Double.Parse(!string.IsNullOrEmpty(dgdcw4[row]) ? dgdcw4[row] : "0");
                        double chewatscore5 = Double.Parse(!string.IsNullOrEmpty(dgdcw5[row]) ? dgdcw5[row] : "0");
                        double chewatscore6 = Double.Parse(!string.IsNullOrEmpty(dgdcw5[row]) ? dgdcw5[row] : "0");
                        double chewatscore7 = Double.Parse(!string.IsNullOrEmpty(dgdcw7[row]) ? dgdcw7[row] : "0");
                        double chewatscore8 = Double.Parse(!string.IsNullOrEmpty(dgdcw8[row]) ? dgdcw8[row] : "0");
                        double chewatscore9 = Double.Parse(!string.IsNullOrEmpty(dgdcw9[row]) ? dgdcw9[row] : "0");
                        double chewatscore10 = Double.Parse(!string.IsNullOrEmpty(dgdcw10[row]) ? dgdcw10[row] : "0");
                        double chewatscore11 = Double.Parse(!string.IsNullOrEmpty(dgdcw11[row]) ? dgdcw11[row] : "0");
                        double chewatscore12 = Double.Parse(!string.IsNullOrEmpty(dgdcw12[row]) ? dgdcw12[row] : "0");
                        double chewatscore13 = Double.Parse(!string.IsNullOrEmpty(dgdcw13[row]) ? dgdcw13[row] : "0");
                        double chewatscore14 = Double.Parse(!string.IsNullOrEmpty(dgdcw14[row]) ? dgdcw14[row] : "0");
                        double chewatscore15 = Double.Parse(!string.IsNullOrEmpty(dgdcw15[row]) ? dgdcw15[row] : "0");
                        double chewatscore16 = Double.Parse(!string.IsNullOrEmpty(dgdcw16[row]) ? dgdcw16[row] : "0");
                        double chewatscore17 = Double.Parse(!string.IsNullOrEmpty(dgdcw17[row]) ? dgdcw17[row] : "0");
                        double chewatscore18 = Double.Parse(!string.IsNullOrEmpty(dgdcw18[row]) ? dgdcw18[row] : "0");
                        double chewatscore19 = Double.Parse(!string.IsNullOrEmpty(dgdcw19[row]) ? dgdcw19[row] : "0");
                        double chewatscore20 = Double.Parse(!string.IsNullOrEmpty(dgdcw20[row]) ? dgdcw20[row] : "0");

                        double behaveScore1 = Double.Parse(!string.IsNullOrEmpty(behave1[row]) ? behave1[row] : "0");
                        double behaveScore2 = Double.Parse(!string.IsNullOrEmpty(behave2[row]) ? behave2[row] : "0");
                        double behaveScore3 = Double.Parse(!string.IsNullOrEmpty(behave3[row]) ? behave3[row] : "0");
                        double behaveScore4 = Double.Parse(!string.IsNullOrEmpty(behave4[row]) ? behave4[row] : "0");
                        double behaveScore5 = Double.Parse(!string.IsNullOrEmpty(behave5[row]) ? behave5[row] : "0");
                        double behaveScore6 = Double.Parse(!string.IsNullOrEmpty(behave6[row]) ? behave6[row] : "0");
                        double behaveScore7 = Double.Parse(!string.IsNullOrEmpty(behave7[row]) ? behave7[row] : "0");
                        double behaveScore8 = Double.Parse(!string.IsNullOrEmpty(behave8[row]) ? behave8[row] : "0");
                        double behaveScore9 = Double.Parse(!string.IsNullOrEmpty(behave9[row]) ? behave9[row] : "0");
                        double behaveScore10 = Double.Parse(!string.IsNullOrEmpty(behave10[row]) ? behave10[row] : "0");

                        double behavesum = behaveScore1 + behaveScore2 + behaveScore3 +
                            behaveScore4 + behaveScore5 + behaveScore6 + behaveScore7 +
                            behaveScore8 + behaveScore9 + behaveScore10;

                        double quizsum = quiz1 + quiz2 + quiz3 + quiz4 + quiz5 + quiz6 + quiz7 +
                        quiz8 + quiz9 + quiz10 + quiz11 + quiz12 + quiz13 + quiz14 + quiz15 +
                        quiz16 + quiz17 + quiz18 + quiz19 + quiz20;

                        double chewatsum = chewatscore1 + chewatscore2 + chewatscore3
                            + chewatscore4 + chewatscore5 + chewatscore6 + chewatscore7 +
                            +chewatscore8 + chewatscore9 + chewatscore10 + chewatscore11 +
                            +chewatscore12 + chewatscore13 + chewatscore14 + chewatscore15 +
                            +chewatscore16 + chewatscore17 + chewatscore18 + chewatscore19 +
                              chewatscore20;

                        quizsum = quizsum + chewatsum;

                        var check2 = check2local.Where(w => w.sID == sID).FirstOrDefault();

                        if (check2 == null)
                        {
                            detail = new TGradeDetail();

                            if (nobehaveCheck.Text == "1")
                            {
                                detail.getBehaviorTotal = "";
                                detail.getBehaviorLabel = "";
                                detail.getReadWrite = "";
                                //detail.scoreBehavior1 = "";
                                //detail.scoreBehavior2 = "";
                                //detail.scoreBehavior3 = "";
                                //detail.scoreBehavior4 = "";
                                //detail.scoreBehavior5 = "";
                                //detail.scoreBehavior6 = "";
                                //detail.scoreBehavior7 = "";
                                //detail.scoreBehavior8 = "";
                                //detail.scoreBehavior9 = "";
                                //detail.scoreBehavior10 = "";
                            }
                            else
                            {
                                detail.getBehaviorLabel = totalBehave;
                                detail.getReadWrite = totalReading;
                                detail.getSamattana = totalSamattana;
                                detail.getBehaviorTotal = behavesum.ToString();
                                //detail.scoreBehavior1 = behave1[row];
                                //detail.scoreBehavior2 = behave2[row];
                                //detail.scoreBehavior3 = behave3[row];
                                //detail.scoreBehavior4 = behave4[row];
                                //detail.scoreBehavior5 = behave5[row];
                                //detail.scoreBehavior6 = behave6[row];
                                //detail.scoreBehavior7 = behave7[row];
                                //detail.scoreBehavior8 = behave8[row];
                                //detail.scoreBehavior9 = behave9[row];
                                //detail.scoreBehavior10 = behave10[row];
                            }
                            detail.getGradeLabel = totalGrade;
                            detail.getScore100 = totalScore;
                            detail.getSpecial = totalOther;
                            detail.nGradeDetailId = ++max_id;
                            detail.nGradeId = checkGrades.nGradeId;
                            detail.sID = sID;
                            detail.scoreFinalTerm = totalLate;
                            detail.scoreMidTerm = totalMid;
                            //detail.scoreGrade1 = str1;
                            //detail.scoreGrade2 = str2;
                            //detail.scoreGrade3 = str3;
                            //detail.scoreGrade4 = str4;
                            //detail.scoreGrade5 = str5;
                            //detail.scoreGrade6 = str6;
                            //detail.scoreGrade7 = str7;
                            //detail.scoreGrade8 = str8;
                            //detail.scoreGrade9 = str9;
                            //detail.scoreGrade10 = str10;
                            //detail.scoreGrade11 = str11;
                            //detail.scoreGrade12 = str12;
                            //detail.scoreGrade13 = str13;
                            //detail.scoreGrade14 = str14;
                            //detail.scoreGrade15 = str15;
                            //detail.scoreGrade16 = str16;
                            //detail.scoreGrade17 = str17;
                            //detail.scoreGrade18 = str18;
                            //detail.scoreGrade19 = str19;
                            //detail.scoreGrade20 = str20;
                            //detail.scoreCheewat1 = cwscore1;
                            //detail.scoreCheewat2 = cwscore2;
                            //detail.scoreCheewat3 = cwscore3;
                            //detail.scoreCheewat4 = cwscore4;
                            //detail.scoreCheewat5 = cwscore5;
                            //detail.scoreCheewat6 = cwscore6;
                            //detail.scoreCheewat7 = cwscore7;
                            //detail.scoreCheewat8 = cwscore8;
                            //detail.scoreCheewat9 = cwscore9;
                            //detail.scoreCheewat10 = cwscore10;
                            //detail.scoreCheewat11 = cwscore11;
                            //detail.scoreCheewat12 = cwscore12;
                            //detail.scoreCheewat13 = cwscore13;
                            //detail.scoreCheewat14 = cwscore14;
                            //detail.scoreCheewat15 = cwscore15;
                            //detail.scoreCheewat16 = cwscore16;
                            //detail.scoreCheewat17 = cwscore17;
                            //detail.scoreCheewat18 = cwscore18;
                            //detail.scoreCheewat19 = cwscore19;
                            //detail.scoreCheewat20 = cwscore20;
                            //detail.scoreMid1 = mid1;
                            //detail.scoreMid2 = mid2;
                            //detail.scoreMid3 = mid3;
                            //detail.scoreMid4 = mid4;
                            //detail.scoreMid5 = mid5;
                            //detail.scoreMid6 = mid6;
                            //detail.scoreMid7 = mid7;
                            //detail.scoreMid8 = mid8;
                            //detail.scoreMid9 = mid9;
                            //detail.scoreMid10 = mid10;
                            //detail.scoreFinal1 = final1;
                            //detail.scoreFinal2 = final2;
                            //detail.scoreFinal3 = final3;
                            //detail.scoreFinal4 = final4;
                            //detail.scoreFinal5 = final5;
                            //detail.scoreFinal6 = final6;
                            //detail.scoreFinal7 = final7;
                            //detail.scoreFinal8 = final8;
                            //detail.scoreFinal9 = final9;
                            //detail.scoreFinal10 = final10;

                            double mid100 = 0;
                            double final100 = 0;
                            double quiz100 = Math.Round(((quizsum * checkGrades.fRatioQuiz) / Double.Parse(checkGrades.maxGradeTotal)), dicimal);
                            if (totalMid != "")
                            {
                                mid100 = Math.Round(((Double.Parse(totalMid) * checkGrades.fRatioMidTerm) / Double.Parse(checkGrades.maxMidTerm)), dicimal);
                            }
                            if (totalLate != "")
                            {
                                final100 = Math.Round(((Double.Parse(totalLate) * checkGrades.fRatioLateTerm) / Double.Parse(checkGrades.maxFinalTerm)), dicimal);
                            }

                            detail.getQuiz100 = quiz100.ToString();
                            detail.getMid100 = mid100.ToString();
                            detail.getFinal100 = final100.ToString();

                            _db.TGradeDetails.Add(detail);
                        }
                        else
                        {
                            if (nobehaveCheck.Text == "1")
                            {
                                check2.getBehaviorTotal = "";
                                check2.getBehaviorLabel = "";
                                check2.getReadWrite = "";
                                //check2.scoreBehavior1 = "";
                                //check2.scoreBehavior2 = "";
                                //check2.scoreBehavior3 = "";
                                //check2.scoreBehavior4 = "";
                                //check2.scoreBehavior5 = "";
                                //check2.scoreBehavior6 = "";
                                //check2.scoreBehavior7 = "";
                                //check2.scoreBehavior8 = "";
                                //check2.scoreBehavior9 = "";
                                //check2.scoreBehavior10 = "";
                            }
                            else
                            {
                                check2.getBehaviorTotal = behavesum.ToString();
                                check2.getBehaviorLabel = totalBehave;
                                check2.getReadWrite = totalReading;
                                check2.getSamattana = totalSamattana;
                                //check2.scoreBehavior1 = behave1[row];
                                //check2.scoreBehavior2 = behave2[row];
                                //check2.scoreBehavior3 = behave3[row];
                                //check2.scoreBehavior4 = behave4[row];
                                //check2.scoreBehavior5 = behave5[row];
                                //check2.scoreBehavior6 = behave6[row];
                                //check2.scoreBehavior7 = behave7[row];
                                //check2.scoreBehavior8 = behave8[row];
                                //check2.scoreBehavior9 = behave9[row];
                                //check2.scoreBehavior10 = behave10[row];
                            }

                            check2.getGradeLabel = totalGrade;
                            check2.getScore100 = totalScore;
                            check2.getSpecial = totalOther;
                            check2.scoreFinalTerm = totalLate;
                            check2.scoreMidTerm = totalMid;
                            //check2.scoreGrade1 = str1;
                            //check2.scoreGrade2 = str2;
                            //check2.scoreGrade3 = str3;
                            //check2.scoreGrade4 = str4;
                            //check2.scoreGrade5 = str5;
                            //check2.scoreGrade6 = str6;
                            //check2.scoreGrade7 = str7;
                            //check2.scoreGrade8 = str8;
                            //check2.scoreGrade9 = str9;
                            //check2.scoreGrade10 = str10;
                            //check2.scoreGrade11 = str11;
                            //check2.scoreGrade12 = str12;
                            //check2.scoreGrade13 = str13;
                            //check2.scoreGrade14 = str14;
                            //check2.scoreGrade15 = str15;
                            //check2.scoreGrade16 = str16;
                            //check2.scoreGrade17 = str17;
                            //check2.scoreGrade18 = str18;
                            //check2.scoreGrade19 = str19;
                            //check2.scoreGrade20 = str20;
                            //check2.scoreCheewat1 = cwscore1;
                            //check2.scoreCheewat2 = cwscore2;
                            //check2.scoreCheewat3 = cwscore3;
                            //check2.scoreCheewat4 = cwscore4;
                            //check2.scoreCheewat5 = cwscore5;
                            //check2.scoreCheewat6 = cwscore6;
                            //check2.scoreCheewat7 = cwscore7;
                            //check2.scoreCheewat8 = cwscore8;
                            //check2.scoreCheewat9 = cwscore9;
                            //check2.scoreCheewat10 = cwscore10;
                            //check2.scoreCheewat11 = cwscore11;
                            //check2.scoreCheewat12 = cwscore12;
                            //check2.scoreCheewat13 = cwscore13;
                            //check2.scoreCheewat14 = cwscore14;
                            //check2.scoreCheewat15 = cwscore15;
                            //check2.scoreCheewat16 = cwscore16;
                            //check2.scoreCheewat17 = cwscore17;
                            //check2.scoreCheewat18 = cwscore18;
                            //check2.scoreCheewat19 = cwscore19;
                            //check2.scoreCheewat20 = cwscore20;
                            //check2.scoreMid1 = mid1;
                            //check2.scoreMid2 = mid2;
                            //check2.scoreMid3 = mid3;
                            //check2.scoreMid4 = mid4;
                            //check2.scoreMid5 = mid5;
                            //check2.scoreMid6 = mid6;
                            //check2.scoreMid7 = mid7;
                            //check2.scoreMid8 = mid8;
                            //check2.scoreMid9 = mid9;
                            //check2.scoreMid10 = mid10;
                            //check2.scoreFinal1 = final1;
                            //check2.scoreFinal2 = final2;
                            //check2.scoreFinal3 = final3;
                            //check2.scoreFinal4 = final4;
                            //check2.scoreFinal5 = final5;
                            //check2.scoreFinal6 = final6;
                            //check2.scoreFinal7 = final7;
                            //check2.scoreFinal8 = final8;
                            //check2.scoreFinal9 = final9;
                            //check2.scoreFinal10 = final10;

                            double mid100 = 0;
                            double final100 = 0;
                            double quiz100 = Math.Round(((quizsum * checkGrades.fRatioQuiz) / Double.Parse(checkGrades.maxGradeTotal)), dicimal);
                            if (totalMid != "")
                            {
                                mid100 = Math.Round(((Double.Parse(totalMid) * checkGrades.fRatioMidTerm) / Double.Parse(checkGrades.maxMidTerm)), dicimal);
                            }
                            if (totalLate != "")
                            {
                                final100 = Math.Round(((Double.Parse(totalLate) * checkGrades.fRatioLateTerm) / Double.Parse(checkGrades.maxFinalTerm)), dicimal);
                            }

                            check2.getQuiz100 = quiz100.ToString();
                            check2.getMid100 = mid100.ToString();
                            check2.getFinal100 = final100.ToString();

                        }
                        row += 1;
                    }


                    if (viplogin.Text == "2")
                    {
                        DateTime now = DateTime.Now;
                        var check = _db.TSettingExtraTimes.Where(w => w.sPlaneID.ToString() == id && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.sEMP == sEmpID && w.useToken == null && w.cDel != 1 && w.addDate.Day == now.Day && w.addDate.Month == now.Month).FirstOrDefault();
                        if (check != null)
                            check.useToken = 1;
                    }

                    _db.SaveChanges();
                    transaction.Complete();
                    _db.Dispose();
                }

                string idlv = Request.QueryString["idlv"];
                string year = Request.QueryString["year"];
                string term = Request.QueryString["term"];
                string mode = Request.QueryString["mode"];
                if (mode == "3")
                {

                    Response.Redirect("BP5cover-chalerm.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term + "&id=" + id + "&mode=" + mode);
                }
                else
                {
                    Response.Redirect("GradeRoomList.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term + "&id=" + id + "&mode=" + mode);
                }
            }
            catch (DbEntityValidationException dbex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = dbex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(dbex.Message, " The validation errors are: ", fullErrorMessage);

                Response.Write(string.Format(exceptionMessage));
            }
            catch (Exception ex)
            {
                Response.Write(string.Format(ex.ToString()));
            }
        }

        string script = "<script>alert('{0}')</script>";

        void btnCancle_Click(object sender, EventArgs e)
        {

            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];
            string mode = Request.QueryString["mode"];

            Response.Redirect("GradeRoomList.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term + "&mode=" + mode);
        }

        private void OpenData()
        {
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities));

            JabJaiMasterEntities dbmaster = Connection.MasterEntities();

            var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

            string sPlaneID = Request.QueryString["id"];
            string year = Request.QueryString["year"];
            string userterm = Request.QueryString["term"];
            string idlv2 = Request.QueryString["idlv2"];
            string mode = Request.QueryString["mode"];
            string idlv = Request.QueryString["idlv"];
            int? idlvn = Int32.Parse(idlv);
            int? useryear = Int32.Parse(year);
            int? idlv2n = Int32.Parse(idlv2);
            string username = "";
            int? ntermsublv2 = 0;
            int nyear = 0;
            string nterm = "";
            int ntermtable = 0;


            //Dialog Text Box  - Default value (Assessment Name)
            setname1.Text = "1";
            setname2.Text = "2";
            setname3.Text = "3";
            setname4.Text = "4";
            setname5.Text = "5";
            setname6.Text = "6";
            setname7.Text = "7";
            setname8.Text = "8";
            setname9.Text = "9";
            setname10.Text = "10";
            setname11.Text = "11";
            setname12.Text = "12";
            setname13.Text = "13";
            setname14.Text = "14";
            setname15.Text = "15";
            setname16.Text = "16";
            setname17.Text = "17";
            setname18.Text = "18";
            setname19.Text = "19";
            setname20.Text = "20";
            chewatname1.Text = "1";
            chewatname2.Text = "2";
            chewatname3.Text = "3";
            chewatname4.Text = "4";
            chewatname5.Text = "5";
            chewatname6.Text = "6";
            chewatname7.Text = "7";
            chewatname8.Text = "8";
            chewatname9.Text = "9";
            chewatname10.Text = "10";
            chewatname11.Text = "11";
            chewatname12.Text = "12";
            chewatname13.Text = "13";
            chewatname14.Text = "14";
            chewatname15.Text = "15";
            chewatname16.Text = "16";
            chewatname17.Text = "17";
            chewatname18.Text = "18";
            chewatname19.Text = "19";
            chewatname20.Text = "20";
            setnameb1.Text = "1 รักชาติ ศาสน์ กษัตริย์";
            setnameb2.Text = "2 ซื่อสัตย์สุจริต";
            setnameb3.Text = "3 มีวินัย";
            setnameb4.Text = "4 ใฝ่เรียนรู้";
            setnameb5.Text = "5 อยู่อย่างพอเพียง";
            setnameb6.Text = "6 มุ่งมั่นการทำงาน";
            setnameb7.Text = "7 รักความเป็นไทย";
            setnameb8.Text = "8 มีจิตสาธารณะ";
            setnameb9.Text = "9";
            setnameb10.Text = "10";
            midmodalname1.Text = "1";
            midmodalname2.Text = "2";
            midmodalname3.Text = "3";
            midmodalname4.Text = "4";
            midmodalname5.Text = "5";
            midmodalname6.Text = "6";
            midmodalname7.Text = "7";
            midmodalname8.Text = "8";
            midmodalname9.Text = "9";
            midmodalname10.Text = "10";
            finalmodalname1.Text = "1";
            finalmodalname2.Text = "2";
            finalmodalname3.Text = "3";
            finalmodalname4.Text = "4";
            finalmodalname5.Text = "5";
            finalmodalname6.Text = "6";
            finalmodalname7.Text = "7";
            finalmodalname8.Text = "8";
            finalmodalname9.Text = "9";
            finalmodalname10.Text = "10";

            //Get the TYear Table Id
            foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear))
            {
                nyear = ff.nYear;
            }

            //Get the TTerm Table Id
            foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear))
            {
                nterm = ee.nTerm;
            }

            // Show the Time Period in Above the Student Grid
            if (tCompany.settingTimePeriod == 1)
            {
                var time = _db.TGradeRegisterPeriods.Where(w => w.nTerm == nterm).FirstOrDefault();
                if (time != null)
                {
                    DateTime now = DateTime.Now.Date;
                    submitPeriod.Text = "";
                    periodNow.Text = "6";

                    if (time.afterMidtermEnd != null && time.afterMidtermStart != null)
                        if (now <= time.afterMidtermEnd.Value.Date && now >= time.afterMidtermStart.Value.Date)
                        {
                            submitPeriod.Text = "ช่วงหลังกลางภาค " + datetimetext(time.afterMidtermStart) + " ถึง " + datetimetext(time.afterMidtermEnd);
                            periodNow.Text = "3";
                        }
                    if (time.duringMidtermEnd != null && time.duringMidtermStart != null)
                        if (now <= time.duringMidtermEnd.Value.Date && now >= time.duringMidtermStart.Value.Date)
                        {
                            submitPeriod.Text = "ช่วงระหว่างกลางภาค " + datetimetext(time.duringMidtermStart) + " ถึง " + datetimetext(time.duringMidtermEnd);
                            periodNow.Text = "2";
                        }
                    if (time.beforeMidtermEnd != null && time.beforeMidtermStart != null)
                        if (now <= time.beforeMidtermEnd.Value.Date && now >= time.beforeMidtermStart.Value.Date)
                        {
                            submitPeriod.Text = "ช่วงก่อนกลางภาค " + datetimetext(time.beforeMidtermStart) + " ถึง " + datetimetext(time.beforeMidtermEnd);
                            periodNow.Text = "1";
                        }
                    if (time.FinaltermEnd != null && time.FinaltermStart != null)
                        if (now <= time.FinaltermEnd.Value.Date && now >= time.FinaltermStart.Value.Date)
                        {
                            submitPeriod.Text = "ช่วงปลายเทอม " + datetimetext(time.FinaltermStart) + " ถึง " + datetimetext(time.FinaltermEnd);
                            periodNow.Text = "4";
                        }
                    if (time.ExtraEnd != null && time.ExtraStart != null)
                        if (now <= time.ExtraEnd.Value.Date && now >= time.ExtraStart.Value.Date)
                        {
                            submitPeriod.Text = "ช่วงพิเศษ " + datetimetext(time.ExtraStart) + " ถึง " + datetimetext(time.ExtraEnd);
                            periodNow.Text = "5";
                        }


                }
                else
                {
                    submitPeriod.Text = "";
                    periodNow.Text = "6";
                }
            }
            else
            {
                submitPeriod.Text = "";
                periodNow.Text = "6";
            }

            ddlbox1.Text = "-1";
            ddlbox2.Text = "-1";
            ddlbox3.Text = "-1";
            ddlbox4.Text = "-1";

            var ratio = _db.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == sPlaneID && w.nTermSubLevel2 == idlv2n).FirstOrDefault();


            if (ratio != null)
            {
                gradeidtxt.Text = ratio.nGradeId.ToString();
                ddlbox1.Text = ratio.fRatioQuiz.ToString();
                ddlbox2.Text = ratio.fRatioMidTerm.ToString();
                ddlbox3.Text = ratio.fRatioLateTerm.ToString();
                if (ratio.fRatioQuizPass != null)
                    ddlbox4.Text = ratio.fRatioQuizPass.ToString();

                setup1.Text = ratio.GradeDicimal;
                setup2.Text = ratio.GradeAutoReadScore;
                setup3.Text = ratio.GradeAddBehavior;
                setup4.Text = ratio.GradeAutoBehaviorScore;
                setup5.Text = ratio.GradeAddCheewat;
                setup6.Text = ratio.GradeCloseBehaviorReadWrite;
                setup7.Text = ratio.GradeShareData;
                setup8.Text = ratio.GradeCloseGrade;
                setup9.Text = ratio.GradeCloseSamattana;
                setup11.Text = ratio.optionMid;
                setup12.Text = ratio.optionFinal;

                var glock = _db.TGradeLocks.Where(w => w.nGradeId == ratio.nGradeId).FirstOrDefault();
                if (glock != null)
                {
                    if (glock.scoreGrade1 != null) lockg1.Text = glock.scoreGrade1.ToString();
                    if (glock.scoreGrade2 != null) lockg2.Text = glock.scoreGrade2.ToString();
                    if (glock.scoreGrade3 != null) lockg3.Text = glock.scoreGrade3.ToString();
                    if (glock.scoreGrade4 != null) lockg4.Text = glock.scoreGrade4.ToString();
                    if (glock.scoreGrade5 != null) lockg5.Text = glock.scoreGrade5.ToString();
                    if (glock.scoreGrade6 != null) lockg6.Text = glock.scoreGrade6.ToString();
                    if (glock.scoreGrade7 != null) lockg7.Text = glock.scoreGrade7.ToString();
                    if (glock.scoreGrade8 != null) lockg8.Text = glock.scoreGrade8.ToString();
                    if (glock.scoreGrade9 != null) lockg9.Text = glock.scoreGrade9.ToString();
                    if (glock.scoreGrade10 != null) lockg10.Text = glock.scoreGrade10.ToString();
                    if (glock.scoreGrade11 != null) lockg11.Text = glock.scoreGrade11.ToString();
                    if (glock.scoreGrade12 != null) lockg12.Text = glock.scoreGrade12.ToString();
                    if (glock.scoreGrade13 != null) lockg13.Text = glock.scoreGrade13.ToString();
                    if (glock.scoreGrade14 != null) lockg14.Text = glock.scoreGrade14.ToString();
                    if (glock.scoreGrade15 != null) lockg15.Text = glock.scoreGrade15.ToString();
                    if (glock.scoreGrade16 != null) lockg16.Text = glock.scoreGrade16.ToString();
                    if (glock.scoreGrade17 != null) lockg17.Text = glock.scoreGrade17.ToString();
                    if (glock.scoreGrade18 != null) lockg18.Text = glock.scoreGrade18.ToString();
                    if (glock.scoreGrade19 != null) lockg19.Text = glock.scoreGrade19.ToString();
                    if (glock.scoreGrade20 != null) lockg20.Text = glock.scoreGrade20.ToString();

                    if (glock.scoreCheewat1 != null) lockcw1.Text = glock.scoreCheewat1.ToString();
                    if (glock.scoreCheewat2 != null) lockcw2.Text = glock.scoreCheewat2.ToString();
                    if (glock.scoreCheewat3 != null) lockcw3.Text = glock.scoreCheewat3.ToString();
                    if (glock.scoreCheewat4 != null) lockcw4.Text = glock.scoreCheewat4.ToString();
                    if (glock.scoreCheewat5 != null) lockcw5.Text = glock.scoreCheewat5.ToString();
                    if (glock.scoreCheewat6 != null) lockcw6.Text = glock.scoreCheewat6.ToString();
                    if (glock.scoreCheewat7 != null) lockcw7.Text = glock.scoreCheewat7.ToString();
                    if (glock.scoreCheewat8 != null) lockcw8.Text = glock.scoreCheewat8.ToString();
                    if (glock.scoreCheewat9 != null) lockcw9.Text = glock.scoreCheewat9.ToString();
                    if (glock.scoreCheewat10 != null) lockcw10.Text = glock.scoreCheewat10.ToString();
                    if (glock.scoreCheewat11 != null) lockcw11.Text = glock.scoreCheewat11.ToString();
                    if (glock.scoreCheewat12 != null) lockcw12.Text = glock.scoreCheewat12.ToString();
                    if (glock.scoreCheewat13 != null) lockcw13.Text = glock.scoreCheewat13.ToString();
                    if (glock.scoreCheewat14 != null) lockcw14.Text = glock.scoreCheewat14.ToString();
                    if (glock.scoreCheewat15 != null) lockcw15.Text = glock.scoreCheewat15.ToString();
                    if (glock.scoreCheewat16 != null) lockcw16.Text = glock.scoreCheewat16.ToString();
                    if (glock.scoreCheewat17 != null) lockcw17.Text = glock.scoreCheewat17.ToString();
                    if (glock.scoreCheewat18 != null) lockcw18.Text = glock.scoreCheewat18.ToString();
                    if (glock.scoreCheewat19 != null) lockcw19.Text = glock.scoreCheewat19.ToString();
                    if (glock.scoreCheewat20 != null) lockcw20.Text = glock.scoreCheewat20.ToString();

                    if (glock.scoreMid1 != null) lockm1.Text = glock.scoreMid1.ToString();
                    if (glock.scoreMid2 != null) lockm2.Text = glock.scoreMid2.ToString();
                    if (glock.scoreMid3 != null) lockm3.Text = glock.scoreMid3.ToString();
                    if (glock.scoreMid4 != null) lockm4.Text = glock.scoreMid4.ToString();
                    if (glock.scoreMid5 != null) lockm5.Text = glock.scoreMid5.ToString();
                    if (glock.scoreMid6 != null) lockm6.Text = glock.scoreMid6.ToString();
                    if (glock.scoreMid7 != null) lockm7.Text = glock.scoreMid7.ToString();
                    if (glock.scoreMid8 != null) lockm8.Text = glock.scoreMid8.ToString();
                    if (glock.scoreMid9 != null) lockm9.Text = glock.scoreMid9.ToString();
                    if (glock.scoreMid10 != null) lockm10.Text = glock.scoreMid10.ToString();

                    if (glock.scoreFinal1 != null) lockf1.Text = glock.scoreFinal1.ToString();
                    if (glock.scoreFinal2 != null) lockf2.Text = glock.scoreFinal2.ToString();
                    if (glock.scoreFinal3 != null) lockf3.Text = glock.scoreFinal3.ToString();
                    if (glock.scoreFinal4 != null) lockf4.Text = glock.scoreFinal4.ToString();
                    if (glock.scoreFinal5 != null) lockf5.Text = glock.scoreFinal5.ToString();
                    if (glock.scoreFinal6 != null) lockf6.Text = glock.scoreFinal6.ToString();
                    if (glock.scoreFinal7 != null) lockf7.Text = glock.scoreFinal7.ToString();
                    if (glock.scoreFinal8 != null) lockf8.Text = glock.scoreFinal8.ToString();
                    if (glock.scoreFinal9 != null) lockf9.Text = glock.scoreFinal9.ToString();
                    if (glock.scoreFinal10 != null) lockf10.Text = glock.scoreFinal10.ToString();

                    if (glock.scoreMidTerm != null) lockmid.Text = glock.scoreMidTerm.ToString();
                    if (glock.scoreFinalTerm != null) lockfinal.Text = glock.scoreFinalTerm.ToString();

                    //if (ratio.maxCheewat1 == "") lockcw1.Text = "";
                    //if (ratio.maxCheewat2 == "") lockcw2.Text = "";
                    //if (ratio.maxCheewat3 == "") lockcw3.Text = "";
                    //if (ratio.maxCheewat4 == "") lockcw4.Text = "";
                    //if (ratio.maxCheewat5 == "") lockcw5.Text = "";
                    //if (ratio.maxCheewat6 == "") lockcw6.Text = "";
                    //if (ratio.maxCheewat7 == "") lockcw7.Text = "";
                    //if (ratio.maxCheewat8 == "") lockcw8.Text = "";
                    //if (ratio.maxCheewat9 == "") lockcw9.Text = "";
                    //if (ratio.maxCheewat10 == "") lockcw10.Text = "";
                    //if (ratio.maxCheewat11 == "") lockcw11.Text = "";
                    //if (ratio.maxCheewat12 == "") lockcw12.Text = "";
                    //if (ratio.maxCheewat13 == "") lockcw13.Text = "";
                    //if (ratio.maxCheewat14 == "") lockcw14.Text = "";
                    //if (ratio.maxCheewat15 == "") lockcw15.Text = "";
                    //if (ratio.maxCheewat16 == "") lockcw16.Text = "";
                    //if (ratio.maxCheewat17 == "") lockcw17.Text = "";
                    //if (ratio.maxCheewat18 == "") lockcw18.Text = "";
                    //if (ratio.maxCheewat19 == "") lockcw19.Text = "";
                    //if (ratio.maxCheewat20 == "") lockcw20.Text = "";

                    //if (ratio.maxFinal1 == "") lockf1.Text = "";
                    //if (ratio.maxFinal2 == "") lockf2.Text = "";
                    //if (ratio.maxFinal3 == "") lockf3.Text = "";
                    //if (ratio.maxFinal4 == "") lockf4.Text = "";
                    //if (ratio.maxFinal5 == "") lockf5.Text = "";
                    //if (ratio.maxFinal6 == "") lockf6.Text = "";
                    //if (ratio.maxFinal7 == "") lockf7.Text = "";
                    //if (ratio.maxFinal8 == "") lockf8.Text = "";
                    //if (ratio.maxFinal9 == "") lockf9.Text = "";
                    //if (ratio.maxFinal10 == "") lockf10.Text = "";

                    //if (ratio.maxGrade1 == "") lockg1.Text = "";
                    //if (ratio.maxGrade2 == "") lockg2.Text = "";
                    //if (ratio.maxGrade3 == "") lockg3.Text = "";
                    //if (ratio.maxGrade4 == "") lockg4.Text = "";
                    //if (ratio.maxGrade5 == "") lockg5.Text = "";
                    //if (ratio.maxGrade6 == "") lockg6.Text = "";
                    //if (ratio.maxGrade7 == "") lockg7.Text = "";
                    //if (ratio.maxGrade8 == "") lockg8.Text = "";
                    //if (ratio.maxGrade9 == "") lockg9.Text = "";
                    //if (ratio.maxGrade10 == "") lockg10.Text = "";
                    //if (ratio.maxGrade11 == "") lockg11.Text = "";
                    //if (ratio.maxGrade12 == "") lockg12.Text = "";
                    //if (ratio.maxGrade13 == "") lockg13.Text = "";
                    //if (ratio.maxGrade14 == "") lockg14.Text = "";
                    //if (ratio.maxGrade15 == "") lockg15.Text = "";
                    //if (ratio.maxGrade16 == "") lockg16.Text = "";
                    //if (ratio.maxGrade17 == "") lockg17.Text = "";
                    //if (ratio.maxGrade18 == "") lockg18.Text = "";
                    //if (ratio.maxGrade19 == "") lockg19.Text = "";
                    //if (ratio.maxGrade20 == "") lockg20.Text = "";

                    //if (ratio.maxMid1 == "") lockm1.Text = "";
                    //if (ratio.maxMid2 == "") lockm2.Text = "";
                    //if (ratio.maxMid3 == "") lockm3.Text = "";
                    //if (ratio.maxMid4 == "") lockm4.Text = "";
                    //if (ratio.maxMid5 == "") lockm5.Text = "";
                    //if (ratio.maxMid6 == "") lockm6.Text = "";
                    //if (ratio.maxMid7 == "") lockm7.Text = "";
                    //if (ratio.maxMid8 == "") lockm8.Text = "";
                    //if (ratio.maxMid9 == "") lockm9.Text = "";
                    //if (ratio.maxMid10 == "") lockm10.Text = "";


                }
            }



            int vip = 0;
            int sEmpID = int.Parse(Session["sEmpID"] + "");
            if (tCompany.settingGradeAdmin == 1)
            {
                var emp = _db.TEmployees.Where(w => w.sEmp == sEmpID).FirstOrDefault();
                if (emp.gradeSystemAdmin == 1)
                    vip = 1;
            }


            var extra = _db.TSettingExtraTimes.Where(w => w.sEMP == sEmpID && w.nTerm == nterm && w.sPlaneID.ToString() == sPlaneID && w.nTermSubLevel2 == idlv2n && w.useToken != 1 && w.cDel != 1 && w.addDate.Day == DateTime.Now.Day && w.addDate.Month == DateTime.Now.Month).FirstOrDefault();
            if (extra != null)
            {
                vip = 2;
            }

            if (vip == 1)
            {
                lockg1.Text = "";
                lockg2.Text = "";
                lockg3.Text = "";
                lockg4.Text = "";
                lockg5.Text = "";
                lockg6.Text = "";
                lockg7.Text = "";
                lockg8.Text = "";
                lockg9.Text = "";
                lockg10.Text = "";
                lockg11.Text = "";
                lockg12.Text = "";
                lockg13.Text = "";
                lockg14.Text = "";
                lockg15.Text = "";
                lockg16.Text = "";
                lockg17.Text = "";
                lockg18.Text = "";
                lockg19.Text = "";
                lockg20.Text = "";

                lockcw1.Text = "";
                lockcw2.Text = "";
                lockcw3.Text = "";
                lockcw4.Text = "";
                lockcw5.Text = "";
                lockcw6.Text = "";
                lockcw7.Text = "";
                lockcw8.Text = "";
                lockcw9.Text = "";
                lockcw10.Text = "";
                lockcw11.Text = "";
                lockcw12.Text = "";
                lockcw13.Text = "";
                lockcw14.Text = "";
                lockcw15.Text = "";
                lockcw16.Text = "";
                lockcw17.Text = "";
                lockcw18.Text = "";
                lockcw19.Text = "";
                lockcw20.Text = "";

                lockm1.Text = "";
                lockm2.Text = "";
                lockm3.Text = "";
                lockm4.Text = "";
                lockm5.Text = "";
                lockm6.Text = "";
                lockm7.Text = "";
                lockm8.Text = "";
                lockm9.Text = "";
                lockm10.Text = "";

                lockf1.Text = "";
                lockf2.Text = "";
                lockf3.Text = "";
                lockf4.Text = "";
                lockf5.Text = "";
                lockf6.Text = "";
                lockf7.Text = "";
                lockf8.Text = "";
                lockf9.Text = "";
                lockf10.Text = "";

                lockmid.Text = "";
                lockfinal.Text = "";

                periodNow.Text = "0";
                viplogin.Text = "1";
            }
            if (vip == 2)
            {
                lockg1.Text = "";
                lockg2.Text = "";
                lockg3.Text = "";
                lockg4.Text = "";
                lockg5.Text = "";
                lockg6.Text = "";
                lockg7.Text = "";
                lockg8.Text = "";
                lockg9.Text = "";
                lockg10.Text = "";
                lockg11.Text = "";
                lockg12.Text = "";
                lockg13.Text = "";
                lockg14.Text = "";
                lockg15.Text = "";
                lockg16.Text = "";
                lockg17.Text = "";
                lockg18.Text = "";
                lockg19.Text = "";
                lockg20.Text = "";

                lockcw1.Text = "";
                lockcw2.Text = "";
                lockcw3.Text = "";
                lockcw4.Text = "";
                lockcw5.Text = "";
                lockcw6.Text = "";
                lockcw7.Text = "";
                lockcw8.Text = "";
                lockcw9.Text = "";
                lockcw10.Text = "";
                lockcw11.Text = "";
                lockcw12.Text = "";
                lockcw13.Text = "";
                lockcw14.Text = "";
                lockcw15.Text = "";
                lockcw16.Text = "";
                lockcw17.Text = "";
                lockcw18.Text = "";
                lockcw19.Text = "";
                lockcw20.Text = "";

                lockm1.Text = "";
                lockm2.Text = "";
                lockm3.Text = "";
                lockm4.Text = "";
                lockm5.Text = "";
                lockm6.Text = "";
                lockm7.Text = "";
                lockm8.Text = "";
                lockm9.Text = "";
                lockm10.Text = "";

                lockf1.Text = "";
                lockf2.Text = "";
                lockf3.Text = "";
                lockf4.Text = "";
                lockf5.Text = "";
                lockf6.Text = "";
                lockf7.Text = "";
                lockf8.Text = "";
                lockf9.Text = "";
                lockf10.Text = "";

                lockmid.Text = "";
                lockfinal.Text = "";

                //periodNow.Text = "-1";
                viplogin.Text = "2";
            }


            int number = 1;
            studentlist2 student = new studentlist2();
            List<studentlist2> studentlist = new List<studentlist2>();
            var max = _db.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == sPlaneID && w.GradeShareData != "1").FirstOrDefault();
            if (max != null)
            {
                //if (max.nameGrade1 != null)
                //    setname1.Text = max.nameGrade1;
                //if (max.nameGrade2 != null)
                //    setname2.Text = max.nameGrade2;
                //if (max.nameGrade3 != null)
                //    setname3.Text = max.nameGrade3;
                //if (max.nameGrade4 != null)
                //    setname4.Text = max.nameGrade4;
                //if (max.nameGrade5 != null)
                //    setname5.Text = max.nameGrade5;
                //if (max.nameGrade6 != null)
                //    setname6.Text = max.nameGrade6;
                //if (max.nameGrade7 != null)
                //    setname7.Text = max.nameGrade7;
                //if (max.nameGrade8 != null)
                //    setname8.Text = max.nameGrade8;
                //if (max.nameGrade9 != null)
                //    setname9.Text = max.nameGrade9;
                //if (max.nameGrade10 != null)
                //    setname10.Text = max.nameGrade10;
                //if (max.nameGrade11 != null)
                //    setname11.Text = max.nameGrade11;
                //if (max.nameGrade12 != null)
                //    setname12.Text = max.nameGrade12;
                //if (max.nameGrade13 != null)
                //    setname13.Text = max.nameGrade13;
                //if (max.nameGrade14 != null)
                //    setname14.Text = max.nameGrade14;
                //if (max.nameGrade15 != null)
                //    setname15.Text = max.nameGrade15;
                //if (max.nameGrade16 != null)
                //    setname16.Text = max.nameGrade16;
                //if (max.nameGrade17 != null)
                //    setname17.Text = max.nameGrade17;
                //if (max.nameGrade18 != null)
                //    setname18.Text = max.nameGrade18;
                //if (max.nameGrade19 != null)
                //    setname19.Text = max.nameGrade19;
                //if (max.nameGrade20 != null)
                //    setname20.Text = max.nameGrade20;

                //if (max.nameCheewat1 != null)
                //    chewatname1.Text = max.nameCheewat1;
                //if (max.nameCheewat2 != null)
                //    chewatname2.Text = max.nameCheewat2;
                //if (max.nameCheewat3 != null)
                //    chewatname3.Text = max.nameCheewat3;
                //if (max.nameCheewat4 != null)
                //    chewatname4.Text = max.nameCheewat4;
                //if (max.nameCheewat5 != null)
                //    chewatname5.Text = max.nameCheewat5;
                //if (max.nameCheewat6 != null)
                //    chewatname6.Text = max.nameCheewat6;
                //if (max.nameCheewat7 != null)
                //    chewatname7.Text = max.nameCheewat7;
                //if (max.nameCheewat8 != null)
                //    chewatname8.Text = max.nameCheewat8;
                //if (max.nameCheewat9 != null)
                //    chewatname9.Text = max.nameCheewat9;
                //if (max.nameCheewat10 != null)
                //    chewatname10.Text = max.nameCheewat10;
                //if (max.nameCheewat11 != null)
                //    chewatname11.Text = max.nameCheewat11;
                //if (max.nameCheewat12 != null)
                //    chewatname12.Text = max.nameCheewat12;
                //if (max.nameCheewat13 != null)
                //    chewatname13.Text = max.nameCheewat13;
                //if (max.nameCheewat14 != null)
                //    chewatname14.Text = max.nameCheewat14;
                //if (max.nameCheewat15 != null)
                //    chewatname15.Text = max.nameCheewat15;
                //if (max.nameCheewat16 != null)
                //    chewatname16.Text = max.nameCheewat16;
                //if (max.nameCheewat17 != null)
                //    chewatname17.Text = max.nameCheewat17;
                //if (max.nameCheewat18 != null)
                //    chewatname18.Text = max.nameCheewat18;
                //if (max.nameCheewat19 != null)
                //    chewatname19.Text = max.nameCheewat19;
                //if (max.nameCheewat20 != null)
                //    chewatname20.Text = max.nameCheewat20;

                //if (max.nameBehavior1 != null)
                //    setnameb1.Text = max.nameBehavior1;
                //if (max.nameBehavior2 != null)
                //    setnameb2.Text = max.nameBehavior2;
                //if (max.nameBehavior3 != null)
                //    setnameb3.Text = max.nameBehavior3;
                //if (max.nameBehavior4 != null)
                //    setnameb4.Text = max.nameBehavior4;
                //if (max.nameBehavior5 != null)
                //    setnameb5.Text = max.nameBehavior5;
                //if (max.nameBehavior6 != null)
                //    setnameb6.Text = max.nameBehavior6;
                //if (max.nameBehavior7 != null)
                //    setnameb7.Text = max.nameBehavior7;
                //if (max.nameBehavior8 != null)
                //    setnameb8.Text = max.nameBehavior8;
                //if (max.nameBehavior9 != null)
                //    setnameb9.Text = max.nameBehavior9;
                //if (max.nameBehavior10 != null)
                //    setnameb10.Text = max.nameBehavior10;

                //if (max.nameMid1 != null)
                //    midmodalname1.Text = max.nameMid1;
                //if (max.nameMid2 != null)
                //    midmodalname2.Text = max.nameMid2;
                //if (max.nameMid3 != null)
                //    midmodalname3.Text = max.nameMid3;
                //if (max.nameMid4 != null)
                //    midmodalname4.Text = max.nameMid4;
                //if (max.nameMid5 != null)
                //    midmodalname5.Text = max.nameMid5;
                //if (max.nameMid6 != null)
                //    midmodalname6.Text = max.nameMid6;
                //if (max.nameMid7 != null)
                //    midmodalname7.Text = max.nameMid7;
                //if (max.nameMid8 != null)
                //    midmodalname8.Text = max.nameMid8;
                //if (max.nameMid9 != null)
                //    midmodalname9.Text = max.nameMid9;
                //if (max.nameMid10 != null)
                //    midmodalname10.Text = max.nameMid10;

                //if (max.nameFinal1 != null)
                //    finalmodalname1.Text = max.nameFinal1;
                //if (max.nameFinal2 != null)
                //    finalmodalname2.Text = max.nameFinal2;
                //if (max.nameFinal3 != null)
                //    finalmodalname3.Text = max.nameFinal3;
                //if (max.nameFinal4 != null)
                //    finalmodalname4.Text = max.nameFinal4;
                //if (max.nameFinal5 != null)
                //    finalmodalname5.Text = max.nameFinal5;
                //if (max.nameFinal6 != null)
                //    finalmodalname6.Text = max.nameFinal6;
                //if (max.nameFinal7 != null)
                //    finalmodalname7.Text = max.nameFinal7;
                //if (max.nameFinal8 != null)
                //    finalmodalname8.Text = max.nameFinal8;
                //if (max.nameFinal9 != null)
                //    finalmodalname9.Text = max.nameFinal9;
                //if (max.nameFinal10 != null)
                //    finalmodalname10.Text = max.nameFinal10;

                //if (max.maxMid1 != null)
                //    midmax1.Text = max.maxMid1;
                //if (max.maxMid2 != null)
                //    midmax2.Text = max.maxMid2;
                //if (max.maxMid3 != null)
                //    midmax3.Text = max.maxMid3;
                //if (max.maxMid4 != null)
                //    midmax4.Text = max.maxMid4;
                //if (max.maxMid5 != null)
                //    midmax5.Text = max.maxMid5;
                //if (max.maxMid6 != null)
                //    midmax6.Text = max.maxMid6;
                //if (max.maxMid7 != null)
                //    midmax7.Text = max.maxMid7;
                //if (max.maxMid8 != null)
                //    midmax8.Text = max.maxMid8;
                //if (max.maxMid9 != null)
                //    midmax9.Text = max.maxMid9;
                //if (max.maxMid10 != null)
                //    midmax10.Text = max.maxMid10;

                //if (max.maxFinal1 != null)
                //    finalmax1.Text = max.maxFinal1;
                //if (max.maxFinal2 != null)
                //    finalmax2.Text = max.maxFinal2;
                //if (max.maxFinal3 != null)
                //    finalmax3.Text = max.maxFinal3;
                //if (max.maxFinal4 != null)
                //    finalmax4.Text = max.maxFinal4;
                //if (max.maxFinal5 != null)
                //    finalmax5.Text = max.maxFinal5;
                //if (max.maxFinal6 != null)
                //    finalmax6.Text = max.maxFinal6;
                //if (max.maxFinal7 != null)
                //    finalmax7.Text = max.maxFinal7;
                //if (max.maxFinal8 != null)
                //    finalmax8.Text = max.maxFinal8;
                //if (max.maxFinal9 != null)
                //    finalmax9.Text = max.maxFinal9;
                //if (max.maxFinal10 != null)
                //    finalmax10.Text = max.maxFinal10;

                //if (max.maxGrade1 != null)
                //    maxS1.Text = max.maxGrade1.ToString();
                //if (max.maxGrade2 != null)
                //    maxS2.Text = max.maxGrade2.ToString();
                //if (max.maxGrade3 != null)
                //    maxS3.Text = max.maxGrade3.ToString();
                //if (max.maxGrade4 != null)
                //    maxS4.Text = max.maxGrade4.ToString();
                //if (max.maxGrade5 != null)
                //    maxS5.Text = max.maxGrade5.ToString();
                //if (max.maxGrade6 != null)
                //    maxS6.Text = max.maxGrade6.ToString();
                //if (max.maxGrade7 != null)
                //    maxS7.Text = max.maxGrade7.ToString();
                //if (max.maxGrade8 != null)
                //    maxS8.Text = max.maxGrade8.ToString();
                //if (max.maxGrade9 != null)
                //    maxS9.Text = max.maxGrade9.ToString();
                //if (max.maxGrade10 != null)
                //    maxS10.Text = max.maxGrade10.ToString();
                //if (max.maxGrade11 != null)
                //    maxS11.Text = max.maxGrade11.ToString();
                //if (max.maxGrade12 != null)
                //    maxS12.Text = max.maxGrade12.ToString();
                //if (max.maxGrade13 != null)
                //    maxS13.Text = max.maxGrade13.ToString();
                //if (max.maxGrade14 != null)
                //    maxS14.Text = max.maxGrade14.ToString();
                //if (max.maxGrade15 != null)
                //    maxS15.Text = max.maxGrade15.ToString();
                //if (max.maxGrade16 != null)
                //    maxS16.Text = max.maxGrade16.ToString();
                //if (max.maxGrade17 != null)
                //    maxS17.Text = max.maxGrade17.ToString();
                //if (max.maxGrade18 != null)
                //    maxS18.Text = max.maxGrade18.ToString();
                //if (max.maxGrade19 != null)
                //    maxS19.Text = max.maxGrade19.ToString();
                //if (max.maxGrade20 != null)
                //    maxS20.Text = max.maxGrade20.ToString();

                //if (max.maxCheewat1 != null)
                //    maxCW1.Text = max.maxCheewat1.ToString();
                //if (max.maxCheewat2 != null)
                //    maxCW2.Text = max.maxCheewat2.ToString();
                //if (max.maxCheewat3 != null)
                //    maxCW3.Text = max.maxCheewat3.ToString();
                //if (max.maxCheewat4 != null)
                //    maxCW4.Text = max.maxCheewat4.ToString();
                //if (max.maxCheewat5 != null)
                //    maxCW5.Text = max.maxCheewat5.ToString();
                //if (max.maxCheewat6 != null)
                //    maxCW6.Text = max.maxCheewat6.ToString();
                //if (max.maxCheewat7 != null)
                //    maxCW7.Text = max.maxCheewat7.ToString();
                //if (max.maxCheewat8 != null)
                //    maxCW8.Text = max.maxCheewat8.ToString();
                //if (max.maxCheewat9 != null)
                //    maxCW9.Text = max.maxCheewat9.ToString();
                //if (max.maxCheewat10 != null)
                //    maxCW10.Text = max.maxCheewat10.ToString();
                //if (max.maxCheewat11 != null)
                //    maxCW11.Text = max.maxCheewat11.ToString();
                //if (max.maxCheewat12 != null)
                //    maxCW12.Text = max.maxCheewat12.ToString();
                //if (max.maxCheewat13 != null)
                //    maxCW13.Text = max.maxCheewat13.ToString();
                //if (max.maxCheewat14 != null)
                //    maxCW14.Text = max.maxCheewat14.ToString();
                //if (max.maxCheewat15 != null)
                //    maxCW15.Text = max.maxCheewat15.ToString();
                //if (max.maxCheewat16 != null)
                //    maxCW16.Text = max.maxCheewat16.ToString();
                //if (max.maxCheewat17 != null)
                //    maxCW17.Text = max.maxCheewat17.ToString();
                //if (max.maxCheewat18 != null)
                //    maxCW18.Text = max.maxCheewat18.ToString();
                //if (max.maxCheewat19 != null)
                //    maxCW19.Text = max.maxCheewat19.ToString();
                //if (max.maxCheewat20 != null)
                //    maxCW20.Text = max.maxCheewat20.ToString();

                //if (max.maxBehavior1 != null)
                //    maxb1.Text = max.maxBehavior1.ToString();
                //if (max.maxBehavior2 != null)
                //    maxb2.Text = max.maxBehavior2.ToString();
                //if (max.maxBehavior3 != null)
                //    maxb3.Text = max.maxBehavior3.ToString();
                //if (max.maxBehavior4 != null)
                //    maxb4.Text = max.maxBehavior4.ToString();
                //if (max.maxBehavior5 != null)
                //    maxb5.Text = max.maxBehavior5.ToString();
                //if (max.maxBehavior6 != null)
                //    maxb6.Text = max.maxBehavior6.ToString();
                //if (max.maxBehavior7 != null)
                //    maxb7.Text = max.maxBehavior7.ToString();
                //if (max.maxBehavior8 != null)
                //    maxb8.Text = max.maxBehavior8.ToString();
                //if (max.maxBehavior9 != null)
                //    maxb9.Text = max.maxBehavior9.ToString();
                //if (max.maxBehavior10 != null)
                //    maxb10.Text = max.maxBehavior10.ToString();

                if (max.maxMidTerm != null)
                    maxSmid.Text = max.maxMidTerm.ToString();
                if (max.maxFinalTerm != null)
                    maxSlate.Text = max.maxFinalTerm.ToString();
            }

            List<TGradeDetail> q_gradedetail = new List<TGradeDetail>();
            var selfdata = _db.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == sPlaneID && w.nTermSubLevel2 == idlv2n).FirstOrDefault();
            if (selfdata != null)
            {
                //Binding Model Dialog Text Box value for Edit - This should be loaded from Assessment
                q_gradedetail = _db.TGradeDetails.Where(w => selfdata.nGradeId == w.nGradeId).ToList();
                //if (selfdata.nameGrade1 != null)
                //    setname1.Text = selfdata.nameGrade1;
                //if (selfdata.nameGrade2 != null)
                //    setname2.Text = selfdata.nameGrade2;
                //if (selfdata.nameGrade3 != null)
                //    setname3.Text = selfdata.nameGrade3;
                //if (selfdata.nameGrade4 != null)
                //    setname4.Text = selfdata.nameGrade4;
                //if (selfdata.nameGrade5 != null)
                //    setname5.Text = selfdata.nameGrade5;
                //if (selfdata.nameGrade6 != null)
                //    setname6.Text = selfdata.nameGrade6;
                //if (selfdata.nameGrade7 != null)
                //    setname7.Text = selfdata.nameGrade7;
                //if (selfdata.nameGrade8 != null)
                //    setname8.Text = selfdata.nameGrade8;
                //if (selfdata.nameGrade9 != null)
                //    setname9.Text = selfdata.nameGrade9;
                //if (selfdata.nameGrade10 != null)
                //    setname10.Text = selfdata.nameGrade10;
                //if (selfdata.nameGrade11 != null)
                //    setname11.Text = selfdata.nameGrade11;
                //if (selfdata.nameGrade12 != null)
                //    setname12.Text = selfdata.nameGrade12;
                //if (selfdata.nameGrade13 != null)
                //    setname13.Text = selfdata.nameGrade13;
                //if (selfdata.nameGrade14 != null)
                //    setname14.Text = selfdata.nameGrade14;
                //if (selfdata.nameGrade15 != null)
                //    setname15.Text = selfdata.nameGrade15;
                //if (selfdata.nameGrade16 != null)
                //    setname16.Text = selfdata.nameGrade16;
                //if (selfdata.nameGrade17 != null)
                //    setname17.Text = selfdata.nameGrade17;
                //if (selfdata.nameGrade18 != null)
                //    setname18.Text = selfdata.nameGrade18;
                //if (selfdata.nameGrade19 != null)
                //    setname19.Text = selfdata.nameGrade19;
                //if (selfdata.nameGrade20 != null)
                //    setname20.Text = selfdata.nameGrade20;

                //if (selfdata.nameCheewat1 != null)
                //    chewatname1.Text = selfdata.nameCheewat1;
                //if (selfdata.nameCheewat2 != null)
                //    chewatname2.Text = selfdata.nameCheewat2;
                //if (selfdata.nameCheewat3 != null)
                //    chewatname3.Text = selfdata.nameCheewat3;
                //if (selfdata.nameCheewat4 != null)
                //    chewatname4.Text = selfdata.nameCheewat4;
                //if (selfdata.nameCheewat5 != null)
                //    chewatname5.Text = selfdata.nameCheewat5;
                //if (selfdata.nameCheewat6 != null)
                //    chewatname6.Text = selfdata.nameCheewat6;
                //if (selfdata.nameCheewat7 != null)
                //    chewatname7.Text = selfdata.nameCheewat7;
                //if (selfdata.nameCheewat8 != null)
                //    chewatname8.Text = selfdata.nameCheewat8;
                //if (selfdata.nameCheewat9 != null)
                //    chewatname9.Text = selfdata.nameCheewat9;
                //if (selfdata.nameCheewat10 != null)
                //    chewatname10.Text = selfdata.nameCheewat10;
                //if (selfdata.nameCheewat11 != null)
                //    chewatname11.Text = selfdata.nameCheewat11;
                //if (selfdata.nameCheewat12 != null)
                //    chewatname12.Text = selfdata.nameCheewat12;
                //if (selfdata.nameCheewat13 != null)
                //    chewatname13.Text = selfdata.nameCheewat13;
                //if (selfdata.nameCheewat14 != null)
                //    chewatname14.Text = selfdata.nameCheewat14;
                //if (selfdata.nameCheewat15 != null)
                //    chewatname15.Text = selfdata.nameCheewat15;
                //if (selfdata.nameCheewat16 != null)
                //    chewatname16.Text = selfdata.nameCheewat16;
                //if (selfdata.nameCheewat17 != null)
                //    chewatname17.Text = selfdata.nameCheewat17;
                //if (selfdata.nameCheewat18 != null)
                //    chewatname18.Text = selfdata.nameCheewat18;
                //if (selfdata.nameCheewat19 != null)
                //    chewatname19.Text = selfdata.nameCheewat19;
                //if (selfdata.nameCheewat20 != null)
                //    chewatname20.Text = selfdata.nameCheewat20;

                //if (selfdata.nameMid1 != null)
                //    midmodalname1.Text = selfdata.nameMid1;
                //if (selfdata.nameMid2 != null)
                //    midmodalname2.Text = selfdata.nameMid2;
                //if (selfdata.nameMid3 != null)
                //    midmodalname3.Text = selfdata.nameMid3;
                //if (selfdata.nameMid4 != null)
                //    midmodalname4.Text = selfdata.nameMid4;
                //if (selfdata.nameMid5 != null)
                //    midmodalname5.Text = selfdata.nameMid5;
                //if (selfdata.nameMid6 != null)
                //    midmodalname6.Text = selfdata.nameMid6;
                //if (selfdata.nameMid7 != null)
                //    midmodalname7.Text = selfdata.nameMid7;
                //if (selfdata.nameMid8 != null)
                //    midmodalname8.Text = selfdata.nameMid8;
                //if (selfdata.nameMid9 != null)
                //    midmodalname9.Text = selfdata.nameMid9;
                //if (selfdata.nameMid10 != null)
                //    midmodalname10.Text = selfdata.nameMid10;

                //if (selfdata.nameFinal1 != null)
                //    finalmodalname1.Text = selfdata.nameFinal1;
                //if (selfdata.nameFinal2 != null)
                //    finalmodalname2.Text = selfdata.nameFinal2;
                //if (selfdata.nameFinal3 != null)
                //    finalmodalname3.Text = selfdata.nameFinal3;
                //if (selfdata.nameFinal4 != null)
                //    finalmodalname4.Text = selfdata.nameFinal4;
                //if (selfdata.nameFinal5 != null)
                //    finalmodalname5.Text = selfdata.nameFinal5;
                //if (selfdata.nameFinal6 != null)
                //    finalmodalname6.Text = selfdata.nameFinal6;
                //if (selfdata.nameFinal7 != null)
                //    finalmodalname7.Text = selfdata.nameFinal7;
                //if (selfdata.nameFinal8 != null)
                //    finalmodalname8.Text = selfdata.nameFinal8;
                //if (selfdata.nameFinal9 != null)
                //    finalmodalname9.Text = selfdata.nameFinal9;
                //if (selfdata.nameFinal10 != null)
                //    finalmodalname10.Text = selfdata.nameFinal10;

                //if (selfdata.maxMid1 != null)
                //    midmax1.Text = selfdata.maxMid1;
                //if (selfdata.maxMid2 != null)
                //    midmax2.Text = selfdata.maxMid2;
                //if (selfdata.maxMid3 != null)
                //    midmax3.Text = selfdata.maxMid3;
                //if (selfdata.maxMid4 != null)
                //    midmax4.Text = selfdata.maxMid4;
                //if (selfdata.maxMid5 != null)
                //    midmax5.Text = selfdata.maxMid5;
                //if (selfdata.maxMid6 != null)
                //    midmax6.Text = selfdata.maxMid6;
                //if (selfdata.maxMid7 != null)
                //    midmax7.Text = selfdata.maxMid7;
                //if (selfdata.maxMid8 != null)
                //    midmax8.Text = selfdata.maxMid8;
                //if (selfdata.maxMid9 != null)
                //    midmax9.Text = selfdata.maxMid9;
                //if (selfdata.maxMid10 != null)
                //    midmax10.Text = selfdata.maxMid10;

                //if (selfdata.maxFinal1 != null)
                //    finalmax1.Text = selfdata.maxFinal1;
                //if (selfdata.maxFinal2 != null)
                //    finalmax2.Text = selfdata.maxFinal2;
                //if (selfdata.maxFinal3 != null)
                //    finalmax3.Text = selfdata.maxFinal3;
                //if (selfdata.maxFinal4 != null)
                //    finalmax4.Text = selfdata.maxFinal4;
                //if (selfdata.maxFinal5 != null)
                //    finalmax5.Text = selfdata.maxFinal5;
                //if (selfdata.maxFinal6 != null)
                //    finalmax6.Text = selfdata.maxFinal6;
                //if (selfdata.maxFinal7 != null)
                //    finalmax7.Text = selfdata.maxFinal7;
                //if (selfdata.maxFinal8 != null)
                //    finalmax8.Text = selfdata.maxFinal8;
                //if (selfdata.maxFinal9 != null)
                //    finalmax9.Text = selfdata.maxFinal9;
                //if (selfdata.maxFinal10 != null)
                //    finalmax10.Text = selfdata.maxFinal10;

                //if (selfdata.nameBehavior1 != null)
                //    setnameb1.Text = selfdata.nameBehavior1;
                //if (selfdata.nameBehavior2 != null)
                //    setnameb2.Text = selfdata.nameBehavior2;
                //if (selfdata.nameBehavior3 != null)
                //    setnameb3.Text = selfdata.nameBehavior3;
                //if (selfdata.nameBehavior4 != null)
                //    setnameb4.Text = selfdata.nameBehavior4;
                //if (selfdata.nameBehavior5 != null)
                //    setnameb5.Text = selfdata.nameBehavior5;
                //if (selfdata.nameBehavior6 != null)
                //    setnameb6.Text = selfdata.nameBehavior6;
                //if (selfdata.nameBehavior7 != null)
                //    setnameb7.Text = selfdata.nameBehavior7;
                //if (selfdata.nameBehavior8 != null)
                //    setnameb8.Text = selfdata.nameBehavior8;
                //if (selfdata.nameBehavior9 != null)
                //    setnameb9.Text = selfdata.nameBehavior9;
                //if (selfdata.nameBehavior10 != null)
                //    setnameb10.Text = selfdata.nameBehavior10;

                //if (selfdata.maxGrade1 != null)
                //    maxS1.Text = selfdata.maxGrade1.ToString();
                //if (selfdata.maxGrade2 != null)
                //    maxS2.Text = selfdata.maxGrade2.ToString();
                //if (selfdata.maxGrade3 != null)
                //    maxS3.Text = selfdata.maxGrade3.ToString();
                //if (selfdata.maxGrade4 != null)
                //    maxS4.Text = selfdata.maxGrade4.ToString();
                //if (selfdata.maxGrade5 != null)
                //    maxS5.Text = selfdata.maxGrade5.ToString();
                //if (selfdata.maxGrade6 != null)
                //    maxS6.Text = selfdata.maxGrade6.ToString();
                //if (selfdata.maxGrade7 != null)
                //    maxS7.Text = selfdata.maxGrade7.ToString();
                //if (selfdata.maxGrade8 != null)
                //    maxS8.Text = selfdata.maxGrade8.ToString();
                //if (selfdata.maxGrade9 != null)
                //    maxS9.Text = selfdata.maxGrade9.ToString();
                //if (selfdata.maxGrade10 != null)
                //    maxS10.Text = selfdata.maxGrade10.ToString();
                //if (selfdata.maxGrade11 != null)
                //    maxS11.Text = selfdata.maxGrade11.ToString();
                //if (selfdata.maxGrade12 != null)
                //    maxS12.Text = selfdata.maxGrade12.ToString();
                //if (selfdata.maxGrade13 != null)
                //    maxS13.Text = selfdata.maxGrade13.ToString();
                //if (selfdata.maxGrade14 != null)
                //    maxS14.Text = selfdata.maxGrade14.ToString();
                //if (selfdata.maxGrade15 != null)
                //    maxS15.Text = selfdata.maxGrade15.ToString();
                //if (selfdata.maxGrade16 != null)
                //    maxS16.Text = selfdata.maxGrade16.ToString();
                //if (selfdata.maxGrade17 != null)
                //    maxS17.Text = selfdata.maxGrade17.ToString();
                //if (selfdata.maxGrade18 != null)
                //    maxS18.Text = selfdata.maxGrade18.ToString();
                //if (selfdata.maxGrade19 != null)
                //    maxS19.Text = selfdata.maxGrade19.ToString();
                //if (selfdata.maxGrade20 != null)
                //    maxS20.Text = selfdata.maxGrade20.ToString();

                //if (selfdata.maxCheewat1 != null)
                //    maxCW1.Text = selfdata.maxCheewat1.ToString();
                //if (selfdata.maxCheewat2 != null)
                //    maxCW2.Text = selfdata.maxCheewat2.ToString();
                //if (selfdata.maxCheewat3 != null)
                //    maxCW3.Text = selfdata.maxCheewat3.ToString();
                //if (selfdata.maxCheewat4 != null)
                //    maxCW4.Text = selfdata.maxCheewat4.ToString();
                //if (selfdata.maxCheewat5 != null)
                //    maxCW5.Text = selfdata.maxCheewat5.ToString();
                //if (selfdata.maxCheewat6 != null)
                //    maxCW6.Text = selfdata.maxCheewat6.ToString();
                //if (selfdata.maxCheewat7 != null)
                //    maxCW7.Text = selfdata.maxCheewat7.ToString();
                //if (selfdata.maxCheewat8 != null)
                //    maxCW8.Text = selfdata.maxCheewat8.ToString();
                //if (selfdata.maxCheewat9 != null)
                //    maxCW9.Text = selfdata.maxCheewat9.ToString();
                //if (selfdata.maxCheewat10 != null)
                //    maxCW10.Text = selfdata.maxCheewat10.ToString();
                //if (selfdata.maxCheewat11 != null)
                //    maxCW11.Text = selfdata.maxCheewat11.ToString();
                //if (selfdata.maxCheewat12 != null)
                //    maxCW12.Text = selfdata.maxCheewat12.ToString();
                //if (selfdata.maxCheewat13 != null)
                //    maxCW13.Text = selfdata.maxCheewat13.ToString();
                //if (selfdata.maxCheewat14 != null)
                //    maxCW14.Text = selfdata.maxCheewat14.ToString();
                //if (selfdata.maxCheewat15 != null)
                //    maxCW15.Text = selfdata.maxCheewat15.ToString();
                //if (selfdata.maxCheewat16 != null)
                //    maxCW16.Text = selfdata.maxCheewat16.ToString();
                //if (selfdata.maxCheewat17 != null)
                //    maxCW17.Text = selfdata.maxCheewat17.ToString();
                //if (selfdata.maxCheewat18 != null)
                //    maxCW18.Text = selfdata.maxCheewat18.ToString();
                //if (selfdata.maxCheewat19 != null)
                //    maxCW19.Text = selfdata.maxCheewat19.ToString();
                //if (selfdata.maxCheewat20 != null)
                //    maxCW20.Text = selfdata.maxCheewat20.ToString();

                //if (selfdata.maxBehavior1 != null)
                //    maxb1.Text = selfdata.maxBehavior1.ToString();
                //if (selfdata.maxBehavior2 != null)
                //    maxb2.Text = selfdata.maxBehavior2.ToString();
                //if (selfdata.maxBehavior3 != null)
                //    maxb3.Text = selfdata.maxBehavior3.ToString();
                //if (selfdata.maxBehavior4 != null)
                //    maxb4.Text = selfdata.maxBehavior4.ToString();
                //if (selfdata.maxBehavior5 != null)
                //    maxb5.Text = selfdata.maxBehavior5.ToString();
                //if (selfdata.maxBehavior6 != null)
                //    maxb6.Text = selfdata.maxBehavior6.ToString();
                //if (selfdata.maxBehavior7 != null)
                //    maxb7.Text = selfdata.maxBehavior7.ToString();
                //if (selfdata.maxBehavior8 != null)
                //    maxb8.Text = selfdata.maxBehavior8.ToString();
                //if (selfdata.maxBehavior9 != null)
                //    maxb9.Text = selfdata.maxBehavior9.ToString();
                //if (selfdata.maxBehavior10 != null)
                //    maxb10.Text = selfdata.maxBehavior10.ToString();

                if (selfdata.maxMidTerm != null)
                    maxSmid.Text = selfdata.maxMidTerm.ToString();
                if (selfdata.maxFinalTerm != null)
                    maxSlate.Text = selfdata.maxFinalTerm.ToString();
            }

            int sorttype1int = 0;
            int sorttype1txt = 0;
            int sorttype2 = 0;
            var q_usermaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();

            var lc_TUsers = _db.TUsers.Where(w => w.nTermSubLevel2 == idlv2n && ((w.cDel ?? "0") != "1")).ToList();
            var q_title = _db.TTitleLists.ToList();

            List<userlist> stdlist2 = new List<userlist>();
            userlist std2 = new userlist();

            int check = 0;

            foreach (var data in _db.TB_StudentViews.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && ((w.cDel ?? "0") != "1")))
            {
                std2 = new userlist();
                std2.sID = data.sID;
                stdlist2.Add(std2);
                check++;
            }

            if (check == 0)
            {
                foreach (var data in _db.TUsers.Where(w => w.nTermSubLevel2 == idlv2n && ((w.cDel ?? "0") != "1")))
                {
                    std2 = new userlist();
                    std2.sID = data.sID;
                    stdlist2.Add(std2);
                }
            }

            stdlist2 = stdlist2.Distinct().ToList();

            foreach (var data2 in stdlist2)
            {
                var data = _db.TUsers.Where(w => w.sID == data2.sID).FirstOrDefault();
                var TUserMaster = q_usermaster.FirstOrDefault(w => w.nSystemID == data.sID);
                if (TUserMaster != null)
                {
                    student = new studentlist2();
                    student.toggleOn = true;
                    student.studentstatus = data.nStudentStatus;
                    student.number = number.ToString();
                    student.sID = data.sID;
                    student.studentid = data.sStudentID;
                    int nTitleid;
                    int.TryParse(data.sStudentTitle, out nTitleid);
                    var f_title = q_title.FirstOrDefault(f => f.nTitleid == nTitleid || f.titleDescription == data.sStudentTitle);

                    if (mode == "EN")
                    {
                        if (data.sStudentNameEN != null && data.sStudentLastEN != null &&
                            data.sStudentNameEN != "" && data.sStudentLastEN != "")
                            student.sName = data.sStudentNameEN + " " + data.sStudentLastEN;
                        else
                        {
                            if (!string.IsNullOrEmpty(data.sStudentTitle))
                                student.sName = data.sName + " " + data.sLastname;
                            else
                            {
                                student.sName = (f_title == null ? "" : f_title.titleDescription) + data.sName + " " + data.sLastname;
                            }
                        }
                    }
                    else
                        student.sName = (f_title == null ? "" : f_title.titleDescription) + data.sName + " " + data.sLastname;

                    int n;
                    bool isNumeric = int.TryParse(data.sStudentID, out n);
                    if (isNumeric == true)
                    {
                        sorttype1int = sorttype1int + 1;
                        student.sort1int = Int32.Parse(data.sStudentID);
                        student.sort1txt = data.sStudentID;
                        student.sort2 = 0;
                    }
                    else if (data.sStudentID == null || data.sStudentID == "")
                    {
                        sorttype1int = sorttype1int + 1;
                        student.sort1int = 0;
                        student.sort1txt = "";
                        student.sort2 = 0;
                    }
                    else
                    {
                        sorttype1txt = sorttype1txt + 1;
                        student.sort1txt = data.sStudentID;
                        student.sort2 = 0;
                    }

                    if (data.nStudentNumber != null)
                    {
                        sorttype2 = sorttype2 + 1;
                        student.sort2 = data.nStudentNumber;
                    }


                    if (selfdata != null)
                    {


                        var data3 = q_gradedetail.FirstOrDefault(f => f.sID == data.sID);


                        if (data3 != null)
                        {
                            //if (data3.scoreGrade1 == "" && data3.scoreGrade2 == "" &&
                            //    data3.scoreGrade3 == "" && data3.scoreGrade4 == "" &&
                            //    data3.scoreCheewat1 == "" && data3.scoreCheewat2 == "" &&
                            //    data3.scoreCheewat3 == "" && data3.scoreCheewat4 == "" &&
                            //    data3.scoreMidTerm == "" && data3.scoreFinalTerm == "" &&
                            //    data3.getSpecial == "-1")
                            if (data3.scoreMidTerm == "" && data3.scoreFinalTerm == "" &&
                              data3.getSpecial == "-1")
                            {
                                var room3 = (from n2 in _db.TRoomChanges
                                             orderby n2.RoomChangeID descending
                                             where n2.sID == data.sID
                                             select n2).FirstOrDefault();
                                if (room3 != null)
                                {
                                    var newgrade = _db.TGrades.Where(w => w.sPlaneID.ToString() == sPlaneID && w.nTermSubLevel2 == room3.Level2Old && w.nTerm == nterm).FirstOrDefault();
                                    if (newgrade != null)
                                    {
                                        var newdetail = _db.TGradeDetails.Where(w => w.nGradeId == newgrade.nGradeId).FirstOrDefault();
                                        if (newdetail != null)
                                            data3 = newdetail;
                                    }

                                }
                            }

                            //Assign Student Score Value
                            //if (data3.scoreGrade1 != null && data3.scoreGrade1 != "")
                            //    student.txtg1 = data3.scoreGrade1.ToString();
                            //if (data3.scoreGrade2 != null && data3.scoreGrade2 != "")
                            //    student.txtg2 = data3.scoreGrade2.ToString();
                            //if (data3.scoreGrade3 != null && data3.scoreGrade3 != "")
                            //    student.txtg3 = data3.scoreGrade3.ToString();
                            //if (data3.scoreGrade4 != null && data3.scoreGrade4 != "")
                            //    student.txtg4 = data3.scoreGrade4.ToString();
                            //if (data3.scoreGrade5 != null && data3.scoreGrade5 != "")
                            //    student.txtg5 = data3.scoreGrade5.ToString();
                            //if (data3.scoreGrade6 != null && data3.scoreGrade6 != "")
                            //    student.txtg6 = data3.scoreGrade6.ToString();
                            //if (data3.scoreGrade7 != null && data3.scoreGrade7 != "")
                            //    student.txtg7 = data3.scoreGrade7.ToString();
                            //if (data3.scoreGrade8 != null && data3.scoreGrade8 != "")
                            //    student.txtg8 = data3.scoreGrade8.ToString();
                            //if (data3.scoreGrade9 != null && data3.scoreGrade9 != "")
                            //    student.txtg9 = data3.scoreGrade9.ToString();
                            //if (data3.scoreGrade10 != null && data3.scoreGrade10 != "")
                            //    student.txtg10 = data3.scoreGrade10.ToString();
                            //if (data3.scoreGrade11 != null && data3.scoreGrade11 != "")
                            //    student.txtg11 = data3.scoreGrade11.ToString();
                            //if (data3.scoreGrade12 != null && data3.scoreGrade12 != "")
                            //    student.txtg12 = data3.scoreGrade12.ToString();
                            //if (data3.scoreGrade13 != null && data3.scoreGrade13 != "")
                            //    student.txtg13 = data3.scoreGrade13.ToString();
                            //if (data3.scoreGrade14 != null && data3.scoreGrade14 != "")
                            //    student.txtg14 = data3.scoreGrade14.ToString();
                            //if (data3.scoreGrade15 != null && data3.scoreGrade15 != "")
                            //    student.txtg15 = data3.scoreGrade15.ToString();
                            //if (data3.scoreGrade16 != null && data3.scoreGrade16 != "")
                            //    student.txtg16 = data3.scoreGrade16.ToString();
                            //if (data3.scoreGrade17 != null && data3.scoreGrade17 != "")
                            //    student.txtg17 = data3.scoreGrade17.ToString();
                            //if (data3.scoreGrade18 != null && data3.scoreGrade18 != "")
                            //    student.txtg18 = data3.scoreGrade18.ToString();
                            //if (data3.scoreGrade19 != null && data3.scoreGrade19 != "")
                            //    student.txtg19 = data3.scoreGrade19.ToString();
                            //if (data3.scoreGrade20 != null && data3.scoreGrade20 != "")
                            //    student.txtg20 = data3.scoreGrade20.ToString();

                            //if(data3.scoreMid1 != null && data3.scoreMid1 != "")
                            //    student.txtmid1 = data3.scoreMid1.ToString();
                            //if (data3.scoreMid2 != null && data3.scoreMid2 != "")
                            //    student.txtmid2 = data3.scoreMid2.ToString();
                            //if (data3.scoreMid3 != null && data3.scoreMid3 != "")
                            //    student.txtmid3 = data3.scoreMid3.ToString();
                            //if (data3.scoreMid4 != null && data3.scoreMid4 != "")
                            //    student.txtmid4 = data3.scoreMid4.ToString();
                            //if (data3.scoreMid5 != null && data3.scoreMid5 != "")
                            //    student.txtmid5 = data3.scoreMid5.ToString();
                            //if (data3.scoreMid6 != null && data3.scoreMid6 != "")
                            //    student.txtmid6 = data3.scoreMid6.ToString();
                            //if (data3.scoreMid7 != null && data3.scoreMid7 != "")
                            //    student.txtmid7 = data3.scoreMid7.ToString();
                            //if (data3.scoreMid8 != null && data3.scoreMid8 != "")
                            //    student.txtmid8 = data3.scoreMid8.ToString();
                            //if (data3.scoreMid9 != null && data3.scoreMid9 != "")
                            //    student.txtmid9 = data3.scoreMid9.ToString();
                            //if (data3.scoreMid10 != null && data3.scoreMid10 != "")
                            //    student.txtmid10 = data3.scoreMid10.ToString();

                            //if (data3.scoreFinal1 != null && data3.scoreFinal1 != "")
                            //    student.txtfinal1 = data3.scoreFinal1.ToString();
                            //if (data3.scoreFinal2 != null && data3.scoreFinal2 != "")
                            //    student.txtfinal2 = data3.scoreFinal2.ToString();
                            //if (data3.scoreFinal3 != null && data3.scoreFinal3 != "")
                            //    student.txtfinal3 = data3.scoreFinal3.ToString();
                            //if (data3.scoreFinal4 != null && data3.scoreFinal4 != "")
                            //    student.txtfinal4 = data3.scoreFinal4.ToString();
                            //if (data3.scoreFinal5 != null && data3.scoreFinal5 != "")
                            //    student.txtfinal5 = data3.scoreFinal5.ToString();
                            //if (data3.scoreFinal6 != null && data3.scoreFinal6 != "")
                            //    student.txtfinal6 = data3.scoreFinal6.ToString();
                            //if (data3.scoreFinal7 != null && data3.scoreFinal7 != "")
                            //    student.txtfinal7 = data3.scoreFinal7.ToString();
                            //if (data3.scoreFinal8 != null && data3.scoreFinal8 != "")
                            //    student.txtfinal8 = data3.scoreFinal8.ToString();
                            //if (data3.scoreFinal9 != null && data3.scoreFinal9 != "")
                            //    student.txtfinal9 = data3.scoreFinal9.ToString();
                            //if (data3.scoreFinal10 != null && data3.scoreFinal10 != "")
                            //    student.txtfinal10 = data3.scoreFinal10.ToString();

                            //if (data3.scoreCheewat1 != null && data3.scoreCheewat1 != "")
                            //    student.txtchewut1 = data3.scoreCheewat1.ToString();
                            //if (data3.scoreCheewat2 != null && data3.scoreCheewat2 != "")
                            //    student.txtchewut2 = data3.scoreCheewat2.ToString();
                            //if (data3.scoreCheewat3 != null && data3.scoreCheewat3 != "")
                            //    student.txtchewut3 = data3.scoreCheewat3.ToString();
                            //if (data3.scoreCheewat4 != null && data3.scoreCheewat4 != "")
                            //    student.txtchewut4 = data3.scoreCheewat4.ToString();
                            //if (data3.scoreCheewat5 != null && data3.scoreCheewat5 != "")
                            //    student.txtchewut5 = data3.scoreCheewat5.ToString();
                            //if (data3.scoreCheewat6 != null && data3.scoreCheewat6 != "")
                            //    student.txtchewut6 = data3.scoreCheewat6.ToString();
                            //if (data3.scoreCheewat7 != null && data3.scoreCheewat7 != "")
                            //    student.txtchewut7 = data3.scoreCheewat7.ToString();
                            //if (data3.scoreCheewat8 != null && data3.scoreCheewat8 != "")
                            //    student.txtchewut8 = data3.scoreCheewat8.ToString();
                            //if (data3.scoreCheewat9 != null && data3.scoreCheewat9 != "")
                            //    student.txtchewut9 = data3.scoreCheewat9.ToString();
                            //if (data3.scoreCheewat10 != null && data3.scoreCheewat10 != "")
                            //    student.txtchewut10 = data3.scoreCheewat10.ToString();
                            //if (data3.scoreCheewat11 != null && data3.scoreCheewat11 != "")
                            //    student.txtchewut11 = data3.scoreCheewat11.ToString();
                            //if (data3.scoreCheewat12 != null && data3.scoreCheewat12 != "")
                            //    student.txtchewut12 = data3.scoreCheewat12.ToString();
                            //if (data3.scoreCheewat13 != null && data3.scoreCheewat13 != "")
                            //    student.txtchewut13 = data3.scoreCheewat13.ToString();
                            //if (data3.scoreCheewat14 != null && data3.scoreCheewat14 != "")
                            //    student.txtchewut14 = data3.scoreCheewat14.ToString();
                            //if (data3.scoreCheewat15 != null && data3.scoreCheewat15 != "")
                            //    student.txtchewut15 = data3.scoreCheewat15.ToString();
                            //if (data3.scoreCheewat16 != null && data3.scoreCheewat16 != "")
                            //    student.txtchewut16 = data3.scoreCheewat16.ToString();
                            //if (data3.scoreCheewat17 != null && data3.scoreCheewat17 != "")
                            //    student.txtchewut17 = data3.scoreCheewat17.ToString();
                            //if (data3.scoreCheewat18 != null && data3.scoreCheewat18 != "")
                            //    student.txtchewut18 = data3.scoreCheewat18.ToString();
                            //if (data3.scoreCheewat19 != null && data3.scoreCheewat19 != "")
                            //    student.txtchewut19 = data3.scoreCheewat19.ToString();
                            //if (data3.scoreCheewat20 != null && data3.scoreCheewat20 != "")
                            //    student.txtchewut20 = data3.scoreCheewat20.ToString();

                            if (data3.scoreMidTerm != null && data3.scoreMidTerm != "")
                                student.txtgmid = data3.scoreMidTerm.ToString();
                            if (data3.scoreFinalTerm != null && data3.scoreFinalTerm != "")
                                student.txtglate = data3.scoreFinalTerm.ToString();
                            if (data3.getBehaviorLabel != null && data3.getBehaviorLabel != "")
                                student.txtGoodBehavior = data3.getBehaviorLabel.ToString();
                            if (data3.getReadWrite != null && data3.getReadWrite != "")
                                student.txtGoodReading = data3.getReadWrite.ToString();
                            if (data3.getSamattana != null && data3.getSamattana != "")
                                student.txtSamattana = data3.getSamattana.ToString();
                            if (data3.getSpecial != null && data3.getSpecial != "")
                                student.txtSpecial = data3.getSpecial.ToString();

                            //if (data3.scoreBehavior1 != null && data3.scoreBehavior1 != "")
                            //    student.txtb1 = data3.scoreBehavior1.ToString();
                            //if (data3.scoreBehavior2 != null && data3.scoreBehavior2 != "")
                            //    student.txtb2 = data3.scoreBehavior2.ToString();
                            //if (data3.scoreBehavior3 != null && data3.scoreBehavior3 != "")
                            //    student.txtb3 = data3.scoreBehavior3.ToString();
                            //if (data3.scoreBehavior4 != null && data3.scoreBehavior4 != "")
                            //    student.txtb4 = data3.scoreBehavior4.ToString();
                            //if (data3.scoreBehavior5 != null && data3.scoreBehavior5 != "")
                            //    student.txtb5 = data3.scoreBehavior5.ToString();
                            //if (data3.scoreBehavior6 != null && data3.scoreBehavior6 != "")
                            //    student.txtb6 = data3.scoreBehavior6.ToString();
                            //if (data3.scoreBehavior7 != null && data3.scoreBehavior7 != "")
                            //    student.txtb7 = data3.scoreBehavior7.ToString();
                            //if (data3.scoreBehavior8 != null && data3.scoreBehavior8 != "")
                            //    student.txtb8 = data3.scoreBehavior8.ToString();
                            //if (data3.scoreBehavior9 != null && data3.scoreBehavior9 != "")
                            //    student.txtb9 = data3.scoreBehavior9.ToString();
                            //if (data3.scoreBehavior10 != null && data3.scoreBehavior10 != "")
                            //    student.txtb10 = data3.scoreBehavior10.ToString();
                        }
                    }

                    number = number + 1;
                    studentlist.Add(student);
                }
            }

            var newSortList = studentlist;

            newSortList = studentlist.OrderBy(x => x.sort1int).ToList();

            if (sorttype1txt != 0)
                newSortList = studentlist.OrderBy(x => x.sort1txt).ToList();

            if (sorttype2 != 0)
                newSortList = studentlist.OrderBy(x => x.sort2).ThenBy(t => t.sort1txt).ToList();

            int counter = 1;


            foreach (var a in newSortList)
            {
                a.number = counter.ToString();
                counter = counter + 1;
            }

            shareheader.Attributes["class"] = "col-xs-12 classchoose2 hidden";
            foreach (var data1 in _db.TGradeShareInfoes.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == sPlaneID && w.from_nTSubLevel2 == idlv2n && w.cDel == null))
            {
                editmulticlass.Text = editmulticlass.Text + data1.to_nTSubLevel2 + "/";
                shareheader.Attributes["class"] = "col-xs-12 classchoose2";
            }


            var data4 = _db.TGradeShareInfoes.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == sPlaneID && w.to_nTSubLevel2 == idlv2n && w.cDel == null).FirstOrDefault();
            if (data4 != null)
            {
                Label3.Text = "ข้อมูลนำเข้ามาจากห้อง";
                var room2 = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == data4.from_nTSubLevel2).FirstOrDefault();
                var sub2 = _db.TSubLevels.Where(w => w.nTSubLevel == room2.nTSubLevel).FirstOrDefault();

                Label4.Text = sub2.SubLevel + " / " + room2.nTSubLevel2;

                check8.Checked = true;
                check8.Attributes["disabled"] = "";

                maxS1.Attributes["disabled"] = "";
                maxS2.Attributes["disabled"] = "";
                maxS3.Attributes["disabled"] = "";
                maxS4.Attributes["disabled"] = "";
                maxS5.Attributes["disabled"] = "";
                maxS6.Attributes["disabled"] = "";
                maxS7.Attributes["disabled"] = "";
                maxS8.Attributes["disabled"] = "";
                maxS9.Attributes["disabled"] = "";
                maxS10.Attributes["disabled"] = "";
                maxS11.Attributes["disabled"] = "";
                maxS12.Attributes["disabled"] = "";
                maxS13.Attributes["disabled"] = "";
                maxS14.Attributes["disabled"] = "";
                maxS15.Attributes["disabled"] = "";
                maxS16.Attributes["disabled"] = "";
                maxS17.Attributes["disabled"] = "";
                maxS18.Attributes["disabled"] = "";
                maxS19.Attributes["disabled"] = "";
                maxS20.Attributes["disabled"] = "";
                maxCW1.Attributes["disabled"] = "";
                maxCW2.Attributes["disabled"] = "";
                maxCW3.Attributes["disabled"] = "";
                maxCW4.Attributes["disabled"] = "";
                maxCW5.Attributes["disabled"] = "";
                maxCW6.Attributes["disabled"] = "";
                maxCW7.Attributes["disabled"] = "";
                maxCW8.Attributes["disabled"] = "";
                maxCW9.Attributes["disabled"] = "";
                maxCW10.Attributes["disabled"] = "";
                maxCW11.Attributes["disabled"] = "";
                maxCW12.Attributes["disabled"] = "";
                maxCW13.Attributes["disabled"] = "";
                maxCW14.Attributes["disabled"] = "";
                maxCW15.Attributes["disabled"] = "";
                maxCW16.Attributes["disabled"] = "";
                maxCW17.Attributes["disabled"] = "";
                maxCW18.Attributes["disabled"] = "";
                maxCW19.Attributes["disabled"] = "";
                maxCW20.Attributes["disabled"] = "";
                midmax1.Attributes["disabled"] = "";
                midmax2.Attributes["disabled"] = "";
                midmax3.Attributes["disabled"] = "";
                midmax4.Attributes["disabled"] = "";
                midmax5.Attributes["disabled"] = "";
                midmax6.Attributes["disabled"] = "";
                midmax7.Attributes["disabled"] = "";
                midmax8.Attributes["disabled"] = "";
                midmax9.Attributes["disabled"] = "";
                midmax10.Attributes["disabled"] = "";
                finalmax1.Attributes["disabled"] = "";
                finalmax2.Attributes["disabled"] = "";
                finalmax3.Attributes["disabled"] = "";
                finalmax4.Attributes["disabled"] = "";
                finalmax5.Attributes["disabled"] = "";
                finalmax6.Attributes["disabled"] = "";
                finalmax7.Attributes["disabled"] = "";
                finalmax8.Attributes["disabled"] = "";
                finalmax9.Attributes["disabled"] = "";
                finalmax10.Attributes["disabled"] = "";
                maxb1.Attributes["disabled"] = "";
                maxb2.Attributes["disabled"] = "";
                maxb3.Attributes["disabled"] = "";
                maxb4.Attributes["disabled"] = "";
                maxb5.Attributes["disabled"] = "";
                maxb6.Attributes["disabled"] = "";
                maxb7.Attributes["disabled"] = "";
                maxb8.Attributes["disabled"] = "";
                maxb9.Attributes["disabled"] = "";
                maxb10.Attributes["disabled"] = "";
                maxSmid.Attributes["disabled"] = "";
                maxSlate.Attributes["disabled"] = "";

                var data3 = _db.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == sPlaneID && w.nTermSubLevel2 == data4.from_nTSubLevel2).FirstOrDefault();

                //Assign The Assessment Max Score
                //maxS1.Text = data3.maxGrade1;
                //maxS2.Text = data3.maxGrade2;
                //maxS3.Text = data3.maxGrade3;
                //maxS4.Text = data3.maxGrade4;
                //maxS5.Text = data3.maxGrade5;
                //maxS6.Text = data3.maxGrade6;
                //maxS7.Text = data3.maxGrade7;
                //maxS8.Text = data3.maxGrade8;
                //maxS9.Text = data3.maxGrade9;
                //maxS10.Text = data3.maxGrade10;
                //maxS11.Text = data3.maxGrade11;
                //maxS12.Text = data3.maxGrade12;
                //maxS13.Text = data3.maxGrade13;
                //maxS14.Text = data3.maxGrade14;
                //maxS15.Text = data3.maxGrade15;
                //maxS16.Text = data3.maxGrade16;
                //maxS17.Text = data3.maxGrade17;
                //maxS18.Text = data3.maxGrade18;
                //maxS19.Text = data3.maxGrade19;
                //maxS20.Text = data3.maxGrade20;
                //maxCW1.Text = data3.maxCheewat1;
                //maxCW2.Text = data3.maxCheewat2;
                //maxCW3.Text = data3.maxCheewat3;
                //maxCW4.Text = data3.maxCheewat4;
                //maxCW5.Text = data3.maxCheewat5;
                //maxCW6.Text = data3.maxCheewat6;
                //maxCW7.Text = data3.maxCheewat7;
                //maxCW8.Text = data3.maxCheewat8;
                //maxCW9.Text = data3.maxCheewat9;
                //maxCW10.Text = data3.maxCheewat10;
                //maxCW11.Text = data3.maxCheewat11;
                //maxCW12.Text = data3.maxCheewat12;
                //maxCW13.Text = data3.maxCheewat13;
                //maxCW14.Text = data3.maxCheewat14;
                //maxCW15.Text = data3.maxCheewat15;
                //maxCW16.Text = data3.maxCheewat16;
                //maxCW17.Text = data3.maxCheewat17;
                //maxCW18.Text = data3.maxCheewat18;
                //maxCW19.Text = data3.maxCheewat19;
                //maxCW20.Text = data3.maxCheewat20;
                //midmax1.Text = data3.maxMid1;
                //midmax2.Text = data3.maxMid2;
                //midmax3.Text = data3.maxMid3;
                //midmax4.Text = data3.maxMid4;
                //midmax5.Text = data3.maxMid5;
                //midmax6.Text = data3.maxMid6;
                //midmax7.Text = data3.maxMid7;
                //midmax8.Text = data3.maxMid8;
                //midmax9.Text = data3.maxMid9;
                //midmax10.Text = data3.maxMid10;
                //finalmax1.Text = data3.maxFinal1;
                //finalmax2.Text = data3.maxFinal2;
                //finalmax3.Text = data3.maxFinal3;
                //finalmax4.Text = data3.maxFinal4;
                //finalmax5.Text = data3.maxFinal5;
                //finalmax6.Text = data3.maxFinal6;
                //finalmax7.Text = data3.maxFinal7;
                //finalmax8.Text = data3.maxFinal8;
                //finalmax9.Text = data3.maxFinal9;
                //finalmax10.Text = data3.maxFinal10;
                //maxb1.Text = data3.maxBehavior1;
                //maxb2.Text = data3.maxBehavior2;
                //maxb3.Text = data3.maxBehavior3;
                //maxb4.Text = data3.maxBehavior4;
                //maxb5.Text = data3.maxBehavior5;
                //maxb6.Text = data3.maxBehavior6;
                //maxb7.Text = data3.maxBehavior7;
                //maxb8.Text = data3.maxBehavior8;
                //maxb9.Text = data3.maxBehavior9;
                //maxb10.Text = data3.maxBehavior10;
                maxSmid.Text = data3.maxMidTerm;
                maxSlate.Text = data3.maxFinalTerm;



                setup3.Text = data3.GradeAddBehavior;
                setup4.Text = data3.GradeAutoBehaviorScore;
                setup5.Text = data3.GradeAddCheewat;
                setup11.Text = data3.optionMid;
                setup12.Text = data3.optionFinal;

                if (data3.GradeAddBehavior == "1")
                    check4.Checked = true;
                if (data3.GradeAutoBehaviorScore == "1")
                    check5.Checked = true;
                if (data3.GradeAddCheewat == "1")
                    check6.Checked = true;
                if (data3.optionMid == "1")
                    check11.Checked = true;
                if (data3.optionFinal == "1")
                    check12.Checked = true;

                check4.Attributes["disabled"] = "";
                check5.Attributes["disabled"] = "";
                check6.Attributes["disabled"] = "";
                check11.Attributes["disabled"] = "";
                check12.Attributes["disabled"] = "";

                //setname1.Text = data3.nameGrade1;
                //setname2.Text = data3.nameGrade2;
                //setname3.Text = data3.nameGrade3;
                //setname4.Text = data3.nameGrade4;
                //setname5.Text = data3.nameGrade5;
                //setname6.Text = data3.nameGrade6;
                //setname7.Text = data3.nameGrade7;
                //setname8.Text = data3.nameGrade8;
                //setname9.Text = data3.nameGrade9;
                //setname10.Text = data3.nameGrade10;
                //setname11.Text = data3.nameGrade11;
                //setname12.Text = data3.nameGrade12;
                //setname13.Text = data3.nameGrade13;
                //setname14.Text = data3.nameGrade14;
                //setname15.Text = data3.nameGrade15;
                //setname16.Text = data3.nameGrade16;
                //setname17.Text = data3.nameGrade17;
                //setname18.Text = data3.nameGrade18;
                //setname19.Text = data3.nameGrade19;
                //setname20.Text = data3.nameGrade20;

                //chewatname1.Text = data3.nameCheewat1;
                //chewatname2.Text = data3.nameCheewat2;
                //chewatname3.Text = data3.nameCheewat3;
                //chewatname4.Text = data3.nameCheewat4;
                //chewatname5.Text = data3.nameCheewat5;
                //chewatname6.Text = data3.nameCheewat6;
                //chewatname7.Text = data3.nameCheewat7;
                //chewatname8.Text = data3.nameCheewat8;
                //chewatname9.Text = data3.nameCheewat9;
                //chewatname10.Text = data3.nameCheewat10;
                //chewatname11.Text = data3.nameCheewat11;
                //chewatname12.Text = data3.nameCheewat12;
                //chewatname13.Text = data3.nameCheewat13;
                //chewatname14.Text = data3.nameCheewat14;
                //chewatname15.Text = data3.nameCheewat15;
                //chewatname16.Text = data3.nameCheewat16;
                //chewatname17.Text = data3.nameCheewat17;
                //chewatname18.Text = data3.nameCheewat18;
                //chewatname19.Text = data3.nameCheewat19;
                //chewatname20.Text = data3.nameCheewat20;

                //midmodalname1.Text = data3.nameMid1;
                //midmodalname2.Text = data3.nameMid2;
                //midmodalname3.Text = data3.nameMid3;
                //midmodalname4.Text = data3.nameMid4;
                //midmodalname5.Text = data3.nameMid5;
                //midmodalname6.Text = data3.nameMid6;
                //midmodalname7.Text = data3.nameMid7;
                //midmodalname8.Text = data3.nameMid8;
                //midmodalname9.Text = data3.nameMid9;
                //midmodalname10.Text = data3.nameMid10;

                //finalmodalname1.Text = data3.nameFinal1;
                //finalmodalname2.Text = data3.nameFinal2;
                //finalmodalname3.Text = data3.nameFinal3;
                //finalmodalname4.Text = data3.nameFinal4;
                //finalmodalname5.Text = data3.nameFinal5;
                //finalmodalname6.Text = data3.nameFinal6;
                //finalmodalname7.Text = data3.nameFinal7;
                //finalmodalname8.Text = data3.nameFinal8;
                //finalmodalname9.Text = data3.nameFinal9;
                //finalmodalname10.Text = data3.nameFinal10;

                //setnameb1.Text = data3.nameBehavior1;
                //setnameb2.Text = data3.nameBehavior2;
                //setnameb3.Text = data3.nameBehavior3;
                //setnameb4.Text = data3.nameBehavior4;
                //setnameb5.Text = data3.nameBehavior5;
                //setnameb6.Text = data3.nameBehavior6;
                //setnameb7.Text = data3.nameBehavior7;
                //setnameb8.Text = data3.nameBehavior8;
                //setnameb9.Text = data3.nameBehavior9;
                //setnameb10.Text = data3.nameBehavior10;
            }

            //Sharing the same assessment to another class
            foreach (var a in _db.TSubLevels.Where(w => w.nTSubLevel == idlvn && w.nWorkingStatus == 1))
            {
                foreach (var b in _db.TTermSubLevel2.Where(w => w.nTSubLevel == a.nTSubLevel && w.nTermSubLevel2 != idlv2n))
                {
                    var check2 = _db.TGradeShareInfoes.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == sPlaneID && w.from_nTSubLevel2 == b.nTermSubLevel2 && w.cDel == null).FirstOrDefault();
                    var check1 = _db.TGradeShareInfoes.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == sPlaneID && w.to_nTSubLevel2 == b.nTermSubLevel2 && w.cDel == null).FirstOrDefault();
                    if (check1 == null && check2 == null)
                    {
                        var item = new ListItem
                        {
                            Text = a.SubLevel + "/" + b.nTSubLevel2,
                            Value = b.nTermSubLevel2.ToString()
                        };
                        modalClass2.Items.Add(item);
                    }
                    else if (check1 != null && check2 == null)
                    {
                        if (check1.from_nTSubLevel2 == idlv2n)
                        {
                            var item = new ListItem
                            {
                                Text = a.SubLevel + "/" + b.nTSubLevel2,
                                Value = b.nTermSubLevel2.ToString()
                            };
                            modalClass2.Items.Add(item);
                        }
                    }

                }
            }

            dgd.DataSource = newSortList;
            dgd.Columns[5].ItemStyle.Width = 5;
            dgd.PageSize = 999;
            dgd.DataBind();

            dgdp2.DataSource = newSortList;
            dgdp2.Columns[5].ItemStyle.Width = 5;
            dgdp2.PageSize = 999;
            dgdp2.DataBind();

            dgdp3.DataSource = newSortList;
            dgdp3.Columns[5].ItemStyle.Width = 5;
            dgdp3.PageSize = 999;
            dgdp3.DataBind();

            dgdp4.DataSource = newSortList;
            dgdp4.Columns[5].ItemStyle.Width = 5;
            dgdp4.PageSize = 999;
            dgdp4.DataBind();

            dgd2.DataSource = newSortList;
            dgd2.PageSize = 999;
            dgd2.DataBind();

            dgd3.DataSource = newSortList;
            dgd3.PageSize = 999;
            dgd3.DataBind();

            dgdp5.DataSource = newSortList;
            dgdp5.PageSize = 999;
            dgdp5.DataBind();

            dgdp6.DataSource = newSortList;
            dgdp6.PageSize = 999;
            dgdp6.DataBind();

            dgdp7.DataSource = newSortList;
            dgdp7.PageSize = 999;
            dgdp7.DataBind();

            dgdp8.DataSource = newSortList;
            dgdp8.PageSize = 999;
            dgdp8.DataBind();

            dgdp9.DataSource = newSortList;
            dgdp9.PageSize = 999;
            dgdp9.DataBind();

            dgdp10.DataSource = newSortList;
            dgdp10.PageSize = 999;
            dgdp10.DataBind();

            dgdmidterm1.DataSource = newSortList;
            dgdmidterm1.PageSize = 999;
            dgdmidterm1.DataBind();

            dgdmidterm2.DataSource = newSortList;
            dgdmidterm2.PageSize = 999;
            dgdmidterm2.DataBind();

            dgdfinalterm1.DataSource = newSortList;
            dgdfinalterm1.PageSize = 999;
            dgdfinalterm1.DataBind();

            dgdfinalterm2.DataSource = newSortList;
            dgdfinalterm2.PageSize = 999;
            dgdfinalterm2.DataBind();


        }






        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        string datetimetext(DateTime? original)
        {


            string txt1 = original.Value.Day.ToString();
            string txt2 = original.Value.Month.ToString();
            string txt3 = original.Value.Year.ToString();

            if (txt2 == "1" || txt2 == "01")
                txt2 = "ม.ค.";
            else if (txt2 == "2" || txt2 == "02")
                txt2 = "ก.พ.";
            else if (txt2 == "3" || txt2 == "03")
                txt2 = "มี.ค.";
            else if (txt2 == "4" || txt2 == "04")
                txt2 = "เม.ย.";
            else if (txt2 == "5" || txt2 == "05")
                txt2 = "พ.ค.";
            else if (txt2 == "6" || txt2 == "06")
                txt2 = "มิ.ย.";
            else if (txt2 == "7" || txt2 == "07")
                txt2 = "ก.ค.";
            else if (txt2 == "8" || txt2 == "08")
                txt2 = "ส.ค.";
            else if (txt2 == "9" || txt2 == "09")
                txt2 = "ก.ย.";
            else if (txt2 == "10")
                txt2 = "ต.ค.";
            else if (txt2 == "11")
                txt2 = "พ.ย.";
            else if (txt2 == "12")
                txt2 = "ธ.ค.";

            int year = int.Parse(txt3);
            if (year < 2500)
                year = year + 543;


            return txt1 + " " + txt2 + " " + year.ToString();
        }

        public string comeorskip(DateTime? day, int check, int userid, List<TLogLearnTimeScan> loglist, List<TLeaveLetter> leavelist, List<THoliday> holiday)
        {

            string come = "0";

            var d1 = loglist.Where(w => w.LogLearnDate == day && w.sID == userid && w.sScheduleID == check).FirstOrDefault();
            if (d1 != null)
            {
                if (d1.LogLearnScanStatus.Trim() == "1" || d1.LogLearnScanStatus.Trim() == "0")
                    come = "1";
                else if (d1.LogLearnScanStatus.Trim() == "3") //ขาด
                {
                    come = "0";
                    var d2 = leavelist.Where(w => w.startDate <= day && w.endDate >= day && w.writerId == userid).FirstOrDefault();
                    if (d2 != null)
                    {
                        if (d2.LetterConfirmdate != null)
                        {
                            if (d2.adminOneComfirm != "0" && d2.adminTwoComfirm != "0" && d2.adminThreeComfirm != "0")
                            {
                                if (d2.letterType == "0")
                                    come = "3";
                                else come = "4";
                            }
                        }

                    }
                }
                else if (d1.LogLearnScanStatus.Trim() == "4") //ลากิจ
                    come = "4";
                else if (d1.LogLearnScanStatus.Trim() == "5") //ลาป่วย
                    come = "3";
                else if (d1.LogLearnScanStatus.Trim() == "6") //กิจกรรม
                    come = "5";
            }

            foreach (var holi in holiday)
            {
                if (holi.sHolidayAll == "1")
                {
                    if (day <= holi.dHolidayEnd && day >= holi.dHolidayStart)
                    {
                        if (come == "7")
                            come = "9";
                        else come = "8";
                    }
                }
                else
                {
                    JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
                    var userroom = _db.TUsers.Where(w => w.sID == userid).FirstOrDefault();
                    foreach (var some in _db.THolidaySomes.Where(w => w.nHoliday == holi.nHoliday && w.Deleted == null))
                    {
                        if (some.nTSubLevel == userroom.nTermSubLevel2)
                        {
                            if (come == "7")
                                come = "9";
                            else come = "8";
                        }
                    }
                }

            }

            return come;
        }

        public string quitCheck(DateTime? day, string oldvalue, DateTime? quitDay, int wordcheck, int? nStudentStatus)
        {

            string come = oldvalue;

            if (nStudentStatus != null)
            {
                if (nStudentStatus == 2)
                {
                    if (quitDay != null)
                    {
                        if (day >= quitDay)
                        {
                            if (wordcheck == 0)
                                come = "A";
                            else if (wordcheck == 1)
                                come = "B";
                            else if (wordcheck == 2)
                                come = "C";
                            else if (wordcheck == 3)
                                come = "D";
                            else if (wordcheck == 4)
                                come = "E";
                            else come = "Q";
                        }

                    }
                }
            }



            return come;
        }

        public int wordCheck(int word, string student)
        {

            if (student == "A" || student == "B" || student == "C" || student == "D" || student == "E")
                word = word + 1;


            return word;
        }
        class userlist
        {
            public int sID { get; set; }
        }
        class studentlist2
        {
            public int? sID { get; set; }
            public string sName { get; set; }
            public string note { get; set; }
            public bool toggleOn { get; set; }
            public string txtg1 { get; set; }
            public string txtg2 { get; set; }
            public string txtg3 { get; set; }
            public string txtg4 { get; set; }
            public string txtg5 { get; set; }
            public string txtg6 { get; set; }
            public string txtg7 { get; set; }
            public string txtg8 { get; set; }
            public string txtg9 { get; set; }
            public string txtg10 { get; set; }
            public string txtg11 { get; set; }
            public string txtg12 { get; set; }
            public string txtg13 { get; set; }
            public string txtg14 { get; set; }
            public string txtg15 { get; set; }
            public string txtg16 { get; set; }
            public string txtg17 { get; set; }
            public string txtg18 { get; set; }
            public string txtg19 { get; set; }
            public string txtg20 { get; set; }
            public string txtgmid { get; set; }
            public string txtglate { get; set; }
            public string number { get; set; }
            public string txtGoodBehavior { get; set; }
            public string txtGoodReading { get; set; }
            public string txtSamattana { get; set; }
            public string txtSpecial { get; set; }
            public string txtb1 { get; set; }
            public string txtb2 { get; set; }
            public string txtb3 { get; set; }
            public string txtb4 { get; set; }
            public string txtb5 { get; set; }
            public string txtb6 { get; set; }
            public string txtb7 { get; set; }
            public string txtb8 { get; set; }
            public string txtb9 { get; set; }
            public string txtb10 { get; set; }
            public string txtchewut1 { get; set; }
            public string txtchewut2 { get; set; }
            public string txtchewut3 { get; set; }
            public string txtchewut4 { get; set; }
            public string txtchewut5 { get; set; }
            public string txtchewut6 { get; set; }
            public string txtchewut7 { get; set; }
            public string txtchewut8 { get; set; }
            public string txtchewut9 { get; set; }
            public string txtchewut10 { get; set; }
            public string txtchewut11 { get; set; }
            public string txtchewut12 { get; set; }
            public string txtchewut13 { get; set; }
            public string txtchewut14 { get; set; }
            public string txtchewut15 { get; set; }
            public string txtchewut16 { get; set; }
            public string txtchewut17 { get; set; }
            public string txtchewut18 { get; set; }
            public string txtchewut19 { get; set; }
            public string txtchewut20 { get; set; }
            public string txtfinal1 { get; set; }
            public string txtfinal2 { get; set; }
            public string txtfinal3 { get; set; }
            public string txtfinal4 { get; set; }
            public string txtfinal5 { get; set; }
            public string txtfinal6 { get; set; }
            public string txtfinal7 { get; set; }
            public string txtfinal8 { get; set; }
            public string txtfinal9 { get; set; }
            public string txtfinal10 { get; set; }
            public string txtmid1 { get; set; }
            public string txtmid2 { get; set; }
            public string txtmid3 { get; set; }
            public string txtmid4 { get; set; }
            public string txtmid5 { get; set; }
            public string txtmid6 { get; set; }
            public string txtmid7 { get; set; }
            public string txtmid8 { get; set; }
            public string txtmid9 { get; set; }
            public string txtmid10 { get; set; }
            public int? sort2 { get; set; }
            public int sort1int { get; set; }
            public string sort1txt { get; set; }
            public string getgrade { get; set; }
            public string totalquiz { get; set; }
            public string total100 { get; set; }
            public string totalfinal { get; set; }
            public string totalmid { get; set; }
            public string studentid { get; set; }
            public string gradeprint { get; set; }
            public string get70 { get; set; }
            public int? studentstatus { get; set; }
        }
    }

}