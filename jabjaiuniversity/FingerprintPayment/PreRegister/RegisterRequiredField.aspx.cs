using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterRequiredField : PreRegisterGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static object SaveRequiredField(List<RequiredFieldData> requiredFieldDatas)
        {
            bool success = true;
            string message = "Success";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                

                try
                {
                    foreach (var rf in requiredFieldDatas)
                    {
                        var requiredFieldObj = en.TPreRegisterRequiredField.Where(w => w.SchoolID == schoolID && w.VFIID == rf.VFIID).FirstOrDefault();
                        if (requiredFieldObj == null)
                        {
                            requiredFieldObj = new TPreRegisterRequiredField
                            {
                                VFIID = rf.VFIID,
                                CategoryID = rf.CategoryID,
                                SchoolID = schoolID,
                                Status = rf.Status,
                                UpdateDate = DateTime.Now,
                                UpdateBy = userData.UserID
                            };

                            en.TPreRegisterRequiredField.Add(requiredFieldObj);
                            en.SaveChanges();
                        }
                        else
                        {
                            requiredFieldObj.Status = rf.Status;
                            requiredFieldObj.UpdateDate = DateTime.Now;
                            requiredFieldObj.UpdateBy = userData.UserID;

                            en.SaveChanges();
                        }
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    message = err.Message;
                }
            }

            return JsonConvert.SerializeObject(new { success, message });
        }

        public class RequiredFieldData
        {
            [JsonProperty(PropertyName = "vfiId")]
            public int VFIID { get; set; }

            [JsonProperty(PropertyName = "categoryId")]
            public int CategoryID { get; set; }

            [JsonProperty(PropertyName = "status")]
            public bool Status { get; set; }
        }

    }
}