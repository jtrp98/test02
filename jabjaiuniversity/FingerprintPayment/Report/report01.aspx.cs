using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class repotr01 : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            btnSearch.Click += new EventHandler(btnSearch_Click);
            if (!Page.IsPostBack)
            {
                JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
                var userData = GetUserData();

                var q = (from a in dbschool.TEmployees.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID)
                         join b in dbschool.TEmpSalaries.Where(w => w.SchoolID == userData.CompanyID) on a.sEmp equals b.sEmp into jab

                         from jb in jab.DefaultIfEmpty()

                         where jb == null || (jb.WorkStatus ?? 1) == 1

                         select new
                         {
                             id = a.sEmp,
                             lable = a.sName + " " + a.sLastname
                         }).ToList();

                fcommon.LinqToDropDownList(q, txtSearch, " ", "id", "lable");
                SearchData();
            }
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            dgd.CurrentPageIndex = 0;
            SearchData();
        }
        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    break;
                case "Page":
                    dgd.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;
                    SearchData();
                    break;
                default:
                    break;
            }
        }
        private void SearchData()
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            DateTime _date = new DateTime();
            DateTime _dateStart = DateTime.Today;

            DateTime _dateEnd = new DateTime();
            var userData = GetUserData();
            if (!string.IsNullOrEmpty(txtdDate.Text))
            {
                _date = DateTime.ParseExact(txtdDate.Text, "dd/MM/yyyy", new CultureInfo("en-us"));
                if (_date.Year > int.Parse(DateTime.Now.ToString("yyyy", new CultureInfo("en-us")))) _date = _date.AddYears(-543);

                _dateStart = _date;
            }


            List<TSystemlog> _log = new List<TSystemlog>();

            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGDBForLogs)
            {
                using (var PGJabjaiLogs = new PostgreSQLLogs.JabjaiSchoolLogsContainer())
                {


                    string sql = string.Format("select * from public.{0}TSystemlog{1} where {0}SchoolID{1}= {2} and {0}dLog{1} between '{3}' and '{4}'  ", '"', '"', userData.CompanyID, _dateStart.ToString("yyyy-MM-dd 00:00:00"), _dateStart.ToString("yyyy-MM-dd 23:59:59"));
                    List<PostgreSQLLogs.TSystemlog> _SystemLog = PGJabjaiLogs.Database.SqlQuery<PostgreSQLLogs.TSystemlog>(sql).ToList();

                    _log = _SystemLog.Select(x => new TSystemlog
                    {
                        SystemLogID = x.SystemLogID,
                        sEmp = x.sEmp,
                        dLog = x.dLog,
                        sLog = x.sLog,
                        nSession = x.nSession,
                        nMenu = x.nMenu,
                        action = x.action,
                        systemtype = x.systemtype,
                        ip = x.ip,
                        SchoolID = x.SchoolID,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                        cDel = (bool)x.cDel

                    }).ToList();
                }
            }
            else
            {
                _log = _db.TSystemlogs.Where(w => w.SchoolID == userData.CompanyID).ToList();
            }
            if (!string.IsNullOrEmpty(txtdDate.Text))
            {
                _log = _log.Where(w => w.dLog.Date == _date).ToList();
            }

            if (!string.IsNullOrEmpty(ddlSearch.SelectedValue))
            {
                int menu_id = int.Parse(ddlSearch.SelectedValue);
                _log = _log.Where(w => w.nMenu == menu_id).ToList();
            }

            List<TEmployee> _TEmployees = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList();

            if (!string.IsNullOrEmpty(txtSearch.SelectedValue))
            {
                int id = int.Parse(txtSearch.SelectedValue);
                int sEmp = _TEmployees.Where(w => w.sEmp.Equals(id)).FirstOrDefault().sEmp;
                _log = _log.Where(w => w.sEmp == sEmp).ToList();
            }

            if (!string.IsNullOrEmpty(ddlaction.SelectedValue))
            {
                int action = int.Parse(ddlaction.SelectedValue);
                _log = _log.Where(w => w.action == action).ToList();
            }

            if (!string.IsNullOrEmpty(ddlplatform.SelectedValue))
            {
                int platform = int.Parse(ddlplatform.SelectedValue);
                _log = _log.Where(w => w.systemtype == platform).ToList();
            }

            DataTable _dt = fcommon.LinqToDataTable(_log.OrderByDescending(o => o.dLog));

            List<report1> report1 = new List<report1>();
            (from a in _TEmployees
             join b in _log on a.sEmp equals b.sEmp
             orderby b.dLog descending
             select new { sName = a.sName + " " + a.sLastname, b.dLog, b.sLog }).ToList().ForEach(f => report1.Add(new report1 { sName = f.sName, sLog = f.sLog.Replace(",", ", "), dLog = f.dLog }));

            dgd.DataSource = report1;
            dgd.DataBind();
        }
    }
    class report1
    {

        public string sName { get; set; }
        public DateTime dLog { get; set; }
        public string sLog { get; set; }
    }
}
