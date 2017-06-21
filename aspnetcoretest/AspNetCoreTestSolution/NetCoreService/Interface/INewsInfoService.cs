using NetCoreData.Models;
using NetCoreModel;

namespace NetCoreService.Interface
{
    public interface INewsInfoService
    {
        /// <summary>
        /// 新增新闻
        /// </summary>
        /// <param name="model">新闻实体信息</param>
        /// <returns></returns>
        bool AddNews(NewsInfo model);

        /// <summary>
        /// 根据主键获取新闻实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        NewsInfo GetNewsInfoyKey(string key);

        /// <summary>
        /// 获取新闻分页信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PageDataView<NewsInfo> GetPageList(int page, int pageSize);
    }
}