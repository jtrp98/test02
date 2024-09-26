using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using WebGrease.Css.Extensions;
using MasterEntity;
using JabjaiEntity.DB;
using JabjaiMainClass;

namespace FingerprintPayment
{
    public partial class behavior_details : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
                dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);

                int sID = 0;
                Int32.TryParse(Request.QueryString["id"], out sID);
                var data2 = _db.TUser.Where(w => w.sID == sID).FirstOrDefault();

                studentName2.Text = data2.sName + " " + data2.sLastname;
                studentId2.Text = data2.sIdentification;

                if (!IsPostBack)
                {
                    OpenData();
                }
            }
        }


        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

        }

        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string behaviorID = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "Edit":
                    break;
                case "Page":
                    dgd.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;
                    OpenData();
                    break;
                case "Del":
                    break;

            }
        }

        private void OpenData()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int studentId = int.Parse(Request.QueryString["id"]);
                int _Index = 1;
                DateTime dateTime = DateTime.Today.AddYears(-1);
                var h2List = (from a in dbschool.TBehaviorHistories.Where(w => w.SchoolID == schoolID && w.cDel == false).ToList()
                              where a.StudentId == studentId && a.dAdd > dateTime
                              select new BehaviorHistory2
                              {
                                  Number = _Index++,
                                  DayAdd = a.dAdd.Value,
                                  BehaviorName = a.BehaviorName,
                                  Type = a.Type == "0" ? "" : "",
                                  Score = a.Score.Value,
                                  ScoreTotal = a.ResidualScore.Value,
                                  Note = a.Note,
                                  BehaviorId = a.BehaviorId.Value,
                                  dCancle = a.dCanCel == null ? "" : "ยกเลิก"
                              }).ToList();

                var tTerm = dbschool.TTerms.Where(w => w.SchoolID == schoolID && (w.cDel ?? "0") == "0").ToList();
                var tYear = dbschool.TYears.Where(w => w.SchoolID == schoolID && w.cDel == false).ToList();

                StudentLogic logi = new StudentLogic(dbschool);

                foreach (var history in h2List)
                {
                    var dataTerm = tTerm.Where(w => w.dStart.Value <= history.DayAdd && w.dEnd.Value >= history.DayAdd).FirstOrDefault();
                    if (dataTerm != null)
                    {
                        history.StudentTerm = dataTerm.sTerm;
                        var f_year = tYear.Where(w => w.nYear == dataTerm.nYear).FirstOrDefault();
                        if (f_year == null)
                        {
                            dataTerm = logi.GetTermDATA(history.DayAdd, new JWTToken.userData { CompanyID = UserData.CompanyID });
                            f_year = tYear.Where(w => w.nYear == dataTerm.nYear).FirstOrDefault();
                            dataTerm.sTerm = dataTerm.sTerm;
                        }

                        history.StudentYear = f_year.numberYear.Value;
                    }
                }
                dgd.DataSource = h2List;
                dgd.DataBind();
            }
        }

        protected int Yearcal(int yearnow, int termnow, int yearnext, int termnext, int type)
        {
            int x = 0;
            //รายเทอม
            if (type == 1)
            {
                if (termnow != termnext || (yearnow != yearnext))
                    x = 1;
                else x = 0;
            }
            else    //รายปี     
            {
                if (yearnow != yearnext)
                    x = 1;
                else x = 0;
            }

            return x;
        }
    }

    public class BehaviorHistory2
    {
        public int BehaviorId { get; set; }
        public string BehaviorName { get; set; }
        public DateTime DayAdd { get; set; }
        public bool Deleted { get; set; }
        public string Note { get; set; }
        public int SchoolId { get; set; }
        public decimal Score { get; set; }
        public int StudentId { get; set; }
        public string Type { get; set; }
        public int UserAddId { get; set; }
        public int Number { get; set; }
        public decimal ScoreTotal { get; set; }
        public string StudentTerm { get; set; }
        public int StudentYear { get; set; }
        public string dCancle { get; set; }
    }

}