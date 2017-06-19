using System.Data.Common;
using NetCoreData.Dapper;
using NetCoreModel;

namespace NetCoreRepository
{
    public class TestRepository : DapperHelper<Test>, ITestRepository
    {
        public TestRepository(DbConnection conn) : base(conn)
        {
        }
    }
}
