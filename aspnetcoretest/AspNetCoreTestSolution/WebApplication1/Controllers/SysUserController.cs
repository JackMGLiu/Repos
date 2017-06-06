using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreService;
using NetCoreService.Interface;
using Newtonsoft.Json;
using NLog;

namespace WebApplication1.Controllers
{
    public class SysUserController : Controller
    {
        /// <summary>
        /// 业务仓储类
        /// </summary>
        protected ISysUserService _sysUserService;

        /// <summary>
        /// 日志类
        /// </summary>
        protected Logger _log;
        /// <summary>
        /// 通用项目平台控制层实例
        /// </summary>
        /// <param name="userService">业务仓储类</param>
        public SysUserController(ISysUserService sysUserService)
        {
            _log = LogManager.GetCurrentClassLogger();
            _sysUserService = sysUserService;
        }

        [Route("sysuser/list")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("sysuser/form")]
        public IActionResult EditForm()
        {
            return PartialView();
        }

        [HttpPost("sysuser/savedata")]
        //[Route("sysuser/savedata")]
        public IActionResult EditForm([FromForm]SysUser model)
        {
            try
            {
                if (model != null)
                {
                    model.UserId = Guid.NewGuid().ToString();
                    model.PassWord = "123456";
                    model.IsDelete = 0;
                    model.CreateTime=DateTime.Now;
                    model.CreateUser = "测试人员";
                    var res = _sysUserService.AddUser(model);
                    if (res)
                    {
                        var json = new {type = 1, data = "", msg = "添加完成！", backurl = ""};
                        return Json(json);
                    }
                    else
                    {
                        var json = new { type = 0, data = "", msg = "添加失败！", backurl = "" };
                        return Json(json);
                    }
                }
                else
                {
                    var json = new { type = 2, data = "", msg = "请填写完整数据！", backurl = "" };
                    return Json(json);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, ex.Message);
                throw;
            }

        }
    }
}