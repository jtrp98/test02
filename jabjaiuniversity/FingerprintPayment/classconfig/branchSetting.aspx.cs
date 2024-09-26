using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;
using System.Globalization;
using System.Web.DynamicData;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace FingerprintPayment.classconfig
{
    public partial class branchSetting : System.Web.UI.Page
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

            editSave.Click += new EventHandler(editSave_Click);
            editSave2.Click += new EventHandler(editSave_Click2);
            editSave3.Click += new EventHandler(editSave_Click3);
            deleteBtn.Click += new EventHandler(delete_Click);
            deleteBtn2.Click += new EventHandler(delete_Click2);
            deleteBtn3.Click += new EventHandler(delete_Click3);
            Button1.Click += new EventHandler(register);
            Button2.Click += new EventHandler(register2);
            Button3.Click += new EventHandler(register3);

            if (!IsPostBack)
            {
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    var q_level = _db.TLevels.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false && (w.LevelName == "ปวช." || w.LevelName == "ปวส.")).ToList();
                    ddlRegisterType.DataSource = q_level;
                    ddlRegisterType.DataTextField = "LevelName";
                    ddlRegisterType.DataValueField = "LevelID";
                    ddlRegisterType.DataBind();

                    Opendata();
                }
            }
        }


        void editSave_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                int id = int.Parse(editid.Text);
                var a = _db.TBranches.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.BranchId == id).FirstOrDefault();

                a.nTLevel = int.Parse(editType.SelectedValue);
                a.BranchName = editName.Text;
                a.SchoolID = userData.CompanyID;
                _db.SaveChanges();

                Response.Redirect("branchSetting.aspx");
            }
        }

        void editSave_Click2(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                int id = int.Parse(edit2id.Text);
                var a = _db.TBranchSubjects.Where(w => w.SchoolID == userData.CompanyID && w.BranchSubjectId == id).FirstOrDefault();

                a.BranchSubjectName = edit2Name.Text;
                a.SchoolID = userData.CompanyID;
                _db.SaveChanges();

                Response.Redirect("branchSetting.aspx");
            }
        }

        void editSave_Click3(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                int id = int.Parse(edit3id.Text);
                var a = _db.TBranchSpecs.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.BranchSpecId == id).FirstOrDefault();

                a.BranchSpecName = edit3name.Text;
                a.SchoolID = userData.CompanyID;
                _db.SaveChanges();

                Response.Redirect("branchSetting.aspx");
            }
        }

        void delete_Click(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                int id = int.Parse(deleteid.Text);
                var a = _db.TBranches.Where(w => w.BranchId == id).FirstOrDefault();
                foreach (var b in _db.TBranchSubjects.Where(w => w.SchoolID == userData.CompanyID && w.BranchId == a.BranchId))
                {
                    foreach (var c in _db.TBranchSpecs.Where(w => w.SchoolID == userData.CompanyID && w.BranchSubjectId == b.BranchSubjectId))
                    {
                        c.cDel = 1;
                        c.UpdatedBy = userData.UserID;
                        c.UpdatedDate = DateTime.Now;
                    }
                    b.cDel = 1;
                    b.UpdatedBy = userData.UserID;
                    b.UpdatedDate = DateTime.Now;
                }
                a.cDel = 1;
                a.UpdatedBy = userData.UserID;
                a.UpdatedDate = DateTime.Now;

                _db.SaveChanges();

                Response.Redirect("branchSetting.aspx");
            }
        }

        void delete_Click2(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                int id = int.Parse(delete2id.Text);
                var a = _db.TBranchSubjects.Where(w => w.SchoolID == userData.CompanyID && w.BranchSubjectId == id).FirstOrDefault();
                foreach (var b in _db.TBranchSpecs.Where(w => w.SchoolID == userData.CompanyID && w.BranchSubjectId == a.BranchSubjectId))
                {
                    b.cDel = 1;
                    b.UpdatedBy = userData.UserID;
                    b.UpdatedDate = DateTime.Now;
                }
                a.cDel = 1;
                a.UpdatedBy = userData.UserID;
                a.UpdatedDate = DateTime.Now;

                _db.SaveChanges();

                Response.Redirect("branchSetting.aspx");
            }
        }

        void delete_Click3(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                int id = int.Parse(delete3id.Text);
                var a = _db.TBranchSpecs.Where(w => w.SchoolID == userData.CompanyID && w.BranchSpecId == id).FirstOrDefault();

                a.cDel = 1;
                a.UpdatedDate = DateTime.Now;
                a.UpdatedBy = userData.UserID;

                _db.SaveChanges();

                Response.Redirect("branchSetting.aspx");
            }
        }

        void register(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();


                //int counter = 1;
                //foreach (var a in _db.TBranches)
                //{
                //    counter = counter + 1;
                //}

                TBranch Branch = new TBranch();
                //Branch.BranchId = counter;
                Branch.BranchName = modalPlanName.Text;
                Branch.cDel = null;
                Branch.nTLevel = int.Parse(ddlRegisterType.SelectedValue);
                Branch.SchoolID = userData.CompanyID;
                Branch.CreatedDate = DateTime.Now;
                Branch.CreatedBy = userData.UserID;

                _db.TBranches.Add(Branch);



                _db.SaveChanges();
                Response.Redirect("branchSetting.aspx");
            }
        }

        void register2(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();


                //int counter = 1;
                //foreach (var a in _db.TBranchSubjects)
                //{
                //    counter = counter + 1;
                //}

                TBranchSubject Branch = new TBranchSubject();
                //Branch.BranchSubjectId = counter;
                Branch.BranchId = int.Parse(ddlSubject.SelectedValue);
                Branch.BranchSubjectName = subjectName.Text;
                Branch.cDel = null;
                Branch.SchoolID = userData.CompanyID;
                Branch.CreatedDate = DateTime.Now;
                Branch.CreatedBy = userData.UserID;

                _db.TBranchSubjects.Add(Branch);



                _db.SaveChanges();
                Response.Redirect("branchSetting.aspx");
            }
        }

        void register3(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }


            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();


                //int counter = 1;
                //foreach (var a in _db.TBranchSpecs)
                //{
                //    counter = counter + 1;
                //}

                TBranchSpec Branch = new TBranchSpec();
                //Branch.BranchSpecId = counter;
                Branch.BranchSpecName = specName.Text;
                Branch.BranchSubjectId = int.Parse(ddlSpec.SelectedValue);
                Branch.cDel = null;
                Branch.SchoolID = userData.CompanyID;
                Branch.CreatedDate = DateTime.Now;
                Branch.CreatedBy = userData.UserID;

                _db.TBranchSpecs.Add(Branch);



                _db.SaveChanges();
                Response.Redirect("branchSetting.aspx");
            }
        }


        private void Opendata()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                var q_level = _db.TLevels.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false && (w.LevelName == "ปวช." || w.LevelName == "ปวส.")).ToList();


                foreach (var data in q_level)
                {
                    var item = new ListItem
                    {
                        Value = data.LevelID.ToString(),
                        Text = data.LevelName,
                    };
                    editType.Items.Add(item);
                }
                var SeValue = q_level.OrderBy(o => o.LevelID).FirstOrDefault();
                editType.SelectedValue = SeValue.LevelID.ToString();


                foreach (var data1 in _db.TBranches.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel != 1))
                {
                    string type = "";
                    if (q_level.Count(c => c.LevelName == "ปวช." && c.LevelID == data1.nTLevel) > 0 || data1.nTLevel == 1)
                        type = "ปวช.";
                    else if (q_level.Count(c => c.LevelName == "ปวส." && c.LevelID == data1.nTLevel) > 0 || data1.nTLevel == 2)
                        type = "ปวส.";
                    var item = new ListItem
                    {
                        Text = data1.BranchName + " (" + type + ")",
                        Value = data1.BranchId.ToString()
                    };
                    ddlSubject.Items.Add(item);
                }

                foreach (var data2 in _db.TBranchSubjects.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel != 1))
                {
                    var data1 = _db.TBranches.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.BranchId == data2.BranchId).FirstOrDefault();
                    string type = "";
                    if (q_level.Count(c => c.LevelName == "ปวช." && c.LevelID == data1.nTLevel) > 0 || data1.nTLevel == 1)
                        type = "ปวช.";
                    else if (q_level.Count(c => c.LevelName == "ปวส." && c.LevelID == data1.nTLevel) > 0 || data1.nTLevel == 2)
                        type = "ปวส.";
                    var item2 = new ListItem
                    {
                        Text = data2.BranchSubjectName + " (" + type + ")",
                        Value = data2.BranchSubjectId.ToString()
                    };
                    ddlSpec.Items.Add(item2);
                }

                int totalbranch1 = 0;
                int totalsubject1 = 0;
                int totalspec1 = 0;

                int totalbranch2 = 0;
                int totalsubject2 = 0;
                int totalspec2 = 0;
                List<branchlist> branchlist = new List<branchlist>();
                branchlist branch = new branchlist();
                int number = 1;
                foreach (var data in _db.TBranches.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel != 1))
                {
                    branch = new branchlist();
                    branch.id = data.BranchId;
                    int countsubject = 0;
                    int countspec = 0;
                    if (q_level.Count(c => c.LevelName == "ปวช." && c.LevelID == data.nTLevel) > 0 || data.nTLevel == 1)
                        totalbranch1 = totalbranch1 + 1;
                    else if (q_level.Count(c => c.LevelName == "ปวส." && c.LevelID == data.nTLevel) > 0 || data.nTLevel == 2)
                        totalbranch2 = totalbranch2 + 1;

                    foreach (var data2 in _db.TBranchSubjects.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.BranchId == data.BranchId && w.cDel != 1))
                    {
                        countsubject = countsubject + 1;
                        if (data.nTLevel == 1)
                            totalsubject1 = totalsubject1 + 1;
                        else if (data.nTLevel == 2)
                            totalsubject2 = totalsubject2 + 1;

                        foreach (var data3 in _db.TBranchSpecs.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.BranchSubjectId == data2.BranchSubjectId && w.cDel != 1))
                        {
                            if (data.nTLevel == 1)
                                totalspec1 = totalspec1 + 1;
                            else if (data.nTLevel == 2)
                                totalspec2 = totalspec2 + 1;

                            countspec = countspec + 1;
                        }
                    }

                    branch.branchSubject = countsubject;
                    branch.branchSpec = countspec;
                    branch.number = number;
                    number = number + 1;
                    if (data.nTLevel != null)
                    {
                        if (q_level.Count(c => c.LevelName == "ปวช." && c.LevelID == data.nTLevel) > 0 || data.nTLevel == 1)
                            branch.nTLevel = "ปวช.";
                        else if (q_level.Count(c => c.LevelName == "ปวส." && c.LevelID == data.nTLevel) > 0 || data.nTLevel == 2)
                            branch.nTLevel = "ปวส.";
                    }
                    else branch.nTLevel = "";
                    branch.name = data.BranchName;
                    branchlist.Add(branch);
                }

                dgd.DataSource = branchlist;
                dgd.PageSize = 999;
                dgd.DataBind();

                List<subjectlist> subjectlist = new List<subjectlist>();
                subjectlist subject = new subjectlist();
                number = 1;
                foreach (var data4 in _db.TBranchSubjects.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel != 1))
                {
                    subject = new subjectlist();
                    subject.id = data4.BranchSubjectId;

                    subject.number = number;
                    number = number + 1;
                    subject.subjectName = data4.BranchSubjectName;

                    var from = _db.TBranches.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.BranchId == data4.BranchId).FirstOrDefault();
                    subject.branchName = from.BranchName;
                    if (from.nTLevel != null)
                    {
                        if (q_level.Count(c => c.LevelName == "ปวช." && c.LevelID == from.nTLevel) > 0 || from.nTLevel == 1)
                            subject.nTLevel = "ปวช.";
                        else if (q_level.Count(c => c.LevelName == "ปวส." && c.LevelID == from.nTLevel) > 0 || from.nTLevel == 2)
                            subject.nTLevel = "ปวส.";
                    }
                    else subject.nTLevel = "";
                    int spectotal = 0;
                    foreach (var dataspec in _db.TBranchSpecs.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel != 1 && w.BranchSubjectId == data4.BranchSubjectId))
                    {
                        spectotal = spectotal + 1;
                    }
                    subject.spectotal = spectotal;
                    subjectlist.Add(subject);
                }
                GridView1.DataSource = subjectlist;
                GridView1.PageSize = 999;
                GridView1.DataBind();

                List<speclist> speclist = new List<speclist>();
                speclist spec = new speclist();
                number = 1;
                //foreach (var data5 in _db.TBranchSpecs.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel != 1))
                //{

                //    spec = new speclist();
                //    spec.id = data5.BranchSpecId;
                //    spec.number = number;
                //    number = number + 1;
                //    spec.specName = data5.BranchSpecName;
                //    var data6 = _db.TBranchSubjects.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.BranchSubjectId == data5.BranchSubjectId).FirstOrDefault();
                //    var data7 = _db.TBranches.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.BranchId == data6.BranchId).FirstOrDefault();
                //    if (data7.nTLevel != null)
                //    {
                //        if (data7.nTLevel == 1)
                //            if (q_level.Count(c => c.LevelName == "ปวช." && c.LevelID == data7.nTLevel) > 0 || data7.nTLevel == 1)
                //                spec.nTLevel = "ปวช.";
                //            else if (q_level.Count(c => c.LevelName == "ปวส." && c.LevelID == data7.nTLevel) > 0 || data7.nTLevel == 2)
                //                spec.nTLevel = "ปวส.";
                //    }
                //    else spec.nTLevel = "";
                //    spec.branchName = data7.BranchName;
                //    spec.subjectName = data6.BranchSubjectName;
                //    speclist.Add(spec);
                //}

                string SQL = @"SELECT ROW_NUMBER() OVER (ORDER BY A.BranchSpecId) AS number,a.BranchSpecId, C.BranchName AS branchName, 
