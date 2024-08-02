namespace MedUnify.Domain.HealthPulse
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProgressNote
    {
        public int NoteId { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public string SectionName { get; set; }
        public string SectionText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}