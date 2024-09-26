using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using FingerprintPayment.Class;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using WebGrease.Css.Extensions;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using ExcelDataReader;
using System.Data.Entity.Validation;

namespace FingerprintPayment.studentCard
{
    public partial class studentCardConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
           

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());

                Button2.Click += new EventHandler(Button2_Click);





                if (tCompany != null)
                {
                    schoolpicture.Src = tCompany.sImage;
                    Img1.Src = tCompany.sImage;
                    Img2.Src = tCompany.sImage;
                }

                if (!IsPostBack)
                {

                }
            }
        }



        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }



        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

            }

        }



        void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("studentCardMain.aspx");
        }

        protected class PlanList
        {
            public int number { get; set; }
        }
        protected class PlanList2
        {
            public string code { get; set; }
            public int gradeId { get; set; }
        }
        protected class stdList
        {
            public string code { get; set; }
            public int sid { get; set; }
        }
        protected class ddl
        {
            public string name { get; set; }
            public string value { get; set; }
            public int sort { get; set; }
        }
    }
}