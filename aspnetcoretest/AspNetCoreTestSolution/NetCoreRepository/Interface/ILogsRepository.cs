using NetCoreData.Dapper;
using NetCoreModel;

namespace NetCoreRepository.Interface
{
    public interface ILogsRepository: IDapperHelper<Logs>
    {
        
    }
}