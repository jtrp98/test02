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
using System.Data.Entity;
using JabjaiSchoolGradeEntity;

namespace FingerprintPayment.grade
{
    public partial class SamattanaRegister : System.Web.UI.Page
    {
        private JWTToken.userData userData = new JWTToken.userData();
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade());
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            dgd.RowDataBound += new GridViewRowEventHandler(dgd_RowDataBound);

            btnSave.Click += new EventHandler(btnCancle_Click);
            btnCancle.Click += new EventHandler(btnCancle_Click);
            
            string id = Request.QueryString["id"];
            string idlv2 = Request.QueryString["idlv2"];
            string idlv = Request.QueryString["idlv"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];
            string mode = Request.QueryString["mode"];
            int nidlv2 = int.Parse(idlv2);
            var room = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == nidlv2 && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var sub = _db.TSubLevels.Where(w => w.nTSubLevel == room.nTSubLevel && w.SchoolID == userData.CompanyID).FirstOrDefault();




            Year.Text = Request.QueryString["term"] + "/" + Request.QueryString["year"];

            if (mode != "EN")
            {
                txtclass.Text = sub.SubLevel + " / " + room.nTSubLevel2;
                headertxtclass.Text = "ชั้นเรียน";               
                headerteacher1.Text = "ครูประจำชั้น";
                headertxtyear.Text = "ปีการศึกษา";
                tablestudentname.Text = "ชื่อ - นามสกุล";
                tablenumber.Text = "ลำดับ";
                tablefullscore.Text = "คะแนนเต็ม";                
                              
                tablescore100.Text = "เฉลี่ย";
                tablegrade.Text = "ระดับคุณภาพ";
                tabOne.Text = "สมรรถนะ 1";
                tabTwo.Text = "สมรรถนะ 2";
                tabThree.Text = "สมรรถนะ 3";
                tabFour.Text = "สมรรถนะ 4";
                tabFive.Text = "สมรรถนะ 5";

               
            }
            else
            {
                txtclass.Text = sub.SubLevelEN + " / " + room.nTSubLevel2;
                headertxtclass.Text = "Classroom";
                headerteacher1.Text = "Homeroom's teacher";
                headertxtyear.Text = "Academic year";
                tablestudentname.Text = "Student's name";
                tablenumber.Text = "No.";
                tablefullscore.Text = "Full score";
                
                tablescore100.Text = "Average";
                tablegrade.Text = "Score";
                
                tabOne.Text = "สมรรถนะ 1";
                tabTwo.Text = "สมรรถนะ 2";
                tabThree.Text = "สมรรถนะ 3";
                tabFour.Text = "สมรรถนะ 4";
                tabFive.Text = "สมรรถนะ 5";                
            }

            if (!IsPostBack)
            {
                OpenData();

               




                if ((year != "" && year != null) && (idlv2 != "" && idlv2 != null))
                {
                    int? idlv2n = Int32.Parse(idlv2);
                    int? useryear = Int32.Parse(year);
                    int nyear = 0;
                    string nterm = "";
                    int ntermtable = 0;
                    List<string> planIdlist = new List<string>();
                    List<string> copyplanIdlist = new List<string>();
                    List<string> teacherList = new List<string>();


                    foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear && w.SchoolID == userData.CompanyID))
                    {
                        nyear = ff.nYear;
                    }

                    foreach (var ee in _db.TTerms.Where(w => w.sTerm == term && w.nYear == nyear && w.SchoolID == userData.CompanyID && w.cDel == null))
                    {
                        nterm = ee.nTerm;
                    }



