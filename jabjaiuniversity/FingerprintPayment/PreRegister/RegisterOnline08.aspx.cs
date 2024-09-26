using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterOnline08 : RegisterGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            // Load title
            int schoolID = Convert.ToInt32(Session["RegisterOnlineSchoolID"]);
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string sqlQuery = string.Format(@"SELECT nTitleid, titleDescription FROM TTitleList WHERE SchoolID = {0} AND deleted <> '1' AND workStatus <> 'notworking'", schoolID);
                List<EntityRegisterStudentTitle> result = en.Database.SqlQuery<EntityRegisterStudentTitle>(sqlQuery).ToList();

                if (result.Count > 0)
                {
                    foreach (var r in result)
                    {
                        this.ltrMotherTitle.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nTitleid, r.titleDescription);
                    }
                }

                // Nation
                var listNation = en.TMasterDatas.Where(w => w.MasterType == "3").OrderBy(o => o.MasterDes).ToList();
                var tempNation = listNation.Where(w => w.MasterCode == "099").FirstOrDefault();
                int tempNationIndex = listNation.FindIndex(f => f.MasterCode == "099");
                listNation.RemoveAt(tempNationIndex);
                listNation.Insert(0, tempNation);
                foreach (var l in listNation)
                {
                    this.ltrMotherNation.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                }

                // Religion
                var listReligion = en.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                foreach (var l in listReligion)
                {
                    this.ltrMotherReligion.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                }

                // Race
                var listRace = en.TMasterDatas.Where(w => w.MasterType == "9").OrderBy(o => o.MasterDes).ToList();
                var tempRace = listRace.Where(w => w.MasterCode == "099").FirstOrDefault();
                int tempRaceIndex = listRace.FindIndex(f => f.MasterCode == "099");
                listRace.RemoveAt(tempRaceIndex);
                listRace.Insert(0, tempRace);
                foreach (var l in listRace)
                {
                    this.ltrMotherRace.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                }

                //Script Required Field
                RegisterOnline registerOnline = (RegisterOnline)this.Master;
                List<string> listElement = registerOnline.GetElementRequiredField(schoolID, 5);

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

            // Load province
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var result2 = dbMaster.provinces.ToList();

                if (result2.Count > 0)
                {
                    foreach (var r in result2)
                    {
                        this.ltrProvince.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.PROVINCE_ID, r.PROVINCE_NAME);
                    }
                }
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