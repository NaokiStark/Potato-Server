using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class CommunitiesMember
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CommunityId { get; set; }
        public DateTime JoinDate { get; set; }

        public virtual Community Community { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
