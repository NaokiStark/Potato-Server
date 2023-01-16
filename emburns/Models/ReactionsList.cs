using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class ReactionsList
    {
        public int Id { get; set; }
        public string ReactionText { get; set; } = null!;
        public string ReactionEmoji { get; set; } = null!;
    }
}
