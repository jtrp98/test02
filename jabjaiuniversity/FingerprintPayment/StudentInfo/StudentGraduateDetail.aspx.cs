using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.StudentInfo
{
    public partial class StudentGraduateDetail : StudentGateway
    {
        protected int? NextSID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                // Find next TUser.sID
                int sid = Convert.ToInt32(Request.QueryString["sid"]);
                if (sid != 0)
                {
                    var user = en.TUser.Where(w => w.sID == sid).FirstOrDefault();
                    if (user != null)
                    {
                        int? classID = user.nTermSubLevel2;
                        if (classID != null)
                        {
                            string queryFindNextSID = string.Format(@"
WITH CTE AS (
	SELECT rn = ROW_NUMBER() OVER (ORDER BY u.nStudentNumber, sStudentID), u.sID
	FROM TUser u
	WHERE u.SchoolID = {0} AND u.cDel IS NULL AND u.nTermSubLevel2 = {1}
)
SELECT nex.sID NextValue
FROM CTE
LEFT JOIN CTE prev ON prev.rn = CTE.rn - 1
LEFT JOIN CTE nex ON nex.rn = CTE.rn + 1
WHERE CTE.sID = {2}", schoolID, classID, sid);

                            NextSID = en.Database.SqlQuery<int?>(queryFindNextSID).FirstOrDefault();

                            NextSID = NextSID == null ? 0 : NextSID;
                        }
                    }
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadDistrict(int provinceID)
        {
            List<EntityDropdown> result = null;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                result = dbMaster.amphurs.Where(w => w.PROVINCE_ID == provinceID).Select(s => new EntityDropdown { id = s.AMPHUR_ID.ToString(), name = s.AMPHUR_NAME }).ToList();

                return result;
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadSubDistrict(int districtID)
        {
            List<EntityDropdown> result = null;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                result = dbMaster.districts.Where(w => w.AMPHUR_ID == districtID).Select(s => new EntityDropdown { id = s.DISTRICT_ID.ToString(), name = s.DISTRICT_NAME }).ToList();

                return result;
            }
        }

    }
}