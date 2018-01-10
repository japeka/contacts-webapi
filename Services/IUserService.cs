using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsWebApi.Models;
namespace ContactsWebApi.Services
{
    public interface IUserService
    {
        User GetUser(string firstName, string lastName);

    }
}
