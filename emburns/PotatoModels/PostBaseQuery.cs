using emburns.Models;
using System.Text.RegularExpressions;

namespace emburns.PotatoModels
{
    public class PostBaseQuery : PotatoModelBase
    {
        public int CreatorId { get; set; }
        public UserBaseQuery? User { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Caption { get; set; }
        public int Points { get; set; }
        public DateTime Created { get; set; }
        public int Status { get; set; }
        public int CommunityId { get; set; }
        public CommunityQueryBase Community { get; set; }

        public PostBaseQuery(Post post, bool includeMetadata = false)
        {
            Id = post.Id;
            CreatorId = post.Creator;
            User = new UserBaseQuery(post.CreatorNavigation);
            Title = post.Title;
            if (includeMetadata)
            {
                Body = post.Body;
            }
            else
            {
                Body = "Body empty, try to change includeMetadata to true";
            }

            Caption = post.Caption;
            if (string.IsNullOrWhiteSpace(post.Caption))
            {
                Caption = post.Community.Avatar;
            }

            Points = post.Points;
            Created = post.Created;
            Status = post.Status;
            CommunityId = post.CommunityId;
            Community = new CommunityQueryBase(post.Community);
        }
    }
}
