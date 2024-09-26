using FingerprintPayment.Class;
using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.StudentInfo
{
    public partial class StdExportQRCode : System.Web.UI.Page
    {
        protected string schoolName = "โรงเรียน..";
        protected string studentName = "";
        protected string qrCodeBase64 = "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw=="; // smallest possible transparent GIF image
        protected void Page_Load(object sender, EventArgs e)
        {
            string liffID = "1653630518-y03N0qao"; // 1653527349-LRexneZx //1653630518-y03N0qao

            string userType = (string)Request.QueryString["userType"];
            string level = (string)Request.QueryString["level"];
            string className = (string)Request.QueryString["className"];
            string stdName = (string)Request.QueryString["stdName"];

            //string sEntities = (string)HttpContext.Current.Session["sEntities"];
            int schoolID = Convert.ToInt32(HttpContext.Current.Session["nCompany"]);

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                StudentLogic studentLogic = new StudentLogic(dbSchool);
                string term = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });

                var company = dbMaster.TCompanies.FirstOrDefault(f => f.nCompany == schoolID);

                schoolName = company.sCompany;

                // Print All
                string sqlCondition = "";

                var userMaster = new int[] { };
                if (string.IsNullOrEmpty(stdName))
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == userType && w.cDel == null).Select(s => (int)s.nSystemID).ToArray();
                }
                else
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == userType && w.cDel == null && (w.sName.Contains(stdName) || w.sLastname.Contains(stdName) || (w.sName + " " + w.sLastname) == stdName))
                        .Select(s => (int)s.nSystemID).ToArray();
                }

                if (userMaster.Length > 0)
                {
                    switch (userType)
                    {
                        case "0":
                            sqlCondition += string.Format(@" AND u.sID IN ({0})", string.Join(",", userMaster));
                            break;
                        case "1":
                            sqlCondition += string.Format(@" AND e.sEmp IN ({0})", string.Join(",", userMaster));
                            break;
                    }
                }

                string query = "";

                switch (userType)
                {
                    case "0":

                        if (!string.IsNullOrEmpty(term)) { sqlCondition += string.Format(@" AND sch.nTerm = '{0}'", term); }
                        if (!string.IsNullOrEmpty(level)) { sqlCondition += string.Format(@" AND sl.nTSubLevel = {0}", level); }
                        if (!string.IsNullOrEmpty(className)) { sqlCondition += string.Format(@" AND tsl.nTermSubLevel2 = {0}", className); }
                        if (!string.IsNullOrEmpty(stdName)) { stdName = stdName.Replace("'", "''"); sqlCondition += string.Format(@" AND (u.sName LIKE '%{0}%' OR u.sLastname LIKE '%{0}%' OR u.sName+' '+u.sLastname = '{0}' OR u.sStudentID LIKE '%{0}%')", stdName); }

                        query = string.Format(@"
SELECT u.sID, u.sName 'Name', u.sLastname 'Lastname', ISNULL(tt.titleDescription, u.sStudentTitle) 'Title', u.sStudentID 'Code'
FROM TUser u 
LEFT JOIN TTermSubLevel2 tsl ON u.nTermSubLevel2 = tsl.nTermSubLevel2
LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel
LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10))
LEFT JOIN TStudentClassroomHistory sch ON u.SchoolID = sch.SchoolID AND u.SchoolID = sch.SchoolID AND u.sID = sch.sID
WHERE u.cDel IS NULL AND u.SchoolID = {0} {1}
", schoolID, sqlCondition);

                        break;
                    case "1":

                        if (!string.IsNullOrEmpty(stdName)) { stdName = stdName.Replace("'", "''"); sqlCondition += string.Format(@" AND (e.sName LIKE '%{0}%' OR e.sLastname LIKE '%{0}%' OR e.sName+' '+e.sLastname = '{0}' OR i.Code LIKE N'%{0}%')", stdName); }

                        query = string.Format(@"
SELECT e.sEmp 'sID', e.sName 'Name', e.sLastname 'Lastname', ISNULL(tt.titleDescription, e.sTitle) 'Title', i.Code 'Code'
FROM TEmployees e 
LEFT JOIN TEmployeeInfo i ON e.sEmp = i.sEmp AND e.SchoolID = i.SchoolID
LEFT JOIN TTitleList tt ON e.sTitle = CAST(tt.nTitleid AS VARCHAR(10))
WHERE e.cDel IS NULL AND e.SchoolID = {0} {1}
", schoolID, sqlCondition);

                        break;
                }


                int no = 1;
                List<EntityStudentPrintQRCodeList> result = dbSchool.Database.SqlQuery<EntityStudentPrintQRCodeList>(query).ToList();
                foreach (var i in result)
                {
                    // init
                    studentName = "";
                    qrCodeBase64 = "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw=="; // smallest possible transparent GIF image

                    var user = dbMaster.TUsers.Where(w => w.nSystemID == i.sID && w.cType == userType && w.cDel == null && w.nCompany == company.nCompany).FirstOrDefault();
                    if (user != null)
                    {
                        string code = string.Format(@"{0}-{1}", company.nCompany, user.sID);
                        string url = string.Format(@"line://app/{0}/?ssid={1}", liffID, EncryptionHelper.Encrypt(code));
                        qrCodeBase64 = QRCodeFunction.Create(url, 200, 200);

                        studentName = i.Title + i.Name + " " + i.Lastname;
                    }

                    ltrPages.Text += DocumentFormat(no++, schoolName, i.Code, studentName, qrCodeBase64);
                }

            }

        }

        private string DocumentFormat(int no, string schoolName, string studentCode, string studentName, string qrCodeBase64)
        {
            string page = string.Format(@"
<div id=""divPage{0}"" data-filename=""{4}"" class=""page"" style=""margin: 0px; border: 0px #D3D3D3 solid; box-shadow: 0 0 0px rgba(0, 0, 0, 0); width: 396.850px !important; height: 559.370px !important;"">
    <div class=""subpage"">
        <div class=""school-logo"">
            <img src=""/images/School Bright logo only.png"" width=""92"" class=""picture-src"" title="""">
        </div>
        <div class=""text-center"" style=""width: 270px; float: left; text-align: left; margin-left: 10px; margin-top: 5px; font-weight: bold;"">
            <span class=""header-title"">{1}</span><br />
            <span class=""header-title"">ชื่อ {2}</span><br />
            <span class=""header-title""></span>
        </div>

        <div class=""no-border tab-2"" style=""padding: 30px 0 0 0;"">
            <table class=""table-fixed"">
                <tr>
                    <th style=""width: 100%;""></th>
                </tr>
                <tr style=""height: 5px;"">
                    <td class=""text-label text-left""></td>
                </tr>
                <tr>
                    <td class=""text-label text-left"" style=""padding-left: 10px; text-decoration: none; font-weight: bold;"">วิธีลงทะเบียนระบบไลน์เพื่อรับข่าวสาร</td>
                </tr>
                <tr>
                    <td class=""text-label text-left"" style=""padding-left: 10px;"">1.เข้าแอปฯไลน์ กดปุ่มเพิ่มเพื่อนด้วย QR Code</td>
                </tr>
                <tr>
                    <td class=""text-label text-left"" style=""padding-left: 10px;"">2.สแกน QR Code นี้</td>
                </tr>
                <tr>
                    <td class=""text-label text-left"" style=""padding-left: 10px;"">3.กดปุ่มตามขั้นตอนที่ปรากฏในหน้าจอ</td>
                </tr>
                <tr>
                    <td class=""text-label text-center"">
                        <img src=""{3}"" width=""200"" class=""picture-src"" title=""""></td>
                </tr>
                <tr>
                    <td class=""text-label text-center"" style=""white-space: normal;"">หากท่านปิดหน้าจอโดยเชื่อมต่อไม่สำเร็จ ท่านสามารถ</td>
                </tr>
                <tr>
                    <td class=""text-label text-center"" style=""white-space: normal;"">สแกน QR Code ใหม่และทำตามขั้นตอนได้อีกครั้งทันที</td>
                </tr>
                <tr style=""height: 10px;"">
                    <td class=""text-label text-left""></td>
                </tr>
                <tr>
                    <td class=""text-label text-center"" style=""white-space: normal;"">หากท่านไม่สามารถสแกน QR Code ได้กรุณาแอดไลน์ <b>@sbbot</b></td>
                </tr>
                <tr>
                    <td class=""text-label text-center"" style=""white-space: normal;"">จากนั้นทำตามคำสั่งในระบบไลน์</td>
                </tr>
            </table>
        </div>

    </div>
</div>", no, schoolName, studentName, qrCodeBase64, studentCode);

            return page;
        }

    }
}