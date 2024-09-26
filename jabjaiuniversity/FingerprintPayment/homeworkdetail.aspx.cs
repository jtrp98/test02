using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;
using System.Globalization;

namespace FingerprintPayment
{
    public partial class homeworkdetail : HomeworkGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string req = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(req))
                {
                    int id = int.Parse(req);
                    int schoolID = UserData.CompanyID;
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        THomework tHomeWork = dbschool.THomeworks.Where(w => w.SchoolID == schoolID && w.nHomeWork == id).FirstOrDefault();
                        ltrdayEnd.Text = tHomeWork.dEnd.Value.ToString("dd/MM/yyyy");
                        ltrdayStart.Text = tHomeWork.dStart.Value.ToString("dd/MM/yyyy");
                        ltrdayNotification.Text = tHomeWork.dNotification.Value.ToString("dd/MM/yyyy");
                        ltrDetail.Text = tHomeWork.sHomeworkDetail;
                        ltrPlane.Text = dbschool.TPlanes.FirstOrDefault(f => f.sPlaneID == tHomeWork.sPlaneID).sPlaneName;
                        if (tHomeWork.sEmp.HasValue)
                        {
                            var qteacher = dbschool.TEmployees.FirstOrDefault(f => f.sEmp == tHomeWork.sEmp);
                            ltrteacher.Text = qteacher.sName + " " + qteacher.sLastname;
                        }

                        var qfile = dbschool.THomeWorkFiles.Where(w => w.SchoolID == schoolID && w.nHomeWorkId == tHomeWork.nHomeWork).ToList();
                        foreach (var file in qfile)
                        {
                            switch (file.ContentType.ToString().ToLower())
                            {
                                case "image/png": //PNG
                                case "image/jpeg": //JPEG
                                case "image/jpg": //JPG
                                    ltrfile.Text += string.Format("<a target=\"_blank\" href=\"" + file.sFileName + "\" ><img src=\"" + file.sFileName + "\" style=\"height: 200px; width: 100px\"/></a>");
                                    break;
                                case "application/pdf": //PDF
                                    ltrfile.Text += string.Format("<a target=\"_blank\" href=\"" + file.sFileName + "\" ><i class=\"fa fa-file-pdf-o\"></i></a>");
                                    break;
                                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": //EXECL 2007
                                case "application/vnd.ms-excel": //EXECL 2003
                                    ltrfile.Text += string.Format("<a target=\"_blank\" href=\"" + file.sFileName + "\" ><i class=\"fa fa-file-excel-o\"></i></a>");
                                    break;
                                case "application/msword": // WORD 2003
                                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document": // WORD 2007
                                    ltrfile.Text += string.Format("<a target=\"_blank\" href=\"" + file.sFileName + "\" ><i class=\"fa fa-file-word-o\"></i></a>");
                                    break;
                                default:
                                    break;
                            }
                        }

                        string Htmltable = @"<table id='tablelistuser' class='table table-striped col-lg-12'>
                            <thead class='table-tab'>
                                <tr>
                                    <td style='width: 10 %; '>ลำดับที่</td>  
                                    <td style='width: 50 %; '>ชื่อ - นามสกุล</td>  
                                    <td style='width: 20 %; '>ระดับ</td>
                                    <td style='width: 20 %; '>ห้อง</td>
                                </tr>
                            </thead>
                            <tbody style='height: 400px; overflow - y: scroll; '>{0}
                            </tbody>
                        </table>";

                        var tHomework_User = dbschool.THomework_User.Where(w => w.SchoolID == schoolID && w.nHomeWork == tHomeWork.nHomeWork);
                        var tUser = dbschool.TUser.Where(w=>w.SchoolID == schoolID);
                        var tTermSubLevel2 = dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolID);
                        var tSubLevels = dbschool.TSubLevels.Where(w => w.SchoolID == schoolID);

                        string StrName = "";

                        var listUser = (from a in tHomework_User
                                        join b in tUser on a.sID equals b.sID
                                        join c in tTermSubLevel2 on b.nTermSubLevel2 equals c.nTermSubLevel2
                                        join d in tSubLevels on c.nTSubLevel equals d.nTSubLevel
                                        select new { b.sName, b.sLastname, c.nTSubLevel2, SubLevel = d.SubLevel.Trim() }).ToList();

                        int index = 1;
                        foreach (var data in listUser)
                        {
                            StrName += @"<tr><td style='width: 10 %; '>" + index++ + @"</td> 
                                        <td style='width: 50 %; '>" + data.sName + " " + data.sLastname + @"</td>  
                                        <td style='width: 20 %; '>" + data.SubLevel + @"</td>
                                        <td style='width: 20 %; '>" + data.nTSubLevel2 + "</td></tr>";
                        }

                        ltrTable.Text = string.Format(Htmltable, StrName);
                    }
                }
            }
        }
    }
}