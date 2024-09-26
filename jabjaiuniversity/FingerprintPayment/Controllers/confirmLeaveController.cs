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
    public class confirmLeaveController : ApiController
    {
        public String Get([FromUri] dffdsf data)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();

            

            return "Success";
        }
    }

    public class dffdsf
    {
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int SchoolId { get; set; }
        public string LeaveType { get; set; }
        public string DayStart { get; set; }
        
    }
}
