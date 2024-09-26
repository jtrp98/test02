using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiSchoolGradeEntity;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FingerprintPayment.grade
{
    public partial class BP5cover2_print : System.Web.UI.Page
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
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();

            string id = Request.QueryString["id"];
                var data = _db.TPlanes.Where(w => w.sPlaneID.ToString() == id).FirstOrDefault();
            
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
            var schooldata = _dbMaster.TCompanies.Where(w => w.nCompany == nCompany.nCompany).FirstOrDefault();
            
            string idlv = Request.QueryString["idlv"];
            int? idlvn = Int32.Parse(idlv);
            string idlv2 = Request.QueryString["idlv2"];
            int? idlv2n = Int32.Parse(idlv2);
            var room = _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == idlvn).FirstOrDefault();
            var room2 = _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2n).FirstOrDefault();
           
            planid.Text = data.sPlaneID.ToString();
            planname.Text = data.sPlaneName;
            Year2.Text = Request.QueryString["year"];
            Term2.Text = Request.QueryString["term"];
           
            if (schooldata != null)
                schoolpicture.Src = schooldata.sImage;
            txtschool.Text = schooldata.sCompany;          
            Label12.Text = room.SubLevel;          
            Label13.Text = room.nTSubLevel + " / " + room2.nTSubLevel2;

            if (!IsPostBack)
            {
                OpenData();
            }
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
            string idlv = Request.QueryString["idlv"];
            int? idlvn = Int32.Parse(idlv);

            int? useryear = Int32.Parse(year);
            int? idlv2n = Int32.Parse(idlv2);
            int nyear = 0;
            string nterm = "";
            
            var room = _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == idlvn).FirstOrDefault();
            var room2 = _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2n).FirstOrDefault();
            Label12.Text = "บันทึกการประเมินการเรียน " + room.SubLevel + " / " + room2.nTSubLevel2;
            Term.Text = "สัดส่วนเวลาเรียนภาคเรียนที่ " + Request.QueryString["term"];
           
            foreach (var ff in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.numberYear == useryear))
            {
                nyear = ff.nYear;
            }

            foreach (var ee in _db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.sTerm == userterm && w.nYear == nyear && w.cDel == null))
            {
                nterm = ee.nTerm;
            }

            var teach = _db.TTermTimeTables.Where(w =>w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2n && w.nTerm == nterm).FirstOrDefault();
            var name = _db.TEmployees.Where(w => w.sEmp == teach.nTeacher && w.SchoolID == userData.CompanyID).FirstOrDefault();
            teacher.Text = name.sName + " " + name.sLastname;

            int totalstudent = 0;
            var xxx = _dbGrade.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.SchoolID == userData.CompanyID).FirstOrDefault();
            foreach(var a in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == xxx.nGradeId && w.cDel == false))
            {
                totalstudent = totalstudent + 1;
            }
            
            double ratiomid = xxx.fRatioMidTerm;
            double ratiolate = xxx.fRatioLateTerm;
            double ratioquiz = xxx.fRatioQuiz;

            List<int> GradeIdlist = new List<int>();

            List<int> sidlist = new List<int>();
            foreach (var data in _db.TUsers.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2n))
            {
                sidlist.Add(data.sID);
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
               
                double totalScore = 0;
                double maxbehavior = 0;

                var grade = _dbGrade.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.SchoolID == userData.CompanyID).FirstOrDefault();
                var detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == aa && w.sID == aa ).FirstOrDefault();

                
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

                if(detail.getSpecial != null && detail.getSpecial != "")
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
            double sd = ((x2 * totalstudent) - (xbar * xbar)) / (totalstudent * (totalstudent - 1));
            double sd2 = Math.Pow(sd, 1.0 / 2);
            sd2 = Math.Round(sd2, 2);
            xbar = xbar / totalstudent;
            xbar = Math.Round(xbar, 2);

            double cv = (sd2 / xbar) * 100;
            cv = Math.Round(cv, 2);

            std0.Text = total00.ToString();
            std40.Text = total40.ToString();
            std35.Text = total35.ToString();
            std30.Text = total30.ToString();
            std25.Text = total25.ToString();
            std20.Text = total20.ToString();
            std15.Text = total15.ToString();
            std10.Text = total10.ToString();
            txttotalstudent.Text = totalstudent.ToString();
            stdror.Text = ror.ToString();
            stdms.Text = MS.ToString();
            stdmk.Text = MK.ToString();
            stdp.Text = P.ToString();
            stdmp.Text = MP.ToString();
            stdother.Text = Other.ToString();


            std40per.Text = ((total40 * 100) / totalstudent).ToString();
            std35per.Text = ((total35 * 100) / totalstudent).ToString();
            std30per.Text = ((total30 * 100) / totalstudent).ToString();
            std25per.Text = ((total25 * 100) / totalstudent).ToString();
            std20per.Text = ((total20 * 100) / totalstudent).ToString();
            std15per.Text = ((total15 * 100) / totalstudent).ToString();
            std10per.Text = ((total10 * 100) / totalstudent).ToString();
            std0per.Text = ((total00 * 100) / totalstudent).ToString();
            stdrorper.Text = ((ror * 100) / totalstudent).ToString();
            stdmsper.Text = ((MS * 100) / totalstudent).ToString();
            stdmkper.Text = ((MK * 100) / totalstudent).ToString();
            stdpper.Text = ((P * 100) / totalstudent).ToString();
            stdmpper.Text = ((MP * 100) / totalstudent).ToString();
            stdotherper.Text = ((Other * 100) / totalstudent).ToString();


            behavior1.Text = Bone.ToString();
            behavior2.Text = Btwo.ToString();
            behavior3.Text = Bthree.ToString();
            behavior0.Text = Bzero.ToString();


            behavior1per.Text = ((Bone * 100) / totalstudent).ToString() + " %";
            behavior2per.Text = ((Btwo * 100) / totalstudent).ToString() + " %";
            behavior3per.Text = ((Bthree * 100) / totalstudent).ToString() + " %";
            behavior0per.Text = ((Bzero * 100) / totalstudent).ToString() + " %";

            reading1.Text = Rone.ToString();
            reading2.Text = Rtwo.ToString();
            reading3.Text = Rthree.ToString();
            reading0.Text = Rzero.ToString();

            reading1per.Text = ((Rone * 100) / totalstudent).ToString() + " %";
            reading2per.Text = ((Rtwo * 100) / totalstudent).ToString() + " %";
            reading3per.Text = ((Rthree * 100) / totalstudent).ToString() + " %";
            reading0per.Text = ((Rzero * 100) / totalstudent).ToString() + " %";



        }

    }

}