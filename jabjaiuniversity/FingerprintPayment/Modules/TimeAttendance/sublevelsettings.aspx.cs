using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class sublevelsettings : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);

            if (Session["sEntities"] != null)
            {
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    if (!Page.IsPostBack)
                    {
                        rblTimeType.DataSource = _db.TTimetypes.Where(w => w.SchoolID == userData.CompanyID).ToList();
                        rblTimeType.DataTextField = "sTimeType";
                        rblTimeType.DataValueField = "nTimeType";
                        rblTimeType.DataBind();
                        rblTimeType.SelectedValue = Request.QueryString["type"] + "";
                    }
                    ListHeader();
                    OpenData();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        private void ListHeader()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            ltrTabHeader.Text = "<ul class='nav nav-tabs' style='width: 90%; font-size: 30px;'>";
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                bool _status = true;
                foreach (var _data in _db.TTimetypes.Where(w => w.SchoolID == userData.CompanyID).ToList())
                {
                    _status = int.Parse(rblTimeType.SelectedValue.ToString()) == _data.nTimeType;
                    if (_status) ltrTabHeader.Text += "<li " + (_status ? "class='active'" : "") + @"><a data-toggle='tab' href='#'>" + _data.sTimeType + "</a></li>";
                    _status = false;
                }
                ltrTabHeader.Text += @"</ul>";
            }
        }

        private void OpenData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sID = (Request.QueryString["id"] + "");
                int dataID = Int32.Parse(sID);
                //int _type = int.Parse(rblTimeType.SelectedValue);

                var _listslv = from a in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                               join b in _db.TTimetypes.ToList()
                               on a.nTimeType equals b.nTimeType
                               where a.nTLevel == dataID
                               select new { a.nTSubLevel, b.sTimeType, a.SubLevel };

                dgd.DataSource = _listslv.ToList();
                dgd.DataBind();

                foreach (DataGridItem _dgi in dgd.Items)
                {
                    Label lblListSub = _dgi.FindControl("lblListSub") as Label;
                    //string _id = Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", int.Parse(STCrypt.DecryptURL(Request.QueryString["id"] + "")[0]))));
                    //string _type = Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", rblTimeType.SelectedValue)));
                    //string _subid = Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", _dgi.Cells[3].Text)));
                    //string _key = Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", _dgi.Cells[4].Text)));
                    string _id = Server.UrlEncode(string.Format("{0:000000}", int.Parse(Request.QueryString["id"] + "")));
                    string _type = Server.UrlEncode(rblTimeType.SelectedValue);
                    string _subid = Server.UrlEncode(string.Format("{0:000000}", _dgi.Cells[3].Text));
                    string _key = Server.UrlEncode(string.Format("{0:000000}", _dgi.Cells[4].Text));
                    int nTSubLevel = int.Parse(_dgi.Cells[3].Text);
                    lblListSub.Text += @"</td>";

                    foreach (var _data in _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).Where(wde => wde.nTSubLevel == nTSubLevel))
                    {
                        string sTime = _db.TTimetypes.Where(w => w.SchoolID == userData.CompanyID).Where(wde => wde.nTimeType == _data.nTimeType).SingleOrDefault().sTimeType;
                        _key = Server.UrlEncode(_dgi.Cells[4].Text.Trim() + " / " + _data.nTSubLevel2);
                        lblListSub.Text += @"</tr><tr id='sublv" + _data.nTSubLevel + "_" + _data.nTermSubLevel2 + @"' class=""alternateCell hidden"" style=""font - style: normal; font - weight: normal; text - decoration: none; "">
                <td align = ""center"" style = ""font-style: normal; font-weight: normal; text-decoration: none;"" > " + _dgi.Cells[4].Text + " / " + _data.nTSubLevel2 + @"</td>
                <td> " + sTime + @" </td>
                <td>
                    <a class=""btn btn-danger btnpermission"" href='subleveladdtable.aspx?id=" + _id + "&type=" + _type + "&subid=" + _subid + "&key=" + _key + "&sublv2=" + _data.nTermSubLevel2 + @"' >สร้างตารางเวลาเฉพาะ</a>&nbsp;
                    <a class=""btn btn-primary btnpermission"" href='subleveledit.aspx?id= " + _id + " &type=" + _type + " &subid= " + _subid + "&key=" + _key + "&sublv2=" + _data.nTermSubLevel2 + @"' data-original-title=""เลือกตารางเวลา"" data-toggle=""tooltip"" data-placement=""right"">เลือกตารางเวลาเฉพาะ</a>
                </td>";
                    }
                    lblListSub.Text += "";
                }
            }
        }

        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string _id = Request.QueryString["id"];// Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", int.Parse(STCrypt.DecryptURL(Request.QueryString["id"] + "")[0]))));
            string _type = rblTimeType.SelectedValue;// Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", rblTimeType.SelectedValue)));
            string _subid = "";
            string _key = "";
            switch (e.CommandName.ToString())
            {

                case "Add":
                    Response.Redirect("subleveladd.aspx?id=" + _id + "&type=" + _type);
                    break;
                case "AddTable":
                    //_subid = Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", e.Item.Cells[3].Text)));
                    //_key = Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", e.Item.Cells[4].Text)));
                    _subid = Server.UrlEncode(e.Item.Cells[3].Text);
                    _key = Server.UrlEncode(e.Item.Cells[4].Text);
                    Response.Redirect("subleveladdtable.aspx?id=" + _id + "&type=" + _type + "&subid=" + _subid + "&key=" + _key);
                    break;
                case "EditTable":
                    //_subid = Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", e.Item.Cells[3].Text)));
                    //_key = Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", e.Item.Cells[4].Text)));
                    _subid = Server.UrlEncode(e.Item.Cells[3].Text);
                    _key = Server.UrlEncode(e.Item.Cells[4].Text);
                    Response.Redirect("subleveledit.aspx?id=" + _id + "&type=" + _type + "&subid=" + _subid + "&key=" + _key);
                    break;
                case "Page":
                    dgd.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;
                    OpenData();
                    break;
            }
        }
    }
}