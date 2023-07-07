using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class UserBadge
    {
        public int Id { get; set; }
        public string Icon { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
