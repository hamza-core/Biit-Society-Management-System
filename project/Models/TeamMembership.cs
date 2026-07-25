using System;

namespace project.Models
{
    public class TeamMembership
    {
        public int MembershipID { get; set; }
        public int TeamID { get; set; }
        public string AridNo { get; set; }
        public string Role { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
