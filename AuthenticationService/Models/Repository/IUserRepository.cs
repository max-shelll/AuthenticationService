using System.Collections.Generic;

namespace AuthenticationService.Models.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetByLogin(string login);
    }
}
