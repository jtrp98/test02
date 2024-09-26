using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FingerprintPayment.grade
{
    public partial class BP5cover3_print : System.Web.UI.Page
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
            string _page = HttpContext.Current.Request.CurrentExecutionFilePath.ToString();
            if (_page.IndexOf("useraddmoney") == -1 && _page.IndexOf("pagesellproduct") == -1)
            {
                //fcommon.ExecuteNonQuery(fcommon.connMaster, "UPDATE TConnect SET cStatus = '0' WHERE nConnectID = (SELECT TOP 1 nConnectID FROM TConnect ORDER BY nConnectID DESC)");
            }
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                if (_db.TEmployees.Count() == 0)
                {

                }
                else if (HttpContext.Current.Request.Url.ToString().IndexOf("checkmoney.aspx") != -1)
                {

                }
                else if (string.IsNullOrEmpty(Session["sEmpID"] + ""))
                {
                    Response.Redirect("/Default.aspx");
                }
                else
                {
                    int sEmpID = int.Parse(Session["sEmpID"] + "");
                    foreach (var _data in _db.TEmployees.Where(w => w.sEmp == sEmpID && w.SchoolID == userData.CompanyID))
                    {

                    }
                }
               

                string id = Request.QueryString["id"];
                //var data = _db.TPlanes.Where(w => w.sPlaneID == id).FirstOrDefault();

                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var schooldata = _dbMaster.TCompanies.Where(w => w.nCompany == nCompany.nCompany).FirstOrDefault();
                if (schooldata != null)
                    schoolpicture.Src = schooldata.sImage;
                txtschool.Text = schooldata.sCompany;
                txtaumpher.Text = "อำเภอ" + schooldata.sAumpher + " ";
                txtprovince.Text = "จงหวัด" + schooldata.sProvince + " ";
                string idlv = Request.QueryString["idlv"];
                int? idlvn = Int32.Parse(idlv);
                string idlv2 = Request.QueryString["idlv2"];
                int? idlv2n = Int32.Parse(idlv2);
                var room = _db.TSubLevels.Where(w => w.nTSubLevel == idlvn && w.SchoolID == userData.CompanyID).FirstOrDefault();
                // Label12.Text = room.SubLevel;
                var room2 = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
                // Label13.Text = room.nTSubLevel + " / " + room2.nTSubLevel2;

                if (!IsPostBack)
                {
                    OpenData();
                }
            }
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
                int nyear = 0;
                string nterm = "";

                foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear && w.SchoolID == userData.CompanyID))
                {
                    nyear = ff.nYear;
                }

                foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.SchoolID == userData.CompanyID && w.cDel == null))
                {
                    nterm = ee.nTerm;
                }

                var teach = _db.TTermTimeTables.Where(w => w.nTermSubLevel2 == idlv2n && w.nTerm == nterm && w.SchoolID == userData.CompanyID).FirstOrDefault();
                var name = _db.TEmployees.Where(w => w.sEmp == teach.nTeacher && w.SchoolID == userData.CompanyID).FirstOrDefault();

                List<int> GradeIdlist = new List<int>();

                List<int> sidlist = new List<int>();
                foreach (var data in _db.TUser.Where(w => w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID))
                {
                    sidlist.Add(data.sID);
                }
                /*
                foreach (var a in sidlist)
                {

                    foreach (var xx in _db.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID == id))
                    {
                        totalstudent = totalstudent + 1;
                        ratiomid = xx.fRatioMidTerm;
                        ratiolate = xx.fRatioLateTerm;
                        ratioquiz = xx.fRatioQuiz;
                        GradeIdlist.Add(xx.nGradeId);
                    }
                }



                int total40 = 0;
                int total35 = 0;
                int total30 = 0;
                int total25 = 0;
                int total20 = 0;
                int total15 = 0;
                int total10 = 0;
                int total00 = 0;

                double ror = 0;
                double MS = 0;
                double MK = 0;
                double P = 0;
                double MP = 0;
                double Other = 0;


                double xbar = 0;
                double x2 = 0;

                int Bthree = 0;
                int Btwo = 0;
                int Bone = 0;
                int Bzero = 0;

                int Rthree = 0;
                int Rtwo = 0;
                int Rone = 0;
                int Rzero = 0;
                foreach (var aa in GradeIdlist)
                {
                    double maxquiz = 0;
                    double getquiz = 0;
                    double maxmid = 0;
                    double getmid = 0;
                    double maxlate = 0;
                    double getlate = 0;
                    double totalMid = 0;
                    double totalLate = 0;
                    double totalQuiz = 0;
                    double totalScore = 0;





                    foreach (var xx in _db.TGradeDetails.Where(w => w.nGradeId == aa))
                    {

                        double maxquiz = 0;

                        double totalScore = 0;
                        double maxbehavior = 0;

                        var grade = _db.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID == id).FirstOrDefault();
                        var detail = _db.TGradeDetails.Where(w => w.nGradeId == aa && w.nStudentId == aa).FirstOrDefault();


                        if (detail.getBehaviorLabel == "0")
                            Bzero = Bzero + 1;
                        else if (detail.getBehaviorLabel == "1")
                            Bone = Bone + 1;
                        else if (detail.getBehaviorLabel == "2")
                            Btwo = Btwo + 1;
                        else if (detail.getBehaviorLabel == "3")
                            Bthree = Bthree + 1;

                        if (detail.getReadWrite == "0")
                            Rzero = Rzero + 1;
                        else if (detail.getReadWrite == "1")
                            Rone = Rone + 1;
                        else if (detail.getReadWrite == "2")
                            Rtwo = Rtwo + 1;
                        else if (detail.getReadWrite == "3")
                            Rthree = Rthree + 1;

                        if (detail.getSpecial != null && detail.getSpecial != "")
                        {
                            if (detail.getSpecial == "1")
                                ror = ror + 1;
                            if (detail.getSpecial == "2")
                                MS = MS + 1;
                            if (detail.getSpecial == "3")
                                MK = MK + 1;
                            if (detail.getSpecial == "4")
                                P = P + 1;
                            if (detail.getSpecial == "5")
                                MP = MP + 1;
                            if (detail.getSpecial == "6")
                                Other = Other + 1;
                        }

                        maxquiz = Double.Parse(grade.maxGradeTotal);
                        maxbehavior = Double.Parse(grade.maxBehaviorTotal);

                        totalScore = Double.Parse(detail.getScore100);
                        xbar = xbar + totalScore;
                        x2 = x2 + (totalScore * totalScore);

                        if (totalScore > 79)
                            total40 = total40 + 1;
                        else if (totalScore < 80 && totalScore > 74)
                            total35 = total35 + 1;
                        else if (totalScore < 75 && totalScore > 69)
                            total30 = total30 + 1;
                        else if (totalScore < 70 && totalScore > 64)
                            total25 = total25 + 1;
                        else if (totalScore < 65 && totalScore > 59)
                            total20 = total20 + 1;
                        else if (totalScore < 60 && totalScore > 54)
                            total15 = total15 + 1;
                        else if (totalScore < 55 && totalScore > 49)
                            total10 = total10 + 1;
                        else total00 = total00 + 1;
                    }

                    totalMid = (getmid * 100) / maxmid;
                    totalMid = (totalMid * ratiomid) / 100;
                    totalLate = (getlate * 100) / maxlate;
                    totalLate = (totalLate * ratiolate) / 100;
                    totalQuiz = (getquiz * 100) / maxquiz;
                    totalQuiz = (totalQuiz * ratioquiz) / 100;
                    totalScore = totalMid + totalLate + totalQuiz;
                    xbar = xbar + totalScore;
                    x2 = x2 + (totalScore * totalScore);

                    if (totalScore > 79)
                        total40 = total40 + 1;
                    else if (totalScore < 80 && totalScore > 74)
                        total35 = total35 + 1;
                    else if (totalScore < 75 && totalScore > 69)
                        total30 = total30 + 1;
                    else if (totalScore < 70 && totalScore > 64)
                        total25 = total25 + 1;
                    else if (totalScore < 65 && totalScore > 59)
                        total20 = total20 + 1;
                    else if (totalScore < 60 && totalScore > 54)
                        total15 = total15 + 1;
                    else if (totalScore < 55 && totalScore > 49)
                        total10 = total10 + 1;
                    else total00 = total00 + 1;
                }
                double sd = ((x2 * totalstudent) - (xbar * xbar)) / (totalstudent * (totalstudent - 1));
                double sd2 = Math.Pow(sd, 1.0 / 2);
                sd2 = Math.Round(sd2, 2);
                xbar = xbar / totalstudent;
                xbar = Math.Round(xbar, 2);

                double cv = (sd2 / xbar) * 100;
                cv = Math.Round(cv, 2);


        */
            }
        }
    }
}