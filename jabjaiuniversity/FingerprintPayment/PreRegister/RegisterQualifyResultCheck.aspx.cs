using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterQualifyResultCheck : System.Web.UI.Page
    {
        protected string schoolLogo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegisterOnlineSchoolLogo"] != null)
            {
                schoolLogo = (string)Session["RegisterOnlineSchoolLogo"];
            }
        }

        [WebMethod(EnableSession = true)]
        public static string ResultCheck(string id)
        {
            bool success = true;
            string code = "200";
            string message = "Search Successfully";
            object data = null;

            if (HttpContext.Current.Session["RegisterOnlineEntities"] != null)
            {
                try
                {
                    int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);
                    using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        int registerYear = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineYear"]);

                        //var preRegister = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.registerYear == registerYear && w.sIdentification == id && w.cDel == null).FirstOrDefault();
                        var preRegister = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.sIdentification == id && w.cDel == null).OrderByDescending(o => o.registerYear).FirstOrDefault();
                        if (preRegister == null)
                        {
                            success = false;
                            message = "ไม่พบข้อมูลรหัสบัตรประชาชนในระบบ";
                        }

                        if (success)
                        {
                            string title = "";
                            if (preRegister.StudentTitle != null)
                            {
                                var objTitle = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == preRegister.StudentTitle).FirstOrDefault();
                                if (objTitle != null)
                                {
                                    title = objTitle.titleDescription;
                                }
                            }
                            string name = title + preRegister.sName + " " + preRegister.sLastname;
                            string year = Convert.ToString(preRegister.registerYear + 543);
                            string plan = "-";
                            string documentsInfo = preRegister.CompleteDocumentsInfo;

                            TRegisterPlanSetup registerPlanSetup;

                            switch (preRegister.CompleteDocuments)
                            {
                                case "1":
                                    code = "201";
                                    message = "ขอแสดงความยินดี<br/>สถานะการตรวจสอบเอกสารครบ";

                                    registerPlanSetup = en.TRegisterPlanSetups.Where(w => w.SchoolID == schoolID && w.nTSubLevel == preRegister.optionLevel && w.RegisterPlanSetupID == preRegister.RegisterPlanSetupID).FirstOrDefault();
                                    if (registerPlanSetup != null)
                                    {
                                        plan = registerPlanSetup.PlanName;
                                    }
                                    
                                    break;
                                case "0":
                                    code = "202";
                                    message = "ขอแสดงความเสียใจ<br/>สถานะการตรวจสอบเอกสารไม่ครบ";

                                    registerPlanSetup = en.TRegisterPlanSetups.Where(w => w.SchoolID == schoolID && w.nTSubLevel == preRegister.optionLevel && w.RegisterPlanSetupID == preRegister.RegisterPlanSetupID).FirstOrDefault();
                                    if (registerPlanSetup != null)
                                    {
                                        plan = registerPlanSetup.PlanName;
                                    }

                                    documentsInfo = preRegister.CompleteDocumentsInfo;

                                    break;
                                default:
                                    code = "203";
                                    //message = "ทางโรงเรียนยังไม่มีการบันทึกสถานะการตรวจสอบเอกสาร";
                                    message = "เอกสารอยู่ในขั้นตอนการตรวจสอบ";
                                    break;
                            }

                            data = new { name, year, plan, documentsInfo };
                        }
                    }
                }
                catch (Exception error)
                {
                    success = false;
                    message = error.Message;
                }
            }
            else
            {
                success = false;
                message = "Session is expire.";
            }

            var result = new { success, code, message, data };

            return JsonConvert.SerializeObject(result);
        }
    }
}