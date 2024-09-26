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
    public partial class DepartmentList_edit : DepartmentGateway
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
                    var aa = _db.TDepartments.Where(w => w.DepID == id2).FirstOrDefault();
                    txt.Text = aa.departmentName;
                }
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
           

            int schoolID = UserData.CompanyID;
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                int id = 0;
                Int32.TryParse(Request.QueryString["id"], out id);

                string fullName = txtSearch3.Text;
                var names = fullName.Split(' ');
                string firstName = names[0];

                int userid = 0;

                if (names.Length > 1)
                {
                    string lastName = names[1];
                    var emp1 = _db.TEmployees.Where(w => w.SchoolID == schoolID && w.sName == firstName && w.sLastname == lastName).FirstOrDefault();
                    if (emp1 != null)
                        userid = emp1.sEmp;
                }
                else
                {
                    var emp2 = _db.TEmployees.Where(w => w.SchoolID == schoolID && w.sName == firstName).FirstOrDefault();
                    if (emp2 != null)
                        userid = emp2.sEmp;
                }

                foreach (var d in _db.TDepartments.Where(w => w.DepID == id))
                {
                    d.departmentName = txt.Text;
                    d.userHeadId = userid;
                }

                _db.SaveChanges();

                Response.Redirect("DepartmentList.aspx");
            }
        }
        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("DepartmentList.aspx");
        }

    }
}