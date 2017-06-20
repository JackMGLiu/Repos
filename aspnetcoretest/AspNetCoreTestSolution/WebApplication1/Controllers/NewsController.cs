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
                    model.CreateUser = "������Ա";
                    var res = _newsInfoService.AddNews(model);
                    if (res)
                    {
                        OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "���������Ϣ�ɹ���Title=" + model.NewsTitle);
                        var json = new { type = 1, data = "", msg = "�����ɣ�", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "���������Ϣʧ�ܣ�Title=" + model.NewsTitle);
                        var json = new { type = 0, data = "", msg = "���ʧ�ܣ�", backurl = "" };
                        return Json(json);
                    }
                }
                else
                {
                    OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "��д������Ϣ��������");
                    var json = new { type = 2, data = "", msg = "����д�������ݣ�", backurl = "" };
                    return Json(json);
                }
            }
            catch (Exception ex)
            {
                OperationLog.ProcessError("������Ա", HttpContext.GetUserIP(), ex.Message, ex);
                throw;
            }
        }
    }
}