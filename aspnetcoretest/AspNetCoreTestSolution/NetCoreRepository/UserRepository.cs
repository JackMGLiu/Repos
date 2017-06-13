using System.Data.Common;
using NetCoreData.Dapper;
using NetCoreModel;

namespace NetCoreRepository
{
    public class UserRepository : DapperHelper<UserTwo>, IUserRepository
    {
        public UserRepository(DbConnection conn) : base(conn)
        {
        }
    }
}
