using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using FingerprintPayment.Class;
using JabjaiNoSQL;
using JabjaiNoSQL.Behavior;
using MasterEntity;
using JabjaiEntity.DB;
using JabjaiMainClass;

namespace FingerprintPayment
{
    public partial class behavior_log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            //btnSearch.Click += new EventHandler(btnSearch_Click);
            if (!Page.IsPostBack) SearchData();
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
            dgd.CurrentPageIndex = 0;
        }
        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                //LinkButton _btnDel = e.Item.FindControl("btnDel") as LinkButton;
                //_btnDel.Attributes.Add("onclick", "j_confirm('ยืนยันการลบข้อมูล','คุณต้องการที่จะลบข้อมูลนี้หรือไม่ ?','" + _btnDel.UniqueID + "');return false;");
            }
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
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            DataContext tdb = new DataContext();
            string SQL = "";


            SQL = @"select TB1.*,SubLevel,TB2.nTSubLevel2
                    from TUser AS TB1 left join TTermSubLevel2 AS TB2 on TB1.nTermSubLevel2 = TB2.nTermSubLevel2
                    left join TSubLevel AS TB3 on TB3.nTSubLevel = TB2.nTSubLevel
                    where cDel IS NULL ";

            var bhistory = new Repository<BehaviorHistory>(tdb);

            string txtSearch = Request.QueryString["txtSearch"];
            string txtdDate = Request.QueryString["txtdDate"];

            int inputDay = 0;
            int inputMonth = 0;
            int inputYear = 0;

            if (string.IsNullOrEmpty(txtdDate))
            {
                inputDay = 0;
                inputMonth = 0;
                inputYear = 0;
            }
            else
            {
                string xx = txtdDate.Substring(0, 2);
                string yy = txtdDate.Substring(3, 2);
                string zz = txtdDate.Substring(6);

                inputDay = int.Parse(xx);
                inputMonth = int.Parse(yy);

                Int32.TryParse(zz, out inputYear);
            }

            string teachername = "";
            int sEmpID = int.Parse(Session["sEmpID"] + "");
            foreach (var _data in _db.TEmployees.Where(w => w.sEmp == sEmpID))
            {
                teachername = _data.sName + " " + _data.sLastname;
                //lbllogin.Text = "ยินดีต้อนรับคุณ " + _data.sName + " " + _data.sLastname + " เข้าสู่ระบบ";
            }


            string input = txtdDate; // dd-MM-yyyy    
            DateTime d;
            DateTime.TryParseExact(input, "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out d);


            List<TEmployee> _TEmployees = _db.TEmployees.ToList();

            List<StudentLog> stuloglist = new List<StudentLog>();
            List<StudentLog> stuloglist2 = new List<StudentLog>();
            List<StudentLog> stuloglistfull = new List<StudentLog>();
            StudentLog stulog = new StudentLog();

            var q1 = (from a in bhistory.List(w => w.Type != "คงเหลือ")
                      join b in _db.TUsers.ToList() on a.StudentId equals b.sID
                      select new
                      {
                          a.Type,
                          a.Score,
                          a.UserAddId,
                          a.BehaviorName
                      ,
                          a.DayAdd,
                          a.StudentId,
                          b.sName,
                          b.sLastname
                      }).ToList();

            foreach (var h in q1)
            {
                stulog = new StudentLog();
                stulog.type = h.Type;
                stulog.score = Math.Abs(h.Score);
                stulog.UserAddId = teachername;
                stulog.behaviorName = h.BehaviorName;
                stulog.dayAdd = h.DayAdd;
                stulog.dataDay = h.DayAdd.Day;
                stulog.dataMonth = h.DayAdd.Month;
                stulog.dataYear = h.DayAdd.Year;
                stulog.studentId = h.StudentId;
                stulog.studentName = h.sName + " " + h.sLastname;
                stulog.studentFirst = h.sName;
                stulog.studentLast = h.sLastname;
                stulog.studentTerm = 1;
                stulog.studentYear = 2000;
                stuloglist.Add(stulog);
            }

            stuloglistfull = stuloglist;

            if (string.IsNullOrEmpty(txtSearch))
            {
                stuloglist = stuloglistfull;
            }
            else
            {
                foreach (var data in stuloglist.Where(w => w.studentFirst == txtSearch || w.studentLast == txtSearch || w.studentName == txtSearch))
                {
                    stuloglist2.Add(data);
                }
                stuloglist = stuloglist2;
            }
            if (string.IsNullOrEmpty(txtdDate))
            {
                stuloglist = stuloglist;
            }
            else
            {
                foreach (var data in stuloglist.Where(w => w.dataDay == inputDay && w.dataMonth == inputMonth && w.dataYear == inputYear).ToList())
                {
                    stuloglist2.Add(data);
                }
                stuloglist = stuloglist2;
            }



            //  List<behaviorlog> report1 = new List<behaviorlog>();
            //  (from a in _TEmployees
            //   join b in _log on a.sEmp equals b.nEmp
            //   orderby b.dLog ascending 
            //   select new { sName = a.sName + " " + a.sLastname, b.dLog, b.sLog }).ToList().ForEach(f => report1.Add(new behaviorlog { sName = f.sName, sLog = f.sLog, dLog = f.dLog }));

            dgd.DataSource = stuloglist;
            dgd.DataBind();
        }
        protected void ddlcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            txtSearch.Text = "";
            dgd.DataSource = _db.TUsers.Where(w => string.IsNullOrEmpty(w.cDel)).ToList();
            dgd.CurrentPageIndex = 0;
            dgd.DataBind();
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            dgd.DataSource = _db.TUsers.Where(w => (w.sName + " " + w.sLastname).Contains(txtSearch.Text) && string.IsNullOrEmpty(w.cDel)).ToList();
            dgd.CurrentPageIndex = 0;
            dgd.DataBind();
        }

    }


    class StudentLog
    {
        public int studentId { get; set; }
        public string behaviorName { get; set; }
        public string studentName { get; set; }
        public int studentYear { get; set; }
        public int studentTerm { get; set; }
        public string studentFirst { get; set; }
        public string studentLast { get; set; }
        public DateTime dayAdd { get; set; }
        public string UserAddId { get; set; }
        public int dataDay { get; set; }
        public int dataMonth { get; set; }
        public int dataYear { get; set; }
        public int score { get; set; }
        public string type { get; set; }
    }
}
