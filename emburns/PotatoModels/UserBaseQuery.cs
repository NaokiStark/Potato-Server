using emburns.Models;
using emburns.PotatoModels.Extras;
using System;

namespace emburns.PotatoModels
{
    public class UserBaseQuery : PotatoModelBase
    {
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime Created { get; set; }
        public string Avatar { get; set; } = null!;
        public string Cover { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Background { get; set; } = null!;
        public bool Status { get; set; }
        public bool Admin { get; set; }
        public decimal Rank { get; set; }
        public string Quote { get; set; } = null!;
        public string? RankName { get; set; }
        public DateOnly Donation { get; set; }

        public UserBaseQuery(User userModel)
        {
            Id = userModel.Id;
            Username = userModel.User1;
            Name = userModel.Name;
            Lastname = userModel.Lastname;
            Created = userModel.Created;
            Avatar = userModel.Avatar;
            Cover = userModel.Cover;
            Country = userModel.Country;
            Background = userModel.Background;
            Status = userModel.Status;
            Admin = userModel.Admin;
            Rank = userModel.Rank;
            Quote = userModel.Quote;
            Donation = userModel.Donation;            
        }

        public void FetchUserRank(IEnumerable<RankValue> ranks)
        {
            string rank = ranks
                        .Where(r => decimal.Truncate(r.RequiredPoints) == decimal.Truncate(Rank))
                        .ToList().FirstOrDefault().Fullname;

            RankName = rank;
        }
    }
}
