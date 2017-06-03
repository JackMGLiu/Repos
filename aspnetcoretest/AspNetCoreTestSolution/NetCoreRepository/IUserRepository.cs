using NetCoreData;
using NetCoreData.Dapper;
using NetCoreModel;

namespace NetCoreRepository
{
    public interface IUserRepository: IDapperHelper<UserTwo>
    {
        
    }
}