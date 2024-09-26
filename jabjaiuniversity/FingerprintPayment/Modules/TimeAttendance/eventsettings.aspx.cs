using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class eventsettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEntities"] + "")) Response.Redirect("/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            btnSave.Click += new EventHandler(btnSave_Click);
            dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            dgd2.ItemDataBound += new DataGridItemEventHandler(dgd2_ItemDataBound);
            dgd2.ItemCommand += new DataGridCommandEventHandler(dgd2_ItemCommand);
            if (!IsPostBack)
            {

                
            }
            Opendata();
            Opendata2();
        }

        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                //THoliday yourDataSource = (THoliday)e.Item.DataItem;
                //LinkButton _setBtn1 = e.Item.FindControl("setBtn1") as LinkButton;
                //_setBtn1.Attributes.Add("onclick", "modalHoliday('" + yourDataSource.nHoliday + "'); return false;");
                //_setBtn1.Attributes.Add("data-toggle", "modal");
                //_setBtn1.Attributes.Add("data-target", "#myModal");
                //_setBtn1.Attributes.Add("holiday-id", yourDataSource.nHoliday);
                //LinkButton _btnDel = e.Item.FindControl("btnDel") as LinkButton;
                //_btnDel.Attributes.Add("onclick", "j_confirm('ยืนยันการลบข้อมูล','คุณต้องการที่จะลบข้อมูลนี้หรือไม่ ?','" + _btnDel.UniqueID + "'); return false;");
            }
        }

        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            string nHoliday = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "Del":
                    _db.THolidays.Where(w => w.nHoliday == nHoliday).ToList().ForEach(f => f.cDel = "1");
                    _db.SaveChanges();
                    Opendata();
                    Opendata2();
                    break;
            }
        }

        void dgd2_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                //THoliday yourDataSource = (THoliday)e.Item.DataItem;
                LinkButton _setBtn2 = e.Item.FindControl("setBtn2") as LinkButton;
                _setBtn2.Attributes.Add("onclick", "tabtimetype('" + e.Item.Cells[4].Text + "','modalSet','modalsub'); return false;");
                _setBtn2.Attributes.Add("data-toggle", "modal");
                _setBtn2.Attributes.Add("data-target", "#myModal");
                _setBtn2.Attributes.Add("holiday-id", e.Item.Cells[4].Text);
                //LinkButton _btnDel2 = e.Item.FindControl("lnkDel") as LinkButton;
                //_btnDel2.Attributes.Add("onclick", "j_confirm('ยืนยันการลบข้อมูล','คุณต้องการที่จะลบข้อมูลนี้หรือไม่ ?','" + _btnDel2.UniqueID + "'); return false;");

            }
        }

        void dgd2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            string nHoliday = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "Del":
                    _db.THolidays.Where(w => w.nHoliday == nHoliday).ToList().ForEach(f => f.cDel = "1");
                    _db.SaveChanges();
                    Opendata();
                    Opendata2();
                    break;
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {

            ContentPlaceHolder MainContent = Page.Master.FindControl("MainContent") as ContentPlaceHolder;
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            THoliday _Holiday = new THoliday();
            _Holiday.sHoliday = txtHoliday.Value;
            if (rd1.Checked)
            { _Holiday.sHolidayType = "3"; }
            else { _Holiday.sHolidayType = "4"; }

            _Holiday.dHolidayStart = DateTime.ParseExact(txtHolidayStart.Value, "dd/MM/yyyy", new CultureInfo("en-us"));
            _Holiday.dHolidayEnd = DateTime.ParseExact(txtHolidayEnd.Value, "dd/MM/yyyy", new CultureInfo("en-us"));



            _Holiday.nHoliday = GenID();
            _db.THolidays.Add(_Holiday);

            foreach (string _str in txtListtime.Text.Split(','))
            {
                if (!string.IsNullOrEmpty(_str))
                {
                    string nHolidaySomeID = "HS" + (DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff") + _db.THolidaySomes.Count() + 1).ToString().PadLeft(48, '0');
                    THolidaySome _THolidaySome = new THolidaySome();
                    _THolidaySome.nTSubLevel = int.Parse(_str);
                    _THolidaySome.nHoliday = _Holiday.nHoliday;
                    _THolidaySome.nHolidaySomeID = nHolidaySomeID;
                    _db.THolidaySomes.Add(_THolidaySome);
                    _db.SaveChanges();
                }
            }

            _db.SaveChanges();
            txtHoliday.Value = "";
            txtHolidayStart.Value = "";
            txtHolidayEnd.Value = "";

            if (rd1.Checked)
            { Opendata(); }
            else { Opendata2(); }
        }

        public class eventday
        {
            public string nHoliday { get; set; }
            public String name { get; set; }
            public DateTime? start { get; set; }
            public DateTime? end { get; set; }
            public bool delete { get; set; }

        }

        private void Opendata()
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            List<eventday> eventlist = new List<eventday>();
            eventday even = new eventday();
            foreach (var a in _db.THolidays.Where(w => w.cDel != "1" && w.sHolidayType == "3").OrderByDescending(w => w.dHolidayStart).ToList())
            {
                even = new eventday();
                even.name = a.sHoliday;
                even.start = a.dHolidayStart;
                even.end = a.dHolidayEnd;
                even.nHoliday = a.nHoliday;
                even.delete = true;
                eventlist.Add(even);
            }
            if (eventlist.Count == 0)
            {
                even = new eventday();
                even.nHoliday = "1";
                even.name = "";
                even.start = null;
                even.end = null;
                even.delete = false;
                eventlist.Add(even);
            }
            dgd.DataSource = eventlist;
            dgd.PageSize = 100;
            dgd.DataBind();
                        
        }

        private void Opendata2()
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            List<eventday> eventlist2 = new List<eventday>();
            eventday even2 = new eventday();
            foreach (var a in _db.THolidays.Where(w => w.cDel != "1" && w.sHolidayType == "4").OrderByDescending(w => w.dHolidayStart).ToList())
            {
                even2 = new eventday();
                even2.name = a.sHoliday;
                even2.start = a.dHolidayStart;
                even2.end = a.dHolidayEnd;
                even2.nHoliday = a.nHoliday;
                even2.delete = true;
                eventlist2.Add(even2);
            }
            if (eventlist2.Count == 0)
            {
                even2 = new eventday();
                even2.nHoliday = "1";
                even2.name = "";
                even2.start = null;
                even2.end = null;
                even2.delete = false;
                eventlist2.Add(even2);
            }
            dgd2.DataSource = eventlist2;
            dgd2.PageSize = 100;
            dgd2.DataBind();
        }

        private string GenID()
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            string nHoliday = "H0001";
            if (_db.THolidays.Count() > 0)
            {
                nHoliday = _db.THolidays.OrderByDescending(O => O.nHoliday).FirstOrDefault().nHoliday;
                nHoliday = "H" + string.Format("{0:0000}", int.Parse(nHoliday.Replace("H", "")) + 1);
            }
            return nHoliday;
        }
    }
}