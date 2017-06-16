using Microsoft.AspNetCore.Mvc;
using NetCoreService.Interface;

namespace WebApplication1.Controllers
{
    public class LogsController : Controller
    {
        /// <summary>
        /// ÒµÎñ²Ö´¢Àà
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
    }
}