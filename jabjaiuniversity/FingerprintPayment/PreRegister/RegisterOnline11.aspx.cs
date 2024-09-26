using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Internal.Entities;
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
    public partial class RegisterOnline11 : RegisterGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                //Script Required Field
                RegisterOnline registerOnline = (RegisterOnline)this.Master;
                List<string> listElement = registerOnline.GetElementRequiredField(schoolID, 8);

                string script = "";
                foreach (var e in listElement)
                {
                    switch (e)
                    {
                        case "iptAllergySymptoms":
                            script += string.Format(@"AddRequiredRulesvalidation('input[name={0}]');", e);
                            break;
                        default:
                            script += string.Format(@"AddRequiredRulesvalidation('#{0}');", e);
                            break;
                    }
                }

                ltrScriptRequiredField.Text = string.Format(@"<script>
        $(document).ready(function () {{
            {0}
        }});
    </script>", script);
            }

        }
    }
}