using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public string ObjectType { get; set; } = null!;
        public int ObjectId { get; set; }
        public int ActionType { get; set; }
        public string Notes { get; set; } = null!;
    }
}
