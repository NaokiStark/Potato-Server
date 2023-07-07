using emburns.Models;

namespace emburns.PotatoModels
{
    public class ReactionQuery
    {
        public string ReactionText { get; set; } = null!;
        public string ReactionEmoji { get; set; } = null!;
        public ReactionQuery(ReactionsList reaction)
        {
            ReactionText = reaction.ReactionText;
            ReactionEmoji = reaction.ReactionEmoji;
        }
        public ReactionQuery() { }
    }
}
