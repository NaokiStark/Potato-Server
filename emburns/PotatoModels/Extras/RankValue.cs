using emburns.Models;

namespace emburns.PotatoModels.Extras
{
    public class RankValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Fullname { get; set; }
        public decimal RequiredPoints { get; set; }
        
        public RankValue(Rank rank)
        {
            Id = rank.Id;
            Name = rank.Name;
            Fullname = rank.Fullname;
            RequiredPoints = rank.RequiredPoints;
        }
    }
}
