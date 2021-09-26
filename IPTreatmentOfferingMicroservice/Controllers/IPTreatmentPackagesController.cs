using IPTreatmentOfferingMicroservices.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static IPTreatmentOfferingMicroservices.Models.IPTreatmentPackages;

namespace IPTreatmentOfferingMicroservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPTreatmentPackagesController : ControllerBase
    {
        ITreatmentPackageRepository _treatPackage;
        public IPTreatmentPackagesController(ITreatmentPackageRepository treatPackage)
        {
            _treatPackage = treatPackage;
        }
        // GET: api/<IPTreatmentPackagesController>
        [HttpGet]
        [Route("IPTreatmentPackages")]
        public IActionResult GetPackage()
        {
           var records=_treatPackage.GetPackageDetails();
            if(records==null)
            {
                return NotFound();
            }
            return Ok(records);
        }

        //// GET api/<IPTreatmentPackagesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpGet]
        [Route("SpecialistDetail")]
        public IActionResult GetSpecialist()
        {
            var records = _treatPackage.GetSpecialistDetails();
          if (records == null)
           {
                return NoContent();
            }
            return Ok(records);
        }
        [HttpGet]
        [Route ("IPTreatmentPackageByName")]
        public IActionResult GetPackagebyName(AilmentCategory ailment, string treatmentPackageName)
        {
            var record = _treatPackage.GetPackagebyName(ailment, treatmentPackageName);
            if(record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpGet]
        [Route("Package")]
        public IActionResult GetPackagesByName(string treatmentPackageName)
        {
            var package = _treatPackage.PackageExists(treatmentPackageName);
            if (!package)
                return NotFound();
            return Ok();
        }

    }
}
