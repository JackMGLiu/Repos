using System.Data.Common;
using NetCoreData.Dapper;
using NetCoreModel;
using NetCoreRepository.Interface;

namespace NetCoreRepository.Impl
{
    public class NewsInfoRepository: DapperHelper<NewsInfo>, INewsInfoRepository
    {
        public NewsInfoRepository(DbConnection conn) : base(conn)
        {
        }
    }
}
