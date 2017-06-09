using System.Data.Common;
using NetCoreData.Dapper;
using NetCoreModel;
using NetCoreRepository.Interface;

namespace NetCoreRepository.Impl
{
    public class DictTypeRepository : DapperHelper<DictType>, IDictTypeRepository
    {
        public DictTypeRepository(DbConnection conn) : base(conn)
        {
        }
    }
}
