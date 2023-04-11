using emburns.PotatoModels.Enums;
using emburns.Utils;
using System.Net.Mail;
using System.Text.Json;

namespace emburns.PotatoModels.Extras
{
    public class FeedAttachment
    {
        public string? Raw { get; set; }
        public string? Provider { get; set; }
        public string? Id { get; set; }
        public AttachmentType Type { get; set; } = AttachmentType.None;

        public FeedAttachment(string? RawAttachment)
        {
            if (RawAttachment == null)
            {
                return;
            }

            try
            {
                Raw = RawAttachment;
                FeedAttachment? feed = JsonSerializer.Deserialize<FeedAttachment>(RawAttachment);
                if (feed != null)
                {
                    Provider = feed.Provider;
                    Id = feed.Provider;
                    Type = AttachmentType.External;
                }

            }
            catch //null or error or img
            {
                Raw = RawAttachment;
                if (Raw.StartsWith("https://"))
                {
                    bool hasExt = false;
                    foreach (string ext in ConfigurationBridge.AttachmentAllowedExtensions)
                    {
                        if (Raw.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                        {
                            hasExt = true;
                            break;
                        }
                    }

                    if (hasExt)
                    {
                        if (Raw.EndsWith(".webm"))
                        {
                            Type = AttachmentType.Webm;
                        }
                        else
                        {
                            Type = AttachmentType.Image;
                        }
                        Provider = "Potato";
                        Id = Raw;
                    }
                }
            }
        }
    }
}
