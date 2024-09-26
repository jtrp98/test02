using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;

namespace FingerprintPayment.App_Logic
{
    public partial class deleteJSON : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                switch (Request.QueryString["mode"])
                {
                    case "delschedule":
                        int learnID = Int32.Parse(Request.QueryString["temp"].ToString());
                        try
                        {
                            _db.TSchedules.Where(w => w.sScheduleID == learnID).ToList().ForEach(f => f.cDel = true);
                            _db.SaveChanges();
                            database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                                "ทำการลบตารางสอน รหัส : " + learnID,
                                HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 1, 4, 0);

                            Response.Write("Success");
                        }
                        catch
                        {
                            Response.Write("error");
                        }
                        Response.End();
                        break;

                    case "delPlaneList":
                        //string planeID = Request.QueryString["planeid"].ToString();
                        //int termSubLevel2 = Int32.Parse(Request.QueryString["termsublv2"].ToString());

                        //try
                        //{
                        //    _db.TPlanLists.Where(w => w.sPlaneID == planeID && w.TTermSublv == termSubLevel2).ToList().ForEach(f => f.cDel = "1");
                        //    _db.SaveChanges();

                        //    _db.TSchedules.Where(w => w.sPlaneID == planeID && w.TermSublv == termSubLevel2).ToList().ForEach(f => f.cDel = "1");
                        //    _db.SaveChanges();

                        //    Response.Write("success");
                        //}
                        //catch (Exception ex)
                        //{
                        //    Response.Write("error");
                        //}
                        //Response.End();

                        break;

                    case "leaver":
                        try
                        {

                            int leaverID = Int32.Parse(Request.QueryString["leaveid"].ToString());
                            foreach (var _data in _db.TLeaves.Where(w => w.LeaveID == leaverID))
                            {
                                _data.cDel = "1";
                            }
                            _db.SaveChanges();
                            Response.Write("success");
                        }
                        catch
                        {
                            Response.Write("error");
                        }
                        Response.End();
                        break;
                }
            }
        }
    }
}