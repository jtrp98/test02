using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using System.Globalization;
using System.Web.DynamicData;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;

namespace FingerprintPayment.PreRegister
{
    public partial class completeRegister : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Click += new EventHandler(link);
         
            
        }

       

        void link(object sender, EventArgs e)
        {
            string sEntities = Request.QueryString["id"];
            
            Response.Redirect("registerUser.aspx?id=" + sEntities);
        }

        
    }
}