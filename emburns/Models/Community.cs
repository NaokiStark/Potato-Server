using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class Community
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Creator { get; set; }
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }
        public string Avatar { get; set; } = null!;
        public string Cover { get; set; } = null!;
        public string Background { get; set; } = null!;
        public DateTime Created { get; set; }
        public int Status { get; set; }
        public string Members { get; set; } = null!;
    }
}
