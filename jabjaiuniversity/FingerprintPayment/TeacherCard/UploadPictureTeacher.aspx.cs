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

namespace FingerprintPayment.TeacherCard
{
    public partial class UploadPictureTeacher : System.Web.UI.Page
    {
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
            btnDelete.Click += new EventHandler(Delete_Click);

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                var card = _db.TTeacherCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "CardCover").FirstOrDefault();
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
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";

                if (FileUpload.HasFile)
                {
                    //var now = DateTime.Now.Second;
                    link = preRegisterPic(FileUpload, 1016, userData.CompanyID);

                    TTeacherCardInfo TeacherCard = new TTeacherCardInfo();

                    var check = _db.TTeacherCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "CardCover" && w.cDel == null).FirstOrDefault();
                    if (check == null)
                    {
                        TeacherCard = new TTeacherCardInfo();
                        TeacherCard.cDel = null;
                        TeacherCard.elementName = "CardCover";
                        TeacherCard.elementValue = link;
                        TeacherCard.SchoolID = userData.CompanyID;

                        _db.TTeacherCardInfoes.Add(TeacherCard);
                    }
                    else
                    {
                        check.elementValue = link;
                    }

                    _db.SaveChanges();
                }

                Response.Redirect("UploadPictureTeacher.aspx");
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string link = "";
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";

                TTeacherCardInfo TeacherCard = new TTeacherCardInfo();

                var check = _db.TTeacherCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "CardCover" && w.cDel == null).FirstOrDefault();

                check.elementValue = "";

                _db.SaveChanges();

                Response.Redirect("UploadPictureTeacher.aspx");
            }
        }


        public static string preRegisterPic(FileUpload files, int newwidthimg, int userid)
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

                newname = "CardPicture.png";
                link = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/coverpic/teacher/" + userid + "/" + newname;
                //resize
                if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/images/profile/" + userid)))
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/images/profile/" + userid));
                }
                path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/images/profile/" + userid + "/" + newname));
                System.Drawing.Image image = System.Drawing.Image.FromStream(postedFile.InputStream);

                float AspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                int newHeight = Convert.ToInt32(newwidthimg / AspectRatio);
                Bitmap resizeBitmap = new Bitmap(image, newwidthimg, newHeight);
                Graphics thumbnailGraph = Graphics.FromImage(resizeBitmap);
                thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newwidthimg, newHeight);
                thumbnailGraph.DrawImage(image, imageRectangle);
                resizeBitmap.Save(path, ImageFormat.Png);
                thumbnailGraph.Dispose();
                resizeBitmap.Dispose();
                image.Dispose();
            }

            try
            {

                ObsClient client;
                string bucketName = "userstorage";
                string endpoint = "obs.ap-southeast-2.myhuaweicloud.com";
                string AK = "UVIKYLYS2BECK7RGLFVN";
                string SK = "LCQcPRii1eSYVHInqMvJU0TklY5lH6VrUeikwhdK";

                ObsConfig config = new ObsConfig();
                config.Endpoint = endpoint;
                client = new ObsClient(AK, SK, config);
             
                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    ObjectKey = "coverpic/teacher/" + userid + "/" + newname,
                    FilePath = path,
                    
                };

                PutObjectResponse response = client.PutObject(request);

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/images/profile/" + userid + "/" + newname)))
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/images/profile/" + userid + "/" + newname));
                }

                return response.ObjectUrl;
            }
            catch (Exception ex)
            {
                return "";
            }
                      
        }






    }
}