using System;
using System.Collections.Generic;
using System.Text;
using NetCoreData.Models;
using NetCoreModel;
using NetCoreRepository.Interface;
using NetCoreService.Interface;

namespace NetCoreService.Impl
{
    public class LogsService: ILogsService
    {
        protected ILogsRepository _logsRepository;

        public LogsService(ILogsRepository logsRepository)
        {
            this._logsRepository = logsRepository;
        }

        public Logs GeLogByKey(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    return _logsRepository.GetModel(key);
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

        public PageDataView<Logs> GetPageList(string level, int page, int pageSize)
        {
            PageCriteria criteria = new PageCriteria();
            criteria.Sort = "CreateDate";
            if (!string.IsNullOrEmpty(level))
            {
                criteria.Condition += string.Format(" and LogLevel like '%{0}%'", level);
            }

            criteria.CurrentPage = page;
            criteria.Fields = "*";
            criteria.PageSize = pageSize;
            criteria.TableName = "Logs";
            criteria.PrimaryKey = "LogId";
            var r = _logsRepository.GetPageData<Logs>(criteria);
            return r;
        }
    }
}
