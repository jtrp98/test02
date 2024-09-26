using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using FingerprintPayment.Class;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using WebGrease.Css.Extensions;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OBS;
using OBS.Model;

namespace FingerprintPayment.studentCard
{
    public partial class uploadiframe_1 : System.Web.UI.Page
    {
        private static ObsClient client;
        private static string bucketName = "onlineclassroom-system";
        private static string endpoint = "obs.ap-southeast-2.myhuaweicloud.com";
        private static string AK = "UVIKYLYS2BECK7RGLFVN";
        private static string SK = "LCQcPRii1eSYVHInqMvJU0TklY5lH6VrUeikwhdK";

        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            btnUpload.Click += new EventHandler(upload_Click);
            Button1.Click += new EventHandler(upload_Click2);


            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read))) {

                var card = _db.TStudentCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "CardCoverSchoolT1").FirstOrDefault();
                if (card != null)
                {
                    if (card.elementValue != "")
                    {
                        upload.Attributes["class"] = "hidden";
                        reset.Attributes["class"] = "";
                    }
                    else
                    {
                        upload.Attributes["class"] = "";
                        reset.Attributes["class"] = "hidden";
                    }
                    blah.Src = card.elementValue;
                }
            }
        }

        protected void upload_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string link = "";
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read))) {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();

                if (FileUpload.HasFile)
                {
                    var now = DateTime.Now.Second;
                    link = preRegisterPic(FileUpload, 1016, userData.CompanyID);

                    TStudentCardInfo card = new TStudentCardInfo();

                    var check = _db.TStudentCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "CardCoverSchoolT1" && w.cDel == null).FirstOrDefault();
                    if (check == null)
                    {
                        card = new TStudentCardInfo();
                        card.cDel = null;
                        card.elementName = "CardCoverSchoolT1";
                        card.elementValue = link;
                        card.SchoolID = userData.CompanyID;
                        card.CreatedBy = userData.UserID;
                        card.CreatedDate = DateTime.Today;

                        _db.TStudentCardInfoes.Add(card);
                    }
                    else
                    {
                        check.elementValue = link;
                        check.UpdatedBy = userData.UserID;
                        check.UpdatedDate = DateTime.Today;
                    }
                    _db.SaveChanges();
                }

                Response.Redirect("uploadiframe-1.aspx");
            }
        }

        protected void upload_Click2(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string link = "";
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";

                TStudentCardInfo card = new TStudentCardInfo();

                var check = _db.TStudentCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "CardCoverSchoolT1" && w.cDel == null).FirstOrDefault();
                check.elementValue = "";
                _db.SaveChanges();

                Response.Redirect("uploadiframe-1.aspx");
            }
        }

        public static string preRegisterPic(FileUpload files, int newwidthimg, int schoolid)
        {
            string link = "";
            var newname = "";
            string fileName = Path.GetFileName(files.PostedFile.FileName);

            var path = "";
            Dictionary<string, object> dict = new Dictionary<string, object>();
            HttpPostedFile postedFile = files.PostedFile;
            var name = postedFile.FileName.Split('.');
            //เช็ดไอดีนักเรียนตามรูปภาพ
            if (postedFile.ContentLength > 0)
            {
                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                string fileName2 = files.PostedFile.FileName;

                string month = DateTime.Now.Month.ToString();
                string year = DateTime.Now.Year.ToString();
                string date = DateTime.Now.DayOfYear.ToString();
                string hour = DateTime.Now.Hour.ToString();
                string min = DateTime.Now.Minute.ToString();
                string sec = DateTime.Now.Second.ToString();
                Random rnd = new Random();
                string rng = rnd.Next(10000000, 99999999).ToString();

               // newname = "CardPictureT1.png";

                try
                {
                    ObsConfig config = new ObsConfig();
                    config.Endpoint = endpoint;
                    client = new ObsClient(AK, SK, config);

                    fileName = files.FileName;

                    //string contentType = GetContentType(fileName);

                    //var correctdata = data.Replace("data:image/pdf;base64,", "").Replace('-', '+').Replace('_', '/');
                    //var contents = new MemoryStream(Convert.FromBase64String(correctdata));

                    string guidFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);

                    PutObjectRequest request = new PutObjectRequest()
                    {
                        BucketName = "cards-system",
                        ObjectKey = "/coverpic/student/" + schoolid + "/" + guidFileName,
                        InputStream = files.PostedFile.InputStream,
                        //ContentType = contentType,
                    };

                    PutObjectResponse response = client.PutObject(request);
                    return HttpUtility.UrlDecode(response.ObjectUrl);
                }
                catch (Exception ex)
                {
                    return "";
                }
                
            }           

            return link;
        }


    }






    



}