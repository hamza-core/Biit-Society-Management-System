using System;

namespace project.Models
{
    public class EventFile
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
         public string FileType { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
