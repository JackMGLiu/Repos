using System;
using NetCoreData.Models;
using NetCoreModel;
using NetCoreRepository.Interface;
using NetCoreService.Interface;

namespace NetCoreService.Impl
{
    public class NewsInfoService: INewsInfoService
    {
        protected INewsInfoRepository _newsInfoRepository;

        public NewsInfoService(INewsInfoRepository newsInfoRepository)
        {
            this._newsInfoRepository = newsInfoRepository;
        }

        public bool AddNews(NewsInfo model)
        {
            bool flag = false;
            try
            {
                model.DoCreate();
                _newsInfoRepository.AddModel(model);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public NewsInfo GetNewsInfoyKey(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    return _newsInfoRepository.GetModel(key);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PageDataView<NewsInfo> GetPageList(int page, int pageSize)
        {
            PageCriteria criteria = new PageCriteria();
            criteria.Condition = "IsDelete=0";
            criteria.Sort = "CreateTime";
            //if (!string.IsNullOrEmpty(username))
            //{
            //    criteria.Condition += string.Format(" and UserName like '%{0}%'", username);
            //}

            criteria.CurrentPage = page;
            criteria.Fields = "*";
            criteria.PageSize = pageSize;
            criteria.TableName = "NewsInfo";
            criteria.PrimaryKey = "NewsId";
            var r = _newsInfoRepository.GetPageData<NewsInfo>(criteria);
            return r;
        }
    }
}
