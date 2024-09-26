using FingerprintPayment.Message.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Message
{
    public partial class NewsReportList : MessageGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                // Get current year
                StudentLogic studentLogic = new StudentLogic(en);
                string currentTerm = studentLogic.GetTermId(UserData);
                int yearID = 0;
                var term = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == currentTerm && w.cDel == null).FirstOrDefault();
                if (term != null)
                {
                    yearID = term.nYear.Value;
                }

                string query = string.Format(@"
SELECT y.nYear 'YearID', y.numberYear 'Year', MIN(t.dStart) 'StartDate', MAX(t.dEnd) 'EndDate'
FROM TYear y RIGHT JOIN TTerm t ON y.nYear = t.nYear
WHERE y.SchoolID={0} AND y.cDel = 0 AND t.cDel IS NULL
GROUP BY y.nYear, y.numberYear", schoolID);
                List<EntityYear> listYear = en.Database.SqlQuery<EntityYear>(query).ToList();

                //var listYear = en.TYears.Where(w => w.SchoolID == schoolID).OrderByDescending(x => x.numberYear).ToList();
                foreach (var l in listYear)
                {
                    if (l.YearID == yearID)
                    {
                        this.ltrYear.Text += string.Format(@"<option selected=""selected"" value=""{0}"" data-start=""{2}"" data-end=""{3}"">{1}</option>", l.YearID, l.Year, l.StartDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), l.EndDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    }
                    else
                    {
                        this.ltrYear.Text += string.Format(@"<option value=""{0}"" data-start=""{2}"" data-end=""{3}"">{1}</option>", l.YearID, l.Year, l.StartDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), l.EndDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    }

                    if (yearID == 0) yearID = l.YearID;
                }

                if (yearID != 0)
                {
                    this.ltrTerm.Text = string.Format(@"<option value="""" data-start="""" data-end="""">ทั้งหมด</option>");

                    var listTerm = en.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == yearID && w.cDel == null).OrderByDescending(o => o.nTerm).ToList();
                    foreach (var l in listTerm)
                    {
                        if (l.nTerm.Trim() == currentTerm)
                        {
                            this.ltrTerm.Text += string.Format(@"<option selected=""selected"" value=""{0}"" data-start=""{2}"" data-end=""{3}"">{1}</option>", l.nTerm, l.sTerm, l.dStart?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), l.dEnd?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                        }
                        else
                        {
                            this.ltrTerm.Text += string.Format(@"<option value=""{0}"" data-start=""{2}"" data-end=""{3}"">{1}</option>", l.nTerm, l.sTerm, l.dStart?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), l.dEnd?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                        }
                    }
                }
            }

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object LoadTerm(string yearID)
        {
            List<EntityTerm> results = null;

            if (!string.IsNullOrEmpty(yearID))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    int yID = Convert.ToInt32(yearID);

                    results = dbschool.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == yID && w.cDel == null).OrderByDescending(o => o.nTerm).Select(s => new EntityTerm { TermID = s.nTerm, Term = s.sTerm, dStartDate = s.dStart, dEndDate = s.dEnd }).ToList();

                    foreach (var r in results)
                    {
                        r.StartDate = r.dStartDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        r.EndDate = r.dEndDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    }
                }
            }

            return JsonConvert.SerializeObject(results);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadNews()
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

            string sortBy = "SmsID";
            switch (sortIndex)
            {
                case "1": sortBy = "SendDate"; break;
                case "2": sortBy = "Recorder"; break;
                case "3": sortBy = "Type"; break;
                case "4": sortBy = "Title"; break;
                case "5": sortBy = "Receiver"; break;
                case "6": sortBy = "Duration"; break;
                case "7": sortBy = "News"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            string startDate = Convert.ToString(jsonObject["startDate"]);
            string endDate = Convert.ToString(jsonObject["endDate"]);
            string sender = Convert.ToString(jsonObject["sender"]);

            var json = QueryEngine.LoadNewsJsonData(draw, pageIndex, pageSize, sortBy, GetUserData().CompanyID, startDate, endDate, sender);

            return json;
        }

    }
}