using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.Controllers
{
    public class studentLeaveController : ApiController
    {
        public String Get([FromUri] studentleave data)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();

            TLeaveLetter Letter = new TLeaveLetter();

            int countletter = 1;
            foreach (var count in _db.TLeaveLetters.ToList())
            {
                countletter = countletter + 1;
            }
            Letter.letterId = countletter;
            Letter.letterSchoolId = data.SchoolId;

            
           

            Letter.letterHeader = "";
            Letter.writerJob = "นักเรียน";
            Letter.letterType = data.LeaveType;
            Letter.writerComment = data.Description;
            string combineSubmit = data.SubmitDate;
            DateTime? dt3;
            if (DateTime.ParseExact(combineSubmit, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                dt3 = DateTime.ParseExact(combineSubmit, "dd/MM/yyyy", new CultureInfo("en-us"));
            else
                dt3 = DateTime.ParseExact(combineSubmit, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

            Letter.letterDate = dt3;
            string combineStart = data.DayStart;
            DateTime? dt1;
            if (DateTime.ParseExact(combineStart, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                dt1 = DateTime.ParseExact(combineStart, "dd/MM/yyyy", new CultureInfo("en-us"));
            else
                dt1 = DateTime.ParseExact(combineStart, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

            
            string combineEnd = data.DayEnd;
            DateTime? dt2;
            if (DateTime.ParseExact(combineEnd, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                dt2 = DateTime.ParseExact(combineEnd, "dd/MM/yyyy", new CultureInfo("en-us"));
            else
                dt2 = DateTime.ParseExact(combineEnd, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);



            Letter.startDate = dt1;
            Letter.endDate = dt2;

            Letter.writerId = data.StudentId;
            Letter.contactAumpher = data.Aumpher;
            Letter.contactHomenumber = data.HomeNumber;
            Letter.contactPhone = data.Phone;
            Letter.contactProvince = data.Province;
            Letter.contactRoad = data.Road;
            Letter.contactTumbon = data.Tumbon;

            _db.TLeaveLetters.Add(Letter);
            _db.SaveChanges();
            

            return "Success";
        }
    }

    public class studentleave
    {
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int SchoolId { get; set; }
        public string LeaveType { get; set; }
        public string DayStart { get; set; }
        public string DayEnd { get; set; }
        public string Description { get; set; }
        public string HomeNumber { get; set; }
        public string Road { get; set; }
        public string Tumbon { get; set; }
        public string Aumpher { get; set; }
        public string Province { get; set; }
        public string Phone { get; set; }
        public string SubmitDate { get; set; }
    }
}
