using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class plans_room : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                OpenData();
            }
        }

        private void OpenData()
        {
            //lvLevel.DataSource = _db.TLevel;
            //lvLevel.DataBind();
        }
    }
}