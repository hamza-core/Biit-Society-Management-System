using System;
using System.Collections.Generic;

namespace project.Models
{
    public class EventParticipation
    {
        public int ParticipationID { get; set; }
        public int EventID { get; set; }
        public string AridNo { get; set; }
        public string Role { get; set; }
        public bool FeePaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsDeleted { get; set; }
        public string AdditionalData { get; set; }
    }
}
