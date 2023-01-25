using emburns.Models;

namespace emburns.PotatoModels
{
    public class LoveQuery
    {
        public UserBaseQuery User { get; set; }
        public ReactionQuery Reaction { get; set; }
        public LoveQuery(Lofe love)
        {
            User = new UserBaseQuery(love.User);
            Reaction = new ReactionQuery(love.ReactionNavigation);
        }

        public static List<LoveQuery> GetAll(IEnumerable<Lofe> loves)
        {
            var loveQuery = new List<LoveQuery>();            

            loves.ToList().ForEach(
                l => loveQuery.Add(new LoveQuery(l))
            );

            return loveQuery;
        }
    }
}
