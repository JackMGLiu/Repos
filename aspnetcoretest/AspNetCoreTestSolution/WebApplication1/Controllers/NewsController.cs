using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreService.Interface;
using NetCoreUtils;
using WebApplication1.Codes;

namespace WebApplication1.Controllers
{
    public class NewsController : BaseController
    {
        protected INewsInfoService _newsInfoService;

        protected IMapper _mapper { get; set; }

        public NewsController(IMapper mapper, INewsInfoService newsInfoService)
        {
            _mapper = mapper;
            _newsInfoService = newsInfoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("news/form")]
        public IActionResult EditForm()
        {
            return View();
        }

        [HttpPost("news/savedata")]
        public IActionResult EditForm(NewsInfo model)
        {
            try
            {
                if (model != null)
                {
                    model.CreateUser = "测试人员";
                    var res = _newsInfoService.AddNews(model);
                    if (res)
                    {
                        OperationLog.ProcessInfo("测试人员", HttpContext.GetUserIP(), "添加内容信息成功！Title=" + model.NewsTitle);
                        var json = new { type = 1, data = "", msg = "添加完成！", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        OperationLog.ProcessInfo("测试人员", HttpContext.GetUserIP(), "添加内容信息失败！Title=" + model.NewsTitle);
                        var json = new { type = 0, data = "", msg = "添加失败！", backurl = "" };
                        return Json(json);
                    }
                }
                else
                {
                    OperationLog.ProcessInfo("测试人员", HttpContext.GetUserIP(), "填写数据信息不完整！");
                    var json = new { type = 2, data = "", msg = "请填写完整数据！", backurl = "" };
                    return Json(json);
                }
            }
            catch (Exception ex)
            {
                OperationLog.ProcessError("测试人员", HttpContext.GetUserIP(), ex.Message, ex);
                throw;
            }
        }
    }
}