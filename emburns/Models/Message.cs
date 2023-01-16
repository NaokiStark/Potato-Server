using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int? Sender { get; set; }
        public int? Addressee { get; set; }
        public string? Message1 { get; set; }
    }
}
