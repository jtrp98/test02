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
using System.Text.RegularExpressions;

namespace FingerprintPayment.BP1
{
    public partial class courseDelete : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string idlv = Request.QueryString["idlv"];
                    string id = Request.QueryString["id"];
                    string sEntities = Session["sEntities"] + "";


                    var data1 = _db.TPlanes.Where(w => w.sPlaneID.ToString() == id).FirstOrDefault();

                    foreach (var a in _db.TPlanes.Where(w => w.sPlaneID.ToString() == id))
                    {
                        a.cDel = "1";

                    }

                    foreach (var b in _db.TCourseRegisters.Where(w => w.sPlaneID.ToString() == id))
                    {
                        b.courseStatus = "0";
                    }

                    _db.SaveChanges();
                    Response.Redirect("courseRegister.aspx?idlv=" + idlv);
                }
            }
        }
    }
}