using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class grade_quiz_edit : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["permission"] == null) Response.Redirect("~/Default.aspx");
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {


                btnSave.Click += new EventHandler(btnSave_Click);
                btnCancle.Click += new EventHandler(btnCancle_Click);


                Year.Text = Request.QueryString["year"];
                Term.Text = Request.QueryString["term"];
                testNumber.Text = Request.QueryString["test"];

                if (!IsPostBack)
                {

                    OpenData();
                }
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string nterm = "";
                int nyear = 0;
                string yyy = Request.QueryString["year"];
                int? useryear = Int32.Parse(yyy);
                string userterm = Request.QueryString["term"];
                string planid = Request.QueryString["plan"];


                foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear))
                {
                    nyear = ff.nYear;
                }
                foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.cDel == null))
                {
                    nterm = ee.nTerm;
                }

                string birthDate = ddlDay.SelectedValue;
                string birthMonth = ddlMonth.SelectedValue;
                string birthYear = ddlAge.SelectedValue;
                string combinedate = birthDate + "/" + birthMonth + "/" + birthYear;
                DateTime dt1 = DateTime.ParseExact(combinedate, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                int sEmpID = int.Parse(Session["sEmpID"] + "");
                string test = Request.QueryString["test"];
                int testnumber = Int32.Parse(test);


                foreach (DataGridItem dgItem in dgd.Items)
                {
                    TextBox txtScore = (TextBox)dgItem.FindControl("txtScore");
                    string score = txtScore.Text;
                    TextBox txtReScore = (TextBox)dgItem.FindControl("txtReScore");
                    string rescore = txtReScore.Text;
                    TextBox txtNote = (TextBox)dgItem.FindControl("txtNote");
                    string note = txtNote.Text;
                    string studentid = dgd.Items[dgItem.DataSetIndex].Cells[3].Text;

                    var data = _db.TQuizs.Where(w => w.sPlanid == planid && w.sTerm == nterm && w.nNumber == testnumber && w.sID == studentid).FirstOrDefault();

                    data.QuizDay = dt1;
                    if (rescore != "" && rescore != null)
                        data.dReScore = Int32.Parse(rescore);
                    else data.dReScore = null;

                    if (maxscore.Text != "" && maxscore.Text != null)
                        data.dfullScore = Int32.Parse(maxscore.Text);
                    else data.dfullScore = null;

                    if (score != "" && score != null)
                        data.dscore = Int32.Parse(score);
                    else data.dscore = null;

                    data.sEmp = sEmpID;
                    data.sNote = note;
                    data.dDatetimeUpdate = DateTime.Now;

                }

                _db.SaveChanges();


                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                string year = Request.QueryString["year"];
                string term = Request.QueryString["term"];


                Response.Redirect("grade-quiz-detail.aspx?plan=" + planid + "&year=" + year + "&term=" + term + "&idlv=" + idlv + "&idlv2=" + idlv2 + "&test=" + test);
            }
        }


        void btnCancle_Click(object sender, EventArgs e)
        {
            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];
            string test = Request.QueryString["test"];
            string planid = Request.QueryString["plan"];


            Response.Redirect("grade-quiz-detail.aspx?plan=" + planid + "&year=" + year + "&term=" + term + "&idlv=" + idlv + "&idlv2=" + idlv2 + "&test=" + test);
        }

        private void OpenData()
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {


                //var _data = _db.TPlanes.Where(w => w.cDel == null).Select(s => new { s.sPlaneName, s.sPlaneID });
                string SQL = "";
                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                string DropDownList1 = Request.QueryString["year"];
                string DropDownList2 = Request.QueryString["term"];
                string sname = Request.QueryString["sname"];
                string planid = Request.QueryString["plan"];


                SQL = @"select TB1.*,SubLevel,TB2.nTSubLevel2
                    from TUser AS TB1 left join TTermSubLevel2 AS TB2 on TB1.nTermSubLevel2 = TB2.nTermSubLevel2
                    left join TSubLevel AS TB3 on TB3.nTSubLevel = TB2.nTSubLevel
                    where cDel IS NULL ";

                if (!string.IsNullOrEmpty(idlv2)) SQL += " AND TB1.nTermSubLevel2 = " + idlv2;
                else if (!string.IsNullOrEmpty(idlv)) SQL += " AND TB2.nTSubLevel = " + idlv;
                if (!string.IsNullOrEmpty(DropDownList1)) SQL += " AND sName + ' ' + sLastname LIKE '%" + sname + "%'";

                string nterm = "";
                int nyear = 0;
                string yyy = Request.QueryString["year"];
                int? useryear = Int32.Parse(yyy);
                string userterm = Request.QueryString["term"];
                string test = Request.QueryString["test"];
                int testnumber = Int32.Parse(test);
                int idlv2int = Int32.Parse(idlv2);

                var plandetail = _db.TPlanes.Where(w => w.sPlaneID.ToString() == planid).FirstOrDefault();
                plan.Text = plandetail.sPlaneID + " " + plandetail.sPlaneName;

                foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear))
                {
                    nyear = ff.nYear;
                }
                foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.cDel == null))
                {
                    nterm = ee.nTerm;
                }
                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());

                var room = fcommon.Get_Data(_conn, SQL);
                quizDetail quizdetail = new quizDetail();
                List<quizDetail> quizdetaillist = new List<quizDetail>();
                int end = 0;

                int countnum = 1;
                foreach (var ee in _db.TQuizs.Where(w => w.sPlanid == planid && w.sTerm == nterm))
                {
                    countnum = countnum + 1;
                }

                int number = 1;

                var detail = _db.TQuizs.Where(w => w.sPlanid == planid && w.sTerm == nterm && w.nNumber == testnumber).FirstOrDefault();

                string quizdate = detail.QuizDay.ToString();

                string letDate = quizdate.Substring(0, quizdate.Length - 11); //Format – dd/MM/yyyy                                                                          
                string[] arrDate3 = letDate.Split('/');

                //now use array to get specific date object
                string cutday3 = arrDate3[1].ToString();
                string cutmonth3 = arrDate3[0].ToString();
                string cutyear3 = arrDate3[2].ToString();

                int quizYear = Int32.Parse(cutyear3);
                if (quizYear < 2550)
                    quizYear = quizYear + 543;
                int quizmonth = Int32.Parse(cutmonth3);
                int quizday = Int32.Parse(cutday3);

                string ltmonth = quizmonth.ToString();

                if (ltmonth == "1")
                    ltmonth = "มกราคม";
                else if (ltmonth == "2")
                    ltmonth = "กุมภาพันธ์";
                else if (ltmonth == "3")
                    ltmonth = "มีนาคม";
                else if (ltmonth == "4")
                    ltmonth = "เมษายน";
                else if (ltmonth == "5")
                    ltmonth = "พฤษภาคม";
                else if (ltmonth == "6")
                    ltmonth = "มิถุนายน";
                else if (ltmonth == "7")
                    ltmonth = "กรกฎาคม";
                else if (ltmonth == "8")
                    ltmonth = "สิงหาคม";
                else if (ltmonth == "9")
                    ltmonth = "กันยายน";
                else if (ltmonth == "10")
                    ltmonth = "ตุลาคม";
                else if (ltmonth == "11")
                    ltmonth = "พฤศจิกายน";
                else if (ltmonth == "12")
                    ltmonth = "ธันวาคม";


                for (int i = 1; i < 10; i++)
                {
                    int year = DateTime.Now.AddYears(-7 + i).Year;
                    if (year < 2550)
                        year = year + 543;
                    int value = year - 543;

                    var item = new ListItem
                    {
                        Text = year.ToString(),
                        Value = value.ToString()
                    };
                    ddlAge.Items.Add(item);
                }

                foreach (var data in _db.TQuizs.Where(w => w.sPlanid == planid && w.sTerm == nterm && w.nNumber == testnumber))
                {
                    ddlAge.SelectedValue = data.QuizDay.Value.Year.ToString();
                    ddlMonth.SelectedValue = string.Format("{0:00}", data.QuizDay.Value.Month);
                    ddlDay.SelectedValue = string.Format("{0:00}", data.QuizDay.Value.Day);
                    var student = _db.TUser.Where(w => w.nTermSubLevel2 == idlv2int && w.sIdentification == data.sID).FirstOrDefault();

                    if (student != null)
                    {
                        maxscore.Text = data.dfullScore.ToString();
                        quizdetail = new quizDetail();
                        quizdetail.note = data.sNote;
                        quizdetail.rescore = data.dReScore;
                        quizdetail.score = data.dscore;
                        quizdetail.testNumber = data.nNumber;
                        quizdetail.sName = student.sName;
                        quizdetail.sLastName = student.sLastname;
                        quizdetail.number = number;
                        number = number + 1;
                        quizdetail.sIdentification = data.sID;
                        quizdetaillist.Add(quizdetail);
                    }
                }
                int counttest = 1;

                foreach (var data2 in _db.TQuizs.Where(w => w.sPlanid == planid && w.sTerm == nterm))
                {
                    var student = _db.TUser.Where(w => w.nTermSubLevel2 == idlv2int && w.sIdentification == data2.sID).FirstOrDefault();

                    if (student != null && counttest < data2.nNumber)
                    {
                        counttest = data2.nNumber;
                    }
                }



                foreach (DataRow row in room.Rows)
                {
                    classroom.Text = row["SubLevel"].ToString() + " / " + row["nTSubLevel2"].ToString();
                }

                dgd.DataSource = quizdetaillist;
                dgd.PageSize = 500;
                dgd.DataBind();
            }

            //protected class quizDetail
            //{

            //    public string sName { get; set; }
            //    public string sLastName { get; set; }
            //    public string sIdentification { get; set; }
            //    public int number { get; set; }
            //    public int testNumber { get; set; }
            //    public decimal? score { get; set; }
            //    public decimal? rescore { get; set; }
            //    public string note { get; set; }
            //}
        }

    }
}