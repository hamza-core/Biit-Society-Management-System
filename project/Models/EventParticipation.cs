using System;
using System.Collections.Generic;

namespace project.Models
{
    public class EventParticipation
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string ParticipantAridNo { get; set; }
        public string ParticipantNumber { get; set; }
        public bool IsPresent { get; set; }
        public DateTime RegisteredAt { get; set; }
        public Dictionary<string, string> AdditionalData { get; set; }
    }
}
