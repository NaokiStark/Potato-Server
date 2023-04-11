using emburns.PotatoModels.Enums;
using emburns.PotatoModels.Extras;

namespace emburns.PotatoModels
{
    public class FeedSchemma
    {
        public string? Text { get; set; }
        public string? Attachment { get;set; }
        public int? AttachmentType { get; set; }
        public bool? Nsfw { get; set; } = false;

        public FeedError Validate()
        {
            Text = Text.Trim();

            // Too long
            if(Text.Length > 500)
            {
                return FeedError.TextTooLong;
            }

            // Too short
            if(string.IsNullOrEmpty(Text))
            {
                return FeedError.TextTooShort;
            }

            // Empty shout
            if(string.IsNullOrEmpty(Attachment) && string.IsNullOrEmpty(Text))
            {
                return FeedError.FeedEmpty;
            }

            var feedAttachment = new FeedAttachment(Attachment);
            
            // Empty feed
            if(feedAttachment == null && string.IsNullOrEmpty(Text))
            {
                return FeedError.FeedEmpty;
            }

            Enums.AttachmentType gettedType = (AttachmentType == null) ? 
                Enums.AttachmentType.None : (Enums.AttachmentType)AttachmentType;

            // Get if getted type and resolved type (by FeedAttachment()) is ok or faked
            if (gettedType != feedAttachment.Type)
            {
                return FeedError.InvalidAttachmentType;
            }

            // Get if invalid Attachment
            if(gettedType != Enums.AttachmentType.None && 
                (string.IsNullOrEmpty(feedAttachment.Id.Trim()) 
                || string.IsNullOrEmpty(feedAttachment.Provider.Trim()))
                )
            {
                return FeedError.InvalidAttachment;
            }

            /**
             * ToDo: Add Extenal validations (eg: youtube | etc...)
             */

            return FeedError.None;
        }
    }
}
