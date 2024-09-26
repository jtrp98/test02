using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Class
{
    public class AzureStorage
    {
        private static string UploadFile(FileUpload files)
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

                newname = year + month + date + hour + min + sec + rng + ".png";
                link = "https://jabjaistorage.blob.core.windows.net/userprofile/" + newname;
                //resize
                //path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/images/" + newname));
                //System.Drawing.Image image = System.Drawing.Image.FromStream(postedFile.InputStream);
                //int newwidthimg = 300;
                //float AspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                //int newHeight = Convert.ToInt32(newwidthimg / AspectRatio);
                //Bitmap resizeBitmap = new Bitmap(image, newwidthimg, newHeight);
                //Graphics thumbnailGraph = Graphics.FromImage(resizeBitmap);
                //thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                //thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                //thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //var imageRectangle = new Rectangle(0, 0, newwidthimg, newHeight);
                //thumbnailGraph.DrawImage(image, imageRectangle);
                //resizeBitmap.Save(path, ImageFormat.Png);
                //thumbnailGraph.Dispose();
                //resizeBitmap.Dispose();
                //image.Dispose();
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("userprofile");
            //Create or overwrite the "myblob" blob with contents from a local file.
            CloudBlockBlob cards = container.GetBlockBlobReference(newname);
            using (var fileStream = System.IO.File.OpenRead(path))//  using (var fileStream = System.IO.File.OpenRead(@"F:\Jabjai\JabJaiSmart\JabJaiSmart\images\uploadtostorage\" + image))
            {
                cards.UploadFromStream(fileStream);
            }

            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;
                    Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;
                    Console.WriteLine("Directory: {0}", directory.Uri);
                }
            }

            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/images/" + newname)))
            {
                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/images/" + newname));
            }

            return link;
        }

        private async static Task<string> UploadFile(HttpPostedFile postedFile, string containername, int schoolid)
        {
            string filename = "";
            //เช็ดไอดีนักเรียนตามรูปภาพ
            if (postedFile.ContentLength > 0)
            {
                filename = Guid.NewGuid().ToString();
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containername + "/" + schoolid);
            //Create or overwrite the "myblob" blob with contents from a local file.
            CloudBlockBlob cards = container.GetBlockBlobReference(filename);

            cards.Properties.ContentType = postedFile.ContentType;
            cards.UploadFromStream(postedFile.InputStream);

            // Loop over items within the container and output the length and URI.
            //foreach (IListBlobItem item in container.ListBlobs(null, false))
            //{
            //    //if (item.GetType() == typeof(CloudBlockBlob))
            //    //{
            //    //    CloudBlockBlob blob = (CloudBlockBlob)item;
            //    //    Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
            //    //}
            //    //else if (item.GetType() == typeof(CloudPageBlob))
            //    //{
            //    //    CloudPageBlob pageBlob = (CloudPageBlob)item;
            //    //    Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);
            //    //}
            //    //else if (item.GetType() == typeof(CloudBlobDirectory))
            //    //{
            //    //    CloudBlobDirectory directory = (CloudBlobDirectory)item;
            //    //    Console.WriteLine("Directory: {0}", directory.Uri);
            //    //}
            //}

            return string.Format("https://jabjaistorage.blob.core.windows.net/{0}/{1}/{2}", containername, schoolid, filename);
        }

        private static string UploadFile(FileUpload files, int newwidthimg, int userid)
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

                newname = "profile.png";
                link = "https://jabjaistorage.blob.core.windows.net/userprofile/" + userid + "/" + newname;
                //resize
                if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/images/profile/" + userid)))
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/images/profile/" + userid));
                }
                path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/images/profile/" + userid + "/" + newname));
                System.Drawing.Image image = System.Drawing.Image.FromStream(postedFile.InputStream);
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
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("userprofile/" + userid);
            //Create or overwrite the "myblob" blob with contents from a local file.
            CloudBlockBlob cards = container.GetBlockBlobReference(newname);
            using (var fileStream = System.IO.File.OpenRead(path))//  using (var fileStream = System.IO.File.OpenRead(@"F:\Jabjai\JabJaiSmart\JabJaiSmart\images\uploadtostorage\" + image))
            {
                cards.UploadFromStream(fileStream);
            }

            // Loop over items within the container and output the length and URI.
            //foreach (IListBlobItem item in container.ListBlobs(null, false))
            //{
            //    if (item.GetType() == typeof(CloudBlockBlob))
            //    {
            //        CloudBlockBlob blob = (CloudBlockBlob)item;
            //        Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
            //    }
            //    else if (item.GetType() == typeof(CloudPageBlob))
            //    {
            //        CloudPageBlob pageBlob = (CloudPageBlob)item;
            //        Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);
            //    }
            //    else if (item.GetType() == typeof(CloudBlobDirectory))
            //    {
            //        CloudBlobDirectory directory = (CloudBlobDirectory)item;
            //        Console.WriteLine("Directory: {0}", directory.Uri);
            //    }
            //}

            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/images/profile/" + userid + "/" + newname)))
            {
                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/images/profile/" + userid + "/" + newname));
            }

            return link;
        }

    }
}