using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class CiSession
    {
        public string Id { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public uint Timestamp { get; set; }
        public byte[] Data { get; set; } = null!;
    }
}
