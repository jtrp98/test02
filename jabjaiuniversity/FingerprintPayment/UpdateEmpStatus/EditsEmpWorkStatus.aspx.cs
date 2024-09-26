using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages.Html;

namespace FingerprintPayment.UpdateEmpStatus
{
    public partial class EditsEmpWorkStatus : System.Web.UI.Page
    {
        public List<SelectListItem> LeaveData { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }


            if (userData.CompanyID == 825)
            {
                if (userData.UserID != 383482)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                LeaveData = db.TLeave_Type.Where(o => o.SchoolID == userData.CompanyID && o.IsDel == false && o.Type == "T")
                   .Select(o => new
                   {
                       o.TypeID,
                       o.TypeName,
                   })
                   .AsEnumerable()
                   .Select(o => new SelectListItem
                   {
                       Text = o.TypeName,
                       Value = o.TypeID + "",
                   })
                   .ToList();

            }
        }
    }
}