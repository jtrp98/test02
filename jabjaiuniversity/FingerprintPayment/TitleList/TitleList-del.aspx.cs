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
    public partial class TitleList_del : TitleGateway
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

                foreach (var a in _db.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == id2))
                {
                    a.deleted = "1";
                    a.cDel = true;
                    a.UpdatedBy = UserData.UserID;
                    a.UpdatedDate = DateTime.Now;
                }

                _db.SaveChanges();
            }

            Response.Redirect("TitleList.aspx");
        }

    }
}