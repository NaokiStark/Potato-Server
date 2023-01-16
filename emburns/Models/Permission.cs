using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class Permission
    {
        public int Id { get; set; }
        public string Permissions { get; set; } = null!;
        public int RankId { get; set; }
    }
}
