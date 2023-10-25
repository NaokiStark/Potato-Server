using emburns.PotatoModels.Extras;
using emburns.Utils;
using System.Text.RegularExpressions;

namespace emburns.Media
{
    public class MediaService
    {
        public PostedAttachment Attachment { get; private set; }
        public ValidationStatus Status { get; private set; }

        /// <summary>
        /// Initializes MediaService and validates the PostedAttachment
        /// </summary>
        /// <param name="attachment">Received attachment</param>
        public MediaService(PostedAttachment attachment)
        {
            Attachment = attachment;
            Status = Validate();
        }

        public ValidationStatus Validate()
        {
            if (Attachment.Type == PotatoModels.Enums.AttachmentType.External && Attachment.Provider == "youtube")
            {
                if (Attachment.Raw == null)
                {
                    return ValidationStatus.EmptyAttachment;
                }

                var youtubeMatch = Regex.Match(Attachment.Raw, @"(?:youtube\.com\/\S*(?:(?:\/e(?:mbed))?\/|watch\?(?:\S*?&?v\=))|youtu\.be\/)([a-zA-Z0-9_-]{6,11})");
                if (!youtubeMatch.Success)
                {
                    return ValidationStatus.InvalidVideo;
                }

                string id = youtubeMatch.Groups[1].Value;
                Attachment.Id = id;
                return ValidationStatus.Ok;
            }

            if (Attachment.Type == PotatoModels.Enums.AttachmentType.Image)
            {
                var format = Utils.ImageValidation.GetImageFormat(Attachment.FileAttachment.OpenReadStream());

                if (format == Utils.ImageFormat.unknown)
                {
                    return ValidationStatus.InvalidImageFormat;
                }

                var bucketService = ConfigurationBridge.ConfigManager.GetSection("MediaServiceBucket").Get<string>();
                string result = string.Empty;

                switch (bucketService)
                {
                    case "S3":

                        var s3Service = new S3MediaService();
                        result = s3Service.UploadFile(Attachment.FileAttachment, Attachment.Type);

                        break;
                    case "BunnyCDN":
                        
                        var bunny = new BunnyMediaService();
                        result = bunny.UploadFile(Attachment.FileAttachment, Attachment.Type);

                        break;
                }

                if (string.IsNullOrWhiteSpace(result))
                {
                    return ValidationStatus.ImageUploadError;
                }

                Attachment.Raw = result;
                return ValidationStatus.Ok;
            }

            return ValidationStatus.Undefined;
        }
    }

    /// <summary>
    /// Result of media validation
    /// </summary>
    public enum ValidationStatus
    {
        /// <summary>
        /// No problems
        /// </summary>
        Ok,
        /// <summary>
        /// Video id invalid
        /// </summary>
        InvalidVideo,
        /// <summary>
        /// Attachment invalid
        /// </summary>
        IvalidAttachment,
        /// <summary>
        /// Attachment is emtpy
        /// </summary>
        EmptyAttachment,
        /// <summary>
        /// Invalid image format
        /// </summary>
        InvalidImageFormat,
        /// <summary>
        /// MediaService image upload error
        /// </summary>
        ImageUploadError,
        /// <summary>
        /// Not defined validation (is not an ok validation, consider it an error)
        /// </summary>
        Undefined,
    }
}
