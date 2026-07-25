using System;

namespace project.Models
{
    public class FormField
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public bool IsRequired { get; set; }

        public string FieldOptions { get; set; }
        public string Label { get; set; }
        public string Placeholder { get; set; }
    }
}
