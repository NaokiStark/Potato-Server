using Amazon.S3;
using Amazon.S3.Transfer;
using emburns.Utils;
using emburns.PotatoModels.Enums;

namespace emburns.Media
{
    public class S3MediaService : IStorageService
    {
        private string AccessKey { get; set; }
        private string SecretKey { get; set; }
        private string ServiceUrl { get; set; }
        private string BucketName { get; set; }

        public S3MediaService()
        {
            AccessKey = ConfigurationBridge.ConfigManager.GetSection("S3Opt:accessKey").Get<string>();
            SecretKey = ConfigurationBridge.ConfigManager.GetSection("S3Opt:secretKey").Get<string>();
            ServiceUrl = ConfigurationBridge.ConfigManager.GetSection("S3Opt:serviceURL").Get<string>();
            BucketName = ConfigurationBridge.ConfigManager.GetSection("S3Opt:bucketName").Get<string>();
        }

        public string UploadFile(IFormFile file, AttachmentType type)
        {
            var filename = $"{(int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds}-{file.FileName}";

            var config = new AmazonS3Config
            {
                ServiceURL = ServiceUrl,
            };

            var client = new AmazonS3Client(AccessKey, SecretKey, config);

            try
            {
                var fileTransferUtil = new TransferUtility(client);
                var fileTransferUtilReq = new TransferUtilityUploadRequest() { 
                    BucketName = BucketName,
                    InputStream = file.OpenReadStream(),
                    StorageClass = S3StorageClass.Standard,
                    Key = filename,
                    CannedACL = S3CannedACL.PublicRead,
                };

                fileTransferUtil.Upload(fileTransferUtilReq);
            }
            catch(Exception ex)
            {                
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return "";
            }

            return filename;
        }
    }
}
