using FingerprintPayment.Card.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Card
{
    public partial class BackupCardHistory : CardGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string cid = Request.QueryString["cid"];
                Guid guidCId = Guid.Parse(cid);
                TBackupCard backupCard = en.TBackupCards.Where(w => w.SchoolID == schoolID && w.CardID == guidCId).FirstOrDefault();
                if (backupCard != null)
                {
                    this.ltrCardName.Text = backupCard.CardName;
                    this.ltrBarCode.Text = string.IsNullOrEmpty(backupCard.BarCode) ? "-" : backupCard.BarCode;
                    this.ltrNFC.Text = string.IsNullOrEmpty(backupCard.NFC) ? "-" : backupCard.NFC;
                }
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadBackupCardHistory(string cid)
        {
            //var jsonStream = "";
            //HttpContext.Current.Request.InputStream.Position = 0;
            //using (var inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            //{
            //    jsonStream = inputStream.ReadToEnd();
            //}
            //var serializer = new JavaScriptSerializer();
            //dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            //int draw = Convert.ToInt32(jsonObject["draw"]);
            //int pageIndex = Convert.ToInt32(jsonObject["page"]);
            //int pageSize = Convert.ToInt32(jsonObject["length"]);
            //string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
            //string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

            //string sortBy = "CreatedDate";
            //switch (sortIndex)
            //{
            //    case "1": sortBy = "BorrowingDate"; break;
            //    case "2": sortBy = "ReturnDate"; break;
            //    case "3": sortBy = "UserType"; break;
            //    case "4": sortBy = "UserName"; break;
            //}
            //sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            //string cid = Convert.ToString(jsonObject["cid"]);

            var json = QueryEngine.LoadBackupCardHistoryJsonData(GetUserData().CompanyID, cid);

            return json;
        }

    }
}