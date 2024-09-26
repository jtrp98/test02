using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Response.Write(string.Format("MachineName: {0} <br/> Version : {1}", Environment.MachineName, Assembly.GetExecutingAssembly().GetName().Version.ToString()
));


                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                {

                    //                    var f_company = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == 34);
                    //                    SqlConnection conn = new SqlConnection("Server=" + f_company.sServer + ";Initial Catalog=" + f_company.sDatabases
                    //+ ";Persist Security Info=False;User ID=" + f_company.sUser + ";Password=" + f_company.sPassword + ";Connection Timeout=30;");

                    //                    var data = fcommon.Get_Data(conn, @"select  sMessage,sell_id,count(*)  from TMessageBox  where nType = 2 and dSend > '10/31/2018' 
                    //group by sMessage, sell_id
                    //having COUNT(*) > 2");

                    //                    if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                    //                    conn.Open();
                    //                    string SQLQuery = "";
                    //                    foreach (DataRow DataRows in data.Rows)
                    //                    {
                    //                        string sell_id = DataRows["sell_id"].ToString();
                    //                        SQLQuery += string.Format(@"delete TMessageBox where sell_id = {0} and nMessageID <> (select top 1 nMessageID  from TMessageBox  where sell_id = {0});  " + Environment.NewLine, sell_id);
                    //                    }

                    //                    SqlCommand comm = new SqlCommand(SQLQuery, conn);
                    //                    comm.ExecuteNonQuery();
                    //                    conn.Close();
                }
                //using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                //{
                //    var q = dbmaster.TCompanies.Select(s => new { s.nCompany, sCompany = s.nCompany + "-" + s.sCompany }).ToList();
                //    fcommon.LinqToDropDownList(q, ddlSchool, "", "nCompany", "sCompany");

                //    var qmobile = dbmaster.TMobileMenus.Where(w => w.Actvie == true).ToList();
                //    var qpermission = dbmaster.permissions.Where(w => w.type == "M").ToList();
                //    var quser = dbmaster.TUsers.Where(w => w.cDel == null && w.cType == "1" && w.sID >= 130229).Select(s => new { s.sID }).ToList();

                //    foreach (var user_data in quser)
                //    {
                //        var q_add = new List<permission>();
                //        foreach (var menu_data in qmobile)
                //        {
                //            var f_permission = qpermission.FirstOrDefault(w => w.user_id == user_data.sID && w.menu_id == menu_data.Menu_Id);
                //            if (f_permission == null)
                //            {
                //                q_add.Add(new permission
                //                {
                //                    actvice = 0,
                //                    menu_id = menu_data.Menu_Id,
                //                    type = "M",
                //                    user_id = user_data.sID
                //                });
                //            }
                //            else
                //            {
                //                //f_permission.actvice = 0;
                //            }
                //        }

                //        dbmaster.permissions.AddRange(q_add);
                //        dbmaster.SaveChanges();
                //    }

                //}
                //Response.Write(OrderAPI());

            }


        }

        static string OrderAPI()
        {

            //JObject payLoad = new JObject(
            //    new JProperty("amount", 200),
            //    new JProperty("currency", "THB"),
            //    new JProperty("description", "TEST PRODUCT"),
            //    new JProperty("source_type", "qr"),
            //    new JProperty("reference_order", "JT620404001"),
            //    new JArray("metadata",
            //          new JObject(
            //            new JProperty("item", "paper"),
            //            new JProperty("qty", 6),
            //            new JProperty("amount", 10)
            //        ))
            //);

            Dictionary<string, string> HttpWebRequestHeader = new Dictionary<string, string>()
                {
                    { "x-api-key","skey_test_228DXmr4Ph3g1LqVo0NuWL7UOOFQ70GULCr"},
                };

            var result = fcommon.send_req("https://dev-kpaymentgateway-services.kasikornbank.com/order", "POST", "{\"amount\" :200,\"currency\" :\"THB\",\"description\": \"TEST PRODUCT\",\"source_type\": \"qr\",\"reference_order\": \"JT620404001\",\"metadata\" :[{\"item\": \"paper\",\"qty\": 6,\"amount\": 10}]}", HttpWebRequestHeader);

            return result;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            var school_id = int.Parse(ddlSchool.SelectedValue.ToString());
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                var f_company = dbmaster.TCompanies.FirstOrDefault(f => f.nCompany == school_id);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(f_company)))
                {
                    var q = (from a in dbschool.TSchedules
                             join b in dbschool.TPlanes on a.sPlaneID equals b.sPlaneID
                             join c in dbschool.TTermTimeTables on a.nTermTable equals c.nTermTable
                             join d in dbschool.TTermSubLevel2 on c.nTermSubLevel2 equals d.nTermSubLevel2
                             join e1 in dbschool.TTerms on c.nTerm.Trim() equals e1.nTerm.Trim()
                             where e1.sTerm == "1"
                             select new
                             {
                                 a.sScheduleID,
                                 b.courseCode,
                                 b.sPlaneID,
                                 b.sPlaneName,
                                 c.nTermTable,
                                 d.nTSubLevel,
                                 //b.nTerm,
                                 sTerm = e1.sTerm,
                                 //Plane_Term = b.nTerm.Value
                             }).ToList();

                    dgd.DataSource = q; //q.Where(w => w.nTerm == 2 && w.sTerm == "1").ToList();
                    dgd.DataBind();
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var school_id = int.Parse(ddlSchool.SelectedValue.ToString());
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                var f_company = dbmaster.TCompanies.FirstOrDefault(f => f.nCompany == school_id);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(f_company)))
                {
                    var q = (from a in dbschool.TSchedules
                             join b in dbschool.TPlanes on a.sPlaneID equals b.sPlaneID
                             join c in dbschool.TTermTimeTables on a.nTermTable equals c.nTermTable
                             join d in dbschool.TTermSubLevel2 on c.nTermSubLevel2 equals d.nTermSubLevel2
                             join e1 in dbschool.TTerms on c.nTerm.Trim() equals e1.nTerm.Trim()
                             select new
                             {
                                 a.sScheduleID,
                                 b.courseCode,
                                 b.sPlaneID,
                                 b.sPlaneName,
                                 c.nTermTable,
                                 d.nTSubLevel,
                                 //b.nTerm,
                                 sTerm = e1.sTerm,
                                 //Plane_Term = b.nTerm.Value
                             }).ToList();

                    //dgd.DataSource = q.Where(w => w.nTerm == 2).ToList();
                    //dgd.DataBind();

                    //var q1 = (from a in dbschool.TPlanes.ToList()
                    //          where a.nTerm == 1
                    //          select new
                    //          {
                    //              a.sPlaneID,
                    //              a.courseCode,
                    //              nTSubLevel = int.Parse(a.nTSubLevel),
                    //          }).ToList();

                    //foreach (var q_data in q.Where(w => w.sTerm == "1" && w.Plane_Term == 2).ToList())
                    //{
                    //    var f_Schedules = dbschool.TSchedules.FirstOrDefault(f => f.sScheduleID == q_data.sScheduleID);
                    //    var f_plane = q1.FirstOrDefault(f => f.courseCode == q_data.courseCode && q_data.nTSubLevel == f.nTSubLevel);

                    //    if (f_Schedules != null && f_plane != null)
                    //    {
                    //        f_Schedules.sPlaneID = f_plane.sPlaneID;
                    //    }
                    //}

                    //dbschool.SaveChanges();
                }
            }
        }

        protected void btnMessageLine_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> HttpWebRequestHeader = new Dictionary<string, string>()
                {
                    { "Authorization","Bearer M4cYgZWS5RIdTM3KxsHKclomQ1dgZ+0wKd6QlJNrVeJoK2GcTlw1I919x/foxErpbcmfW3E16i60C7cD5mriHUxG7nN+dAvsiYDtb7FODIdNY28OOugYrHmtflA0Ork6PXeumwyJxtgAcdZztu2kLQdB04t89/1O/w1cDnyilFU="},
                    //{ "Content-Type","application/json"},
                };

            var result = fcommon.send_req("https://api.line.me/v2/bot/message/push", "POST", "{\"to\": \"Cc56a52ea2c8edfbeeafadc1e27f2bfec\", \"messages\": [ {\"type\": \"text\",\"text\":\"" + textbox.Text + "\"} ]}", HttpWebRequestHeader);
            Response.Write(result);
        }
    }
}