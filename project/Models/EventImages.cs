using System;

namespace project.Models
{
    public class EventImage
    {
        public int ImageId { get; set; }
        public int EventId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
