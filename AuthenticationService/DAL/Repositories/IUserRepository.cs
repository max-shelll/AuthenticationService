using System.Collections.Generic;
using AuthenticationService.BLL.Models;

namespace AuthenticationService.DAL.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetByLogin(string login);
    }
}
