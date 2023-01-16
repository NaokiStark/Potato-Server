using System;
using System.Collections.Generic;

namespace emburns.Models
{
    public partial class UsersRequest
    {
        public int Id { get; set; }
        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateOnly Created { get; set; }
        public string Avatar { get; set; } = null!;
        public string Cover { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Background { get; set; } = null!;
        public bool Status { get; set; }
        public bool Moderator { get; set; }
        public bool Admin { get; set; }
        public float Rank { get; set; }
        public string Quote { get; set; } = null!;
        public DateTime Lastpost { get; set; }
        public string Wshash { get; set; } = null!;
        public string RequestHash { get; set; } = null!;
    }
}
