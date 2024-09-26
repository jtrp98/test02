using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class grade_quiz_detail : System.Web.UI.Page
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

                Button1.Click += new EventHandler(Button1_Click);
                btnCancle.Click += new EventHandler(btnCancle_Click);
                btnedit.Click += new EventHandler(btnEdit_Click);

                Year.Text = Request.QueryString["year"];
                Term.Text = Request.QueryString["term"];


                if (!IsPostBack)
                {

                    OpenData();
                }
            }
        }

        void btnCancle_Click(object sender, EventArgs e)
        {
            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];


            Response.Redirect("grade-quiz.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term);
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            string ddlnumber = ddlTestNumber.SelectedValue;
            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];
            string planid = Request.QueryString["plan"];
            Response.Redirect("grade-quiz-edit.aspx?plan=" + planid + "&year=" + year + "&term=" + term + "&idlv=" + idlv + "&idlv2=" + idlv2 + "&test=" + ddlnumber);
        }

        void Button1_Click(object sender, EventArgs e)
        {

            string ddlnumber = ddlTestNumber.SelectedValue;
            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];
            string planid = Request.QueryString["plan"];
            Response.Redirect("grade-quiz-detail.aspx?plan=" + planid + "&year=" + year + "&term=" + term + "&idlv=" + idlv + "&idlv2=" + idlv2 + "&test=" + ddlnumber);
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


                maxScore.Text = detail.dfullScore.ToString();
                examDate.Text = quizday + " " + ltmonth + " " + quizYear;

                foreach (var data in _db.TQuizs.Where(w => w.sPlanid == planid && w.sTerm == nterm && w.nNumber == testnumber))
                {


                    var student = _db.TUser.Where(w => w.nTermSubLevel2 == idlv2int && w.sIdentification == data.sID).FirstOrDefault();

                    if (student != null)
                    {
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

                for (int i = 1; i <= counttest; i++)
                {

                    var item = new ListItem
                    {
                        Text = i.ToString(),
                        Value = i.ToString()
                    };
                    ddlTestNumber.Items.Add(item);
                }

                ddlTestNumber.SelectedIndex = testnumber - 1;

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