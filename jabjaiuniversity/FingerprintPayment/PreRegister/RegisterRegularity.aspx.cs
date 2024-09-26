using FingerprintPayment.Class;
using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterRegularity : PreRegisterGateway
    {
        protected string ExplanationData = "";
        protected string RegularityData = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ExplanationData = GetExplanationData();

            RegularityData = GetRegularityData();
        }

        private string GetExplanationData()
        {
            string data = "";
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                TRegisterExplanation registerExplanation = en.TRegisterExplanations.Where(w => w.SchoolID == schoolID).FirstOrDefault();
                if (registerExplanation != null)
                {
                    data = registerExplanation.Description;
                    //data = HttpUtility.HtmlDecode(data);
                    data = data
                        .Replace("`", "&grave;");
                    //    .Replace("'Comic Sans MS'", "&quot;Comic Sans MS&quot;")
                    //    .Replace("'Courier New'", "&quot;Courier New&quot;")
                    //    .Replace("'Times New Roman'", "&quot;Times New Roman&quot;");
                }
            }

            return data;
        }

        private string GetRegularityData()
        {
            string data = "";
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                var listRegularity = from s in en.TSubLevels.Where(w => w.SchoolID == schoolID)
                                     join r in en.TRegisterRegularities.Where(w => w.SchoolID == schoolID && w.cDel != true) on s.nTSubLevel equals r.nTSubLevel into r_join
                                     from r in r_join.DefaultIfEmpty()
                                     where
                                       s.nWorkingStatus == 1
                                     select new
                                     {
                                         s.nTSubLevel,
                                         s.SubLevel,
                                         r.Filename
                                     };

                int rowNumber = 1;
                foreach (var r in listRegularity)
                {
                    data += string.Format(@"
<tr>
    <td>{0}.</td>
    <td>{1}</td>
    <td style=""border-right-width: 0px; text-align: right;"">
        <div id=""divProgress{2}"" style=""display: inline-block; vertical-align: middle;"">
            {3}
        </div>
        <div class=""div-file"">
            <a href=""#"" class=""fa fa-cloud-upload"" data-lid=""{2}"" style=""text-decoration: none; vertical-align: middle; margin-right: 5px; margin-top: 5px; font-size: 22px;""></a>
            <input type=""file"" data-lid=""{2}"" accept=""application/pdf""/>
        </div>
        <a href=""#"" class=""fa fa-remove text-danger regularity-remove"" data-lid=""{2}"" style=""text-decoration: none; vertical-align: middle; font-size: 22px;""></a>
    </td>
    <td style=""border-left-width: 0px; width: 7%;""></td>
</tr>", rowNumber, r.SubLevel, r.nTSubLevel, string.IsNullOrEmpty(r.Filename) ? "" : @"<i class=""fa fa-check text-success"" aria-hidden=""true"" style=""font-size: 22px; vertical-align: middle; margin-right: 5px; margin-top: 3px;""></i>");
                    rowNumber++;
                }
            }

            return data;
        }

        [WebMethod(EnableSession = true)]
        public static string SaveExplanationData(string messageData)
        {
            bool success = true;
            string message = "Save Successfully";

            try
            {
                messageData = Regex.Replace(messageData, @"<script[^>]*>[\s\S]*?</script>", string.Empty);
                messageData = Regex.Replace(messageData, @"\﻿?", string.Empty); // "<[^>/][^>]*>\﻿?</[^>]*>", "<([^<>]+)([\s]+[^<>]+)*>\s*<\/\1>", "<\s*[^>/]*>((&nbsp;)*|\s*)</\s*[^></]*>" // \ufeff

                var regex = new Regex(@"<\s*[^>/]*>((&nbsp;)*|\s*)</\s*[^></]*>");
                Match match = regex.Match(messageData);
                while (match.Success)
                {
                    messageData = Regex.Replace(messageData, @"<\s*[^>/]*>((&nbsp;)*|\s*)</\s*[^></]*>", string.Empty);

                    match = regex.Match(messageData);
                }

                if (messageData == "<p><br></p>") messageData = "";

                var userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {
                    TRegisterExplanation registerExplanation;
                    int existFirstRow = en.TRegisterExplanations.Where(w => w.SchoolID == schoolID).Count();

                    if (existFirstRow == 0)
                    {
                        registerExplanation = new TRegisterExplanation
                        {
                            //ID = 1,
                            Description = messageData,
                            SchoolID = schoolID,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now
                        };

                        en.TRegisterExplanations.Add(registerExplanation);
                    }
                    else
                    {
                        registerExplanation = en.TRegisterExplanations.Where(w => w.SchoolID == schoolID).First();

                        registerExplanation.Description = messageData;
                        registerExplanation.UpdateDate = DateTime.Now;
                        registerExplanation.UpdateBy = userData.UserID;
                    }

                    en.SaveChanges();

                    if (existFirstRow == 0)
                    {
                        database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลตั้งค่าคำชี้แจง/ระเบียบการ รหัส:" + registerExplanation.RegisterExplanationID, HttpContext.Current.Request, 163, 2, 0, schoolID);
                    }
                    else
                    {
                        database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลตั้งค่าคำชี้แจง/ระเบียบการ รหัส:" + registerExplanation.RegisterExplanationID, HttpContext.Current.Request, 163, 3, 0, schoolID);
                    }
                }
            }
            catch (Exception error)
            {
                success = false;
                message = error.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static string RemoveRegularityData(int lid)
        {
            bool success = true;
            string message = "Save Successfully";

            try
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {
                    var itemToRemove = en.TRegisterRegularities.SingleOrDefault(x => x.SchoolID == schoolID && x.nTSubLevel == lid);

                    if (itemToRemove != null)
                    {
                        itemToRemove.cDel = true;
                        itemToRemove.UpdateBy = userData.UserID;
                        itemToRemove.UpdateDate = DateTime.Now;

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลตั้งค่าคำชี้แจง/ระเบียบการ รหัส:" + lid, HttpContext.Current.Request, 163, 4, 0, schoolID);
                    }
                }
            }
            catch (Exception error)
            {
                success = false;
                message = error.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

    }
}