using System.Data.Common;
using NetCoreData.Dapper;
using NetCoreModel;
using NetCoreRepository.Interface;

namespace NetCoreRepository.Impl
{
    public class LogsRepository: DapperHelper<Logs>, ILogsRepository
    {
        public LogsRepository(DbConnection conn) : base(conn)
        {
        }
    }
}
