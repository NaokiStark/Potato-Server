using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class Lofe
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public int Post { get; set; }
        public int Reaction { get; set; }

        public virtual Feed PostNavigation { get; set; } = null!;
        public virtual ReactionsList ReactionNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
