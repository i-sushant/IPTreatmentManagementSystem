using FluentAssertions;
using IPTreatmentOfferingMicroservices.Controllers;
using IPTreatmentOfferingMicroservices.Models;
using IPTreatmentOfferingMicroservices.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using static IPTreatmentOfferingMicroservices.Models.IPTreatmentPackages;

namespace IPTreatmentOfferingMicroservice.Tests
{
    [TestFixture]
    public class Tests
    {
        private Mock<ITreatmentPackageRepository> _packageRepositoryStub;
        [SetUp]
        public void Setup()
        {
            _packageRepositoryStub = new Mock<ITreatmentPackageRepository>();
        }

        [Test]
        public void GetAllPackage_WithExistingPackageDetail_ReturnsAllPackageDetail()
        {
            var expectedItem = new List<IPTreatmentPackages>
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

            }
            };
            _packageRepositoryStub.Setup(repo => repo.GetPackageDetails()).Returns(expectedItem);
            var controller = new IPTreatmentPackagesController(_packageRepositoryStub.Object);
            var response = controller.GetPackage();
            var result = response as OkObjectResult;
            object p = result.Value.Should().BeEquivalentTo(expectedItem,
               options => options.ComparingByMembers<IPTreatmentPackages>());
        }


        [Test]
        public void GetAllPackage_PackageDetailDoesNotExist_ReturnsNotFound()
        {
            _packageRepositoryStub.Setup(repo => repo.GetPackageDetails()).Returns((IEnumerable<IPTreatmentPackages>)null);
            var controller = new IPTreatmentPackagesController(_packageRepositoryStub.Object);
            var response = controller.GetPackage();
            response.Should().BeOfType<NotFoundResult>();
            (response as NotFoundResult).StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [Test]
        public void GetAllSpecialist_WithExistingSpecialistDetail_ReturnsAllSpecialistDetail()
        {
            var expectedItems = new List<SpecialistDetail>
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
            _packageRepositoryStub.Setup(repo => repo.GetSpecialistDetails()).Returns(expectedItems);
            var controller = new IPTreatmentPackagesController(_packageRepositoryStub.Object);
            var response = controller.GetSpecialist();
            var result = response as OkObjectResult;
            object p = result.Value.Should().BeEquivalentTo(expectedItems,
               options => options.ComparingByMembers<SpecialistDetail>());
        }

        [Test]
        public void GetAllSpecialist_WhenSpecialistDetailExistDoesNotExist_ReturnsNoContent()
        {
            _packageRepositoryStub.Setup(repo => repo.GetSpecialistDetails()).Returns((IEnumerable<SpecialistDetail>)null);
            var controller = new IPTreatmentPackagesController(_packageRepositoryStub.Object);
            var response = controller.GetSpecialist();
            response.Should().BeOfType<NoContentResult>();
            (response as NoContentResult).StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [Test]
        public void GetPackageByName_WhenPackageExists_ReturnsPackageDetail()
        {
            var expected = new IPTreatmentPackages
            {
                Ailment = AilmentCategory.Urology,
                PackageDetail = new PackageDetail
                {
                    TreatmentPackageName = "Package 1",
                    TestDetails = "UPT1,UPT2",
                    Cost = 4000,
                    TreatmentDuration = 4
                }
            };
            _packageRepositoryStub.Setup(repo => repo.GetPackagebyName(AilmentCategory.Urology, "Package 1")).Returns(expected);
            var controller = new IPTreatmentPackagesController(_packageRepositoryStub.Object);
            var response = controller.GetPackagebyName(AilmentCategory.Urology, "Package 1");
            var result = response as OkObjectResult;
            result.Value.Should().BeEquivalentTo(expected,
                options => options.ComparingByMembers<IPTreatmentPackages>());

        }
        [Test]
        public void GetPackageByName_WhenPackageNotExists_ReturnsNotFound()
        {
            var controller = new IPTreatmentPackagesController(_packageRepositoryStub.Object);
            var response = controller.GetPackagebyName(AilmentCategory.Urology, "Package 3");
            response.Should().BeOfType<NotFoundResult>();
            (response as NotFoundResult).StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test]
        public void GetPackagesByName_WhenPackagesExist_ReturnsPackageDetail()
        {
            _packageRepositoryStub.Setup(repo => repo.PackageExists("Package 1")).Returns(true);
            var controller = new IPTreatmentPackagesController(_packageRepositoryStub.Object);
            var response = controller.GetPackagesByName("Package 1");
            var result = response as OkObjectResult;
            response.Should().BeOfType<OkResult>();
            (response as OkResult).StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [Test]
        public void GetPackagesByName_WhenPackagesDoNotExist_ReturnsNotFound()
        {
            _packageRepositoryStub.Setup(repo => repo.PackageExists("Package 1")).Returns(false);
            var controller = new IPTreatmentPackagesController(_packageRepositoryStub.Object);
            var response = controller.GetPackagesByName("Package 1");
            response.Should().BeOfType<NotFoundResult>();
            (response as NotFoundResult).StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
    }
}