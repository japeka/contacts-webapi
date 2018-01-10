using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContactsWebApi.Models;
using Newtonsoft.Json;
using ContactsWebApi.Config;

namespace ContactsWebApi.Services
{

    public class UserService : IUserService
    {
        private User user;
        public UserService()
        {
            user = new User();
        }

        public User GetUser(string firstName, string lastName)
        {
            this.user.FirstName = firstName;
            this.user.LastName = lastName;
            return user;
        }

    }
}
