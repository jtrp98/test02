using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.Services;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System.Web.Script.Services;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class timeiosettings : System.Web.UI.Page
    {
        Char _cDel1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            btnSave.Click += new EventHandler(btnSave_Click);
            btnInsertData.Click += new EventHandler(btnInsertData_Click);
            btnEditData.Click += BtnEditData_Click;
            String nDay;
            if (!IsPostBack)
            {
                ListHeader();
            }

        }

        private void BtnEditData_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            int nTimeType = int.Parse(txtid.Text);
            foreach (var _data in _db.TTimetypes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTimeType == nTimeType))
            {
                _data.sTimeType = txtsTimetype.Text;
                _data.cUserType = rblcUserType.SelectedValue.ToString();
                _data.UpdatedBy = userData.UserID;
                _data.UpdatedDate = DateTime.Now;
            }
            _db.SaveChanges();
            ListHeader();
        }

        private void ListHeader()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            //ltrTabHeader.Text = "<ul class='nav nav-tabs' style='width: 90%; font-size: 30px; margin-left: 58px;'>";
            int _index = 0;
            int _startType1 = 0;
            int _startType2 = 0;
            ltrTabHeaderMenu1.Text = "";
            ltrTabHeaderMenu2.Text = "";
            foreach (var _data in _db.TTimetypes.Where(w => w.SchoolID == userData.CompanyID).Where(wde => string.IsNullOrEmpty(wde.cDel)))
            {
                if (hdfnTimeType.Value == "")
                {
                    hdfnTimeType.Value = _data.nTimeType.ToString();
                }
                string _str = "_" + (_data.cType == "1" ? _startType1 : _startType2);
                if (_data.cType == "2") _startType2 += 1;
                string _sFuntionEdit = "";
                if (_data.cType == "1" && mp.Permission_Page.permission == "0")
                {
                    _sFuntionEdit = @"ondblclick='FunctionPopUp(""show"",""แก้ไจข้อมูล""," + _data.nTimeType +
                    @"," + '"' + _data.sTimeType + '"' + "," + @"" + _data.cUserType + @"" + ")'";
                }
                if (_data.cType == "1")
                {
                    ltrTabHeaderMenu1.Text += "<li id='type" + _data.cType + _str + "'><a ' href='#' id='" + _index
                    + "' onclick='GetData(" + "" + _data.nTimeType + "" + @"," + '"' + "type" + _data.cType + _str + '"' + "," + '"' + _data.cUserType + '"' + @")' " + _sFuntionEdit + " >" + _data.sTimeType + "</a></li>";
                }
                else
                {
                    ltrTabHeaderMenu2.Text += "<li id='type" + _data.cType + _str + "'><a href='#' id='" + _index
               + "' onclick='GetData(" + "" + _data.nTimeType + "," + '"' + "type" + _data.cType + _str + '"' + "," + '"' + _data.cUserType + '"' + @")' " + _sFuntionEdit + " >" + _data.sTimeType + "</a></li>";

                }
                if (_data.cType == "1") _startType1++;
                if (_data.cType == "2") _startType2++;
            }
            ltrTabHeaderMenu1.Text += @"<li id='type1' style='Display:none;' class=""btnpermission""><a href='#' style='background-color:#449d44;'  onclick=""FunctionPopUpInser('show','เพิ่มข้อมูล')"" >เพิ่มข้อมูล</a></li>";
            //else ltrTabHeader.Text += "</ul>";
            //rblTimeType.DataSource = fcommon.LinqToDataTable(_db.TTimetype);
            //rblTimeType.DataTextField = "sTimeType";
            //rblTimeType.DataValueField = "nTimeType";
            //rblTimeType.DataBind();
            //rblTimeType.SelectedIndex = 0;
        }

        void btnInsertData_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            TTimetype _Timetype = new TTimetype();
            //int _nTimeType = _db.TTimetypes.Count() == 0 ? 1 : _db.TTimetypes.Max(m => m.nTimeType) + 1;
            //_Timetype.nTimeType = _nTimeType;
            _Timetype.sTimeType = txtsTimetype.Text;
            _Timetype.cType = "1";
            _Timetype.cUserType = rblcUserType.SelectedValue;
            _Timetype.SchoolID = userData.CompanyID;
            _Timetype.CreatedBy = userData.UserID;
            _Timetype.CreatedDate = DateTime.Now;

            _db.TTimetypes.Add(_Timetype);
            _db.SaveChanges();

            #region
            int i = 0;
            //int _nTime = _db.TTimes.Max(m => m.TimeID);
            while (i <= 6)
            {
                DateTime _dt1 = Convert.ToDateTime("01/01/2558 06:00:00"),
                    _dt2 = Convert.ToDateTime("01/01/2558 07:50:00"),
                    _dt3 = Convert.ToDateTime("01/01/2558 17:00:00"),
                    _dt4 = Convert.ToDateTime("01/01/2558 18:50:00"),
                    _dt5 = Convert.ToDateTime("01/01/2558 12:00:00");
                _cDel1 = '0';
                //_nTime++;
                _cDel1 = i < 5 ? '1' : '0';
                TTime _data = new TTime();
                _data.dTimeStart_IN = _dt1;
                _data.dTimeEnd_IN = _dt2;
                _data.dTimeStart_OUT = _dt3;
                _data.dTimeEnd_OUT = _dt4;
                _data.dTimeHalf = _dt5;
                _data.nTimeLate = 120;
                _data.cDel = _cDel1.ToString();
                _data.nTimeType = _Timetype.nTimeType;
                //_data.TimeID = _nTime;
                _data.nDay = i;
                _data.SchoolID = userData.CompanyID;
                _data.CreatedBy = userData.UserID;
                _data.CreatedDate = DateTime.Now;

                _db.TTimes.Add(_data);
                _db.SaveChanges();
                i++;
            }
            #endregion
            Response.Redirect("/Modules/TimeAttendance/timeiosettings.aspx");
        }
        public void setValue(String day, String start1, String start2, String stop1, String stop2)
        {
            switch (day)
            {
                case "0":
                    monstart1.Value = start1;
                    monstart2.Value = start2;
                    monstop1.Value = stop1;
                    monstop2.Value = stop2;
                    break;
                case "1":
                    tuestart1.Value = start1;
                    tuestart2.Value = start2;
                    tuestop1.Value = stop1;
                    tuestop2.Value = stop2;
                    break;
                case "2":
                    wesstart1.Value = start1;
                    wesstart2.Value = start2;
                    wesstop1.Value = stop1;
                    wesstop2.Value = stop2;
                    break;
                case "3":
                    thustart1.Value = start1;
                    thustart2.Value = start2;
                    thustop1.Value = stop1;
                    thustop2.Value = stop2;
                    break;
                case "4":
                    fristart1.Value = start1;
                    fristart2.Value = start2;
                    fristop1.Value = stop1;
                    fristop2.Value = stop2;
                    break;
                case "5":
                    satstart1.Value = start1;
                    satstart2.Value = start2;
                    satstop1.Value = stop1;
                    satstop2.Value = stop2;
                    break;
                case "6":
                    sunstart1.Value = start1;
                    sunstart2.Value = start2;
                    sunstop1.Value = stop1;
                    sunstop2.Value = stop2;
                    break;
            }
        }

        [ScriptMethod]
        [WebMethod(EnableSession = true)]
        public static object SaveData(List<TTime> times)
        {
            return null;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            int i = 0;
            int nTimeType = int.Parse(hdfnTimeType.Value);
            var qtimetype = _db.TTimetypes.Find(nTimeType, userData.CompanyID);
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
                            _dt1 = Convert.ToDateTime("01/01/2558 " + monstart1.Value.Replace(".", ":") + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + monstart2.Value.Replace(".", ":") + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + monstop1.Value.Replace(".", ":") + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + monstop2.Value.Replace(".", ":") + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + montimehalf.Value + ":00");

                            nTimeLate = int.Parse(montimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                    case 1:
                        if (chtue.Checked == true)
                        {
                            _dt1 = Convert.ToDateTime("01/01/2558 " + tuestart1.Value.Replace(".", ":") + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + tuestart2.Value.Replace(".", ":") + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + tuestop1.Value.Replace(".", ":") + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + tuestop2.Value.Replace(".", ":") + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + tuetimehalf.Value + ":00");

                            nTimeLate = int.Parse(tuetimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                    case 2:
                        if (chwes.Checked == true)
                        {
                            _dt1 = Convert.ToDateTime("01/01/2558 " + wesstart1.Value.Replace(".", ":") + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + wesstart2.Value.Replace(".", ":") + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + wesstop1.Value.Replace(".", ":") + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + wesstop2.Value.Replace(".", ":") + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + westimehalf.Value + ":00");

                            nTimeLate = int.Parse(westimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                    case 3:
                        if (chthu.Checked == true)
                        {
                            _dt1 = Convert.ToDateTime("01/01/2558 " + thustart1.Value.Replace(".", ":") + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + thustart2.Value.Replace(".", ":") + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + thustop1.Value.Replace(".", ":") + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + thustop2.Value.Replace(".", ":") + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + thutimehalf.Value + ":00");

                            nTimeLate = int.Parse(thutimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                    case 4:
                        if (chfri.Checked == true)
                        {
                            _dt1 = Convert.ToDateTime("01/01/2558 " + fristart1.Value.Replace(".", ":") + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + fristart2.Value.Replace(".", ":") + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + fristop1.Value.Replace(".", ":") + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + fristop2.Value.Replace(".", ":") + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + fritimehalf.Value + ":00");

                            nTimeLate = int.Parse(fritimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                    case 5:
                        if (chsat.Checked == true)
                        {
                            _dt1 = Convert.ToDateTime("01/01/2558 " + satstart1.Value.Replace(".", ":") + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + satstart2.Value.Replace(".", ":") + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + satstop1.Value.Replace(".", ":") + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + satstop2.Value.Replace(".", ":") + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + sattimehalf.Value + ":00");

                            nTimeLate = int.Parse(sattimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                    case 6:
                        if (chsun.Checked == true)
                        {
                            _dt1 = Convert.ToDateTime("01/01/2558 " + sunstart1.Value.Replace(".", ":") + ":00");
                            _dt2 = Convert.ToDateTime("01/01/2558 " + sunstart2.Value.Replace(".", ":") + ":00");
                            _dt3 = Convert.ToDateTime("01/01/2558 " + sunstop1.Value.Replace(".", ":") + ":00");
                            _dt4 = Convert.ToDateTime("01/01/2558 " + sunstop2.Value.Replace(".", ":") + ":00");
                            _dt5 = Convert.ToDateTime("01/01/2558 " + suntimehalf.Value + ":00");

                            nTimeLate = int.Parse(suntimelate.Value);
                            _cDel1 = '1';
                        }
                        break;
                }

                foreach (TTime _data in _db.TTimes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nDay == i && w.nTimeType == nTimeType))
                {
                    _data.dTimeStart_IN = _dt1;
                    _data.dTimeEnd_IN = _dt2;
                    _data.dTimeStart_OUT = _dt3;
                    _data.dTimeEnd_OUT = _dt4;
                    _data.cDel = _cDel1.ToString();
                    _data.nTimeLate = nTimeLate;
                    _data.dTimeHalf = _dt5;
                    _data.UpdatedBy = userData.UserID;
                    _data.UpdatedDate = DateTime.Now;
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
                        SchoolID = userData.CompanyID,
                        CreatedBy = userData.UserID,
                        CreatedDate = DateTime.Now
                    });
                }
                i += 1;
                _db.SaveChanges();
            }

            database.InsertLog(Session["sEmpID"] + "", "แก้ไขข้อมูลเวลาเข้า - ออกโรงเรียน ตาราง " + qtimetype.nTimeType + " : " + qtimetype.sTimeType,
                HttpContext.Current.Session["sEntities"].ToString(), Request, 22, 3, 0);

            Response.Redirect("/Modules/TimeAttendance/timeiosettings.aspx");
        }

    }
}