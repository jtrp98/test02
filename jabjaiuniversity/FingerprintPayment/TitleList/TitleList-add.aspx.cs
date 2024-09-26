using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.TitleList
{
    public partial class TitleList_add : TitleGateway
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

                //string queryTitleID = string.Format(@"SELECT ISNULL(MAX(CAST(REPLACE(CAST(nTitleid AS VARCHAR(10)), CAST(SchoolID AS VARCHAR(4)), '') AS INT)), 0) + 1 FROM TTitleList WHERE SchoolID = {0}", schoolID);
                //int newTitleID = _db.Database.SqlQuery<int>(queryTitleID).FirstOrDefault();
                //int titleID = Convert.ToInt32(string.Format(@"{0}{1}", schoolID.ToString().PadRight(3, '0'), newTitleID.ToString().PadLeft(6, '0')));

                TTitleList member = new TTitleList();
                //member.nTitleid = titleID;            
                member.titleDescription = txt.Text;
                member.deleted = "0";
                member.workStatus = "working";
                member.nSchoolId = schoolID;
                member.SchoolID = schoolID;

                _db.TTitleLists.Add(member);

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