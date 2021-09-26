using AuthorizationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService.Service
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
