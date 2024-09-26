using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class subleveladdtable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //สร้างเวลาเฉพาะ
            btnCancle.Click += new EventHandler(btnCancel_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            if (!this.IsPostBack)
            {
                SiteMap.Provider.CurrentNode.ParentNode.ReadOnly = false;
                SiteMap.CurrentNode.ParentNode.Url = "/Modules/TimeAttendance/sublevelsettings.aspx?id=" + Server.UrlEncode(Request.QueryString["id"]) + "&type=" + Server.UrlEncode(Request.QueryString["type"]);
                ltrTabHeader.Text = @"<ul class='nav nav-tabs label-table subleveladdtable-label-table'>
                    <li class='active'><a data-toggle='tab' href='#' onclick='#'>สร้างเวลาเฉพาะ " + (Request.QueryString["key"] + "") + "</a></li></ul>";

                int nSubid = int.Parse((Request.QueryString["subid"] + ""));
                DataTable _dt = fcommon.LinqToDataTable(_db.TTermSubLevel2.Where(wde => wde.nTSubLevel == nSubid));
                fcommon.ListDataTableToDropDownList(_dt, ddlnTermSubLevel2, "ทั้งหมด", "nTermSubLevel2", "nTSubLevel2");

            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            string sTimeType = "เวลาเฉพาะ " + (Request.QueryString["key"] + "").Trim();
            Char _cDel1;
            //int _nTimeType = 1;
            int nTSubLevel = int.Parse((Request.QueryString["subid"] + ""));
            int nTermSubLevel2 = string.IsNullOrEmpty(Request.QueryString["sublv2"]) ? 0 : int.Parse(Request.QueryString["sublv2"]);
            TTimetype _Timetype = new TTimetype();
            if (_db.TTimetypes.Where(wde => wde.sTimeType == sTimeType).Count() == 0)
            {
                //_nTimeType = _db.TTimetypes.Count() == 0 ? 1 : _db.TTimetypes.Max(m => m.nTimeType) + 1;
                //_Timetype.nTimeType = _nTimeType;
                _Timetype.sTimeType = sTimeType;
                //if (ddlnTermSubLevel2.SelectedIndex > 0)
                //    _Timetype.sTimeType = _Timetype.sTimeType.Trim() + " / " + ddlnTermSubLevel2.SelectedItem.Text;
                _Timetype.cType = "2";
                _Timetype.cUserType = "1";
                
                _db.TTimetypes.Add(_Timetype);
                _db.SaveChanges();
                List<TTermSubLevel2> _lTermSubLevel2 = _db.TTermSubLevel2.Where(W => W.nTSubLevel == nTSubLevel).ToList();
                if (nTermSubLevel2 > 0)
                {
                    _lTermSubLevel2 = _lTermSubLevel2.Where(wde => wde.nTermSubLevel2 == nTermSubLevel2).ToList();
                }
                foreach (var _data in _lTermSubLevel2)
                {
                    _data.nTimeType = _Timetype.nTimeType;
                }
                _db.SaveChanges();
            }

            #region
            int i = 0;
            int nTimeType = _db.TTimetypes.Where(wde => wde.nTimeType == _Timetype.nTimeType).SingleOrDefault().nTimeType;
            while (i <= 6)
            {
                DateTime _dt1 = Convert.ToDateTime("01/01/2558 06:00:00"),
                    _dt2 = Convert.ToDateTime("01/01/2558 07:50:00"),
                    _dt3 = Convert.ToDateTime("01/01/2558 17:00:00"),
                    _dt4 = Convert.ToDateTime("01/01/2558 18:50:00"),
                    _dt5 = Convert.ToDateTime("01/01/2558 12:00:00");
                int nTimeLate = 120;
                _cDel1 = '0';
                switch (i)
                {
                    case 0:
                        if (chmon.Checked == true)
                        {
                            _dt1 = Convert.ToDateTime("01/01/2558 " + monstart1.Value + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + monstart2.Value + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + monstop1.Value + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + monstop2.Value + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + montimehalf.Value + ":00");

                            nTimeLate = int.Parse(montimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                    case 1:
                        if (chtue.Checked == true)
                        {
                            _dt1 = Convert.ToDateTime("01/01/2558 " + tuestart1.Value + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + tuestart2.Value + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + tuestop1.Value + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + tuestop2.Value + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + tuetimehalf.Value + ":00");

                            nTimeLate = int.Parse(tuetimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                    case 2:
                        if (chwes.Checked == true)
                        {
                            _dt1 = Convert.ToDateTime("01/01/2558 " + wesstart1.Value + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + wesstart2.Value + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + wesstop1.Value + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + wesstop2.Value + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + westimehalf.Value + ":00");

                            nTimeLate = int.Parse(westimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                    case 3:
                        if (chthu.Checked == true)
                        {
                            _dt1 = Convert.ToDateTime("01/01/2558 " + thustart1.Value + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + thustart2.Value + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + thustop1.Value + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + thustop2.Value + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + thutimehalf.Value + ":00");

                            nTimeLate = int.Parse(thutimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                    case 4:
                        if (chfri.Checked == true)
                        {
                            _dt1 = Convert.ToDateTime("01/01/2558 " + fristart1.Value + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + fristart2.Value + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + fristop1.Value + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + fristop2.Value + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + fritimehalf.Value + ":00");

                            nTimeLate = int.Parse(fritimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                    case 5:
                        if (chsat.Checked == true)
                        {
                            _dt1 = Convert.ToDateTime("01/01/2558 " + satstart1.Value + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + satstart2.Value + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + satstop1.Value + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + satstop2.Value + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + sattimehalf.Value + ":00");

                            nTimeLate = int.Parse(sattimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                    case 6:
                        if (chsun.Checked == true)
                        {
                            _dt1 = Convert.ToDateTime("01/01/2558 " + sunstart1.Value + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + sunstart2.Value + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + sunstop1.Value + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + sunstop2.Value + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + suntimehalf.Value + ":00");

                            nTimeLate = int.Parse(suntimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                }

                foreach (TTime _data in _db.TTimes.Where(w => w.nDay == i && w.nTimeType == nTimeType))
                {
                    _data.dTimeStart_IN = _dt1;
                    _data.dTimeEnd_IN = _dt2;
                    _data.dTimeStart_OUT = _dt3;
                    _data.dTimeEnd_OUT = _dt4;
                    _data.cDel = _cDel1.ToString();
                    _data.nTimeLate = nTimeLate;
                    _data.dTimeHalf = _dt5;
                }
                if (_db.TTimes.Where(w => w.nDay == i && w.nTimeType == nTimeType).Count() == 0)
                {
                    //int nTime = _db.TTimes.Max(max => max.TimeID) + 1;
                    _db.TTimes.Add(new TTime
                    {
                        nDay = i,
                        nTimeType = nTimeType,
                        //TimeID = nTime,
                        dTimeStart_IN = _dt1,
                        dTimeEnd_IN = _dt2,
                        dTimeStart_OUT = _dt3,
                        dTimeEnd_OUT = _dt4,
                        cDel = _cDel1.ToString(),
                        nTimeLate = nTimeLate,
                        dTimeHalf = _dt5,
                    });
                }
                i += 1;
                _db.SaveChanges();
            }
            #endregion

            Response.Redirect("sublevelsettings.aspx?id=" + Server.UrlEncode(Request.QueryString["id"]) + "&type=" + Server.UrlEncode(Request.QueryString["type"]));
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("sublevelsettings.aspx?id=" + Server.UrlEncode(Request.QueryString["id"]) + "&type=" + Server.UrlEncode(Request.QueryString["type"]));
        }
    }
}