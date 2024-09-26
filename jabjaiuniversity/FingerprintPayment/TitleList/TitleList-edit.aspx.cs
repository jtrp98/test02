using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.TitleList
{
    public partial class TitleList_edit : TitleGateway
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string id = Request.QueryString["id"];
                int id2 = Int32.Parse(id);

                btnSave.Click += new EventHandler(btnSave_Click);
                btnCancle.Click += new EventHandler(btnCancle_Click);
                if (!IsPostBack)
                {
                    var aa = _db.TTitleLists.Where(w => w.nTitleid == id2).FirstOrDefault();
                    txt.Text = aa.titleDescription;
                }
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {

            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string id = Request.QueryString["id"];
                int id2 = Int32.Parse(id);

                foreach (var a in _db.TTitleLists.Where(w => w.nTitleid == id2))
                {
                    a.titleDescription = txt.Text;
                }

                _db.SaveChanges();

                Response.Redirect("TitleList.aspx");
            }
        }
        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("TitleList.aspx");
        }
    }
}