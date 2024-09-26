using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.studentclass
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            if (!this.IsPostBack)
            {
                fcommon.ListDBToDropDownList(_conn, ddlyear, "select * from TYear order by numberYear desc", "", "nYear", "numberYear");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            JabJaiMasterEntities masterEntities = Connection.MasterEntities();
            var f_company = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == 34);
            using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(f_company)))
            {

                int nYear = int.Parse(ddlyear.SelectedValue);
                var q_student = entities.TUsers.Where(w => w.cDel == null && (w.nStudentStatus ?? 0) == 0).ToList();
                var q_term = entities.TTerms.Where(w => w.nYear == 3).ToList();
                var q_termId = q_term.Select(s => s.nTerm.Trim()).ToList();

                var q_history = entities.TStudentClassroomHistories.Where(w => q_termId.Contains(w.nTerm)).ToList();

                foreach (var studentData in q_student)
                {
                    foreach (var termData in q_term)
                    {
                        var f_history = q_history.FirstOrDefault(f => f.nTerm == termData.nTerm && f.sID == studentData.sID);

                        if (f_history == null)
                        {
                            entities.TStudentClassroomHistories.Add(new TStudentClassroomHistory
                            {
                                sID = studentData.sID,
                                nTerm = termData.nTerm.Trim(),
                                nTermSubLevel2 = studentData.nTermSubLevel2
                            });
                        }
                    }
                }
                entities.SaveChanges();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString())))
            {
                entities.TStudentClassroomHistories.RemoveRange(entities.TStudentClassroomHistories.ToList());
                entities.TStudentHIstories.RemoveRange(entities.TStudentHIstories.ToList());

                foreach (var studentData in entities.TUsers.Where(w => w.cDel == "G"))
                {
                    studentData.nStudentStatus = null;
                }
                entities.SaveChanges();
            }
        }
    }
}