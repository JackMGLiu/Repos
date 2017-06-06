using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreRepository;
using NetCoreService;
using Newtonsoft.Json;
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

        public IActionResult GetMenus()
        {
            var data = this.MenusData().OrderBy(m => m.SortCode);
            return Json(data);
        }

        #region 模拟目录数据

        private List<SysMenu> MenusData()
        {
            List<SysMenu> list = new List<SysMenu>
            {
                new SysMenu
                {
                    MenuId = "1",
                    ParentId = "0",
                    MenuName = "系统管理",
                    Icon = "",
                    LinkUrl = "",
                    IsHeader = 1,
                    SortCode = 0
                },
                new SysMenu
                {
                    MenuId = "11",
                    ParentId = "1",
                    MenuName = "基本管理",
                    Icon = "",
                    LinkUrl = "",
                    IsHeader = 0,
                    SortCode = 0
                },
                new SysMenu
                {
                    MenuId = "111",
                    ParentId = "11",
                    MenuName = "系统设置",
                    Icon = "",
                    LinkUrl = "/Main/SysSetting",
                    IsHeader = 0,
                    SortCode = 0
                },
                new SysMenu
                {
                    MenuId = "112",
                    ParentId = "11",
                    MenuName = "系统日志",
                    Icon = "",
                    LinkUrl = "/Main/SysLogging",
                    IsHeader = 0,
                    SortCode = 1
                },
                new SysMenu
                {
                    MenuId = "113",
                    ParentId = "11",
                    MenuName = "异常日志",
                    Icon = "",
                    LinkUrl = "/Main/ErrLogging",
                    IsHeader = 0,
                    SortCode = 2
                },
                new SysMenu
                {
                    MenuId = "12",
                    ParentId = "1",
                    MenuName = "数据字典",
                    Icon = "",
                    LinkUrl = "",
                    IsHeader = 0,
                    SortCode = 1
                },
                new SysMenu
                {
                    MenuId = "121",
                    ParentId = "12",
                    MenuName = "基础数据",
                    Icon = "",
                    LinkUrl = "/Main/BasicData",
                    IsHeader = 0,
                    SortCode = 0
                },
                new SysMenu
                {
                    MenuId = "122",
                    ParentId = "12",
                    MenuName = "区域管理",
                    Icon = "",
                    LinkUrl = "/Main/AreaData",
                    IsHeader = 0,
                    SortCode = 1
                },
                new SysMenu
                {
                    MenuId = "123",
                    ParentId = "12",
                    MenuName = "机构管理",
                    Icon = "",
                    LinkUrl = "/Main/OrgData",
                    IsHeader = 0,
                    SortCode = 2
                },
                new SysMenu
                {
                    MenuId = "124",
                    ParentId = "12",
                    MenuName = "部门管理",
                    Icon = "",
                    LinkUrl = "/Main/DepData",
                    IsHeader = 0,
                    SortCode = 3
                },
                new SysMenu
                {
                    MenuId = "2",
                    ParentId = "0",
                    MenuName = "权限管理",
                    Icon = "",
                    LinkUrl = "",
                    IsHeader = 1,
                    SortCode = 1
                },
                new SysMenu
                {
                    MenuId = "21",
                    ParentId = "2",
                    MenuName = "基本管理",
                    Icon = "",
                    LinkUrl = "",
                    IsHeader = 0,
                    SortCode = 0
                },
                new SysMenu
                {
                    MenuId = "211",
                    ParentId = "21",
                    MenuName = "人员管理",
                    Icon = "",
                    LinkUrl = "/sysuser/list",
                    IsHeader = 0,
                    SortCode = 0
                },
                new SysMenu
                {
                    MenuId = "212",
                    ParentId = "21",
                    MenuName = "角色管理",
                    Icon = "",
                    LinkUrl = "/Main/Role",
                    IsHeader = 0,
                    SortCode = 1
                },
                new SysMenu
                {
                    MenuId = "213",
                    ParentId = "21",
                    MenuName = "按钮管理",
                    Icon = "",
                    LinkUrl = "/Main/Button",
                    IsHeader = 0,
                    SortCode = 2
                },
                new SysMenu
                {
                    MenuId = "214",
                    ParentId = "21",
                    MenuName = "目录管理",
                    Icon = "",
                    LinkUrl = "/Main/Menu",
                    IsHeader = 0,
                    SortCode = 3
                }
            };

            return list;
        }


        #endregion


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