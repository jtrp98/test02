using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class grade_register : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                btnSave.Click += new EventHandler(btnSave_Click);
                btnCancle.Click += new EventHandler(btnCancle_Click);
                int eee = 0;
                Int32.TryParse(Request.QueryString["id"], out eee);
                var data2 = _db.TUser.Where(w => w.sID == eee).FirstOrDefault();
                studentName2.Text = data2.sName + " " + data2.sLastname;
                studentId2.Text = data2.sIdentification;
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
                string zzz = Request.QueryString["id"];
                int userid = Int32.Parse(zzz);
                string yyy = Request.QueryString["year"];
                int? useryear = Int32.Parse(yyy);
                string userterm = Request.QueryString["term"];


                foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear))
                {
                    nyear = ff.nYear;
                }
                foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.cDel == null))
                {
                    nterm = ee.nTerm;
                }

                int countRecord = 1;

                foreach (var xxxxx in _db.TSchoolRecords.Where(w => w.nSchoolRecordId > 0))
                {
                    countRecord = countRecord + 1;
                }

                List<TSchoolRecord> recordlist = new List<TSchoolRecord>();
                TSchoolRecord record = new TSchoolRecord();
                List<TSchoolRecord_Detail> detaillist = new List<TSchoolRecord_Detail>();
                TSchoolRecord_Detail detail = new TSchoolRecord_Detail();
                TSchoolRecord b = new TSchoolRecord();

                int sEmpID = int.Parse(Session["sEmpID"] + "");
                var check = _db.TSchoolRecords.Where(w => w.nTerm == nterm && w.nTsudentId == userid).FirstOrDefault();
                if (check == null)
                {
                    record = new TSchoolRecord();
                    record.UserAdd = sEmpID;
                    record.UserUpdate = sEmpID;
                    record.dAdd = DateTime.Now;
                    record.dUpdate = DateTime.Now;
                    record.nSchoolRecordId = countRecord;
                    record.nTerm = nterm;
                    record.nTsudentId = userid;
                    _db.TSchoolRecords.Add(record);


                    foreach (DataGridItem dgItem in dgd.Items)
                    {

                        TextBox txtGrade = (TextBox)dgItem.FindControl("txtGrade");
                        string str1 = txtGrade.Text;
                        TextBox txtScore = (TextBox)dgItem.FindControl("txtScore");
                        string str2 = txtScore.Text;
                        TextBox txtReScore = (TextBox)dgItem.FindControl("txtReScore");
                        string str3 = txtReScore.Text;
                        TextBox txtNote = (TextBox)dgItem.FindControl("txtNote");
                        string str4 = txtNote.Text;
                        int planeid = 0;
                        int.TryParse(dgd.Items[dgItem.DataSetIndex].Cells[0].Text, out planeid);
                        detail = new TSchoolRecord_Detail();
                        detail.Grade = str1;
                        detail.Note = str4;
                        detail.ReGrade = str3;
                        detail.Score = str2;
                        detail.sPlaneID = planeid;
                        detail.nSchoolRecordId = countRecord;
                        _db.TSchoolRecord_Detail.Add(detail);

                    }
                }
                else
                {
                    check.UserAdd = sEmpID;
                    check.UserUpdate = sEmpID;
                    check.dAdd = DateTime.Now;
                    check.dUpdate = DateTime.Now;

                    foreach (DataGridItem dgItem in dgd.Items)
                    {
                        TextBox txtGrade = (TextBox)dgItem.FindControl("txtGrade");
                        string str1 = txtGrade.Text;
                        TextBox txtScore = (TextBox)dgItem.FindControl("txtScore");
                        string str2 = txtScore.Text;
                        TextBox txtReScore = (TextBox)dgItem.FindControl("txtReScore");
                        string str3 = txtReScore.Text;
                        TextBox txtNote = (TextBox)dgItem.FindControl("txtNote");
                        string str4 = txtNote.Text;
                        string planid = dgd.Items[dgItem.DataSetIndex].Cells[0].Text;
                        var data3 = _db.TSchoolRecord_Detail.Where(w => w.nSchoolRecordId == check.nSchoolRecordId && w.sPlaneID.ToString() == planid).FirstOrDefault();
                        if (data3 != null)
                        {
                            data3.Grade = str1;
                            data3.Note = str4;
                            data3.ReGrade = str3;
                            data3.Score = str2;
                        }
                        else
                        {
                            int sPlaneId = 0;
                            int.TryParse(dgd.Items[dgItem.DataSetIndex].Cells[0].Text, out sPlaneId);
                            detail = new TSchoolRecord_Detail();
                            detail.Grade = str1;
                            detail.Note = str4;
                            detail.ReGrade = str3;
                            detail.Score = str2;
                            detail.sPlaneID = sPlaneId;
                            detail.nSchoolRecordId = countRecord;
                            _db.TSchoolRecord_Detail.Add(detail);
                        }

                    }
                }

                _db.SaveChanges();

                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                string year = Request.QueryString["year"];
                string term = Request.QueryString["term"];

                Response.Redirect("grade-classroom.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term);
            }
        }



        void btnCancle_Click(object sender, EventArgs e)
        {

            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];

            Response.Redirect("grade-classroom.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term);
        }

        private void OpenData()
        {

            //int number = 1;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                //var _data = _db.TPlanes.Where(w => w.cDel == null).Select(s => new { s.sPlaneName, s.sPlaneID });

                string zzz = Request.QueryString["id"];
                string yyy = Request.QueryString["year"];
                string userterm = Request.QueryString["term"];

                int userid = Int32.Parse(zzz);
                int? useryear = Int32.Parse(yyy);
                string username = "";
                int? ntermsublv2 = 0;
                int nyear = 0;
                string nterm = "";
                int ntermtable = 0;
                List<string> planIdlist = new List<string>();


                foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear))
                {
                    nyear = ff.nYear;
                }

                foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.cDel == null))
                {
                    nterm = ee.nTerm;
                }

                foreach (var ff in _db.TUser.Where(w => w.sID == userid))
                {
                    username = ff.sName + " " + ff.sLastname;
                    ntermsublv2 = ff.nTermSubLevel2;
                }


                foreach (var dd in _db.TTermTimeTables.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == ntermsublv2))
                {
                    ntermtable = dd.nTermTable;
                }

                foreach (var hh in _db.TSchedules.Where(w => w.nTermTable == ntermtable))
                {
                    string planId = "";
                    if (hh.sPlaneID != null)
                    {
                        planId = hh.sPlaneID.ToString();
                        planIdlist.Add(planId);
                    }
                }

                List<string> unique = planIdlist.Distinct().ToList();

                planresult totalplan = new planresult();
                List<planresult> totalplanlist = new List<planresult>();
                foreach (var ii in unique)
                {
                    foreach (var kk in _db.TPlanes.Where(w => w.sPlaneID.ToString() == ii))
                    {
                        totalplan = new planresult();
                        totalplan.sPlanName = kk.sPlaneName;
                        totalplan.sPlanID = kk.sPlaneID.ToString();

                        var data1 = _db.TSchoolRecords.Where(w => w.nTsudentId == userid && w.nTerm == nterm).FirstOrDefault();
                        if (data1 != null)
                        {
                            var data2 = _db.TSchoolRecord_Detail.Where(w => w.nSchoolRecordId == data1.nSchoolRecordId && w.sPlaneID.ToString() == ii).FirstOrDefault();
                            if (data2 != null)
                            {
                                totalplan.grade = data2.Grade;
                                totalplan.note = data2.Note;
                                totalplan.reGrade = data2.ReGrade;
                                totalplan.score = data2.Score;
                                totalplan.maxScore = data2.MaxScore;
                            }
                        }
                        totalplanlist.Add(totalplan);
                    }
                }

                dgd.DataSource = totalplanlist;
                dgd.DataBind();
            }
        }

        //class planresult
        //{
        //    public string sPlanName { get; set; }
        //    public string sPlanID { get; set; }
        //    public string note { get; set; }
        //    public string reGrade { get; set; }
        //    public string score { get; set; }
        //    public string grade { get; set; }
        //    public string maxScore { get; set; }
        //}

    }
}