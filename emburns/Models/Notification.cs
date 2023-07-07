using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int Userid { get; set; }
        public int From { get; set; }
        public string Info { get; set; } = null!;
        public bool Readed { get; set; }
        public bool Wsqueue { get; set; }
        public bool? Listable { get; set; }
        public int Meta { get; set; }
    }
}
