using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class PostCategory
    {
        public PostCategory()
        {
            Communities = new HashSet<Community>();
        }

        public int Id { get; set; }
        public string Icon { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Shortname { get; set; } = null!;
        public string CatImage { get; set; } = null!;

        public virtual ICollection<Community> Communities { get; set; }
    }
}
