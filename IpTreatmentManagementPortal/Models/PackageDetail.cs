using System.ComponentModel;

namespace IpTreatmentManagementPortal.Models
{
    public class PackageDetail
    {
        [DisplayName("Treatment Package Name")]
        public string TreatmentPackageName { get; set; }
        [DisplayName("Test Details")]
        public string TestDetails { get; set; }
        public long Cost { get; set; }
        [DisplayName("Treatment Duration")]
        public int TreatmentDuration { get; set; }
        
    }
}