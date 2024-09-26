using FingerprintPayment.Card.CsCode;
using FingerprintPayment.Helper;
using FingerprintPayment.StudentCall.Handler;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using OfficeOpenXml.ConditionalFormatting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Card.PermissionCard
{
    public partial class Preview : CardGateway
    {
        public class FormModel
        {
            public string Logo { get; set; }
            public string School { get; set; }

            public string Date { get; set; }
            public string RefNo { get; set; }
            public string FullName { get; set; }
            public string Type { get; set; }
            public string Cause { get; set; }
            public string Date1 { get; set; }
            public string Date2 { get; set; }

            public string Teacher1 { get; set; }
            public string Teacher2 { get; set; }

        }

        public FormModel FormData { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var userData = GetUserData();

                FormData = new FormModel();

                var sID = Request.QueryString["sID"];
                var term = Request.QueryString["term"];
                var pID = Request.QueryString["pID"];
           

                using (var mst = Connection.MasterEntities(ConnectionDB.Read))
                using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                {
                    var school = mst.TCompanies.Where(o => o.nCompany == userData.CompanyID)
                        .Select(o => new
                        {
                            o.nStudentDevelopmentDirectorid,
                            o.sImage,
                            o.sCompany,
                        })
                        .FirstOrDefault();

                    FormData.Logo = school?.sImage;
                    FormData.School = school?.sCompany;
                    FormData.Teacher2 = ctx.TEmployees
                        .Where(o => o.SchoolID == userData.CompanyID && o.sEmp == school.nStudentDevelopmentDirectorid)
                        .Select(o => o.sName + " " + o.sLastname)
                        .FirstOrDefault();
                 
                    var student = (from a in ctx.TB_StudentViews
                                    .Where(o => o.SchoolID == userData.CompanyID && o.nTerm == term & o.sID + "" == sID)

                                   from b in ctx.TClassMembers
                                    .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                                    .DefaultIfEmpty()

                                   from c in ctx.TEmployees
                                    .Where(o => o.sEmp == b.nTeacherHeadid)
                                    .DefaultIfEmpty()

                                   select new
                                   {
                                       fullName = a.titleDescription + " " + a.sName + " " + a.sLastname,
                                       teacher = c.sName + " " + c.sLastname,
                                   }).FirstOrDefault();

                    FormData.FullName = student?.fullName;
                    FormData.Teacher1 = student?.teacher;

                    if (!string.IsNullOrEmpty(pID))
                    {
                        var card = (from a in ctx.TPermissionCard
                                    .Where(o => o.SchoolID == userData.CompanyID && o.ID + "" == pID)
                                    from b in ctx.TPermissionCardType
                                     .Where(o => o.SchoolID == userData.CompanyID && o.ID == a.TypeID).DefaultIfEmpty()

                                    select new
                                    {
                                        Date = a.Created,
                                        a.RefNo,
                                        a.Cause,
                                        date1 = a.StartDate,
                                        date2 = a.EndDate,
                                        type = b.Permission,
                                    }).FirstOrDefault();
                   
                        FormData.Date = card?.Date?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        FormData.RefNo = card?.RefNo + "";
                        FormData.Cause = card?.Cause + "";
                        FormData.Type = card?.type;

                        if(card?.date1?.Date == card?.date2?.Date)
                        {
                            FormData.Date1 = card?.date1?.ToString("HH:mm", new CultureInfo("th-TH"));
                            FormData.Date2 = card?.date2?.ToString("HH:mm", new CultureInfo("th-TH"));
                        }
                        else
                        {
                            FormData.Date1 = card?.date1?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                            FormData.Date2 = card?.date2?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                        }
                       
                    }
                    else
                    {
                        FormData.Date = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        FormData.RefNo = Request.QueryString["refNo"];
                        FormData.Cause = Request.QueryString["cause"];
                        FormData.Type = Request.QueryString["type"];
                        FormData.Date1 = Request.QueryString["date1"];
                        FormData.Date2 = Request.QueryString["date2"];
                      
                    }
                }
            }
        }

    }

}

//[System.Web.Script.Services.ScriptMethod()]
//[System.Web.Services.WebMethod(EnableSession = true)]
//public static object SaveData()//(Dictionary<string,object> data , object file)
//{          
//    return new { };
//    //var userData = GetUserData();
//    //using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
//    //{
//    //    var logic = new VisitHomeLogic(ctx);

//    //    var result = logic.LoadDataList(userData.CompanyID, search.term, search.level2, search.name);

//    //    return new
//    //    {
//    //        data = result.Select((o, i) => new
//    //        {
//    //            Index = i + 1,
//    //            o.Code,
//    //            o.sID,
//    //            o.FullName,
//    //            o.Status1,
//    //            o.Status2,
//    //        })
//    //    };
//    //}
//    //var result = GetData(term);

//    //return result.GroupBy(o => o.Room)
//    //    .Select(o => new
//    //    {
//    //        Room = o.Key,
//    //        Count = o.Count(),
//    //        Data = o.Select(i => new
//    //        {
//    //            i.sID,
//    //            i.title,
//    //            i.FullName,
//    //            moveInDate = i.moveInDate.HasValue ? i.moveInDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : "-",
//    //            i.sStudentID,
//    //            oldSchoolName = string.IsNullOrWhiteSpace(i.oldSchoolName) ? "-" : i.oldSchoolName,

//    //        })
//    //    })
//    //    .ToList();
//}
