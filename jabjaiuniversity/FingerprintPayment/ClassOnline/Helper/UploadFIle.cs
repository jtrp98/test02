using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using EllipticCurve.Utils;
using System.Net.Http;
using OBS.Model;
using OBS;
using System.Net.Mime;

namespace FingerprintPayment.ClassOnline.Helper
{
    public static class UploadFile
    {
        private static ObsClient client;
        private static string bucketName = "onlineclassroom-system";
        private static string endpoint = "obs.ap-southeast-2.myhuaweicloud.com";
        private static string AK = "UVIKYLYS2BECK7RGLFVN";
        private static string SK = "LCQcPRii1eSYVHInqMvJU0TklY5lH6VrUeikwhdK";

        private static string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=jabjaistorage;AccountKey=/fNzuPO0JOcsMfe8e/recdXUtYqC8cXk4bQL8BUgdy0sFPmhq8ogrePc6IJ42mU0nxfqZBckb3uReRywXtfRdA==";
        

        public static string UploadFileParentCardBG(HttpPostedFile files, string folder, int schoolid)
        {
            try
            {
                ObsConfig config = new ObsConfig();
                config.Endpoint = endpoint;
                client = new ObsClient(AK, SK, config);

                string fileName = files.FileName;

                string contentType = GetContentType(fileName);

                //var correctdata = data.Replace("data:image/pdf;base64,", "").Replace('-', '+').Replace('_', '/');
                //var contents = new MemoryStream(Convert.FromBase64String(correctdata));

                string guidFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);

                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = "cards-system",
                    ObjectKey = folder + "/" + schoolid + "/" + guidFileName,
                    InputStream = files.InputStream,
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

        public static string UploadFileFromBase64(string fileName, string data, string folder, int schoolid, int id)
        {

            try
            {
                ObsConfig config = new ObsConfig();
                config.Endpoint = endpoint;
                client = new ObsClient(AK, SK, config);

                string contentType = GetContentType(fileName);

                var correctdata = data.Replace("data:image/pdf;base64,", "").Replace('-', '+').Replace('_', '/');
                var contents = new MemoryStream(Convert.FromBase64String(correctdata));

                string guidFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(fileName);

                
                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    ObjectKey = folder + "/" + schoolid + "/" + id + "/" + guidFileName,
                    InputStream = contents,
                    //ContentType = contentType,
                    
                };
              
                //if (contentType == "application/pdf")
                //    request.Metadata.Add("Content-Disposition", $@"inline; filename=""{fileName}"";");


                PutObjectResponse response = client.PutObject(request);
                return HttpUtility.UrlDecode(response.ObjectUrl);
            }
            catch (Exception ex)
            {
                return "";
            }
           
        }

        public static string UploadFileHttpRequestBase(HttpPostedFile files, string folder, int schoolid, int id)
        {
            try
            {
                ObsConfig config = new ObsConfig();
                config.Endpoint = endpoint;
                client = new ObsClient(AK, SK, config);

                string fileName = files.FileName;

                string contentType = GetContentType(fileName);

                //var correctdata = data.Replace("data:image/pdf;base64,", "").Replace('-', '+').Replace('_', '/');
                //var contents = new MemoryStream(Convert.FromBase64String(correctdata));

                string guidFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);

                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    ObjectKey = folder + "/" + schoolid + "/" + guidFileName,
                    InputStream = files.InputStream,
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

        private static string GetContentType(string fileName)
        {
            var contentType = "";
            switch (Path.GetExtension(fileName).ToLower())
            {
                case ".pdf":
                    contentType = "application/pdf";
                    break;

                case ".mp4":
                    contentType = "video/mp4";
                    break;

                case ".mp3":
                    contentType = "audio/mpeg";
                    break;

                case ".mov":
                    contentType = "video/quicktime";
                    break;

                case ".avi":
                    contentType = "video/x-msvideo";
                    break;

                case ".wmv":
                    contentType = "video/x-ms-wmv";
                    break;

                case ".mkv":
                    contentType = "video/x-matroska";
                    break;

                case ".png":
                    contentType = "image/png";
                    break;
                case ".jpg":
                    contentType = "image/jpg";
                    break;
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;

                default:
                    break;
            }

            return contentType;
        }

    }

    public static class CommonHelper
    {
        public static string SubstringFileName(this string fileName, int len)
        {
            var a = Path.GetFileNameWithoutExtension(fileName);
            var b = Path.GetExtension(fileName);

            if (a.Length > len)
            {
                return a.Substring(0, len) + b;
            }

            return fileName;
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
                case "image/jpg":
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