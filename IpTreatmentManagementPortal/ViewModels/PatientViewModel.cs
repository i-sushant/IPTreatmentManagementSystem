using IpTreatmentManagementPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IpTreatmentManagementPortal.ViewModels
{
    public class PatientViewModel
    {
        public PatientDetail PatientDetail{ get; set; }
        public TreatmentPlan TreatmentPlan { get; set; }
        public InitiateClaim Claim { get; set; } = null;
    }
}
