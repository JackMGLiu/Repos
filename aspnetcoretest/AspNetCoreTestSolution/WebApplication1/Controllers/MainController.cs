using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreRepository;
using NetCoreService;
using NLog;

namespace WebApplication1.Controllers
{
    public class MainController : Controller
    {
        /// <summary>
        /// 业务仓储类
        /// </summary>99
        //protected IUserRepository _userRepository;
        protected IUserService _userService;
        /// <summary>
        /// 日志类
        /// </summary>
        protected Logger _log;
        /// <summary>
        /// 通用项目平台控制层实例
        /// </summary>
        /// <param name="userService">业务仓储类</param>
        public MainController(IUserService userService)
        {
            _log = LogManager.GetCurrentClassLogger();
            _userService = userService;
        }

        public IActionResult SysMain()
        {
            return View();
        }


        public IActionResult Index()
        {
            try
            {
                var count = _userService.GetUsersCount();
                //var model = _userRepository.GetModel(2);
                //ViewData["username"] = model.RealName;
                ViewData["username"] = count;
                return View();
            }
            catch (Exception ex)
            {
                _log.Error(ex, "自定义错误！");
                throw;
            }
            //var count = _userRepository.GetList("select * from UserTwo",null).Count();
        }

        public IActionResult AddUser()
        {
            try
            {
                var model = new UserTwo();
                model.UserName = "测试88888";
                model.UserPass = "123123";
                model.RealName = "王明189";
                var res = _userService.AddUser(model);
                if (res)
                {
                    return Content("OKOKOK");
                }
                else
                {
                    return Content("ERROR");
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, "这里错了！！！！！" + ex.Message);
                throw;
            }
        }
    }
}