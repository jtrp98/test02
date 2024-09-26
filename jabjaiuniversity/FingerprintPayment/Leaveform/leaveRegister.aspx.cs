using JabjaiEntity.DB;
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
    public partial class leaveRegister : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                Button1.Click += new EventHandler(Button1_Click);
                string sEntities = Session["sEntities"] + "";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                foreach (var _dr in _dbMaster.TCompanies.Where(w => w.nCompany == nCompany.nCompany))
                {
                    labelSchool.Text = _dr.sCompany;
                }

                int sEmpID = int.Parse(Session["sEmpID"] + "");
                foreach (var _data in _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sEmp == sEmpID))
                {
                    labelName.Text = _data.sName + " " + _data.sLastname;
                }

                string date = DateTime.Now.Day.ToString();
                string month = DateTime.Now.Month.ToString();
                if (month == "1")
                    month = "มกราคม";
                else if (month == "2")
                    month = "กุมภาพันธ์";
                else if (month == "3")
                    month = "มีนาคม";
                else if (month == "4")
                    month = "เมษายน";
                else if (month == "5")
                    month = "พฤษภาคม";
                else if (month == "6")
                    month = "มิถุนายน";
                else if (month == "7")
                    month = "กรกฎาคม";
                else if (month == "8")
                    month = "สิงหาคม ";
                else if (month == "9")
                    month = "กันยายน";
                else if (month == "10")
                    month = "ตุลาคม";
                else if (month == "11")
                    month = "พฤศจิกายน";
                else if (month == "12")
                    month = "ธันวาคม";

                int y = DateTime.Now.Year;
                if (y < 2550)
                    y = y + 543;
                string year = y.ToString();

                labelDate.Text = date + " " + month + " " + year;

                int i = DateTime.Now.Year;
                if (i < 2550)
                    i = i + 543;
                startYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                endYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                i = i + 1;
                startYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                endYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                i = i + 1;
                startYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                endYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                i = i + 1;
                startYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                endYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                i = i + 1;
                startYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                endYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

        }

        void Button1_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = Session["sEntities"] + "";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");

                if (txtHeader.Text == "")
                {
                    Response.Write("<script>alert('กรุณากรอกข้อความให้ครบถ้วน');</script>");
                    return;
                }


                string type = "zzzzzzzzz";
                if (RadioSick.Checked == true)
                    type = "0";
                else if (RadioBusiness.Checked == true)
                    type = "1";
                else if (RadioSon.Checked == true)
                    type = "2";
                else if (RadioOther.Checked == true)
                    type = txtOther.Text;

                if (type == "zzzzzzzzz")
                {
                    Response.Write("<script>alert('กรุณาเลือกประเภทการลา');</script>");
                    return;
                }

                TLeaveLetter Letter = new TLeaveLetter();

                //int countletter = 1;
                //foreach (var data in _db.TLeaveLetters.Where(x => x.SchoolID == userData.CompanyID).ToList())
                //{
                //    countletter = countletter + 1;
                //}
                //Letter.letterId = countletter;
                Letter.letterSchoolId = nCompany.nCompany;

                int sEmpID = int.Parse(Session["sEmpID"] + "");
                Letter.writerId = sEmpID;
                if (DateTime.Now.Year > 2550)
                    Letter.letterDate = DateTime.Now.AddYears(-543);
                else Letter.letterDate = DateTime.Now;

                Letter.letterHeader = txtHeader.Text;

                Letter.writerJob = "1";
                Letter.letterType = type;
                Letter.writerComment = txtReason.Text;

                string stDate = startDate.SelectedValue;
                string stMonth = startMonth.SelectedValue;
                string stYear = startYear.SelectedValue;
                string combineStart = stDate + "/" + stMonth + "/" + stYear;
                DateTime? dt1;
                if (DateTime.ParseExact(combineStart, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    dt1 = DateTime.ParseExact(combineStart, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    dt1 = DateTime.ParseExact(combineStart, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);


                string edDate = endDate.SelectedValue;
                string edMonth = endMonth.SelectedValue;
                string edYear = endYear.SelectedValue;
                string combineEnd = edDate + "/" + edMonth + "/" + edYear;
                DateTime? dt2;
                if (DateTime.ParseExact(combineEnd, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    dt2 = DateTime.ParseExact(combineEnd, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    dt2 = DateTime.ParseExact(combineEnd, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);



                Letter.startDate = dt1;
                Letter.endDate = dt2;

                if (Letter.endDate < Letter.startDate)
                {
                    Response.Write("<script>alert('กรุณาตรวจสอบความถูกต้องของวันที่ลา');</script>");
                    return;
                }

                Letter.contactAumpher = txtAumpher.Text;
                Letter.contactHomenumber = txtHomenumber.Text;
                Letter.contactPhone = txtPhone.Text;
                Letter.contactProvince = Ddlprovince.SelectedValue;
                Letter.contactRoad = txtRoad.Text;
                Letter.contactTumbon = txtTumbon.Text;
                Letter.SchoolID = userData.CompanyID;

                _db.TLeaveLetter.Add(Letter);
                _db.SaveChanges();



                Response.Redirect("leaveList.aspx");
            }
        }
    }
}