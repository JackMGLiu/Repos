using System.Data.Common;
using NetCoreData.Dapper;
using NetCoreModel;
using NetCoreRepository.Interface;

namespace NetCoreRepository.Impl
{
    public class NewsTypeRepository : DapperHelper<NewsType>, INewsTypeRepository
    {
        public NewsTypeRepository(DbConnection conn) : base(conn)
        {
        }
    }
}
