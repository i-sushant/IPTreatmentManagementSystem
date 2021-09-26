using System;
using System.ComponentModel;

namespace IpTreatmentManagementPortal.Models
{
    public class TreatmentPlan
    {
        [DisplayName("Patient Id")]
        public int PatientId { get; set; }
        [DisplayName("Package Name")]
        public string PackageName { get; set; }
        [DisplayName("Test Details")]
        public String TestDetails { get; set; }
        [DisplayName("Cost")]
        public int Cost { get; set; }
        [DisplayName("Specialist Id")]
        public int SpecialistId { get; set; }
        [DisplayName("Treatment Commencement Date")]
        public DateTime TreatmentCommencementDate { get; set; }
        [DisplayName("Treatment End Date")]
        public DateTime TreatmentEndDate { get; set; }
    }
}
