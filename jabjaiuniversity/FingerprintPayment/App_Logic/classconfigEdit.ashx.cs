using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Web.DynamicData;
using System.Drawing;
using System.IO;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for classconfigEdit
    /// </summary>
    public class classconfigEdit : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
               


                    string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                dynamic rss = new JObject();
                string mode = fcommon.ReplaceInjection(context.Request["mode"]);
                string id = fcommon.ReplaceInjection(context.Request["id"]);

                var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int? idn = Int32.Parse(id);

                if (mode == "sublv")
                {
                    var data = _db.TSubLevels.Where(w => w.nTSubLevel == idn && w.SchoolID == userData.CompanyID).FirstOrDefault();
                    var TLevel = _db.TLevels.Where(w => w.LevelID == data.nTLevel && w.SchoolID == userData.CompanyID).FirstOrDefault();

                    //TSubLevel sublv = new TSubLevel();
                    //List<TSubLevel> sublvList = new List<TSubLevel>();

                    NnTSubLevel nnTSubLevel = new NnTSubLevel();
                    List<NnTSubLevel> nnTSubLevels = new List<NnTSubLevel>();

                    nnTSubLevel.fullName = data.fullName;
                    nnTSubLevel.nDeleted = data.nDeleted;
                    nnTSubLevel.nTLevel = data.nTLevel;
                    nnTSubLevel.nTSubLevel = data.nTSubLevel;
                    nnTSubLevel.SubLevel = data.SubLevel;
                    nnTSubLevel.SubLevelEN = data.SubLevelEN;
                    nnTSubLevel.fullNameEN = data.fullNameEN;
                    nnTSubLevel.TLevelName = TLevel.LevelName;
                    nnTSubLevels.Add(nnTSubLevel);

                    //sublv.fullName = data.fullName;
                    //sublv.nDeleted = data.nDeleted;
                    //sublv.nTLevel = data.nTLevel;
                    //sublv.nTSubLevel = data.nTSubLevel;
                    //sublv.SubLevel = data.SubLevel;
                    //sublv.SubLevelEN = data.SubLevelEN;
                    //sublv.fullNameEN = data.fullNameEN;
                    //sublvList.Add(sublv);

                    rss = new JArray(from a in nnTSubLevels
                                     select new JObject(
                                         new JProperty("fullName", a.fullName),
                                         new JProperty("nDeleted", a.nDeleted),
                                         new JProperty("nTLevel", a.nTLevel),
                                         new JProperty("nTSubLevel", a.nTSubLevel),
                                         new JProperty("SubLevelEN", a.SubLevelEN),
                                         new JProperty("fullNameEN", a.fullNameEN),
                                         new JProperty("SubLevel", a.SubLevel),
                                         new JProperty("TLevelName", a.TLevelName)
                                         ));
                }
                else
                {
                    var data = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idn && w.SchoolID == userData.CompanyID).FirstOrDefault();

                    deletemodal tterm = new deletemodal();
                    List<deletemodal> ttermList = new List<deletemodal>();

                    tterm.nTermSubLevel2 = data.nTermSubLevel2;
                    tterm.nTSubLevel = data.nTSubLevel;
                    tterm.nTSubLevel2 = data.nTSubLevel2;
                    tterm.branchSpecId = data.nBranchSpecId;
                    var data2 = _db.TSubLevels.Where(w => w.nTSubLevel == data.nTSubLevel && w.SchoolID == userData.CompanyID).FirstOrDefault();
                    var TLevel = _db.TLevels.Where(w => w.LevelID == data2.nTLevel && w.SchoolID == userData.CompanyID).FirstOrDefault();
                    tterm.nTLevel = data2.nTLevel.ToString();
                    tterm.fullname = data2.fullName;
                    tterm.time = data.nTimeType.ToString();
                    tterm.TLevelName = TLevel.LevelName;
                    ttermList.Add(tterm);

                    rss = new JArray(from a in ttermList
                                     select new JObject(
                                         new JProperty("nTermSubLevel2", a.nTermSubLevel2),
                                         new JProperty("nTSubLevel", a.nTSubLevel),
                                         new JProperty("fullName", a.fullname),
                                         new JProperty("nTLevel", a.nTLevel),
                                         new JProperty("time", a.time),
                                         new JProperty("branchSpecId", a.branchSpecId),
                                         new JProperty("nTSubLevel2", a.nTSubLevel2),
                                         new JProperty("TLevelName", a.TLevelName)
                                         ));
                }
                context.Response.Expires = -1;
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";
                //context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(rss);
                context.Response.End();
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        class deletemodal
        {
            public int nTermSubLevel2 { get; set; }
            public int nTSubLevel { get; set; }
            public string time { get; set; }
            public string nTSubLevel2 { get; set; }
            public string fullname { get; set; }
            public string nTLevel { get; set; }
            public string TLevelName { get; set; }
            public int? branchSpecId { get; set; }
        }


        class NnTSubLevel
        {
            public int nTSubLevel { get; set; }
            public string fullName { get; set; }
            public Nullable<int> nDeleted { get; set; }
            public Nullable<int> nTLevel { get; set; }
            public string SubLevel { get; set; }
            public string SubLevelEN { get; set; }
            public string fullNameEN { get; set; }
            public string TLevelName { get; set; }
        }




    }


}