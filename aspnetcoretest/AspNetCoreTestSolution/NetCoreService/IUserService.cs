using System.Collections.Generic;
using NetCoreModel;

namespace NetCoreService
{
    public interface IUserService
    {
        bool AddUser(UserTwo model);

        bool AddUsers(IEnumerable<UserTwo> models);

        long GetUsersCount();
    }
}