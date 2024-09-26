using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.StudentInfo
{
    public partial class StdHealth : StudentGateway
    {
        private static string SessionPrimaryKey = "STUDENTID";
        private static string SessionHealthPrimaryKey = "HEALTHID";

        private static string[] InterLevel = new string[] { "P.1", "P.2", "P.3", "P.4", "P.5", "P.6" };

        protected string GenerateGraph = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        #region Method

        private void InitialPage()
        {
            string v = Request.QueryString["v"];
            switch (v)
            {
                case "list":
                    MvContent.ActiveViewIndex = 0; break;
                case "form":
                    MvContent.ActiveViewIndex = 1;

                    int sID = Convert.ToInt32(Request.QueryString["sid"]);
                    string tID = Request.QueryString["tid"].ToString();

                    GenerateHealthTable(sID, tID);

                    break;
                case "view":
                    MvContent.ActiveViewIndex = 2;

                    int sID2 = Convert.ToInt32(Request.QueryString["sid"]);
                    string tID2 = Request.QueryString["tid"].ToString();

                    GenerateHealthTableView(sID2, tID2);

                    break;
            }
        }

        public void GenerateHealthTable(int sID, string termID)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                JabjaiEntity.DB.TUser user = en.TUser.Where(w => w.SchoolID == schoolID && w.sID == sID && w.cDel == null).FirstOrDefault();
                if (user != null)
                {
                    var health = en.TStudentHealthInfoes.Where(w => w.SchoolID == schoolID && w.sID == sID && w.sDeleted == "false").FirstOrDefault();
                    var sch = en.TStudentClassroomHistories.Where(w => w.sID == sID && w.nTerm == termID).FirstOrDefault();
                    int? roomID = 0;
                    if (sch != null)
                    {
                        roomID = sch.nTermSubLevel2;
                    }
                    else
                    {
                        roomID = user.nTermSubLevel2;
                    }

                    var room = en.TTermSubLevel2.Where(w => w.SchoolID == schoolID && w.nTermSubLevel2 == roomID).FirstOrDefault();
                    var level = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == room.nTSubLevel).FirstOrDefault();

                    string sqlQueryNewOrderSubLevel = string.Format(@"
SELECT CAST(ROW_NUMBER() OVER (ORDER BY fullName)  AS INT) 'CurrentYear', nTSubLevel 'LevelID'
FROM TSubLevel 
WHERE SchoolID = {0} AND nTLevel IN ({1}) AND nDeleted = 0 AND nWorkingStatus = 1 {2}
ORDER BY fullName
", schoolID, level.nTLevel, (Array.IndexOf(InterLevel, level.SubLevel) != -1 ? " AND SubLevel IN ('" + String.Join("', '", InterLevel) + "')" : ""));
                    List<EntityNewOrderSubLevel> newOrderSubLevels = en.Database.SqlQuery<EntityNewOrderSubLevel>(sqlQueryNewOrderSubLevel).ToList();

                    int currentEduYearOrderSubLevel = 0;
                    if (newOrderSubLevels.Count > 0)
                    {
                        var newOrderSubLevel = newOrderSubLevels.Where(w => w.LevelID == room.nTSubLevel).FirstOrDefault();
                        if (newOrderSubLevel != null)
                        {
                            currentEduYearOrderSubLevel = newOrderSubLevel.CurrentYear;
                        }
                    }

                    string sqlQuery = string.Format(@"
SELECT l.nTSubLevel 'LevelID', l.SubLevel 'SubLevel', l.fullName 'FullName', CAST(ROW_NUMBER() OVER (ORDER BY l.fullName) AS INT) - {0} 'CurrentYear'
, MAX(CASE g.nMonth WHEN 5 THEN g.Weight ELSE NULL END) 'Weight5', MAX(CASE g.nMonth WHEN 5 THEN g.Height ELSE NULL END) 'Height5'
, MAX(CASE g.nMonth WHEN 8 THEN g.Weight ELSE NULL END) 'Weight8', MAX(CASE g.nMonth WHEN 8 THEN g.Height ELSE NULL END) 'Height8'
, MAX(CASE g.nMonth WHEN 11 THEN g.Weight ELSE NULL END) 'Weight11', MAX(CASE g.nMonth WHEN 11 THEN g.Height ELSE NULL END) 'Height11'
, MAX(CASE g.nMonth WHEN 2 THEN g.Weight ELSE NULL END) 'Weight2', MAX(CASE g.nMonth WHEN 2 THEN g.Height ELSE NULL END) 'Height2'
FROM TSubLevel l 
LEFT JOIN (
	SELECT * FROM TStudentHealthGrowth WHERE SchoolID = {3} AND nHealthID = {2}
) g ON l.nTSubLevel = g.nTSubLevel
WHERE l.nTLevel IN ({1}) AND l.SchoolID = {3} AND nDeleted = 0 AND nWorkingStatus = 1 {4}
GROUP BY l.nTSubLevel, l.SubLevel, l.fullName
ORDER BY l.fullName ", currentEduYearOrderSubLevel, level.nTLevel, (health == null ? 0 : health.nHealthID), schoolID, (Array.IndexOf(InterLevel, level.SubLevel) != -1 ? " AND l.SubLevel IN ('" + String.Join("', '", InterLevel) + "')" : ""));
                    List<EntityStudentHealthGrowth> result = en.Database.SqlQuery<EntityStudentHealthGrowth>(sqlQuery).ToList();

                    string headLevel = "";
                    string bodyMonth = "";
                    string bodyAge = "";
                    string bodyInputWeight = "";
                    string bodyInputHeight = "";

                    string graphLabel = "";
                    string graphData = "";

                    DateTime nowDate = DateTime.Today;
                    int[] quaterMonth = new int[] { 5, 8, 11, 2 };
                    int[] quaterYear = new int[] { 0, 0, 0, 1 };
                    string highlight = "";
                    foreach (var r in result)
                    {
                        if (r.CurrentYear == 0) { highlight = "highlight"; } else { highlight = ""; }

                        headLevel += string.Format(@"<th scope=""col"" colspan=""4"" class=""{1}"">{0}</th>", r.FullName, highlight);
                        bodyMonth += string.Format(@"<td class=""{0}"">พ.ค.</td><td class=""{0}"">ส.ค.</td><td class=""{0}"">พ.ย.</td><td class=""{0}"">ก.พ.</td>", highlight);

                        int iQuaterYear = 0;
                        foreach (var qm in quaterMonth)
                        {
                            DateTime runQuater = new DateTime(nowDate.Year + r.CurrentYear + quaterYear[iQuaterYear++], qm + 1, 1).AddDays(-1);

                            var dateSpan = DateTimeSpan.CompareDates(user.dBirth.Value, runQuater);
                            bodyAge += string.Format(@"<td class=""{2}""><span>{0} ปี<br />{1} ด.</span></td>", dateSpan.Years, dateSpan.Months, highlight);

                            graphLabel += string.Format(@",""{0}/{1} """, dateSpan.Years, dateSpan.Months);

                            decimal? weight = 0;
                            decimal? height = 0;
                            switch (qm)
                            {
                                case 5: weight = r.Weight5; height = r.Height5; break;
                                case 8: weight = r.Weight8; height = r.Height8; break;
                                case 11: weight = r.Weight11; height = r.Height11; break;
                                case 2: weight = r.Weight2; height = r.Height2; break;
                            }
                            bodyInputWeight += string.Format(@"<td class=""{3}""><input data-type=""weight"" data-level=""{0}"" data-month=""{1}"" value=""{2}"" type=""text"" class=""form-control input-health"" placeholder=""0.00"" maxlength=""6"" /></td>", r.LevelID, qm, weight, highlight);
                            bodyInputHeight += string.Format(@"<td class=""{3}""><input data-type=""height"" data-level=""{0}"" data-month=""{1}"" value=""{2}"" type=""text"" class=""form-control input-health"" placeholder=""0.00"" maxlength=""6"" /></td>", r.LevelID, qm, height, highlight);

                            decimal? bmi = 0;
                            if (height != null && height > 0)
                            {
                                bmi = weight / ((height * 0.01M) * (height * 0.01M));
                            }
                            graphData += string.Format(@",{0:0.00}", bmi == null ? 0 : bmi);
                        }
                    }

                    this.ltrTable.Text = string.Format(@"
                            <table class=""table table-bordered table-cell-center {5}"">
                                <thead>
                                    <tr class=""bg-primary"">
                                        <th scope=""col"">ชั้นปี</th>
                                        {0}
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class=""bg-primary"">
                                        <th scope=""row"">เดือน</th>
                                        {1}
                                    </tr>
                                    <tr class=""age"">
                                        <th scope=""row"" style=""vertical-align: inherit;"">อายุ (ปี/เดือน)</th>
                                        {2}
                                    </tr>
                                    <tr>
                                        <th scope=""row"">น้ำหนัก (กก.)</th>
                                        {3}
                                    </tr>
                                    <tr>
                                        <th scope=""row"">ส่วนสูง (ซม.)</th>
                                        {4}
                                    </tr>
                                </tbody>
                            </table>", headLevel, bodyMonth, bodyAge, bodyInputWeight, bodyInputHeight, result.Count > 3 ? "level-n-6" : "");

                    // bmi : weight / ((height * 0.01) * (height * 0.01))
                    GenerateGraph = string.Format(@"drawHealthGraph([{0}], [{1}]);", graphLabel.Remove(0, 1), graphData.Remove(0, 1));
                }
            }
        }

        public void GenerateHealthTableView(int sID, string termID)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                JabjaiEntity.DB.TUser user = en.TUser.Where(w => w.SchoolID == schoolID && w.sID == sID && w.cDel == null).FirstOrDefault();
                if (user != null)
                {
                    var health = en.TStudentHealthInfoes.Where(w => w.SchoolID == schoolID && w.sID == sID && w.sDeleted == "false").FirstOrDefault();
                    var sch = en.TStudentClassroomHistories.Where(w => w.sID == sID && w.nTerm == termID).FirstOrDefault();
                    int? roomID = 0;
                    if (sch != null)
                    {
                        roomID = sch.nTermSubLevel2;
                    }
                    else
                    {
                        roomID = user.nTermSubLevel2;
                    }

                    var room = en.TTermSubLevel2.Where(w => w.SchoolID == schoolID && w.nTermSubLevel2 == roomID).FirstOrDefault();
                    var level = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == room.nTSubLevel).FirstOrDefault();

                    string sqlQueryNewOrderSubLevel = string.Format(@"
SELECT CAST(ROW_NUMBER() OVER (ORDER BY fullName)  AS INT) 'CurrentYear', nTSubLevel 'LevelID'
FROM TSubLevel 
WHERE SchoolID = {0} AND nTLevel IN ({1}) AND nDeleted = 0 AND nWorkingStatus = 1 {2}
ORDER BY fullName
", schoolID, level.nTLevel, (Array.IndexOf(InterLevel, level.SubLevel) != -1 ? " AND SubLevel IN ('" + String.Join("', '", InterLevel) + "')" : ""));
                    List<EntityNewOrderSubLevel> newOrderSubLevels = en.Database.SqlQuery<EntityNewOrderSubLevel>(sqlQueryNewOrderSubLevel).ToList();

                    int currentEduYearOrderSubLevel = 0;
                    if (newOrderSubLevels.Count > 0)
                    {
                        var newOrderSubLevel = newOrderSubLevels.Where(w => w.LevelID == room.nTSubLevel).FirstOrDefault();
                        if (newOrderSubLevel != null)
                        {
                            currentEduYearOrderSubLevel = newOrderSubLevel.CurrentYear;
                        }
                    }

                    string sqlQuery = string.Format(@"
SELECT l.nTSubLevel 'LevelID', l.SubLevel 'SubLevel', l.fullName 'FullName', CAST(ROW_NUMBER() OVER (ORDER BY l.fullName) AS INT) - {0} 'CurrentYear'
, MAX(CASE g.nMonth WHEN 5 THEN g.Weight ELSE NULL END) 'Weight5', MAX(CASE g.nMonth WHEN 5 THEN g.Height ELSE NULL END) 'Height5'
, MAX(CASE g.nMonth WHEN 8 THEN g.Weight ELSE NULL END) 'Weight8', MAX(CASE g.nMonth WHEN 8 THEN g.Height ELSE NULL END) 'Height8'
, MAX(CASE g.nMonth WHEN 11 THEN g.Weight ELSE NULL END) 'Weight11', MAX(CASE g.nMonth WHEN 11 THEN g.Height ELSE NULL END) 'Height11'
, MAX(CASE g.nMonth WHEN 2 THEN g.Weight ELSE NULL END) 'Weight2', MAX(CASE g.nMonth WHEN 2 THEN g.Height ELSE NULL END) 'Height2'
FROM TSubLevel l 
LEFT JOIN (
	SELECT * FROM TStudentHealthGrowth WHERE SchoolID = {3} AND nHealthID = {2}
) g ON l.nTSubLevel = g.nTSubLevel
WHERE l.nTLevel IN ({1}) AND l.SchoolID = {3} AND nDeleted = 0 AND nWorkingStatus = 1 {4}
GROUP BY l.nTSubLevel, l.SubLevel, l.fullName
ORDER BY l.fullName ", currentEduYearOrderSubLevel, level.nTLevel, (health == null ? 0 : health.nHealthID), schoolID, (Array.IndexOf(InterLevel, level.SubLevel) != -1 ? " AND l.SubLevel IN ('" + String.Join("', '", InterLevel) + "')" : ""));
                    List<EntityStudentHealthGrowth> result = en.Database.SqlQuery<EntityStudentHealthGrowth>(sqlQuery).ToList();

                    string headLevel = "";
                    string bodyMonth = "";
                    string bodyAge = "";
                    string bodyInputWeight = "";
                    string bodyInputHeight = "";

                    string graphLabel = "";
                    string graphData = "";

                    DateTime nowDate = DateTime.Today;
                    int[] quaterMonth = new int[] { 5, 8, 11, 2 };
                    int[] quaterYear = new int[] { 0, 0, 0, 1 };
                    string highlight = "";
                    foreach (var r in result)
                    {
                        //if (r.CurrentYear == 0) { highlight = "highlight"; } else { highlight = ""; }

                        headLevel += string.Format(@"<th scope=""col"" colspan=""4"" class=""{1}"">{0}</th>", r.FullName, highlight);
                        bodyMonth += string.Format(@"<td class=""{0}"">พ.ค.</td><td class=""{0}"">ส.ค.</td><td class=""{0}"">พ.ย.</td><td class=""{0}"">ก.พ.</td>", highlight);

                        int iQuaterYear = 0;
                        foreach (var qm in quaterMonth)
                        {
                            DateTime runQuater = new DateTime(nowDate.Year + r.CurrentYear + quaterYear[iQuaterYear++], qm + 1, 1).AddDays(-1);

                            var dateSpan = DateTimeSpan.CompareDates(user.dBirth.Value, runQuater);
                            bodyAge += string.Format(@"<td class=""{2}""><span>{0} ปี<br />{1} ด.</span></td>", dateSpan.Years, dateSpan.Months, highlight);

                            graphLabel += string.Format(@",""{0}/{1} """, dateSpan.Years, dateSpan.Months);

                            decimal? weight = 0;
                            decimal? height = 0;
                            switch (qm)
                            {
                                case 5: weight = r.Weight5; height = r.Height5; break;
                                case 8: weight = r.Weight8; height = r.Height8; break;
                                case 11: weight = r.Weight11; height = r.Height11; break;
                                case 2: weight = r.Weight2; height = r.Height2; break;
                            }
                            bodyInputWeight += string.Format(@"<td class=""{3}""><span data-type=""weight"" data-level=""{0}"" data-month=""{1}"" type=""text"" class=""input-health-view"">{2}</span></td>", r.LevelID, qm, weight, highlight);
                            bodyInputHeight += string.Format(@"<td class=""{3}""><span data-type=""height"" data-level=""{0}"" data-month=""{1}"" type=""text"" class=""input-health-view"">{2}</span></td>", r.LevelID, qm, height, highlight);

                            decimal? bmi = 0;
                            if (height != null && height > 0)
                            {
                                bmi = weight / ((height * 0.01M) * (height * 0.01M));
                            }
                            graphData += string.Format(@",{0:0.00}", bmi == null ? 0 : bmi);
                        }
                    }

                    this.ltrTableView.Text = string.Format(@"
                            <table class=""table table-bordered table-cell-center {5}"">
                                <thead>
                                    <tr class=""bg-primary"">
                                        <th scope=""col"">ชั้นปี</th>
                                        {0}
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class=""bg-primary"">
                                        <th scope=""row"">เดือน</th>
                                        {1}
                                    </tr>
                                    <tr class=""age"">
                                        <th scope=""row"" style=""vertical-align: inherit;"">อายุ (ปี/เดือน)</th>
                                        {2}
                                    </tr>
                                    <tr>
                                        <th scope=""row"">น้ำหนัก (กก.)</th>
                                        {3}
                                    </tr>
                                    <tr>
                                        <th scope=""row"">ส่วนสูง (ซม.)</th>
                                        {4}
                                    </tr>
                                </tbody>
                            </table>", headLevel, bodyMonth, bodyAge, bodyInputWeight, bodyInputHeight, result.Count > 3 ? "level-n-6" : "");

                    // bmi : weight / ((height * 0.01) * (height * 0.01))
                    GenerateGraph = string.Format(@"drawHealthGraph([{0}], [{1}]);", graphLabel.Remove(0, 1), graphData.Remove(0, 1));
                }
            }
        }

        [WebMethod]
        public static string GetItem(string stdID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    TStudentHealthInfo p = en.TStudentHealthInfoes.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 6; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        //HttpContext.Current.Session[SessionHealthPrimaryKey] = p.nHealthID;

                        dt.Rows[0]["F0"] = p.nHealthID;
                        dt.Rows[0]["F1"] = p.sBlood;
                        dt.Rows[0]["F2"] = p.sSickFood;
                        dt.Rows[0]["F3"] = p.sSickDrug;
                        dt.Rows[0]["F4"] = p.sSickOther;
                        dt.Rows[0]["F5"] = p.sSickNormal;
                        dt.Rows[0]["F6"] = p.sSickDanger;

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                //if (infor == "new" || infor == "error") HttpContext.Current.Session[SessionHealthPrimaryKey] = null;

                return infor;
            }
        }

        [WebMethod]
        public static string ReloadHealthGraph(string stdID, string termID)
        {
            bool success = true;

            List<string> graphLabel = new List<string>();
            List<decimal> graphData = new List<decimal>();

            try
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {

                    if (!int.TryParse(stdID, out int sID)) { sID = 0; }

                    JabjaiEntity.DB.TUser user = en.TUser.Where(w => w.SchoolID == schoolID && w.sID == sID && w.cDel == null).FirstOrDefault();
                    if (user != null)
                    {
                        var health = en.TStudentHealthInfoes.Where(w => w.SchoolID == schoolID && w.sID == sID && w.sDeleted == "false").FirstOrDefault();
                        var sch = en.TStudentClassroomHistories.Where(w => w.sID == sID && w.nTerm == termID).FirstOrDefault();
                        int? roomID = 0;
                        if (sch != null)
                        {
                            roomID = sch.nTermSubLevel2;
                        }
                        else
                        {
                            roomID = user.nTermSubLevel2;
                        }

                        var room = en.TTermSubLevel2.Where(w => w.SchoolID == schoolID && w.nTermSubLevel2 == roomID).FirstOrDefault();
                        var level = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == room.nTSubLevel).FirstOrDefault();

                        string sqlQuery = string.Format(@"
SELECT l.nTSubLevel 'LevelID', l.SubLevel 'SubLevel', l.fullName 'FullName', CAST(ROW_NUMBER() OVER (ORDER BY l.nTSubLevel) - 1 AS INT) 'CurrentYear' --, l.nTSubLevel - {0} 'CurrentYear'
, MAX(CASE g.nMonth WHEN 5 THEN g.Weight ELSE NULL END) 'Weight5', MAX(CASE g.nMonth WHEN 5 THEN g.Height ELSE NULL END) 'Height5'
, MAX(CASE g.nMonth WHEN 8 THEN g.Weight ELSE NULL END) 'Weight8', MAX(CASE g.nMonth WHEN 8 THEN g.Height ELSE NULL END) 'Height8'
, MAX(CASE g.nMonth WHEN 11 THEN g.Weight ELSE NULL END) 'Weight11', MAX(CASE g.nMonth WHEN 11 THEN g.Height ELSE NULL END) 'Height11'
, MAX(CASE g.nMonth WHEN 2 THEN g.Weight ELSE NULL END) 'Weight2', MAX(CASE g.nMonth WHEN 2 THEN g.Height ELSE NULL END) 'Height2'
FROM TSubLevel l 
LEFT JOIN (
	SELECT * FROM TStudentHealthGrowth WHERE SchoolID = {3} AND nHealthID = {2}
) g ON l.nTSubLevel = g.nTSubLevel
WHERE l.nTLevel IN ({1}) AND l.SchoolID = {3} {4}
GROUP BY l.nTSubLevel, l.SubLevel, l.fullName, l.nTSubLevel - {0}
ORDER BY l.nTSubLevel ", room.nTSubLevel, level.nTLevel, (health == null ? 0 : health.nHealthID), schoolID, (Array.IndexOf(InterLevel, level.SubLevel) != -1 ? " AND l.SubLevel IN ('" + String.Join("', '", InterLevel) + "')" : ""));
                        List<EntityStudentHealthGrowth> listGrowth = en.Database.SqlQuery<EntityStudentHealthGrowth>(sqlQuery).ToList();

                        DateTime nowDate = DateTime.Today.AddYears(-1);
                        int[] quaterMonth = new int[] { 5, 8, 11, 2 };
                        int[] quaterYear = new int[] { 0, 0, 0, 1 };

                        foreach (var r in listGrowth)
                        {
                            int iQuaterYear = 0;
                            foreach (var qm in quaterMonth)
                            {
                                DateTime runQuater = new DateTime(nowDate.Year + r.CurrentYear + quaterYear[iQuaterYear++], qm, nowDate.Day);

                                var dateSpan = DateTimeSpan.CompareDates(user.dBirth.Value, runQuater);

                                graphLabel.Add(string.Format(@"{0}/{1} ", dateSpan.Years, dateSpan.Months));

                                decimal? weight = 0;
                                decimal? height = 0;
                                switch (qm)
                                {
                                    case 5: weight = r.Weight5; height = r.Height5; break;
                                    case 8: weight = r.Weight8; height = r.Height8; break;
                                    case 11: weight = r.Weight11; height = r.Height11; break;
                                    case 2: weight = r.Weight2; height = r.Height2; break;
                                }

                                // bmi : weight / ((height * 0.01) * (height * 0.01))
                                decimal? bmi = weight / ((height * 0.01M) * (height * 0.01M));
                                graphData.Add(Convert.ToDecimal(string.Format(@"{0:0.00}", bmi == null ? 0 : bmi)));
                            }
                        }
                    }
                }
            }
            catch
            {
                success = false;
            }

            var result = new { success, graphLabel, graphData };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static string SaveItem(WebMethodStudentHealth data)
        {
            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                //int stdID = (int)HttpContext.Current.Session[SessionPrimaryKey];
                int stdID = int.Parse(data.sID);

                try
                {
                    int hltID = int.Parse(data.hID);

                    string HealthBlood = string.IsNullOrEmpty(data.blood) ? null : data.blood;
                    string HealthSickFood = string.IsNullOrEmpty(data.sickFood) ? null : data.sickFood;
                    string HealthSickDrug = string.IsNullOrEmpty(data.sickDrug) ? null : data.sickDrug;
                    string HealthSickOther = string.IsNullOrEmpty(data.sickOther) ? null : data.sickOther;
                    string HealthSickCongenital = string.IsNullOrEmpty(data.sickCongenital) ? null : data.sickCongenital;
                    string HealthSickDanger = string.IsNullOrEmpty(data.sickDanger) ? null : data.sickDanger;
                    var HealthData = (from h in data.healthData
                                      group h by new
                                      {
                                          h.level,
                                          h.month
                                      } into hg
                                      select new
                                      {
                                          hg.Key.level,
                                          hg.Key.month,
                                          weight = hg.Max(m => (m.type == "weight" ? m.value : null)),
                                          height = hg.Max(m => (m.type == "height" ? m.value : null))
                                      });

                    //if (HttpContext.Current.Session[SessionHealthPrimaryKey] != null)
                    if (hltID != 0)
                    {
                        // Modify Section
                        //hltID = Convert.ToInt32(HttpContext.Current.Session[SessionHealthPrimaryKey]);

                        TStudentHealthInfo pi = en.TStudentHealthInfoes.First(f => f.SchoolID == schoolID && f.nHealthID == hltID);

                        pi.sBlood = HealthBlood;
                        pi.sSickFood = HealthSickFood;
                        pi.sSickDrug = HealthSickDrug;
                        pi.sSickOther = HealthSickOther;
                        pi.sSickNormal = HealthSickCongenital;
                        pi.sSickDanger = HealthSickDanger;
                        pi.UpdatedBy = userData.UserID;
                        pi.UpdatedDate = DateTime.Now;

                        // Loop insert/update Health Growth
                        foreach (var h in HealthData)
                        {
                            var ch = en.TStudentHealthGrowths.Where(w => w.SchoolID == schoolID && w.nHealthID == hltID && w.nTSubLevel == h.level && w.nMonth == h.month).Count();
                            if (ch != 0)
                            {
                                var studentHealthGrowth = en.TStudentHealthGrowths.Where(w => w.SchoolID == schoolID && w.nHealthID == hltID && w.nTSubLevel == h.level && w.nMonth == h.month).First();
                                studentHealthGrowth.Weight = h.weight;
                                studentHealthGrowth.Height = h.height;
                                studentHealthGrowth.UpdatedBy = userData.UserID;
                                studentHealthGrowth.UpdatedDate = DateTime.Now;
                            }
                            else
                            {
                                TStudentHealthGrowth ph = new TStudentHealthGrowth
                                {
                                    nHealthID = hltID,
                                    nTSubLevel = h.level,
                                    nMonth = h.month,
                                    Weight = h.weight,
                                    Height = h.height,
                                    SchoolID = schoolID,
                                    CreatedBy = userData.UserID,
                                    CreatedDate = DateTime.Now
                                };

                                en.TStudentHealthGrowths.Add(ph);
                            }
                        }

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลนักเรียน(ข้อมูลสุขภาพ) รหัสนักเรียน:" + pi.sID, HttpContext.Current.Request, 14, 2, 0, schoolID);
                    }
                    else
                    {
                        // Insert Section
                        hltID = (int)(en.TStudentHealthInfoes.Where(w => w.SchoolID == schoolID).Count() == 0 ? 1 : en.TStudentHealthInfoes.Where(w => w.SchoolID == schoolID).Max(m => m.nHealthID) + 1);

                        // Get Item
                        TStudentHealthInfo p = new TStudentHealthInfo
                        {
                            sID = stdID,
                            nHealthID = hltID,
                            sBlood = HealthBlood,
                            sSickFood = HealthSickFood,
                            sSickDrug = HealthSickDrug,
                            sSickOther = HealthSickOther,
                            sSickNormal = HealthSickCongenital,
                            sSickDanger = HealthSickDanger,

                            sDeleted = "false",
                            SchoolID = schoolID,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now
                        };

                        en.TStudentHealthInfoes.Add(p);

                        //hltID = p.nHealthID;

                        // Loop insert/update Health Growth
                        foreach (var h in HealthData)
                        {
                            var ch = en.TStudentHealthGrowths.Where(w => w.SchoolID == schoolID && w.nHealthID == hltID && w.nTSubLevel == h.level && w.nMonth == h.month).Count();
                            if (ch != 0)
                            {
                                var studentHealthGrowth = en.TStudentHealthGrowths.Where(w => w.SchoolID == schoolID && w.nHealthID == hltID && w.nTSubLevel == h.level && w.nMonth == h.month).First();
                                studentHealthGrowth.Weight = h.weight;
                                studentHealthGrowth.Height = h.height;
                                studentHealthGrowth.UpdatedBy = userData.UserID;
                                studentHealthGrowth.UpdatedDate = DateTime.Now;
                            }
                            else
                            {
                                TStudentHealthGrowth ph = new TStudentHealthGrowth
                                {
                                    nHealthID = hltID,
                                    nTSubLevel = h.level,
                                    nMonth = h.month,
                                    Weight = h.weight,
                                    Height = h.height,
                                    SchoolID = schoolID,
                                    CreatedBy = userData.UserID,
                                    CreatedDate = DateTime.Now
                                };

                                en.TStudentHealthGrowths.Add(ph);
                            }
                        }

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลนักเรียน(ข้อมูลสุขภาพ) รหัสนักเรียน:" + p.sID, HttpContext.Current.Request, 14, 2, 0, schoolID);
                    }

                    isComplete += "-" + hltID;
                }
                catch (Exception error)
                {
                    isComplete = "error";
                }

                return isComplete;
            }
        }

        [WebMethod]
        public static string ClearSessionID()
        {
            //HttpContext.Current.Session[SessionHealthPrimaryKey] = null;

            return "";
        }


        [WebMethod]
        public static string GetItemView(string stdID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    TStudentHealthInfo p = en.TStudentHealthInfoes.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 6; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        dt.Rows[0]["F0"] = p.nHealthID;
                        dt.Rows[0]["F1"] = p.sBlood;
                        dt.Rows[0]["F2"] = p.sSickFood;
                        dt.Rows[0]["F3"] = p.sSickDrug;
                        dt.Rows[0]["F4"] = p.sSickOther;
                        dt.Rows[0]["F5"] = p.sSickNormal;
                        dt.Rows[0]["F6"] = p.sSickDanger;

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                return infor;
            }
        }

        #endregion

    }

    public class WebMethodStudentHealth
    {
        //nTSubLevel
        public string sID { get; set; }
        public string hID { get; set; }
        public string blood { get; set; }
        public string sickFood { get; set; }
        public string sickDrug { get; set; }
        public string sickOther { get; set; }
        public string sickCongenital { get; set; }
        public string sickDanger { get; set; }
        public List<WebMethodStudentHealthGrowth> healthData { get; set; }
    }

    public class WebMethodStudentHealthGrowth
    {
        //nTSubLevel
        public string type { get; set; }
        public int level { get; set; }
        public int month { get; set; }
        public decimal? value { get; set; }
    }
}