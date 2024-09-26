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
    public partial class grade_result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["permission"] == null) Response.Redirect("/Default.aspx");
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

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

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string zzz = Request.QueryString["id"];
                string yyy = Request.QueryString["year"];
                string userterm = Request.QueryString["term"];

                int userid = Int32.Parse(zzz);
                int? useryear = Int32.Parse(yyy);

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

                List<planresult> planresultlist = new List<planresult>();
                planresult planresult = new planresult();
                var data1 = _db.TSchoolRecords.Where(w => w.nTsudentId == userid && w.nTerm == nterm).FirstOrDefault();
                if (data1 != null)
                {
                    foreach (var data2 in _db.TSchoolRecord_Detail.Where(w => w.nSchoolRecordId == data1.nSchoolRecordId).ToList())
                    {
                        var kk = _db.TPlanes.Where(w => w.sPlaneID == data2.sPlaneID).FirstOrDefault();
                        planresult = new planresult
                        {
                            sPlanName = kk.sPlaneName,
                            sPlanID = data2.sPlaneID.ToString(),
                            grade = data2.Grade,
                            note = data2.Note,
                            reGrade = data2.ReGrade,
                            score = data2.Score
                        };
                        planresultlist.Add(planresult);
                    }
                }

                dgd.DataSource = planresultlist;
                dgd.DataBind();
            }
            //class planresult
            //{            
            //    public string sPlanName { get; set; }
            //    public string sPlanID { get; set; }
            //    public string note { get; set; }
            //    public string reGrade { get; set; }
            //    public string score { get; set; }
            //    public string grade { get; set; }
            //}

        }

    }

}