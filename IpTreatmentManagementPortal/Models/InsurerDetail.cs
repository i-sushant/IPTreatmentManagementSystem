using System.ComponentModel;

namespace IpTreatmentManagementPortal.Models
{
    public class InsurerDetail
    {
        [DisplayName("Insurer Name")]
        public string InsurerName { get; set; }
        [DisplayName("Insurer Package Name")]
        public string InsurerPackageName { get; set; }
        [DisplayName("Insurance Amount Limit")]
        public long InsuranceAmountLimit { get; set; }
        [DisplayName("Disbursement Duration(in months)")]
        public long DisbursementDuration { get; set; }
    }
}
