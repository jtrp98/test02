using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiSchoolGradeEntity;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for bp5ImportScore
    /// </summary>
    public class bp5ImportScore : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade());
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

            dynamic rss = new JObject();
            string userterm = fcommon.ReplaceInjection(context.Request["term"]);
            string year = fcommon.ReplaceInjection(context.Request["year"]);
            string planid = fcommon.ReplaceInjection(context.Request["id"]);
            string mode = fcommon.ReplaceInjection(context.Request["mode"]);
            string idlv2 = fcommon.ReplaceInjection(context.Request["idlv2"]);
            int idlv2n = int.Parse(idlv2);

            int? useryear = Int32.Parse(year);
            int nyear = 0;
            string nterm = "";

            foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear && userData.CompanyID == w.SchoolID))
            {
                nyear = ff.nYear;
            }

            foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && userData.CompanyID == w.SchoolID && w.cDel == null))
            {
                nterm = ee.nTerm;
            }

            var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
            

            if (mode == "1")
            {

                bp5copybehaveMax bp5copybehaveMax = new bp5copybehaveMax();
                List<bp5copybehaveMax> bp5copybehaveMaxList = new List<bp5copybehaveMax>();

                foreach (var data2 in _dbGrade.TB_GradeViews.Where(w => w.sPlaneID.ToString() == planid && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID))
                {

                    bp5copybehaveMax = new bp5copybehaveMax();
                    bp5copybehaveMax.behave1max = data2.maxBehavior1;
                    bp5copybehaveMax.behave2max = data2.maxBehavior2;
                    bp5copybehaveMax.behave3max = data2.maxBehavior3;
                    bp5copybehaveMax.behave4max = data2.maxBehavior4;
                    bp5copybehaveMax.behave5max = data2.maxBehavior5;
                    bp5copybehaveMax.behave6max = data2.maxBehavior6;
                    bp5copybehaveMax.behave7max = data2.maxBehavior7;
                    bp5copybehaveMax.behave8max = data2.maxBehavior8;
                    bp5copybehaveMax.behave9max = data2.maxBehavior9;
                    bp5copybehaveMax.behave10max = data2.maxBehavior10;
                    bp5copybehaveMax.behave1name = data2.nameBehavior1;
                    bp5copybehaveMax.behave2name = data2.nameBehavior2;
                    bp5copybehaveMax.behave3name = data2.nameBehavior3;
                    bp5copybehaveMax.behave4name = data2.nameBehavior4;
                    bp5copybehaveMax.behave5name = data2.nameBehavior5;
                    bp5copybehaveMax.behave6name = data2.nameBehavior6;
                    bp5copybehaveMax.behave7name = data2.nameBehavior7;
                    bp5copybehaveMax.behave8name = data2.nameBehavior8;
                    bp5copybehaveMax.behave9name = data2.nameBehavior9;
                    bp5copybehaveMax.behave10name = data2.nameBehavior10;
                    bp5copybehaveMaxList.Add(bp5copybehaveMax);
                }



                rss = new JArray(from a in bp5copybehaveMaxList
                                 select new JObject(
                       new JProperty("behave1max", a.behave1max),
                       new JProperty("behave2max", a.behave2max),
                       new JProperty("behave3max", a.behave3max),
                       new JProperty("behave4max", a.behave4max),
                       new JProperty("behave5max", a.behave5max),
                       new JProperty("behave6max", a.behave6max),
                       new JProperty("behave7max", a.behave7max),
                       new JProperty("behave8max", a.behave8max),
                       new JProperty("behave9max", a.behave9max),
                       new JProperty("behave10max", a.behave10max),
                       new JProperty("behave1name", a.behave1name),
                       new JProperty("behave2name", a.behave2name),
                       new JProperty("behave3name", a.behave3name),
                       new JProperty("behave4name", a.behave4name),
                       new JProperty("behave5name", a.behave5name),
                       new JProperty("behave6name", a.behave6name),
                       new JProperty("behave7name", a.behave7name),
                       new JProperty("behave8name", a.behave8name),
                       new JProperty("behave9name", a.behave9name),
                       new JProperty("behave10name", a.behave10name)

                    ));
            }
            else if (mode == "2")
            {
                bp5copybehave bp5copybehave = new bp5copybehave();
                List<bp5copybehave> bp5copybehaveList = new List<bp5copybehave>();


                var data = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == planid && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
                
                foreach (var data2 in _dbGrade.GetGradeDetailView(userData.CompanyID, data.nGradeId, 0))
                {
                    bp5copybehave = new bp5copybehave();
                    bp5copybehave.sID = data2.sID.ToString();
                    bp5copybehave.behave1 = data2.scoreBehavior1;
                    bp5copybehave.behave2 = data2.scoreBehavior2;
                    bp5copybehave.behave3 = data2.scoreBehavior3;
                    bp5copybehave.behave4 = data2.scoreBehavior4;
                    bp5copybehave.behave5 = data2.scoreBehavior5;
                    bp5copybehave.behave6 = data2.scoreBehavior6;
                    bp5copybehave.behave7 = data2.scoreBehavior7;
                    bp5copybehave.behave8 = data2.scoreBehavior8;
                    bp5copybehave.behave9 = data2.scoreBehavior9;
                    bp5copybehave.behave10 = data2.scoreBehavior10;
                    bp5copybehave.behaveTotal = data2.getBehaviorLabel;
                    bp5copybehaveList.Add(bp5copybehave);

                }

                rss = new JArray(from a in bp5copybehaveList
                                 select new JObject(
                       new JProperty("sID", a.sID),
                       new JProperty("behave1", a.behave1),
                       new JProperty("behave2", a.behave2),
                       new JProperty("behave3", a.behave3),
                       new JProperty("behave4", a.behave4),
                       new JProperty("behave5", a.behave5),
                       new JProperty("behave6", a.behave6),
                       new JProperty("behave7", a.behave7),
                       new JProperty("behave8", a.behave8),
                       new JProperty("behave9", a.behave9),
                       new JProperty("behave10", a.behave10),
                       new JProperty("behaveTotal", a.behaveTotal)

                    ));
            }
            else if (mode == "3")
            {
                bp5copyreading bp5copyreading = new bp5copyreading();
                List<bp5copyreading> bp5copyreadingList = new List<bp5copyreading>();

                var data = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == planid && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
                foreach (var data2 in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == data.nGradeId))
                {
                    if (data2.cDel == false)
                    {

                        bp5copyreading = new bp5copyreading();
                        bp5copyreading.sID = data2.sID.ToString();
                        bp5copyreading.readingTotal = data2.getReadWrite;
                        bp5copyreadingList.Add(bp5copyreading);
                    }
                }

                rss = new JArray(from a in bp5copyreadingList
                                 select new JObject(
                       new JProperty("sID", a.sID),                       
                       new JProperty("readingTotal", a.readingTotal)

                    ));
            }
            else if (mode == "4")
            {
                bp5copysamattana bp5copysamattana = new bp5copysamattana();
                List<bp5copysamattana> bp5copysamattanaList = new List<bp5copysamattana>();

                var data = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == planid && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
                foreach (var data2 in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == data.nGradeId))
                {
                    if (data2.cDel == false)
                    {
                        bp5copysamattana = new bp5copysamattana();
                        bp5copysamattana.sID = data2.sID.ToString();
                        bp5copysamattana.samattanaTotal = data2.getSamattana;
                        bp5copysamattanaList.Add(bp5copysamattana);
                    }
                }

                rss = new JArray(from a in bp5copysamattanaList
                                 select new JObject(
                       new JProperty("sID", a.sID),
                       new JProperty("samattanaTotal", a.samattanaTotal)

                    ));
            }


            context.Response.Expires = -1;
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            //context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(rss);
            context.Response.End();
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    class bp5copybehave
    {
        public string sID { get; set; }
        public string behave1 { get; set; }
        public string behave2 { get; set; }
        public string behave3 { get; set; }
        public string behave4 { get; set; }
        public string behave5 { get; set; }
        public string behave6 { get; set; }
        public string behave7 { get; set; }
        public string behave8 { get; set; }
        public string behave9 { get; set; }
        public string behave10 { get; set; }
        public string behaveTotal { get; set; }

    }

    class bp5copybehaveMax
    {
        public string behave1max { get; set; }
        public string behave2max { get; set; }
        public string behave3max { get; set; }
        public string behave4max { get; set; }
        public string behave5max { get; set; }
        public string behave6max { get; set; }
        public string behave7max { get; set; }
        public string behave8max { get; set; }
        public string behave9max { get; set; }
        public string behave10max { get; set; }
        public string behave1name { get; set; }
        public string behave2name { get; set; }
        public string behave3name { get; set; }
        public string behave4name { get; set; }
        public string behave5name { get; set; }
        public string behave6name { get; set; }
        public string behave7name { get; set; }
        public string behave8name { get; set; }
        public string behave9name { get; set; }
        public string behave10name { get; set; }

    }

    class bp5copyreading
    {
        public string sID { get; set; }        
        public string readingTotal { get; set; }

    }
    class bp5copysamattana
    {
        public string sID { get; set; }
        public string samattanaTotal { get; set; }

    }
}