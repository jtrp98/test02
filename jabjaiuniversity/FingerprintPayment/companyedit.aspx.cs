using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiMainClass;

namespace FingerprintPayment
{
    public partial class companyedit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            if (!Page.IsPostBack)
            {
                if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
                //JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
                //if (_db.TCompanies.Count() > 0) Response.Redirect("addemployeesstartsystem.aspx");
                OpenData();
            }
        }

        private void OpenData()
        {
            DataTable _dt = fcommon.Get_Data(fcommon.connMaster, "SELECT * FROM TCompany WHERE sEntities = '" + Session["sEntities"] + "'");
            foreach (DataRow _dr in _dt.Rows)
            {
                txtsAddress.Text = _dr["sAddress"] + "";
                txtsCompany.Text = _dr["sCompany"] + "";
                txtsTel.Text = _dr["sTel"] + "";
                if (!string.IsNullOrEmpty(_dr["sImage"] + ""))
                {
                    //byte[] bytes = (byte[])_dr["sImage"];
                    //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    imgLogo.ImageUrl = "data:image/png;base64," + _dr["sImage"];
                    ViewState["sImage"] = _dr["sImage"];
                }

            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string SQL = "";
            string sImage = "";
            if (fulLogo.FileName == "")
            {
                sImage = "'" + ViewState["sImage"] + "'";
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
            Response.Redirect("AdminMain.aspx");
        }

    }

}