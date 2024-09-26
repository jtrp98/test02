using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment.grade
{
    public partial class GradeCheckConfig : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                
                btnSave.Click += new EventHandler(btnSave_Click);

                string delete = Request.QueryString["del"];

                if (delete != null && delete != "")
                {
                    int ndel = int.Parse(delete);
                    TGradeCheck check = new TGradeCheck();
                    List<TGradeCheck> checkList = new List<TGradeCheck>();

                    var data = _db.TGradeChecks.Where(w => w.nGradeCheckId == ndel).FirstOrDefault();
                    if (data != null)
                    {
                        data.Deleted = "1";

                    }

                    _db.SaveChanges();
                }

                if (!IsPostBack)
                {
                    OpenData();

                    var item2 = new ListItem
                    {
                        Text = "ทุกวิชา",
                        Value = "-1"
                    };
                    modalcourse.Items.Add(item2);
                    courseddl.Items.Add(item2);

                    foreach (var a in _db.TPlanes.Where(w => w.cDel != "1"))
                    {
                        var item = new ListItem
                        {
                            Text = a.courseCode + " " + a.sPlaneName,
                            Value = a.sPlaneID.ToString()
                        };
                        modalcourse.Items.Add(item);
                        courseddl.Items.Add(item);
                    }

                    var emp = _db.TEmployees.Where(w => w.cDel == null).ToList();
                    var newList = emp.OrderBy(x => x.sName).ToList();

                    foreach (var t in newList)
                    {
                        var item3 = new ListItem
                        {
                            Text = t.sName + " " + t.sLastname,
                            Value = t.sEmp.ToString()
                        };
                        modalTeacher.Items.Add(item3);
                    }

                }
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                TGradeCheck check = new TGradeCheck();
                List<TGradeCheck> checkList = new List<TGradeCheck>();

                int? ddlteacher = int.Parse(modalTeacher.SelectedValue);
                int? ddlplan = int.Parse(modalcourse.SelectedValue);

                int count = 1;
                foreach (var c in _db.TGradeChecks)
                {
                    if (c.nGradeCheckId >= count)
                        count = c.nGradeCheckId + 1;
                }
                var checknull = _db.TGradeChecks.Where(w => w.teacherId == ddlteacher && w.PlanId == ddlplan).FirstOrDefault();
                if (checknull == null)
                {
                    check = new TGradeCheck();
                    check.nGradeCheckId = count;
                    check.PlanId = ddlplan;
                    check.teacherId = ddlteacher;
                    check.Deleted = null;

                    _db.TGradeChecks.Add(check);
                }
                else if (checknull != null && checknull.Deleted == "1")
                {
                    checknull.Deleted = null;
                }


                _db.SaveChanges();

                Response.Redirect("GradeCheckConfig.aspx");
            }
        }

        private void OpenData()
        {

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                

                List<gradecheck> gradecheckList = new List<gradecheck>();
                gradecheck gradecheck = new gradecheck();
                string id = Request.QueryString["id"];

                int number = 1;
                if (id == null || id == "" || id == "-1")
                {
                    foreach (var data in _db.TGradeChecks.Where(w => w.Deleted == null))
                    {
                        gradecheck = new gradecheck();
                        gradecheck.courseId = data.PlanId.ToString();

                        var plan = _db.TPlanes.Where(w => w.sPlaneID == data.PlanId).FirstOrDefault();
                        if (data.PlanId == -1)
                            gradecheck.courseName = "ทุกวิชา";
                        else if (plan != null)
                            gradecheck.courseName = plan.courseCode + " " + plan.sPlaneName;
                        else gradecheck.courseName = "";

                        var teacher = _db.TEmployees.Where(w => w.sEmp == data.teacherId).FirstOrDefault();
                        if (teacher != null)
                            gradecheck.name = teacher.sName + " " + teacher.sLastname;
                        else gradecheck.name = "";

                        gradecheck.number = number;
                        gradecheck.gradecheckId = data.nGradeCheckId;
                        number = number + 1;
                        gradecheckList.Add(gradecheck);
                    }
                }
                else
                {
                    int idn = int.Parse(id);
                    foreach (var data in _db.TGradeChecks.Where(w => w.Deleted == null && w.PlanId == idn))
                    {
                        gradecheck = new gradecheck();
                        gradecheck.courseId = data.PlanId.ToString();

                        var plan = _db.TPlanes.Where(w => w.sPlaneID == data.PlanId).FirstOrDefault();
                        if (data.PlanId == -1)
                            gradecheck.courseName = "ทุกวิชา";
                        else if (plan != null)
                            gradecheck.courseName = plan.courseCode + " " + plan.sPlaneName;
                        else gradecheck.courseName = "";

                        var teacher = _db.TEmployees.Where(w => w.sEmp == data.teacherId).FirstOrDefault();
                        if (teacher != null)
                            gradecheck.name = teacher.sName + " " + teacher.sLastname;
                        else gradecheck.name = "";

                        gradecheck.number = number;
                        gradecheck.gradecheckId = data.nGradeCheckId;
                        number = number + 1;
                        gradecheckList.Add(gradecheck);
                    }
                }

                dgd.DataSource = gradecheckList;
                dgd.PageSize = 999;
                dgd.DataBind();
            }
            //protected class gradecheck
            //{
            //    public int number { get; set; }
            //    public string name { get; set; }
            //    public string courseName { get; set; }
            //    public string courseId { get; set; }
            //    public int gradecheckId { get; set; }
            //}
        }
    }
}

