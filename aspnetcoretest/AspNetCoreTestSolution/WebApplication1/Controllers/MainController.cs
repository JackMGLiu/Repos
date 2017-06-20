using System;
using Microsoft.AspNetCore.Mvc;
using NetCoreService;
using NetCoreService.Interface;
using NetCoreUtils;
using WebApplication1.Codes;

namespace WebApplication1.Controllers
{
    public class MainController : BaseController
    {
        /// <summary>
        /// 业务仓储类
        /// </summary>99
        //protected IUserRepository _userRepository;
        protected IUserService _userService;

        protected ISysMenuService _sysMenuService;

        /// <summary>
        /// 通用项目平台控制层实例
        /// </summary>
        /// <param name="userService">业务仓储类</param>
        public MainController(IUserService userService, ISysMenuService sysMenuService)
        {
            _userService = userService;
            this._sysMenuService = sysMenuService;
        }

        public IActionResult SysMain()
        {
            return View();
        }

        [Route("main/getmenus")]
        public IActionResult GetMenus()
        {
            var data = _sysMenuService.GerMenuList();
            return Json(data);
        }

        public IActionResult Index()
        {
            try
            {
                var count = _userService.GetUsersCount();
                ViewData["username"] = count;
                return View();
            }
            catch (Exception ex)
            {
                OperationLog.ProcessError("测试人员", HttpContext.GetUserIP(), ex.Message, ex);
                throw;
            }
        }
    }
}