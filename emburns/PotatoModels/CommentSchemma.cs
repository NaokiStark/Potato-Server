using emburns.Models;
using emburns.PotatoModels.Enums;
using emburns.PotatoModels.Extras;
using Microsoft.EntityFrameworkCore;

namespace emburns.PotatoModels
{
    public class CommentSchemma
    {
        public string? Text { get; set; }
        public string? Attachment { get; set; }
        public int? AttachmentType { get; set; }
        public bool? Nsfw { get; set; } = false;
        public int Parent { get; set; } = 0;

        public async Task<FeedError> Validate(mokyuContext context)
        {

            Text = Text.Trim();

            // Too long
            if (Text.Length > 500)
            {
                return FeedError.TextTooLong;
            }

            // Too short
            if (string.IsNullOrEmpty(Text))
            {
                return FeedError.TextTooShort;
            }

            // Empty shout
            if (string.IsNullOrEmpty(Attachment) && string.IsNullOrEmpty(Text))
            {
                return FeedError.FeedEmpty;
            }

            var feedAttachment = new FeedAttachment(Attachment);

            // Empty feed
            if (feedAttachment == null && string.IsNullOrEmpty(Text))
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
            if (gettedType != Enums.AttachmentType.None &&
                (string.IsNullOrEmpty(feedAttachment.Id.Trim())
                || string.IsNullOrEmpty(feedAttachment.Provider.Trim()))
                )
            {
                return FeedError.InvalidAttachment;
            }

            /**
             * ToDo: Add Extenal validations (eg: youtube | etc...)
             */

            // Check parent shout exists
            if (Parent == 0)
            {
                return FeedError.ParentNotFoundOrInvalid;
            }

            var feed = await context.Feeds
                .Select(x => new { x.Id })
                .Where(f => f.Id == Parent)
                .ToListAsync();

            if (feed.Count < 1)
            {
                return FeedError.ParentNotFoundOrInvalid;
            }

            return FeedError.None;
        }
    }
}
