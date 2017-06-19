using NetCoreData.Dapper;
using NetCoreModel;

namespace NetCoreRepository
{
    public interface ITestRepository: IDapperHelper<Test>
    {
        
    }
}