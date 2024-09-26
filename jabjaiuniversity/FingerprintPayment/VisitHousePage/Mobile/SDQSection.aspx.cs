using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.VisitHousePage.Mobile
{
    public partial class SDQSection : System.Web.UI.Page
    {
        protected string StudentPicture = "/Content/VisitHouse/assets/img/user.png";
        protected string StudentCode = "";
        protected string StudentName = "";
        protected string StudentClass = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        private void InitialPage()
        {
            int.TryParse((string)Request.QueryString["schoolid"], out int schoolID);
            int.TryParse((string)Request.QueryString["roomid"], out int roomID);
            int.TryParse((string)Request.QueryString["sid"], out int studentID);

            using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                StudentLogic studentLogic = new StudentLogic(dbSchool);
                string currentTermID = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });

                string query = string.Format(@"
SELECT u.sStudentID 'StudentID', u.sName 'Name', u.sLastname 'Lastname', u.sStudentPicture 'StudentPicture', sv.SubLevel 'Level', sv.nTSubLevel2 'Classroom'
FROM TUser u LEFT JOIN TB_StudentViews sv ON u.SchoolID = sv.SchoolID AND u.sID = sv.sID 
WHERE u.SchoolID = {0} AND u.sID = {1} AND sv.nTerm = '{2}'", schoolID, studentID, currentTermID);
                var studentData = dbSchool.Database.SqlQuery<StudentData>(query).FirstOrDefault();
                if (studentData != null)
                {
                    if (!string.IsNullOrEmpty(studentData.StudentPicture)) StudentPicture = studentData.StudentPicture;

                    StudentCode = studentData.StudentID;
                    StudentName = studentData.Name + " " + studentData.Lastname;
                    var schoolData = mctx.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                    if (schoolData.ClassNameDisable ?? false)
                    {
                        // Hide room
                        StudentClass = studentData.Level;
                    }
                    else
                    {
                        StudentClass = studentData.Level + " / " + studentData.Classroom;
                    }
                }

            }
        }

        public class StudentData
        {
            public string StudentID { get; set; }
            public string Name { get; set; }
            public string Lastname { get; set; }
            public string StudentPicture { get; set; }
            public string Level { get; set; }
            public string Classroom { get; set; }
        }

    }
}