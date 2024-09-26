using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.VisitHousePage.Web
{
    public partial class WebForm1 : BehaviorGateway
    {

        public class Search
        {
            public string term { get; set; }
            public int level1 { get; set; }
            public int? level2 { get; set; }
            public string name { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object LoadData(Search search)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new VisitHomeRepository(ctx);

                var result = logic.LoadDataList(userData.CompanyID, search.term , search.level1, search.level2, search.name);

                return new
                {
                    data = result.Select((o, i) => new
                    {
                        Index = i + 1,
                        o.TermID,
                        o.Code,
                        o.sID,
                        o.FullName,
                        o.Status1,
                        o.Status2,
                    })
                };
            }
            //var result = GetData(term);

            //return result.GroupBy(o => o.Room)
            //    .Select(o => new
            //    {
            //        Room = o.Key,
            //        Count = o.Count(),
            //        Data = o.Select(i => new
            //        {
            //            i.sID,
            //            i.title,
            //            i.FullName,
            //            moveInDate = i.moveInDate.HasValue ? i.moveInDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : "-",
            //            i.sStudentID,
            //            oldSchoolName = string.IsNullOrWhiteSpace(i.oldSchoolName) ? "-" : i.oldSchoolName,

            //        })
            //    })
            //    .ToList();
        }
    }
}