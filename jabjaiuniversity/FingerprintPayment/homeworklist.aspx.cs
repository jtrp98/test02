using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterEntity;
using JabjaiMainClass;

namespace FingerprintPayment
{
    public class HomeworkGateway : System.Web.UI.Page
    {
        private JWTToken.userData userData;
        protected JWTToken.userData UserData { get { return userData; } }

        protected override void OnLoad(EventArgs e)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

            // Be sure to call the base class's OnLoad method!
            base.OnLoad(e);
        }

        public static JWTToken.userData GetUserData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Default.aspx");
            }

            return userData;
        }
    }

    public partial class homeworklist : HomeworkGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlcType.SelectedIndexChanged += DdlcType_SelectedIndexChanged;
            dgd.ItemCommand += Dgd_ItemCommand;
            if (!this.IsPostBack)
            {
                int schoolID = UserData.CompanyID;
                JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));

                var tTermTimeTable = db.TTermTimeTables.Where(w => w.SchoolID == schoolID);//.Where(w => w.nTerm == nTerm).ToList();
                var listplane4homework = (from a in tTermTimeTable
                                          join b in db.TSchedules.Where(w => w.SchoolID == schoolID) on a.nTermTable equals b.nTermTable
                                          join c in db.TPlanes.Where(w => w.SchoolID == schoolID) on b.sPlaneID equals c.sPlaneID
                                          where c.cDel == null
                                          group c.sPlaneID by new { c.sPlaneID, c.sPlaneName, c.courseCode } into gb
                                          select new { gb.Key.sPlaneID, sPlaneName = gb.Key.courseCode + " - " + gb.Key.sPlaneName }).ToList();

                fcommon.LinqToDropDownList(listplane4homework, ddlcType, "- ทั้งหมด -", "sPlaneID", "sPlaneName");
                Opendata();
            }
        }

        private void DdlcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgd.CurrentPageIndex = 0;
            Opendata();
        }

        private void Dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "page":
                    dgd.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;
                    Opendata();
                    break;
            }
        }

        private void Opendata()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int sEmp = UserData.UserID;
                string sPlaneID = "";

                var qhomework = _db.THomeworks.Where(w => w.SchoolID == schoolID);
                var qplane = _db.TPlanes.Where(w => w.SchoolID == schoolID);

                int index = 1;
                var lHomework = (from a in qhomework
                                 join b in qplane on a.sPlaneID equals b.sPlaneID
                                 //where a.sEmp == sEmp
                                 orderby a.dOrder descending
                                 select new { a.dOrder, b.sPlaneName, a.nHomeWork, a.dEnd, b.sPlaneID, b.courseCode }).ToList();

                if (ddlcType.SelectedValue != "")
                {
                    sPlaneID = ddlcType.SelectedValue.ToString();
                    lHomework = lHomework.Where(w => w.sPlaneID.ToString() == sPlaneID).ToList();
                }

                dgd.DataSource = (from a in lHomework
                                  select new
                                  {
                                      index = index++,
                                      a.dOrder,
                                      sPlaneName = a.courseCode + " - " + a.sPlaneName,
                                      a.nHomeWork,
                                      a.dEnd,
                                      a.sPlaneID
                                  }).ToList();
                dgd.DataBind();
            }
        }
    }
}