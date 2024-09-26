using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class schoolsetting : BehaviorGateway
    {
        public TSystemSetting Model;
        public TCompany ModelCompany;

        protected void Page_Load(object sender, EventArgs e)
        {

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var userData = GetUserData();
                var systemSetting = _dbMaster.TSystemSetting.FirstOrDefault(f => f.SchoolID == userData.CompanyID);
                if (systemSetting == null)
                {
                    systemSetting = new TSystemSetting();
                    systemSetting.CreatedDate = DateTime.Now;
                    systemSetting.CreatedBy = userData.UserID;
                    systemSetting.bScanOut = true;
                    systemSetting.SchoolID = userData.CompanyID;

                    _dbMaster.TSystemSetting.Add(systemSetting);
                    _dbMaster.SaveChanges();
                }

                ModelCompany = _dbMaster.TCompanies.FirstOrDefault(f => f.nCompany ==  userData.CompanyID);
                Model = systemSetting;
            }
        }

        [ScriptMethod()]
        [WebMethod(EnableSession = true)]
        public static object SaveData(TSystemSetting setting,TCompany company)
        {
            API_Reuslt reuslt = new API_Reuslt();
            try
            {
                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var userData = GetUserData();
                    var systemSetting = _dbMaster.TSystemSetting.FirstOrDefault(f => f.SchoolID == userData.CompanyID);

                    systemSetting.bScanOut = setting.bScanOut;
                    systemSetting.UpdatedDate = DateTime.Now;
                    systemSetting.UpdatedBy = userData.UserID;

                    var company1 = _dbMaster.TCompanies.FirstOrDefault(f => f.nCompany == userData.CompanyID);
                    company1.ClassNameDisable = company.ClassNameDisable;

                    _dbMaster.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string message_Error = "";
                reuslt.resultDes = message_Error;
                reuslt.StatusCode = 500;
            }

            return reuslt;
        }
    }
}