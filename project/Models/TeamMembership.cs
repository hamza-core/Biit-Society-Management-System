using System;

namespace project.Models
{
    public class TeamMembership
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string MemberAridNo { get; set; }
        public string Role { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
