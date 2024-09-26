using FingerprintPayment.StudentCall;
using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Data;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Shop
{
    public partial class Config : BaseStudentCall
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static object GetData()
        {
            var userData = GetUserData();
            var schoolId = userData.CompanyID;
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var d = dbmaster.TSystemSetting
                  .Where(o => o.SchoolID == schoolId)
                  .FirstOrDefault();

                return new
                {
                    maxTopup = d?.MaxTopup ?? 1000,
                };
            }
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static object SaveData(FormSaveData data)
        {
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var userData = GetUserData();
                var schoolId = userData.CompanyID;

                var d = dbmaster.TSystemSetting
                  .Where(o => o.SchoolID == schoolId)
                  .FirstOrDefault();

                if (d == null)
                {
                    d = new TSystemSetting();
                    d.SchoolID = schoolId;
                    d.MaxTopup = data.maxTopup;
                    d.CreatedDate = DateTime.Now;
                    dbmaster.TSystemSetting.Add(d);
                }
                else
                {
                    d.SchoolID = schoolId;
                    d.UpdatedDate = DateTime.Now;
                    d.MaxTopup = data.maxTopup;
                }

                dbmaster.SaveChanges();

                return new { status = "ok" };
            }
        }

        public class FormSaveData
        {
            public float? maxTopup { get; set; }
        }


    }
}