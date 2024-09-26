using FingerprintPayment.Class;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class UpdateLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!IsPostBack)
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    // List Message System
                    // var list = dbmaster.TMessageSystems.OrderByDescending(o => o.AddDate).ToList();
                    var qry = "";
                    if (userData.IsAdmin)
                    {
                        qry = $@" SELECT A.NewsID, A.Created, A.Title, A.NoteAppWeb 'Detail'
                            FROM JabjaiMasterSingleDB.dbo.TNews2  A                          
                            WHERE IsNoteAppWeb = 1                           
                            AND (A.SchoolID IS NULL OR A.SchoolID = {userData.CompanyID} )
                            --AND GETDATE() BETWEEN StartDate AND EndDate 
                            AND A.IsDelete = 0 ";
                    }
                    else
                    {
                        qry = $@" SELECT A.NewsID, A.Created, A.Title, A.NoteAppWeb 'Detail'
                            FROM JabjaiMasterSingleDB.dbo.TNews2  A
                            JOIN JabjaiMasterSingleDB.dbo.TNewsPushNotify  B ON A.SchoolID = B.SchoolID AND A.NewsID = B.NewsID
                            WHERE IsNoteAppWeb = 1 
                            AND B.UserID = {userData.UserID}
                            AND (A.SchoolID IS NULL OR A.SchoolID = {userData.CompanyID} )
                            --AND GETDATE() BETWEEN StartDate AND EndDate 
                            AND A.IsDelete = 0 ";
                    }

                    string query = $@"SELECT *
FROM
(
	SELECT ID 'NewsID', AddDate 'Created', Title, Message 'Detail' 
    FROM TMessageSystem 
	
    UNION

    SELECT NewsID, Created, Title, Detail 
    FROM TNews 
    WHERE remark='RemindInvoice' and SchoolId = {userData.CompanyID} AND GETDATE() BETWEEN StartDate AND EndDate

    UNION 

    SELECT NewsID, Created, Title, Detail 
    FROM TNews 
    WHERE IsBroadcast = 1 --ORDER BY Created DESC

    UNION

	{qry}

) A
ORDER BY Created DESC
";
                    var list = dbmaster.Database.SqlQuery<BroadcastModel>(query).ToList();
                    if (list.Count > 0)
                    {
                        foreach (var l in list)
                        {
                            //string timeAgo = ComFunction.GetTimeSince(l.AddDate.Value);
                            this.ltrMessageSystem.Text += string.Format(@"
                        <div class=""dropdown-item d-flex align-items-center"" href=""#"" data-id=""{0}"">
                            <div class=""dropdown-list-image mr-3"">
                                <img class=""rounded-circle"" src=""images/School Bright logo only.png"" style=""width: 48px;"" alt="""">
                            </div>
                            <div class=""font-weight-bold"" style=""width: 100%; padding-left: 60px;"">
                                <div style=""font-size: 26px;"">{1}</div>
                                <div class=""text-truncate"">{2}</div>
                                <div class=""small text-gray-500 word-wrap"">{3}</div>
                            </div>
                        </div>
                        <hr style=""margin-top: 10px; margin-bottom: 10px;""/>", l.NewsID, l.Created.ToString("yyyy-MM-dd", new CultureInfo("th-TH")), l.Title, l.Detail.Replace("\n", "<br />"));
                        }
                    }
                    else
                    {
                        this.ltrMessageSystem.Text = "<center>ไม่พบข่าวสารประกาศจากโรงเรียน</center>";
                    }
                }
            }
        }
    }
}