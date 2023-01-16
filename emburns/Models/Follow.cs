using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class Follow
    {
        public int Id { get; set; }
        public int Follower { get; set; }
        public int Following { get; set; }
    }
}
