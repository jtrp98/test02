using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;

namespace FingerprintPayment
{
    public partial class maclist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnCancel.Click += new EventHandler(btnCancel_Click);
            //btnSave.Click += new EventHandler(btnSave_Click);
            dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            if (Session["sEmpID"] == null) Response.Redirect("/Default.aspx");
            //timecode.Tick += Timecode_Tick;
            if (!IsPostBack)
            {
                Opendata();
                //JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
                //DataTable _dt = fcommon.LinqToDataTable(_db.TClass.ToList());
                //fcommon.ListDataTableToDropDownList(_dt, ddlClass, "", "sClassID", "sClass");

            }
        }

        private void Timecode_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session["nAvermentID"] + ""))
            {
                DataTable _dt = fcommon.Get_Data(fcommon.connMaster, @"SELECT * FROM TAverment where nAvermentID = '" + Session["nAvermentID"] + "' AND sMac IS NOT NULL ");
                if (_dt.Rows.Count > 0)
                {
                    Session["nAvermentID"] = "";
                    Opendata();
                    Response.Redirect("maclist.aspx");
                }
            }

        }

        private void Opendata()
        {
            DataTable _dt = fcommon.Get_Data(fcommon.connMaster, "SELECT * FROM TComputer WHERE nCompany = (SELECT nCompany FROM TCompany WHERE sEntities = '" + fcommon.GetCookies() + "') ");
            dgd.DataSource = _dt;
            dgd.DataBind();
        }

        private void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string sClassID = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "Del":
                    fcommon.ExecuteNonQuery(fcommon.connMaster, "DELETE TComputer WHERE nComputerID = " + sClassID);
                    Opendata();
                    break;
                case "Edit":
                    foreach (DataRow _dr in fcommon.Get_Data(fcommon.connMaster, "SELECT * FROM TComputer WHERE nComputerID = " + sClassID).Rows)
                    {
                        //txtsClass.Text = _dr["sComputerName"] + "";
                        //string[] sMac = (_dr["sMac"] + "").Split(':');
                        //txtsMac1.Text = sMac[0];
                        //txtsMac2.Text = sMac[1];
                        //txtsMac3.Text = sMac[2];
                        //txtsMac4.Text = sMac[3];
                        //txtsMac5.Text = sMac[4];
                        //txtsMac6.Text = sMac[5];
                        //hfdsClassID.Value = _dr["nComputerID"] + "";
                        //type1.Checked = _dr["cType"] + "" == "0";
                        //type2.Checked = _dr["cType"] + "" == "1";
                        //type1.Disabled = true;
                        //type2.Disabled = true;
                        //ddlClass.Enabled = false;
                        //ddlClass.SelectedItem.Text = _dr["sComputerName"] + "";
                    }

                    break;
            }
        }

        private void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                LinkButton _btnDel = e.Item.FindControl("btnDel") as LinkButton;
                //_btnDel.Attributes.Add("onclick", "j_confirm('ยืนยันการลบข้อมูล','คุณต้องการที่จะลบข้อมูลนี้หรือไม่ ?','" + _btnDel.UniqueID + "'); return false;");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
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
            //Response.Redirect("maclist.aspx");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("maclist.aspx");
        }
    }
}