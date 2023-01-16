using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class Block
    {
        public int Id { get; set; }
        public int Blocker { get; set; }
        public int Blocked { get; set; }
        public DateTime Date { get; set; }
    }
}
