using NetCoreData.Models;
using NetCoreModel;

namespace NetCoreService.Interface
{
    public interface ILogsService
    {
        /// <summary>
        /// 根据主键获取日志实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        Logs GeLogByKey(string key);

        /// <summary>
        /// 获取日志分页信息
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PageDataView<Logs> GetPageList(string level, int page, int pageSize);
    }
}