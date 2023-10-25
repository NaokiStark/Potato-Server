using BunnyCDN.Net.Storage;
using emburns.Utils;
using System.IO;
using emburns.PotatoModels.Enums;

namespace emburns.Media
{
    public class BunnyMediaService : IStorageService
    {
        private string StorageZoneName { get; set; }
        private string ApiAccessKey { get; set; }
        private string Folder { get; set; }

        public BunnyMediaService()
        {
            StorageZoneName = ConfigurationBridge.ConfigManager.GetSection("BunnyCDN:storageZoneName").Get<string>();
            ApiAccessKey = ConfigurationBridge.ConfigManager.GetSection("BunnyCDN:apiAccessKey").Get<string>();
            Folder = ConfigurationBridge.ConfigManager.GetSection("BunnyCDN:folder").Get<string>();
        }

        public string UploadFile(IFormFile file, PotatoModels.Enums.AttachmentType type)
        {
            var filename = $"{(int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds}-{file.FileName}";
            var folder = string.IsNullOrWhiteSpace(Folder) ? "" : $"{Folder}/";
            var ctype = "i";

            switch (type)
            {
                case AttachmentType.Webm:
                    ctype = "v";
                    break;
            }

            try
            {
                var bunnyCDNStorage = new BunnyCDNStorage(StorageZoneName, ApiAccessKey, "br");
                var r = bunnyCDNStorage.UploadAsync(file.OpenReadStream(), $"/{folder}/{ctype}/{filename}");
                r.Wait();

                return $"https://embr.b-cdn.net/{ctype}/{filename}";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return "";
            }
        }
    }
}
