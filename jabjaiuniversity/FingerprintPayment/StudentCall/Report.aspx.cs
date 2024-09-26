using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using FingerprintPayment.Class;
using Antlr.Runtime.Misc;
using iTextSharp.text.pdf.parser;
using System.Linq.Expressions;
using FingerprintPayment.Memory;
using static FingerprintPayment.App_Code.StdCallingHub;
using System.Web.Services;
using System.Web.Script.Services;
using Microsoft.AspNet.SignalR;

using System.Configuration;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNet.SignalR.Client;


namespace FingerprintPayment.StudentCall
{
    //this code was copy from studentcardregister.aspx
    public partial class Report : BaseStudentCall
    {
        //protected override int MenuID => PermissionHelper.MENUID_STUDENTCALL_REPORT;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                List<TSubLevel> SubLevel = new List<TSubLevel>();
                TSubLevel sub = new TSubLevel();

                using (var db = new JabJaiEntities(MasterEntity.Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    SubLevel = db.TSubLevels.Where(w => w.SchoolID == UserData.CompanyID).Where(w => w.nWorkingStatus == 1).ToList();
                }

                foreach (var t in SubLevel)
                {
                    var item = new ListItem
                    {
                        Text = t.SubLevel,
                        Value = t.nTSubLevel.ToString()
                    };
                    ddlsublevel.Items.Add(item);
                }

            }
        }

        public void ssss()
        {

        }
        

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static object ResendAnnouncement(int sID)
        {
            var userData = GetUserData();

            var schoolId = userData.CompanyID;
           

            var url = ConfigurationManager.AppSettings["StudentCallHubUrl"] + "";

            if (url == "")
                url = "https://signalr-studentcall.schoolbright.co/";

            var connection = new HubConnection(url);
            var hub = connection.CreateHubProxy("StdCallingHub");
        
            connection.Start().Wait();
            hub.Invoke("ResendAnnouncement", schoolId, sID).Wait();
            connection.Stop();

            //Task.Factory.StartNew(async () =>
            //{
            //    await connection.Start();
            //    await hub.Invoke("ResendAnnouncement", schoolId, sID);
            //    connection.Stop();
            //}, CancellationToken.None
            //, TaskCreationOptions.LongRunning
            //, TaskScheduler.Default).Wait();

            return true;
        }
    }
}