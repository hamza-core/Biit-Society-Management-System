using System;
using System.Collections.Generic;

namespace project.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public int SocietyId { get; set; }
        public bool TeamRequired { get; set; }
        public int MaxTeamSize { get; set; }
        public List<FormField> FormStructure { get; set; }
        public bool IsDeleted { get; set; }
    }
}
