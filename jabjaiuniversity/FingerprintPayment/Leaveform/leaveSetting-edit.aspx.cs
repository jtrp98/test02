using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Leaveform
{
    public partial class leaveSetting_edit : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancle.Click += new EventHandler(btnCancle_Click);


        }

        void btnSave_Click(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {


                int id = 0;
                Int32.TryParse(Request.QueryString["id"], out id);


                var data = _db.TDepartments.Where(w => w.DepID == id).FirstOrDefault();

                if (txtSearch.Text != "" && txtSearch.Text != null)
                {
                    string fullName = txtSearch.Text;
                    var names = fullName.Split(' ');
                    string firstName = names[0];



                    if (names.Length > 1)
                    {
                        string lastName = names[1];
                        var emp1 = _db.TEmployees.Where(w => w.sName == firstName && w.sLastname == lastName).FirstOrDefault();
                        if (emp1 != null)
                            data.userApproveOne = emp1.sEmp;
                    }
                    else
                    {
                        var emp2 = _db.TEmployees.Where(w => w.sName == firstName).FirstOrDefault();
                        if (emp2 != null)
                            data.userApproveOne = emp2.sEmp;
                    }
                }
                else
                {
                    data.userApproveOne = null;
                }

                if (txtSearch2.Text != "" && txtSearch2.Text != null)
                {
                    string fullName = txtSearch2.Text;
                    var names = fullName.Split(' ');
                    string firstName = names[0];

                    if (names.Length > 1)
                    {
                        string lastName = names[1];
                        var emp1 = _db.TEmployees.Where(w => w.sName == firstName && w.sLastname == lastName).FirstOrDefault();
                        if (emp1 != null)
                            data.userApproveTwo = emp1.sEmp;
                    }
                    else
                    {
                        var emp2 = _db.TEmployees.Where(w => w.sName == firstName).FirstOrDefault();
                        if (emp2 != null)
                            data.userApproveTwo = emp2.sEmp;
                    }
                }
                else
                {
                    data.userApproveTwo = null;
                }



                _db.SaveChanges();


                Response.Redirect("leaveSetting.aspx");



            }
        }

        void btnCancle_Click(object sender, EventArgs e)
        {


            Response.Redirect("leaveSetting.aspx");
        }

    }
}