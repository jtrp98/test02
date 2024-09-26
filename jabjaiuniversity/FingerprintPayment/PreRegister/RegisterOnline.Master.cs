using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterOnline : System.Web.UI.MasterPage
    {
        protected string schoolLogo = "";
        protected string schoolName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegisterOnlineSchoolLogo"] != null)
            {
                schoolLogo = (string)Session["RegisterOnlineSchoolLogo"];
            }
            if (Session["RegisterOnlineSchoolName"] != null)
            {
                schoolName = (string)Session["RegisterOnlineSchoolName"];
            }
        }

        public List<string> GetElementRequiredField(int schoolID, int categoryID)
        {
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string query = string.Format(@"SELECT InputFieldName
FROM TPreRegisterRequiredFieldInitiate rfi 
LEFT JOIN TPreRegisterRequiredField rf ON rfi.VFIID = rf.VFIID AND rfi.CategoryID = rf.CategoryID AND rf.SchoolID = {0}
WHERE rfi.IsDel = 0 AND ISNULL(rf.Status, rfi.DefaultStatus) = 1 AND rfi.CategoryID = {1}", schoolID, categoryID);
                List<string> listElement = en.Database.SqlQuery<string>(query).ToList();

                return listElement;
            }
        }
    }
}