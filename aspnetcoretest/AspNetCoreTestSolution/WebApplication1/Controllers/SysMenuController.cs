using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreService.DTO.FormatModel;

namespace WebApplication1.Controllers
{
    public class SysMenuController : Controller
    {

        [Route("sysmenu/list")]
        public IActionResult Index()
        {
            return View();
        }

        List<MenuModel> menudata = new List<MenuModel>();

        [HttpGet("sysmenu/getmenus")]
        public IActionResult GetData()
        {
            //var data = MenusData().OrderBy(m => m.SortCode);
            var data = GetTreeData("0", MenusData());
            GetTree(1, data, ref menudata);
            return Json(menudata);
        }

        #region ģ��Ŀ¼����

        public void GetTree(int length, List<TreeGridModel> data, ref List<MenuModel> resultData)
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
                model.Id = item.Id;
                model.Name = prefix + item.Name;
                resultData.Add(model);
                if (item.Children.Any())
                {
                    GetTree(length + 1, item.Children, ref resultData);
                }
            }
        }

        public List<TreeGridModel> GetTreeData(string pid, List<SysMenu> menus)
        {
            List<TreeGridModel> result = new List<TreeGridModel>();
            foreach (var sysMenu in menus)
            {
                TreeGridModel node = new TreeGridModel();
                if (!string.IsNullOrEmpty(sysMenu.MenuId) && sysMenu.ParentId == pid)
                {
                    node.Id = sysMenu.MenuId;
                    node.Name = sysMenu.MenuName;
                    List<TreeGridModel> children = GetTreeData(sysMenu.MenuId, menus);
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
                    MenuName = "ϵͳ����",
                    Icon = "",
                    LinkUrl = "",
                    IsHeader = 1,
                    SortCode = 0
                },
                new SysMenu
                {
                    MenuId = "11",
                    ParentId = "1",
                    MenuName = "��������",
                    Icon = "",
                    LinkUrl = "",
                    IsHeader = 0,
                    SortCode = 0
                },
                new SysMenu
                {
                    MenuId = "111",
                    ParentId = "11",
                    MenuName = "ϵͳ����",
                    Icon = "",
                    LinkUrl = "/Main/SysSetting",
                    IsHeader = 0,
                    SortCode = 0
                },
                new SysMenu
                {
                    MenuId = "112",
                    ParentId = "11",
                    MenuName = "ϵͳ��־",
                    Icon = "",
                    LinkUrl = "/Main/SysLogging",
                    IsHeader = 0,
                    SortCode = 1
                },
                new SysMenu
                {
                    MenuId = "113",
                    ParentId = "11",
                    MenuName = "�쳣��־",
                    Icon = "",
                    LinkUrl = "/Main/ErrLogging",
                    IsHeader = 0,
                    SortCode = 2
                },
                new SysMenu
                {
                    MenuId = "12",
                    ParentId = "1",
                    MenuName = "�����ֵ�",
                    Icon = "",
                    LinkUrl = "",
                    IsHeader = 0,
                    SortCode = 1
                },
                new SysMenu
                {
                    MenuId = "121",
                    ParentId = "12",
                    MenuName = "��������",
                    Icon = "",
                    LinkUrl = "/dict/list",
                    IsHeader = 0,
                    SortCode = 0
                },
                new SysMenu
                {
                    MenuId = "122",
                    ParentId = "12",
                    MenuName = "�������",
                    Icon = "",
                    LinkUrl = "/Main/AreaData",
                    IsHeader = 0,
                    SortCode = 1
                },
                new SysMenu
                {
                    MenuId = "123",
                    ParentId = "12",
                    MenuName = "��������",
                    Icon = "",
                    LinkUrl = "/Main/OrgData",
                    IsHeader = 0,
                    SortCode = 2
                },
                new SysMenu
                {
                    MenuId = "124",
                    ParentId = "12",
                    MenuName = "���Ź���",
                    Icon = "",
                    LinkUrl = "/Main/DepData",
                    IsHeader = 0,
                    SortCode = 3
                },
                new SysMenu
                {
                    MenuId = "2",
                    ParentId = "0",
                    MenuName = "Ȩ�޹���",
                    Icon = "",
                    LinkUrl = "",
                    IsHeader = 1,
                    SortCode = 1
                },
                new SysMenu
                {
                    MenuId = "21",
                    ParentId = "2",
                    MenuName = "��������",
                    Icon = "",
                    LinkUrl = "",
                    IsHeader = 0,
                    SortCode = 0
                },
                new SysMenu
                {
                    MenuId = "211",
                    ParentId = "21",
                    MenuName = "��Ա����",
                    Icon = "",
                    LinkUrl = "/sysuser/list",
                    IsHeader = 0,
                    SortCode = 0
                },
                new SysMenu
                {
                    MenuId = "212",
                    ParentId = "21",
                    MenuName = "��ɫ����",
                    Icon = "",
                    LinkUrl = "/Main/Role",
                    IsHeader = 0,
                    SortCode = 1
                },
                new SysMenu
                {
                    MenuId = "213",
                    ParentId = "21",
                    MenuName = "��ť����",
                    Icon = "",
                    LinkUrl = "/Main/Button",
                    IsHeader = 0,
                    SortCode = 2
                },
                new SysMenu
                {
                    MenuId = "214",
                    ParentId = "21",
                    MenuName = "Ŀ¼����",
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

    public class MenuModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}