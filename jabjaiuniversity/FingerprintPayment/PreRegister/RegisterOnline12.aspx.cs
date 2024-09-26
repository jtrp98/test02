using FingerprintPayment.Class;
using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterOnline12 : RegisterGateway
    {
        protected string NoAssumptionSriracha = "";
        protected string NoSuankularbNonthaburi = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            string specialScript = "";
            int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);
            if (schoolID != 229)
            {
                NoAssumptionSriracha = "no-assumption-sriracha";
            }
            if (schoolID != 1043)
            {
                NoSuankularbNonthaburi = "no-suankularb-nonthaburi";
            }
            else
            {
                specialScript = string.Format(@"$('.re-order').each(function(i){{$(this).text(parseInt($(this).text())+3);}});");
            }

            //Script Required Field
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string query = string.Format(@"SELECT InputFieldName
FROM TPreRegisterRequiredFieldInitiate rfi 
LEFT JOIN TPreRegisterRequiredField rf ON rfi.VFIID = rf.VFIID AND rfi.CategoryID = rf.CategoryID AND rf.SchoolID = {0}
WHERE rfi.IsDel = 0 AND ISNULL(rf.Status, rfi.DefaultStatus) = 1 AND rfi.CategoryID = 9 {1}{2}", schoolID, (schoolID != 229 ? " AND rfi.VFIID NOT IN (18, 19)" : ""), (schoolID != 1043 ? " AND rfi.VFIID NOT IN (166, 167, 168)" : ""));
                List<string> listElement = en.Database.SqlQuery<string>(query).ToList();

                string script = "";
                foreach (var e in listElement)
                {
                    // Exception
                    if (schoolID == 1043 && (e == "fileDocument06" || e == "fileDocument13" || e == "fileDocument024")) { script += string.Format(@"EnableFileUploadDocument('#{0}');", e); continue; }

                    script += string.Format(@"AddRequiredRulesvalidation('#{0}'); EnableFileUploadDocument('#{0}');", e);
                }

                ltrScriptRequiredField.Text = string.Format(@"<script>
        $(document).ready(function () {{
            {0} {1} ReNoFileUploadDocument(); ReapplyTableStriping();
        }});
    </script>", script, specialScript);
            }

        }

    }
}