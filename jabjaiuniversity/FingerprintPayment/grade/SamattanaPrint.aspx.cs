using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;
using FingerprintPayment.Class;
using JabjaiMainClass;
using System.Globalization;
using System.Web.DynamicData;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace FingerprintPayment.grade
{
    public partial class SamattanaPrint : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

              




                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var schooldata = _dbMaster.TCompanies.Where(w => w.nCompany == nCompany.nCompany).FirstOrDefault();

                string idlv = Request.QueryString["idlv"];
                int? idlvn = Int32.Parse(idlv);
                string idlv2 = Request.QueryString["idlv2"];
                int? idlv2n = Int32.Parse(idlv2);
                var room = _db.TSubLevels.Where(w => w.nTSubLevel == idlvn).FirstOrDefault();

                var room2 = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idlv2n).FirstOrDefault();


                Year2.Text = Request.QueryString["term"] + "/" + Request.QueryString["year"];


                if (schooldata != null)
                {
                    schoolpicture.Src = schooldata.sImage;
                }

                txtschool.Text = schooldata.sCompany;
                Label13.Text = room.SubLevel + " / " + room2.nTSubLevel2;


                string year = Request.QueryString["year"];
                string userterm = Request.QueryString["term"];
                string userterm2 = "";
                if (userterm == "1") userterm2 = "2";
                if (userterm == "2") userterm2 = "1";

                var roomz = _db.TSubLevels.Where(w => w.nTSubLevel == idlvn).FirstOrDefault();
                var room2z = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idlv2n).FirstOrDefault();
                pageheader1.Text = "การประเมินสมรรถนะสำคัญของผู้เรียน" + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                pageheader1ex.Text = "การประเมินสมรรถนะสำคัญของผู้เรียน" + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                pageheader2.Text = "แบบบันทึกผลการประเมินสมรรถนะที่ 1 ความสามารถในการสื่อสาร" + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                pageheader2ex.Text = "แบบบันทึกผลการประเมินสมรรถนะที่ 1 ความสามารถในการสื่อสาร" + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                pageheader3.Text = "แบบบันทึกผลการประเมินสมรรถนะที่ 2 ความสามารถในการคิด" + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                pageheader3ex.Text = "แบบบันทึกผลการประเมินสมรรถนะที่ 2 ความสามารถในการคิด" + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                pageheader4.Text = "แบบบันทึกผลการประเมินสมรรถนะที่ 3 ความสามารถในการแก้ปัญหา" + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                pageheader4ex.Text = "แบบบันทึกผลการประเมินสมรรถนะที่ 3 ความสามารถในการแก้ปัญหา" + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                pageheader5.Text = "แบบบันทึกผลการประเมินสมรรถนะที่ 4 ความสามารถในการใช้ทักษะชีวิต" + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                pageheader5ex.Text = "แบบบันทึกผลการประเมินสมรรถนะที่ 4 ความสามารถในการใช้ทักษะชีวิต" + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                pageheader6.Text = "แบบบันทึกผลการประเมินสมรรถนะที่ 5 ความสามารถในการใช้เทคโนโลยี" + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                pageheader6ex.Text = "แบบบันทึกผลการประเมินสมรรถนะที่ 5 ความสามารถในการใช้เทคโนโลยี" + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
                if (!IsPostBack)
                {
                    OpenData();


                }
            }
        }

        private void OpenData()
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
               

                string sEntities = Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();


                string year = Request.QueryString["year"];
                string userterm = Request.QueryString["term"];
                string userterm2 = "";
                if (userterm == "1") userterm2 = "2";
                else if (userterm == "2") userterm2 = "1";
                string idlv2 = Request.QueryString["idlv2"];
                string idlv = Request.QueryString["idlv"];
                int? idlvn = Int32.Parse(idlv);

                int? useryear = Int32.Parse(year);
                int? idlv2n = Int32.Parse(idlv2);
                string username = "";
                int? ntermsublv2 = 0;
                int nyear = 0;
                string nterm = "";
                string nterm2 = "";
                int ntermtable = 0;

                var room = _db.TSubLevels.Where(w => w.nTSubLevel == idlvn).FirstOrDefault();
                var room2 = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idlv2n).FirstOrDefault();



                //txtpaper4.Text = "บันทึกเวลาเรียน";

                foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear))
                {
                    nyear = ff.nYear;
                }

                foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.cDel == null))
                {
                    nterm = ee.nTerm;
                }

                foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm2 && w.nYear == nyear && w.cDel == null))
                {
                    nterm2 = ee.nTerm;
                }



            }
        }

    }
}