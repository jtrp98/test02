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
    public partial class DepartmentList_add : DepartmentGateway
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancle.Click += new EventHandler(btnCancle_Click);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();

            //int id = 0;
            //Int32.TryParse(Request.QueryString["id"], out id);

            int schoolID = UserData.CompanyID;
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID));

            TDepartment depart = new TDepartment();

            if (txtSearch2.Text != "" && txtSearch2.Text != null)
            {
                string fullName = txtSearch2.Text;
                var names = fullName.Split(' ');
                string firstName = names[0];

                if (names.Length > 1)
                {
                    string lastName = names[1];
                    var emp1 = _db.TEmployees.Where(w => w.SchoolID == schoolID && w.sName == firstName && w.sLastname == lastName).FirstOrDefault();
                    if (emp1 != null)
                        depart.userHeadId = emp1.sEmp;
                }
                else
                {
                    var emp2 = _db.TEmployees.Where(w => w.SchoolID == schoolID && w.sName == firstName).FirstOrDefault();
                    if (emp2 != null)
                        depart.userHeadId = emp2.sEmp;
                }
            }

            depart.deleted = 0;
            //depart.departmentId = count;
            depart.departmentName = txt.Text;
            depart.userApproveOne = null;
            depart.userApproveTwo = null;
            depart.SchoolID = schoolID;

            _db.TDepartments.Add(depart);

            _db.SaveChanges();

            Response.Redirect("DepartmentList.aspx");
        }
        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("DepartmentList.aspx");
        }

    }
}