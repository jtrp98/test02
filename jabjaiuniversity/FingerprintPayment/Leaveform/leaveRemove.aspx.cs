using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class leaveRemove : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {


                int id = 0;
                Int32.TryParse(Request.QueryString["id"], out id);

                var f_letter = _db.TLeaveLetter.Where(w => w.letterId == id).FirstOrDefault();

                f_letter.deleted = 1;
                f_letter.letterStatus = "Canceled";
                _db.SaveChanges();

                var user = _dbMaster.TUsers.FirstOrDefault(f => f.sID == f_letter.writerId);

                LeaveLogic leaveLogic = new LeaveLogic(_db);
                leaveLogic.CancelStatus(id, user);


                //f_letter.deleted = 1;
                //f_letter.letterStatus = "Canceled";

                //if (f_letter.writerJob == "0")
                //{
                //    _db.TLogLearnTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.LogLearnDate >= f_letter.startDate && w.LogLearnDate <= f_letter.endDate && w.sID == f_letter.writerId && w.sUserType == "0")
                //        .ToList().ForEach(f =>
                //        {
                //            _db.TLogLearnTimeScans.Remove(f);
                //        });

                //    var q_data = _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.sID == f_letter.writerId && w.LogDate >= f_letter.startDate && w.LogDate <= f_letter.endDate).ToList();
                //    foreach (var f_data in q_data)
                //    {
                //        if (f_data.LogDate <= DateTime.Today)
                //        {
                //            f_data.bLockStatus = null;
                //            f_data.LogScanStatus = "99";
                //        }
                //        else
                //        {
                //            _db.TLogUserTimeScans.Remove(f_data);
                //        }
                //    }
                //    _db.SaveChanges();

                //}
                //else
                //{
                //    var q_data = _db.TLogEmpTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == f_letter.writerId && w.LogEmpDate >= f_letter.startDate && w.LogEmpDate <= f_letter.endDate).ToList();
                //    foreach (var f_data in q_data)
                //    {
                //        if (f_data.LogEmpDate <= DateTime.Today)
                //        {
                //            f_data.LogEmpScanStatus = "3";
                //        }
                //        else
                //        {
                //            _db.TLogEmpTimeScans.Remove(f_data);
                //        }
                //    }
                //    _db.SaveChanges();
                //}


                Response.Redirect("leaveList.aspx");
            }


        }
    }
}