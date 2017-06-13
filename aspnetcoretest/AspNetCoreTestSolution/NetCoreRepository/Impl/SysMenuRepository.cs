using System.Data.Common;
using NetCoreData.Dapper;
using NetCoreModel;
using NetCoreRepository.Interface;

namespace NetCoreRepository.Impl
{
    public class SysMenuRepository:DapperHelper<SysMenu>, ISysMenuRepository
    {
        public SysMenuRepository(DbConnection conn) : base(conn)
        {
        }
    }
}
