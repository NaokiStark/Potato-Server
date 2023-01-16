using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class Reaction
    {
        public int Id { get; set; }
        public int ElementType { get; set; }
        public int ElementId { get; set; }
        public string Reactions { get; set; } = null!;
    }
}
