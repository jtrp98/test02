using FingerprintPayment.Employees.CsCode;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Employees
{
    public partial class EmployeeDetail : EmployeeGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static List<EntityRegisterDistrict> LoadDistrict(int provinceID)
        {
            List<EntityRegisterDistrict> result = null;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                result = dbMaster.amphurs.Where(w => w.PROVINCE_ID == provinceID).Select(s => new EntityRegisterDistrict { AMPHUR_ID = s.AMPHUR_ID, AMPHUR_NAME = s.AMPHUR_NAME }).ToList();

                return result;
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityRegisterSubDistrict> LoadSubDistrict(int districtID)
        {
            List<EntityRegisterSubDistrict> result = null;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                result = dbMaster.districts.Where(w => w.AMPHUR_ID == districtID).Select(s => new EntityRegisterSubDistrict { DISTRICT_ID = s.DISTRICT_ID, DISTRICT_NAME = s.DISTRICT_NAME }).ToList();

                return result;
            }
        }

    }
}