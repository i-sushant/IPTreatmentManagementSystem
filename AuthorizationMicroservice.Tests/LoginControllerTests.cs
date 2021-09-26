using AuthorizationMicroService.Controllers;
using AuthorizationService.DTOs;
using AuthorizationService.Models;
using AuthorizationService.Repository;
using AuthorizationService.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net;

namespace AuthorizationMicroservice.Tests
{
    public class Tests
    {
        private Mock<IUserRepository> _userRepositoryStub;
        private Mock<ITokenService> _tokenServiceStub;
        [SetUp]
        public void Setup()
        {
            _userRepositoryStub = new Mock<IUserRepository>();
            _tokenServiceStub = new Mock<ITokenService>();
        }

        [Test]
        public void Login_WhenLoginSuccessful_ReturnsOkResult()
        {
            var user = new User { Id = 1, Username = "admin", Password = "admin", Role = "admin" };
            _userRepositoryStub.Setup(repo => repo.GetUser(user.Username)).Returns(user);
            var controller = new AuthenticationController(_userRepositoryStub.Object, _tokenServiceStub.Object);
            var response = controller.Login(new UserDto { Username = "admin", Password = "admin" });
            response.Should().BeOfType<OkObjectResult>();
            (response as OkObjectResult).StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public void Login_WhenLoginUnSuccessful_ReturnsUnauthorizedResult()
        {
            var user = new User { Id = 1, Username = "admin", Password = "admin", Role = "admin" };
            _userRepositoryStub.Setup(repo => repo.GetUser(user.Username)).Returns(user);
            var controller = new AuthenticationController(_userRepositoryStub.Object, _tokenServiceStub.Object);
            var response = controller.Login(new UserDto { Username = "admin", Password = "pass" });
            response.Should().BeOfType<UnauthorizedResult>();
            (response as UnauthorizedResult).StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
        }
        [Test]
        public void Login_WhenUserDoesNotExist_ReturnsUnauthorizedResult()
        {
            var user = new User { Id = 1, Username = "admin", Password = "pass", Role = "admin" };
            _userRepositoryStub.Setup(repo => repo.GetUser(user.Username)).Returns((User)null);
            var controller = new AuthenticationController(_userRepositoryStub.Object, _tokenServiceStub.Object);
            var response = controller.Login(new UserDto { Username = "admin", Password = "pass" });
            response.Should().BeOfType<UnauthorizedResult>();
            (response as UnauthorizedResult).StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
        }
    }
}