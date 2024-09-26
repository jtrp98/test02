using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.StudentInfo
{
    // Ref : RetestStudentListByClassRoom
    public partial class StudentReportFailExam01 : StudentGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            int schoolID = UserData.CompanyID;
            JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID));

            // Get current year
            StudentLogic studentLogic = new StudentLogic(en);
            string currentTerm = studentLogic.GetTermId(UserData);
            int yearID = 0;
            var term = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == currentTerm && w.cDel == null).FirstOrDefault();
            if (term != null)
            {
                yearID = term.nYear.Value;
            }

            var listYear = en.TYears.Where(w => w.SchoolID == schoolID).OrderByDescending(x => x.numberYear).ToList();
            foreach (var l in listYear)
            {
                if (l.nYear == yearID)
                {
                    this.ltrYear.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                }
                else
                {
                    this.ltrYear.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                }

                if (yearID == 0) yearID = l.nYear;
            }

            if (yearID != 0)
            {
                var listTerm = en.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == yearID && w.cDel == null).OrderByDescending(o => o.nTerm).ToList();
                foreach (var l in listTerm)
                {
                    if (l.nTerm.Trim() == currentTerm)
                    {
                        this.ltrTerm.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nTerm, l.sTerm);
                    }
                    else
                    {
                        this.ltrTerm.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nTerm, l.sTerm);
                    }
                }
            }

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadStudent()
        {
            var jsonStream = "";
            HttpContext.Current.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                jsonStream = inputStream.ReadToEnd();
            }
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            int draw = Convert.ToInt32(jsonObject["draw"]);
            int pageIndex = Convert.ToInt32(jsonObject["page"]);
            int pageSize = Convert.ToInt32(jsonObject["length"]);
            string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
            string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

            string sortBy = "sID";
            switch (sortIndex)
            {
                case "1": sortBy = "Level"; break;
                case "2": sortBy = "ClassRoom"; break;
                case "3": sortBy = "StudentID"; break;
                case "4": sortBy = "CourseCode"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            string year = Convert.ToString(jsonObject["year"]);
            string term = Convert.ToString(jsonObject["term"]);
            string grade = Convert.ToString(jsonObject["grade"]);

            var json = QueryEngine.LoadStudentReportFailExam01JsonData(draw, pageIndex, pageSize, sortBy, GetUserData().CompanyID, year, term, grade);

            return json;
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadTerm(string yearID)
        {
            List<EntityDropdown> result = null;

            if (!string.IsNullOrEmpty(yearID))
            {
                int schoolID = GetUserData().CompanyID;
                JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID));

                int yID = Convert.ToInt32(yearID);

                result = dbschool.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == yID && w.cDel == null).OrderByDescending(o => o.nTerm).Select(s => new EntityDropdown { id = s.nTerm, name = s.sTerm }).ToList();
            }

            return result;
        }

    }
}