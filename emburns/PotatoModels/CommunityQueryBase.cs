using emburns.Models;
using emburns.PotatoModels.Extras;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace emburns.PotatoModels
{
    public class CommunityQueryBase : PotatoModelBase
    {
        public string Name { get; set; } = null!;
        public UserBaseQuery Creator { get; set; }
        public string Description { get; set; } = null!;
        public CategoryQuery Category { get; set; }
        public string Avatar { get; set; } = null!;
        public string Cover { get; set; } = null!;
        public string Background { get; set; } = null!;
        public DateTime Created { get; set; }
        public int Status { get; set; }
        public List<CommunityMember> Members { get; set; } = new List<CommunityMember>();
        public int MembersCount { get; set; } = 0;

        public CommunityQueryBase(Community community, bool withMembers=false)
        {
            Id = community.Id;
            Name = community.Name;
            Creator = new UserBaseQuery(community.CreatorNavigation);
            Description = community.Description;
            Category = new CategoryQuery(community.Category);
            Avatar = community.Avatar;
            Cover = community.Cover;
            Background = community.Background;
            Created = community.Created;
            Status = community.Status;

            if (withMembers)
            {
                Members = CommunityMember.FromDb(community.CommunitiesMembers);
            }
            
            MembersCount = community.CommunitiesMembers.Count();
        }

    }
}
