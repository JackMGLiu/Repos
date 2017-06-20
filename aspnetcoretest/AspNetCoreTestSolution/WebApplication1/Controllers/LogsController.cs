using System;
using Microsoft.AspNetCore.Mvc;
using NetCoreService.Interface;
using NetCoreUtils;
using WebApplication1.Codes;

namespace WebApplication1.Controllers
{
    public class LogsController : BaseController
    {
        /// <summary>
        /// 业务仓储类
        /// </summary>
        protected ILogsService _logsService;

        public LogsController(ILogsService logsService)
        {
            _logsService = logsService;
        }

        [Route("sys/logs")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("sys/loglist")]
        public IActionResult GetLogData(int page = 1)
        {
            var data = _logsService.GetPageList("", page, 20);
            var json = new { data.TotalNum, Items = data.Items, data.CurrentPage, data.TotalPageCount };
            return Json(json);
        }

        [Route("sys/loginfo")]
        public IActionResult ShowInfo()
        {
            return View();
        }


        [HttpGet("sys/getloginfo")]
        public IActionResult GetModelByKey(string key)
        {
            try
            {
                var model = _logsService.GeLogByKey(key);
                return Json(model);
            }
            catch (Exception ex)
            {
                OperationLog.ProcessError("测试人员", HttpContext.GetUserIP(), ex.Message, ex);
                throw;
            }
        }
    }
}