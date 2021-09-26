using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IpTreatmentManagementPortal.Models
{
    public class InitiateClaim
    {
        [DisplayName("Patient Name")]
        public string PatientName { get; set; }
        [DisplayName("Patient ID")]
        public int PatientId { get; set; }
        [DisplayName("Ailment")]
        [Required(ErrorMessage = "Value must either be 0 for Orthopaedic and 1 for Urology")]
        public int Ailment { get; set; }

        [DisplayName("Treatment Package Name")]
        [RegularExpression(@"Package (1|2)")]
        public string TreatmentPackageName { get; set; }
        [DisplayName("Insurer Name")]
        [RegularExpression(@"((Bajaj|Reliance|Tata|ICICI) Insurance|LIC)")]
        public string InsurerName { get; set; }

        public long BalanceAmount { get; set; }
    }
}
