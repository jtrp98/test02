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
    /// Summary description for studentList
    /// </summary>
    public class RestoreItem : IHttpHandler, IRequiresSessionState
    {

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            string type = fcommon.ReplaceInjection(context.Request["type"]);
            string date1 = fcommon.ReplaceInjection(context.Request["date1"]);
            string date2 = fcommon.ReplaceInjection(context.Request["date2"]);
            string name = fcommon.ReplaceInjection(context.Request["name"]);

            DateTime oDate1, oDate2;
            var isValid1 = DateTime.TryParseExact(date1, "yyyyMMdd", new CultureInfo("th-TH"), DateTimeStyles.None, out oDate1);
            var isValid2 = DateTime.TryParseExact(date2, "yyyyMMdd", new CultureInfo("th-TH"), DateTimeStyles.None, out oDate2);

            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (var _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var SQL = $@"SELECT 1 'Type' , A.sID , 'ข้อมูลนักเรียน' 'Title' 
, A.sStudentID + ' ' + A.sName + ' ' + A.sLastname + ' ' + C.SubLevel + '/' + B.nTSubLevel2  'Detail' 
, A.dUpdate 'Date' , E.sName + ' ' + E.sLastname 'Editor'--, *
  FROM [JabjaiSchoolSingleDB].[dbo].[TUser] A
  LEFT JOIN [JabjaiSchoolSingleDB].[dbo].TTermSubLevel2 B ON A.nTermSubLevel2 = B.nTermSubLevel2 AND A.SchoolID = B.SchoolID
  LEFT JOIN [JabjaiSchoolSingleDB].[dbo].TSubLevel C ON B.nTSubLevel = C.nTSubLevel AND B.SchoolID = C.SchoolID 
  LEFT JOIN JabjaiMasterSingleDB.dbo.TUser E ON E.sID = A.UpdatedBy AND A.SchoolID = E.nCompany

  WHERE A.SchoolID = {userData.CompanyID} AND A.cDel = '1' AND A.dUpdate between '{oDate1.ToString("yyyyMMdd")}' 
AND '{oDate2.ToString("yyyyMMdd")} 23:59' 
{(string.IsNullOrEmpty(name) ? "" : $"AND ( REPLACE(A.sName+A.sLastname, ' ', '') like '%{name.Replace(" ", "")}%' OR REPLACE(A.sStudentID, ' ', '') like '%{name.Replace(" ", "")}%')" )} ";

                var lst = _dbMaster.Database.SqlQuery<ModelItem>(SQL).ToList();

                context.Response.Expires = -1;
                //context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";

                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    data = lst
                      .OrderByDescending(o => o.Date)
                      .Select((o, i) => new
                      {
                          type = o.Type,
                          index = i + 1,
                          sid = o.sID,
                          title = o.Title,
                          detail = o.Detail,
                          date = o.Date.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")),
                          editor = o.Editor,
                          customCol = "",
                      })

                }));

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



        protected class stdDetail
        {

            public int sID { get; set; }
            public string fullName { get; set; }
            public string firstName { get; set; }
            public string FirstandLast { get; set; }
        }

        private class ModelItem
        {
            public int Type { get; set; }
            public int sID { get; set; }
            public string Title { get; set; }
            public string Detail { get; set; }
            public DateTime Date { get; set; }
            public string Editor{ get; set; }
        }
    }


}