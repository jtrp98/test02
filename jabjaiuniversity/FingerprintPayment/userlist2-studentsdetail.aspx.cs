using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using JabjaiNoSQL;
using JabjaiNoSQL.Behavior;
using WebGrease.Css.Extensions;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class userlist2_studentsdetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sEmpID"] == null) Response.Redirect("/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            //dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);
            if (!IsPostBack)
            {
                OpenData();
            }
        }


        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

        }

        private void OpenData()
        {
            //JabJaiEntities _db = new JabJaiEntities(Session["sEntities"].ToString());
             //var _data = _db.TPlanes.Where(w => w.cDel == null).Select(s => new { s.sPlaneName, s.sPlaneID });

            //DataContext tdb = new DataContext();
            //var tBehavior = new Repository<TBehavior>(tdb);
            //var tActivities = new Repository<TBehaviorSetting>(tdb);
            //int sEmpID = int.Parse(Session["sEmpID"] + "");
            //int nSchool = int.Parse(Session["nCompany"] + "");
            //List<beha> b1l = new List<beha>();
            //List<beha> b2l = new List<beha>();
            //List<beha> b3l = new List<beha>();
            //beha b1 = new beha();
            //beha b2 = new beha();
            //beha b3 = new beha();
            //int number1 = 1;
            //int number2 = 1;
            //int number3 = 1;

            //var aaa = tBehavior.List(w => w.Status == true && w.Type == "ลด" && w.SchoolId == nSchool);
            //foreach (var aac in aaa)
            //{
            //    b1 = new beha();
            //    b1.BehaviorName = aac.BehaviorName;
            //    b1.BehaviorId = aac.BehaviorId;
            //    b1.Type = aac.Type;
            //    b1.Score = aac.Score;
            //    b1.number = number1;
            //    b1l.Add(b1);
            //    number1 = number1 + 1;
            //}

            //var bbb = tBehavior.List(w => w.Status == true && w.Type == "เพิ่ม" && w.SchoolId == nSchool);
            //foreach (var aad in bbb)
            //{
            //    b2 = new beha();
            //    b2.BehaviorName = aad.BehaviorName;
            //    b2.BehaviorId = aad.BehaviorId;
            //    b2.Type = aad.Type;
            //    b2.Score = aad.Score;
            //    b2.number = number2;
            //    b2l.Add(b2);
            //    number2 = number2 + 1;
            //}

            //var ccc = tBehavior.List(w => w.Deleted != true && w.Type == "ระบบ" && w.SchoolId == nSchool);
            //foreach (var aae in ccc)
            //{
            //    b3 = new beha();
            //    b3.BehaviorName = aae.BehaviorName;
            //    b3.BehaviorId = aae.BehaviorId;
            //    b3.Type = aae.Type;
            //    b3.Score = aae.Score;
            //    b3.number = number3;
            //    b3l.Add(b3);
            //    number3 = number3 + 1;
            //}

            //dgd.DataSource = b1l;
            //dgd.DataBind();
            //dgd2.DataSource = b1l;
            //dgd2.DataBind();
            //dgd3.DataSource = b2l;
            //dgd3.DataBind();
            //dgd4.DataSource = b3l;
            //dgd4.DataBind();
        }
    }
}