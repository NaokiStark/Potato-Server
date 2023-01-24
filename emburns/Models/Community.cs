using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class Community
    {
        public Community()
        {
            Posts = new HashSet<Post>();
        }

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

        public virtual PostCategory Category { get; set; } = null!;
        public virtual User CreatorNavigation { get; set; } = null!;
        public virtual ICollection<Post> Posts { get; set; }
    }
}
