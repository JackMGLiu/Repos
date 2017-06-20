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
        /// ҵ��ִ���
        /// </summary>99
        //protected IUserRepository _userRepository;
        protected IUserService _userService;

        protected ISysMenuService _sysMenuService;

        /// <summary>
        /// ͨ����Ŀƽ̨���Ʋ�ʵ��
        /// </summary>
        /// <param name="userService">ҵ��ִ���</param>
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
                OperationLog.ProcessError("������Ա", HttpContext.GetUserIP(), ex.Message, ex);
                throw;
            }
        }
    }
}