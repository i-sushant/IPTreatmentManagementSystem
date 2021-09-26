using IpTreatmentManagementPortal.Models;
using System.ComponentModel;

namespace IPTreatmentOfferingMicroservices.Models
{
    public class SpecialistDetail
    {
        [DisplayName("Specialist Id")]
        public int SpecialistId { get; set; }
        public string Name { get; set; }
        [DisplayName("Area of Expertise")]
        public AilmentCategory AreaOfExpertise { get; set; }
        [DisplayName("Experience (In Years)")]
        public int ExperienceInYears { get; set; }
        [DisplayName("Contact Number")]
        public long ContactNumber { get; set; }
    }
}
