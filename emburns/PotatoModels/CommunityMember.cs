using emburns.Models;

namespace emburns.PotatoModels
{
    public class CommunityMember
    {
        public UserBaseQuery User { get; set; } = null!;
        public DateTime JoinDate { get; set; }
        public int CommunityId { get; set; } = 0;

        public CommunityMember(CommunitiesMember member)
        {
            User = new UserBaseQuery(member.User);
            JoinDate = member.JoinDate;
            CommunityId = member.Community.Id;
        }

        public static List<CommunityMember> FromDb(ICollection<CommunitiesMember> members)
        {
            var lst = new List<CommunityMember>();

            foreach (CommunitiesMember member in members)
            {
                lst.Add(new CommunityMember(member));
            }
            return lst;
        }
    }
}
