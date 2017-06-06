using System.Data.Common;
using NetCoreData.Dapper;
using NetCoreModel;
using NetCoreRepository.Interface;

namespace NetCoreRepository.Impl
{
    public class SysUserRepository : DapperHelper<SysUser>, ISysUserRepository
    {
        public SysUserRepository(DbConnection conn) : base(conn)
        {
        }
    }
}
