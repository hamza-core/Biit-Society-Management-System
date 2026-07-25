using System;
using System.Collections.Generic;

namespace project.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string Name { get; set; }

        public String Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public int SocietyID { get; set; }

        public bool RequiresFee { get; set; }

        public decimal FeeAmount { get; set; }
        public string TeamRequired { get; set; }
        public int MaxTeamSize { get; set; }
        public List<FormField> FormStructure { get; set; }

        public DateTime EventDate { get; set; }

        public String Venue { get; set; }

        public string EventApprovalStatus { get; set; }
        public bool IsDeleted { get; set; }

        public string CreatedByStudentAridNo { get; set; }
        public DateTime EventApprovalDate
        {
            get; set;
        }

        public string ApprovedByTeacherID { get; set; }
    }
}
