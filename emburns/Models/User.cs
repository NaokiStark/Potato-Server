using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Communities = new HashSet<Community>();
            FeedUsers = new HashSet<Feed>();
            FeedVia = new HashSet<Feed>();
            PostComments = new HashSet<PostComment>();
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string User1 { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime Created { get; set; }
        public string Avatar { get; set; } = null!;
        public string Cover { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Background { get; set; } = null!;
        public bool Status { get; set; }
        public bool Moderator { get; set; }
        public bool Admin { get; set; }
        public decimal Rank { get; set; }
        public string Quote { get; set; } = null!;
        public DateTime Lastpost { get; set; }
        public string Wshash { get; set; } = null!;
        public DateTime AnonExpires { get; set; }
        public DateOnly Donation { get; set; }
        public int RemainPoints { get; set; }
        public int PointDate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Community> Communities { get; set; }
        public virtual ICollection<Feed> FeedUsers { get; set; }
        public virtual ICollection<Feed> FeedVia { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
