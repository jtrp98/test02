using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using MasterEntity;
using System.IO;
using System.Drawing.Drawing2D;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text.RegularExpressions;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Security.Cryptography;
using System.Text;
using OBS;
using OBS.Model;
using System.Net.Http;
using Newtonsoft.Json;
using static FingerprintPayment.Class.TempScanLastVersion;

namespace FingerprintPayment.Class
{
    public class ComFunction
    {
        public static void RenderStream(HttpContext context, string filePath)
        {
            Bitmap output = null;

            try
            {
                Image img = Image.FromFile(filePath);

                output = new Bitmap(img);

                output.Save(context.Response.OutputStream, ImageFormat.Png);
                output.Dispose();

                img.Dispose();
            }
            catch
            {
                output = new Bitmap(1, 1);

                output.Save(context.Response.OutputStream, ImageFormat.Png);
                output.Dispose();
            }
        }

        public static string GetFileNameFromBrowser(HttpPostedFile file)
        {
            string fileName;
            if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
            {
                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                fileName = testfiles[testfiles.Length - 1];
            }
            else
            {
                fileName = file.FileName;
            }

            return fileName;
        }

        public static string RandomNumber()
        {
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                Random rand = new Random((int)DateTime.Now.Ticks);
                int numIterations = 0;
                string snumIterations = "";
                do
                {
                    numIterations = rand.Next(100000, 999999);
                    snumIterations = numIterations.ToString();

                } while (dbMaster.TUsers.Where(w => w.sPassword == snumIterations && string.IsNullOrEmpty(w.sFinger)).ToList().Count > 0);

                return numIterations.ToString();
            }
        }

