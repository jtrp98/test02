using EntityFramework.BulkInsert.Extensions;
using FingerprintPayment.App_Start;
using FingerprintPayment.Helper;
using Jabjai.Object;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiMainClass.Autocompletes;
using JabjaiMainClass.Models;
using MasterEntity;
using Newtonsoft.Json;
using SchoolBright.DataAccess;
using Sentry;
using Sentry.AspNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace FingerprintPayment
{
    public partial class TGradeEditNew
    {
        public int GradeEditsID { get; set; }
        public int GradeDetailID { get; set; }
        public Nullable<decimal> GradeCalculation { get; set; }
        public Nullable<decimal> GradeSet { get; set; }
        public Nullable<int> UserUpdate { get; set; }
        public string DateUpdate { get; set; }
        public Nullable<int> UseGradeSet { get; set; }
        public int SchoolID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public bool cDel { get; set; }
        public string scoreMidTerm { get; set; }
        public string scoreFinalTerm { get; set; }
        public string getScore100 { get; set; }
        public string getMid100 { get; set; }
        public string getFinal100 { get; set; }
        public string getGradeLabel { get; set; }
    }
    public partial class APIConfiguration
    {
        public int ConfigurationID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public partial class TrackingData
    {
        public int ID { get; set; }
        public Nullable<int> PID { get; set; }
        public Nullable<long> ChVer { get; set; }
        public Nullable<long> ChCrVer { get; set; }
        public string ChOp { get; set; }
        public string sPID { get; set; }
    }
    public static class ServeData
    {
        public static TB_Server B_Server = new TB_Server();
        public static string ServerID = "9A476AF6-C77C-4EE5-824C-03CC60B42841";
        public static List<TM_Configuration> configurations = new List<TM_Configuration>();

        public static string getConfiguration(string Key)
        {
            var f1 = configurations.FirstOrDefault(f => f.Key == Key);
            if (f1 == null) return string.Empty;
            else return f1.Value;
        }
    }

    public class Global : HttpApplication
    {
        public static bool bRefreshMemory = false;
        public static int bProcess1 = 1;
        public static int bProcess2 = 1;
        public static int bProcess3 = 1;
        public static int bProcess4 = 1;

        public static long _CHVerTAssessment = 0;
        public static long _CHVerTAssessmentType = 0;
        public static long _CHVerTGrade = 0;
        public static long _CHVerTGradeDetail = 0;
        public static long _CHVerTGradeEdits = 0;
        public static bool SystemLog = false;

        public static string SecretAPIKey = "";

        public override void Init()
        {
            base.Init();

            //string XRayWebApp = ConfigurationManager.AppSettings["XRayWebApp"];
            //AWSXRayASPNET.RegisterXRay(this, XRayWebApp); // Configuring web app with X-Ray
            //AWSSDKHandler.RegisterXRayForAllServices(); // Configure AWS SDK Handler for X-Ray 
            //try
            //{
            //    base.Init();
            //    string XRayWebApp = ConfigurationManager.AppSettings["XRayWebApp"];
            //    AWSXRayASPNET.RegisterXRay(this, XRayWebApp); // default name of the web app
            //}
            //catch {

            //}


            //HttpContext context = HttpContext.Current;
            //if (context != null && context.Items.Contains(AWSXRayASPNET.XRayEntity))
            //{
            //    Segment requestSegment = (Segment)context.Items[AWSXRayASPNET.XRayEntity];
            //    AWSXRayRecorder.Instance.SetEntity(requestSegment);
            //    AWSXRayRecorder recorder = new AWSXRayRecorderBuilder().Build();
            //    AWSXRayRecorder.InitializeInstance(recorder: recorder);
            //    //AWSXRayASPNET.RegisterXRay(this, Url); // default name of the web app
            //}
            //else
            //{
            //    //HttpContext.Current.Response.Redirect("AdminMain.aspx");
            //}
        }

        static readonly HttpRequest initialRequest;
        public const Newtonsoft.Json.Formatting MyFormatting = Newtonsoft.Json.Formatting.Indented;

        static Global()
        {
            initialRequest = HttpContext.Current.Request;
        }

        // Global error catcher
        protected void Application_Error() => Server.CaptureLastError();

        protected void Application_BeginRequest()
        {
            Context.StartSentryTransaction();
        }

        protected void Application_EndRequest()
        {
            Context.FinishSentryTransaction();
        }

        protected void Application_End()
        {
            // Flushes out events before shutting down.
            _sentry?.Dispose();
        }

        private IDisposable _sentry;
        void Application_Start(object sender, EventArgs e)
        {
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //string Url = ConfigurationManager.AppSettings["XRayWebApp"];
            //HttpContext context = HttpContext.Current;
            //if (context.Items.Contains(AWSXRayASPNET.XRayEntity))
            //{
            //    AWSXRayASPNET.RegisterXRay(this, Url); // default name of the web app
            //}
            //var a1 = HttpContext.Current.Request.Url;
            //urlHost = a1.ToString();
            //urlHost = Dns.GetHostName();

            // Initialize Sentry to capture AppDomain unhandled exceptions and more.
            _sentry = SentrySdk.Init(o =>
            {
                o.Dsn = "https://2c687e8cc9c4049f0c6a1add0cdb3e7e@o4506920728723456.ingest.us.sentry.io/4507077277515776";
                // When configuring for the first time, to see what the SDK is doing:
                o.Debug = true;
                o.EnableTracing = true;
                o.DiagnosticLevel = SentryLevel.Error;
                o.TracesSampleRate = 0.2;

                // If you also installed the Sentry.EntityFramework package
                o.AddEntityFramework();
                o.AddAspNet();
            });

            urlHost = ConfigurationManager.AppSettings["URLHOST"].ToString();
            SecretAPIKey = ConfigurationManager.AppSettings["SecretAPIKey"].ToString();
            SystemLog = bool.Parse((ConfigurationManager.AppSettings["SystemLog"] ?? "false").ToString());
            ServeData.ServerID = ConfigurationManager.AppSettings["ServerID"].ToString();
            //AWSTempScanFunction awsTempScanFunction = new AWSTempScanFunction(0);
            //ServeData.configurations = awsTempScanFunction.ReadTempScanVersionS3Object($"Configuration/system-config.json");

            using (JabJaiMasterEntities entitiesy = Connection.MasterEntities(ConnectionDB.Read))
            {
                Guid guid = new Guid(ServeData.ServerID);
                ServeData.B_Server = entitiesy.TB_Server.FirstOrDefault(f => f.ID == guid);
            }



            RefreshConfiguration();


            //AWSTempScanFunction awsTempScanFunction = new AWSTempScanFunction(0);
            //ServeData.configurations = awsTempScanFunction.ReadTempScanVersionS3Object($"Configuration /system-config.json");

            //urlHost = ServeData.getConfiguration("URLHOST");
            //ServeData.ServerID = ServeData.getConfiguration("ServerID");

            //urlHost = initialRequest.Url.GetLeftPart(UriPartial.Authority).ToString();

            #region disabled

            //BackgroundWorker work1 = new BackgroundWorker();
            //work1.DoWork += new DoWorkEventHandler(Work_Sync_TAssessment);
            //work1.RunWorkerAsync();
            //BackgroundWorker dowork2 = new BackgroundWorker();
            //dowork2.DoWork += new DoWorkEventHandler(Work_Sync_TGrade);
            //dowork2.RunWorkerAsync();
            //BackgroundWorker work3 = new BackgroundWorker();
            //work3.DoWork += new DoWorkEventHandler(Work_Sync_TGradeDetails);
            //work3.RunWorkerAsync();
            //BackgroundWorker work4 = new BackgroundWorker();
            //work4.DoWork += new DoWorkEventHandler(Work_Sync_TGradeEdit);
            //work4.RunWorkerAsync();
            #endregion

            //BackgroundWorker work5 = new BackgroundWorker();
            //work5.DoWork += new DoWorkEventHandler(Work_Sync_MemoryDataTGrade);
            //work5.RunWorkerAsync();

            //BackgroundWorker work6 = new BackgroundWorker();
            //work6.DoWork += new DoWorkEventHandler(Work_Sync_MemoryDataTAssessment);
            //work6.RunWorkerAsync();

            //BackgroundWorker work7 = new BackgroundWorker();
            //work7.DoWork += new DoWorkEventHandler(Work_Sync_MemoryDataTGradeDetail);
            //work7.RunWorkerAsync();

            //BackgroundWorker work8 = new BackgroundWorker();
            //work8.DoWork += new DoWorkEventHandler(Work_Sync_MemoryDataTGradeEdit);
            //work8.RunWorkerAsync();

            //BackgroundWorker work9 = new BackgroundWorker();
            //work9.DoWork += new DoWorkEventHandler(Work_Sync_TAssessmentType);
            //work9.RunWorkerAsync();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            DTOConfigurator.Configure();
            Configurator.Initialize();

            if (ConfigurationManager.AppSettings["BackgroundWorker"].ToString() == "N")
            {
                BackgroundWorker work = new BackgroundWorker();
                work.DoWork += new DoWorkEventHandler(work_DoWork);
                work.RunWorkerAsync();

                BackgroundWorker autoCompleteTopup = new BackgroundWorker();
                autoCompleteTopup.DoWork += new DoWorkEventHandler(WorkAutoCompleteTopup_DoWork);
                autoCompleteTopup.RunWorkerAsync();

                update_Autocompletes();
            }

            using (JabJaiMasterEntities entitiesy = Connection.MasterEntities(ConnectionDB.Read))
            {
                Guid guid = new Guid(ServeData.ServerID);
                ServeData.B_Server = entitiesy.TB_Server.FirstOrDefault(f => f.ID == guid);
            }

        }
        public static void RefreshConfiguration()
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
            {
                string sql = "select * from APIConfiguration where Name in ('bUsePGLiteDBForGrade','bUsePGDBForLogs') ";

                List<APIConfiguration> _ApiConfigurationList = dbschool.Database.SqlQuery<APIConfiguration>(sql).ToList();
                foreach (APIConfiguration _APIConfiguration in _ApiConfigurationList)
                {
                    switch (_APIConfiguration.Name)
                    {
                        case "bUsePGLiteDBForGrade":
                            if (_APIConfiguration.Value == "Y")
                                DataAccessHelper.bUsePGLiteDBForGrade = true;
                            else
                                DataAccessHelper.bUsePGLiteDBForGrade = false;
                            break;

                        case "bUsePGDBForLogs":
                            if (_APIConfiguration.Value == "Y")
                                DataAccessHelper.bUsePGDBForLogs = true;
                            else
                                DataAccessHelper.bUsePGDBForLogs = false;
                            break;
                    }
                }
            }
        }

        static DateTime dateUpdate = new DateTime(1999, 1, 1);
        static string urlHost = "";


        //static void Work_Sync_MemoryDataTGrade(object sender, DoWorkEventArgs e)
        //{
        //    while (true)
        //    {
        //        try
        //        {


        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {


        //                #region TGrade

        //                List<PGTGrade> _PGTGradeProcess = DataAccessHelper._TGradeList.Take(500).ToList();
        //                List<PGTGrade> _PGTGradeProcessSelected = new List<PGTGrade>();
        //                _PGTGradeProcessSelected.AddRange(_PGTGradeProcess);


        //                if (_PGTGradeProcess.Count > 0)
        //                {
        //                    List<TGrade> _TGrades = (from t in _PGTGradeProcess
        //                                             select new
        //                                             {
        //                                                 t.nGradeId,
        //                                                 t.nTerm,
        //                                                 t.sPlaneID,
        //                                                 t.sNote,
        //                                                 t.dAdd,
        //                                                 t.dUpdate,
        //                                                 t.nUserAdd,
        //                                                 t.nUserUpdate,
        //                                                 t.fRatioQuiz,
        //                                                 t.fRatioMidTerm,
        //                                                 t.fRatioLateTerm,
        //                                                 t.maxMidTerm,
        //                                                 t.maxFinalTerm,
        //                                                 t.maxGradeTotal,
        //                                                 t.maxBehaviorTotal,
        //                                                 t.studyMonday,
        //                                                 t.studyTuesday,
        //                                                 t.studyWednesday,
        //                                                 t.studyThursday,
        //                                                 t.studyFriday,
        //                                                 t.studySaturday,
        //                                                 t.studySunday,
        //                                                 t.GradeDicimal,
        //                                                 t.GradeAutoReadScore,
        //                                                 t.GradeAutoBehaviorScore,
        //                                                 t.GradeAddBehavior,
        //                                                 t.GradeAddCheewat,
        //                                                 t.GradeCloseBehaviorReadWrite,
        //                                                 t.GradeShareData,
        //                                                 t.nTermSubLevel2,
        //                                                 t.fRatioQuizPass,
        //                                                 t.GradeCloseGrade,
        //                                                 t.sortNumber,
        //                                                 t.reportPrint,
        //                                                 t.GradeCloseSamattana,
        //                                                 t.optionMid,
        //                                                 t.optionFinal,
        //                                                 t.SchoolID,
        //                                                 t.CreatedBy,
        //                                                 t.UpdatedBy,
        //                                                 t.CreatedDate,
        //                                                 t.UpdatedDate,
        //                                                 t.cDel,
        //                                                 t.GradeAddSamattana,
        //                                                 t.maxReadWriteTotal,
        //                                                 t.maxSamattanaTotal,
        //                                                 t.maxBeforeMidTerm,
        //                                                 t.maxAfterMidTerm,
        //                                                 t.GradeShowFullScore,
        //                                                 t.fRatioBeforeMidTerm,
        //                                                 t.fRatioAfterMidTerm,
        //                                                 t.PlanId,
        //                                                 t.GradeDicimalForFinal,
        //                                             }
        //                             ).AsEnumerable().Select(x => new TGrade
        //                             {
        //                                 nGradeId = (int)x.nGradeId,
        //                                 nTerm = x.nTerm,
        //                                 sPlaneID = x.sPlaneID,
        //                                 sNote = x.sNote,
        //                                 dAdd = x.dAdd,
        //                                 dUpdate = x.dUpdate,
        //                                 nUserAdd = x.nUserAdd,
        //                                 nUserUpdate = x.nUserUpdate,
        //                                 fRatioQuiz = x.fRatioQuiz,
        //                                 fRatioMidTerm = x.fRatioMidTerm,
        //                                 fRatioLateTerm = x.fRatioLateTerm,
        //                                 maxMidTerm = x.maxMidTerm,
        //                                 maxFinalTerm = x.maxFinalTerm,
        //                                 maxGradeTotal = x.maxGradeTotal,
        //                                 maxBehaviorTotal = x.maxBehaviorTotal,
        //                                 studyMonday = x.studyMonday,
        //                                 studyTuesday = x.studyTuesday,
        //                                 studyWednesday = x.studyWednesday,
        //                                 studyThursday = x.studyThursday,
        //                                 studyFriday = x.studyFriday,
        //                                 studySaturday = x.studySaturday,
        //                                 studySunday = x.studySunday,
        //                                 GradeDicimal = x.GradeDicimal,
        //                                 GradeAutoReadScore = x.GradeAutoReadScore,
        //                                 GradeAutoBehaviorScore = x.GradeAutoBehaviorScore,
        //                                 GradeAddBehavior = x.GradeAddBehavior,
        //                                 GradeAddCheewat = x.GradeAddCheewat,
        //                                 GradeCloseBehaviorReadWrite = x.GradeCloseBehaviorReadWrite,
        //                                 GradeShareData = x.GradeShareData,
        //                                 nTermSubLevel2 = x.nTermSubLevel2,
        //                                 fRatioQuizPass = x.fRatioQuizPass,
        //                                 GradeCloseGrade = x.GradeCloseGrade,
        //                                 sortNumber = x.sortNumber,
        //                                 reportPrint = x.reportPrint,
        //                                 GradeCloseSamattana = x.GradeCloseSamattana,
        //                                 optionMid = x.optionMid,
        //                                 optionFinal = x.optionFinal,
        //                                 SchoolID = x.SchoolID,
        //                                 CreatedBy = x.CreatedBy,
        //                                 UpdatedBy = x.UpdatedBy,
        //                                 CreatedDate = x.CreatedDate,
        //                                 UpdatedDate = x.UpdatedDate,
        //                                 cDel = x.cDel,
        //                                 GradeAddSamattana = x.GradeAddSamattana,
        //                                 maxReadWriteTotal = x.maxReadWriteTotal,
        //                                 maxSamattanaTotal = x.maxSamattanaTotal,
        //                                 maxBeforeMidTerm = x.maxBeforeMidTerm,
        //                                 maxAfterMidTerm = x.maxAfterMidTerm,
        //                                 GradeShowFullScore = x.GradeShowFullScore,
        //                                 fRatioBeforeMidTerm = x.fRatioBeforeMidTerm,
        //                                 fRatioAfterMidTerm = x.fRatioAfterMidTerm,
        //                                 PlanId = x.PlanId,
        //                                 GradeDicimalForFinal = x.GradeDicimalForFinal,

        //                             }).ToList();



        //                    foreach (TGrade sData in _TGrades)
        //                    {
        //                        TGrade _ExistingObj = _dbGrade.TGrades.Where(x => x.nGradeId == sData.nGradeId).FirstOrDefault();
        //                        if (_ExistingObj != null)
        //                        {
        //                            _dbGrade.TGrades.AddOrUpdate(sData);
        //                            _dbGrade.SaveChanges();

        //                            _PGTGradeProcess.RemoveAll(x => x.nGradeId == sData.nGradeId);
        //                        }



        //                    }



        //                    try
        //                    {



        //                        if (_PGTGradeProcess.Count > 0)
        //                        {


        //                            var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeProcess);
        //                            var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);

        //                            var dt = new DataTable();
        //                            var conn = _dbGrade.Database.Connection;
        //                            var connectionState = conn.State;
        //                            _dbGrade.Database.CommandTimeout = 16000;


        //                            try
        //                            {


        //                                if (connectionState != ConnectionState.Open) conn.Open();
        //                                using (var cmd = conn.CreateCommand())
        //                                {
        //                                    cmd.CommandTimeout = 16000;
        //                                    cmd.CommandText = "SQAddTGrade";
        //                                    cmd.CommandType = CommandType.StoredProcedure;
        //                                    cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));




        //                                    using (var reader = cmd.ExecuteReader())
        //                                    {
        //                                        dt.Load(reader);
        //                                    }




        //                                }
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                // return new GetGradeDetailView_Result();

        //                            }
        //                            finally
        //                            {
        //                                //if (connectionState != ConnectionState.Closed) conn.Close();
        //                            }
        //                        }
        //                    }
        //                    catch
        //                    {

        //                    }

        //                    DataAccessHelper._TGradeList.RemoveAll(x => _PGTGradeProcessSelected.Contains(x));

        //                    List<PGTGrade> _PGTGradeRemoveProcess = DataAccessHelper._TGradeRemoveList.Take(500).ToList();
        //                    if (_PGTGradeProcess.Count > 0)
        //                    {
        //                        List<TGrade> _TGradesRemove = (from t in _PGTGradeProcess
        //                                                       select new
        //                                                       {
        //                                                           t.nGradeId,
        //                                                           t.nTerm,
        //                                                           t.sPlaneID,
        //                                                           t.sNote,
        //                                                           t.dAdd,
        //                                                           t.dUpdate,
        //                                                           t.nUserAdd,
        //                                                           t.nUserUpdate,
        //                                                           t.fRatioQuiz,
        //                                                           t.fRatioMidTerm,
        //                                                           t.fRatioLateTerm,
        //                                                           t.maxMidTerm,
        //                                                           t.maxFinalTerm,
        //                                                           t.maxGradeTotal,
        //                                                           t.maxBehaviorTotal,
        //                                                           t.studyMonday,
        //                                                           t.studyTuesday,
        //                                                           t.studyWednesday,
        //                                                           t.studyThursday,
        //                                                           t.studyFriday,
        //                                                           t.studySaturday,
        //                                                           t.studySunday,
        //                                                           t.GradeDicimal,
        //                                                           t.GradeAutoReadScore,
        //                                                           t.GradeAutoBehaviorScore,
        //                                                           t.GradeAddBehavior,
        //                                                           t.GradeAddCheewat,
        //                                                           t.GradeCloseBehaviorReadWrite,
        //                                                           t.GradeShareData,
        //                                                           t.nTermSubLevel2,
        //                                                           t.fRatioQuizPass,
        //                                                           t.GradeCloseGrade,
        //                                                           t.sortNumber,
        //                                                           t.reportPrint,
        //                                                           t.GradeCloseSamattana,
        //                                                           t.optionMid,
        //                                                           t.optionFinal,
        //                                                           t.SchoolID,
        //                                                           t.CreatedBy,
        //                                                           t.UpdatedBy,
        //                                                           t.CreatedDate,
        //                                                           t.UpdatedDate,
        //                                                           t.cDel,
        //                                                           t.GradeAddSamattana,
        //                                                           t.maxReadWriteTotal,
        //                                                           t.maxSamattanaTotal,
        //                                                           t.maxBeforeMidTerm,
        //                                                           t.maxAfterMidTerm,
        //                                                           t.GradeShowFullScore,
        //                                                           t.fRatioBeforeMidTerm,
        //                                                           t.fRatioAfterMidTerm,
        //                                                           t.PlanId,
        //                                                           t.GradeDicimalForFinal,
        //                                                       }
        //                            ).AsEnumerable().Select(x => new TGrade
        //                            {
        //                                nGradeId = (int)x.nGradeId,
        //                                nTerm = x.nTerm,
        //                                sPlaneID = x.sPlaneID,
        //                                sNote = x.sNote,
        //                                dAdd = x.dAdd,
        //                                dUpdate = x.dUpdate,
        //                                nUserAdd = x.nUserAdd,
        //                                nUserUpdate = x.nUserUpdate,
        //                                fRatioQuiz = x.fRatioQuiz,
        //                                fRatioMidTerm = x.fRatioMidTerm,
        //                                fRatioLateTerm = x.fRatioLateTerm,
        //                                maxMidTerm = x.maxMidTerm,
        //                                maxFinalTerm = x.maxFinalTerm,
        //                                maxGradeTotal = x.maxGradeTotal,
        //                                maxBehaviorTotal = x.maxBehaviorTotal,
        //                                studyMonday = x.studyMonday,
        //                                studyTuesday = x.studyTuesday,
        //                                studyWednesday = x.studyWednesday,
        //                                studyThursday = x.studyThursday,
        //                                studyFriday = x.studyFriday,
        //                                studySaturday = x.studySaturday,
        //                                studySunday = x.studySunday,
        //                                GradeDicimal = x.GradeDicimal,
        //                                GradeAutoReadScore = x.GradeAutoReadScore,
        //                                GradeAutoBehaviorScore = x.GradeAutoBehaviorScore,
        //                                GradeAddBehavior = x.GradeAddBehavior,
        //                                GradeAddCheewat = x.GradeAddCheewat,
        //                                GradeCloseBehaviorReadWrite = x.GradeCloseBehaviorReadWrite,
        //                                GradeShareData = x.GradeShareData,
        //                                nTermSubLevel2 = x.nTermSubLevel2,
        //                                fRatioQuizPass = x.fRatioQuizPass,
        //                                GradeCloseGrade = x.GradeCloseGrade,
        //                                sortNumber = x.sortNumber,
        //                                reportPrint = x.reportPrint,
        //                                GradeCloseSamattana = x.GradeCloseSamattana,
        //                                optionMid = x.optionMid,
        //                                optionFinal = x.optionFinal,
        //                                SchoolID = x.SchoolID,
        //                                CreatedBy = x.CreatedBy,
        //                                UpdatedBy = x.UpdatedBy,
        //                                CreatedDate = x.CreatedDate,
        //                                UpdatedDate = x.UpdatedDate,
        //                                cDel = x.cDel,
        //                                GradeAddSamattana = x.GradeAddSamattana,
        //                                maxReadWriteTotal = x.maxReadWriteTotal,
        //                                maxSamattanaTotal = x.maxSamattanaTotal,
        //                                maxBeforeMidTerm = x.maxBeforeMidTerm,
        //                                maxAfterMidTerm = x.maxAfterMidTerm,
        //                                GradeShowFullScore = x.GradeShowFullScore,
        //                                fRatioBeforeMidTerm = x.fRatioBeforeMidTerm,
        //                                fRatioAfterMidTerm = x.fRatioAfterMidTerm,
        //                                PlanId = x.PlanId,
        //                                GradeDicimalForFinal = x.GradeDicimalForFinal,

        //                            }).ToList();

        //                        _dbGrade.TGrades.RemoveRange(_TGradesRemove);
        //                        _dbGrade.SaveChanges();

        //                        DataAccessHelper._TGradeRemoveList.RemoveAll(x => _PGTGradeRemoveProcess.Contains(x));
        //                    }
        //                }

        //                #endregion

        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        System.Threading.Thread.Sleep(100); // 10 second

        //    }
        //}
        //static void Work_Sync_MemoryDataTAssessment(object sender, DoWorkEventArgs e)
        //{
        //    while (true)
        //    {
        //        try
        //        {


        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                #region TAssessment

        //                List<PGTAssessment> _PGTAssessmentProcess = DataAccessHelper._TAssessmentsList.Take(500).ToList();
        //                List<PGTAssessment> _PGTAssessmentProcessSelected = new List<PGTAssessment>();
        //                _PGTAssessmentProcessSelected.AddRange(_PGTAssessmentProcess);

        //                if (_PGTAssessmentProcess.Count > 0)
        //                {
        //                    List<TAssessment> _TAssessments = (from a in _PGTAssessmentProcess
        //                                                       select new
        //                                                       {
        //                                                           a.AssessmentId,
        //                                                           a.AssessmentName,
        //                                                           a.AssessmentTypeId,
        //                                                           a.AssessmentMaxScore,
        //                                                           a.NameIdentifier,
        //                                                           a.MaxScoreIdentifier,
        //                                                           a.SchoolId,
        //                                                           a.nGradeId,
        //                                                           a.nYear,
        //                                                           a.nTerm,
        //                                                           a.nTSubLevel,
        //                                                           a.nTermSubLevel2,
        //                                                           a.sPlaneID,
        //                                                           a.CreatedDate,
        //                                                           a.UpdatedBy,
        //                                                           a.CreatedBy,
        //                                                           a.UpdatedDate,
        //                                                           a.IsActive,
        //                                                           a.cDel,
        //                                                           a.SubmitPeriod,
        //                                                           a.AssessmentNameEng,
        //                                                       }
        //                        ).AsEnumerable().Select(x => new TAssessment
        //                        {
        //                            AssessmentId = (int)x.AssessmentId,
        //                            AssessmentName = x.AssessmentName,
        //                            AssessmentTypeId = x.AssessmentTypeId,
        //                            AssessmentMaxScore = x.AssessmentMaxScore,
        //                            NameIdentifier = x.NameIdentifier,
        //                            MaxScoreIdentifier = x.MaxScoreIdentifier,
        //                            SchoolId = x.SchoolId,
        //                            nGradeId = x.nGradeId,
        //                            nYear = x.nYear,
        //                            nTerm = x.nTerm,
        //                            nTSubLevel = x.nTSubLevel,
        //                            nTermSubLevel2 = x.nTermSubLevel2,
        //                            sPlaneID = x.sPlaneID,
        //                            CreatedDate = x.CreatedDate,
        //                            UpdatedBy = x.UpdatedBy,
        //                            CreatedBy = x.CreatedBy,
        //                            UpdatedDate = x.UpdatedDate,
        //                            IsActive = x.IsActive,
        //                            cDel = x.cDel,
        //                            SubmitPeriod = x.SubmitPeriod,
        //                            AssessmentNameEng = x.AssessmentNameEng,

        //                        }).ToList();

        //                    foreach (TAssessment sData in _TAssessments)
        //                    {
        //                        TAssessment _ExistingObj = _dbGrade.TAssessments.Where(x => x.AssessmentId == sData.AssessmentId).FirstOrDefault();
        //                        if (_ExistingObj != null)
        //                        {
        //                            _dbGrade.TAssessments.AddOrUpdate(sData);
        //                            _dbGrade.SaveChanges();

        //                            _PGTAssessmentProcess.RemoveAll(x => x.AssessmentId == sData.AssessmentId);
        //                        }


        //                    }


        //                    try
        //                    {


        //                        if (_PGTAssessmentProcess.Count > 0)
        //                        {

        //                            var Datas = ServiceHelper.GetXMLTAssessmentParameter(_PGTAssessmentProcess);
        //                            var xmlDataList = Common.GetXMLFromObject(Datas);

        //                            var dt = new DataTable();
        //                            var conn = _dbGrade.Database.Connection;
        //                            var connectionState = conn.State;
        //                            _dbGrade.Database.CommandTimeout = 16000;


        //                            try
        //                            {


        //                                if (connectionState != ConnectionState.Open) conn.Open();
        //                                using (var cmd = conn.CreateCommand())
        //                                {
        //                                    cmd.CommandTimeout = 16000;
        //                                    cmd.CommandText = "SQAddTAssessment";
        //                                    cmd.CommandType = CommandType.StoredProcedure;
        //                                    cmd.Parameters.Add(new SqlParameter("@XMLTAssessmentList", xmlDataList));




        //                                    using (var reader = cmd.ExecuteReader())
        //                                    {
        //                                        dt.Load(reader);
        //                                    }




        //                                }
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                // return new GetGradeDetailView_Result();

        //                            }
        //                            finally
        //                            {
        //                                //if (connectionState != ConnectionState.Closed) conn.Close();
        //                            }
        //                        }
        //                    }
        //                    catch
        //                    {

        //                    }

        //                    DataAccessHelper._TAssessmentsList.RemoveAll(x => _PGTAssessmentProcessSelected.Contains(x));

        //                    List<PGTAssessment> _PGTAssessmentRemoveProcess = DataAccessHelper._TAssessmentsRemoveList.Take(500).ToList();
        //                    if (_PGTAssessmentRemoveProcess.Count > 0)
        //                    {
        //                        List<TAssessment> _TAssessmentRemoves = (from a in _PGTAssessmentRemoveProcess
        //                                                                 select new
        //                                                                 {
        //                                                                     a.AssessmentId,
        //                                                                     a.AssessmentName,
        //                                                                     a.AssessmentTypeId,
        //                                                                     a.AssessmentMaxScore,
        //                                                                     a.NameIdentifier,
        //                                                                     a.MaxScoreIdentifier,
        //                                                                     a.SchoolId,
        //                                                                     a.nGradeId,
        //                                                                     a.nYear,
        //                                                                     a.nTerm,
        //                                                                     a.nTSubLevel,
        //                                                                     a.nTermSubLevel2,
        //                                                                     a.sPlaneID,
        //                                                                     a.CreatedDate,
        //                                                                     a.UpdatedBy,
        //                                                                     a.CreatedBy,
        //                                                                     a.UpdatedDate,
        //                                                                     a.IsActive,
        //                                                                     a.cDel,
        //                                                                     a.SubmitPeriod,
        //                                                                     a.AssessmentNameEng,
        //                                                                 }
        //                       ).AsEnumerable().Select(x => new TAssessment
        //                       {
        //                           AssessmentId = (int)x.AssessmentId,
        //                           AssessmentName = x.AssessmentName,
        //                           AssessmentTypeId = x.AssessmentTypeId,
        //                           AssessmentMaxScore = x.AssessmentMaxScore,
        //                           NameIdentifier = x.NameIdentifier,
        //                           MaxScoreIdentifier = x.MaxScoreIdentifier,
        //                           SchoolId = x.SchoolId,
        //                           nGradeId = x.nGradeId,
        //                           nYear = x.nYear,
        //                           nTerm = x.nTerm,
        //                           nTSubLevel = x.nTSubLevel,
        //                           nTermSubLevel2 = x.nTermSubLevel2,
        //                           sPlaneID = x.sPlaneID,
        //                           CreatedDate = x.CreatedDate,
        //                           UpdatedBy = x.UpdatedBy,
        //                           CreatedBy = x.CreatedBy,
        //                           UpdatedDate = x.UpdatedDate,
        //                           IsActive = x.IsActive,
        //                           cDel = x.cDel,
        //                           SubmitPeriod = x.SubmitPeriod,
        //                           AssessmentNameEng = x.AssessmentNameEng,

        //                       }).ToList();

        //                        _dbGrade.TAssessments.RemoveRange(_TAssessmentRemoves);
        //                        _dbGrade.SaveChanges();

        //                        DataAccessHelper._TAssessmentsRemoveList.RemoveAll(x => _PGTAssessmentRemoveProcess.Contains(x));
        //                    }
        //                }

        //                #endregion


        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        System.Threading.Thread.Sleep(100); // 10 second

        //    }
        //}


        //static void Work_Sync_MemoryDataTGradeDetail(object sender, DoWorkEventArgs e)
        //{
        //    while (true)
        //    {
        //        try
        //        {


        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {

        //                #region TGradeDetail

        //                List<PGTGradeDetail> _PGTGradeDetailProcess = DataAccessHelper._TGradeDetailList.Take(500).ToList();
        //                List<PGTGradeDetail> _PGTGradeDetailProcessSelected = new List<PGTGradeDetail>();
        //                _PGTGradeDetailProcessSelected.AddRange(_PGTGradeDetailProcess);


        //                if (_PGTGradeDetailProcess.Count > 0)
        //                {
        //                    List<TGradeDetail> _TGradeDetails = (from t in _PGTGradeDetailProcess
        //                                                         select new
        //                                                         {
        //                                                             t.nGradeDetailId,
        //                                                             t.nGradeId,
        //                                                             t.sID,
        //                                                             t.scoreMidTerm,
        //                                                             t.scoreFinalTerm,
        //                                                             t.getScore100,
        //                                                             t.getGradeLabel,
        //                                                             t.getBehaviorLabel,
        //                                                             t.getReadWrite,
        //                                                             t.getSpecial,
        //                                                             t.getBehaviorTotal,
        //                                                             t.getQuiz100,
        //                                                             t.getMid100,
        //                                                             t.getFinal100,
        //                                                             t.getSamattana,
        //                                                             t.SchoolID,
        //                                                             t.CreatedBy,
        //                                                             t.UpdatedBy,
        //                                                             t.CreatedDate,
        //                                                             t.UpdatedDate,
        //                                                             t.cDel,
        //                                                             t.scoreBeforeAfterMidTerm,
        //                                                             t.ScoreData,
        //                                                             t.ScoreBeforeMidTerm,
        //                                                             t.ScoreAfterMidTerm,
        //                                                             t.getReadWriteTotal,
        //                                                             t.getSamattanaTotal,
        //                                                             t.getBeforeQuiz100,
        //                                                             t.getAfterQuiz100,
        //                                                             t.nCredit,
        //                                                             t.CourseTotalHour,
        //                                                             t.PlanId,
        //                                                         }
        //                             ).AsEnumerable().Select(x => new TGradeDetail
        //                             {
        //                                 nGradeDetailId = (int)x.nGradeDetailId,
        //                                 nGradeId = x.nGradeId,
        //                                 sID = x.sID,
        //                                 scoreMidTerm = x.scoreMidTerm,
        //                                 scoreFinalTerm = x.scoreFinalTerm,
        //                                 getScore100 = x.getScore100,
        //                                 getGradeLabel = x.getGradeLabel,
        //                                 getBehaviorLabel = x.getBehaviorLabel,
        //                                 getReadWrite = x.getReadWrite,
        //                                 getSpecial = x.getSpecial,
        //                                 getBehaviorTotal = x.getBehaviorTotal,
        //                                 getQuiz100 = x.getQuiz100,
        //                                 getMid100 = x.getMid100,
        //                                 getFinal100 = x.getFinal100,
        //                                 getSamattana = x.getSamattana,
        //                                 SchoolID = x.SchoolID,
        //                                 CreatedBy = x.CreatedBy,
        //                                 UpdatedBy = x.UpdatedBy,
        //                                 CreatedDate = x.CreatedDate,
        //                                 UpdatedDate = x.UpdatedDate,
        //                                 cDel = x.cDel,
        //                                 scoreBeforeAfterMidTerm = x.scoreBeforeAfterMidTerm,
        //                                 ScoreData = x.ScoreData,
        //                                 ScoreBeforeMidTerm = x.ScoreBeforeMidTerm,
        //                                 ScoreAfterMidTerm = x.ScoreAfterMidTerm,
        //                                 getReadWriteTotal = x.getReadWriteTotal,
        //                                 getSamattanaTotal = x.getSamattanaTotal,
        //                                 getBeforeQuiz100 = x.getBeforeQuiz100,
        //                                 getAfterQuiz100 = x.getAfterQuiz100,
        //                                 nCredit = x.nCredit,
        //                                 CourseTotalHour = x.CourseTotalHour,
        //                                 PlanId = x.PlanId,

        //                             }).ToList();

        //                    foreach (TGradeDetail sData in _TGradeDetails)
        //                    {
        //                        TGradeDetail _ExistingObj = _dbGrade.TGradeDetails.Where(x => x.nGradeDetailId == sData.nGradeDetailId).FirstOrDefault();
        //                        if (_ExistingObj != null)
        //                        {
        //                            _dbGrade.TGradeDetails.AddOrUpdate(sData);
        //                            _dbGrade.SaveChanges();

        //                            _PGTGradeDetailProcess.RemoveAll(x => x.nGradeDetailId == sData.nGradeDetailId);
        //                        }

        //                    }



        //                    try
        //                    {


        //                        if (_PGTGradeDetailProcess.Count > 0)
        //                        {

        //                            var Datas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailProcess);
        //                            var xmlDataList = Common.GetXMLFromObject(Datas);

        //                            var dt = new DataTable();
        //                            var conn = _dbGrade.Database.Connection;
        //                            var connectionState = conn.State;
        //                            _dbGrade.Database.CommandTimeout = 16000;


        //                            try
        //                            {


        //                                if (connectionState != ConnectionState.Open) conn.Open();
        //                                using (var cmd = conn.CreateCommand())
        //                                {
        //                                    cmd.CommandTimeout = 16000;
        //                                    cmd.CommandText = "SQAddTGradeDetail";
        //                                    cmd.CommandType = CommandType.StoredProcedure;
        //                                    cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlDataList));




        //                                    using (var reader = cmd.ExecuteReader())
        //                                    {
        //                                        dt.Load(reader);
        //                                    }




        //                                }
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                // return new GetGradeDetailView_Result();

        //                            }
        //                            finally
        //                            {
        //                                //if (connectionState != ConnectionState.Closed) conn.Close();
        //                            }
        //                        }
        //                    }
        //                    catch
        //                    {

        //                    }

        //                    DataAccessHelper._TGradeDetailList.RemoveAll(x => _PGTGradeDetailProcessSelected.Contains(x));


        //                    List<PGTGradeDetail> _PGTGradeDetailRemoveProcess = DataAccessHelper._TGradeDetailRemoveList.Take(500).ToList();
        //                    if (_PGTGradeDetailRemoveProcess.Count > 0)
        //                    {
        //                        List<TGradeDetail> _TGradeDetailsRemove = (from t in _PGTGradeDetailRemoveProcess
        //                                                                   select new
        //                                                                   {
        //                                                                       t.nGradeDetailId,
        //                                                                       t.nGradeId,
        //                                                                       t.sID,
        //                                                                       t.scoreMidTerm,
        //                                                                       t.scoreFinalTerm,
        //                                                                       t.getScore100,
        //                                                                       t.getGradeLabel,
        //                                                                       t.getBehaviorLabel,
        //                                                                       t.getReadWrite,
        //                                                                       t.getSpecial,
        //                                                                       t.getBehaviorTotal,
        //                                                                       t.getQuiz100,
        //                                                                       t.getMid100,
        //                                                                       t.getFinal100,
        //                                                                       t.getSamattana,
        //                                                                       t.SchoolID,
        //                                                                       t.CreatedBy,
        //                                                                       t.UpdatedBy,
        //                                                                       t.CreatedDate,
        //                                                                       t.UpdatedDate,
        //                                                                       t.cDel,
        //                                                                       t.scoreBeforeAfterMidTerm,
        //                                                                       t.ScoreData,
        //                                                                       t.ScoreBeforeMidTerm,
        //                                                                       t.ScoreAfterMidTerm,
        //                                                                       t.getReadWriteTotal,
        //                                                                       t.getSamattanaTotal,
        //                                                                       t.getBeforeQuiz100,
        //                                                                       t.getAfterQuiz100,
        //                                                                       t.nCredit,
        //                                                                       t.CourseTotalHour,
        //                                                                       t.PlanId,
        //                                                                   }
        //                            ).AsEnumerable().Select(x => new TGradeDetail
        //                            {
        //                                nGradeDetailId = (int)x.nGradeDetailId,
        //                                nGradeId = x.nGradeId,
        //                                sID = x.sID,
        //                                scoreMidTerm = x.scoreMidTerm,
        //                                scoreFinalTerm = x.scoreFinalTerm,
        //                                getScore100 = x.getScore100,
        //                                getGradeLabel = x.getGradeLabel,
        //                                getBehaviorLabel = x.getBehaviorLabel,
        //                                getReadWrite = x.getReadWrite,
        //                                getSpecial = x.getSpecial,
        //                                getBehaviorTotal = x.getBehaviorTotal,
        //                                getQuiz100 = x.getQuiz100,
        //                                getMid100 = x.getMid100,
        //                                getFinal100 = x.getFinal100,
        //                                getSamattana = x.getSamattana,
        //                                SchoolID = x.SchoolID,
        //                                CreatedBy = x.CreatedBy,
        //                                UpdatedBy = x.UpdatedBy,
        //                                CreatedDate = x.CreatedDate,
        //                                UpdatedDate = x.UpdatedDate,
        //                                cDel = x.cDel,
        //                                scoreBeforeAfterMidTerm = x.scoreBeforeAfterMidTerm,
        //                                ScoreData = x.ScoreData,
        //                                ScoreBeforeMidTerm = x.ScoreBeforeMidTerm,
        //                                ScoreAfterMidTerm = x.ScoreAfterMidTerm,
        //                                getReadWriteTotal = x.getReadWriteTotal,
        //                                getSamattanaTotal = x.getSamattanaTotal,
        //                                getBeforeQuiz100 = x.getBeforeQuiz100,
        //                                getAfterQuiz100 = x.getAfterQuiz100,
        //                                nCredit = x.nCredit,
        //                                CourseTotalHour = x.CourseTotalHour,
        //                                PlanId = x.PlanId,

        //                            }).ToList();

        //                        _dbGrade.TGradeDetails.RemoveRange(_TGradeDetailsRemove);
        //                        _dbGrade.SaveChanges();




        //                        DataAccessHelper._TGradeDetailRemoveList.RemoveAll(x => _PGTGradeDetailRemoveProcess.Contains(x));
        //                    }
        //                }

        //                #endregion


        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        System.Threading.Thread.Sleep(100); // 10 second

        //    }
        //}

        //static void Work_Sync_MemoryDataTGradeEdit(object sender, DoWorkEventArgs e)
        //{
        //    while (true)
        //    {
        //        try
        //        {


        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {

        //                #region TGradeEdit

        //                List<PGTGradeEdit> _PGTGradeEditProcess = DataAccessHelper._TGradeEditList.Take(500).ToList();
        //                List<PGTGradeEdit> _PGTGradeEditProcessSelected = new List<PGTGradeEdit>();
        //                _PGTGradeEditProcessSelected.AddRange(_PGTGradeEditProcess);

        //                if (_PGTGradeEditProcess.Count > 0)
        //                {
        //                    List<TGradeEdit> _TGradeEdits = (from t in _PGTGradeEditProcess
        //                                                     select new
        //                                                     {
        //                                                         t.GradeEditsID,
        //                                                         t.GradeDetailID,
        //                                                         GradeCalculation = t.GradeCalculation == null ? 0 : t.GradeCalculation,
        //                                                         GradeSet = t.GradeSet == null ? 0 : t.GradeSet,
        //                                                         t.UserUpdate,
        //                                                         t.DateUpdate,
        //                                                         t.UseGradeSet,
        //                                                         t.SchoolID,
        //                                                         t.CreatedBy,
        //                                                         t.UpdatedBy,
        //                                                         t.CreatedDate,
        //                                                         t.UpdatedDate,
        //                                                         t.cDel,
        //                                                         t.scoreMidTerm,
        //                                                         t.scoreFinalTerm,
        //                                                         t.getScore100,
        //                                                         t.getMid100,
        //                                                         t.getFinal100,
        //                                                         t.getGradeLabel,
        //                                                     }
        //                             ).AsEnumerable().Select(x => new TGradeEdit
        //                             {
        //                                 GradeEditsID = (int)x.GradeEditsID,
        //                                 GradeDetailID = x.GradeDetailID,
        //                                 GradeCalculation = (decimal)x.GradeCalculation,
        //                                 GradeSet = (decimal)x.GradeSet,
        //                                 UserUpdate = x.UserUpdate,
        //                                 DateUpdate = x.DateUpdate,
        //                                 UseGradeSet = x.UseGradeSet,
        //                                 SchoolID = x.SchoolID,
        //                                 CreatedBy = x.CreatedBy,
        //                                 UpdatedBy = x.UpdatedBy,
        //                                 CreatedDate = x.CreatedDate,
        //                                 UpdatedDate = x.UpdatedDate,
        //                                 cDel = x.cDel,
        //                                 scoreMidTerm = x.scoreMidTerm,
        //                                 scoreFinalTerm = x.scoreFinalTerm,
        //                                 getScore100 = x.getScore100,
        //                                 getMid100 = x.getMid100,
        //                                 getFinal100 = x.getFinal100,
        //                                 getGradeLabel = x.getGradeLabel,

        //                             }).ToList();

        //                    foreach (TGradeEdit sData in _TGradeEdits)
        //                    {
        //                        TGradeEdit _ExistingObj = _dbGrade.TGradeEdits.Where(x => x.GradeEditsID == sData.GradeEditsID).FirstOrDefault();
        //                        if (_ExistingObj != null)
        //                        {
        //                            _dbGrade.TGradeEdits.AddOrUpdate(sData);
        //                            _dbGrade.SaveChanges();

        //                            _PGTGradeEditProcess.RemoveAll(x => x.GradeEditsID == sData.GradeEditsID);
        //                        }

        //                    }

        //                    try
        //                    {



        //                        if (_PGTGradeEditProcessSelected.Count > 0)
        //                        {
        //                            var Datas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditProcess);
        //                            var xmlDataList = Common.GetXMLFromObject(Datas);

        //                            var dt = new DataTable();
        //                            var conn = _dbGrade.Database.Connection;
        //                            var connectionState = conn.State;
        //                            _dbGrade.Database.CommandTimeout = 16000;


        //                            try
        //                            {


        //                                if (connectionState != ConnectionState.Open) conn.Open();
        //                                using (var cmd = conn.CreateCommand())
        //                                {
        //                                    cmd.CommandTimeout = 16000;
        //                                    cmd.CommandText = "SQAddTGradeEdits";
        //                                    cmd.CommandType = CommandType.StoredProcedure;
        //                                    cmd.Parameters.Add(new SqlParameter("@XMLTGradeEditsList", xmlDataList));




        //                                    using (var reader = cmd.ExecuteReader())
        //                                    {
        //                                        dt.Load(reader);
        //                                    }




        //                                }
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                // return new GetGradeDetailView_Result();

        //                            }
        //                            finally
        //                            {
        //                                //if (connectionState != ConnectionState.Closed) conn.Close();
        //                            }
        //                        }
        //                    }
        //                    catch
        //                    {

        //                    }

        //                    DataAccessHelper._TGradeEditList.RemoveAll(x => _PGTGradeEditProcessSelected.Contains(x));

        //                    List<PGTGradeEdit> _PGTGradeEditRemoveProcess = DataAccessHelper._TGradeEditRemoveList.Take(500).ToList();
        //                    if (_PGTGradeEditRemoveProcess.Count > 0)
        //                    {
        //                        List<TGradeEdit> _TGradeEditsRemove = (from t in _PGTGradeEditRemoveProcess
        //                                                               select new
        //                                                               {
        //                                                                   t.GradeEditsID,
        //                                                                   t.GradeDetailID,
        //                                                                   GradeCalculation = t.GradeCalculation == null ? 0 : t.GradeCalculation,
        //                                                                   GradeSet = t.GradeSet == null ? 0 : t.GradeSet,
        //                                                                   t.UserUpdate,
        //                                                                   t.DateUpdate,
        //                                                                   t.UseGradeSet,
        //                                                                   t.SchoolID,
        //                                                                   t.CreatedBy,
        //                                                                   t.UpdatedBy,
        //                                                                   t.CreatedDate,
        //                                                                   t.UpdatedDate,
        //                                                                   t.cDel,
        //                                                                   t.scoreMidTerm,
        //                                                                   t.scoreFinalTerm,
        //                                                                   t.getScore100,
        //                                                                   t.getMid100,
        //                                                                   t.getFinal100,
        //                                                                   t.getGradeLabel,
        //                                                               }
        //                           ).AsEnumerable().Select(x => new TGradeEdit
        //                           {
        //                               GradeEditsID = (int)x.GradeEditsID,
        //                               GradeDetailID = x.GradeDetailID,
        //                               GradeCalculation = (decimal)x.GradeCalculation,
        //                               GradeSet = (decimal)x.GradeSet,
        //                               UserUpdate = x.UserUpdate,
        //                               DateUpdate = x.DateUpdate,
        //                               UseGradeSet = x.UseGradeSet,
        //                               SchoolID = x.SchoolID,
        //                               CreatedBy = x.CreatedBy,
        //                               UpdatedBy = x.UpdatedBy,
        //                               CreatedDate = x.CreatedDate,
        //                               UpdatedDate = x.UpdatedDate,
        //                               cDel = x.cDel,
        //                               scoreMidTerm = x.scoreMidTerm,
        //                               scoreFinalTerm = x.scoreFinalTerm,
        //                               getScore100 = x.getScore100,
        //                               getMid100 = x.getMid100,
        //                               getFinal100 = x.getFinal100,
        //                               getGradeLabel = x.getGradeLabel,

        //                           }).ToList();

        //                        _dbGrade.TGradeEdits.RemoveRange(_TGradeEditsRemove);
        //                        _dbGrade.SaveChanges();

        //                        DataAccessHelper._TGradeEditRemoveList.RemoveAll(x => _PGTGradeEditRemoveProcess.Contains(x));

        //                    }
        //                }

        //                #endregion
        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        System.Threading.Thread.Sleep(100); // 10 second

        //    }
        //}

        //static void Work_Sync_TAssessmentType(object sender, DoWorkEventArgs e)
        //{
        //    while (true)
        //    {
        //        try
        //        {

        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {
        //                var _TableTrackingInfo = pgLiteDB.TableTrackingInfoes.Where(x => x.TableName == "TAssessmentType").FirstOrDefault();

        //                _CHVerTAssessment = (long)_TableTrackingInfo.UpdatedID;

        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    _dbGrade.Database.CommandTimeout = 16000;

        //                    string sql = @"
        //                     SELECT 0 as ID,
        //                    ChVer = SYS_CHANGE_VERSION,
        //                    ChCrVer = SYS_CHANGE_CREATION_VERSION,
        //                    ChOp = SYS_CHANGE_OPERATION,
        //                    {1} as PID,'0' as sPID
        //                    FROM CHANGETABLE(CHANGES {0}, {2}) AS ChTbl
        //                    order by 1 ";

        //                    var trackingData = _dbGrade.Database.SqlQuery<TrackingData>(string.Format(sql, "TAssessmentType", "AssessmentTypeId", _CHVerTAssessmentType)).ToList().AsEnumerable()
        //         .Select(x => new TrackingData { PID = x.PID, ChVer = x.ChVer, ChCrVer = x.ChCrVer, ChOp = x.ChOp }).OrderBy(x => x.ChVer).ToList();

        //                    foreach (TrackingData _Result in trackingData)
        //                    {
        //                        PGTAssessmentType lData = pgLiteDB.PGTAssessmentTypes.Where(x => x.AssessmentTypeId == _Result.PID).FirstOrDefault();

        //                        PGTAssessmentType sData = (from a in _dbGrade.TAssessmentTypes
        //                                                   where a.AssessmentTypeId == _Result.PID
        //                                                   select new
        //                                                   {
        //                                                       a.AssessmentTypeId,
        //                                                       a.AssessmentTypeDesc,
        //                                                       a.DisplayOrder,
        //                                                       a.IsActive,
        //                                                       a.SchoolID,
        //                                                       a.CreatedBy,
        //                                                       a.UpdatedBy,
        //                                                       a.CreatedDate,
        //                                                       a.UpdatedDate,
        //                                                       a.cDel


        //                                                   }
        //                              ).AsEnumerable().Select(x => new PGTAssessmentType
        //                              {
        //                                  AssessmentTypeId = x.AssessmentTypeId,
        //                                  AssessmentTypeDesc = x.AssessmentTypeDesc,
        //                                  DisplayOrder = x.DisplayOrder,
        //                                  IsActive = x.IsActive,
        //                                  SchoolID = x.SchoolID,
        //                                  CreatedBy = x.CreatedBy,
        //                                  UpdatedBy = x.UpdatedBy,
        //                                  CreatedDate = x.CreatedDate,
        //                                  UpdatedDate = x.UpdatedDate,
        //                                  cDel = x.cDel,

        //                              }).FirstOrDefault();

        //                        if (lData != null)
        //                        {
        //                            if (sData != null)
        //                            {
        //                                pgLiteDB.PGTAssessmentTypes.AddOrUpdate(sData);
        //                                pgLiteDB.SaveChanges();

        //                            }
        //                            else
        //                            {
        //                                pgLiteDB.PGTAssessmentTypes.Remove(lData);
        //                                pgLiteDB.SaveChanges();
        //                            }

        //                        }
        //                        else
        //                        {
        //                            if (sData != null)
        //                            {
        //                                long PrimaryID = sData.AssessmentTypeId;
        //                                //    sData.AssessmentTypeId = 0;
        //                                pgLiteDB.PGTAssessmentTypes.Add(sData);
        //                                pgLiteDB.SaveChanges();


        //                                //string sqlUpdate = "UPDATE public.PGTAssessmentType SET AssessmentTypeId = " + PrimaryID + " WHERE  AssessmentTypeId = " + sData.AssessmentTypeId + " ";
        //                                //pgLiteDB.Database.ExecuteSqlCommand(sqlUpdate);


        //                            }
        //                        }

        //                        _CHVerTAssessmentType = (long)_Result.ChVer;

        //                        _TableTrackingInfo.UpdatedID = (int)_CHVerTAssessment;

        //                        pgLiteDB.TableTrackingInfoes.AddOrUpdate(_TableTrackingInfo);
        //                        pgLiteDB.SaveChanges();

        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        System.Threading.Thread.Sleep(100); // 10 second

        //    }
        //}
        //static void Work_Sync_TAssessment(object sender, DoWorkEventArgs e)
        //{
        //    while (true)
        //    {
        //        try
        //        {

        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {
        //                TableTrackingInfo _TableTrackingInfo = pgLiteDB.TableTrackingInfoes.Where(x => x.TableName == "TAssessment").FirstOrDefault();

        //                _CHVerTAssessment = (long)_TableTrackingInfo.UpdatedID;

        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    _dbGrade.Database.CommandTimeout = 16000;

        //                    string sql = @"
        //                     SELECT 0 as ID,
        //                    ChVer = SYS_CHANGE_VERSION,
        //                    ChCrVer = SYS_CHANGE_CREATION_VERSION,
        //                    ChOp = SYS_CHANGE_OPERATION,
        //                    {1} as PID,'0' as sPID
        //                    FROM CHANGETABLE(CHANGES {0}, {2}) AS ChTbl
        //                    order by 1 ";

        //                    var trackingData = _dbGrade.Database.SqlQuery<TrackingData>(string.Format(sql, "TAssessment", "AssessmentId", _CHVerTAssessment)).ToList().AsEnumerable()
        //         .Select(x => new TrackingData { PID = x.PID, ChVer = x.ChVer, ChCrVer = x.ChCrVer, ChOp = x.ChOp }).OrderBy(x => x.ChVer).ToList();

        //                    foreach (TrackingData _Result in trackingData)
        //                    {
        //                        PGTAssessment lData = pgLiteDB.PGTAssessments.Where(x => x.AssessmentId == _Result.PID).FirstOrDefault();

        //                        PGTAssessment sData = (from a in _dbGrade.TAssessments
        //                                               where a.AssessmentId == _Result.PID
        //                                               select new
        //                                               {
        //                                                   a.AssessmentId,
        //                                                   a.AssessmentName,
        //                                                   a.AssessmentTypeId,
        //                                                   a.AssessmentMaxScore,
        //                                                   a.NameIdentifier,
        //                                                   a.MaxScoreIdentifier,
        //                                                   a.SchoolId,
        //                                                   a.nGradeId,
        //                                                   a.nYear,
        //                                                   a.nTerm,
        //                                                   a.nTSubLevel,
        //                                                   a.nTermSubLevel2,
        //                                                   a.sPlaneID,
        //                                                   a.CreatedDate,
        //                                                   a.UpdatedBy,
        //                                                   a.CreatedBy,
        //                                                   a.UpdatedDate,
        //                                                   a.IsActive,
        //                                                   a.cDel,
        //                                                   a.SubmitPeriod,
        //                                                   a.AssessmentNameEng,
        //                                               }
        //                             ).AsEnumerable().Select(x => new PGTAssessment
        //                             {
        //                                 AssessmentId = x.AssessmentId,
        //                                 AssessmentName = x.AssessmentName,
        //                                 AssessmentTypeId = x.AssessmentTypeId,
        //                                 AssessmentMaxScore = x.AssessmentMaxScore,
        //                                 NameIdentifier = x.NameIdentifier,
        //                                 MaxScoreIdentifier = x.MaxScoreIdentifier,
        //                                 SchoolId = x.SchoolId,
        //                                 nGradeId = x.nGradeId,
        //                                 nYear = x.nYear,
        //                                 nTerm = x.nTerm,
        //                                 nTSubLevel = x.nTSubLevel,
        //                                 nTermSubLevel2 = x.nTermSubLevel2,
        //                                 sPlaneID = x.sPlaneID,
        //                                 CreatedDate = x.CreatedDate,
        //                                 UpdatedBy = x.UpdatedBy,
        //                                 CreatedBy = x.CreatedBy,
        //                                 UpdatedDate = x.UpdatedDate,
        //                                 IsActive = x.IsActive,
        //                                 cDel = x.cDel,
        //                                 SubmitPeriod = x.SubmitPeriod,
        //                                 AssessmentNameEng = x.AssessmentNameEng,

        //                             }).FirstOrDefault();

        //                        if (lData != null)
        //                        {
        //                            if (sData != null)
        //                            {
        //                                pgLiteDB.PGTAssessments.AddOrUpdate(sData);
        //                                pgLiteDB.SaveChanges();

        //                            }
        //                            else
        //                            {
        //                                pgLiteDB.PGTAssessments.Remove(lData);
        //                                pgLiteDB.SaveChanges();
        //                            }

        //                        }
        //                        else
        //                        {
        //                            if (sData != null)
        //                            {
        //                                long PrimaryID = sData.AssessmentId;

        //                                pgLiteDB.PGTAssessments.Add(sData);
        //                                pgLiteDB.SaveChanges();

        //                                string sqlUpdate = "UPDATE PGTAssessment SET AssessmentId = " + PrimaryID + " WHERE  AssessmentId = " + sData.AssessmentId + " ";
        //                                pgLiteDB.Database.ExecuteSqlCommand(sqlUpdate);


        //                            }
        //                        }

        //                        _CHVerTAssessment = (long)_Result.ChVer;

        //                        _TableTrackingInfo.UpdatedID = (int)_CHVerTAssessment;

        //                        pgLiteDB.TableTrackingInfoes.AddOrUpdate(_TableTrackingInfo);
        //                        pgLiteDB.SaveChanges();

        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        System.Threading.Thread.Sleep(100); // 10 second

        //    }
        //}
        //static void Work_Sync_TGrade(object sender, DoWorkEventArgs e)
        //{
        //    while (true)
        //    {
        //        try
        //        {

        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {
        //                TableTrackingInfo _TableTrackingInfo = pgLiteDB.TableTrackingInfoes.Where(x => x.TableName == "TGrade").FirstOrDefault();

        //                _CHVerTGrade = (long)_TableTrackingInfo.UpdatedID;

        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    _dbGrade.Database.CommandTimeout = 16000;

        //                    string sql = @"
        //                     SELECT 0 as ID,
        //                    ChVer = SYS_CHANGE_VERSION,
        //                    ChCrVer = SYS_CHANGE_CREATION_VERSION,
        //                    ChOp = SYS_CHANGE_OPERATION,
        //                    {1} as PID,'0' as sPID
        //                    FROM CHANGETABLE(CHANGES {0}, {2}) AS ChTbl
        //                    order by 1 ";

        //                    var trackingData = _dbGrade.Database.SqlQuery<TrackingData>(string.Format(sql, "TGrade", "nGradeID", _CHVerTGrade)).ToList().AsEnumerable()
        //         .Select(x => new TrackingData { PID = x.PID, ChVer = x.ChVer, ChCrVer = x.ChCrVer, ChOp = x.ChOp }).OrderBy(x => x.ChVer).ToList();

        //                    foreach (TrackingData _Result in trackingData)
        //                    {
        //                        PGTGrade lData = pgLiteDB.PGTGrades.Where(x => x.nGradeId == _Result.PID).FirstOrDefault();

        //                        PGTGrade sData = (from t in _dbGrade.TGrades
        //                                          where t.nGradeId == _Result.PID
        //                                          select new
        //                                          {
        //                                              t.nGradeId,
        //                                              t.nTerm,
        //                                              t.sPlaneID,
        //                                              t.sNote,
        //                                              t.dAdd,
        //                                              t.dUpdate,
        //                                              t.nUserAdd,
        //                                              t.nUserUpdate,
        //                                              t.fRatioQuiz,
        //                                              t.fRatioMidTerm,
        //                                              t.fRatioLateTerm,
        //                                              t.maxMidTerm,
        //                                              t.maxFinalTerm,
        //                                              t.maxGradeTotal,
        //                                              t.maxBehaviorTotal,
        //                                              t.studyMonday,
        //                                              t.studyTuesday,
        //                                              t.studyWednesday,
        //                                              t.studyThursday,
        //                                              t.studyFriday,
        //                                              t.studySaturday,
        //                                              t.studySunday,
        //                                              t.GradeDicimal,
        //                                              t.GradeAutoReadScore,
        //                                              t.GradeAutoBehaviorScore,
        //                                              t.GradeAddBehavior,
        //                                              t.GradeAddCheewat,
        //                                              t.GradeCloseBehaviorReadWrite,
        //                                              t.GradeShareData,
        //                                              t.nTermSubLevel2,
        //                                              t.fRatioQuizPass,
        //                                              t.GradeCloseGrade,
        //                                              t.sortNumber,
        //                                              t.reportPrint,
        //                                              t.GradeCloseSamattana,
        //                                              t.optionMid,
        //                                              t.optionFinal,
        //                                              t.SchoolID,
        //                                              t.CreatedBy,
        //                                              t.UpdatedBy,
        //                                              t.CreatedDate,
        //                                              t.UpdatedDate,
        //                                              t.cDel,
        //                                              t.GradeAddSamattana,
        //                                              t.maxReadWriteTotal,
        //                                              t.maxSamattanaTotal,
        //                                              t.maxBeforeMidTerm,
        //                                              t.maxAfterMidTerm,
        //                                              t.GradeShowFullScore,
        //                                              t.fRatioBeforeMidTerm,
        //                                              t.fRatioAfterMidTerm,
        //                                              t.PlanId,
        //                                              t.GradeDicimalForFinal,
        //                                          }
        //                             ).AsEnumerable().Select(x => new PGTGrade
        //                             {
        //                                 nGradeId = x.nGradeId,
        //                                 nTerm = x.nTerm,
        //                                 sPlaneID = x.sPlaneID,
        //                                 sNote = x.sNote,
        //                                 dAdd = x.dAdd,
        //                                 dUpdate = x.dUpdate,
        //                                 nUserAdd = x.nUserAdd,
        //                                 nUserUpdate = x.nUserUpdate,
        //                                 fRatioQuiz = x.fRatioQuiz,
        //                                 fRatioMidTerm = x.fRatioMidTerm,
        //                                 fRatioLateTerm = x.fRatioLateTerm,
        //                                 maxMidTerm = x.maxMidTerm,
        //                                 maxFinalTerm = x.maxFinalTerm,
        //                                 maxGradeTotal = x.maxGradeTotal,
        //                                 maxBehaviorTotal = x.maxBehaviorTotal,
        //                                 studyMonday = x.studyMonday,
        //                                 studyTuesday = x.studyTuesday,
        //                                 studyWednesday = x.studyWednesday,
        //                                 studyThursday = x.studyThursday,
        //                                 studyFriday = x.studyFriday,
        //                                 studySaturday = x.studySaturday,
        //                                 studySunday = x.studySunday,
        //                                 GradeDicimal = x.GradeDicimal,
        //                                 GradeAutoReadScore = x.GradeAutoReadScore,
        //                                 GradeAutoBehaviorScore = x.GradeAutoBehaviorScore,
        //                                 GradeAddBehavior = x.GradeAddBehavior,
        //                                 GradeAddCheewat = x.GradeAddCheewat,
        //                                 GradeCloseBehaviorReadWrite = x.GradeCloseBehaviorReadWrite,
        //                                 GradeShareData = x.GradeShareData,
        //                                 nTermSubLevel2 = x.nTermSubLevel2,
        //                                 fRatioQuizPass = x.fRatioQuizPass,
        //                                 GradeCloseGrade = x.GradeCloseGrade,
        //                                 sortNumber = x.sortNumber,
        //                                 reportPrint = x.reportPrint,
        //                                 GradeCloseSamattana = x.GradeCloseSamattana,
        //                                 optionMid = x.optionMid,
        //                                 optionFinal = x.optionFinal,
        //                                 SchoolID = x.SchoolID,
        //                                 CreatedBy = x.CreatedBy,
        //                                 UpdatedBy = x.UpdatedBy,
        //                                 CreatedDate = x.CreatedDate,
        //                                 UpdatedDate = x.UpdatedDate,
        //                                 cDel = x.cDel,
        //                                 GradeAddSamattana = x.GradeAddSamattana,
        //                                 maxReadWriteTotal = x.maxReadWriteTotal,
        //                                 maxSamattanaTotal = x.maxSamattanaTotal,
        //                                 maxBeforeMidTerm = x.maxBeforeMidTerm,
        //                                 maxAfterMidTerm = x.maxAfterMidTerm,
        //                                 GradeShowFullScore = x.GradeShowFullScore,
        //                                 fRatioBeforeMidTerm = x.fRatioBeforeMidTerm,
        //                                 fRatioAfterMidTerm = x.fRatioAfterMidTerm,
        //                                 PlanId = x.PlanId,
        //                                 GradeDicimalForFinal = x.GradeDicimalForFinal,

        //                             }).FirstOrDefault();

        //                        if (lData != null)
        //                        {
        //                            if (sData != null)
        //                            {
        //                                pgLiteDB.PGTGrades.AddOrUpdate(sData);
        //                                pgLiteDB.SaveChanges();

        //                            }
        //                            else
        //                            {
        //                                pgLiteDB.PGTGrades.Remove(lData);
        //                                pgLiteDB.SaveChanges();
        //                            }

        //                        }
        //                        else
        //                        {
        //                            if (sData != null)
        //                            {

        //                                long PrimaryID = sData.nGradeId;

        //                                pgLiteDB.PGTGrades.Add(sData);
        //                                pgLiteDB.SaveChanges();

        //                                string sqlUpdate = "UPDATE PGTGrade SET nGradeId = " + PrimaryID + " WHERE  nGradeId = " + sData.nGradeId + " ";
        //                                pgLiteDB.Database.ExecuteSqlCommand(sqlUpdate);


        //                            }
        //                        }

        //                        _CHVerTGrade = (long)_Result.ChVer;

        //                        _TableTrackingInfo.UpdatedID = (int)_CHVerTGrade;

        //                        pgLiteDB.TableTrackingInfoes.AddOrUpdate(_TableTrackingInfo);
        //                        pgLiteDB.SaveChanges();

        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        System.Threading.Thread.Sleep(100); // 10 second

        //    }
        //}
        //static void Work_Sync_TGradeDetails(object sender, DoWorkEventArgs e)
        //{
        //    while (true)
        //    {
        //        try
        //        {

        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {
        //                TableTrackingInfo _TableTrackingInfo = pgLiteDB.TableTrackingInfoes.Where(x => x.TableName == "TGradeDetail").FirstOrDefault();

        //                _CHVerTGradeDetail = (long)_TableTrackingInfo.UpdatedID;

        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    _dbGrade.Database.CommandTimeout = 16000;

        //                    string sql = @"
        //                     SELECT 0 as ID,
        //                    ChVer = SYS_CHANGE_VERSION,
        //                    ChCrVer = SYS_CHANGE_CREATION_VERSION,
        //                    ChOp = SYS_CHANGE_OPERATION,
        //                    {1} as PID,'0' as sPID
        //                    FROM CHANGETABLE(CHANGES {0}, {2}) AS ChTbl
        //                    order by 1 ";

        //                    var trackingData = _dbGrade.Database.SqlQuery<TrackingData>(string.Format(sql, "TGradeDetail", "nGradeDetailId", _CHVerTGradeDetail)).ToList().AsEnumerable()
        //         .Select(x => new TrackingData { PID = x.PID, ChVer = x.ChVer, ChCrVer = x.ChCrVer, ChOp = x.ChOp }).OrderBy(x => x.ChVer).ToList();

        //                    foreach (TrackingData _Result in trackingData)
        //                    {
        //                        PGTGradeDetail lData = pgLiteDB.PGTGradeDetails.Where(x => x.nGradeDetailId == _Result.PID).FirstOrDefault();

        //                        PGTGradeDetail sData = (from t in _dbGrade.TGradeDetails
        //                                                where t.nGradeDetailId == _Result.PID
        //                                                select new
        //                                                {
        //                                                    t.nGradeDetailId,
        //                                                    t.nGradeId,
        //                                                    t.sID,
        //                                                    t.scoreMidTerm,
        //                                                    t.scoreFinalTerm,
        //                                                    t.getScore100,
        //                                                    t.getGradeLabel,
        //                                                    t.getBehaviorLabel,
        //                                                    t.getReadWrite,
        //                                                    t.getSpecial,
        //                                                    t.getBehaviorTotal,
        //                                                    t.getQuiz100,
        //                                                    t.getMid100,
        //                                                    t.getFinal100,
        //                                                    t.getSamattana,
        //                                                    t.SchoolID,
        //                                                    t.CreatedBy,
        //                                                    t.UpdatedBy,
        //                                                    t.CreatedDate,
        //                                                    t.UpdatedDate,
        //                                                    t.cDel,
        //                                                    t.scoreBeforeAfterMidTerm,
        //                                                    t.ScoreData,
        //                                                    t.ScoreBeforeMidTerm,
        //                                                    t.ScoreAfterMidTerm,
        //                                                    t.getReadWriteTotal,
        //                                                    t.getSamattanaTotal,
        //                                                    t.getBeforeQuiz100,
        //                                                    t.getAfterQuiz100,
        //                                                    t.nCredit,
        //                                                    t.CourseTotalHour,
        //                                                    t.PlanId,
        //                                                }
        //                             ).AsEnumerable().Select(x => new PGTGradeDetail
        //                             {
        //                                 nGradeDetailId = x.nGradeDetailId,
        //                                 nGradeId = x.nGradeId,
        //                                 sID = x.sID,
        //                                 scoreMidTerm = x.scoreMidTerm,
        //                                 scoreFinalTerm = x.scoreFinalTerm,
        //                                 getScore100 = x.getScore100,
        //                                 getGradeLabel = x.getGradeLabel,
        //                                 getBehaviorLabel = x.getBehaviorLabel,
        //                                 getReadWrite = x.getReadWrite,
        //                                 getSpecial = x.getSpecial,
        //                                 getBehaviorTotal = x.getBehaviorTotal,
        //                                 getQuiz100 = x.getQuiz100,
        //                                 getMid100 = x.getMid100,
        //                                 getFinal100 = x.getFinal100,
        //                                 getSamattana = x.getSamattana,
        //                                 SchoolID = x.SchoolID,
        //                                 CreatedBy = x.CreatedBy,
        //                                 UpdatedBy = x.UpdatedBy,
        //                                 CreatedDate = x.CreatedDate,
        //                                 UpdatedDate = x.UpdatedDate,
        //                                 cDel = x.cDel,
        //                                 scoreBeforeAfterMidTerm = x.scoreBeforeAfterMidTerm,
        //                                 ScoreData = x.ScoreData,
        //                                 ScoreBeforeMidTerm = x.ScoreBeforeMidTerm,
        //                                 ScoreAfterMidTerm = x.ScoreAfterMidTerm,
        //                                 getReadWriteTotal = x.getReadWriteTotal,
        //                                 getSamattanaTotal = x.getSamattanaTotal,
        //                                 getBeforeQuiz100 = x.getBeforeQuiz100,
        //                                 getAfterQuiz100 = x.getAfterQuiz100,
        //                                 nCredit = x.nCredit,
        //                                 CourseTotalHour = x.CourseTotalHour,
        //                                 PlanId = x.PlanId,

        //                             }).FirstOrDefault();

        //                        if (lData != null)
        //                        {
        //                            if (sData != null)
        //                            {
        //                                pgLiteDB.PGTGradeDetails.AddOrUpdate(sData);
        //                                pgLiteDB.SaveChanges();

        //                            }
        //                            else
        //                            {
        //                                pgLiteDB.PGTGradeDetails.Remove(lData);
        //                                pgLiteDB.SaveChanges();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (sData != null)
        //                            {

        //                                long PrimaryID = sData.nGradeDetailId;

        //                                pgLiteDB.PGTGradeDetails.AddOrUpdate(sData);
        //                                pgLiteDB.SaveChanges();

        //                                string sqlUpdate = "UPDATE PGTGradeDetail SET nGradeDetailId = " + PrimaryID + " WHERE  nGradeDetailId = " + sData.nGradeDetailId + " ";
        //                                pgLiteDB.Database.ExecuteSqlCommand(sqlUpdate);



        //                            }
        //                        }

        //                        _CHVerTGradeDetail = (long)_Result.ChVer;

        //                        _TableTrackingInfo.UpdatedID = (int)_CHVerTGradeDetail;

        //                        pgLiteDB.TableTrackingInfoes.AddOrUpdate(_TableTrackingInfo);
        //                        pgLiteDB.SaveChanges();

        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        System.Threading.Thread.Sleep(100); // 10 second

        //    }
        //}
        //static void Work_Sync_TGradeEdit(object sender, DoWorkEventArgs e)
        //{
        //    while (true)
        //    {
        //        try
        //        {

        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {
        //                TableTrackingInfo _TableTrackingInfo = pgLiteDB.TableTrackingInfoes.Where(x => x.TableName == "TGradeEdits").FirstOrDefault();

        //                _CHVerTGradeEdits = (long)_TableTrackingInfo.UpdatedID;

        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    _dbGrade.Database.CommandTimeout = 16000;

        //                    string sql = @"
        //                     SELECT 0 as ID,
        //                    ChVer = SYS_CHANGE_VERSION,
        //                    ChCrVer = SYS_CHANGE_CREATION_VERSION,
        //                    ChOp = SYS_CHANGE_OPERATION,
        //                    {1} as PID,'0' as sPID
        //                    FROM CHANGETABLE(CHANGES {0}, {2}) AS ChTbl
        //                    order by 1 ";

        //                    var trackingData = _dbGrade.Database.SqlQuery<TrackingData>(string.Format(sql, "TGradeEdits", "GradeEditsID", _CHVerTGradeEdits)).ToList().AsEnumerable()
        //         .Select(x => new TrackingData { PID = x.PID, ChVer = x.ChVer, ChCrVer = x.ChCrVer, ChOp = x.ChOp }).OrderBy(x => x.ChVer).ToList();

        //                    foreach (TrackingData _Result in trackingData)
        //                    {
        //                        var lData = pgLiteDB.PGTGradeEdits.Where(x => x.GradeEditsID == _Result.PID).FirstOrDefault();



        //                        PGTGradeEdit sData = (from t in _dbGrade.TGradeEdits
        //                                              where t.GradeEditsID == _Result.PID
        //                                              select new
        //                                              {
        //                                                  t.GradeEditsID,
        //                                                  t.GradeDetailID,
        //                                                  GradeCalculation = t.GradeCalculation == null ? 0 : t.GradeCalculation,
        //                                                  GradeSet = t.GradeSet == null ? 0 : t.GradeSet,
        //                                                  t.UserUpdate,
        //                                                  t.DateUpdate,
        //                                                  t.UseGradeSet,
        //                                                  t.SchoolID,
        //                                                  t.CreatedBy,
        //                                                  t.UpdatedBy,
        //                                                  CreatedDate = t.CreatedDate == null ? t.DateUpdate : t.CreatedDate,
        //                                                  UpdatedDate = t.UpdatedDate == null ? t.DateUpdate : t.UpdatedDate,
        //                                                  t.cDel,
        //                                                  t.scoreMidTerm,
        //                                                  t.scoreFinalTerm,
        //                                                  t.getScore100,
        //                                                  t.getMid100,
        //                                                  t.getFinal100,
        //                                                  t.getGradeLabel,
        //                                              }
        //                             ).AsEnumerable().Select(x => new PGTGradeEdit
        //                             {
        //                                 GradeEditsID = x.GradeEditsID,
        //                                 GradeDetailID = x.GradeDetailID,
        //                                 GradeCalculation = x.GradeCalculation,
        //                                 GradeSet = x.GradeSet,
        //                                 UserUpdate = x.UserUpdate,
        //                                 DateUpdate = x.DateUpdate,
        //                                 UseGradeSet = x.UseGradeSet,
        //                                 SchoolID = x.SchoolID,
        //                                 CreatedBy = x.CreatedBy,
        //                                 UpdatedBy = x.UpdatedBy,
        //                                 CreatedDate = x.CreatedDate,
        //                                 UpdatedDate = x.UpdatedDate,
        //                                 cDel = x.cDel,
        //                                 scoreMidTerm = x.scoreMidTerm,
        //                                 scoreFinalTerm = x.scoreFinalTerm,
        //                                 getScore100 = x.getScore100,
        //                                 getMid100 = x.getMid100,
        //                                 getFinal100 = x.getFinal100,
        //                                 getGradeLabel = x.getGradeLabel,

        //                             }).FirstOrDefault();

        //                        if (lData != null)
        //                        {
        //                            if (sData != null)
        //                            {
        //                                pgLiteDB.PGTGradeEdits.AddOrUpdate(sData);
        //                                pgLiteDB.SaveChanges();

        //                            }
        //                            else
        //                            {
        //                                pgLiteDB.PGTGradeEdits.Remove(lData);
        //                                pgLiteDB.SaveChanges();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (sData != null)
        //                            {

        //                                long PrimaryID = sData.GradeEditsID;

        //                                pgLiteDB.PGTGradeEdits.Add(sData);
        //                                pgLiteDB.SaveChanges();

        //                                string sqlUpdate = "UPDATE PGTGradeEdits SET GradeEditsID = " + PrimaryID + " WHERE  GradeEditsID = " + sData.GradeEditsID + " ";
        //                                pgLiteDB.Database.ExecuteSqlCommand(sqlUpdate);


        //                            }
        //                        }

        //                        _CHVerTGradeEdits = (long)_Result.ChVer;

        //                        _TableTrackingInfo.UpdatedID = (int)_CHVerTGradeEdits;

        //                        pgLiteDB.TableTrackingInfoes.AddOrUpdate(_TableTrackingInfo);
        //                        pgLiteDB.SaveChanges();

        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        System.Threading.Thread.Sleep(100); // 10 second

        //    }
        //}
        public static void update_Autocompletes()
        {
            TopupMoney.CreateData();
        }
        static void WorkAutoCompleteTopup_DoWork_LastOne(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {

                    DateTime _dtNow = DateTime.Now;
                    if (_dtNow.Hour == 3 && !bRefreshMemory)
                    {
                        TopupMoney.CreateData();
                        bRefreshMemory = true;
                    }

                    if (_dtNow.Hour == 4)
                    {
                        bRefreshMemory = false;
                    }

                    if (TopupMoney.Thread_0.Count() > 0)
                    {
                        TopupMoney.ThreadNumber = 1;


                        if (TopupMoney._delete_0.Count() > 0)
                        {
                            TopupMoney.topupMoney.RemoveAll(w => TopupMoney._delete_0.Contains(w.User_Id.ToUpper()));
                            TopupMoney._delete_0 = new List<string>();
                        }

                        var q = TopupMoney.InsertUserToMemory(TopupMoney.Thread_0);
                        if (q.Count > 0)
                        {
                            TopupMoney.topupMoney.AddRange(q);
                        }

                        TopupMoney.Thread_0 = new List<AC_TopupMoney>();


                    }

                    if (TopupMoney.Thread_1.Count() > 0)
                    {
                        TopupMoney.ThreadNumber = 2;

                        if (TopupMoney._delete_1.Count() > 0)
                        {

                            TopupMoney.topupMoney.RemoveAll(w => TopupMoney._delete_1.Contains(w.User_Id.ToUpper()));
                            TopupMoney._delete_1 = new List<string>();
                        }

                        var q = TopupMoney.InsertUserToMemory(TopupMoney.Thread_1);
                        if (q.Count > 0)
                        {
                            TopupMoney.topupMoney.AddRange(q);
                        }

                        TopupMoney.Thread_1 = new List<AC_TopupMoney>();



                    }



                    if (TopupMoney.Thread_2.Count() > 0)
                    {
                        TopupMoney.ThreadNumber = 0;

                        if (TopupMoney._delete_2.Count() > 0)
                        {

                            TopupMoney.topupMoney.RemoveAll(w => TopupMoney._delete_2.Contains(w.User_Id.ToUpper()));
                            TopupMoney._delete_2 = new List<string>();
                        }

                        var q = TopupMoney.InsertUserToMemory(TopupMoney.Thread_2);
                        if (q.Count > 0)
                        {
                            TopupMoney.topupMoney.AddRange(q);
                        }

                        TopupMoney.Thread_2 = new List<AC_TopupMoney>();
                    }

                    if (TopupMoney.Thread_Delete.Count() > 0)
                    {
                        foreach (var r in TopupMoney.Thread_Delete)
                        {
                            TopupMoney.topupMoney.RemoveAll(w => w.User_Id == r.User_Id && w.User_Type == r.User_Type);
                            TopupMoney.Thread_Delete.Remove(r);
                        }
                    }



                }
                catch (Exception ex)
                {
                    string logMessagePattern = @"[Function:Autocompletes], [ErrorLine:{2}], [ErrorMessage:{3}]";
                    string errorMessage = ex.Message;
                    string innerExceptionMessage = "";
                    while (ex.InnerException != null) { innerExceptionMessage += ", " + ex.InnerException.Message; ex = ex.InnerException; }
                    string logMessageDebug = string.Format(logMessagePattern, "", GetLineNumberError(ex), errorMessage + ", " + innerExceptionMessage);

                    //int? sEmpID = userData.UserID;

                    InsertLogDebug(null, null, null, logMessageDebug);

                    //TopupMoney.workingStatus = true;
                }
                System.Threading.Thread.Sleep(100); // 10 second
                //System.Threading.Thread.Sleep(10 * 1000); // 10 second
            }
        }
        static void WorkAutoCompleteTopup_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {

                    DateTime _dtNow = DateTime.Now;
                    if (_dtNow.Hour == 3 && !bRefreshMemory)
                    {
                        TopupMoney.CreateData();
                        bRefreshMemory = true;
                    }

                    if (_dtNow.Hour == 4)
                    {
                        bRefreshMemory = false;
                    }

                    if (JabjaiMainClass.Autocompletes.TopupMoney.workingStatus)
                    {
                        if (TopupMoney.Thread_Delete.Count() > 0)
                        {
                            foreach (var r in TopupMoney.Thread_Delete.ToList())
                            {
                                TopupMoney.topupMoney.RemoveAll(w => w.User_Id == r.User_Id && w.User_Type == r.User_Type);
                                TopupMoney.Thread_Delete.Remove(r);
                            }
                        }

                        if (TopupMoney.Thread_0.Count() > 0)
                        {
                            TopupMoney.ThreadNumber = 1;

                            List<AC_TopupMoney> AC_TopupMoneyProcess = TopupMoney.Thread_0.ToList();


                            TopupMoney.InsertUserCardDataToMemory(AC_TopupMoneyProcess);
                            if (TopupMoney.Thread_0.Count > AC_TopupMoneyProcess.Count)
                            {

                                try
                                {
                                    TopupMoney.Thread_0.RemoveAll(x => AC_TopupMoneyProcess.Contains(x));
                                }
                                catch
                                {
                                    TopupMoney.Thread_0 = new List<AC_TopupMoney>();
                                }
                            }
                            else
                            {
                                TopupMoney.Thread_0 = new List<AC_TopupMoney>();
                            }

                        }

                        if (TopupMoney.Thread_1.Count() > 0)
                        {
                            TopupMoney.ThreadNumber = 2;


                            List<AC_TopupMoney> AC_TopupMoneyProcess = TopupMoney.Thread_1.ToList();


                            TopupMoney.InsertUserCardDataToMemory(AC_TopupMoneyProcess);
                            if (TopupMoney.Thread_1.Count > AC_TopupMoneyProcess.Count)
                            {

                                try
                                {
                                    TopupMoney.Thread_1.RemoveAll(x => AC_TopupMoneyProcess.Contains(x));
                                }
                                catch
                                {
                                    TopupMoney.Thread_1 = new List<AC_TopupMoney>();
                                }
                            }
                            else
                            {
                                TopupMoney.Thread_1 = new List<AC_TopupMoney>();
                            }


                        }



                        if (TopupMoney.Thread_2.Count() > 0)
                        {
                            TopupMoney.ThreadNumber = 0;

                            List<AC_TopupMoney> AC_TopupMoneyProcess = TopupMoney.Thread_2.ToList();



                            TopupMoney.InsertUserCardDataToMemory(AC_TopupMoneyProcess);
                            if (TopupMoney.Thread_2.Count > AC_TopupMoneyProcess.Count)
                            {

                                try
                                {
                                    TopupMoney.Thread_2.RemoveAll(x => AC_TopupMoneyProcess.Contains(x));
                                }
                                catch
                                {
                                    TopupMoney.Thread_2 = new List<AC_TopupMoney>();
                                }
                            }
                            else
                            {
                                TopupMoney.Thread_2 = new List<AC_TopupMoney>();
                            }


                        }

                    }
                }
                catch (Exception ex)
                {
                    string logMessagePattern = @"[Function:Autocompletes], [ErrorLine:{2}], [ErrorMessage:{3}]";
                    string errorMessage = ex.Message;
                    string innerExceptionMessage = "";
                    while (ex.InnerException != null) { innerExceptionMessage += ", " + ex.InnerException.Message; ex = ex.InnerException; }
                    string logMessageDebug = string.Format(logMessagePattern, "", GetLineNumberError(ex), errorMessage + ", " + innerExceptionMessage);

                    //int? sEmpID = userData.UserID;

                    InsertLogDebug(null, null, null, logMessageDebug);

                    //TopupMoney.workingStatus = true;
                }
                System.Threading.Thread.Sleep(100); // 10 second
                //System.Threading.Thread.Sleep(10 * 1000); // 10 second
            }
        }

        static void workAutocompletes_DoWork_NotUsedBackup(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    //TopupMoney.workingStatus = false;

                    switch (TopupMoney.ThreadNumber)
                    {
                        case 0:
                            if (TopupMoney._delete_0.Count() > 0 || TopupMoney.Thread_0.Count() > 0)
                            {
                                if (TopupMoney._delete_0.Count() > 0)
                                {
                                    TopupMoney.topupMoney.RemoveAll(w => TopupMoney._delete_0.Contains(w.User_Id.ToUpper()));
                                    TopupMoney._delete_0 = new List<string>();
                                }

                                if (TopupMoney.Thread_0.Count() > 0)
                                {
                                    var q = TopupMoney.InsertUserToMemory(TopupMoney.Thread_0);
                                    TopupMoney.topupMoney.AddRange(q);
                                    TopupMoney.Thread_0 = new List<AC_TopupMoney>();
                                }

                                TopupMoney.ThreadNumber = 1;
                            }
                            break;
                        case 1:
                            if (TopupMoney._delete_1.Count() > 0 || TopupMoney.Thread_1.Count() > 0)
                            {
                                if (TopupMoney._delete_1.Count() > 0)
                                {
                                    TopupMoney.topupMoney.RemoveAll(w => TopupMoney._delete_1.Contains(w.User_Id.ToUpper()));
                                    TopupMoney._delete_1 = new List<string>();
                                }

                                if (TopupMoney.Thread_1.Count() > 0)
                                {
                                    var q = TopupMoney.InsertUserToMemory(TopupMoney.Thread_1);
                                    TopupMoney.topupMoney.AddRange(q);
                                    TopupMoney.Thread_1 = new List<AC_TopupMoney>();
                                }
                                TopupMoney.ThreadNumber = 2;
                            }
                            break;
                        case 2:
                            if (TopupMoney._delete_2.Count() > 0 || TopupMoney.Thread_2.Count() > 0)
                            {
                                if (TopupMoney._delete_2.Count() > 0)
                                {
                                    TopupMoney.topupMoney.RemoveAll(w => TopupMoney._delete_2.Contains(w.User_Id.ToUpper()));
                                    TopupMoney._delete_2 = new List<string>();
                                }

                                if (TopupMoney.Thread_2.Count() > 0)
                                {
                                    var q = TopupMoney.InsertUserToMemory(TopupMoney.Thread_2);
                                    TopupMoney.topupMoney.AddRange(q);
                                    TopupMoney.Thread_2 = new List<AC_TopupMoney>();
                                }
                                TopupMoney.ThreadNumber = 0;
                            }
                            break;
                    }

                    //TopupMoney.workingStatus = true;
                }
                catch (Exception ex)
                {
                    string logMessagePattern = @"[Function:Autocompletes], [ErrorLine:{2}], [ErrorMessage:{3}]";
                    string errorMessage = ex.Message;
                    string innerExceptionMessage = "";
                    while (ex.InnerException != null) { innerExceptionMessage += ", " + ex.InnerException.Message; ex = ex.InnerException; }
                    string logMessageDebug = string.Format(logMessagePattern, "", GetLineNumberError(ex), errorMessage + ", " + innerExceptionMessage);

                    //int? sEmpID = userData.UserID;

                    InsertLogDebug(null, null, null, logMessageDebug);

                    //TopupMoney.workingStatus = true;
                }

                System.Threading.Thread.Sleep(10 * 1000); // 10 second
            }
        }
        public static int GetLineNumberError(Exception ex)
        {
            var lineNumber = 0;
            const string lineSearch = ":line ";
            var index = ex.StackTrace.LastIndexOf(lineSearch);
            if (index != -1)
            {
                var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                if (int.TryParse(lineNumberText, out lineNumber))
                {
                }
            }
            return lineNumber;
        }

        public static void InsertLogDebug(int? schoolID, int? studentID, int? employeeID, string logMessage)
        {
            try
            {
                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    dbMaster.TLogDebugs.Add(new TLogDebug
                    {
                        SchoolID = schoolID,
                        StudentID = studentID,
                        EmployeeID = employeeID,
                        LogMessage = logMessage,
                        LogDate = DateTime.Now,
                        //IP = GetIPAddress()
                    });

                    dbMaster.SaveChanges();
                }

                if (logMessage.ToLower().Contains("object reference not set to an instance of an object"))
                {
                    // TopupMoney.CreateData();

                    using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        dbMaster.TLogDebugs.Add(new TLogDebug
                        {
                            SchoolID = schoolID,
                            StudentID = studentID,
                            EmployeeID = employeeID,
                            LogMessage = "Auto Refresh Data due to object reference not set to an instance of an object",
                            LogDate = DateTime.Now,

                        });

                        dbMaster.SaveChanges();
                    }
                }
            }
            catch { }
        }

        static void work_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                LogStatus.LogDetail logDetail = new LogStatus.LogDetail();
                List<LogStatus> logStatuses = new List<LogStatus>();
                LogStatus logStatus = new LogStatus();
                logStatus.details = new List<LogStatus.LogDetail>();

                try
                {
                    try
                    {
                        if (TopupMoney.dateTime != DateTime.Today)
                        {
                            //LINENotify notify = new LINENotify();
                            //notify.LineSBErrorSend(new LINENotifyDATA
                            //{
                            //    //Parameter = topup,
                            //    Date_Time = DateTime.Now,
                            //    URL = urlHost,
                            //    Error_Method = $"UPDATE WEB MEMORY [{ConfigurationManager.AppSettings["XRayWebApp"].ToString()}]"
                            //});
                            TopupMoney.CreateData();
                        }
                    }
                    catch
                    {

                    }

                    logDetail = new LogStatus.LogDetail();
                    logDetail.LogName = "SystemLog";
                    logDetail.Rows = database._tSystemLog.Count;

                    if (database._tSystemLog.Count > 0)
                    {
                        List<JabjaiEntity.DB.TSystemlog> _ProcessSystemLog = new List<JabjaiEntity.DB.TSystemlog>();
                        _ProcessSystemLog.AddRange(database._tSystemLog.ToList());

                        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGDBForLogs)
                        {
                            using (var PGJabjaiLogs = new PostgreSQLLogs.JabjaiSchoolLogsContainer())
                            {

                                var PGSystemLogs = _ProcessSystemLog.Select(x => new PostgreSQLLogs.TSystemlog
                                {
                                    sEmp = x.sEmp,
                                    dLog = x.dLog,
                                    sLog = x.sLog,
                                    nSession = x.nSession,
                                    nMenu = x.nMenu,
                                    action = x.action,
                                    systemtype = x.systemtype,
                                    ip = x.ip,
                                    SchoolID = x.SchoolID,
                                    CreatedBy = x.CreatedBy,
                                    CreatedDate = x.CreatedDate,
                                    UpdatedBy = x.UpdatedBy,
                                    UpdatedDate = x.UpdatedDate,
                                    cDel = x.cDel

                                }).ToList();



                                PGJabjaiLogs.TSystemlogs.AddRange(PGSystemLogs);
                                PGJabjaiLogs.SaveChanges();
                            }
                        }
                        else
                        {
                            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
                            {
                                schoolDbContext.BulkInsert<JabjaiEntity.DB.TSystemlog>(_ProcessSystemLog);
                                schoolDbContext.SaveChanges();
                            }
                        }

                        database._tSystemLog.RemoveAll(x => _ProcessSystemLog.Contains(x));
                        _ProcessSystemLog.Clear();
                    }

                    logDetail.Status = "Success";
                }
                catch (Exception e1)
                {

                    logDetail.Status = "Fail";
                    logDetail.ErrorMessage = fcommon.ExceptionMessage(e1);
                }

                try
                {
                    string physicalPath = HostingEnvironment.ApplicationPhysicalPath;
                    string path = physicalPath + $"\\Log\\{DateTime.Today.ToString("yyyyMMdd")}\\{DateTime.Now.ToString("yyyyMMdd_HH")}.json";
                    Directory.CreateDirectory(physicalPath + $"\\Log\\{DateTime.Today.ToString("yyyyMMdd")}");


                    if (File.Exists(path))
                    {
                        string _log = File.ReadAllText(path);
                        logStatuses = JsonConvert.DeserializeObject<List<LogStatus>>(_log);
                    }

                    try
                    {

                        logDetail = new LogStatus.LogDetail();
                        logDetail.LogName = "Send_API_Logs";
                        logDetail.Rows = InsertLogAPI._Send_API_Logs.Count;

                        if (InsertLogAPI._Send_API_Logs.Count > 0)
                        {
                            //    List<MasterEntity.TB_Send_API_Log> _ProcessTB_Send_API_Log = new List<MasterEntity.TB_Send_API_Log>();
                            //    _ProcessTB_Send_API_Log.AddRange(InsertLogAPI._Send_API_Logs);
                            //    if (SchoolBright.DataAccess.DataAccessHelper.bUsePGDBForLogs)
                            //    {
                            //        using (var PGJabjaiLogs = new PostgreSQLLogs.JabjaiSchoolLogsContainer())
                            //        {
                            //            var PGSend_API_Log = (from a in _ProcessTB_Send_API_Log
                            //                                  select new PostgreSQLLogs.TB_Send_API_Log
                            //                                  {
                            //                                      Api_Name = a.Api_Name,
                            //                                      ID = a.ID,
                            //                                      Info = a.Info,
                            //                                      ResponseTime = a.ResponseTime,
                            //                                      Result = a.Result,
                            //                                      SchoolID = a.SchoolID,
                            //                                      //Tstamp = DateTime.Now,
                            //                                  }).ToList();

                            //            using (var dbContext = new JabjaiSchoolLogsBulkInsert())
                            //            {
                            //                dbContext.Configuration.AutoDetectChangesEnabled = false; // Disable change tracking for better performance

                            //                // Bulk insert entities
                            //                dbContext.BulkInsert(PGSend_API_Log);

                            //                // Save changes to the database
                            //                dbContext.SaveChanges();
                            //            }
                            //        }
                            //    }
                            //    else
                            //    {

                            //    }

                            InsertLogAPI._Send_API_Logs.Clear();
                        }
                        //logDetail.Status = "Success";
                    }
                    catch (Exception e1)
                    {

                        logDetail.Status = "Fail";
                        logDetail.ErrorMessage = fcommon.ExceptionMessage(e1);
                    }

                    logStatus.details.Add(logDetail);

                    logStatus.details.Add(logDetail);

                    logStatus.LogTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    logStatuses.Add(logStatus);

                    if (SystemLog)
                    {
                        using (StreamWriter writer = new StreamWriter(path))
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            string sLog = JsonConvert.SerializeObject(logStatuses.OrderByDescending(o => o.LogTime).ToArray(), MyFormatting);

                            writer.Write(sLog);
                        }
                    }
                }
                catch
                {

                }


                Thread.Sleep(1000 * 30);

            }
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiConfig.UrlPrefixRelative);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            // Code that runs when an unhandled error occurs
            //Exception ex = Server.GetLastError();
            //Server.ClearError();
            //Server.Transfer("AdminMain.aspx");
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            string path = "N/A";
            if (sender is HttpApplication)
                path = ((HttpApplication)sender).Request.Url.PathAndQuery;

            Exception baseException = Server.GetLastError().GetBaseException();

            // Code that runs when an unhandled error occurs
            ServiceHelper.CreateApplicationExceptionLog(exc, path, baseException);
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            //if (Session["sEntities"] == null)
            //{
            //    Response.Redirect("~/Default.aspx");
            //}
        }

        void Session_End(object sender, EventArgs e)
        {
            //if (HttpContext.Current != null)
            //{
            //    HttpContext.Current.Session.Clear();
            //    HttpContext.Current.Session.Abandon();
            //    string[] allDomainCookes = HttpContext.Current.Request.Cookies.AllKeys;

            //    foreach (string domainCookie in allDomainCookes)
            //    {
            //        if (domainCookie.Contains("ASPXAUTH"))
            //        {
            //            var expiredCookie = new HttpCookie(domainCookie)
            //            {
            //                Expires = DateTime.Now.AddDays(-1),
            //            };
            //            HttpContext.Current.Response.Cookies.Add(expiredCookie);
            //        }
            //    }
            //    HttpContext.Current.Request.Cookies.Clear();
            //}
            //JWTToken token = new JWTToken();
            //if (!token.CheckToken(HttpContext.Current)) { Response.Redirect("Default.aspx"); }
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }

        public class LogStatus
        {
            public string LogTime { get; set; }
            public List<LogDetail> details { get; set; }
            public class LogDetail
            {
                public string LogName { get; set; }
                public int Rows { get; set; }
                public string Status { get; set; }
                public string ErrorMessage { get; set; }
            }
        }
    }
}
