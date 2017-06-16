using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreService.Interface;
using NLog;

namespace WebApplication1.Controllers
{
    public class LogsController : Controller
    {
        /// <summary>
        /// 业务仓储类
        /// </summary>
        protected ILogsService _logsService;

        /// <summary>
        /// 日志类
        /// </summary>
        protected Logger _log;

        public LogsController(ILogsService logsService)
        {
            _log = LogManager.GetCurrentClassLogger();
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
    }
}