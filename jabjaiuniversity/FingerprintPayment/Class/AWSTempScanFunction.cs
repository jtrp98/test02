using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FingerprintPayment.Class
{
    public class AWSTempScanFunction
    {
        private const string BUCKET_NAME = "face-rekog";
        private const int FACE_THRESHOLD = 90;

        private IAmazonS3 s3Client;

        private int COUNT_NO;
        private int SCHOOL_ID;
        private string COLLECTION_ID;

        public AWSTempScanFunction(int SchoolID)
        {
            SCHOOL_ID = SchoolID;

            COLLECTION_ID = "face-collect-" + SCHOOL_ID;

            COUNT_NO = 1;

            InitialS3Client();
        }

        private void InitialS3Client()
        {
            var accessKeyID = "AKIATGHSKGZ2LCBCT3JH";
            var secretKey = "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";
            var credentials = new BasicAWSCredentials(accessKeyID, secretKey);

            s3Client = new AmazonS3Client(credentials, RegionEndpoint.APSoutheast1);
        }

        public TempScanLastVersion ReadTempScanVersionS3Object()
        {
            TempScanLastVersion tempScanLastVersion = new TempScanLastVersion();

            try
            {
                var key = $"Installer/version.txt";

                var request = new GetObjectRequest()
                {
                    BucketName = BUCKET_NAME,
                    Key = key
                };

                GetObjectResponse response = s3Client.GetObject(request);

                StreamReader reader = new StreamReader(response.ResponseStream);

                string json = reader.ReadToEnd();

                tempScanLastVersion = JsonConvert.DeserializeObject<TempScanLastVersion>(json);
                tempScanLastVersion.setupx86.url = GeneratePreSignedURL(tempScanLastVersion.setupx86.name, 3000);
                tempScanLastVersion.setupx64.url = GeneratePreSignedURL(tempScanLastVersion.setupx64.name, 3000);
                tempScanLastVersion.IsSuccess = true;
            }
            catch (AmazonS3Exception err)
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

        public List<TM_Configuration> ReadTempScanVersionS3Object(string key)
        {
            List<TM_Configuration> configuration = new List<TM_Configuration>();

            try
            {
                var request = new GetObjectRequest()
                {
                    BucketName = BUCKET_NAME,
                    Key = key
                };

                GetObjectResponse response = s3Client.GetObject(request);

                StreamReader reader = new StreamReader(response.ResponseStream);

                string json = reader.ReadToEnd();

                configuration = JsonConvert.DeserializeObject<List<TM_Configuration>>(json);

                return configuration;
            }
            catch (AmazonS3Exception err)
            {
                //tempScanLastVersion.IsSuccess = false;
                //tempScanLastVersion.Message = err.Message;
                //tempScanLastVersion.StackTrace = err.StackTrace;
            }
            catch (Exception err)
            {
                //tempScanLastVersion.IsSuccess = false;
                //tempScanLastVersion.Message = err.Message;
                //tempScanLastVersion.StackTrace = err.StackTrace;
            }

            return configuration;
        }

        public string GeneratePreSignedURL(string fileName, int expireInSeconds)
        {
            string urlString = string.Empty;

            try
            {
                var key = $"Installer/{fileName}";

                GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
                {
                    BucketName = BUCKET_NAME,
                    Key = key,
                    Expires = DateTime.Now.AddSeconds(expireInSeconds)
                };

                urlString = s3Client.GetPreSignedURL(request);
            }
            catch (AmazonS3Exception err)
            {
            }
            catch (Exception err)
            {
            }

            return urlString;
        }

    }

}

public class TM_Configuration
{ 
     public string Key { get; set; }
     public string Value { get; set; }
}