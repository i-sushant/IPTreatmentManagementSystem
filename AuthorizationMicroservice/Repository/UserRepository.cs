using AuthorizationService.Models;
using System.Collections.Generic;

namespace AuthorizationService.Repository
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users;
        public UserRepository()
        {
            _users = new List<User>()
            {
                new User { Id = 1, Username = "admin", Password ="admin", Role = "admin"}
            };
        }
        public User GetUser(string username)
        {
            return _users.Find(user => user.Username == username);
        }
        
    }
}
