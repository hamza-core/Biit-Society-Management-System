using System;

namespace project.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string RecipientAridNo { get; set; }
        public string SenderId { get; set; }
        public int SocietyID { get; set; }
        public string EventId { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
