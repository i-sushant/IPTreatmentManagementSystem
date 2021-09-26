using System;
using System.ComponentModel;

namespace IpTreatmentManagementPortal.Models
{
    public class IPTreatmentPackage
    {
        [DisplayName("Ailment")]
        public AilmentCategory Ailment { get; set; }
        public PackageDetail PackageDetail { get; set; }
        
    }
}
