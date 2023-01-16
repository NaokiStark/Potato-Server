using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class KyunScore
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public int Beatmapid { get; set; }
        public long Totalscore { get; set; }
        public int Totalcombo { get; set; }
        public string Beatmaptitle { get; set; } = null!;
        public string Beatmapartist { get; set; } = null!;
        public string Beatmapversion { get; set; } = null!;
    }
}
