using AuthorizationService.DTOs;
using AuthorizationService.Models;
using AuthorizationService.Repository;
using AuthorizationService.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuthorizationMicroService.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IUserRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }
        [HttpPost]
        public IActionResult Login(UserDto userData)
        {
            var user = _repository.GetUser(userData.Username.ToLower());
            if(user != null)
            {
                if (user.Password == userData.Password) return Ok(new { token = _tokenService.GenerateToken(user) }); 
                else Unauthorized();
            }
            return Unauthorized();
        }

        
    }
}