        public static string Rot13Transform(string value)
        {
            char[] array = value.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                int number = (int)array[i];

                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                array[i] = (char)number;
            }
            return new string(array);
        }

        private static string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=jabjaistorage;AccountKey=/fNzuPO0JOcsMfe8e/recdXUtYqC8cXk4bQL8BUgdy0sFPmhq8ogrePc6IJ42mU0nxfqZBckb3uReRywXtfRdA==";

        private static ObsClient client;
        private static string bucketName = "userstorage";
        private static string endpoint = "obs.ap-southeast-2.myhuaweicloud.com";
        private static string AK = "UVIKYLYS2BECK7RGLFVN";
        private static string SK = "LCQcPRii1eSYVHInqMvJU0TklY5lH6VrUeikwhdK";
        public static string GetBucketName { get { return bucketName; } }
        public static string GetEndPoint { get { return endpoint; } }
        public static string GetAK { get { return AK; } }
        public static string GetSK { get { return SK; } }

        public static string UploadFileHttpRequestBase(HttpPostedFileBase files, string folder, int newwidthimg, int schoolid, int userid)
        {
            string newname = "profile.png";
            string datetimeFolder = DateTime.Now.ToString("yyyyMMddHHmmss");

            try
            {
                //resize
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + userid + "/" + datetimeFolder)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + userid + "/" + datetimeFolder));
                }

                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + userid + "/" + datetimeFolder + "/" + newname));
                Image image = Image.FromStream(files.InputStream);
                if (image.Size.Width != newwidthimg)
                {
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
                else
                {
                    files.SaveAs(path);
                }


                ObsConfig config = new ObsConfig();
                config.Endpoint = endpoint;
                client = new ObsClient(AK, SK, config);

                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    ObjectKey = folder + "/" + schoolid + "/" + userid + "/" + datetimeFolder + "/" + newname,
                    FilePath = path
                };

                PutObjectResponse response = client.PutObject(request);
                return response.ObjectUrl;
            }
            catch (Exception err)
            {
                return "";
            }
            finally
            {
                if (File.Exists(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + userid + "/" + datetimeFolder + "/" + newname)))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + userid + "/" + datetimeFolder + "/" + newname));
                }
            }
        }
        public static string UploadProfilePictureFromByteData(string byteData, string folder, int newwidthimg, int schoolID, int userID, string extraInfo)
        {
            string fileName = "profile.png";
            string datetimeFolder = DateTime.Now.ToString("yyyyMMddHHmmss");

            try
            {
                // Check & Create folder
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + userID + "/" + datetimeFolder)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + userID + "/" + datetimeFolder));
                }

                string filePath = HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + userID + "/" + datetimeFolder + "/" + fileName);

                // Save file in File folder.
                byte[] bytes = Convert.FromBase64String(byteData);
                Image image = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));
                if (image.Size.Width != newwidthimg)
                {
                    float AspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                    int newHeight = Convert.ToInt32(newwidthimg / AspectRatio);
                    Bitmap resizeBitmap = new Bitmap(image, newwidthimg, newHeight);
                    Graphics thumbnailGraph = Graphics.FromImage(resizeBitmap);
                    thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                    thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                    thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    var imageRectangle = new Rectangle(0, 0, newwidthimg, newHeight);
                    thumbnailGraph.DrawImage(image, imageRectangle);
                    resizeBitmap.Save(filePath, ImageFormat.Png);
                    thumbnailGraph.Dispose();
                    resizeBitmap.Dispose();
                    image.Dispose();
                }
                else
                {
                    File.WriteAllBytes(filePath, bytes);
                }

                try
                {
                    using (var bitmap = new Bitmap(new MemoryStream(File.ReadAllBytes(filePath))))
                    {
                        bitmap.SetMetaValue(ExtensionMethod.MetaProperty.Copyright, "Copyrights © 2023 All Rights Reserved by Jabjai Corporation Co.,Ltd.");
                        bitmap.SetMetaValue(ExtensionMethod.MetaProperty.DateTime, DateTime.Now.ToString());
                        bitmap.SetMetaValue(ExtensionMethod.MetaProperty.UserComment, extraInfo);
                        bitmap.SetMetaValue(ExtensionMethod.MetaProperty.ImageDescription, "User profile picture cropped.");
                        bitmap.Save(filePath);
                    }
                }
                catch { }

                ObsConfig config = new ObsConfig();
                config.Endpoint = endpoint;
                client = new ObsClient(AK, SK, config);

                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    ObjectKey = folder + "/" + schoolID + "/" + userID + "/" + datetimeFolder + "/" + fileName,
                    FilePath = filePath
                };

                PutObjectResponse response = client.PutObject(request);
                return response.ObjectUrl;
            }
            catch (Exception err)
            {
                return "";
            }
            finally
            {
                if (File.Exists(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + userID + "/" + datetimeFolder + "/" + fileName)))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + userID + "/" + datetimeFolder + "/" + fileName));
                }
            }
        }
        public static string UploadFileOriginalHttpRequestBase(HttpPostedFileBase files, string folder, int schoolid, int userid)
        {
            string newname = "profile-original.png";
            string datetimeFolder = DateTime.Now.ToString("yyyyMMddHHmmss");

            try
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + userid + "/" + datetimeFolder)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + userid + "/" + datetimeFolder));
                }

                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + userid + "/" + datetimeFolder + "/" + newname));
                Image image = Image.FromStream(files.InputStream);
                files.SaveAs(path);


                ObsConfig config = new ObsConfig();
                config.Endpoint = endpoint;
                client = new ObsClient(AK, SK, config);

                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    ObjectKey = folder + "/" + schoolid + "/" + userid + "/" + datetimeFolder + "/" + newname,
                    FilePath = path
                };

                PutObjectResponse response = client.PutObject(request);
                return response.ObjectUrl;
            }
            catch (Exception err)
            {
                return "";
            }
            finally
            {
                if (File.Exists(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + userid + "/" + datetimeFolder + "/" + newname)))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + userid + "/" + datetimeFolder + "/" + newname));
                }
            }
        }
        public static string UploadFileHttpRequestBase(HttpPostedFileBase files, string folder, int schoolid, int id)
        {
            string fileName = "";

            try
            {
                fileName = files.FileName;

                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + id)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + id));
                }
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + id + "/" + fileName));
                files.SaveAs(path);


                ObsConfig config = new ObsConfig();
                config.Endpoint = endpoint;
                client = new ObsClient(AK, SK, config);

                string ObjectKey = Guid.NewGuid().ToString() + Path.GetExtension(fileName);

                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    ObjectKey = folder + "/" + schoolid + "/" + id + "/" + ObjectKey,
                    FilePath = path,
                    //ContentType = files.ContentType,
                };

                PutObjectResponse response = client.PutObject(request);
                return response.ObjectUrl;
            }
            catch (Exception err)
            {
                return "";
            }
            finally
            {
                if (File.Exists(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + id + "/" + fileName)))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolid + "/" + id + "/" + fileName));
                }
            }
        }
        public static string UploadFaceScanBase64ImageToAzure(string folder, int schoolID, int sID, string base64Image)
        {
            string url = "";

            try
            {
                //facescanpicture/yyyyMMdd/schoolID/sID/yyyyMMddhhmmss.jpg
                string fileName = string.Format(@"{0:yyyyMMddHHmmss}.jpg", DateTime.Now); // *.jpg

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageConnectionString);

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference(string.Format(@"{0}/{1:yyyyMMdd}/{2}/{3}", folder, DateTime.Now, schoolID, sID));
                //Create or overwrite the "myblob" blob with contents from a local file.
                CloudBlockBlob cblob = container.GetBlockBlobReference(fileName);

                var bytes = Convert.FromBase64String(base64Image);// without data:image/jpg;base64 prefix, just base64 string
                using (var stream = new MemoryStream(bytes))
                {
                    cblob.UploadFromStream(stream);
                }

                url = string.Format(@"https://jabjaistorage.blob.core.windows.net/{0}/{1:yyyyMMdd}/{2}/{3}/{4}", folder, DateTime.Now, schoolID, sID, fileName);
            }
            catch
            {

            }

            return url;
        }
        public static string UploadFileFromByteData(string fileName, string byteData, string folder, int schoolID, int registerID)
        {
            try
            {
                // Check & Create folder
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + registerID)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + registerID));
                }

                string filePath = HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + registerID + "/" + fileName);

                // Save file in File folder.
                byte[] bytes = Convert.FromBase64String(byteData);
                File.WriteAllBytes(filePath, bytes);


                ObsConfig config = new ObsConfig();
                config.Endpoint = endpoint;
                client = new ObsClient(AK, SK, config);

                string ObjectKey = Guid.NewGuid().ToString() + Path.GetExtension(fileName);

                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    ObjectKey = folder + "/" + schoolID + "/" + registerID + "/" + ObjectKey,
                    FilePath = filePath
                };

                PutObjectResponse response = client.PutObject(request);
                return response.ObjectUrl;
            }
            catch (Exception err)
            {
                return "";
            }
            finally
            {
                if (File.Exists(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + registerID + "/" + fileName)))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + registerID + "/" + fileName));
                }
            }
        }
        public static string UploadFileFromByteData(string byteData, string folder, int newwidthimg, int schoolID, int registerID)
        {
            string fileName = "profile.png";
            string datetimeFolder = DateTime.Now.ToString("yyyyMMddHHmmss");

            try
            {
                // Check & Create folder
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + registerID + "/" + datetimeFolder)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + registerID + "/" + datetimeFolder));
                }

                string filePath = HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + registerID + "/" + datetimeFolder + "/" + fileName);

                // Save file in File folder.
                byte[] bytes = Convert.FromBase64String(byteData);
                Image image = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));
                if (image.Size.Width != newwidthimg)
                {
                    float AspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                    int newHeight = Convert.ToInt32(newwidthimg / AspectRatio);
                    Bitmap resizeBitmap = new Bitmap(image, newwidthimg, newHeight);
                    Graphics thumbnailGraph = Graphics.FromImage(resizeBitmap);
                    thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                    thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                    thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    var imageRectangle = new Rectangle(0, 0, newwidthimg, newHeight);
                    thumbnailGraph.DrawImage(image, imageRectangle);
                    resizeBitmap.Save(filePath, ImageFormat.Png);
                    thumbnailGraph.Dispose();
                    resizeBitmap.Dispose();
                    image.Dispose();
                }
                else
                {
                    File.WriteAllBytes(filePath, bytes);
                }


                ObsConfig config = new ObsConfig();
                config.Endpoint = endpoint;
                client = new ObsClient(AK, SK, config);

                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    ObjectKey = folder + "/" + schoolID + "/" + registerID + "/" + datetimeFolder + "/" + fileName,
                    FilePath = filePath
                };

                PutObjectResponse response = client.PutObject(request);
                return response.ObjectUrl;
            }
            catch (Exception err)
            {
                return "";
            }
            finally
            {
                if (File.Exists(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + registerID + "/" + datetimeFolder + "/" + fileName)))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/images/" + folder + "/" + schoolID + "/" + registerID + "/" + datetimeFolder + "/" + fileName));
                }
            }
        }

        /// <summary>  
        /// For calculating only age  
        /// </summary>  
        /// <param name="dateOfBirth">Date of birth</param>  
        /// <returns> age e.g. 26</returns>  
        public static int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            {
                age -= 1;
            }

            return age;
        }

        public static string CalculateAge(DateTime Dob, string display)
        {
            string res = "";
            if (DateTime.Now > Dob)
            {
                DateTime Now = DateTime.Now;
                int _Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
                DateTime _DOBDateNow = Dob.AddYears(_Years);
                int _Months = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (_DOBDateNow.AddMonths(i) == Now)
                    {
                        _Months = i;
                        break;
                    }
                    else if (_DOBDateNow.AddMonths(i) >= Now)
                    {
                        _Months = i - 1;
                        break;
                    }
                }
                int Days = Now.Subtract(_DOBDateNow.AddMonths(_Months)).Days;

                switch (display)
                {
                    case "year": res = $"{_Years}"; break;
                    case "month": res = $"{_Months}"; break;
                    case "day": res = $"{Days}"; break;
                }
            }

            return res;
        }

        public static string GenerateExamCode(int registerYear, int? nTLevel, int registerClass, int eduProgram, int runID)
        {
            string level = "";
            switch (nTLevel)
            {
                case 10: level = "1"; break; // อนุบาลศึกษา
                case 7: level = "2"; break; // ประถมศึกษา
                case 9: level = "3"; break; // มัธยมศึกษา
            }

            string subLevel = "";
            switch (registerClass)
            {
                case 39: subLevel = "1"; break; // อ.1
                case 40: subLevel = "2"; break; // อ.2
                case 41: subLevel = "3"; break; // อ.3

                case 42: subLevel = "1"; break; // ป.1
                case 43: subLevel = "2"; break; // ป.2
                case 44: subLevel = "3"; break; // ป.3
                case 45: subLevel = "4"; break; // ป.4
                case 46: subLevel = "5"; break; // ป.5
                case 47: subLevel = "6"; break; // ป.6

                case 48: subLevel = "1"; break; // ม.1
                case 49: subLevel = "2"; break; // ม.2
                case 50: subLevel = "3"; break; // ม.3
                case 51: subLevel = "4"; break; // ม.4
                case 52: subLevel = "5"; break; // ม.5
                case 53: subLevel = "6"; break; // ม.6

            }

            string examCode = string.Format(@"{0}-{1}{2}-{3:D2}-{4:D3}", registerYear.ToString().Substring(2, 2), level, subLevel, eduProgram, runID); // [year{2}]-[levelID][subLevelID]-[planID]-[runID] = 63-11-01-001

            return examCode;
        }

        public static string GenerateExamCode2(int registerYear, string nTLevel, string registerClass, int eduProgram, int runID)
        {
            string level = "";
            switch (nTLevel)
            {
                case "อนุบาลศึกษา": level = "1"; break; // อนุบาลศึกษา
                case "ประถมศึกษา": level = "2"; break; // ประถมศึกษา
                case "มัธยมศึกษา": level = "3"; break; // มัธยมศึกษา
            }

            string subLevel = "";
            switch (registerClass)
            {
                case "อ.1": subLevel = "1"; break; // อ.1
                case "อ.2": subLevel = "2"; break; // อ.2
                case "อ.3": subLevel = "3"; break; // อ.3

                case "ป.1": subLevel = "1"; break; // ป.1
                case "ป.2": subLevel = "2"; break; // ป.2
                case "ป.3": subLevel = "3"; break; // ป.3
                case "ป.4": subLevel = "4"; break; // ป.4
                case "ป.5": subLevel = "5"; break; // ป.5
                case "ป.6": subLevel = "6"; break; // ป.6

                case "ม.1": subLevel = "1"; break; // ม.1
                case "ม.2": subLevel = "2"; break; // ม.2
                case "ม.3": subLevel = "3"; break; // ม.3
                case "ม.4": subLevel = "4"; break; // ม.4
                case "ม.5": subLevel = "5"; break; // ม.5
                case "ม.6": subLevel = "6"; break; // ม.6

            }

            string examCode = string.Format(@"{0}-{1}{2}-{3:D2}-{4:D3}", registerYear.ToString().Substring(2, 2), level, subLevel, eduProgram, runID); // [year{2}]-[levelID][subLevelID]-[planID]-[runID] = 63-11-01-001

            return examCode;
        }

        public static int GetLineNumberError(Exception ex)
        {
            var lineNumber = 0;
            const string lineSearch = ":line ";
            var index = ex.StackTrace.LastIndexOf(lineSearch);
            if (index != -1)
            {
                var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                if (int.TryParse(lineNumberText, out lineNumber))
                {
                }
            }
            return lineNumber;
        }

        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static void InsertLogDebug(int? schoolID, int? studentID, int? employeeID, string logMessage)
        {
            try
            {
                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    dbMaster.TLogDebugs.Add(new TLogDebug
                    {
                        SchoolID = schoolID,
                        StudentID = studentID,
                        EmployeeID = employeeID,
                        LogMessage = logMessage,
                        LogDate = DateTime.Now,
                        IP = GetIPAddress()
                    });

                    dbMaster.SaveChanges();
                }
            }
            catch { }
        }

        // return how much time passed since date object
        public static string GetTimeSince(DateTime objDateTime)
        {
            // here we are going to subtract the passed in DateTime from the current time converted to UTC
            TimeSpan ts = DateTime.Now.Subtract(objDateTime);
            int intDays = ts.Days;
            int intHours = ts.Hours;
            int intMinutes = ts.Minutes;
            int intSeconds = ts.Seconds;

            if (intDays > 0)
                return string.Format("{0} days", intDays);

            if (intHours > 0)
                return string.Format("{0} hours", intHours);

            if (intMinutes > 0)
                return string.Format("{0} minutes", intMinutes);

            if (intSeconds > 0)
                return string.Format("{0} seconds", intSeconds);

            // let's handle future times..just in case
            if (intDays < 0)
                return string.Format("in {0} days", Math.Abs(intDays));

            if (intHours < 0)
                return string.Format("in {0} hours", Math.Abs(intHours));

            if (intMinutes < 0)
                return string.Format("in {0} minutes", Math.Abs(intMinutes));

            if (intSeconds < 0)
                return string.Format("in {0} seconds", Math.Abs(intSeconds));

            return "a bit";
        }

        /// <summary>
        /// Stores a value in a user Cookie, creating it if it doesn't exists yet.
        /// </summary>
        public static void StoreInCookie(
            string cookieName,
            string cookieDomain,
            string keyName,
            string value,
            DateTime? expirationDate,
            bool httpOnly = false)
        {
            // NOTE: we have to look first in the response, and then in the request.
            // This is required when we update multiple keys inside the cookie.
            HttpCookie cookie = HttpContext.Current.Response.Cookies[cookieName]
                ?? HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null) cookie = new HttpCookie(cookieName);
            if (!String.IsNullOrEmpty(keyName)) cookie.Values.Set(keyName, value);
            else cookie.Value = value;
            if (expirationDate.HasValue) cookie.Expires = expirationDate.Value;
            if (!String.IsNullOrEmpty(cookieDomain)) cookie.Domain = cookieDomain;
            if (httpOnly) cookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        /// <summary>
        /// Stores multiple values in a Cookie using a key-value dictionary, 
        ///  creating the cookie (and/or the key) if it doesn't exists yet.
        /// </summary>
        public static void StoreInCookie(
            string cookieName,
            string cookieDomain,
            Dictionary<string, string> keyValueDictionary,
            DateTime? expirationDate,
            bool httpOnly = false)
        {
            // NOTE: we have to look first in the response, and then in the request.
            // This is required when we update multiple keys inside the cookie.
            HttpCookie cookie = HttpContext.Current.Response.Cookies[cookieName]
                ?? HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null) cookie = new HttpCookie(cookieName);
            if (keyValueDictionary == null || keyValueDictionary.Count == 0)
                cookie.Value = null;
            else
                foreach (var kvp in keyValueDictionary)
                    cookie.Values.Set(kvp.Key, kvp.Value);
            if (expirationDate.HasValue) cookie.Expires = expirationDate.Value;
            if (!String.IsNullOrEmpty(cookieDomain)) cookie.Domain = cookieDomain;
            if (httpOnly) cookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        /// <summary>
        /// Retrieves a single value from Request.Cookies
        /// </summary>
        public static string GetFromCookie(string cookieName, string keyName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                string val = (!String.IsNullOrEmpty(keyName)) ? cookie[keyName] : cookie.Value;
                if (!String.IsNullOrEmpty(val)) return Uri.UnescapeDataString(val);
            }
            return null;
        }

        /// <summary>
        /// Removes a single value from a cookie or the whole cookie (if keyName is null)
        /// </summary>
        public static void RemoveCookie(string cookieName, string keyName, string domain = null)
        {
            if (String.IsNullOrEmpty(keyName))
            {
                if (HttpContext.Current.Request.Cookies[cookieName] != null)
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                    cookie.Expires = DateTime.UtcNow.AddYears(-1);
                    if (!String.IsNullOrEmpty(domain)) cookie.Domain = domain;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    HttpContext.Current.Request.Cookies.Remove(cookieName);
                }
            }
            else
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                cookie.Values.Remove(keyName);
                if (!String.IsNullOrEmpty(domain)) cookie.Domain = domain;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// Checks if a cookie / key exists in the current HttpContext.
        /// </summary>
        public static bool CookieExist(string cookieName, string keyName)
        {
            HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
            return (String.IsNullOrEmpty(keyName))
                ? cookies[cookieName] != null
                : cookies[cookieName] != null && cookies[cookieName][keyName] != null;
        }

        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static void SendMail(string subject, string body, string fromEmail = "no-reply@schoolbright.co", string fromName = "School Bright", string toEmail = "no-reply@schoolbright.co", string toName = "School Bright")
        {
            try
            {
                var client = new SendGridClient("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");
                var from = new EmailAddress(fromEmail, fromName);
                var to = new EmailAddress(toEmail, toName);

                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", body);

                client.SendEmailAsync(msg).Wait();
            }
            catch (Exception ex)
            {

            }
        }

        public static string HashSHA1(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hashSh1 = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));

                // declare stringbuilder
                var sb = new StringBuilder(hashSh1.Length * 2);

                // computing hashSh1
                foreach (byte b in hashSh1)
                {
                    // "x2"
                    sb.Append(b.ToString("X2").ToLower());
                }

                return sb.ToString().ToUpper();
            }
        }

        public static string GetImageAsBase64Url(string url)
        {
            string base64 = "R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";

            try
            {
                var httClient = new HttpClient();
                var imageBytes = httClient.GetByteArrayAsync(url).Result;
                base64 = Convert.ToBase64String(imageBytes);
            }
            catch { }

            return "data:image/png;base64," + base64;
        }

        public static TempScanLastVersion ReadTempScanVersionFromApi()
        {
            TempScanLastVersion tempScanLastVersion = new TempScanLastVersion();

            try
            {
                var client = new RestSharp.RestClient(@"https://sbapi.schoolbright.co/api/application");
                client.Timeout = -1;
                var request = new RestSharp.RestRequest(RestSharp.Method.GET);
                RestSharp.IRestResponse response = client.Execute(request);

                List<Installer> installers = JsonConvert.DeserializeObject<List<Installer>>(response.Content);

                // x86
                var x86 = installers.Where(w => w.ApplicationName == "School Bright TempScan (x86)").FirstOrDefault();
                tempScanLastVersion.setupx86 = new Setup();
                tempScanLastVersion.setupx86.name = Path.GetFileName(x86.UrlDownload);
                tempScanLastVersion.setupx86.url = x86.UrlDownload;

                // x64
                var x64 = installers.Where(w => w.ApplicationName == "School Bright TempScan (x64)").FirstOrDefault();
                tempScanLastVersion.setupx64 = new Setup();
                tempScanLastVersion.setupx64.name = Path.GetFileName(x64.UrlDownload);
                tempScanLastVersion.setupx64.url = x64.UrlDownload;

                tempScanLastVersion.version = x64.ApplicationVersion;

                tempScanLastVersion.IsSuccess = true;
            }
            catch (ObsException err)
            {
                tempScanLastVersion.IsSuccess = false;
                tempScanLastVersion.Message = err.Message;
                tempScanLastVersion.StackTrace = err.StackTrace;
            }
            catch (Exception err)
            {
                tempScanLastVersion.IsSuccess = false;
                tempScanLastVersion.Message = err.Message;
                tempScanLastVersion.StackTrace = err.StackTrace;
            }

            return tempScanLastVersion;
        }

        public static TempScanLastVersion GetSBShopLastVersion()
        {
            TempScanLastVersion tempScanLastVersion = new TempScanLastVersion();

            try
            {
                var client = new RestSharp.RestClient(@"https://sbapi.schoolbright.co/api/application");
                client.Timeout = -1;
                var request = new RestSharp.RestRequest(RestSharp.Method.GET);
                RestSharp.IRestResponse response = client.Execute(request);

                List<Installer> installers = JsonConvert.DeserializeObject<List<Installer>>(response.Content);

                // x86
                var x86 = installers.Where(w => w.ApplicationName == "School Bright Shop (x86)").FirstOrDefault();
                tempScanLastVersion.setupx86 = new Setup();
                tempScanLastVersion.setupx86.name = Path.GetFileName(x86.UrlDownload);
                tempScanLastVersion.setupx86.url = x86.UrlDownload;

                // x64
                var x64 = installers.Where(w => w.ApplicationName == "School Bright Shop (x64)").FirstOrDefault();
                tempScanLastVersion.setupx64 = new Setup();
                tempScanLastVersion.setupx64.name = Path.GetFileName(x64.UrlDownload);
                tempScanLastVersion.setupx64.url = x64.UrlDownload;

                tempScanLastVersion.version = x64.ApplicationVersion;

                tempScanLastVersion.IsSuccess = true;
            }
            catch (ObsException err)
            {
                tempScanLastVersion.IsSuccess = false;
                tempScanLastVersion.Message = err.Message;
                tempScanLastVersion.StackTrace = err.StackTrace;
            }
            catch (Exception err)
            {
                tempScanLastVersion.IsSuccess = false;
                tempScanLastVersion.Message = err.Message;
                tempScanLastVersion.StackTrace = err.StackTrace;
            }

            return tempScanLastVersion;
        }

        public class Installer
        {
            [JsonProperty(PropertyName = "application_name")]
            public string ApplicationName { get; set; }

            [JsonProperty(PropertyName = "application_version")]
            public string ApplicationVersion { get; set; }

            [JsonProperty(PropertyName = "url_download")]
            public string UrlDownload { get; set; }

            [JsonProperty(PropertyName = "updateDate")]
            public string UpdateDate { get; set; }

        }


        public static string GenerateEmailContent(string type,int schoolId,string studentName,string studentNameEn,string invCode, string year, string invUrl)
        {
            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            {
                var invoice_email = HttpContext.Current.Server.MapPath("~/PreRegister/template/invoice_email.html");
                if (type != "invoice")
                {
                    invoice_email = HttpContext.Current.Server.MapPath("~/PreRegister/template/reciept_email.html");
                }
                var emailHtmlContent = System.IO.File.ReadAllText(invoice_email);

                var company = masterEntities.TCompanies.FirstOrDefault(m => m.nCompany == schoolId);
                emailHtmlContent = emailHtmlContent.Replace("@SchoolLogoUrl", company.sImage);
                emailHtmlContent = emailHtmlContent.Replace("@SchoolName", company.sCompany);
                emailHtmlContent = emailHtmlContent.Replace("@SchoolNameEn", company.sNameEN);
                emailHtmlContent = emailHtmlContent.Replace("@SchoolAddress", $"{company.sHomeNumber} ซอย {company.sSoy} หมู่ {company.sMuu} ถนน {company.sRoad} ตำบล {company.sTumbon} อำเภอ {company.sAumpher} จังหวัด {company.sProvince} {company.sPost}");
                emailHtmlContent = emailHtmlContent.Replace("@SchoolTax", company.TaxId);
                emailHtmlContent = emailHtmlContent.Replace("@SchoolTel", company.sPhoneOne);
                emailHtmlContent = emailHtmlContent.Replace("@StudentName", studentName);
                emailHtmlContent = emailHtmlContent.Replace("@StudentNameEn", studentNameEn);
                emailHtmlContent = emailHtmlContent.Replace("@InvoiceCode", invCode);
                emailHtmlContent = emailHtmlContent.Replace("@Year", year);
                emailHtmlContent = emailHtmlContent.Replace("@InvoiceUrl", invUrl);        
                return emailHtmlContent;
            }            
        }
    }
}