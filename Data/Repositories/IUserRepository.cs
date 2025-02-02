using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> Get(UserTypes? userType = null, string name = null, string email = null);
        IEnumerable<User> GetUserByTag(string tag);
        void DeleteAll();
    }
}