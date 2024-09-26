using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
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
    public partial class StdEducation : StudentGateway
    {
        private static string SessionPrimaryKey = "STUDENTID";

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

                    // Init data dropdown
                    using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                    {

                        // Province
                        var provinces = dbMaster.provinces.ToList();
                        foreach (var r in provinces)
                        {
                            this.ltrOldSchoolProvince.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.PROVINCE_ID, r.PROVINCE_NAME);
                        }
                    }
                    break;
                case "view":
                    MvContent.ActiveViewIndex = 2; break;
            }
        }

        [WebMethod]
        public static string GetItem(string stdID)
        {
            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    JabjaiEntity.DB.TUser p = en.TUser.Where(w => w.SchoolID == schoolID && w.sID == iStdID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 9; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        dt.Rows[0]["F1"] = p.oldSchoolName;
                        dt.Rows[0]["F2"] = p.oldSchoolProvince;
                        dt.Rows[0]["F3"] = p.oldSchoolAumpher;
                        dt.Rows[0]["F4"] = p.oldSchoolTumbon;
                        //dt.Rows[0]["F5"] = p.oldSchoolGPA?.ToString("0.00");
                        dt.Rows[0]["F5"] = p.oldSchoolGPA2;
                        dt.Rows[0]["F6"] = GetNameOldSchoolGraduated(p.oldSchoolGraduated);
                        dt.Rows[0]["F7"] = p.moveOutReason;
                        dt.Rows[0]["F8"] = p.Credit?.ToString("0.00");
                        dt.Rows[0]["F9"] = p.OldSchoolDateGraduated?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

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

        [WebMethod]
        public static string SaveItem(List<string> data)
        {
            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                try
                {
                    int stdID = int.Parse(data[0]);

                    //if (HttpContext.Current.Session[SessionPrimaryKey] != null)
                    if (stdID != 0)
                    {
                        //int stdID = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);

                        string OldSchoolName = data[1];
                        string OldSchoolProvince = data[2];
                        string OldSchoolAmphoe = data[3];
                        string OldSchoolTombon = data[4];
                        //decimal? OldSchoolGPA = string.IsNullOrEmpty(data[5]) ? (decimal?)null : decimal.Parse(data[5]);
                        string OldSchoolGPA2 = data[5];
                        string OldSchoolGraduated = data[6];
                        string MoveOutReason = data[7];
                        decimal? Credit = string.IsNullOrEmpty(data[8]) ? (decimal?)null : decimal.Parse(data[8]);
                        DateTime? OldSchoolDateGraduated = string.IsNullOrEmpty(data[9]) ? (DateTime?)null : DateTime.Parse(data[9], new CultureInfo("th-TH"));

                        JabjaiEntity.DB.TUser pi = en.TUser.First(f => f.sID == stdID);

                        pi.oldSchoolName = OldSchoolName;
                        pi.oldSchoolProvince = string.IsNullOrEmpty(OldSchoolProvince) ? null : OldSchoolProvince;
                        pi.oldSchoolAumpher = string.IsNullOrEmpty(OldSchoolAmphoe) ? null : OldSchoolAmphoe;
                        pi.oldSchoolTumbon = string.IsNullOrEmpty(OldSchoolTombon) ? null : OldSchoolTombon;
                        pi.oldSchoolGPA2 = OldSchoolGPA2;
                        pi.oldSchoolGraduated = OldSchoolGraduated;
                        pi.moveOutReason = MoveOutReason;
                        pi.Credit = Credit;
                        pi.OldSchoolDateGraduated = OldSchoolDateGraduated;
                        pi.dUpdate = DateTime.Now;
                        pi.UpdatedBy = userData.UserID;
                        pi.UpdatedDate = DateTime.Now;

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลนักเรียน(ข้อมูลการศึกษา) รหัสนักเรียน:" + pi.sStudentID, HttpContext.Current.Request, 14, 2, 0, schoolID);
                    }
                    else
                    {
                        isComplete = "error";
                    }
                }
                catch
                {
                    isComplete = "error";
                }

                return isComplete;
            }
        }

        [WebMethod]
        public static string ClearSessionID()
        {
            //HttpContext.Current.Session[SessionPrimaryKey] = null;

            return "";
        }


        [WebMethod]
        public static string GetItemView(string stdID)
        {
            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    JabjaiEntity.DB.TUser p = en.TUser.Where(w => w.SchoolID == schoolID && w.sID == iStdID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 9; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        dt.Rows[0]["F1"] = p.oldSchoolName;

                        var provinceID = string.IsNullOrEmpty(p.oldSchoolProvince) ? 0 : int.Parse(p.oldSchoolProvince);
                        var province = "";
                        var provinceObj = dbMaster.provinces.Where(w => w.PROVINCE_ID == provinceID).FirstOrDefault();
                        if (provinceObj != null)
                        {
                            province = provinceObj.PROVINCE_NAME;
                        }
                        dt.Rows[0]["F2"] = province;

                        var aumpherID = string.IsNullOrEmpty(p.oldSchoolAumpher) ? 0 : int.Parse(p.oldSchoolAumpher);
                        var aumpher = "";
                        var aumpherObj = dbMaster.amphurs.Where(w => w.PROVINCE_ID == provinceID && w.AMPHUR_ID == aumpherID).FirstOrDefault();
                        if (aumpherObj != null)
                        {
                            aumpher = aumpherObj.AMPHUR_NAME;
                        }
                        dt.Rows[0]["F3"] = aumpher;

                        var tumbonID = string.IsNullOrEmpty(p.oldSchoolTumbon) ? 0 : int.Parse(p.oldSchoolTumbon);
                        var tumbon = "";
                        var tumbonObj = dbMaster.districts.Where(w => w.AMPHUR_ID == aumpherID && w.DISTRICT_ID == tumbonID).FirstOrDefault();
                        if (tumbonObj != null)
                        {
                            tumbon = tumbonObj.DISTRICT_NAME;
                        }
                        dt.Rows[0]["F4"] = tumbon;
                        dt.Rows[0]["F5"] = p.oldSchoolGPA2;
                        dt.Rows[0]["F6"] = GetNameOldSchoolGraduated(p.oldSchoolGraduated);
                        dt.Rows[0]["F7"] = p.moveOutReason;
                        dt.Rows[0]["F8"] = p.Credit?.ToString("0.00");
                        dt.Rows[0]["F9"] = p.OldSchoolDateGraduated?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

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

        // Function
        public static string GetNameOldSchoolGraduated(string oldSchoolGraduated, string lang="thai")
        {
            string name = oldSchoolGraduated ?? "";
            if (lang == "thai")
            {
                switch (oldSchoolGraduated)
                {
                    case "11": name = "เตรียมอนุบาลศึกษา"; break;
                    case "12": name = "อนุบาลศึกษา 1"; break;
                    case "13": name = "อนุบาลศึกษา 2"; break;
                    case "14": name = "อนุบาลศึกษา 3"; break;
                    case "1": name = "ประถมศึกษาปีที่ 1"; break;
                    case "2": name = "ประถมศึกษาปีที่ 2"; break;
                    case "3": name = "ประถมศึกษาปีที่ 3"; break;
                    case "4": name = "ประถมศึกษาปีที่ 4"; break;
                    case "5": name = "ประถมศึกษาปีที่ 5"; break;
                    case "6": name = "ประถมศึกษาปีที่ 6"; break;
                    case "18": name = "มัธยมศึกษาปีที่ 1"; break;
                    case "19": name = "มัธยมศึกษาปีที่ 2"; break;
                    case "20": name = "มัธยมศึกษาปีที่ 3"; break;
                    case "21": name = "มัธยมศึกษาปีที่ 4"; break;
                    case "22": name = "มัธยมศึกษาปีที่ 5"; break;
                    case "23": name = "มัธยมศึกษาปีที่ 6"; break;
                    //case "7": name = "มัธยมศึกษาตอนต้น"; break;
                    //case "8": name = "มัธยมศึกษาตอนปลาย"; break;
                    case "9": name = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 1"; break;
                    case "15": name = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 2"; break;
                    case "16": name = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 3"; break;
                    case "10": name = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 1"; break;
                    case "17": name = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 2"; break;
                    default: break;
                }
            }
            else
            {
                switch (oldSchoolGraduated)
                {
                    case "11": name = "Preparing for kindergarten"; break;
                    case "12": name = "kindergarten education 1"; break;
                    case "13": name = "kindergarten education 2"; break;
                    case "14": name = "kindergarten education 3"; break;
                    case "1": name = "Primary school 1"; break;
                    case "2": name = "Primary school 2"; break;
                    case "3": name = "Primary school 3"; break;
                    case "4": name = "Primary school 4"; break;
                    case "5": name = "Primary school 5"; break;
                    case "6": name = "Primary school 6"; break;
                    case "18": name = "Secondary 1"; break;
                    case "19": name = "Secondary 2"; break;
                    case "20": name = "Secondary 3"; break;
                    case "21": name = "Secondary 4"; break;
                    case "22": name = "Secondary 5"; break;
                    case "23": name = "Secondary 6"; break;
                    //case "7": name = "Junior high school"; break;
                    //case "8": name = "High School"; break;
                    case "9": name = "Professional certificate, first year 1"; break;
                    case "15": name = "Professional certificate, first year 2"; break;
                    case "16": name = "Professional certificate, first year 3"; break;
                    case "10": name = "Advanced Vocational Certificate 1"; break;
                    case "17": name = "Advanced Vocational Certificate"; break;
                    default: break;
                }
            }

            return name;
        }

    }
}