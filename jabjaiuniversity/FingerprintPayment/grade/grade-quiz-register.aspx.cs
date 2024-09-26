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
    public partial class grade_quiz_register : System.Web.UI.Page
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

                string birthDate = DropDownList1.SelectedValue;
                string birthMonth = DropDownList2.SelectedValue;
                string birthYear = ddlAge.SelectedValue;
                string combinedate = birthDate + "/" + birthMonth + "/" + birthYear;
                DateTime dt1 = DateTime.ParseExact(combinedate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                List<TQuiz> quizlist = new List<TQuiz>();
                TQuiz quiz = new TQuiz();

                int sEmpID = int.Parse(Session["sEmpID"] + "");

                int countnum = 1;
                foreach (var ee in _db.TQuizs.Where(w => w.sPlanid == planid && w.sTerm == nterm))
                {
                    if (countnum == ee.nNumber)
                        countnum = countnum + 1;
                }

                foreach (DataGridItem dgItem in dgd.Items)
                {
                    TextBox txtScore = (TextBox)dgItem.FindControl("txtScore");
                    string score = txtScore.Text;
                    TextBox txtReScore = (TextBox)dgItem.FindControl("txtReScore");
                    string rescore = txtReScore.Text;
                    TextBox txtNote = (TextBox)dgItem.FindControl("txtNote");
                    string note = txtNote.Text;

                    quiz = new TQuiz();
                    quiz.QuizDay = dt1;
                    quiz.dDatetimeCreate = DateTime.Now;
                    if (rescore != "" && rescore != null)
                        quiz.dReScore = Int32.Parse(rescore);
                    else quiz.dReScore = null;

                    if (maxscore.Text != "" && maxscore.Text != null)
                        quiz.dfullScore = Int32.Parse(maxscore.Text);
                    else quiz.dfullScore = null;

                    if (score != "" && score != null)
                        quiz.dscore = Int32.Parse(score);
                    else quiz.dscore = null;

                    string test = Request.QueryString["test"];
                    int testint = Int32.Parse(test);
                    testint = testint + 1;

                    quiz.sEmp = sEmpID;
                    quiz.sNote = note;
                    quiz.sTerm = nterm;
                    quiz.sPlanid = planid;
                    quiz.nNumber = testint;
                    quiz.dDatetimeUpdate = null;
                    quiz.sID = dgd.Items[dgItem.DataSetIndex].Cells[3].Text;

                    _db.TQuizs.Add(quiz);

                }

                _db.SaveChanges();

                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                string year = Request.QueryString["year"];
                string term = Request.QueryString["term"];

                Response.Redirect("grade-quiz.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term);
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

        private void OpenData()
        {
           
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
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


                for (int i = 1; i < 5; i++)
                {
                    int year = DateTime.Now.AddYears(-3 + i).Year;
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

                string yyy = Request.QueryString["year"];
                string userterm = Request.QueryString["term"];

                int? useryear = Int32.Parse(yyy);
                //string username = "";
                //int? ntermsublv2 = 0;
                //int nyear = 0;
                //string nterm = "";
                //int ntermtable = 0;

                var kk = _db.TPlanes.Where(w => w.sPlaneID.ToString() == planid).FirstOrDefault();
                plan.Text = kk.sPlaneID + " " + kk.sPlaneName;


                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());

                var x = fcommon.Get_Data(_conn, SQL);
                StudentScore xa = new StudentScore();
                List<StudentScore> list = new List<StudentScore>();
                //int end = 0;

                string test = Request.QueryString["test"];
                int testint = Int32.Parse(test);
                testint = testint + 1;

                testNumber.Text = testint.ToString();

                int number = 1;
                int xxx = 0;

                foreach (DataRow row in x.Rows)
                {
                    xa = new StudentScore();
                    xa.year = DropDownList1;
                    xa.term = DropDownList2;
                    xa.number = number;
                    xa.sName = row["sName"].ToString();
                    xa.sLastName = row["sLastName"].ToString();
                    xa.sIdentification = row["sIdentification"].ToString();
                    Int32.TryParse(xa.sIdentification, out xxx);

                    classroom.Text = row["SubLevel"].ToString() + " / " + row["nTSubLevel2"].ToString();
                    list.Add(xa);
                    number = number + 1;
                }


                dgd.DataSource = list;
                dgd.PageSize = 500;
                dgd.DataBind();
            }

            //protected class StudentScore
            //{

            //    public string year { get; set; }
            //    public string term { get; set; }
            //    public string sName { get; set; }
            //    public string sLastName { get; set; }
            //    public string sIdentification { get; set; }
            //    public string SubLevel { get; set; }
            //    public string nTSubLevel2 { get; set; }
            //    public int endScore { get; set; }
            //    public int number { get; set; }
            //}
        }


    }
}