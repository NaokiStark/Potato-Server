using emburns.Models;
using emburns.PotatoModels.Extras;

namespace emburns.PotatoModels
{
    public class FeedBaseQuery : PotatoModelBase
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public string Text { get; set; } = null!;
        public string Attachment { get; set; } = null!;
        public int AttachmentType { get; set; }
        public int ViaId { get; set; }
        public int ParentId { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }
        public int Wall { get; set; }
        public int Loves { get; set; }
        public bool Nsfw { get; set; }
        public bool Sticky { get; set; }
        public UserBaseQuery? ParentUser { get; set; }

        public virtual UserBaseQuery User { get; set; }

        public FeedBaseQuery(Feed feed)
        {

            Id = feed.Id;
            Userid = feed.Userid;
            Text = feed.Text;
            Attachment = feed.Attachment;
            AttachmentType = feed.AttachmentType;
            ViaId = feed.ViaId;
            ParentId = feed.ParentId;
            Created = feed.Created;
            Status = feed.Status;
            Wall = feed.Wall;
            Loves = feed.Loves;
            Nsfw = feed.Nsfw;
            Sticky = feed.Sticky;
            User = new UserBaseQuery(feed.User);
            if (feed.Via.Id == 0)
            {
                ParentUser = null;
            }
            else
            {
                ParentUser = new UserBaseQuery(feed.Via);
            }
        }
    }
}
