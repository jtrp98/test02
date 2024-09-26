using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterOnline06 : RegisterGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
          
           


            int schoolID = Convert.ToInt32(Session["RegisterOnlineSchoolID"]);

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                var result = dbMaster.provinces.ToList();
                if (result.Count > 0)
                {
                    foreach (var r in result)
                    {
                        this.ltrProvince.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.PROVINCE_ID, r.PROVINCE_NAME);
                    }
                }


                string sqlQuery = string.Format(@"SELECT nTitleid, titleDescription FROM TTitleList WHERE SchoolID = {0} AND deleted <> '1' AND workStatus <> 'notworking'", schoolID);
                List<EntityRegisterStudentTitle> titles = en.Database.SqlQuery<EntityRegisterStudentTitle>(sqlQuery).ToList();

                if (titles.Count > 0)
                {
                    foreach (var r in titles)
                    {
                        this.ltrStudentStayWithTitle.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nTitleid, r.titleDescription);
                    }
                }

                sqlQuery = string.Format(@"
SELECT CAST(nTSubLevel AS VARCHAR(10)) 'nTSubLevel', SubLevel 
FROM TSubLevel WHERE SchoolID={0} 
ORDER BY (CASE WHEN SubLevel LIKE N'%เตรียมอนุบาล%' THEN 0 WHEN SubLevel LIKE N'อ.%' OR SubLevel LIKE N'ป.%' OR SubLevel LIKE N'ม.%' THEN 1 ELSE 2 END), MasterCode
", schoolID);
                List<EntityStudentClass> classs = en.Database.SqlQuery<EntityStudentClass>(sqlQuery).ToList();
                if (classs.Count > 0)
                {
                    foreach (var c in classs)
                    {
                        this.ltrStudentNeighborSubLevel.Text += string.Format(@"<option value=""{0}"">{1}</option>", c.nTSubLevel, c.SubLevel);
                    }
                }


                //Script Required Field
                RegisterOnline registerOnline = (RegisterOnline)this.Master;
                List<string> listElement = registerOnline.GetElementRequiredField(schoolID, 3);

                string script = "";
                foreach (var e in listElement)
                {
                    script += string.Format(@"AddRequiredRulesvalidation('#{0}');", e);
                }

                ltrScriptRequiredField.Text = string.Format(@"<script>
        $(document).ready(function () {{
            {0}
        }});
    </script>", script);
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityRegisterDistrict> LoadDistrict(int provinceID)
        {
            List<EntityRegisterDistrict> result = null;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                if (HttpContext.Current.Session["RegisterOnlineEntities"] != null)
                {
                   
                    result = dbMaster.amphurs.Where(w => w.PROVINCE_ID == provinceID).Select(s => new EntityRegisterDistrict { AMPHUR_ID = s.AMPHUR_ID, AMPHUR_NAME = s.AMPHUR_NAME }).ToList();
                }

                return result;
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityRegisterSubDistrict> LoadSubDistrict(int districtID)
        {
            List<EntityRegisterSubDistrict> result = null;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                if (HttpContext.Current.Session["RegisterOnlineEntities"] != null)
                {
                    
                    result = dbMaster.districts.Where(w => w.AMPHUR_ID == districtID).Select(s => new EntityRegisterSubDistrict { DISTRICT_ID = s.DISTRICT_ID, DISTRICT_NAME = s.DISTRICT_NAME }).ToList();
                }

                return result;
            }
        }
    }
}