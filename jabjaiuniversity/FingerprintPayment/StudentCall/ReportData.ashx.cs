using FingerprintPayment.ClassOnline.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using static FingerprintPayment.App_Code.StdCallingHub;

namespace FingerprintPayment.StudentCall
{
    /// <summary>
    /// Summary description for UploadFileBG
    /// </summary>
    public class ReportData : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }

            var date = context.Request.QueryString["date"] + "";
            var status = int.Parse(context.Request.QueryString["status"] + "");
            var lvl1 = int.Parse(context.Request.QueryString["lvl1"] + "");
            var lvl2 = int.Parse(context.Request.QueryString["lvl2"] + "");
            var name = context.Request.QueryString["name"] + "";
            var schoolId = userData.CompanyID;
            var toDay = DateTime.Now.Date;
                     
            string sql = $@"-- StudentCall/Report
SELECT A.SchoolID,A.sID,C.Base64Sound 'Sound'
, A.sNickName 'NickName' , ISNULL(F.titleDescription,A.sStudentTitle) 'Title', A.sName 'FirstName' , A.sLastname  'LastName'
, E.fullName 'Level1', D.nTSubLevel2 'Level2' 
, A.sStudentID 'Code' , A.sStudentPicture 'Img'
--, CASE WHEN B.Status = 1 THEN B.Created WHEN B.Status = 2 THEN B.Announced WHEN B.Status = 3 THEN B.Completed END 'tTime'
, B.Created 'tTime1', B.Announced 'tTime2' , B.Completed 'tTime3'
, B.Status
--, G.ParentName 'Receiver' 

FROM JabjaiSchoolSingleDB.dbo.TUser A
INNER JOIN JabjaiSchoolSingleDB.dbo.TStudentClassroomHistory G ON A.SchoolID = G.SchoolID AND A.sID = G.sID 
INNER JOIN JabjaiMasterSingleDB.dbo.TStudentCall B on A.SchoolID = B.SchoolID and A.sID = B.sID 
LEFT JOIN JabjaiMasterSingleDB.dbo.TSound_Student C on A.SchoolID = C.SchoolID and A.sID = C.sID and A.nTermSubLevel2 = C.nTermSubLevel2
INNER JOIN JabjaiSchoolSingleDB.dbo.TTermSubLevel2 D on G.SchoolID = D.SchoolID and G.nTermSubLevel2 = D.nTermSubLevel2
INNER JOIN JabjaiSchoolSingleDB.dbo.TSubLevel E on D.SchoolID = E.SchoolID and D.nTSubLevel = E.nTSubLevel
LEFT JOIN  JabjaiSchoolSingleDB.dbo.TTitleList F on A.SchoolID = F.SchoolID and A.sStudentTitle   = CAST( F.nTitleid as nvarchar)

WHERE A.SchoolID = {schoolId} and G.cDel = 0 "; 

            //and B.CallDate = '{_date.ToString("yyyyMMdd")}'
            DateTime _date;

            if (DateTime.TryParseExact(date, "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out _date))
            {
                sql += " AND B.CallDate = '" + _date.ToString("yyyyMMdd") + "'";
            }
            else
            {
                _date = DateTime.Now;
            }

            if (status != 0)
            {
                sql += " AND B.Status = " + status;
            }

            if (lvl1 != 0)
            {
                sql += " AND E.nTSubLevel = " + lvl1;
            }


            if (lvl2 != 0)
            {
                sql += " AND G.nTermSubLevel2 = " + lvl2;
            }

            if (!string.IsNullOrEmpty(name))
            {
                sql += $" AND REPLACE(A.sStudentID + A.sName  + A.sLastname, ' ' , '') like '%{name.Replace(" ", "")}%' ";
            }


            using (var _ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolId,ConnectionDB.Read)))
            {
                var sl = new StudentLogic(_ctx);
                var nterm = sl.GetTermId(_date, new JWTToken.userData { CompanyID = schoolId });

                sql += $" AND G.nTerm = '{nterm}' ";
            }

            using (var dbmaster = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))
            {
                var lst = dbmaster.Database.SqlQuery<ModelStudent>(sql).ToList();

                //foreach (var i in lst)
                //{
                ////    i.Level1 = i.Level1.Replace("ศึกษาปีที่", "")
                ////     .Replace("ประกาศนียบัตรวิชาชีพชั้นสูง", "ป ว ส")
                ////     .Replace("ประกาศนียบัตรวิชาชีพ", "ป ว ช");
                //    i.FullName = $"{i.Title} {i.FirstName} {i.LastName} ";
                //    i.Level = $"{i.Level1}/{i.Level2}";
                //    i.Time = i.tTime.ToString(@"hh\:mm\:ss");

                //}

                //return new
                //{
                //    status1 = lst.Where(o => o.Status == 1).OrderByDescending(o => o.Time),
                //    status2 = lst.Where(o => o.Status == 2).OrderByDescending(o => o.Time),
                //    status3 = lst.Where(o => o.Status == 3).OrderByDescending(o => o.Time),
                //};

                var d = _date.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

                //return new
                //{
                //    data = lst.Select((o, i) => new
                //    {
                //        no = i + 1,
                //        level = $"{o.Level1}/{o.Level2}",
                //        code = o.Code,
                //        title = o.Title,
                //        fullName = $"{o.FirstName} {o.LastName}".Trim(),
                //        status = o.Status == 1 ? "รอประกาศ" : (o.Status == 2 ? "ประกาศแล้ว" : "รับแล้ว"),
                //        date1 = $"{_date}<br/>{o.tTime.ToString(@"hh\:mm\:ss")}",
                //        //date2 = $"{_date}<br/>{o.tTime.ToString(@"hh\:mm\:ss")}",
                //        isAnnouce = _date.Date == toDay.Date ? 1 : 0,
                //    })
                //};

                context.Response.ContentType = "application/json";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    data = lst
                     .OrderBy(o => o.tTime1)
                     .Select((o, i) => new
                     {
                         sid = o.sID,
                         no = i + 1,
                         level = $"{o.Level1}/{o.Level2}",
                         code = o.Code,
                         title = o.Title,
                         fullName = $"{o.FirstName} {o.LastName}".Trim(),
                         status = o.Status + "",// == 1 ? "รอประกาศ" : (o.Status == 2 ? "ประกาศแล้ว" : "รับแล้ว"),
                         date0 = d,
                         date1 = o.tTime1.HasValue ? o.tTime1?.ToString(@"hh\:mm\:ss") : "-",
                         date2 = o.tTime2.HasValue ? o.tTime2?.ToString(@"hh\:mm\:ss") : "-",
                         date3 = o.tTime3.HasValue ? o.tTime3?.ToString(@"hh\:mm\:ss") : "-",
                         //date1 = $"{d}&nbsp;<br/>{(o.tTime2 ?? o.tTime1)?.ToString(@"hh\:mm\:ss")}",//: (o.Status == 2 ? $"{d}<br/>{o.tTime2.ToString(@"hh\:mm\:ss")}" : ""),
                         //date2 = o.Status == 3 ? $"{d}&nbsp;<br/>{o.tTime3?.ToString(@"hh\:mm\:ss")}" : "",
                         isAnnouce = _date.Date == toDay.Date ? "1" : "0",
                     })

                }));
            }



        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
//if (context.Request.Files.Count > 0)
//{
//    HttpFileCollection files = context.Request.Files;
//    for (int i = 0; i < files.Count; i++)
//    {
//        HttpPostedFile file = files[i];
//        string fname = context.Server.MapPath("~/uploads/" + file.FileName);
//        file.SaveAs(fname);
//    }
//}
