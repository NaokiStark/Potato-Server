using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class Feed
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public string Text { get; set; } = null!;
        public string Attachment { get; set; } = null!;
        public int AttachmentType { get; set; }
        public int ViaId { get; set; }
        public int ParentId { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }
        public int Wall { get; set; }
        public int Loves { get; set; }
        public bool Nsfw { get; set; }
        public bool Sticky { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
