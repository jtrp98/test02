using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiMainClass;
using JabjaiEntity.DB;

namespace FingerprintPayment
{
    public partial class company : System.Web.UI.Page
    {
        //FingerPaymentEntities _db = new FingerPaymentEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            if (!Page.IsPostBack)
            {
                string comid = Request.QueryString["comid"];
                string sEntities = fcommon.Get_Value(fcommon.connMaster, "SELECT sEntities FROM TCompany WHERE nCompany = " + comid);
                JabJaiEntities _db = new JabJaiEntities(sEntities);
                if (_db.TEmployees.ToList().Count > 0) Response.Redirect("Default.aspx");
                //if (_db.TCompanies.Count() > 0) Response.Redirect("addemployeesstartsystem.aspx");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string comid = Request.QueryString["comid"];
            if (string.IsNullOrEmpty(comid)) Response.Redirect("Default.aspx");
            string sEntities = fcommon.Get_Value(fcommon.connMaster, "SELECT sEntities FROM TCompany WHERE nCompany = " + comid);
            if (string.IsNullOrEmpty(sEntities)) Response.Redirect("Default.aspx");
            HttpContext.Current.Session["sEntities"] = sEntities;
            string SQL = "";
            string sImage = "";
            if (fulLogo.FileName == "")
            {
                sImage = "sImage";
            }
            else
            {
                //sImage = string.Format("(SELECT BulkColumn FROM Openrowset( Bulk '{0}', Single_Blob) as img)", fulLogo.PostedFile.FileName);
                //FileStream FS = new FileStream(@"~/" + fulLogo.PostedFile.FileName, FileMode.Open, FileAccess.Read); //create a file stream object associate to user selected file 
                byte[] img = fulLogo.FileBytes; //new byte[FS.Length]; //create a byte array with size of user select file stream length
                //FS.Read(img, 0, Convert.ToInt32(FS.Length));//read user selected file stream in to byte array
                sImage = "'" + Convert.ToBase64String(img, 0, img.Length) + "'";
            }
            SQL = string.Format(@"UPDATE [dbo].[TCompany]
            SET sImage = {0}
            ,sAddress = '{1}'
            ,sCompany = '{2}'
            ,sTel = '{3}'
             WHERE sEntities = '{4}'", sImage, txtsAddress.Text, txtsCompany.Text, txtsTel.Text, Session["sEntities"] + "");
            fcommon.ExecuteNonQuery(fcommon.connMaster, SQL);
            Response.Redirect("addemployeesstartsystem.aspx");
            //Response.Redirect("AdminMain.aspx");
        }
    }
}