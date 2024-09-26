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
    public partial class Joblist_switch : JobGateway
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

                foreach (var a in _db.TJobLists.Where(w => w.nJobid == id2))
                {
                    if (a.workStatus == "notworking")
                        a.workStatus = "working";
                    else a.workStatus = "notworking";

                }

                _db.SaveChanges();

                Response.Redirect("Joblist.aspx");
            }
        }
    }
}