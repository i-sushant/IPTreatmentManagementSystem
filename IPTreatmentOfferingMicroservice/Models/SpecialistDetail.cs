using IPTreatmentOfferingMicroservice.Models;

namespace IPTreatmentOfferingMicroservices.Models
{
    public class SpecialistDetail
    {
        public int SpecialistId { get; set; }
        public string Name { get; set; }
        public string AreaOfExpertise { get; set; }
        
        public int ExperienceInYears { get; set; }
        public long ContactNumber { get; set; }
    }
}
