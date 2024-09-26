using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterEntity;
using JabjaiEntity.DB;
using JabjaiMainClass;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using FingerprintPayment.Qusetion.CsCode;
using FingerprintPayment.Class;
using Newtonsoft.Json.Linq;


namespace FingerprintPayment.Qusetion
{
    public partial class Summary_SDQ1 : StudentGateway
    {

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        private void InitialPage()
        {
            var SchoolID = GetUserData().CompanyID;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(SchoolID,ConnectionDB.Read)))
            {



            }
        }








    }





}