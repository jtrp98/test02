using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FingerprintPayment.grade
{
    public partial class BP5attendance : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                dgd.RowDataBound += new GridViewRowEventHandler(dgd_RowDataBound);

                btnSave.Click += new EventHandler(btnSave_Click);
                btnCancle.Click += new EventHandler(btnCancle_Click);
                string id = Request.QueryString["id"];
                var data = _db.TPlanes.Where(w => w.sPlaneID.ToString() == id && w.SchoolID == userData.CompanyID).FirstOrDefault();
                planid.Text = data.sPlaneID.ToString();
                planname.Text = data.sPlaneName;
                Year.Text = Request.QueryString["year"];
                Term.Text = Request.QueryString["term"];

                if (!IsPostBack)
                {
                    OpenData();
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
            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];
            string id = Request.QueryString["id"];

            Response.Redirect("Webform3.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term + "&id=" + id);
        }


        void btnCancle_Click(object sender, EventArgs e)
        {

            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];

            Response.Redirect("GradeRoomList.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term);
        }

        private void OpenData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                

                string sEntities = Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                string id = Request.QueryString["id"];
                string year = Request.QueryString["year"];
                string userterm = Request.QueryString["term"];
                string idlv2 = Request.QueryString["idlv2"];

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

                DateTime? daystart = DateTime.Now;
                DateTime? dayend = DateTime.Now;

                foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.SchoolID == userData.CompanyID && w.cDel == null))
                {
                    nterm = ee.nTerm;
                    daystart = ee.dStart;
                    dayend = ee.dEnd;
                }

                var ttable = _db.TTermTimeTables.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
                int countday = 0;

                int number = 1;
                studentlist5 student = new studentlist5();
                List<studentlist5> studentlist = new List<studentlist5>();

                DateTime? day1 = daystart;
                DateTime? day2 = daystart.Value.AddDays(1);
                DateTime? day3 = daystart.Value.AddDays(2);
                DateTime? day4 = daystart.Value.AddDays(3);
                DateTime? day5 = daystart.Value.AddDays(4);
                DateTime? day6 = daystart.Value.AddDays(5);
                DateTime? day7 = daystart.Value.AddDays(6);
                DateTime? day8 = daystart.Value.AddDays(7);
                DateTime? day9 = daystart.Value.AddDays(8);
                DateTime? day10 = daystart.Value.AddDays(9);
                DateTime? day11 = daystart.Value.AddDays(10);
                DateTime? day12 = daystart.Value.AddDays(11);
                DateTime? day13 = daystart.Value.AddDays(12);
                DateTime? day14 = daystart.Value.AddDays(13);
                DateTime? day15 = daystart.Value.AddDays(14);
                DateTime? day16 = daystart.Value.AddDays(15);
                DateTime? day17 = daystart.Value.AddDays(16);
                DateTime? day18 = daystart.Value.AddDays(17);
                DateTime? day19 = daystart.Value.AddDays(18);
                DateTime? day20 = daystart.Value.AddDays(19);
                DateTime? day21 = daystart.Value.AddDays(20);
                DateTime? day22 = daystart.Value.AddDays(21);
                DateTime? day23 = daystart.Value.AddDays(22);
                DateTime? day24 = daystart.Value.AddDays(23);
                DateTime? day25 = daystart.Value.AddDays(24);
                DateTime? day26 = daystart.Value.AddDays(25);
                DateTime? day27 = daystart.Value.AddDays(26);
                DateTime? day28 = daystart.Value.AddDays(27);
                DateTime? day29 = daystart.Value.AddDays(28);
                DateTime? day30 = daystart.Value.AddDays(29);
                DateTime? day31 = daystart.Value.AddDays(30);
                DateTime? day32 = daystart.Value.AddDays(31);
                DateTime? day33 = daystart.Value.AddDays(32);
                DateTime? day34 = daystart.Value.AddDays(33);
                DateTime? day35 = daystart.Value.AddDays(34);
                DateTime? day36 = daystart.Value.AddDays(35);
                DateTime? day37 = daystart.Value.AddDays(36);
                DateTime? day38 = daystart.Value.AddDays(37);
                DateTime? day39 = daystart.Value.AddDays(38);
                DateTime? day40 = daystart.Value.AddDays(39);
                DateTime? day41 = daystart.Value.AddDays(40);
                DateTime? day42 = daystart.Value.AddDays(41);
                DateTime? day43 = daystart.Value.AddDays(42);
                DateTime? day44 = daystart.Value.AddDays(43);
                DateTime? day45 = daystart.Value.AddDays(44);
                DateTime? day46 = daystart.Value.AddDays(45);
                DateTime? day47 = daystart.Value.AddDays(46);
                DateTime? day48 = daystart.Value.AddDays(47);
                DateTime? day49 = daystart.Value.AddDays(48);
                DateTime? day50 = daystart.Value.AddDays(49);
                DateTime? day51 = daystart.Value.AddDays(50);
                DateTime? day52 = daystart.Value.AddDays(51);
                DateTime? day53 = daystart.Value.AddDays(52);
                DateTime? day54 = daystart.Value.AddDays(53);
                DateTime? day55 = daystart.Value.AddDays(54);
                DateTime? day56 = daystart.Value.AddDays(55);
                DateTime? day57 = daystart.Value.AddDays(56);
                DateTime? day58 = daystart.Value.AddDays(57);
                DateTime? day59 = daystart.Value.AddDays(58);
                DateTime? day60 = daystart.Value.AddDays(59);
                DateTime? day61 = daystart.Value.AddDays(60);
                DateTime? day62 = daystart.Value.AddDays(61);
                DateTime? day63 = daystart.Value.AddDays(62);
                DateTime? day64 = daystart.Value.AddDays(63);
                DateTime? day65 = daystart.Value.AddDays(64);
                DateTime? day66 = daystart.Value.AddDays(65);
                DateTime? day67 = daystart.Value.AddDays(66);
                DateTime? day68 = daystart.Value.AddDays(67);
                DateTime? day69 = daystart.Value.AddDays(68);
                DateTime? day70 = daystart.Value.AddDays(69);
                DateTime? day71 = daystart.Value.AddDays(70);
                DateTime? day72 = daystart.Value.AddDays(71);
                DateTime? day73 = daystart.Value.AddDays(72);
                DateTime? day74 = daystart.Value.AddDays(73);
                DateTime? day75 = daystart.Value.AddDays(74);
                DateTime? day76 = daystart.Value.AddDays(75);
                DateTime? day77 = daystart.Value.AddDays(76);
                DateTime? day78 = daystart.Value.AddDays(77);
                DateTime? day79 = daystart.Value.AddDays(78);
                DateTime? day80 = daystart.Value.AddDays(79);
                DateTime? day81 = daystart.Value.AddDays(80);
                DateTime? day82 = daystart.Value.AddDays(81);
                DateTime? day83 = daystart.Value.AddDays(82);
                DateTime? day84 = daystart.Value.AddDays(83);
                DateTime? day85 = daystart.Value.AddDays(84);
                DateTime? day86 = daystart.Value.AddDays(85);
                DateTime? day87 = daystart.Value.AddDays(86);
                DateTime? day88 = daystart.Value.AddDays(87);
                DateTime? day89 = daystart.Value.AddDays(88);
                DateTime? day90 = daystart.Value.AddDays(89);
                DateTime? day91 = daystart.Value.AddDays(90);
                DateTime? day92 = daystart.Value.AddDays(91);
                DateTime? day93 = daystart.Value.AddDays(92);
                DateTime? day94 = daystart.Value.AddDays(93);
                DateTime? day95 = daystart.Value.AddDays(94);
                DateTime? day96 = daystart.Value.AddDays(95);
                DateTime? day97 = daystart.Value.AddDays(96);
                DateTime? day98 = daystart.Value.AddDays(97);
                DateTime? day99 = daystart.Value.AddDays(98);
                DateTime? day100 = daystart.Value.AddDays(99);
                DateTime? day101 = daystart.Value.AddDays(100);
                DateTime? day102 = daystart.Value.AddDays(101);
                DateTime? day103 = daystart.Value.AddDays(102);
                DateTime? day104 = daystart.Value.AddDays(103);
                DateTime? day105 = daystart.Value.AddDays(104);
                DateTime? day106 = daystart.Value.AddDays(105);
                DateTime? day107 = daystart.Value.AddDays(106);
                DateTime? day108 = daystart.Value.AddDays(107);
                DateTime? day109 = daystart.Value.AddDays(108);
                DateTime? day110 = daystart.Value.AddDays(109);
                DateTime? day111 = daystart.Value.AddDays(110);
                DateTime? day112 = daystart.Value.AddDays(111);
                DateTime? day113 = daystart.Value.AddDays(112);
                DateTime? day114 = daystart.Value.AddDays(113);
                DateTime? day115 = daystart.Value.AddDays(114);
                DateTime? day116 = daystart.Value.AddDays(115);
                DateTime? day117 = daystart.Value.AddDays(116);
                DateTime? day118 = daystart.Value.AddDays(117);
                DateTime? day119 = daystart.Value.AddDays(118);
                DateTime? day120 = daystart.Value.AddDays(119);
                DateTime? day121 = daystart.Value.AddDays(120);
                DateTime? day122 = daystart.Value.AddDays(121);
                DateTime? day123 = daystart.Value.AddDays(122);
                DateTime? day124 = daystart.Value.AddDays(123);
                DateTime? day125 = daystart.Value.AddDays(124);
                DateTime? day126 = daystart.Value.AddDays(125);
                DateTime? day127 = daystart.Value.AddDays(126);
                DateTime? day128 = daystart.Value.AddDays(127);
                DateTime? day129 = daystart.Value.AddDays(128);
                DateTime? day130 = daystart.Value.AddDays(129);
                DateTime? day131 = daystart.Value.AddDays(130);
                DateTime? day132 = daystart.Value.AddDays(131);
                DateTime? day133 = daystart.Value.AddDays(132);
                DateTime? day134 = daystart.Value.AddDays(133);
                DateTime? day135 = daystart.Value.AddDays(134);
                DateTime? day136 = daystart.Value.AddDays(135);
                DateTime? day137 = daystart.Value.AddDays(136);
                DateTime? day138 = daystart.Value.AddDays(137);
                DateTime? day139 = daystart.Value.AddDays(138);
                DateTime? day140 = daystart.Value.AddDays(139);

                TLogLearnTimeScan log = new TLogLearnTimeScan();
                List<TLogLearnTimeScan> loglist = new List<TLogLearnTimeScan>();

                List<int> schedulidlist = new List<int>();

                foreach (var aa in _db.TSchedules.Where(w => w.nTermTable == ttable.nTermTable && w.sPlaneID.ToString() == id && w.SchoolID == userData.CompanyID))
                {
                    countday = countday + 1;
                    schedulidlist.Add(aa.sScheduleID);
                }

                foreach (var check in schedulidlist)
                {

                    foreach (var d1 in _db.TLogLearnTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.LogLearnDate > day1 && w.LogLearnDate <= day140 && w.sScheduleID == check && (w.LogLearnType == "1" || w.LogLearnType == "0")))
                    {
                        log = new TLogLearnTimeScan();
                        log = d1;
                        loglist.Add(log);
                    }
                }


                foreach (var data in _db.TUser.Where(w => w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID))
                {
                    int totalcome = 0;
                    int totalskip = 0;

                    student = new studentlist5();
                    student.number = number.ToString();
                    student.sID = data.sID;
                    student.sName = data.sName + " " + data.sLastname;

                    student.week1_1 = comeorskip(day1, schedulidlist, data.sID, dayend, loglist);
                    if (student.week1_1 == "1") totalcome = totalcome + 1;
                    if (student.week1_1 == "0") totalskip = totalskip + 1;
                    student.week1_2 = comeorskip(day2, schedulidlist, data.sID, dayend, loglist);
                    if (student.week1_2 == "1") totalcome = totalcome + 1;
                    if (student.week1_2 == "0") totalskip = totalskip + 1;
                    student.week1_3 = comeorskip(day3, schedulidlist, data.sID, dayend, loglist);
                    if (student.week1_3 == "1") totalcome = totalcome + 1;
                    if (student.week1_3 == "0") totalskip = totalskip + 1;
                    student.week1_4 = comeorskip(day4, schedulidlist, data.sID, dayend, loglist);
                    if (student.week1_4 == "1") totalcome = totalcome + 1;
                    if (student.week1_4 == "0") totalskip = totalskip + 1;
                    student.week1_5 = comeorskip(day5, schedulidlist, data.sID, dayend, loglist);
                    if (student.week1_5 == "1") totalcome = totalcome + 1;
                    if (student.week1_5 == "0") totalskip = totalskip + 1;
                    student.week1_6 = comeorskip(day6, schedulidlist, data.sID, dayend, loglist);
                    if (student.week1_6 == "1") totalcome = totalcome + 1;
                    if (student.week1_6 == "0") totalskip = totalskip + 1;
                    student.week1_7 = comeorskip(day7, schedulidlist, data.sID, dayend, loglist);
                    if (student.week1_7 == "1") totalcome = totalcome + 1;
                    if (student.week1_7 == "0") totalskip = totalskip + 1;

                    student.week2_1 = comeorskip(day8, schedulidlist, data.sID, dayend, loglist);
                    if (student.week2_1 == "1") totalcome = totalcome + 1;
                    if (student.week2_1 == "0") totalskip = totalskip + 1;
                    student.week2_2 = comeorskip(day9, schedulidlist, data.sID, dayend, loglist);
                    if (student.week2_2 == "1") totalcome = totalcome + 1;
                    if (student.week2_2 == "0") totalskip = totalskip + 1;
                    student.week2_3 = comeorskip(day10, schedulidlist, data.sID, dayend, loglist);
                    if (student.week2_3 == "1") totalcome = totalcome + 1;
                    if (student.week2_3 == "0") totalskip = totalskip + 1;
                    student.week2_4 = comeorskip(day11, schedulidlist, data.sID, dayend, loglist);
                    if (student.week2_4 == "1") totalcome = totalcome + 1;
                    if (student.week2_4 == "0") totalskip = totalskip + 1;
                    student.week2_5 = comeorskip(day12, schedulidlist, data.sID, dayend, loglist);
                    if (student.week2_5 == "1") totalcome = totalcome + 1;
                    if (student.week2_5 == "0") totalskip = totalskip + 1;
                    student.week2_6 = comeorskip(day13, schedulidlist, data.sID, dayend, loglist);
                    if (student.week2_6 == "1") totalcome = totalcome + 1;
                    if (student.week2_6 == "0") totalskip = totalskip + 1;
                    student.week2_7 = comeorskip(day14, schedulidlist, data.sID, dayend, loglist);
                    if (student.week2_7 == "1") totalcome = totalcome + 1;
                    if (student.week2_7 == "0") totalskip = totalskip + 1;

                    student.week3_1 = comeorskip(day15, schedulidlist, data.sID, dayend, loglist);
                    if (student.week3_1 == "1") totalcome = totalcome + 1;
                    if (student.week3_1 == "0") totalskip = totalskip + 1;
                    student.week3_2 = comeorskip(day16, schedulidlist, data.sID, dayend, loglist);
                    if (student.week3_2 == "1") totalcome = totalcome + 1;
                    if (student.week3_2 == "0") totalskip = totalskip + 1;
                    student.week3_3 = comeorskip(day17, schedulidlist, data.sID, dayend, loglist);
                    if (student.week3_3 == "1") totalcome = totalcome + 1;
                    if (student.week3_3 == "0") totalskip = totalskip + 1;
                    student.week3_4 = comeorskip(day18, schedulidlist, data.sID, dayend, loglist);
                    if (student.week3_4 == "1") totalcome = totalcome + 1;
                    if (student.week3_4 == "0") totalskip = totalskip + 1;
                    student.week3_5 = comeorskip(day19, schedulidlist, data.sID, dayend, loglist);
                    if (student.week3_5 == "1") totalcome = totalcome + 1;
                    if (student.week3_5 == "0") totalskip = totalskip + 1;
                    student.week3_6 = comeorskip(day20, schedulidlist, data.sID, dayend, loglist);
                    if (student.week3_6 == "1") totalcome = totalcome + 1;
                    if (student.week3_6 == "0") totalskip = totalskip + 1;
                    student.week3_7 = comeorskip(day21, schedulidlist, data.sID, dayend, loglist);
                    if (student.week3_7 == "1") totalcome = totalcome + 1;
                    if (student.week3_7 == "0") totalskip = totalskip + 1;

                    student.week4_1 = comeorskip(day22, schedulidlist, data.sID, dayend, loglist);
                    if (student.week4_1 == "1") totalcome = totalcome + 1;
                    if (student.week4_1 == "0") totalskip = totalskip + 1;
                    student.week4_2 = comeorskip(day23, schedulidlist, data.sID, dayend, loglist);
                    if (student.week4_2 == "1") totalcome = totalcome + 1;
                    if (student.week4_2 == "0") totalskip = totalskip + 1;
                    student.week4_3 = comeorskip(day24, schedulidlist, data.sID, dayend, loglist);
                    if (student.week4_3 == "1") totalcome = totalcome + 1;
                    if (student.week4_3 == "0") totalskip = totalskip + 1;
                    student.week4_4 = comeorskip(day25, schedulidlist, data.sID, dayend, loglist);
                    if (student.week4_4 == "1") totalcome = totalcome + 1;
                    if (student.week4_4 == "0") totalskip = totalskip + 1;
                    student.week4_5 = comeorskip(day26, schedulidlist, data.sID, dayend, loglist);
                    if (student.week4_5 == "1") totalcome = totalcome + 1;
                    if (student.week4_5 == "0") totalskip = totalskip + 1;
                    student.week4_6 = comeorskip(day27, schedulidlist, data.sID, dayend, loglist);
                    if (student.week4_6 == "1") totalcome = totalcome + 1;
                    if (student.week4_6 == "0") totalskip = totalskip + 1;
                    student.week4_7 = comeorskip(day28, schedulidlist, data.sID, dayend, loglist);
                    if (student.week4_7 == "1") totalcome = totalcome + 1;
                    if (student.week4_7 == "0") totalskip = totalskip + 1;

                    student.week5_1 = comeorskip(day29, schedulidlist, data.sID, dayend, loglist);
                    if (student.week5_1 == "1") totalcome = totalcome + 1;
                    if (student.week5_1 == "0") totalskip = totalskip + 1;
                    student.week5_2 = comeorskip(day30, schedulidlist, data.sID, dayend, loglist);
                    if (student.week5_2 == "1") totalcome = totalcome + 1;
                    if (student.week5_2 == "0") totalskip = totalskip + 1;
                    student.week5_3 = comeorskip(day31, schedulidlist, data.sID, dayend, loglist);
                    if (student.week5_3 == "1") totalcome = totalcome + 1;
                    if (student.week5_3 == "0") totalskip = totalskip + 1;
                    student.week5_4 = comeorskip(day32, schedulidlist, data.sID, dayend, loglist);
                    if (student.week5_4 == "1") totalcome = totalcome + 1;
                    if (student.week5_4 == "0") totalskip = totalskip + 1;
                    student.week5_5 = comeorskip(day33, schedulidlist, data.sID, dayend, loglist);
                    if (student.week5_5 == "1") totalcome = totalcome + 1;
                    if (student.week5_5 == "0") totalskip = totalskip + 1;
                    student.week5_6 = comeorskip(day34, schedulidlist, data.sID, dayend, loglist);
                    if (student.week5_6 == "1") totalcome = totalcome + 1;
                    if (student.week5_6 == "0") totalskip = totalskip + 1;
                    student.week5_7 = comeorskip(day35, schedulidlist, data.sID, dayend, loglist);
                    if (student.week5_7 == "1") totalcome = totalcome + 1;
                    if (student.week5_7 == "0") totalskip = totalskip + 1;

                    student.week6_1 = comeorskip(day36, schedulidlist, data.sID, dayend, loglist);
                    if (student.week6_1 == "1") totalcome = totalcome + 1;
                    if (student.week6_1 == "0") totalskip = totalskip + 1;
                    student.week6_2 = comeorskip(day37, schedulidlist, data.sID, dayend, loglist);
                    if (student.week6_2 == "1") totalcome = totalcome + 1;
                    if (student.week6_2 == "0") totalskip = totalskip + 1;
                    student.week6_3 = comeorskip(day38, schedulidlist, data.sID, dayend, loglist);
                    if (student.week6_3 == "1") totalcome = totalcome + 1;
                    if (student.week6_3 == "0") totalskip = totalskip + 1;
                    student.week6_4 = comeorskip(day39, schedulidlist, data.sID, dayend, loglist);
                    if (student.week6_4 == "1") totalcome = totalcome + 1;
                    if (student.week6_4 == "0") totalskip = totalskip + 1;
                    student.week6_5 = comeorskip(day40, schedulidlist, data.sID, dayend, loglist);
                    if (student.week6_5 == "1") totalcome = totalcome + 1;
                    if (student.week6_5 == "0") totalskip = totalskip + 1;
                    student.week6_6 = comeorskip(day41, schedulidlist, data.sID, dayend, loglist);
                    if (student.week6_6 == "1") totalcome = totalcome + 1;
                    if (student.week6_6 == "0") totalskip = totalskip + 1;
                    student.week6_7 = comeorskip(day42, schedulidlist, data.sID, dayend, loglist);
                    if (student.week6_7 == "1") totalcome = totalcome + 1;
                    if (student.week6_7 == "0") totalskip = totalskip + 1;

                    student.week7_1 = comeorskip(day43, schedulidlist, data.sID, dayend, loglist);
                    if (student.week7_1 == "1") totalcome = totalcome + 1;
                    if (student.week7_1 == "0") totalskip = totalskip + 1;
                    student.week7_2 = comeorskip(day44, schedulidlist, data.sID, dayend, loglist);
                    if (student.week7_2 == "1") totalcome = totalcome + 1;
                    if (student.week7_2 == "0") totalskip = totalskip + 1;
                    student.week7_3 = comeorskip(day45, schedulidlist, data.sID, dayend, loglist);
                    if (student.week7_3 == "1") totalcome = totalcome + 1;
                    if (student.week7_3 == "0") totalskip = totalskip + 1;
                    student.week7_4 = comeorskip(day46, schedulidlist, data.sID, dayend, loglist);
                    if (student.week7_4 == "1") totalcome = totalcome + 1;
                    if (student.week7_4 == "0") totalskip = totalskip + 1;
                    student.week7_5 = comeorskip(day47, schedulidlist, data.sID, dayend, loglist);
                    if (student.week7_5 == "1") totalcome = totalcome + 1;
                    if (student.week7_5 == "0") totalskip = totalskip + 1;
                    student.week7_6 = comeorskip(day48, schedulidlist, data.sID, dayend, loglist);
                    if (student.week7_6 == "1") totalcome = totalcome + 1;
                    if (student.week7_6 == "0") totalskip = totalskip + 1;
                    student.week7_7 = comeorskip(day49, schedulidlist, data.sID, dayend, loglist);
                    if (student.week7_7 == "1") totalcome = totalcome + 1;
                    if (student.week7_7 == "0") totalskip = totalskip + 1;

                    student.week8_1 = comeorskip(day50, schedulidlist, data.sID, dayend, loglist);
                    if (student.week8_1 == "1") totalcome = totalcome + 1;
                    if (student.week8_1 == "0") totalskip = totalskip + 1;
                    student.week8_2 = comeorskip(day51, schedulidlist, data.sID, dayend, loglist);
                    if (student.week8_2 == "1") totalcome = totalcome + 1;
                    if (student.week8_2 == "0") totalskip = totalskip + 1;
                    student.week8_3 = comeorskip(day52, schedulidlist, data.sID, dayend, loglist);
                    if (student.week8_3 == "1") totalcome = totalcome + 1;
                    if (student.week8_3 == "0") totalskip = totalskip + 1;
                    student.week8_4 = comeorskip(day53, schedulidlist, data.sID, dayend, loglist);
                    if (student.week8_4 == "1") totalcome = totalcome + 1;
                    if (student.week8_4 == "0") totalskip = totalskip + 1;
                    student.week8_5 = comeorskip(day54, schedulidlist, data.sID, dayend, loglist);
                    if (student.week8_5 == "1") totalcome = totalcome + 1;
                    if (student.week8_5 == "0") totalskip = totalskip + 1;
                    student.week8_6 = comeorskip(day55, schedulidlist, data.sID, dayend, loglist);
                    if (student.week8_6 == "1") totalcome = totalcome + 1;
                    if (student.week8_6 == "0") totalskip = totalskip + 1;
                    student.week8_7 = comeorskip(day56, schedulidlist, data.sID, dayend, loglist);
                    if (student.week8_7 == "1") totalcome = totalcome + 1;
                    if (student.week8_7 == "0") totalskip = totalskip + 1;

                    student.week9_1 = comeorskip(day57, schedulidlist, data.sID, dayend, loglist);
                    if (student.week9_1 == "1") totalcome = totalcome + 1;
                    if (student.week9_1 == "0") totalskip = totalskip + 1;
                    student.week9_2 = comeorskip(day58, schedulidlist, data.sID, dayend, loglist);
                    if (student.week9_2 == "1") totalcome = totalcome + 1;
                    if (student.week9_2 == "0") totalskip = totalskip + 1;
                    student.week9_3 = comeorskip(day59, schedulidlist, data.sID, dayend, loglist);
                    if (student.week9_3 == "1") totalcome = totalcome + 1;
                    if (student.week9_3 == "0") totalskip = totalskip + 1;
                    student.week9_4 = comeorskip(day60, schedulidlist, data.sID, dayend, loglist);
                    if (student.week9_4 == "1") totalcome = totalcome + 1;
                    if (student.week9_4 == "0") totalskip = totalskip + 1;
                    student.week9_5 = comeorskip(day61, schedulidlist, data.sID, dayend, loglist);
                    if (student.week9_5 == "1") totalcome = totalcome + 1;
                    if (student.week9_5 == "0") totalskip = totalskip + 1;
                    student.week9_6 = comeorskip(day62, schedulidlist, data.sID, dayend, loglist);
                    if (student.week9_6 == "1") totalcome = totalcome + 1;
                    if (student.week9_6 == "0") totalskip = totalskip + 1;
                    student.week9_7 = comeorskip(day63, schedulidlist, data.sID, dayend, loglist);
                    if (student.week9_7 == "1") totalcome = totalcome + 1;
                    if (student.week9_7 == "0") totalskip = totalskip + 1;

                    student.week10_1 = comeorskip(day64, schedulidlist, data.sID, dayend, loglist);
                    if (student.week10_1 == "1") totalcome = totalcome + 1;
                    if (student.week10_1 == "0") totalskip = totalskip + 1;
                    student.week10_2 = comeorskip(day65, schedulidlist, data.sID, dayend, loglist);
                    if (student.week10_2 == "1") totalcome = totalcome + 1;
                    if (student.week10_2 == "0") totalskip = totalskip + 1;
                    student.week10_3 = comeorskip(day66, schedulidlist, data.sID, dayend, loglist);
                    if (student.week10_3 == "1") totalcome = totalcome + 1;
                    if (student.week10_3 == "0") totalskip = totalskip + 1;
                    student.week10_4 = comeorskip(day67, schedulidlist, data.sID, dayend, loglist);
                    if (student.week10_4 == "1") totalcome = totalcome + 1;
                    if (student.week10_4 == "0") totalskip = totalskip + 1;
                    student.week10_5 = comeorskip(day68, schedulidlist, data.sID, dayend, loglist);
                    if (student.week10_5 == "1") totalcome = totalcome + 1;
                    if (student.week10_5 == "0") totalskip = totalskip + 1;
                    student.week10_6 = comeorskip(day69, schedulidlist, data.sID, dayend, loglist);
                    if (student.week10_6 == "1") totalcome = totalcome + 1;
                    if (student.week10_6 == "0") totalskip = totalskip + 1;
                    student.week10_7 = comeorskip(day70, schedulidlist, data.sID, dayend, loglist);
                    if (student.week10_7 == "1") totalcome = totalcome + 1;
                    if (student.week10_7 == "0") totalskip = totalskip + 1;

                    student.week11_1 = comeorskip(day71, schedulidlist, data.sID, dayend, loglist);
                    if (student.week11_1 == "1") totalcome = totalcome + 1;
                    if (student.week11_1 == "0") totalskip = totalskip + 1;
                    student.week11_2 = comeorskip(day72, schedulidlist, data.sID, dayend, loglist);
                    if (student.week11_2 == "1") totalcome = totalcome + 1;
                    if (student.week11_2 == "0") totalskip = totalskip + 1;
                    student.week11_3 = comeorskip(day73, schedulidlist, data.sID, dayend, loglist);
                    if (student.week11_3 == "1") totalcome = totalcome + 1;
                    if (student.week11_3 == "0") totalskip = totalskip + 1;
                    student.week11_4 = comeorskip(day74, schedulidlist, data.sID, dayend, loglist);
                    if (student.week11_4 == "1") totalcome = totalcome + 1;
                    if (student.week11_4 == "0") totalskip = totalskip + 1;
                    student.week11_5 = comeorskip(day75, schedulidlist, data.sID, dayend, loglist);
                    if (student.week11_5 == "1") totalcome = totalcome + 1;
                    if (student.week11_5 == "0") totalskip = totalskip + 1;
                    student.week11_6 = comeorskip(day76, schedulidlist, data.sID, dayend, loglist);
                    if (student.week11_6 == "1") totalcome = totalcome + 1;
                    if (student.week11_6 == "0") totalskip = totalskip + 1;
                    student.week11_7 = comeorskip(day77, schedulidlist, data.sID, dayend, loglist);
                    if (student.week11_7 == "1") totalcome = totalcome + 1;
                    if (student.week11_7 == "0") totalskip = totalskip + 1;

                    student.week12_1 = comeorskip(day78, schedulidlist, data.sID, dayend, loglist);
                    if (student.week12_1 == "1") totalcome = totalcome + 1;
                    if (student.week12_1 == "0") totalskip = totalskip + 1;
                    student.week12_2 = comeorskip(day79, schedulidlist, data.sID, dayend, loglist);
                    if (student.week12_2 == "1") totalcome = totalcome + 1;
                    if (student.week12_2 == "0") totalskip = totalskip + 1;
                    student.week12_3 = comeorskip(day80, schedulidlist, data.sID, dayend, loglist);
                    if (student.week12_3 == "1") totalcome = totalcome + 1;
                    if (student.week12_3 == "0") totalskip = totalskip + 1;
                    student.week12_4 = comeorskip(day81, schedulidlist, data.sID, dayend, loglist);
                    if (student.week12_4 == "1") totalcome = totalcome + 1;
                    if (student.week12_4 == "0") totalskip = totalskip + 1;
                    student.week12_5 = comeorskip(day82, schedulidlist, data.sID, dayend, loglist);
                    if (student.week12_5 == "1") totalcome = totalcome + 1;
                    if (student.week12_5 == "0") totalskip = totalskip + 1;
                    student.week12_6 = comeorskip(day83, schedulidlist, data.sID, dayend, loglist);
                    if (student.week12_6 == "1") totalcome = totalcome + 1;
                    if (student.week12_6 == "0") totalskip = totalskip + 1;
                    student.week12_7 = comeorskip(day84, schedulidlist, data.sID, dayend, loglist);
                    if (student.week12_7 == "1") totalcome = totalcome + 1;
                    if (student.week12_7 == "0") totalskip = totalskip + 1;

                    student.week13_1 = comeorskip(day85, schedulidlist, data.sID, dayend, loglist);
                    if (student.week13_1 == "1") totalcome = totalcome + 1;
                    if (student.week13_1 == "0") totalskip = totalskip + 1;
                    student.week13_2 = comeorskip(day86, schedulidlist, data.sID, dayend, loglist);
                    if (student.week13_2 == "1") totalcome = totalcome + 1;
                    if (student.week13_2 == "0") totalskip = totalskip + 1;
                    student.week13_3 = comeorskip(day87, schedulidlist, data.sID, dayend, loglist);
                    if (student.week13_3 == "1") totalcome = totalcome + 1;
                    if (student.week13_3 == "0") totalskip = totalskip + 1;
                    student.week13_4 = comeorskip(day88, schedulidlist, data.sID, dayend, loglist);
                    if (student.week13_4 == "1") totalcome = totalcome + 1;
                    if (student.week13_4 == "0") totalskip = totalskip + 1;
                    student.week13_5 = comeorskip(day89, schedulidlist, data.sID, dayend, loglist);
                    if (student.week13_5 == "1") totalcome = totalcome + 1;
                    if (student.week13_5 == "0") totalskip = totalskip + 1;
                    student.week13_6 = comeorskip(day90, schedulidlist, data.sID, dayend, loglist);
                    if (student.week13_6 == "1") totalcome = totalcome + 1;
                    if (student.week13_6 == "0") totalskip = totalskip + 1;
                    student.week13_7 = comeorskip(day91, schedulidlist, data.sID, dayend, loglist);
                    if (student.week13_7 == "1") totalcome = totalcome + 1;
                    if (student.week13_7 == "0") totalskip = totalskip + 1;

                    student.week14_1 = comeorskip(day92, schedulidlist, data.sID, dayend, loglist);
                    if (student.week14_1 == "1") totalcome = totalcome + 1;
                    if (student.week14_1 == "0") totalskip = totalskip + 1;
                    student.week14_2 = comeorskip(day93, schedulidlist, data.sID, dayend, loglist);
                    if (student.week14_2 == "1") totalcome = totalcome + 1;
                    if (student.week14_2 == "0") totalskip = totalskip + 1;
                    student.week14_3 = comeorskip(day94, schedulidlist, data.sID, dayend, loglist);
                    if (student.week14_3 == "1") totalcome = totalcome + 1;
                    if (student.week14_3 == "0") totalskip = totalskip + 1;
                    student.week14_4 = comeorskip(day95, schedulidlist, data.sID, dayend, loglist);
                    if (student.week14_4 == "1") totalcome = totalcome + 1;
                    if (student.week14_4 == "0") totalskip = totalskip + 1;
                    student.week14_5 = comeorskip(day96, schedulidlist, data.sID, dayend, loglist);
                    if (student.week14_5 == "1") totalcome = totalcome + 1;
                    if (student.week14_5 == "0") totalskip = totalskip + 1;
                    student.week14_6 = comeorskip(day97, schedulidlist, data.sID, dayend, loglist);
                    if (student.week14_6 == "1") totalcome = totalcome + 1;
                    if (student.week14_6 == "0") totalskip = totalskip + 1;
                    student.week14_7 = comeorskip(day98, schedulidlist, data.sID, dayend, loglist);
                    if (student.week14_7 == "1") totalcome = totalcome + 1;
                    if (student.week14_7 == "0") totalskip = totalskip + 1;

                    student.week15_1 = comeorskip(day99, schedulidlist, data.sID, dayend, loglist);
                    if (student.week15_1 == "1") totalcome = totalcome + 1;
                    if (student.week15_1 == "0") totalskip = totalskip + 1;
                    student.week15_2 = comeorskip(day100, schedulidlist, data.sID, dayend, loglist);
                    if (student.week15_2 == "1") totalcome = totalcome + 1;
                    if (student.week15_2 == "0") totalskip = totalskip + 1;
                    student.week15_3 = comeorskip(day101, schedulidlist, data.sID, dayend, loglist);
                    if (student.week15_3 == "1") totalcome = totalcome + 1;
                    if (student.week15_3 == "0") totalskip = totalskip + 1;
                    student.week15_4 = comeorskip(day102, schedulidlist, data.sID, dayend, loglist);
                    if (student.week15_4 == "1") totalcome = totalcome + 1;
                    if (student.week15_4 == "0") totalskip = totalskip + 1;
                    student.week15_5 = comeorskip(day103, schedulidlist, data.sID, dayend, loglist);
                    if (student.week15_5 == "1") totalcome = totalcome + 1;
                    if (student.week15_5 == "0") totalskip = totalskip + 1;
                    student.week15_6 = comeorskip(day104, schedulidlist, data.sID, dayend, loglist);
                    if (student.week15_6 == "1") totalcome = totalcome + 1;
                    if (student.week15_6 == "0") totalskip = totalskip + 1;
                    student.week15_7 = comeorskip(day105, schedulidlist, data.sID, dayend, loglist);
                    if (student.week15_7 == "1") totalcome = totalcome + 1;
                    if (student.week15_7 == "0") totalskip = totalskip + 1;

                    student.week16_1 = comeorskip(day106, schedulidlist, data.sID, dayend, loglist);
                    if (student.week16_1 == "1") totalcome = totalcome + 1;
                    if (student.week16_1 == "0") totalskip = totalskip + 1;
                    student.week16_2 = comeorskip(day107, schedulidlist, data.sID, dayend, loglist);
                    if (student.week16_2 == "1") totalcome = totalcome + 1;
                    if (student.week16_2 == "0") totalskip = totalskip + 1;
                    student.week16_3 = comeorskip(day108, schedulidlist, data.sID, dayend, loglist);
                    if (student.week16_3 == "1") totalcome = totalcome + 1;
                    if (student.week16_3 == "0") totalskip = totalskip + 1;
                    student.week16_4 = comeorskip(day109, schedulidlist, data.sID, dayend, loglist);
                    if (student.week16_4 == "1") totalcome = totalcome + 1;
                    if (student.week16_4 == "0") totalskip = totalskip + 1;
                    student.week16_5 = comeorskip(day110, schedulidlist, data.sID, dayend, loglist);
                    if (student.week16_5 == "1") totalcome = totalcome + 1;
                    if (student.week16_5 == "0") totalskip = totalskip + 1;
                    student.week16_6 = comeorskip(day111, schedulidlist, data.sID, dayend, loglist);
                    if (student.week16_6 == "1") totalcome = totalcome + 1;
                    if (student.week16_6 == "0") totalskip = totalskip + 1;
                    student.week16_7 = comeorskip(day112, schedulidlist, data.sID, dayend, loglist);
                    if (student.week16_7 == "1") totalcome = totalcome + 1;
                    if (student.week16_7 == "0") totalskip = totalskip + 1;

                    student.week17_1 = comeorskip(day113, schedulidlist, data.sID, dayend, loglist);
                    if (student.week17_1 == "1") totalcome = totalcome + 1;
                    if (student.week17_1 == "0") totalskip = totalskip + 1;
                    student.week17_2 = comeorskip(day114, schedulidlist, data.sID, dayend, loglist);
                    if (student.week17_2 == "1") totalcome = totalcome + 1;
                    if (student.week17_2 == "0") totalskip = totalskip + 1;
                    student.week17_3 = comeorskip(day115, schedulidlist, data.sID, dayend, loglist);
                    if (student.week17_3 == "1") totalcome = totalcome + 1;
                    if (student.week17_3 == "0") totalskip = totalskip + 1;
                    student.week17_4 = comeorskip(day116, schedulidlist, data.sID, dayend, loglist);
                    if (student.week17_4 == "1") totalcome = totalcome + 1;
                    if (student.week17_4 == "0") totalskip = totalskip + 1;
                    student.week17_5 = comeorskip(day117, schedulidlist, data.sID, dayend, loglist);
                    if (student.week17_5 == "1") totalcome = totalcome + 1;
                    if (student.week17_5 == "0") totalskip = totalskip + 1;
                    student.week17_6 = comeorskip(day118, schedulidlist, data.sID, dayend, loglist);
                    if (student.week17_6 == "1") totalcome = totalcome + 1;
                    if (student.week17_6 == "0") totalskip = totalskip + 1;
                    student.week17_7 = comeorskip(day119, schedulidlist, data.sID, dayend, loglist);
                    if (student.week17_7 == "1") totalcome = totalcome + 1;
                    if (student.week17_7 == "0") totalskip = totalskip + 1;

                    student.week18_1 = comeorskip(day120, schedulidlist, data.sID, dayend, loglist);
                    if (student.week18_1 == "1") totalcome = totalcome + 1;
                    if (student.week18_1 == "0") totalskip = totalskip + 1;
                    student.week18_2 = comeorskip(day121, schedulidlist, data.sID, dayend, loglist);
                    if (student.week18_2 == "1") totalcome = totalcome + 1;
                    if (student.week18_2 == "0") totalskip = totalskip + 1;
                    student.week18_3 = comeorskip(day122, schedulidlist, data.sID, dayend, loglist);
                    if (student.week18_3 == "1") totalcome = totalcome + 1;
                    if (student.week18_3 == "0") totalskip = totalskip + 1;
                    student.week18_4 = comeorskip(day123, schedulidlist, data.sID, dayend, loglist);
                    if (student.week18_4 == "1") totalcome = totalcome + 1;
                    if (student.week18_4 == "0") totalskip = totalskip + 1;
                    student.week18_5 = comeorskip(day124, schedulidlist, data.sID, dayend, loglist);
                    if (student.week18_5 == "1") totalcome = totalcome + 1;
                    if (student.week18_5 == "0") totalskip = totalskip + 1;
                    student.week18_6 = comeorskip(day125, schedulidlist, data.sID, dayend, loglist);
                    if (student.week18_6 == "1") totalcome = totalcome + 1;
                    if (student.week18_6 == "0") totalskip = totalskip + 1;
                    student.week18_7 = comeorskip(day126, schedulidlist, data.sID, dayend, loglist);
                    if (student.week18_7 == "1") totalcome = totalcome + 1;
                    if (student.week18_7 == "0") totalskip = totalskip + 1;

                    student.week19_1 = comeorskip(day127, schedulidlist, data.sID, dayend, loglist);
                    if (student.week19_1 == "1") totalcome = totalcome + 1;
                    if (student.week19_1 == "0") totalskip = totalskip + 1;
                    student.week19_2 = comeorskip(day128, schedulidlist, data.sID, dayend, loglist);
                    if (student.week19_2 == "1") totalcome = totalcome + 1;
                    if (student.week19_2 == "0") totalskip = totalskip + 1;
                    student.week19_3 = comeorskip(day129, schedulidlist, data.sID, dayend, loglist);
                    if (student.week19_3 == "1") totalcome = totalcome + 1;
                    if (student.week19_3 == "0") totalskip = totalskip + 1;
                    student.week19_4 = comeorskip(day130, schedulidlist, data.sID, dayend, loglist);
                    if (student.week19_4 == "1") totalcome = totalcome + 1;
                    if (student.week19_4 == "0") totalskip = totalskip + 1;
                    student.week19_5 = comeorskip(day131, schedulidlist, data.sID, dayend, loglist);
                    if (student.week19_5 == "1") totalcome = totalcome + 1;
                    if (student.week19_5 == "0") totalskip = totalskip + 1;
                    student.week19_6 = comeorskip(day132, schedulidlist, data.sID, dayend, loglist);
                    if (student.week19_6 == "1") totalcome = totalcome + 1;
                    if (student.week19_6 == "0") totalskip = totalskip + 1;
                    student.week19_7 = comeorskip(day133, schedulidlist, data.sID, dayend, loglist);
                    if (student.week19_7 == "1") totalcome = totalcome + 1;
                    if (student.week19_7 == "0") totalskip = totalskip + 1;

                    student.week20_1 = comeorskip(day134, schedulidlist, data.sID, dayend, loglist);
                    if (student.week20_1 == "1") totalcome = totalcome + 1;
                    if (student.week20_1 == "0") totalskip = totalskip + 1;
                    student.week20_2 = comeorskip(day135, schedulidlist, data.sID, dayend, loglist);
                    if (student.week20_2 == "1") totalcome = totalcome + 1;
                    if (student.week20_2 == "0") totalskip = totalskip + 1;
                    student.week20_3 = comeorskip(day136, schedulidlist, data.sID, dayend, loglist);
                    if (student.week20_3 == "1") totalcome = totalcome + 1;
                    if (student.week20_3 == "0") totalskip = totalskip + 1;
                    student.week20_4 = comeorskip(day137, schedulidlist, data.sID, dayend, loglist);
                    if (student.week20_4 == "1") totalcome = totalcome + 1;
                    if (student.week20_4 == "0") totalskip = totalskip + 1;
                    student.week20_5 = comeorskip(day138, schedulidlist, data.sID, dayend, loglist);
                    if (student.week20_5 == "1") totalcome = totalcome + 1;
                    if (student.week20_5 == "0") totalskip = totalskip + 1;
                    student.week20_6 = comeorskip(day139, schedulidlist, data.sID, dayend, loglist);
                    if (student.week20_6 == "1") totalcome = totalcome + 1;
                    if (student.week20_6 == "0") totalskip = totalskip + 1;
                    student.week20_7 = comeorskip(day140, schedulidlist, data.sID, dayend, loglist);
                    if (student.week20_7 == "1") totalcome = totalcome + 1;
                    if (student.week20_7 == "0") totalskip = totalskip + 1;

                    student.totalskip = totalskip.ToString();
                    student.totalcome = totalcome.ToString();
                    var TUserMaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0" && w.nSystemID == data.sID).FirstOrDefault();
                    if (TUserMaster != null)
                    {
                        number = number + 1;
                        studentlist.Add(student);
                    }
                }

                dgd.DataSource = studentlist;
                dgd.PageSize = 999;
                dgd.DataBind();
            }
        }

        public string comeorskip(DateTime? day, List<int> schedulidlist, int userid, DateTime? dayend, List<TLogLearnTimeScan> loglist)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
               

                string sEntities = Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                int today = (int)day.Value.DayOfWeek;

                string come = "";


                if (day < dayend.Value.AddDays(1))
                {

                    foreach (var check in schedulidlist)
                    {
                        var studyday = _db.TSchedules.Where(w => w.sScheduleID == check && w.SchoolID == userData.CompanyID).FirstOrDefault();
                        if (studyday.nPlaneDay == today)
                        {
                            come = "0";
                            var d1 = loglist.Where(w => w.LogLearnDate == day && w.sID == userid && w.sScheduleID == check && (w.LogLearnType == "1" || w.LogLearnType == "0")).FirstOrDefault();
                            if (d1 != null)
                                come = "1";
                        }
                    }
                }
                else
                {
                    come = "2";
                }

                return come;
            }
        }

        protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 1";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 2";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 3";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 4";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 5";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 6";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 7";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 8";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 9";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 10";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 11";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 12";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 13";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 14";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 15";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 16";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 17";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 18";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 19";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "สัปดาห์ที่ 20";
                HeaderCell.ColumnSpan = 7;
                HeaderCell.CssClass = "centertext";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow.Cells.Add(HeaderCell);
                dgd.Controls[0].Controls.AddAt(0, HeaderGridRow);

            }
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];
            string id = Request.QueryString["id"];

            Response.Redirect("Webform5.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term + "&id=" + id);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        class studentlist5
        {
            public int? sID { get; set; }
            public string sName { get; set; }
            public string week1_1 { get; set; }
            public string week1_2 { get; set; }
            public string week1_3 { get; set; }
            public string week1_4 { get; set; }
            public string week1_5 { get; set; }
            public string week1_6 { get; set; }
            public string week1_7 { get; set; }
            public string week2_1 { get; set; }
            public string week2_2 { get; set; }
            public string week2_3 { get; set; }
            public string week2_4 { get; set; }
            public string week2_5 { get; set; }
            public string week2_6 { get; set; }
            public string week2_7 { get; set; }
            public string week3_1 { get; set; }
            public string week3_2 { get; set; }
            public string week3_3 { get; set; }
            public string week3_4 { get; set; }
            public string week3_5 { get; set; }
            public string week3_6 { get; set; }
            public string week3_7 { get; set; }
            public string week4_1 { get; set; }
            public string week4_2 { get; set; }
            public string week4_3 { get; set; }
            public string week4_4 { get; set; }
            public string week4_5 { get; set; }
            public string week4_6 { get; set; }
            public string week4_7 { get; set; }
            public string week5_1 { get; set; }
            public string week5_2 { get; set; }
            public string week5_3 { get; set; }
            public string week5_4 { get; set; }
            public string week5_5 { get; set; }
            public string week5_6 { get; set; }
            public string week5_7 { get; set; }
            public string week6_1 { get; set; }
            public string week6_2 { get; set; }
            public string week6_3 { get; set; }
            public string week6_4 { get; set; }
            public string week6_5 { get; set; }
            public string week6_6 { get; set; }
            public string week6_7 { get; set; }
            public string week7_1 { get; set; }
            public string week7_2 { get; set; }
            public string week7_3 { get; set; }
            public string week7_4 { get; set; }
            public string week7_5 { get; set; }
            public string week7_6 { get; set; }
            public string week7_7 { get; set; }
            public string week8_1 { get; set; }
            public string week8_2 { get; set; }
            public string week8_3 { get; set; }
            public string week8_4 { get; set; }
            public string week8_5 { get; set; }
            public string week8_6 { get; set; }
            public string week8_7 { get; set; }
            public string week9_1 { get; set; }
            public string week9_2 { get; set; }
            public string week9_3 { get; set; }
            public string week9_4 { get; set; }
            public string week9_5 { get; set; }
            public string week9_6 { get; set; }
            public string week9_7 { get; set; }
            public string week10_1 { get; set; }
            public string week10_2 { get; set; }
            public string week10_3 { get; set; }
            public string week10_4 { get; set; }
            public string week10_5 { get; set; }
            public string week10_6 { get; set; }
            public string week10_7 { get; set; }
            public string week11_1 { get; set; }
            public string week11_2 { get; set; }
            public string week11_3 { get; set; }
            public string week11_4 { get; set; }
            public string week11_5 { get; set; }
            public string week11_6 { get; set; }
            public string week11_7 { get; set; }
            public string week12_1 { get; set; }
            public string week12_2 { get; set; }
            public string week12_3 { get; set; }
            public string week12_4 { get; set; }
            public string week12_5 { get; set; }
            public string week12_6 { get; set; }
            public string week12_7 { get; set; }
            public string week13_1 { get; set; }
            public string week13_2 { get; set; }
            public string week13_3 { get; set; }
            public string week13_4 { get; set; }
            public string week13_5 { get; set; }
            public string week13_6 { get; set; }
            public string week13_7 { get; set; }
            public string week14_1 { get; set; }
            public string week14_2 { get; set; }
            public string week14_3 { get; set; }
            public string week14_4 { get; set; }
            public string week14_5 { get; set; }
            public string week14_6 { get; set; }
            public string week14_7 { get; set; }
            public string week15_1 { get; set; }
            public string week15_2 { get; set; }
            public string week15_3 { get; set; }
            public string week15_4 { get; set; }
            public string week15_5 { get; set; }
            public string week15_6 { get; set; }
            public string week15_7 { get; set; }
            public string week16_1 { get; set; }
            public string week16_2 { get; set; }
            public string week16_3 { get; set; }
            public string week16_4 { get; set; }
            public string week16_5 { get; set; }
            public string week16_6 { get; set; }
            public string week16_7 { get; set; }
            public string week17_1 { get; set; }
            public string week17_2 { get; set; }
            public string week17_3 { get; set; }
            public string week17_4 { get; set; }
            public string week17_5 { get; set; }
            public string week17_6 { get; set; }
            public string week17_7 { get; set; }
            public string week18_1 { get; set; }
            public string week18_2 { get; set; }
            public string week18_3 { get; set; }
            public string week18_4 { get; set; }
            public string week18_5 { get; set; }
            public string week18_6 { get; set; }
            public string week18_7 { get; set; }
            public string week19_1 { get; set; }
            public string week19_2 { get; set; }
            public string week19_3 { get; set; }
            public string week19_4 { get; set; }
            public string week19_5 { get; set; }
            public string week19_6 { get; set; }
            public string week19_7 { get; set; }
            public string week20_1 { get; set; }
            public string week20_2 { get; set; }
            public string week20_3 { get; set; }
            public string week20_4 { get; set; }
            public string week20_5 { get; set; }
            public string week20_6 { get; set; }
            public string week20_7 { get; set; }
            public string totalcome { get; set; }
            public string totalskip { get; set; }
            public string number { get; set; }
            public bool come { get; set; }
            public bool skip { get; set; }
            public bool nostudy { get; set; }
        }
    }

}