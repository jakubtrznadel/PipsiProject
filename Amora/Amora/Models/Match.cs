using System;

namespace Amora.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public int MatchedUserId { get; set; }

        public virtual RegisterViewModel User { get; set; }
        public virtual RegisterViewModel MatchedUser { get; set; }
    }
}
