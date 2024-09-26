using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.VisitHousePage.Web
{
    public partial class Edit : BehaviorGateway
    {

        //public class Search
        //{
        //    public string term { get; set; }
        //    public int level2 { get; set; }
        //    public string name { get; set; }
        //}

        protected VisitHomeModel.EditForm DataForm { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitPage();
            }
        }

        private void InitPage()
        {
            using (JabJaiMasterEntities mst = Connection.MasterEntities(ConnectionDB.Read))
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new VisitHomeRepository(ctx, mst);
                var sid = Request.QueryString["sid"]; 
                var term = Request.QueryString["term"];
                var model = logic.LoadEditForm(UserData.CompanyID, Convert.ToInt32(sid) , term, UserData);

                DataForm = model;

                if (DataForm.Visit != null)
                {
                    if (!string.IsNullOrEmpty(DataForm.Visit.StudentSignature))
                        DataForm.Visit.StudentSignature = ConvertImageURLToBase64(DataForm.Visit.StudentSignature);
                    if (!string.IsNullOrEmpty(DataForm.Visit.ParentSignature))
                        DataForm.Visit.ParentSignature = ConvertImageURLToBase64(DataForm.Visit.ParentSignature);
                    if (!string.IsNullOrEmpty(DataForm.Visit.TeacherSignature))
                        DataForm.Visit.TeacherSignature = ConvertImageURLToBase64(DataForm.Visit.TeacherSignature);
                }
            }
        }


        private String ConvertImageURLToBase64(String url)
        {
            StringBuilder _sb = new StringBuilder();

            Byte[] _byte = this.GetImage(url);

            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

            return _sb.ToString();
        }

        private byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception exp)
            {
                buf = null;
            }

            return (buf);
        }

        public static string GetType(string contentType)
        {
            switch (contentType)
            {
                case "text/plain":
                    return "text";

                case "application/pdf":
                    return "pdf";

                case "application/msword":
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                case "application/vnd.ms-excel":
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                case "application/vnd.ms-powerpoint":
                case "application/vnd.openxmlformats-officedocument.presentationml.presentation":
                    return "office";

                case "image/png":
                case "image/jpeg":
                case "image/gif":
                    return "image";

                case "video/mp4":
                case "video/x-matroska":
                case "video/x-ms-wmv":
                case "video/x-msvideo":
                case "video/quicktime":

                    return "video";

                case "audio/mpeg":
                    return "audio";

                default: return "";
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
