using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class userlist2_delete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));

            int id = 0;
            Int32.TryParse(Request.QueryString["id"], out id);

            foreach (var _data in _db.TUsers.Where(w => w.sID == id))
            {
                _data.cDel = "1";
                database.InsertLog(Session["sEmpID"] + "", "ลบข้อมูลนักเรียน " + _data.sName + " " + _data.sLastname,
                    Session["sEntities"].ToString(), Request, 14, 4, 0);
            }
            _db.SaveChanges();

            #region Add Data Master
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            foreach (var _data2 in _dbMaster.TUsers.Where(w => w.sID == id))
            {
                _data2.cDel = "1";
            }


            _dbMaster.SaveChanges();

            #endregion


            Response.Redirect("userlist2.aspx");
        }
    }
}