                    foreach (var dd in _db.TTermTimeTables.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID))
                    {
                        ntermtable = dd.nTermTable;
                    }

                    foreach (var hh in _db.TSchedules.Where(w => w.nTermTable == ntermtable && w.sPlaneID.ToString() == id && w.SchoolID == userData.CompanyID))
                    {
                        string planId = "";
                        if (hh.sPlaneID != null)
                        {
                            planId = hh.sPlaneID.ToString();
                            planIdlist.Add(planId);
                        }
                        teacherList.Add(hh.sEmp.ToString());
                    }

                    foreach (var hh2 in _db.TSchedules.Where(w => w.nTermTable == ntermtable && w.SchoolID == userData.CompanyID))
                    {
                        string planId2 = "";
                        planId2 = hh2.sPlaneID.ToString();
                        copyplanIdlist.Add(planId2);
                    }

                    teacher1.Text = "";
                    var teacherdata = _db.TClassMembers.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();

                    if (teacherdata != null && teacherdata.nTeacherHeadid != null)
                    {
                        var teacher = _db.TEmployees.Where(w => w.sEmp == teacherdata.nTeacherHeadid && w.SchoolID == userData.CompanyID).FirstOrDefault();
                        if (teacher != null)
                            teacher1.Text = teacher.sName + " " + teacher.sLastname;
                    }


                    List<string> unique3 = copyplanIdlist.Distinct().ToList();
                    List<string> unique = planIdlist.Distinct().ToList();
                    List<string> unique2 = teacherList.Distinct().ToList();
                    string teach = "";
                    int count2 = 0;
                    int countid = 0;

                    foreach (var teacherid in unique2)
                    {
                        if (teacherid != "" && teacherid != null)
                        {
                            countid = countid + 1;
                        }
                    }

                    if (countid > 1)
                    {
                        foreach (var teacherid in unique2)
                        {

                            count2 = count2 + 1;
                            if (teacherid != "" && teacherid != null)
                            {
                                int nemp = int.Parse(teacherid);
                                var tdata = _db.TEmployees.Where(w => w.sEmp == nemp && w.SchoolID == userData.CompanyID).FirstOrDefault();
                                if (tdata != null)
                                {
                                    teach = teach + count2 + "." + tdata.sName + " " + tdata.sLastname + " ";
                                }
                            }
                        }
                    }
                    else if (countid == 1)
                    {
                        foreach (var teacherid2 in unique2)
                        {
                            if (teacherid2 != "" && teacherid2 != null)
                            {
                                int nemp = int.Parse(teacherid2);
                                var tdata = _db.TEmployees.Where(w => w.sEmp == nemp && w.SchoolID == userData.CompanyID).FirstOrDefault();
                                if (tdata != null)
                                {
                                    teach = tdata.sName + " " + tdata.sLastname + " ";
                                }
                            }
                        }
                    }


                    int count = 0;
                    foreach (var planid in unique3)
                    {
                        var grade = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == planid && w.nTerm == nterm && w.GradeShareData != "1" && w.SchoolID == userData.CompanyID).FirstOrDefault();
                        if (grade != null)
                        {
                            count = count + 1;
                             var plan = _db.TPlanes.Where(w => w.sPlaneID == grade.sPlaneID && w.SchoolID == userData.CompanyID).FirstOrDefault();
                            var item = new ListItem
                            {
                                Text = plan.sPlaneName,
                                Value = plan.sPlaneID.ToString()
                            };
                            ddlcopy1.Items.Add(item);
                            ddlcopy2.Items.Add(item);
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
            JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade());
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));

            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == HttpContext.Current.Session["sEntities"].ToString()).FirstOrDefault();

            string nterm = "";
            int nyear = 0;
            string id = Request.QueryString["id"];
            string idlv2 = Request.QueryString["idlv2"];
            int? idlv2n = Int32.Parse(idlv2);
            string yyy = Request.QueryString["year"];
            int? useryear = Int32.Parse(yyy);
            string userterm = Request.QueryString["term"];
            string idlv = Request.QueryString["idlv"];
            int? idlvn = Int32.Parse(idlv);


            foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear && w.SchoolID == userData.CompanyID))
            {
                nyear = ff.nYear;
            }
            foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.SchoolID == userData.CompanyID && w.cDel == null))
            {
                nterm = ee.nTerm;
               
            }

            List<TGrade> gradelist = new List<TGrade>();
            TGrade grade = new TGrade();
            List<TGradeDetail> detail_list = new List<TGradeDetail>();
            TGradeDetail detail = new TGradeDetail();
            int sEmpID = int.Parse(Session["sEmpID"] + "");

            int dgdrow = 0;
            foreach (GridViewRow dgItem in dgd.Rows)
            {
                dgdrow = dgdrow + 1;
            }

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
            string[] dgdother = new string[dgdrow];
            string[] dgdTotalScore = new string[dgdrow];
            string[] dgdTotalGrade = new string[dgdrow];

            int row = 0;
            foreach (GridViewRow dgItem in dgdp2.Rows)
            {
                TextBox txtGrade6 = (TextBox)dgItem.FindControl("txtGrade6");
                dgd6[row] = txtGrade6.Text;
                TextBox txtGrade7 = (TextBox)dgItem.FindControl("txtGrade7");
                dgd7[row] = txtGrade7.Text;
                TextBox txtGrade8 = (TextBox)dgItem.FindControl("txtGrade8");
                dgd8[row] = txtGrade8.Text;
                TextBox txtGrade9 = (TextBox)dgItem.FindControl("txtGrade9");
                dgd9[row] = txtGrade9.Text;
                TextBox txtGrade10 = (TextBox)dgItem.FindControl("txtGrade10");
                dgd10[row] = txtGrade10.Text;
                row = row + 1;
            }
            row = 0;

            foreach (GridViewRow dgItem in dgdp3.Rows)
            {
                TextBox txtGrade11 = (TextBox)dgItem.FindControl("txtGrade11");
                dgd11[row] = txtGrade11.Text;
                TextBox txtGrade12 = (TextBox)dgItem.FindControl("txtGrade12");
                dgd12[row] = txtGrade12.Text;
                TextBox txtGrade13 = (TextBox)dgItem.FindControl("txtGrade13");
                dgd13[row] = txtGrade13.Text;
                TextBox txtGrade14 = (TextBox)dgItem.FindControl("txtGrade14");
                dgd14[row] = txtGrade14.Text;
                TextBox txtGrade15 = (TextBox)dgItem.FindControl("txtGrade15");
                dgd15[row] = txtGrade15.Text;
                row = row + 1;
            }
            row = 0;

            foreach (GridViewRow dgItem in dgdp4.Rows)
            {
                TextBox txtGrade16 = (TextBox)dgItem.FindControl("txtGrade16");
                dgd16[row] = txtGrade16.Text;
                TextBox txtGrade17 = (TextBox)dgItem.FindControl("txtGrade17");
                dgd17[row] = txtGrade17.Text;
                TextBox txtGrade18 = (TextBox)dgItem.FindControl("txtGrade18");
                dgd18[row] = txtGrade18.Text;
                TextBox txtGrade19 = (TextBox)dgItem.FindControl("txtGrade19");
                dgd19[row] = txtGrade19.Text;
                TextBox txtGrade20 = (TextBox)dgItem.FindControl("txtGrade20");
                dgd20[row] = txtGrade20.Text;
                row = row + 1;
            }
            row = 0;

            foreach (GridViewRow dgItem in dgdp5.Rows)
            {
                TextBox behavior1 = (TextBox)dgItem.FindControl("behave1");
                behave1[row] = behavior1.Text;
                TextBox behavior2 = (TextBox)dgItem.FindControl("behave2");
                behave2[row] = behavior2.Text;
                TextBox behavior3 = (TextBox)dgItem.FindControl("behave3");
                behave3[row] = behavior3.Text;
                TextBox behavior4 = (TextBox)dgItem.FindControl("behave4");
                behave4[row] = behavior4.Text;
                TextBox behavior5 = (TextBox)dgItem.FindControl("behave5");
                behave5[row] = behavior5.Text;
                row = row + 1;
            }
            row = 0;

            foreach (GridViewRow dgItem in dgdp6.Rows)
            {
                TextBox behavior6 = (TextBox)dgItem.FindControl("behave6");
                behave6[row] = behavior6.Text;
                TextBox behavior7 = (TextBox)dgItem.FindControl("behave7");
                behave7[row] = behavior7.Text;
                TextBox behavior8 = (TextBox)dgItem.FindControl("behave8");
                behave8[row] = behavior8.Text;
                TextBox behavior9 = (TextBox)dgItem.FindControl("behave9");
                behave9[row] = behavior9.Text;
                TextBox behavior10 = (TextBox)dgItem.FindControl("behave10");
                behave10[row] = behavior10.Text;
                row = row + 1;
            }
            row = 0;

            foreach (GridViewRow dgItem in dgdp7.Rows)
            {
                TextBox chewat1 = (TextBox)dgItem.FindControl("chewat1");
                dgdcw1[row] = chewat1.Text;
                TextBox chewat2 = (TextBox)dgItem.FindControl("chewat2");
                dgdcw2[row] = chewat2.Text;
                TextBox chewat3 = (TextBox)dgItem.FindControl("chewat3");
                dgdcw3[row] = chewat3.Text;
                TextBox chewat4 = (TextBox)dgItem.FindControl("chewat4");
                dgdcw4[row] = chewat4.Text;
                TextBox chewat5 = (TextBox)dgItem.FindControl("chewat5");
                dgdcw5[row] = chewat5.Text;
                row = row + 1;
            }
            row = 0;

           
            foreach (GridViewRow dgItem in dgd3.Rows)
            {

                TextBox txtSID = (TextBox)dgItem.FindControl("sID");
                int sID = Int32.Parse(txtSID.Text);

                TextBox txtMidScore = (TextBox)dgItem.FindControl("txtMidScore");
                dgdmid[row] = txtMidScore.Text;
                TextBox txtLateScore = (TextBox)dgItem.FindControl("txtLateScore");
                dgdlate[row] = txtLateScore.Text;
                TextBox txtGoodBehavior = (TextBox)dgItem.FindControl("txtGoodBehavior");
                dgdbehave[row] = txtGoodBehavior.Text;
                TextBox txtGoodReading = (TextBox)dgItem.FindControl("txtGoodReading");
                dgdreading[row] = txtGoodReading.Text;
                TextBox txtTotalScore = (TextBox)dgItem.FindControl("txtTotalScore");
                dgdTotalScore[row] = txtTotalScore.Text;
                TextBox txtTotalGrade = (TextBox)dgItem.FindControl("txtTotalGrade");
                dgdTotalGrade[row] = txtTotalGrade.Text;
                DropDownList ddlOther = (DropDownList)dgItem.FindControl("ddlOther");
                dgdother[row] = ddlOther.SelectedValue;
                row = row + 1;
            }
            row = 0;
            //int countDetail = 0;
            //foreach (var x in _db.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID))
            //{
            //    if (countDetail <= x.nGradeDetailId)
            //        countDetail = x.nGradeDetailId + 1;
            //    else countDetail = countDetail + 1;
            //}

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

                string strb1 = behave1[row];
                string strb2 = behave2[row];
                string strb3 = behave3[row];
                string strb4 = behave4[row];
                string strb5 = behave5[row];
                string strb6 = behave6[row];
                string strb7 = behave7[row];
                string strb8 = behave8[row];
                string strb9 = behave9[row];
                string strb10 = behave10[row];

                string totalMid = dgdmid[row];
                string totalLate = dgdlate[row];
                string totalBehave = dgdbehave[row];
                string totalReading = dgdreading[row];
                string totalOther = dgdother[row];
                string totalScore = dgdTotalScore[row];
                string totalGrade = dgdTotalGrade[row];

                int totalcome = 0;
                int totalskip = 0;
                int totalleave = 0;
                int totalsick = 0;

                
                var data = _db.TUsers.Where(w => w.sID == sID && w.SchoolID == userData.CompanyID).FirstOrDefault();
                
                double quiz1 = 0;
                double quiz2 = 0;
                double quiz3 = 0;
                double quiz4 = 0;
                double quiz5 = 0;
                double quiz6 = 0;
                double quiz7 = 0;
                double quiz8 = 0;
                double quiz9 = 0;
                double quiz10 = 0;
                double quiz11 = 0;
                double quiz12 = 0;
                double quiz13 = 0;
                double quiz14 = 0;
                double quiz15 = 0;
                double quiz16 = 0;
                double quiz17 = 0;
                double quiz18 = 0;
                double quiz19 = 0;
                double quiz20 = 0;

                double chewatscore1 = 0;
                double chewatscore2 = 0;
                double chewatscore3 = 0;
                double chewatscore4 = 0;
                double chewatscore5 = 0;
                double chewatscore6 = 0;
                double chewatscore7 = 0;
                double chewatscore8 = 0;
                double chewatscore9 = 0;
                double chewatscore10 = 0;
                double chewatscore11 = 0;
                double chewatscore12 = 0;
                double chewatscore13 = 0;
                double chewatscore14 = 0;
                double chewatscore15 = 0;
                double chewatscore16 = 0;
                double chewatscore17 = 0;
                double chewatscore18 = 0;
                double chewatscore19 = 0;
                double chewatscore20 = 0;

                double behaveScore1 = 0;
                double behaveScore2 = 0;
                double behaveScore3 = 0;
                double behaveScore4 = 0;
                double behaveScore5 = 0;
                double behaveScore6 = 0;
                double behaveScore7 = 0;
                double behaveScore8 = 0;
                double behaveScore9 = 0;
                double behaveScore10 = 0;

                if (strb1 != "")
                    behaveScore1 = Double.Parse(strb1);
                if (strb2 != "")
                    behaveScore2 = Double.Parse(strb2);
                if (strb3 != "")
                    behaveScore3 = Double.Parse(strb3);
                if (strb4 != "")
                    behaveScore4 = Double.Parse(strb4);
                if (strb5 != "")
                    behaveScore5 = Double.Parse(strb5);
                if (strb6 != "")
                    behaveScore6 = Double.Parse(strb6);
                if (strb7 != "")
                    behaveScore7 = Double.Parse(strb7);
                if (strb8 != "")
                    behaveScore8 = Double.Parse(strb8);
                if (strb9 != "")
                    behaveScore9 = Double.Parse(strb9);
                if (strb10 != "")
                    behaveScore10 = Double.Parse(strb10);

                if (cwscore1 != "")
                    chewatscore1 = Double.Parse(cwscore1);
                if (cwscore2 != "")
                    chewatscore2 = Double.Parse(cwscore2);
                if (cwscore3 != "")
                    chewatscore3 = Double.Parse(cwscore3);
                if (cwscore4 != "")
                    chewatscore4 = Double.Parse(cwscore4);
                if (cwscore5 != "")
                    chewatscore5 = Double.Parse(cwscore5);
                if (cwscore6 != "")
                    chewatscore6 = Double.Parse(cwscore6);
                if (cwscore7 != "")
                    chewatscore7 = Double.Parse(cwscore7);
                if (cwscore8 != "")
                    chewatscore8 = Double.Parse(cwscore8);
                if (cwscore9 != "")
                    chewatscore9 = Double.Parse(cwscore9);
                if (cwscore10 != "")
                    chewatscore10 = Double.Parse(cwscore10);
                if (cwscore11 != "")
                    chewatscore11 = Double.Parse(cwscore11);
                if (cwscore12 != "")
                    chewatscore12 = Double.Parse(cwscore12);
                if (cwscore13 != "")
                    chewatscore13 = Double.Parse(cwscore13);
                if (cwscore14 != "")
                    chewatscore14 = Double.Parse(cwscore14);
                if (cwscore15 != "")
                    chewatscore15 = Double.Parse(cwscore15);
                if (cwscore16 != "")
                    chewatscore16 = Double.Parse(cwscore16);
                if (cwscore17 != "")
                    chewatscore17 = Double.Parse(cwscore17);
                if (cwscore18 != "")
                    chewatscore18 = Double.Parse(cwscore18);
                if (cwscore19 != "")
                    chewatscore19 = Double.Parse(cwscore19);
                if (cwscore20 != "")
                    chewatscore20 = Double.Parse(cwscore20);

                if (str1 != "")
                    quiz1 = Double.Parse(str1);
                if (str2 != "")
                    quiz2 = Double.Parse(str2);
                if (str3 != "")
                    quiz3 = Double.Parse(str3);
                if (str4 != "")
                    quiz4 = Double.Parse(str4);
                if (str5 != "")
                    quiz5 = Double.Parse(str5);
                if (str6 != "")
                    quiz6 = Double.Parse(str6);
                if (str7 != "")
                    quiz7 = Double.Parse(str7);
                if (str8 != "")
                    quiz8 = Double.Parse(str8);
                if (str9 != "")
                    quiz9 = Double.Parse(str9);
                if (str10 != "")
                    quiz10 = Double.Parse(str10);
                if (str11 != "")
                    quiz11 = Double.Parse(str11);
                if (str12 != "")
                    quiz12 = Double.Parse(str12);
                if (str13 != "")
                    quiz13 = Double.Parse(str13);
                if (str14 != "")
                    quiz14 = Double.Parse(str14);
                if (str15 != "")
                    quiz15 = Double.Parse(str15);
                if (str16 != "")
                    quiz16 = Double.Parse(str16);
                if (str17 != "")
                    quiz17 = Double.Parse(str17);
                if (str18 != "")
                    quiz18 = Double.Parse(str18);
                if (str19 != "")
                    quiz19 = Double.Parse(str19);
                if (str20 != "")
                    quiz20 = Double.Parse(str20);

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

                var check1 = _dbGrade.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
                var check2 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == check1.nGradeId && w.sID == sID).FirstOrDefault();

                int sPlaneId = 0;
                int.TryParse(id, out sPlaneId);
                //var studentAssessmentScore = new TStudentAssessmentScore() { sID = sID, UpdatedBy = sEmpID, UpdatedDate = DateTime.UtcNow, SchoolId = tCompany.nCompany, nTermSubLevel2 = idlv2n, nTSubLevel = idlvn, nGradeId = check1.nGradeId, sPlaneID = sPlaneId, nTerm = nterm };


                if (check2 == null)
                {
                    detail = new TGradeDetail();

                    if (nobehaveCheck.Text == "1")
                    {
                        detail.getBehaviorTotal = "";
                        detail.getBehaviorLabel = "";
                        detail.getReadWrite = "";

                        //InsertOrUpdateStudentScore("nameBehavior1", "scoreBehavior1", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior2", "scoreBehavior2", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior3", "scoreBehavior3", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior4", "scoreBehavior4", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior5", "scoreBehavior5", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior6", "scoreBehavior6", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior7", "scoreBehavior7", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior8", "scoreBehavior8", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior9", "scoreBehavior9", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior10", "scoreBehavior10", "", studentAssessmentScore, _db);

                       
                    }
                    else
                    {
                        detail.getBehaviorLabel = totalBehave;
                        detail.getReadWrite = totalReading;
                        detail.getBehaviorTotal = behavesum.ToString();

                        //InsertOrUpdateStudentScore("nameBehavior1", "scoreBehavior1", strb1, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior2", "scoreBehavior2", strb2, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior3", "scoreBehavior3", strb3, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior4", "scoreBehavior4", strb4, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior5", "scoreBehavior5", strb5, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior6", "scoreBehavior6", strb6, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior7", "scoreBehavior7", strb7, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior8", "scoreBehavior8", strb8, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior9", "scoreBehavior9", strb9, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior10", "scoreBehavior10", strb10, studentAssessmentScore, _db);

                      
                    }
                    detail.getGradeLabel = totalGrade;
                    detail.getScore100 = totalScore;
                    detail.getSpecial = totalOther;
                    ////detail.nGradeDetailId = countDetail;
                    //countDetail = countDetail + 1;
                    detail.nGradeId = check1.nGradeId;
                    detail.sID = sID;
                    detail.scoreFinalTerm = totalLate;
                    detail.scoreMidTerm = totalMid;


                    //InsertOrUpdateStudentScore("nameGrade1", "scoreGrade1", str1, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade2", "scoreGrade2", str2, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade3", "scoreGrade3", str3, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade4", "scoreGrade4", str4, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade5", "scoreGrade5", str5, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade6", "scoreGrade6", str6, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade7", "scoreGrade7", str7, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade8", "scoreGrade8", str8, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade9", "scoreGrade9", str9, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade10", "scoreGrade10", str10, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade11", "scoreGrade11", str11, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade12", "scoreGrade12", str12, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade13", "scoreGrade13", str13, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade14", "scoreGrade14", str14, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade15", "scoreGrade15", str15, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade16", "scoreGrade16", str16, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade17", "scoreGrade17", str17, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade18", "scoreGrade18", str18, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade19", "scoreGrade19", str19, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade20", "scoreGrade20", str20, studentAssessmentScore, _db);


                    //InsertOrUpdateStudentScore("nameCheewat1", "scoreCheewat1", cwscore1, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat2", "scoreCheewat2", cwscore2, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat3", "scoreCheewat3", cwscore3, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat4", "scoreCheewat4", cwscore4, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat5", "scoreCheewat5", cwscore5, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat6", "scoreCheewat6", cwscore6, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat7", "scoreCheewat7", cwscore7, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat8", "scoreCheewat8", cwscore8, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat9", "scoreCheewat9", cwscore9, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat10", "scoreCheewat10", cwscore10, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat11", "scoreCheewat11", cwscore11, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat12", "scoreCheewat12", cwscore12, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat13", "scoreCheewat13", cwscore13, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat14", "scoreCheewat14", cwscore14, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat15", "scoreCheewat15", cwscore15, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat16", "scoreCheewat16", cwscore16, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat17", "scoreCheewat17", cwscore17, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat18", "scoreCheewat18", cwscore18, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat19", "scoreCheewat19", cwscore19, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat20", "scoreCheewat20", cwscore20, studentAssessmentScore, _db);

                    _dbGrade.TGradeDetails.Add(detail);
                }
                else
                {
                    if (nobehaveCheck.Text == "1")
                    {
                        check2.getBehaviorTotal = "";
                        check2.getBehaviorLabel = "";
                        check2.getReadWrite = "";
                        //InsertOrUpdateStudentScore("nameBehavior1", "scoreBehavior1", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior2", "scoreBehavior2", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior3", "scoreBehavior3", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior4", "scoreBehavior4", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior5", "scoreBehavior5", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior6", "scoreBehavior6", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior7", "scoreBehavior7", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior8", "scoreBehavior8", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior9", "scoreBehavior9", "", studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior10", "scoreBehavior10", "", studentAssessmentScore, _db);
                    }
                    else
                    {
                        check2.getBehaviorTotal = behavesum.ToString();
                        check2.getBehaviorLabel = totalBehave;
                        check2.getReadWrite = totalReading;

                        //InsertOrUpdateStudentScore("nameBehavior1", "scoreBehavior1", strb1, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior2", "scoreBehavior2", strb2, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior3", "scoreBehavior3", strb3, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior4", "scoreBehavior4", strb4, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior5", "scoreBehavior5", strb5, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior6", "scoreBehavior6", strb6, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior7", "scoreBehavior7", strb7, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior8", "scoreBehavior8", strb8, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior9", "scoreBehavior9", strb9, studentAssessmentScore, _db);
                        //InsertOrUpdateStudentScore("nameBehavior10", "scoreBehavior10", strb10, studentAssessmentScore, _db);

                    }

                    check2.getGradeLabel = totalGrade;
                    check2.getScore100 = totalScore;
                    check2.getSpecial = totalOther;
                    check2.scoreFinalTerm = totalLate;
                    check2.scoreMidTerm = totalMid;


                    //InsertOrUpdateStudentScore("nameGrade1", "scoreGrade1", str1, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade2", "scoreGrade2", str2, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade3", "scoreGrade3", str3, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade4", "scoreGrade4", str4, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade5", "scoreGrade5", str5, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade6", "scoreGrade6", str6, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade7", "scoreGrade7", str7, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade8", "scoreGrade8", str8, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade9", "scoreGrade9", str9, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade10", "scoreGrade10", str10, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade11", "scoreGrade11", str11, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade12", "scoreGrade12", str12, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade13", "scoreGrade13", str13, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade14", "scoreGrade14", str14, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade15", "scoreGrade15", str15, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade16", "scoreGrade16", str16, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade17", "scoreGrade17", str17, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade18", "scoreGrade18", str18, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade19", "scoreGrade19", str19, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameGrade20", "scoreGrade20", str20, studentAssessmentScore, _db);

                    //InsertOrUpdateStudentScore("nameCheewat1", "scoreCheewat1", cwscore1, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat2", "scoreCheewat2", cwscore2, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat3", "scoreCheewat3", cwscore3, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat4", "scoreCheewat4", cwscore4, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat5", "scoreCheewat5", cwscore5, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat6", "scoreCheewat6", cwscore6, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat7", "scoreCheewat7", cwscore7, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat8", "scoreCheewat8", cwscore8, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat9", "scoreCheewat9", cwscore9, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat10", "scoreCheewat10", cwscore10, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat11", "scoreCheewat11", cwscore11, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat12", "scoreCheewat12", cwscore12, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat13", "scoreCheewat13", cwscore13, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat14", "scoreCheewat14", cwscore14, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat15", "scoreCheewat15", cwscore15, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat16", "scoreCheewat16", cwscore16, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat17", "scoreCheewat17", cwscore17, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat18", "scoreCheewat18", cwscore18, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat19", "scoreCheewat19", cwscore19, studentAssessmentScore, _db);
                    //InsertOrUpdateStudentScore("nameCheewat20", "scoreCheewat20", cwscore20, studentAssessmentScore, _db);


                    double mid100 = 0;
                    

                }

                row = row + 1;

            }

            _db.SaveChanges();

            //string idlv = Request.QueryString["idlv"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];

            Response.Redirect("SamattanaList.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term + "&id=" + id);
        }

        //private void InsertOrUpdateStudentScore(string nameIndentifer, string scoreIdentifier, string studentScore, TStudentAssessmentScore studentAssessmentScore, JabJaiEntities _db)
        //{
        //    if (_db.TStudentAssessmentScores.Where(c => c.nGradeId == studentAssessmentScore.nGradeId && c.sID == studentAssessmentScore.sID && c.ScoreIdentifier == scoreIdentifier && c.SchoolId == userData.CompanyID).Count() > 0)
        //    {
        //        _db.TStudentAssessmentScores.Where(c => c.nGradeId == studentAssessmentScore.nGradeId && c.sID == studentAssessmentScore.sID && c.ScoreIdentifier == scoreIdentifier && c.SchoolId == userData.CompanyID).ForEachAsync(f => { f.Score = studentScore; f.UpdatedBy = studentAssessmentScore.UpdatedBy; f.UpdatedDate = studentAssessmentScore.UpdatedDate; });
        //    }
        //    else
        //    {

        //        var assessment = _db.TAssessments.Where(c => c.nGradeId == studentAssessmentScore.nGradeId && c.nTerm == studentAssessmentScore.nTerm && c.sPlaneID == studentAssessmentScore.sPlaneID && c.nYear == studentAssessmentScore.nYear && c.NameIdentifier == nameIndentifer && c.SchoolId == studentAssessmentScore.SchoolId).FirstOrDefault();

        //        if (assessment != null)
        //        {
        //            studentAssessmentScore.AssessmentId = assessment.AssessmentId;
        //            studentAssessmentScore.CreatedBy = studentAssessmentScore.UpdatedBy;
        //            studentAssessmentScore.CreatedDate = studentAssessmentScore.UpdatedDate;
        //            studentAssessmentScore.Score = !string.IsNullOrEmpty(studentScore) ? studentScore : "";
        //            studentAssessmentScore.ScoreIdentifier = scoreIdentifier;
        //            _db.TStudentAssessmentScores.Add(studentAssessmentScore);
        //        }
        //    }
        //}

        void btnCancle_Click(object sender, EventArgs e)
        {

            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];
           
            Response.Redirect("SamattanaList.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term);
        }

        private void OpenData()
        {
            JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade());
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();

            string sEntities = Session["sEntities"].ToString();
            var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

            string id = Request.QueryString["id"];
            string year = Request.QueryString["year"];
            string userterm = Request.QueryString["term"];
            string idlv2 = Request.QueryString["idlv2"];
            string mode = Request.QueryString["mode"];

            int? useryear = Int32.Parse(year);
            int? idlv2n = Int32.Parse(idlv2);
            string username = "";
            int? ntermsublv2 = 0;
            int nyear = 0;
            string nterm = "";
            int ntermtable = 0;

            setname1.Text = "ตัวชี้วัด 1.1";
            setname2.Text = "ตัวชี้วัด 1.2";
            setname3.Text = "ตัวชี้วัด 1.3";
            setname4.Text = "ตัวชี้วัด 1.4";
            setname5.Text = "ตัวชี้วัด 2.1";
            setname6.Text = "ตัวชี้วัด 2.2";
            setname7.Text = "ตัวชี้วัด 3.1";
            setname8.Text = "ตัวชี้วัด 3.2";
            setname9.Text = "ตัวชี้วัด 4";

            setname10.Text = "ตัวชี้วัด 1.1";
            setname11.Text = "ตัวชี้วัด 1.2";
            setname12.Text = "ตัวชี้วัด 1.3";
            setname13.Text = "ตัวชี้วัด 2.1";
            setname14.Text = "ตัวชี้วัด 2.2";
            setname15.Text = "ตัวชี้วัด 2.3";
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

            foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear && w.SchoolID == userData.CompanyID))
            {
                nyear = ff.nYear;
            }

            foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.SchoolID == userData.CompanyID && w.cDel == null))
            {
                nterm = ee.nTerm;
            }

            ddlbox1.Text = "60";
            ddlbox2.Text = "20";
            ddlbox3.Text = "20";
            ddlbox4.Text = "60";

            var ratio = _dbGrade.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            if (ratio != null)
            {

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
            }


            int number = 1;
            studentlist2 student = new studentlist2();
            List<studentlist2> studentlist = new List<studentlist2>();
            var max = _dbGrade.TB_GradeViews.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.GradeShareData != "1" && w.SchoolID == userData.CompanyID).FirstOrDefault();
            if (max != null)
            {
                if (max.nameGrade1 != null)
                    setname1.Text = max.nameGrade1;
                if (max.nameGrade2 != null)
                    setname2.Text = max.nameGrade2;
                if (max.nameGrade3 != null)
                    setname3.Text = max.nameGrade3;
                if (max.nameGrade4 != null)
                    setname4.Text = max.nameGrade4;
                if (max.nameGrade5 != null)
                    setname5.Text = max.nameGrade5;
                if (max.nameGrade6 != null)
                    setname6.Text = max.nameGrade6;
                if (max.nameGrade7 != null)
                    setname7.Text = max.nameGrade7;
                if (max.nameGrade8 != null)
                    setname8.Text = max.nameGrade8;
                if (max.nameGrade9 != null)
                    setname9.Text = max.nameGrade9;
                if (max.nameGrade10 != null)
                    setname10.Text = max.nameGrade10;
                if (max.nameGrade11 != null)
                    setname11.Text = max.nameGrade11;
                if (max.nameGrade12 != null)
                    setname12.Text = max.nameGrade12;
                if (max.nameGrade13 != null)
                    setname13.Text = max.nameGrade13;
                if (max.nameGrade14 != null)
                    setname14.Text = max.nameGrade14;
                if (max.nameGrade15 != null)
                    setname15.Text = max.nameGrade15;
                if (max.nameGrade16 != null)
                    setname16.Text = max.nameGrade16;
                if (max.nameGrade17 != null)
                    setname17.Text = max.nameGrade17;
                if (max.nameGrade18 != null)
                    setname18.Text = max.nameGrade18;
                if (max.nameGrade19 != null)
                    setname19.Text = max.nameGrade19;
                if (max.nameGrade20 != null)
                    setname20.Text = max.nameGrade20;

                if (max.nameCheewat1 != null)
                    chewatname1.Text = max.nameCheewat1;
                if (max.nameCheewat2 != null)
                    chewatname2.Text = max.nameCheewat2;
                if (max.nameCheewat3 != null)
                    chewatname3.Text = max.nameCheewat3;
                if (max.nameCheewat4 != null)
                    chewatname4.Text = max.nameCheewat4;
                if (max.nameCheewat5 != null)
                    chewatname5.Text = max.nameCheewat5;
                if (max.nameCheewat6 != null)
                    chewatname6.Text = max.nameCheewat6;
                if (max.nameCheewat7 != null)
                    chewatname7.Text = max.nameCheewat7;
                if (max.nameCheewat8 != null)
                    chewatname8.Text = max.nameCheewat8;
                if (max.nameCheewat9 != null)
                    chewatname9.Text = max.nameCheewat9;
                if (max.nameCheewat10 != null)
                    chewatname10.Text = max.nameCheewat10;
                if (max.nameCheewat11 != null)
                    chewatname11.Text = max.nameCheewat11;
                if (max.nameCheewat12 != null)
                    chewatname12.Text = max.nameCheewat12;
                if (max.nameCheewat13 != null)
                    chewatname13.Text = max.nameCheewat13;
                if (max.nameCheewat14 != null)
                    chewatname14.Text = max.nameCheewat14;
                if (max.nameCheewat15 != null)
                    chewatname15.Text = max.nameCheewat15;
                if (max.nameCheewat16 != null)
                    chewatname16.Text = max.nameCheewat16;
                if (max.nameCheewat17 != null)
                    chewatname17.Text = max.nameCheewat17;
                if (max.nameCheewat18 != null)
                    chewatname18.Text = max.nameCheewat18;
                if (max.nameCheewat19 != null)
                    chewatname19.Text = max.nameCheewat19;
                if (max.nameCheewat20 != null)
                    chewatname20.Text = max.nameCheewat20;

                if (max.nameBehavior1 != null)
                    setnameb1.Text = max.nameBehavior1;
                if (max.nameBehavior2 != null)
                    setnameb2.Text = max.nameBehavior2;
                if (max.nameBehavior3 != null)
                    setnameb3.Text = max.nameBehavior3;
                if (max.nameBehavior4 != null)
                    setnameb4.Text = max.nameBehavior4;
                if (max.nameBehavior5 != null)
                    setnameb5.Text = max.nameBehavior5;
                if (max.nameBehavior6 != null)
                    setnameb6.Text = max.nameBehavior6;
                if (max.nameBehavior7 != null)
                    setnameb7.Text = max.nameBehavior7;
                if (max.nameBehavior8 != null)
                    setnameb8.Text = max.nameBehavior8;
                if (max.nameBehavior9 != null)
                    setnameb9.Text = max.nameBehavior9;
                if (max.nameBehavior10 != null)
                    setnameb10.Text = max.nameBehavior10;

                if (max.maxGrade1 != null)
                    maxS1.Text = max.maxGrade1.ToString();
                if (max.maxGrade2 != null)
                    maxS2.Text = max.maxGrade2.ToString();
                if (max.maxGrade3 != null)
                    maxS3.Text = max.maxGrade3.ToString();
                if (max.maxGrade4 != null)
                    maxS4.Text = max.maxGrade4.ToString();
                if (max.maxGrade5 != null)
                    maxS5.Text = max.maxGrade5.ToString();
                if (max.maxGrade6 != null)
                    maxS6.Text = max.maxGrade6.ToString();
                if (max.maxGrade7 != null)
                    maxS7.Text = max.maxGrade7.ToString();
                if (max.maxGrade8 != null)
                    maxS8.Text = max.maxGrade8.ToString();
                if (max.maxGrade9 != null)
                    maxS9.Text = max.maxGrade9.ToString();
                if (max.maxGrade10 != null)
                    maxS10.Text = max.maxGrade10.ToString();
                if (max.maxGrade11 != null)
                    maxS11.Text = max.maxGrade11.ToString();
                if (max.maxGrade12 != null)
                    maxS12.Text = max.maxGrade12.ToString();
                if (max.maxGrade13 != null)
                    maxS13.Text = max.maxGrade13.ToString();
                if (max.maxGrade14 != null)
                    maxS14.Text = max.maxGrade14.ToString();
                if (max.maxGrade15 != null)
                    maxS15.Text = max.maxGrade15.ToString();
                if (max.maxGrade16 != null)
                    maxS16.Text = max.maxGrade16.ToString();
                if (max.maxGrade17 != null)
                    maxS17.Text = max.maxGrade17.ToString();
                if (max.maxGrade18 != null)
                    maxS18.Text = max.maxGrade18.ToString();
                if (max.maxGrade19 != null)
                    maxS19.Text = max.maxGrade19.ToString();
                if (max.maxGrade20 != null)
                    maxS20.Text = max.maxGrade20.ToString();

                if (max.maxCheewat1 != null)
                    maxCW1.Text = max.maxCheewat1.ToString();
                if (max.maxCheewat2 != null)
                    maxCW2.Text = max.maxCheewat2.ToString();
                if (max.maxCheewat3 != null)
                    maxCW3.Text = max.maxCheewat3.ToString();
                if (max.maxCheewat4 != null)
                    maxCW4.Text = max.maxCheewat4.ToString();
                if (max.maxCheewat5 != null)
                    maxCW5.Text = max.maxCheewat5.ToString();
                if (max.maxCheewat6 != null)
                    maxCW6.Text = max.maxCheewat6.ToString();
                if (max.maxCheewat7 != null)
                    maxCW7.Text = max.maxCheewat7.ToString();
                if (max.maxCheewat8 != null)
                    maxCW8.Text = max.maxCheewat8.ToString();
                if (max.maxCheewat9 != null)
                    maxCW9.Text = max.maxCheewat9.ToString();
                if (max.maxCheewat10 != null)
                    maxCW10.Text = max.maxCheewat10.ToString();
                

                if (max.maxBehavior1 != null)
                    maxb1.Text = max.maxBehavior1.ToString();
                if (max.maxBehavior2 != null)
                    maxb2.Text = max.maxBehavior2.ToString();
                if (max.maxBehavior3 != null)
                    maxb3.Text = max.maxBehavior3.ToString();
                if (max.maxBehavior4 != null)
                    maxb4.Text = max.maxBehavior4.ToString();
                if (max.maxBehavior5 != null)
                    maxb5.Text = max.maxBehavior5.ToString();
                if (max.maxBehavior6 != null)
                    maxb6.Text = max.maxBehavior6.ToString();
                if (max.maxBehavior7 != null)
                    maxb7.Text = max.maxBehavior7.ToString();
                if (max.maxBehavior8 != null)
                    maxb8.Text = max.maxBehavior8.ToString();
               
            }

            var selfdata = _dbGrade.TB_GradeViews.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            if (selfdata != null)
            {
                if (selfdata.nameGrade1 != null)
                    setname1.Text = selfdata.nameGrade1;
                if (selfdata.nameGrade2 != null)
                    setname2.Text = selfdata.nameGrade2;
                if (selfdata.nameGrade3 != null)
                    setname3.Text = selfdata.nameGrade3;
                if (selfdata.nameGrade4 != null)
                    setname4.Text = selfdata.nameGrade4;
                if (selfdata.nameGrade5 != null)
                    setname5.Text = selfdata.nameGrade5;
                if (selfdata.nameGrade6 != null)
                    setname6.Text = selfdata.nameGrade6;
                if (selfdata.nameGrade7 != null)
                    setname7.Text = selfdata.nameGrade7;
                if (selfdata.nameGrade8 != null)
                    setname8.Text = selfdata.nameGrade8;
                if (selfdata.nameGrade9 != null)
                    setname9.Text = selfdata.nameGrade9;
                if (selfdata.nameGrade10 != null)
                    setname10.Text = selfdata.nameGrade10;
                if (selfdata.nameGrade11 != null)
                    setname11.Text = selfdata.nameGrade11;
                if (selfdata.nameGrade12 != null)
                    setname12.Text = selfdata.nameGrade12;
                if (selfdata.nameGrade13 != null)
                    setname13.Text = selfdata.nameGrade13;
                if (selfdata.nameGrade14 != null)
                    setname14.Text = selfdata.nameGrade14;
                if (selfdata.nameGrade15 != null)
                    setname15.Text = selfdata.nameGrade15;
                if (selfdata.nameGrade16 != null)
                    setname16.Text = selfdata.nameGrade16;
                if (selfdata.nameGrade17 != null)
                    setname17.Text = selfdata.nameGrade17;
                if (selfdata.nameGrade18 != null)
                    setname18.Text = selfdata.nameGrade18;
                if (selfdata.nameGrade19 != null)
                    setname19.Text = selfdata.nameGrade19;
                if (selfdata.nameGrade20 != null)
                    setname20.Text = selfdata.nameGrade20;

                if (selfdata.nameCheewat1 != null)
                    chewatname1.Text = selfdata.nameCheewat1;
                if (selfdata.nameCheewat2 != null)
                    chewatname2.Text = selfdata.nameCheewat2;
                if (selfdata.nameCheewat3 != null)
                    chewatname3.Text = selfdata.nameCheewat3;
                if (selfdata.nameCheewat4 != null)
                    chewatname4.Text = selfdata.nameCheewat4;
                if (selfdata.nameCheewat5 != null)
                    chewatname5.Text = selfdata.nameCheewat5;
                if (selfdata.nameCheewat6 != null)
                    chewatname6.Text = selfdata.nameCheewat6;
                if (selfdata.nameCheewat7 != null)
                    chewatname7.Text = selfdata.nameCheewat7;
                if (selfdata.nameCheewat8 != null)
                    chewatname8.Text = selfdata.nameCheewat8;
                if (selfdata.nameCheewat9 != null)
                    chewatname9.Text = selfdata.nameCheewat9;
                if (selfdata.nameCheewat10 != null)
                    chewatname10.Text = selfdata.nameCheewat10;
                if (selfdata.nameCheewat11 != null)
                    chewatname11.Text = selfdata.nameCheewat11;
                if (selfdata.nameCheewat12 != null)
                    chewatname12.Text = selfdata.nameCheewat12;
                if (selfdata.nameCheewat13 != null)
                    chewatname13.Text = selfdata.nameCheewat13;
                if (selfdata.nameCheewat14 != null)
                    chewatname14.Text = selfdata.nameCheewat14;
                if (selfdata.nameCheewat15 != null)
                    chewatname15.Text = selfdata.nameCheewat15;
                if (selfdata.nameCheewat16 != null)
                    chewatname16.Text = selfdata.nameCheewat16;
                if (selfdata.nameCheewat17 != null)
                    chewatname17.Text = selfdata.nameCheewat17;
                if (selfdata.nameCheewat18 != null)
                    chewatname18.Text = selfdata.nameCheewat18;
                if (selfdata.nameCheewat19 != null)
                    chewatname19.Text = selfdata.nameCheewat19;
                if (selfdata.nameCheewat20 != null)
                    chewatname20.Text = selfdata.nameCheewat20;

                if (selfdata.nameBehavior1 != null)
                    setnameb1.Text = selfdata.nameBehavior1;
                if (selfdata.nameBehavior2 != null)
                    setnameb2.Text = selfdata.nameBehavior2;
                if (selfdata.nameBehavior3 != null)
                    setnameb3.Text = selfdata.nameBehavior3;
                if (selfdata.nameBehavior4 != null)
                    setnameb4.Text = selfdata.nameBehavior4;
                if (selfdata.nameBehavior5 != null)
                    setnameb5.Text = selfdata.nameBehavior5;
                if (selfdata.nameBehavior6 != null)
                    setnameb6.Text = selfdata.nameBehavior6;
                if (selfdata.nameBehavior7 != null)
                    setnameb7.Text = selfdata.nameBehavior7;
                if (selfdata.nameBehavior8 != null)
                    setnameb8.Text = selfdata.nameBehavior8;
                if (selfdata.nameBehavior9 != null)
                    setnameb9.Text = selfdata.nameBehavior9;
                if (selfdata.nameBehavior10 != null)
                    setnameb10.Text = selfdata.nameBehavior10;

                if (selfdata.maxGrade1 != null)
                    maxS1.Text = selfdata.maxGrade1.ToString();
                if (selfdata.maxGrade2 != null)
                    maxS2.Text = selfdata.maxGrade2.ToString();
                if (selfdata.maxGrade3 != null)
                    maxS3.Text = selfdata.maxGrade3.ToString();
                if (selfdata.maxGrade4 != null)
                    maxS4.Text = selfdata.maxGrade4.ToString();
                if (selfdata.maxGrade5 != null)
                    maxS5.Text = selfdata.maxGrade5.ToString();
                if (selfdata.maxGrade6 != null)
                    maxS6.Text = selfdata.maxGrade6.ToString();
                if (selfdata.maxGrade7 != null)
                    maxS7.Text = selfdata.maxGrade7.ToString();
                if (selfdata.maxGrade8 != null)
                    maxS8.Text = selfdata.maxGrade8.ToString();
                if (selfdata.maxGrade9 != null)
                    maxS9.Text = selfdata.maxGrade9.ToString();
                if (selfdata.maxGrade10 != null)
                    maxS10.Text = selfdata.maxGrade10.ToString();
                if (selfdata.maxGrade11 != null)
                    maxS11.Text = selfdata.maxGrade11.ToString();
                if (selfdata.maxGrade12 != null)
                    maxS12.Text = selfdata.maxGrade12.ToString();
                if (selfdata.maxGrade13 != null)
                    maxS13.Text = selfdata.maxGrade13.ToString();
                if (selfdata.maxGrade14 != null)
                    maxS14.Text = selfdata.maxGrade14.ToString();
                if (selfdata.maxGrade15 != null)
                    maxS15.Text = selfdata.maxGrade15.ToString();
                if (selfdata.maxGrade16 != null)
                    maxS16.Text = selfdata.maxGrade16.ToString();
                if (selfdata.maxGrade17 != null)
                    maxS17.Text = selfdata.maxGrade17.ToString();
                if (selfdata.maxGrade18 != null)
                    maxS18.Text = selfdata.maxGrade18.ToString();
                if (selfdata.maxGrade19 != null)
                    maxS19.Text = selfdata.maxGrade19.ToString();
                if (selfdata.maxGrade20 != null)
                    maxS20.Text = selfdata.maxGrade20.ToString();

                if (selfdata.maxCheewat1 != null)
                    maxCW1.Text = selfdata.maxCheewat1.ToString();
                if (selfdata.maxCheewat2 != null)
                    maxCW2.Text = selfdata.maxCheewat2.ToString();
                if (selfdata.maxCheewat3 != null)
                    maxCW3.Text = selfdata.maxCheewat3.ToString();
                if (selfdata.maxCheewat4 != null)
                    maxCW4.Text = selfdata.maxCheewat4.ToString();
                if (selfdata.maxCheewat5 != null)
                    maxCW5.Text = selfdata.maxCheewat5.ToString();
                if (selfdata.maxCheewat6 != null)
                    maxCW6.Text = selfdata.maxCheewat6.ToString();
                if (selfdata.maxCheewat7 != null)
                    maxCW7.Text = selfdata.maxCheewat7.ToString();
                if (selfdata.maxCheewat8 != null)
                    maxCW8.Text = selfdata.maxCheewat8.ToString();
                if (selfdata.maxCheewat9 != null)
                    maxCW9.Text = selfdata.maxCheewat9.ToString();
                if (selfdata.maxCheewat10 != null)
                    maxCW10.Text = selfdata.maxCheewat10.ToString();
                

                if (selfdata.maxBehavior1 != null)
                    maxb1.Text = selfdata.maxBehavior1.ToString();
                if (selfdata.maxBehavior2 != null)
                    maxb2.Text = selfdata.maxBehavior2.ToString();
                if (selfdata.maxBehavior3 != null)
                    maxb3.Text = selfdata.maxBehavior3.ToString();
                if (selfdata.maxBehavior4 != null)
                    maxb4.Text = selfdata.maxBehavior4.ToString();
                if (selfdata.maxBehavior5 != null)
                    maxb5.Text = selfdata.maxBehavior5.ToString();
                if (selfdata.maxBehavior6 != null)
                    maxb6.Text = selfdata.maxBehavior6.ToString();
                if (selfdata.maxBehavior7 != null)
                    maxb7.Text = selfdata.maxBehavior7.ToString();
                if (selfdata.maxBehavior8 != null)
                    maxb8.Text = selfdata.maxBehavior8.ToString();
               
            }



            int sorttype1int = 0;
            int sorttype1txt = 0;
            int sorttype2 = 0;
            foreach (var data in _db.TUsers.Where(w => w.nTermSubLevel2 == idlv2n && w.cDel == null && w.SchoolID == userData.CompanyID))
            {
                var TUserMaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0" && w.nSystemID == data.sID).FirstOrDefault();
                if (TUserMaster != null)
                {

                    student = new studentlist2();
                    student.toggleOn = true;
                    student.number = number.ToString();
                    student.sID = data.sID;
                    if (mode == "EN")
                    {
                        if (data.sStudentNameEN != null && data.sStudentLastEN != null &&
                            data.sStudentNameEN != "" && data.sStudentLastEN != "")
                            student.sName = data.sStudentNameEN + " " + data.sStudentLastEN;
                        else student.sName = data.sName + " " + data.sLastname;
                    }
                    else
                        student.sName = data.sName + " " + data.sLastname;

                    int n;
                    bool isNumeric = int.TryParse(data.sStudentID, out n);
                    if (isNumeric == true)
                    {
                        sorttype1int = sorttype1int + 1;
                        student.sort1int = Int32.Parse(data.sStudentID);
                        student.sort1txt = data.sStudentID;
                        student.sort2 = 999999;
                    }
                    else if (data.sStudentID == null || data.sStudentID == "")
                    {
                        sorttype1int = sorttype1int + 1;
                        student.sort1int = 0;
                        student.sort1txt = "";
                        student.sort2 = 999999;
                    }
                    else
                    {
                        sorttype1txt = sorttype1txt + 1;
                        student.sort1txt = data.sStudentID;
                        student.sort2 = 999999;
                    }

                    if (data.nStudentNumber != null)
                    {
                        sorttype2 = sorttype2 + 1;
                        student.sort2 = data.nStudentNumber;
                    }



                    var data2 = _dbGrade.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID && w.SchoolID == userData.CompanyID).FirstOrDefault();
                    if (data2 != null)
                    {

                        var data3 = _dbGrade.GetGradeDetailView(userData.CompanyID, data2.nGradeId, data.sID).FirstOrDefault();
                        if (data3 != null)
                        {
                            if (data3.scoreGrade1 != null && data3.scoreGrade1 != "")
                                student.txtg1 = data3.scoreGrade1.ToString();
                            if (data3.scoreGrade2 != null && data3.scoreGrade2 != "")
                                student.txtg2 = data3.scoreGrade2.ToString();
                            if (data3.scoreGrade3 != null && data3.scoreGrade3 != "")
                                student.txtg3 = data3.scoreGrade3.ToString();
                            if (data3.scoreGrade4 != null && data3.scoreGrade4 != "")
                                student.txtg4 = data3.scoreGrade4.ToString();
                            if (data3.scoreGrade5 != null && data3.scoreGrade5 != "")
                                student.txtg5 = data3.scoreGrade5.ToString();
                            if (data3.scoreGrade6 != null && data3.scoreGrade6 != "")
                                student.txtg6 = data3.scoreGrade6.ToString();
                            if (data3.scoreGrade7 != null && data3.scoreGrade7 != "")
                                student.txtg7 = data3.scoreGrade7.ToString();
                            if (data3.scoreGrade8 != null && data3.scoreGrade8 != "")
                                student.txtg8 = data3.scoreGrade8.ToString();
                            if (data3.scoreGrade9 != null && data3.scoreGrade9 != "")
                                student.txtg9 = data3.scoreGrade9.ToString();
                            if (data3.scoreGrade10 != null && data3.scoreGrade10 != "")
                                student.txtg10 = data3.scoreGrade10.ToString();
                            if (data3.scoreGrade11 != null && data3.scoreGrade11 != "")
                                student.txtg11 = data3.scoreGrade11.ToString();
                            if (data3.scoreGrade12 != null && data3.scoreGrade12 != "")
                                student.txtg12 = data3.scoreGrade12.ToString();
                            if (data3.scoreGrade13 != null && data3.scoreGrade13 != "")
                                student.txtg13 = data3.scoreGrade13.ToString();
                            if (data3.scoreGrade14 != null && data3.scoreGrade14 != "")
                                student.txtg14 = data3.scoreGrade14.ToString();
                            if (data3.scoreGrade15 != null && data3.scoreGrade15 != "")
                                student.txtg15 = data3.scoreGrade15.ToString();
                            if (data3.scoreGrade16 != null && data3.scoreGrade16 != "")
                                student.txtg16 = data3.scoreGrade16.ToString();
                            if (data3.scoreGrade17 != null && data3.scoreGrade17 != "")
                                student.txtg17 = data3.scoreGrade17.ToString();
                            if (data3.scoreGrade18 != null && data3.scoreGrade18 != "")
                                student.txtg18 = data3.scoreGrade18.ToString();
                            if (data3.scoreGrade19 != null && data3.scoreGrade19 != "")
                                student.txtg19 = data3.scoreGrade19.ToString();
                            if (data3.scoreGrade20 != null && data3.scoreGrade20 != "")
                                student.txtg20 = data3.scoreGrade20.ToString();

                            if (data3.scoreCheewat1 != null && data3.scoreCheewat1 != "")
                                student.txtchewut1 = data3.scoreCheewat1.ToString();
                            if (data3.scoreCheewat2 != null && data3.scoreCheewat2 != "")
                                student.txtchewut2 = data3.scoreCheewat2.ToString();
                            if (data3.scoreCheewat3 != null && data3.scoreCheewat3 != "")
                                student.txtchewut3 = data3.scoreCheewat3.ToString();
                            if (data3.scoreCheewat4 != null && data3.scoreCheewat4 != "")
                                student.txtchewut4 = data3.scoreCheewat4.ToString();
                            if (data3.scoreCheewat5 != null && data3.scoreCheewat5 != "")
                                student.txtchewut5 = data3.scoreCheewat5.ToString();
                            if (data3.scoreCheewat6 != null && data3.scoreCheewat6 != "")
                                student.txtchewut6 = data3.scoreCheewat6.ToString();
                            if (data3.scoreCheewat7 != null && data3.scoreCheewat7 != "")
                                student.txtchewut7 = data3.scoreCheewat7.ToString();
                            if (data3.scoreCheewat8 != null && data3.scoreCheewat8 != "")
                                student.txtchewut8 = data3.scoreCheewat8.ToString();
                            if (data3.scoreCheewat9 != null && data3.scoreCheewat9 != "")
                                student.txtchewut9 = data3.scoreCheewat9.ToString();
                            if (data3.scoreCheewat10 != null && data3.scoreCheewat10 != "")
                                student.txtchewut10 = data3.scoreCheewat10.ToString();
                            if (data3.scoreCheewat11 != null && data3.scoreCheewat11 != "")
                                student.txtchewut11 = data3.scoreCheewat11.ToString();
                            if (data3.scoreCheewat12 != null && data3.scoreCheewat12 != "")
                                student.txtchewut12 = data3.scoreCheewat12.ToString();
                            if (data3.scoreCheewat13 != null && data3.scoreCheewat13 != "")
                                student.txtchewut13 = data3.scoreCheewat13.ToString();
                            if (data3.scoreCheewat14 != null && data3.scoreCheewat14 != "")
                                student.txtchewut14 = data3.scoreCheewat14.ToString();
                            if (data3.scoreCheewat15 != null && data3.scoreCheewat15 != "")
                                student.txtchewut15 = data3.scoreCheewat15.ToString();
                            if (data3.scoreCheewat16 != null && data3.scoreCheewat16 != "")
                                student.txtchewut16 = data3.scoreCheewat16.ToString();
                            if (data3.scoreCheewat17 != null && data3.scoreCheewat17 != "")
                                student.txtchewut17 = data3.scoreCheewat17.ToString();
                            if (data3.scoreCheewat18 != null && data3.scoreCheewat18 != "")
                                student.txtchewut18 = data3.scoreCheewat18.ToString();
                            if (data3.scoreCheewat19 != null && data3.scoreCheewat19 != "")
                                student.txtchewut19 = data3.scoreCheewat19.ToString();
                            if (data3.scoreCheewat20 != null && data3.scoreCheewat20 != "")
                                student.txtchewut20 = data3.scoreCheewat20.ToString();

                            if (data3.scoreMidTerm != null && data3.scoreMidTerm != "")
                                student.txtgmid = data3.scoreMidTerm.ToString();
                            if (data3.scoreFinalTerm != null && data3.scoreFinalTerm != "")
                                student.txtglate = data3.scoreFinalTerm.ToString();
                            if (data3.getBehaviorLabel != null && data3.getBehaviorLabel != "")
                                student.txtGoodBehavior = data3.getBehaviorLabel.ToString();
                            if (data3.getReadWrite != null && data3.getReadWrite != "")
                                student.txtGoodReading = data3.getReadWrite.ToString();
                            if (data3.getSpecial != null && data3.getSpecial != "")
                                student.txtSpecial = data3.getSpecial.ToString();

                            if (data3.scoreBehavior1 != null && data3.scoreBehavior1 != "")
                                student.txtb1 = data3.scoreBehavior1.ToString();
                            if (data3.scoreBehavior2 != null && data3.scoreBehavior2 != "")
                                student.txtb2 = data3.scoreBehavior2.ToString();
                            if (data3.scoreBehavior3 != null && data3.scoreBehavior3 != "")
                                student.txtb3 = data3.scoreBehavior3.ToString();
                            if (data3.scoreBehavior4 != null && data3.scoreBehavior4 != "")
                                student.txtb4 = data3.scoreBehavior4.ToString();
                            if (data3.scoreBehavior5 != null && data3.scoreBehavior5 != "")
                                student.txtb5 = data3.scoreBehavior5.ToString();
                            if (data3.scoreBehavior6 != null && data3.scoreBehavior6 != "")
                                student.txtb6 = data3.scoreBehavior6.ToString();
                            if (data3.scoreBehavior7 != null && data3.scoreBehavior7 != "")
                                student.txtb7 = data3.scoreBehavior7.ToString();
                            if (data3.scoreBehavior8 != null && data3.scoreBehavior8 != "")
                                student.txtb8 = data3.scoreBehavior8.ToString();
                            if (data3.scoreBehavior9 != null && data3.scoreBehavior9 != "")
                                student.txtb9 = data3.scoreBehavior9.ToString();
                            if (data3.scoreBehavior10 != null && data3.scoreBehavior10 != "")
                                student.txtb10 = data3.scoreBehavior10.ToString();
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
                newSortList = studentlist.OrderBy(x => x.sort2).ToList();

            int counter = 1;


            foreach (var a in newSortList)
            {
                a.number = counter.ToString();
                counter = counter + 1;
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
         
        }

        private List<studentlist2> OpenData2()
        {
            JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade());
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();

            string sEntities = Session["sEntities"].ToString();
            var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

            string id = Request.QueryString["id"];
            string year = Request.QueryString["year"];
            string userterm = Request.QueryString["term"];
            string idlv2 = Request.QueryString["idlv2"];
            string mode = Request.QueryString["mode"];

            int? useryear = Int32.Parse(year);
            int? idlv2n = Int32.Parse(idlv2);
            string username = "";
            int? ntermsublv2 = 0;
            int nyear = 0;
            string nterm = "";
            int ntermtable = 0;


            foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear && w.SchoolID == userData.CompanyID))
            {
                nyear = ff.nYear;
            }

            foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.SchoolID == userData.CompanyID && w.cDel == null))
            {
                nterm = ee.nTerm;
            }



            int number = 1;
            studentlist2 student = new studentlist2();
            List<studentlist2> studentlist = new List<studentlist2>();



            int sorttype1int = 0;
            int sorttype1txt = 0;
            int sorttype2 = 0;
            foreach (var data in _db.TUsers.Where(w => w.nTermSubLevel2 == idlv2n && w.cDel == null && w.SchoolID == userData.CompanyID))
            {
                var TUserMaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0" && w.nSystemID == data.sID).FirstOrDefault();
                if (TUserMaster != null)
                {

                    student = new studentlist2();
                    student.toggleOn = true;
                    student.number = number.ToString();
                    student.sID = data.sID;
                    student.studentid = data.sStudentID;
                    if (mode == "EN")
                    {
                        if (data.sStudentNameEN != null && data.sStudentLastEN != null &&
                            data.sStudentNameEN != "" && data.sStudentLastEN != "")
                            student.sName = data.sStudentNameEN + " " + data.sStudentLastEN;
                        else student.sName = data.sName + " " + data.sLastname;
                    }
                    else
                        student.sName = data.sName + " " + data.sLastname;

                    int n;
                    bool isNumeric = int.TryParse(data.sStudentID, out n);
                    if (isNumeric == true)
                    {
                        sorttype1int = sorttype1int + 1;
                        student.sort1int = Int32.Parse(data.sStudentID);
                        student.sort1txt = data.sStudentID;
                        student.sort2 = 999999;
                    }
                    else if (data.sStudentID == null || data.sStudentID == "")
                    {
                        sorttype1int = sorttype1int + 1;
                        student.sort1int = 0;
                        student.sort1txt = "";
                        student.sort2 = 999999;
                    }
                    else
                    {
                        sorttype1txt = sorttype1txt + 1;
                        student.sort1txt = data.sStudentID;
                        student.sort2 = 999999;
                    }

                    if (data.nStudentNumber != null)
                    {
                        sorttype2 = sorttype2 + 1;
                        student.sort2 = data.nStudentNumber;
                    }



                    var data2 = _dbGrade.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
                    if (data2 != null)
                    {

                        var data3 = _dbGrade.GetGradeDetailView(userData.CompanyID, data2.nGradeId, data.sID).FirstOrDefault();
                        if (data3 != null)
                        {
                            if (data3.scoreGrade1 != null && data3.scoreGrade1 != "")
                                student.txtg1 = data3.scoreGrade1.ToString();
                            if (data3.scoreGrade2 != null && data3.scoreGrade2 != "")
                                student.txtg2 = data3.scoreGrade2.ToString();
                            if (data3.scoreGrade3 != null && data3.scoreGrade3 != "")
                                student.txtg3 = data3.scoreGrade3.ToString();
                            if (data3.scoreGrade4 != null && data3.scoreGrade4 != "")
                                student.txtg4 = data3.scoreGrade4.ToString();
                            if (data3.scoreGrade5 != null && data3.scoreGrade5 != "")
                                student.txtg5 = data3.scoreGrade5.ToString();
                            if (data3.scoreGrade6 != null && data3.scoreGrade6 != "")
                                student.txtg6 = data3.scoreGrade6.ToString();
                            if (data3.scoreGrade7 != null && data3.scoreGrade7 != "")
                                student.txtg7 = data3.scoreGrade7.ToString();
                            if (data3.scoreGrade8 != null && data3.scoreGrade8 != "")
                                student.txtg8 = data3.scoreGrade8.ToString();
                            if (data3.scoreGrade9 != null && data3.scoreGrade9 != "")
                                student.txtg9 = data3.scoreGrade9.ToString();
                            if (data3.scoreGrade10 != null && data3.scoreGrade10 != "")
                                student.txtg10 = data3.scoreGrade10.ToString();
                            if (data3.scoreGrade11 != null && data3.scoreGrade11 != "")
                                student.txtg11 = data3.scoreGrade11.ToString();
                            if (data3.scoreGrade12 != null && data3.scoreGrade12 != "")
                                student.txtg12 = data3.scoreGrade12.ToString();
                            if (data3.scoreGrade13 != null && data3.scoreGrade13 != "")
                                student.txtg13 = data3.scoreGrade13.ToString();
                            if (data3.scoreGrade14 != null && data3.scoreGrade14 != "")
                                student.txtg14 = data3.scoreGrade14.ToString();
                            if (data3.scoreGrade15 != null && data3.scoreGrade15 != "")
                                student.txtg15 = data3.scoreGrade15.ToString();
                            if (data3.scoreGrade16 != null && data3.scoreGrade16 != "")
                                student.txtg16 = data3.scoreGrade16.ToString();
                            if (data3.scoreGrade17 != null && data3.scoreGrade17 != "")
                                student.txtg17 = data3.scoreGrade17.ToString();
                            if (data3.scoreGrade18 != null && data3.scoreGrade18 != "")
                                student.txtg18 = data3.scoreGrade18.ToString();
                            if (data3.scoreGrade19 != null && data3.scoreGrade19 != "")
                                student.txtg19 = data3.scoreGrade19.ToString();
                            if (data3.scoreGrade20 != null && data3.scoreGrade20 != "")
                                student.txtg20 = data3.scoreGrade20.ToString();

                            if (data3.scoreCheewat1 != null && data3.scoreCheewat1 != "")
                                student.txtchewut1 = data3.scoreCheewat1.ToString();
                            if (data3.scoreCheewat2 != null && data3.scoreCheewat2 != "")
                                student.txtchewut2 = data3.scoreCheewat2.ToString();
                            if (data3.scoreCheewat3 != null && data3.scoreCheewat3 != "")
                                student.txtchewut3 = data3.scoreCheewat3.ToString();
                            if (data3.scoreCheewat4 != null && data3.scoreCheewat4 != "")
                                student.txtchewut4 = data3.scoreCheewat4.ToString();
                            if (data3.scoreCheewat5 != null && data3.scoreCheewat5 != "")
                                student.txtchewut5 = data3.scoreCheewat5.ToString();
                            if (data3.scoreCheewat6 != null && data3.scoreCheewat6 != "")
                                student.txtchewut6 = data3.scoreCheewat6.ToString();
                            if (data3.scoreCheewat7 != null && data3.scoreCheewat7 != "")
                                student.txtchewut7 = data3.scoreCheewat7.ToString();
                            if (data3.scoreCheewat8 != null && data3.scoreCheewat8 != "")
                                student.txtchewut8 = data3.scoreCheewat8.ToString();
                            if (data3.scoreCheewat9 != null && data3.scoreCheewat9 != "")
                                student.txtchewut9 = data3.scoreCheewat9.ToString();
                            if (data3.scoreCheewat10 != null && data3.scoreCheewat10 != "")
                                student.txtchewut10 = data3.scoreCheewat10.ToString();
                            if (data3.scoreCheewat11 != null && data3.scoreCheewat11 != "")
                                student.txtchewut11 = data3.scoreCheewat11.ToString();
                            if (data3.scoreCheewat12 != null && data3.scoreCheewat12 != "")
                                student.txtchewut12 = data3.scoreCheewat12.ToString();
                            if (data3.scoreCheewat13 != null && data3.scoreCheewat13 != "")
                                student.txtchewut13 = data3.scoreCheewat13.ToString();
                            if (data3.scoreCheewat14 != null && data3.scoreCheewat14 != "")
                                student.txtchewut14 = data3.scoreCheewat14.ToString();
                            if (data3.scoreCheewat15 != null && data3.scoreCheewat15 != "")
                                student.txtchewut15 = data3.scoreCheewat15.ToString();
                            if (data3.scoreCheewat16 != null && data3.scoreCheewat16 != "")
                                student.txtchewut16 = data3.scoreCheewat16.ToString();
                            if (data3.scoreCheewat17 != null && data3.scoreCheewat17 != "")
                                student.txtchewut17 = data3.scoreCheewat17.ToString();
                            if (data3.scoreCheewat18 != null && data3.scoreCheewat18 != "")
                                student.txtchewut18 = data3.scoreCheewat18.ToString();
                            if (data3.scoreCheewat19 != null && data3.scoreCheewat19 != "")
                                student.txtchewut19 = data3.scoreCheewat19.ToString();
                            if (data3.scoreCheewat20 != null && data3.scoreCheewat20 != "")
                                student.txtchewut20 = data3.scoreCheewat20.ToString();

                            if (data3.scoreMidTerm != null && data3.scoreMidTerm != "")
                                student.txtgmid = data3.scoreMidTerm.ToString();
                            if (data3.scoreFinalTerm != null && data3.scoreFinalTerm != "")
                                student.txtglate = data3.scoreFinalTerm.ToString();
                            if (data3.getBehaviorLabel != null && data3.getBehaviorLabel != "")
                                student.txtGoodBehavior = data3.getBehaviorLabel.ToString();
                            if (data3.getReadWrite != null && data3.getReadWrite != "")
                                student.txtGoodReading = data3.getReadWrite.ToString();
                            if (data3.getSpecial != null && data3.getSpecial != "")
                                student.txtSpecial = data3.getSpecial.ToString();

                            if (data3.scoreBehavior1 != null && data3.scoreBehavior1 != "")
                                student.txtb1 = data3.scoreBehavior1.ToString();
                            if (data3.scoreBehavior2 != null && data3.scoreBehavior2 != "")
                                student.txtb2 = data3.scoreBehavior2.ToString();
                            if (data3.scoreBehavior3 != null && data3.scoreBehavior3 != "")
                                student.txtb3 = data3.scoreBehavior3.ToString();
                            if (data3.scoreBehavior4 != null && data3.scoreBehavior4 != "")
                                student.txtb4 = data3.scoreBehavior4.ToString();
                            if (data3.scoreBehavior5 != null && data3.scoreBehavior5 != "")
                                student.txtb5 = data3.scoreBehavior5.ToString();
                            if (data3.scoreBehavior6 != null && data3.scoreBehavior6 != "")
                                student.txtb6 = data3.scoreBehavior6.ToString();
                            if (data3.scoreBehavior7 != null && data3.scoreBehavior7 != "")
                                student.txtb7 = data3.scoreBehavior7.ToString();
                            if (data3.scoreBehavior8 != null && data3.scoreBehavior8 != "")
                                student.txtb8 = data3.scoreBehavior8.ToString();
                            if (data3.scoreBehavior9 != null && data3.scoreBehavior9 != "")
                                student.txtb9 = data3.scoreBehavior9.ToString();
                            if (data3.scoreBehavior10 != null && data3.scoreBehavior10 != "")
                                student.txtb10 = data3.scoreBehavior10.ToString();

                            student.getgrade = data3.getGradeLabel;
                            student.totalquiz = data3.getQuiz100;
                            student.total100 = data3.getScore100;
                            student.totalfinal = data3.getFinal100;
                            student.totalmid = data3.getMid100;
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
                newSortList = studentlist.OrderBy(x => x.sort2).ToList();

            int counter = 1;


            foreach (var a in newSortList)
            {
                a.number = counter.ToString();
                counter = counter + 1;
            }

            return newSortList;
        }

        public string comeorskip(DateTime? day, int check, int userid, List<TLogLearnTimeScan> loglist, List<TLeaveLetter> leavelist)
        {

            string come = "0";


            var d1 = loglist.Where(w => w.LogLearnDate == day && w.sID == userid && w.sScheduleID == check).FirstOrDefault();
            if (d1 != null)
            {
                if (d1.LogLearnScanStatus.Trim() == "1" || d1.LogLearnScanStatus.Trim() == "0")
                    come = "1";
                else if (d1.LogLearnScanStatus.Trim() == "4")
                    come = "4";
                else if (d1.LogLearnScanStatus.Trim() == "5")
                    come = "3";
                else if (d1.LogLearnScanStatus.Trim() == "6")
                    come = "5";
            }


            if (come == "0")
            {
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

            return come;
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
            public int? sort2 { get; set; }
            public int sort1int { get; set; }
            public string sort1txt { get; set; }
            public string getgrade { get; set; }
            public string totalquiz { get; set; }
            public string total100 { get; set; }
            public string totalfinal { get; set; }
            public string totalmid { get; set; }
            public string studentid { get; set; }
        }
    }

}