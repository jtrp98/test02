using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FingerprintPayment.Infrastructure
{
    public class AzureStorageMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        private readonly CloudBlobContainer _blobContainer;
        private readonly string[] _supportedMimeTypesImage = { "image/png", "image/jpeg", "image/jpg" };

        private readonly string[] _supportedMimeTypePDF = { "application/pdf" };
        private readonly string[] _supportedMimeTypeExecl = { "application/vnd.ms-excel" };
        private readonly string[] _supportedMimeTypeWord = { "application/msword" };


        public AzureStorageMultipartFormDataStreamProvider(CloudBlobContainer blobContainer) : base("azure")
        {
            _blobContainer = blobContainer;
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (headers == null) throw new ArgumentNullException(nameof(headers));
            if (headers.ContentType == null) return Stream.Null;

            if (string.IsNullOrEmpty(ContentType(headers)))
            {
                var ContentType = headers.ContentType.ToString();
                throw new NotSupportedException("Only jpeg and png are supported" + ContentType);
            }

            // Generate a new filename for every new blob
            var fileName = Guid.NewGuid().ToString();

            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(fileName);

            if (headers.ContentType != null)
            {
                // Set appropriate content type for your uploaded file
                blob.Properties.ContentType = ContentType(headers);
            }

            this.FileData.Add(new MultipartFileData(headers, blob.Name));

            return blob.OpenWrite();
        }

        private string ContentType(HttpContentHeaders headers)
        {
            switch (headers.ContentType.ToString().ToLower())
            {
                case "image/png": //PNG
                    return headers.ContentType.MediaType;
                case "image/jpeg": //JPEG
                    return headers.ContentType.MediaType;
                case "image/jpg": //JPG
                    return headers.ContentType.MediaType;
                case "application/pdf": //PDF
                    return "application/pdf";
                case "application/vnd.ms-excel": //EXECL 2003
                    return "application/vnd.ms-excel";
                case "application/msword": // WORD 2003
                    return "application/msword";
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": //EXECL 2007
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document": // WORD 2007
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                default:
                    return "";
            }
        }
    }
}
