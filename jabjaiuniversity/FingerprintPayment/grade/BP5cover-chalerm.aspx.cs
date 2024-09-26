using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment.grade
{
    public partial class BP5cover_chalerm : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        private JWTToken.userData userData = new JWTToken.userData();
        protected void Page_Load(object sender, EventArgs e)
        {

            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");


            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

               

                string id = Request.QueryString["id"];
                //int idn = int.Parse(id);
                //var data = _db.TPlanes.Where(w => w.sPlaneID == id).FirstOrDefault();

                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var schooldata = _dbMaster.TCompanies.Where(w => w.nCompany == nCompany.nCompany).FirstOrDefault();

                string idlv = Request.QueryString["idlv"];
                int? idlvn = Int32.Parse(idlv);
                string idlv2 = Request.QueryString["idlv2"];
                int? idlv2n = Int32.Parse(idlv2);
                var room = _db.TSubLevels.Where(w => w.nTSubLevel == idlvn && w.SchoolID == userData.CompanyID).FirstOrDefault();

                var room2 = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();



                string year = Request.QueryString["year"];
                string userterm = Request.QueryString["term"];
                string userterm2 = "";
                if (userterm == "1") userterm2 = "2";
                if (userterm == "2") userterm2 = "1";

                var plandata = _db.TPlanes.Where(w => w.sPlaneID.ToString() == id && w.SchoolID == userData.CompanyID).FirstOrDefault();
                var roomz = _db.TSubLevels.Where(w => w.nTSubLevel == idlvn && w.SchoolID == userData.CompanyID).FirstOrDefault();
                var room2z = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
                paper23.Text = "บันทึกเวลาเรียน วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                paper24.Text = "บันทึกเวลาเรียน วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                if (!IsPostBack)
                {
                    OpenData();


                }
            }
        }


        private void OpenData()
        {
            //JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read));

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                

                string sEntities = Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                string id = Request.QueryString["id"];
                string year = Request.QueryString["year"];
                string userterm = Request.QueryString["term"];
                string userterm2 = "";
                if (userterm == "1") userterm2 = "2";
                else if (userterm == "2") userterm2 = "1";
                string idlv2 = Request.QueryString["idlv2"];
                string idlv = Request.QueryString["idlv"];
                int? idlvn = Int32.Parse(idlv);

                int? useryear = Int32.Parse(year);
                int? idlv2n = Int32.Parse(idlv2);
                int nyear = 0;
                string nterm = "";
                string nterm2 = "";

                var room = _db.TSubLevels.Where(w => w.nTSubLevel == idlvn && w.SchoolID == userData.CompanyID).FirstOrDefault();
                var room2 = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();


                DateTime? day1 = DateTime.Now;


                //txtpaper4.Text = "บันทึกเวลาเรียน";

                foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear && w.SchoolID == userData.CompanyID))
                {
                    nyear = ff.nYear;
                }

                foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.SchoolID == userData.CompanyID && w.cDel == null))
                {
                    nterm = ee.nTerm;
                    day1 = ee.dStart;
                }

                foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm2 && w.nYear == nyear && w.SchoolID == userData.CompanyID && w.cDel == null))
                {
                    nterm2 = ee.nTerm;
                }


                //var grade = _dbGrade.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.SchoolID == userData.CompanyID).FirstOrDefault();
                //var grade2 = _dbGrade.TGrades.Where(w => w.nTerm == nterm2 && w.sPlaneID.ToString() == id && w.SchoolID == userData.CompanyID).FirstOrDefault();


                while ((int)day1.Value.DayOfWeek != 1)
                {
                    day1 = day1.Value.AddDays(-1);
                }

                var q1 = (from a in _db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID)
                          join b in _db.TSchedules.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable equals b.nTermTable
                          where a.nTerm == nterm && a.nTermSubLevel2 == idlv2n &&
                          b.cDel != true && b.calculate != false && b.sPlaneID.ToString() == id
                          select

                              b
                          ).ToList();

                int countSunday = 0, countMonday = 0, countTuesday = 0, countWednesday = 0, countFriday = 0, countThursday = 0, countSaturday = 0;
                foreach (var data in q1)
                {
                    double dif1 = data.tEnd.Value.TotalMinutes - data.tStart.Value.TotalMinutes;
                    int cal1 = (int)dif1 / 50;

                    if (data.nPlaneDay == 0)
                        countSunday = countSunday + cal1;
                    else if (data.nPlaneDay == 1)
                        countMonday = countMonday + cal1;
                    else if (data.nPlaneDay == 2)
                        countTuesday = countTuesday + cal1;
                    else if (data.nPlaneDay == 3)
                        countWednesday = countWednesday + cal1;
                    else if (data.nPlaneDay == 4)
                        countThursday = countThursday + cal1;
                    else if (data.nPlaneDay == 5)
                        countFriday = countFriday + cal1;
                    else if (data.nPlaneDay == 6)
                        countSaturday = countSaturday + cal1;
                }


                Label2.Text = countMonday.ToString();
                Label3.Text = countTuesday.ToString();
                Label4.Text = countWednesday.ToString();
                Label5.Text = countThursday.ToString();
                Label6.Text = countFriday.ToString();

                date1.Text = day1.Value.Day.ToString();
                date2.Text = day1.Value.AddDays(1).Day.ToString();
                date3.Text = day1.Value.AddDays(2).Day.ToString();
                date4.Text = day1.Value.AddDays(3).Day.ToString();
                date5.Text = day1.Value.AddDays(4).Day.ToString();

                date6.Text = day1.Value.AddDays(7).Day.ToString();
                date7.Text = day1.Value.AddDays(8).Day.ToString();
                date8.Text = day1.Value.AddDays(9).Day.ToString();
                date9.Text = day1.Value.AddDays(10).Day.ToString();
                date10.Text = day1.Value.AddDays(11).Day.ToString();

                date11.Text = day1.Value.AddDays(14).Day.ToString();
                date12.Text = day1.Value.AddDays(15).Day.ToString();
                date13.Text = day1.Value.AddDays(16).Day.ToString();
                date14.Text = day1.Value.AddDays(17).Day.ToString();
                date15.Text = day1.Value.AddDays(18).Day.ToString();

                date16.Text = day1.Value.AddDays(21).Day.ToString();
                date17.Text = day1.Value.AddDays(22).Day.ToString();
                date18.Text = day1.Value.AddDays(23).Day.ToString();
                date19.Text = day1.Value.AddDays(24).Day.ToString();
                date20.Text = day1.Value.AddDays(25).Day.ToString();

                date21.Text = day1.Value.AddDays(28).Day.ToString();
                date22.Text = day1.Value.AddDays(29).Day.ToString();
                date23.Text = day1.Value.AddDays(30).Day.ToString();
                date24.Text = day1.Value.AddDays(31).Day.ToString();
                date25.Text = day1.Value.AddDays(32).Day.ToString();

                date26.Text = day1.Value.AddDays(35).Day.ToString();
                date27.Text = day1.Value.AddDays(36).Day.ToString();
                date28.Text = day1.Value.AddDays(37).Day.ToString();
                date29.Text = day1.Value.AddDays(38).Day.ToString();
                date30.Text = day1.Value.AddDays(39).Day.ToString();

                date31.Text = day1.Value.AddDays(42).Day.ToString();
                date32.Text = day1.Value.AddDays(43).Day.ToString();
                date33.Text = day1.Value.AddDays(44).Day.ToString();
                date34.Text = day1.Value.AddDays(45).Day.ToString();
                date35.Text = day1.Value.AddDays(46).Day.ToString();

                date36.Text = day1.Value.AddDays(49).Day.ToString();
                date37.Text = day1.Value.AddDays(50).Day.ToString();
                date38.Text = day1.Value.AddDays(51).Day.ToString();
                date39.Text = day1.Value.AddDays(52).Day.ToString();
                date40.Text = day1.Value.AddDays(53).Day.ToString();

                date41.Text = day1.Value.AddDays(56).Day.ToString();
                date42.Text = day1.Value.AddDays(57).Day.ToString();
                date43.Text = day1.Value.AddDays(58).Day.ToString();
                date44.Text = day1.Value.AddDays(59).Day.ToString();
                date45.Text = day1.Value.AddDays(60).Day.ToString();

                date46.Text = day1.Value.AddDays(63).Day.ToString();
                date47.Text = day1.Value.AddDays(64).Day.ToString();
                date48.Text = day1.Value.AddDays(65).Day.ToString();
                date49.Text = day1.Value.AddDays(66).Day.ToString();
                date50.Text = day1.Value.AddDays(67).Day.ToString();

                date51.Text = day1.Value.AddDays(70).Day.ToString();
                date52.Text = day1.Value.AddDays(71).Day.ToString();
                date53.Text = day1.Value.AddDays(72).Day.ToString();
                date54.Text = day1.Value.AddDays(73).Day.ToString();
                date55.Text = day1.Value.AddDays(74).Day.ToString();

                date56.Text = day1.Value.AddDays(77).Day.ToString();
                date57.Text = day1.Value.AddDays(78).Day.ToString();
                date58.Text = day1.Value.AddDays(79).Day.ToString();
                date59.Text = day1.Value.AddDays(80).Day.ToString();
                date60.Text = day1.Value.AddDays(81).Day.ToString();

                date61.Text = day1.Value.AddDays(84).Day.ToString();
                date62.Text = day1.Value.AddDays(85).Day.ToString();
                date63.Text = day1.Value.AddDays(86).Day.ToString();
                date64.Text = day1.Value.AddDays(87).Day.ToString();
                date65.Text = day1.Value.AddDays(88).Day.ToString();

                date66.Text = day1.Value.AddDays(91).Day.ToString();
                date67.Text = day1.Value.AddDays(92).Day.ToString();
                date68.Text = day1.Value.AddDays(93).Day.ToString();
                date69.Text = day1.Value.AddDays(94).Day.ToString();
                date70.Text = day1.Value.AddDays(95).Day.ToString();

                date71.Text = day1.Value.AddDays(98).Day.ToString();
                date72.Text = day1.Value.AddDays(99).Day.ToString();
                date73.Text = day1.Value.AddDays(100).Day.ToString();
                date74.Text = day1.Value.AddDays(101).Day.ToString();
                date75.Text = day1.Value.AddDays(102).Day.ToString();

                date76.Text = day1.Value.AddDays(105).Day.ToString();
                date77.Text = day1.Value.AddDays(106).Day.ToString();
                date78.Text = day1.Value.AddDays(107).Day.ToString();
                date79.Text = day1.Value.AddDays(108).Day.ToString();
                date80.Text = day1.Value.AddDays(109).Day.ToString();

                date81.Text = day1.Value.AddDays(112).Day.ToString();
                date82.Text = day1.Value.AddDays(113).Day.ToString();
                date83.Text = day1.Value.AddDays(114).Day.ToString();
                date84.Text = day1.Value.AddDays(115).Day.ToString();
                date85.Text = day1.Value.AddDays(116).Day.ToString();

                date86.Text = day1.Value.AddDays(119).Day.ToString();
                date87.Text = day1.Value.AddDays(120).Day.ToString();
                date88.Text = day1.Value.AddDays(121).Day.ToString();
                date89.Text = day1.Value.AddDays(122).Day.ToString();
                date90.Text = day1.Value.AddDays(123).Day.ToString();

                date91.Text = day1.Value.AddDays(126).Day.ToString();
                date92.Text = day1.Value.AddDays(127).Day.ToString();
                date93.Text = day1.Value.AddDays(128).Day.ToString();
                date94.Text = day1.Value.AddDays(129).Day.ToString();
                date95.Text = day1.Value.AddDays(130).Day.ToString();

                date96.Text = day1.Value.AddDays(133).Day.ToString();
                date97.Text = day1.Value.AddDays(134).Day.ToString();
                date98.Text = day1.Value.AddDays(135).Day.ToString();
                date99.Text = day1.Value.AddDays(136).Day.ToString();
                date100.Text = day1.Value.AddDays(137).Day.ToString();

                var holidayList = _db.THolidays.Where(w => w.SchoolID == userData.CompanyID).ToList();

                holiday1.Text = holidayCheck(day1, holidayList);
                holiday2.Text = holidayCheck(day1.Value.AddDays(1), holidayList);
                holiday3.Text = holidayCheck(day1.Value.AddDays(2), holidayList);
                holiday4.Text = holidayCheck(day1.Value.AddDays(3), holidayList);
                holiday5.Text = holidayCheck(day1.Value.AddDays(4), holidayList);

                holiday6.Text = holidayCheck(day1.Value.AddDays(7), holidayList);
                holiday7.Text = holidayCheck(day1.Value.AddDays(8), holidayList);
                holiday8.Text = holidayCheck(day1.Value.AddDays(9), holidayList);
                holiday9.Text = holidayCheck(day1.Value.AddDays(10), holidayList);
                holiday10.Text = holidayCheck(day1.Value.AddDays(11), holidayList);

                holiday11.Text = holidayCheck(day1.Value.AddDays(14), holidayList);
                holiday12.Text = holidayCheck(day1.Value.AddDays(15), holidayList);
                holiday13.Text = holidayCheck(day1.Value.AddDays(16), holidayList);
                holiday14.Text = holidayCheck(day1.Value.AddDays(17), holidayList);
                holiday15.Text = holidayCheck(day1.Value.AddDays(18), holidayList);

                holiday16.Text = holidayCheck(day1.Value.AddDays(21), holidayList);
                holiday17.Text = holidayCheck(day1.Value.AddDays(22), holidayList);
                holiday18.Text = holidayCheck(day1.Value.AddDays(23), holidayList);
                holiday19.Text = holidayCheck(day1.Value.AddDays(24), holidayList);
                holiday20.Text = holidayCheck(day1.Value.AddDays(25), holidayList);

                holiday21.Text = holidayCheck(day1.Value.AddDays(28), holidayList);
                holiday22.Text = holidayCheck(day1.Value.AddDays(29), holidayList);
                holiday23.Text = holidayCheck(day1.Value.AddDays(30), holidayList);
                holiday24.Text = holidayCheck(day1.Value.AddDays(31), holidayList);
                holiday25.Text = holidayCheck(day1.Value.AddDays(32), holidayList);

                holiday26.Text = holidayCheck(day1.Value.AddDays(35), holidayList);
                holiday27.Text = holidayCheck(day1.Value.AddDays(36), holidayList);
                holiday28.Text = holidayCheck(day1.Value.AddDays(37), holidayList);
                holiday29.Text = holidayCheck(day1.Value.AddDays(38), holidayList);
                holiday30.Text = holidayCheck(day1.Value.AddDays(39), holidayList);

                holiday31.Text = holidayCheck(day1.Value.AddDays(42), holidayList);
                holiday32.Text = holidayCheck(day1.Value.AddDays(43), holidayList);
                holiday33.Text = holidayCheck(day1.Value.AddDays(44), holidayList);
                holiday34.Text = holidayCheck(day1.Value.AddDays(45), holidayList);
                holiday35.Text = holidayCheck(day1.Value.AddDays(46), holidayList);

                holiday36.Text = holidayCheck(day1.Value.AddDays(49), holidayList);
                holiday37.Text = holidayCheck(day1.Value.AddDays(50), holidayList);
                holiday38.Text = holidayCheck(day1.Value.AddDays(51), holidayList);
                holiday39.Text = holidayCheck(day1.Value.AddDays(52), holidayList);
                holiday40.Text = holidayCheck(day1.Value.AddDays(53), holidayList);

                holiday41.Text = holidayCheck(day1.Value.AddDays(56), holidayList);
                holiday42.Text = holidayCheck(day1.Value.AddDays(57), holidayList);
                holiday43.Text = holidayCheck(day1.Value.AddDays(58), holidayList);
                holiday44.Text = holidayCheck(day1.Value.AddDays(59), holidayList);
                holiday45.Text = holidayCheck(day1.Value.AddDays(60), holidayList);

                holiday46.Text = holidayCheck(day1.Value.AddDays(63), holidayList);
                holiday47.Text = holidayCheck(day1.Value.AddDays(64), holidayList);
                holiday48.Text = holidayCheck(day1.Value.AddDays(65), holidayList);
                holiday49.Text = holidayCheck(day1.Value.AddDays(66), holidayList);
                holiday50.Text = holidayCheck(day1.Value.AddDays(67), holidayList);

                holiday51.Text = holidayCheck(day1.Value.AddDays(70), holidayList);
                holiday52.Text = holidayCheck(day1.Value.AddDays(71), holidayList);
                holiday53.Text = holidayCheck(day1.Value.AddDays(72), holidayList);
                holiday54.Text = holidayCheck(day1.Value.AddDays(73), holidayList);
                holiday55.Text = holidayCheck(day1.Value.AddDays(74), holidayList);

                holiday56.Text = holidayCheck(day1.Value.AddDays(77), holidayList);
                holiday57.Text = holidayCheck(day1.Value.AddDays(78), holidayList);
                holiday58.Text = holidayCheck(day1.Value.AddDays(79), holidayList);
                holiday59.Text = holidayCheck(day1.Value.AddDays(80), holidayList);
                holiday60.Text = holidayCheck(day1.Value.AddDays(81), holidayList);

                holiday61.Text = holidayCheck(day1.Value.AddDays(84), holidayList);
                holiday62.Text = holidayCheck(day1.Value.AddDays(85), holidayList);
                holiday63.Text = holidayCheck(day1.Value.AddDays(86), holidayList);
                holiday64.Text = holidayCheck(day1.Value.AddDays(87), holidayList);
                holiday65.Text = holidayCheck(day1.Value.AddDays(88), holidayList);

                holiday66.Text = holidayCheck(day1.Value.AddDays(91), holidayList);
                holiday67.Text = holidayCheck(day1.Value.AddDays(92), holidayList);
                holiday68.Text = holidayCheck(day1.Value.AddDays(93), holidayList);
                holiday69.Text = holidayCheck(day1.Value.AddDays(94), holidayList);
                holiday70.Text = holidayCheck(day1.Value.AddDays(95), holidayList);

                holiday71.Text = holidayCheck(day1.Value.AddDays(98), holidayList);
                holiday72.Text = holidayCheck(day1.Value.AddDays(99), holidayList);
                holiday73.Text = holidayCheck(day1.Value.AddDays(100), holidayList);
                holiday74.Text = holidayCheck(day1.Value.AddDays(101), holidayList);
                holiday75.Text = holidayCheck(day1.Value.AddDays(102), holidayList);

                holiday76.Text = holidayCheck(day1.Value.AddDays(105), holidayList);
                holiday77.Text = holidayCheck(day1.Value.AddDays(106), holidayList);
                holiday78.Text = holidayCheck(day1.Value.AddDays(107), holidayList);
                holiday79.Text = holidayCheck(day1.Value.AddDays(108), holidayList);
                holiday80.Text = holidayCheck(day1.Value.AddDays(109), holidayList);

                holiday81.Text = holidayCheck(day1.Value.AddDays(112), holidayList);
                holiday82.Text = holidayCheck(day1.Value.AddDays(113), holidayList);
                holiday83.Text = holidayCheck(day1.Value.AddDays(114), holidayList);
                holiday84.Text = holidayCheck(day1.Value.AddDays(115), holidayList);
                holiday85.Text = holidayCheck(day1.Value.AddDays(116), holidayList);

                holiday86.Text = holidayCheck(day1.Value.AddDays(119), holidayList);
                holiday87.Text = holidayCheck(day1.Value.AddDays(120), holidayList);
                holiday88.Text = holidayCheck(day1.Value.AddDays(121), holidayList);
                holiday89.Text = holidayCheck(day1.Value.AddDays(122), holidayList);
                holiday90.Text = holidayCheck(day1.Value.AddDays(123), holidayList);

                holiday91.Text = holidayCheck(day1.Value.AddDays(126), holidayList);
                holiday92.Text = holidayCheck(day1.Value.AddDays(127), holidayList);
                holiday93.Text = holidayCheck(day1.Value.AddDays(128), holidayList);
                holiday94.Text = holidayCheck(day1.Value.AddDays(129), holidayList);
                holiday95.Text = holidayCheck(day1.Value.AddDays(130), holidayList);

                holiday96.Text = holidayCheck(day1.Value.AddDays(133), holidayList);
                holiday97.Text = holidayCheck(day1.Value.AddDays(134), holidayList);
                holiday98.Text = holidayCheck(day1.Value.AddDays(135), holidayList);
                holiday99.Text = holidayCheck(day1.Value.AddDays(136), holidayList);
                holiday100.Text = holidayCheck(day1.Value.AddDays(137), holidayList);

                holiday101.Text = holidayCheck(day1.Value.AddDays(140), holidayList);
                holiday102.Text = holidayCheck(day1.Value.AddDays(141), holidayList);
                holiday103.Text = holidayCheck(day1.Value.AddDays(142), holidayList);
                holiday104.Text = holidayCheck(day1.Value.AddDays(143), holidayList);
                holiday105.Text = holidayCheck(day1.Value.AddDays(144), holidayList)
                    ;
                holiday106.Text = holidayCheck(day1.Value.AddDays(147), holidayList);
                holiday107.Text = holidayCheck(day1.Value.AddDays(148), holidayList);
                holiday108.Text = holidayCheck(day1.Value.AddDays(149), holidayList);
                holiday109.Text = holidayCheck(day1.Value.AddDays(150), holidayList);
                holiday110.Text = holidayCheck(day1.Value.AddDays(151), holidayList);

            }
        }

        public string holidayCheck(DateTime? thisday, List<THoliday> holiday)
        {
            string name = "";
            string idlv2 = Request.QueryString["idlv2"];
            int? idlv2n = int.Parse(idlv2);

            var target1 = holiday.Where(w => w.dHolidayEnd >= thisday && thisday >= w.dHolidayStart && w.cDel == null && (w.sHolidayAll == "1" || w.sHolidayAll == null)).FirstOrDefault();
            if (target1 != null)
            {
                name = target1.sHoliday;
            }

            var target2 = holiday.Where(w => w.dHolidayEnd >= thisday && thisday >= w.dHolidayStart && w.cDel == null && w.sHolidayAll == "0").FirstOrDefault();
            if (target2 != null)
            {
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {

                    var holisome = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == target2.nHoliday && w.Deleted == null).FirstOrDefault();
                    if (holisome != null)
                    {
                        if (holisome.nTSubLevel == idlv2n)
                        {
                            name = target2.sHoliday;
                        }
                    }
                }

                if (name != "")
                {
                    int length = name.Length;

                    int totalDash = (348 - length) / 2;
                    string dash = "";
                    for (int x = 0; x < totalDash; x++)
                    {
                        dash = dash + "-";
                    }

                    name = dash + " " + name + " " + dash;
                }
            }

            return name;
        }
    }
}