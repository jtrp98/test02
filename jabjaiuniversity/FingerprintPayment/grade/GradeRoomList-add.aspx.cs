using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using FingerprintPayment.ViewModels;

namespace FingerprintPayment.grade
{
    public partial class GradeRoomList_add : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
            {

                btnSave.Click += new EventHandler(btnSave_Click);
                btnCancle.Click += new EventHandler(btnCancle_Click);
                string id = Request.QueryString["id"];
                var data = _db.TPlanes.Where(w => w.sPlaneID.ToString() == id).FirstOrDefault();
                planid.Text = data.sPlaneID.ToString();
                planname.Text = data.sPlaneName;
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
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
            {
                string nterm = "";
                int nyear = 0;
                string id = Request.QueryString["id"];
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

                foreach (DataGridItem dgItem in dgd.Items)
                {

                    TextBox txtSID = (TextBox)dgItem.FindControl("sID");
                    string sidtxt = dgd.Items[dgItem.DataSetIndex].Cells[1].Text;
                    int? sID = Int32.Parse(sidtxt);
                    var check2 = _db.TSchoolRecords.Where(w => w.nTerm == nterm && w.nTsudentId == sID).FirstOrDefault();
                    int sPlaneId = 0;
                    int.TryParse(id, out sPlaneId);
                    if (check2 == null)
                    {
                        record = new TSchoolRecord();
                        record.UserAdd = sEmpID;
                        record.UserUpdate = sEmpID;
                        record.dAdd = DateTime.Now;
                        record.dUpdate = DateTime.Now;
                        record.nSchoolRecordId = countRecord;
                        record.nTerm = nterm;
                        record.nTsudentId = sID;
                        _db.TSchoolRecords.Add(record);

                        TextBox txtGrade = (TextBox)dgItem.FindControl("txtGrade");
                        string str1 = txtGrade.Text;
                        TextBox txtScore = (TextBox)dgItem.FindControl("txtScore");
                        string str2 = txtScore.Text;
                        TextBox txtReScore = (TextBox)dgItem.FindControl("txtReScore");
                        string str3 = txtReScore.Text;
                        TextBox txtNote = (TextBox)dgItem.FindControl("txtNote");
                        string str4 = txtNote.Text;

                        detail = new TSchoolRecord_Detail();
                        detail.MaxScore = maxScore.Text;
                        detail.Grade = str1;
                        detail.Note = str4;
                        detail.ReGrade = str3;
                        detail.Score = str2;
                        detail.sPlaneID = sPlaneId;
                        detail.nSchoolRecordId = countRecord;
                        _db.TSchoolRecord_Detail.Add(detail);
                    }
                    else
                    {
                        check2.UserAdd = sEmpID;
                        check2.UserUpdate = sEmpID;
                        check2.dAdd = DateTime.Now;
                        check2.dUpdate = DateTime.Now;

                        var check3 = _db.TSchoolRecord_Detail.Where(w => w.nSchoolRecordId == check2.nSchoolRecordId && w.sPlaneID == sPlaneId).FirstOrDefault();
                        if (check3 == null)
                        {
                            TextBox txtGrade = (TextBox)dgItem.FindControl("txtGrade");
                            string str1 = txtGrade.Text;
                            TextBox txtScore = (TextBox)dgItem.FindControl("txtScore");
                            string str2 = txtScore.Text;
                            TextBox txtReScore = (TextBox)dgItem.FindControl("txtReScore");
                            string str3 = txtReScore.Text;
                            TextBox txtNote = (TextBox)dgItem.FindControl("txtNote");
                            string str4 = txtNote.Text;

                            detail = new TSchoolRecord_Detail();
                            detail.MaxScore = maxScore.Text;
                            detail.Grade = str1;
                            detail.Note = str4;
                            detail.ReGrade = str3;
                            detail.Score = str2;
                            detail.sPlaneID = sPlaneId;
                            detail.nSchoolRecordId = countRecord;
                            _db.TSchoolRecord_Detail.Add(detail);

                        }
                        else
                        {
                            TextBox txtGrade = (TextBox)dgItem.FindControl("txtGrade");
                            string str1 = txtGrade.Text;
                            TextBox txtScore = (TextBox)dgItem.FindControl("txtScore");
                            string str2 = txtScore.Text;
                            TextBox txtReScore = (TextBox)dgItem.FindControl("txtReScore");
                            string str3 = txtReScore.Text;
                            TextBox txtNote = (TextBox)dgItem.FindControl("txtNote");
                            string str4 = txtNote.Text;
                            string planid = id;
                            var data3 = _db.TSchoolRecord_Detail.Where(w => w.nSchoolRecordId == check3.nSchoolRecordId && w.sPlaneID == sPlaneId).FirstOrDefault();
                            data3.Grade = str1;
                            data3.Note = str4;
                            data3.ReGrade = str3;
                            data3.Score = str2;
                            data3.MaxScore = maxScore.Text;
                        }

                    }


                    countRecord = countRecord + 1;
                }


                _db.SaveChanges();

                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                string year = Request.QueryString["year"];
                string term = Request.QueryString["term"];

                Response.Redirect("GradeRoomList.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term);
            }
        }


        void btnCancle_Click(object sender, EventArgs e)
        {

            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];

            Response.Redirect("GradeRoomList.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + year + "&term=" + term);
        }

        private void OpenData()
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
            {
                JabJaiMasterEntities dbmaster = Connection.MasterEntities();

                string sEntities = Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                string id = Request.QueryString["id"];
                string year = Request.QueryString["year"];
                string userterm = Request.QueryString["term"];
                string idlv2 = Request.QueryString["idlv2"];

                int? useryear = Int32.Parse(year);
                int? idlv2n = Int32.Parse(idlv2);
                int nyear = 0;
                string nterm = "";

                foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear))
                {
                    nyear = ff.nYear;
                }

                foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.cDel == null))
                {
                    nterm = ee.nTerm;
                }
                int sPlaneId = 0;
                int.TryParse(id, out sPlaneId);
                int number = 1;
                studentlist student = new studentlist();
                List<studentlist> studentlist = new List<studentlist>();
                foreach (var data in _db.TUser.Where(w => w.nTermSubLevel2 == idlv2n))
                {

                    student = new studentlist();
                    student.number = number;
                    student.sID = data.sID;
                    student.sName = data.sName + " " + data.sLastname;


                    var data2 = _db.TSchoolRecords.Where(w => w.nTsudentId == data.sID && w.nTerm == nterm).FirstOrDefault();
                    if (data2 != null)
                    {
                        var data3 = _db.TSchoolRecord_Detail.Where(w => w.nSchoolRecordId == data2.nSchoolRecordId && w.sPlaneID.ToString() == id).FirstOrDefault();
                        student.grade = data3.Grade;
                        student.note = data3.Note;
                        student.reGrade = data3.ReGrade;
                        student.score = data3.Score;
                    }

                    var TUserMaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0" && w.nSystemID == data.sID).FirstOrDefault();
                    if (TUserMaster != null)
                    {
                        number = number + 1;
                        studentlist.Add(student);
                    }
                }

                dgd.DataSource = studentlist;
                dgd.PageSize = 999;
                dgd.DataBind();
            }
        }

        //class studentlist
        //{
        //    public int sID { get; set; }
        //    public string sName { get; set; }
        //    public string note { get; set; }
        //    public string reGrade { get; set; }
        //    public string score { get; set; }
        //    public string grade { get; set; }
        //    public int number { get; set; }
        //}
    }
}