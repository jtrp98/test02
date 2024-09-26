using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.department
{
    public partial class DepartmentList_del : DepartmentGateway
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read);

            int schoolID = UserData.CompanyID;
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string id = Request.QueryString["id"];
                int id2 = Int32.Parse(id);

                foreach (var a in _db.TDepartments.Where(w => w.SchoolID == schoolID && w.DepID == id2))
                {
                    a.deleted = 1;
                    a.cDel = true;
                    a.UpdatedBy = UserData.UserID;
                    a.UpdatedDate = DateTime.Now;
                }

                _db.SaveChanges();

                Response.Redirect("DepartmentList.aspx");
            }
        }

    }
}