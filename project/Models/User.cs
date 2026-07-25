using System;

namespace project.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string RelatedId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
