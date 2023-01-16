using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class PostComment
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public int Postid { get; set; }
        public string Text { get; set; } = null!;
        public DateTime Created { get; set; }
        public bool Status { get; set; }
    }
}
