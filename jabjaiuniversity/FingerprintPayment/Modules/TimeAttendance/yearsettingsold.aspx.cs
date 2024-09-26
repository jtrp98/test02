using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class yearsettingsold : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            if (!IsPostBack)
            {
                if (_db.TYears.Where(w => w.YearStatus == "1").Count() == 0)
                {
                    btnnew.Visible = true;
                }
                else { btnnew.Visible = false; }
            }
            Opendata();
        }

        private void Opendata()
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            dgd.DataSource = _db.TYears
                .OrderBy(w => w.numberYear).ToList();
            dgd.DataBind();
        }

        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            /*  string nHoliday = e.CommandArgument.ToString();
              switch (e.CommandName)
              {
                  case "Del":
                      _db.THolidays.Where(w => w.nHoliday == nHoliday).ToList().ForEach(f => f.cDel = "1");
                      _db.SaveChanges();
                      Opendata();
                      break;
              }*/
        }

        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.Cells[1].Text == "1")
                {
                    e.Item.Cells[1].Text = "ยังไม่จบปีการศึกษา";
                    TYear yourDataSource = (TYear)e.Item.DataItem;
                    int term = fcommon.LinqToDataTable(_db.TTerms.Where(w => w.nYear == yourDataSource.nYear)).Rows.Count;
                    LinkButton _btnManage = e.Item.FindControl("btnManage") as LinkButton;
                    _btnManage.Attributes.Add("onclick", "modalYear('" + yourDataSource.numberYear + "','"+term+"');return false;");
                    _btnManage.Attributes.Add("data-toggle", "modal");
                    _btnManage.Attributes.Add("data-target", "#modalYear");
                    _btnManage.Attributes.Add("year-id", yourDataSource.nYear.ToString());

                    LinkButton _btnStdManage = e.Item.FindControl("btnStdManage") as LinkButton;
                    _btnStdManage.Attributes.Add("onclick", "modalStudent();return false;");
                    _btnStdManage.Attributes.Add("data-toggle", "modal");
                    _btnStdManage.Attributes.Add("data-target", "#Studentmodal");
                    _btnStdManage.Attributes.Add("year-id", yourDataSource.nYear.ToString());


                    LinkButton _btnCongrate = e.Item.FindControl("btnCongrate") as LinkButton;
                    _btnCongrate.Attributes.Add("onclick", "modalCongrate();return false;");
                    _btnCongrate.Attributes.Add("data-toggle", "modal");
                    _btnCongrate.Attributes.Add("data-target", "#modalCongrate");
                    _btnCongrate.Attributes.Add("year-id", yourDataSource.nYear.ToString());

                    Panel _panelNull = e.Item.FindControl("pnlNull") as Panel;
                    _panelNull.Visible = false;
                }
                else
                {
                    e.Item.Cells[1].Text = "จบปีการศึกษา";
                    LinkButton _btnManage = e.Item.FindControl("btnManage") as LinkButton;
                    _btnManage.Visible = false;
                    LinkButton _btnStdManage = e.Item.FindControl("btnStdManage") as LinkButton;
                    _btnStdManage.Visible = false;
                    LinkButton _btnCongrate = e.Item.FindControl("btnCongrate") as LinkButton;
                    _btnCongrate.Visible = false;
                   Panel _panelNull = e.Item.FindControl("pnlNull") as Panel;
                    _panelNull.Visible = true;
                }
                 
                /*LinkButton _btnManage = e.Item.FindControl("btnManage") as LinkButton;
                _btnManage.Attributes.Add("onclick", "modalYear(1);return false;");
                _btnManage.Attributes.Add("data-toggle", "modal");
                _btnManage.Attributes.Add("data-target", "#modalYear");
                _btnManage.Attributes.Add("year-id", yourDataSource.nYear.ToString());*/
            }
        }

    }
}