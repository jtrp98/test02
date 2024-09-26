﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class reportempscanning : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["sEmpID"] != null)
            {
                string checkpermission = HttpContext.Current.Session["sEmpID"].ToString();

                if (!String.IsNullOrEmpty(checkpermission))
                {
                    checkpermission = checkpermission.Substring(11, 1);
                    if (checkpermission == "0")
                    {
                    }
                }
            }
            else
            {
                Response.Redirect("/Default.aspx");
            }
        }


        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            JabJaiEntities _db2 = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            DataTable dtName = fcommon.LinqToDataTable(_db2.TEmployees.Where(w => w.cDel == null && (w.sName.Contains(prefixText) || w.sLastname.Contains(prefixText) || w.sIdentification.Contains(prefixText))));
            List<string> customers = new List<string>();
            if (dtName != null)
            {
                foreach (DataRow dr in dtName.Rows)
                {
                    customers.Add(dr["sName"] + " " + dr["sLastname"]);
                }
            }
            return customers;
        }
    }
}