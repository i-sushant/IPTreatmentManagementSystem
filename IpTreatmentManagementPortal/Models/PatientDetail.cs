using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IpTreatmentManagementPortal.Models
{
    public class PatientDetail
    {
        public int Id { get; set; } = 0;

        [Required]
        public string Name { get; set; }
        [Range(typeof(int), "1", "200", ErrorMessage = "Age must be greater than 1 and less than 200")]
        public int Age { get; set; }

        [RegularExpression(@"^(0|1)")]
        [Required(ErrorMessage = "Value must either be 0 for Orthopaedic and 1 for Urology")]
        public AilmentCategory Ailment { get; set; }
        [DisplayName("Treatment Package Name")]
        [RegularExpression(@"Package (1|2)")]
        public string TreatmentPackageName { get; set; }
        [DisplayName("Treatment Commencement Date")]
        public DateTime TreatmentCommencementDate { get; set; }
        [DisplayName("Treatment Completion Status")]
        public bool TreatmentCompletionStatus { get; set; } = false;
    }
}
