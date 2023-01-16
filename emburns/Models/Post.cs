using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public int Creator { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string Caption { get; set; } = null!;
        public int Points { get; set; }
        public bool Sticky { get; set; }
        public DateTime Created { get; set; }
        public int Status { get; set; }
        public int CommunityId { get; set; }
    }
}
