using emburns.PotatoModels.Enums;

namespace emburns.PotatoModels.Extras
{
    public class PostedAttachment
    {
        public string? Raw { get; set; }
        public string? Provider { get; set; }
        public string? Id { get; set; }
        public AttachmentType Type { get; set; } = AttachmentType.None;
        public IFormFile? FileAttachment { get; set; } = null;
    }
}
