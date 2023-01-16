using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class Rank
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public int RequiredPoints { get; set; }
    }
}
