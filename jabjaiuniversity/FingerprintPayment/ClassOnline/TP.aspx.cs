using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.ClassOnline
{
    public partial class TP : BaseOnlinePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (PermissionType.NoPermission == PermissionHelper.GetPermissionOnWEB(6, UserData.UserID, UserData.CompanyID))
            //{
            //    Response.Redirect("~/NoPermission.aspx");               
            //}


            if (!this.IsPostBack)
            {
                //var schoolId = Request.QueryString["school"] + "";

                //if (!string.IsNullOrEmpty(schoolId))
                //{
                //    var id = Convert.ToInt32(schoolId);
                //    var aws = new AWSFunction();
                //    aws.UpdateAllTUserBySchool(id);
                //}

                //RemoveCollection();


                //ImportNFC();
            }
        }

    }
}

