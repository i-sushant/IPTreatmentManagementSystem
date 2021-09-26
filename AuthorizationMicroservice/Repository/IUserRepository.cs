using AuthorizationService.Models;
using System.Collections.Generic;

namespace AuthorizationService.Repository
{
    public interface IUserRepository
    {
        User GetUser(string username);

    }
}
