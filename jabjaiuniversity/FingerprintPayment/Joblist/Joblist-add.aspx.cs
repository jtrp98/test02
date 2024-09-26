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
    public partial class Joblist_add : JobGateway
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
           

            int schoolID = UserData.CompanyID;

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                //string queryJobID = string.Format(@"SELECT ISNULL(MAX(CAST(REPLACE(CAST(nJobid AS VARCHAR(10)), CAST(SchoolID AS VARCHAR(4)), '') AS INT)), 0) + 1 FROM TJobList WHERE SchoolID = {0}", schoolID);
                //int newJobID = _db.Database.SqlQuery<int>(queryJobID).FirstOrDefault();
                //int jobID = Convert.ToInt32(string.Format(@"{0}{1}", schoolID.ToString().PadRight(3, '0'), newJobID.ToString().PadLeft(6, '0')));

                TJobList member = new TJobList();
                //member.nJobid = jobID;
                member.jobDescription = txt.Text;
                member.deleted = "0";
                member.workStatus = "working";
                member.nSchoolId = schoolID;
                member.empType = UserType.SelectedValue;
                member.SchoolID = schoolID;

                _db.TJobLists.Add(member);

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