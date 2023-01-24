using emburns.Models;

namespace emburns.PotatoModels
{
    public class CommunityQueryBase : PotatoModelBase
    {
        public string Name { get; set; } = null!;
        public int Creator { get; set; }
        public UserBaseQuery UserCreator { get; set; }
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }
        public CategoryQuery Category { get; set; }
        public string Avatar { get; set; } = null!;
        public string Cover { get; set; } = null!;
        public string Background { get; set; } = null!;
        public DateTime Created { get; set; }
        public int Status { get; set; }
        public string Members { get; set; } = null!;

        public CommunityQueryBase(Community community) {
            Id = community.Id;
            Name = community.Name;
            Creator = community.Creator;
            UserCreator = new UserBaseQuery(community.CreatorNavigation);
            Description = Description;
            CategoryId = community.CategoryId;
            Category = new CategoryQuery(community.Category);
            Avatar = community.Avatar;
            Cover = community.Cover;
            Background = community.Background;
            Created = community.Created;
            Status = community.Status;
            Members = community.Members;
        }
    }
}
