using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class periodslist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            //btnSave.Click += new EventHandler(btnSave_Click);
            if (!Page.IsPostBack)
            {
                Opendata();
            }
        }

        private void Opendata()
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            lvListData.DataSource = _db.TPeriods.ToList();
            lvListData.DataBind();
        }

        //void btnSave_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("periodsadd.aspx");
        //}

        protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
        }

        protected void lvListData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lvListData_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }
    }
}