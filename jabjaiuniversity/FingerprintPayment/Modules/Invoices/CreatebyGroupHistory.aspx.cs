using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Modules.Invoices
{
    public partial class CreatebyGroupHistory : StudentGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object LoadHistory(Search data)
        {
            using (PeakengineEntities entities = Connection.PeakengineEntities(ConnectionDB.Read))
            {
                string SQL = @"SELECT Fd_LevelName,Fd_UID,Fd_TermNumber,Fd_YearNumber,Fd_InvoiceStatus,Fd_Amount,Fd_HistoryID,
FORMAT(Fd_CreateDate, 'dd/MM/yyyy HH:mm:ss น.', 'th-TH') AS Fd_CreateDate,B.sName + ' ' + B.sLastname Fd_CreateBy 
FROM TB_CreateGroupHistory AS A 
INNER JOIN JabjaiSchoolSingleDB.DBO.TEmployees AS B ON A.Fd_CreateBy = B.sEmp";
                var q1 = entities.Database.SqlQuery<TM_CreateGroupHistory>(SQL).ToList();

                return q1;
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object LoadData(Guid Fd_UID)
        {
            using (PeakengineEntities entities = Connection.PeakengineEntities(ConnectionDB.Read))
            {
                return entities.TB_CreateGroupHistory.FirstOrDefault(f => f.Fd_UID == Fd_UID);
            }
        }


        public class Search
        {
            public string startDate { get; set; }
            public string endDate { get; set; }

        }

        public class TM_CreateGroupHistory
        {
            public System.Guid Fd_UID { get; set; }
            public Nullable<int> Fd_SchoolID { get; set; }
            public Nullable<int> Fd_LevelID { get; set; }
            public string Fd_LevelName { get; set; }
            public string Fd_InvoiceStatus { get; set; }
            public Nullable<decimal> Fd_Amount { get; set; }
            public string Fd_HistoryID { get; set; }
            public string Fd_TermNumber { get; set; }
            public string Fd_YearNumber { get; set; }
            public string Fd_CreateDate { get; set; }
            public string Fd_CreateBy { get; set; }
        }
    }
}