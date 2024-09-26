using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class macsetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnCancel.Click += BtnCancel_Click;
            btnSave.Click += BtnSave_Click;
            if (!this.IsPostBack) {
                JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
                DataTable _dt = fcommon.LinqToDataTable(_db.TClasses.ToList());
                fcommon.ListDataTableToDropDownList(_dt, ddlClass, "", "sClassID", "sClass");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //int nComputerID = 0;
            //string nCompany = "";
            //string SQL = "";
            //string sMac = txtsMac1.Text + ":" + txtsMac2.Text + ":" + txtsMac3.Text + ":" + txtsMac4.Text + ":" + txtsMac5.Text + ":" + txtsMac6.Text;
            //string cType = type1.Checked ? "0" : "1";
            //if (string.IsNullOrEmpty(hfdsClassID.Value))
            //{
            //    nComputerID = int.Parse(fcommon.Get_Value(fcommon.connMaster, "SELECT TOP 1 nComputerID+1 FROM TComputer ORDER BY nComputerID DESC"));
            //    nCompany = fcommon.Get_Value(fcommon.connMaster, "SELECT nCompany FROM TCompany WHERE sEntities = '" + fcommon.GetCookies() + "'");
            //    SQL = string.Format(@"INSERT INTO TComputer
            //(nComputerID,sComputerName,sMac,nCompany,cType) VALUES 
            //('{0}','{1}','{2}','{3}','{4}')", nComputerID, txtsClass.Text, sMac, nCompany, cType);

            //}
            //else {
            //    SQL = string.Format(@"UPDATE [TComputer] SET 
            //    sComputerName = '{1}',sMac = '{2}' 
            //    WHERE nComputerID = {0}"
            //, hfdsClassID.Value, txtsClass.Text, sMac, nCompany);
            //}
            //fcommon.ExecuteNonQuery(fcommon.connMaster, SQL);
            Response.Redirect("maclist.aspx");
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("maclist.aspx");
        }
    }
}