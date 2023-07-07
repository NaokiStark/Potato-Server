using emburns.Models;

namespace emburns.PotatoModels
{
    public class UserStats
    {
        public List<UserBadge> UserBadges { get; set; } = new List<UserBadge>();
        public List<int> Follows { get; set; } = new List<int>();
        public List<int> Followers { get; set; } = new List<int>();

        // lastposts no está implementado pero como cosas del front lo voy a dejar para futura implementacion asi que eso
        public List<string> Lastposts { get; set; } = new List<string>();

        public UserStats(List<UserBadge> badges) => UserBadges = badges;
    }
}