A.BranchSpecName AS BranchSpecName,A.BranchSpecId AS id,B.BranchSubjectName AS subjectName,
CASE WHEN D.LevelName IS NOT NULL THEN D.LevelName 
WHEN C.nTLevel = 1 THEN 'ปวช.'
WHEN C.nTLevel = 2 THEN 'ปวส.'
END nTLevel 
FROM TBranchSpec AS A
INNER JOIN TBranchSubject AS B ON A.BranchSubjectId = B.BranchSubjectId AND A.SchoolID = B.SchoolID
INNER JOIN TBranch AS C ON B.BranchId = C.BranchId AND C.SchoolID = B.SchoolID
LEFT OUTER JOIN TLevel AS D ON D.LevelID = C.nTLevel AND D.SchoolID = C.SchoolID
WHERE A.SchoolID = " + userData.CompanyID + " AND ISNULL(A.cDel,0) = 0 ";

                speclist.AddRange(_db.Database.SqlQuery<speclist>(SQL).ToList());

                GridView2.DataSource = speclist;
                GridView2.PageSize = 999;
                GridView2.DataBind();
                header11.Text = "จำนวนประเภทวิชาทั้งหมด (ปวช.)";
                header12.Text = totalbranch1.ToString();
                header13.Text = "จำนวนประเภทวิชาทั้งหมด (ปวส.)";
                header14.Text = totalbranch2.ToString();
                header21.Text = "จำนวนสาขาวิชาทั้งหมด (ปวช.)";
                header22.Text = totalsubject1.ToString();
                header23.Text = "จำนวนสาขาวิชาทั้งหมด (ปวส.)";
                header24.Text = totalsubject2.ToString();
                header31.Text = "จำนวนสาขางานทั้งหมด (ปวช.)";
                header32.Text = totalspec1.ToString();
                header33.Text = "จำนวนสาขางานทั้งหมด (ปวส.)";
                header34.Text = totalspec2.ToString();

            }
        }

        protected class branchlist
        {
            public int number { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string nTLevel { get; set; }
            public int branchSubject { get; set; }
            public int branchSpec { get; set; }

        }

        protected class subjectlist
        {
            public int id { get; set; }
            public int number { get; set; }
            public int spectotal { get; set; }
            public string subjectName { get; set; }
            public string branchName { get; set; }
            public string nTLevel { get; set; }
        }

        protected class speclist
        {
            public int id { get; set; }
            public Int64 number { get; set; }
            public string subjectName { get; set; }
            public string specName { get; set; }
            public string nTLevel { get; set; }
            public string branchName { get; set; }

            public string BranchSpecName { get; set; }
        }
    }
}