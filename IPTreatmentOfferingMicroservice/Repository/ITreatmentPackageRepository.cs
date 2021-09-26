using IPTreatmentOfferingMicroservices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using static FinalDemo.Models.IPTreatmentPackages;

namespace IPTreatmentOfferingMicroservices.Repository
{
    public interface ITreatmentPackageRepository
    {
        IEnumerable<IPTreatmentPackages> GetPackageDetails();

        IPTreatmentPackages GetPackagebyName(IPTreatmentPackages.AilmentCategory ailment,string treatmentPackageName);
        IEnumerable<SpecialistDetail> GetSpecialistDetails();
        bool PackageExists(string packageName);
    }
}
