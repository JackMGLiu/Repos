using NetCoreData.Dapper;
using NetCoreModel;

namespace NetCoreRepository.Interface
{
    public interface INewsInfoRepository : IDapperHelper<NewsInfo>
    {
        
    }
}