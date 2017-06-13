using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreService.DTO;
using NetCoreService.DTO.FormatModel;
using NetCoreService.Interface;

namespace WebApplication1.Controllers
{
    public class SysMenuController : Controller
    {
        private List<MenuModel> menudata = null;
        private ISysMenuService _sysMenuService;


        public SysMenuController(ISysMenuService sysMenuService)
        {
            menudata = new List<MenuModel>();
            this._sysMenuService = sysMenuService;
        }

        [Route("sysmenu/list")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("sysmenu/getmenus")]
        public IActionResult GetData()
        {
            var data = _sysMenuService.GerMenuList().ToList();
            var res = GetTreeData("0", data);
            GetMenuTree(1, res, ref menudata);
            return Json(menudata);
        }

        [Route("sysmenu/form")]
        public IActionResult EditForm()
        {
            return View();
        }

        [HttpPost("sysmenu/savedata")]
        public IActionResult EditForm(string key, SysMenu model)
        {
            return null;
            //try
            //{
            //    if (string.IsNullOrEmpty(key))
            //    {
            //        if (model != null)
            //        {
            //            model.PassWord = "123456";
            //            model.CreateUser = "测试人员";
            //            var res = _sysUserService.AddUser(model);
            //            if (res)
            //            {
            //                var json = new { type = 1, data = "", msg = "添加完成！", backurl = "" };
            //                return Json(json);
            //            }
            //            else
            //            {
            //                var json = new { type = 0, data = "", msg = "添加失败！", backurl = "" };
            //                return Json(json);
            //            }
            //        }
            //        else
            //        {
            //            var json = new { type = 2, data = "", msg = "请填写完整数据！", backurl = "" };
            //            return Json(json);
            //        }
            //    }
            //    else
            //    {
            //        var currentmodel = _sysUserService.GetSysUserByKey(key);
            //        currentmodel.UserName = model.UserName;
            //        currentmodel.RealName = model.RealName;
            //        currentmodel.NickName = model.NickName;
            //        currentmodel.HeadImg = model.HeadImg;
            //        currentmodel.Age = model.Age;
            //        currentmodel.Gender = model.Gender;
            //        currentmodel.Nation = model.Nation;
            //        currentmodel.BirthDay = model.BirthDay;
            //        currentmodel.CardId = model.CardId;
            //        currentmodel.Phone = model.Phone;
            //        currentmodel.Mobile = model.Mobile;
            //        currentmodel.Email = model.Email;
            //        currentmodel.QQ = model.QQ;
            //        currentmodel.WeChat = model.WeChat;
            //        currentmodel.Status = model.Status;
            //        currentmodel.Address = model.Address;
            //        currentmodel.Description = model.Description;
            //        currentmodel.ModifyUser = "测试修改人员";
            //        var res = _sysUserService.EditUser(currentmodel);
            //        if (res)
            //        {
            //            var json = new { type = 1, data = "", msg = "编辑完成！", backurl = "" };
            //            return Json(json);
            //        }
            //        else
            //        {
            //            var json = new { type = 0, data = "", msg = "编辑失败！", backurl = "" };
            //            return Json(json);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _log.Error(ex, ex.Message);
            //    throw;
            //}
        }

        #region 模拟目录数据

        public void GetMenuTree(int length, List<MenuModel> data, ref List<MenuModel> resultData)
        {
            string prefix =string.Empty;
            if (length > 1)
            {
                string nbsp = "&nbsp;";
                for (var i = 0; i < length; i++)
                {
                    nbsp += "&nbsp;&nbsp;";
                }
                prefix = nbsp+"|";
            }
            else
            {
                prefix = "|";
            }
            for (var i = 0; i < length; i++)
            {
                prefix += "-";
            }
            MenuModel model;
            foreach (var item in data)
            {
                model = new MenuModel();
                model.MenuId = item.MenuId;
                model.MenuName = prefix + item.MenuName;
                model.ParentId = item.ParentId;
                model.LinkUrl = item.LinkUrl;
                model.Icon = item.Icon;
                model.IsHeader = item.IsHeader;
                model.Status = item.Status;
                model.SortCode = item.SortCode;
                model.IsDelete = item.IsDelete;
                model.Description = item.Description;
                model.CreateUser = item.CreateUser;
                model.CreateTime = item.CreateTime;
                model.ModifyUser = item.ModifyUser;
                model.ModifyTime = item.ModifyTime;
                resultData.Add(model);
                if (item.Children.Any())
                {
                    GetMenuTree(length + 1, item.Children, ref resultData);
                }
            }
        }

        public List<MenuModel> GetTreeData(string pid, List<SysMenu> menus)
        {
            List<MenuModel> result = new List<MenuModel>();
            foreach (var sysMenu in menus)
            {
                MenuModel node = new MenuModel();
                if (!string.IsNullOrEmpty(sysMenu.MenuId) && sysMenu.ParentId == pid)
                {
                    node.MenuId = sysMenu.MenuId;
                    node.MenuName = sysMenu.MenuName;
                    node.ParentId = sysMenu.ParentId;
                    node.LinkUrl = sysMenu.LinkUrl;
                    node.Icon = sysMenu.Icon;
                    node.IsHeader = sysMenu.IsHeader;
                    node.Status = sysMenu.Status;
                    node.SortCode = sysMenu.SortCode;
                    node.IsDelete = sysMenu.IsDelete;
                    node.Description = sysMenu.Description;
                    node.CreateUser = node.CreateUser;
                    node.CreateTime = node.CreateTime;
                    node.ModifyUser = node.ModifyUser;
                    node.ModifyTime = node.ModifyTime;
                    List<MenuModel> children = GetTreeData(sysMenu.MenuId, menus);
                    if (null != children && children.Any())
                    {
                        node.Children = children;
                    }
                    result.Add(node);
                }
            }
            return result;
        }




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
                    LinkUrl = "/dict/list",
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
    }
}