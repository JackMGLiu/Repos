using System.Data.Common;
using NetCoreData.Dapper;
using NetCoreModel;
using NetCoreRepository.Interface;

namespace NetCoreRepository.Impl
{
    public class DictDetailRepository : DapperHelper<DictDetail>, IDictDetailRepository
    {
        public DictDetailRepository(DbConnection conn) : base(conn)
        {
        }
    }
}