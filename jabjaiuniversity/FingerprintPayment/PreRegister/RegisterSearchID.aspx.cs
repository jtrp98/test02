using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterSearchID : RegisterGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static string SearchData(string id)
        {
            bool success = true;
            string message = "Search Successfully";
            string redirectUrl = "RegisterPrint.aspx";
            List<RegisterData> registerDatas = new List<RegisterData>();

            if (HttpContext.Current.Session["RegisterOnlineEntities"] != null)
            {
                try
                {
                    int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);
                    using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        //int registerYear = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineYear"]);

                        switch (schoolID)
                        {
                            case 258: redirectUrl = "RegisterPrint00258.aspx"; break;
                            default: break;
                        }

                        var preRegisterList = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.sIdentification == id && w.cDel != 1).ToList();
                        switch (preRegisterList.Count)
                        {
                            case 0:

                                success = false;
                                message = "ไม่พบข้อมูลรหัสบัตรประชาชนในระบบ";

                                break;
                            case 1:

                                var preRegisterObj = preRegisterList.FirstOrDefault();

                                registerDatas.Add(new RegisterData { RegisterID = preRegisterObj.preRegisterId, RegisterYear = preRegisterObj.registerYear + 543 });

                                HttpContext.Current.Session["RegisterPrintID"] = preRegisterObj.preRegisterId;

                                break;
                            default:

                                foreach (var r in preRegisterList)
                                {
                                    registerDatas.Add(new RegisterData { RegisterID = r.preRegisterId, RegisterYear = r.registerYear + 543 });
                                }

                                break;
                        }

                    }
                }
                catch (Exception error)
                {
                    success = false;
                    message = error.Message;
                }
            }

            var result = new { success, message, registerDatas, redirectUrl };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static string ChooseStudyApplication(int registerID)
        {
            bool success = true;
            string message = "Choose Successfully";
            string redirectUrl = "RegisterPrint.aspx";

            if (HttpContext.Current.Session["RegisterOnlineEntities"] != null)
            {
                try
                {
                    int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);
                    using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        switch (schoolID)
                        {
                            case 258: redirectUrl = "RegisterPrint00258.aspx"; break;
                            default: break;
                        }

                        var preRegisterObj = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == registerID && w.cDel != 1).FirstOrDefault();
                        if (preRegisterObj != null)
                        {
                            HttpContext.Current.Session["RegisterPrintID"] = registerID;
                        }
                        else
                        {
                            success = false;
                            message = "ไม่พบข้อมูลใบสมัครในระบบ";
                        }
                    }
                }
                catch (Exception error)
                {
                    success = false;
                    message = error.Message;
                }
            }

            var result = new { success, message, redirectUrl };

            return JsonConvert.SerializeObject(result);
        }

        public class RegisterData
        {
            public int RegisterID { get; set; }
            public int RegisterYear { get; set; }
        }
    }
}