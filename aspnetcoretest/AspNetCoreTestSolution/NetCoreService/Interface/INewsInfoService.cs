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
    }
}