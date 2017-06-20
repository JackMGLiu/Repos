using System;
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
    }
}
