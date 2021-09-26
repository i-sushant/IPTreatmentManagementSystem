using IPTreatmentOfferingMicroservices.Models;
using System.Collections.Generic;
using System.Linq;
using static IPTreatmentOfferingMicroservices.Models.IPTreatmentPackages;

namespace IPTreatmentOfferingMicroservices.Repository
{
    public class TreatmentPackageRepository : ITreatmentPackageRepository
    {
        public static List<IPTreatmentPackages> _packList = new List<IPTreatmentPackages>()
        {
          new IPTreatmentPackages
          {
              Ailment = AilmentCategory.Orthopaedics,
              PackageDetail = new PackageDetail
              {
                TreatmentPackageName="Package 1",
                TestDetails="OPT1,OPT2",
                Cost=2500,
                TreatmentDuration=4
              }
          },
          
           new IPTreatmentPackages
          {
              Ailment = AilmentCategory.Orthopaedics,
              PackageDetail = new PackageDetail
              {
                TreatmentPackageName="Package 2",
                TestDetails="OPT3,OPT4",
                Cost=3000,
                TreatmentDuration=6
              }
          },
            new IPTreatmentPackages
          {
              Ailment = AilmentCategory.Urology,
              PackageDetail = new PackageDetail
              {
                TreatmentPackageName="Package 1",
                TestDetails="UPT1,UPT2",
                Cost=4000,
                TreatmentDuration=4
               }
          },
             new IPTreatmentPackages
          {
              Ailment=AilmentCategory.Urology,
              PackageDetail=new PackageDetail
              {
                TreatmentPackageName="Package 2",
                TestDetails="UPT3,UPT4",
                Cost=5000,
                TreatmentDuration=6
              }
          },
        };
        public static List<SpecialistDetail> _speciaList = new List<SpecialistDetail>()
        {
            new SpecialistDetail
            {
             SpecialistId=1,
             Name="Sushant",
             AreaOfExpertise="Orthopaedics",
             ExperienceInYears=10,
             ContactNumber=9890456793,
            },
            new SpecialistDetail
            {
             SpecialistId=2,
             Name="Gauthami",
             AreaOfExpertise="Urology",
             ExperienceInYears=20,
             ContactNumber=8890456793,
            },
            new SpecialistDetail
            {
             SpecialistId=3,
             Name="Jayesh",
             AreaOfExpertise="Orthopaedics",
             ExperienceInYears=14,
             ContactNumber=9790456793,
            },
            new SpecialistDetail
            {
             SpecialistId=4,
             Name="Shubham",
             AreaOfExpertise="Urology",
             ExperienceInYears=10,
             ContactNumber=6690456793,
            }

        };
        public IEnumerable<IPTreatmentPackages> GetPackageDetails()
        {
            return _packList;
           
        }

        public IPTreatmentPackages GetPackagebyName(IPTreatmentPackages.AilmentCategory ailment, string treatmentPackageName)
        {
            var result = _packList.Find(package => package.Ailment == ailment && package.PackageDetail.TreatmentPackageName.ToLower() == treatmentPackageName.ToLower());
            return result;
           
        }
        public IEnumerable<SpecialistDetail> GetSpecialistDetails()
        {
            return _speciaList;
        }

        public bool PackageExists(string packageName)
        {
            return _packList.Exists(package => package.PackageDetail.TreatmentPackageName.ToLower() == packageName.ToLower());        
        }
    }
}
