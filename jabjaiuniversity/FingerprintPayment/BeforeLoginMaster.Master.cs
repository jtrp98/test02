using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace FingerprintPayment
{
    public partial class BeforeLoginMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteMapPath1.SiteMapProvider = "2SiteMap";
            SiteMapDataSource1.SiteMapProvider = "2SiteMap";
        }

        public HtmlGenericControl SetBody
        {
            get { return this.from2; }
        }
    }
}