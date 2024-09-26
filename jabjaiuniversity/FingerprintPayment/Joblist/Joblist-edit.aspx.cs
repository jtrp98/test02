using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class Joblist_edit : JobGateway
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           

            int schoolID = UserData.CompanyID;

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string id = Request.QueryString["id"];
                int id2 = Int32.Parse(id);

                btnSave.Click += new EventHandler(btnSave_Click);
                btnCancle.Click += new EventHandler(btnCancle_Click);
                if (!IsPostBack)
                {
                    var aa = _db.TJobLists.Where(w => w.SchoolID == schoolID && w.nJobid == id2).FirstOrDefault();
                    txt.Text = aa.jobDescription;
                }
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            

            int schoolID = UserData.CompanyID;

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string id = Request.QueryString["id"];
                int id2 = Int32.Parse(id);

                foreach (var a in _db.TJobLists.Where(w => w.SchoolID == schoolID && w.nJobid == id2))
                {
                    a.jobDescription = txt.Text;
                    a.empType = UserType.SelectedValue;
                }

                _db.SaveChanges();

                Response.Redirect("Joblist.aspx");
            }
        }

        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Joblist.aspx");
        }
    }
}