using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreService.DTO;
using NetCoreService.Interface;
using NetCoreUtils;
using NLog;
using WebApplication1.Codes;

namespace WebApplication1.Controllers
{
    public class SysMenuController : Controller
    {
        private List<MenuModel> menudata = null;

        protected IMapper _mapper { get; set; }

        private ISysMenuService _sysMenuService;



        public SysMenuController(IMapper mapper,ISysMenuService sysMenuService)
        {
            menudata = new List<MenuModel>();
            this._mapper = mapper;
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
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    if (model != null)
                    {
                        model.CreateUser = "������Ա";
                        var res = _sysMenuService.AddMenu(model);
                        if (res)
                        {
                            OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "���Ŀ¼��Ϣ�ɹ���MenuName=" + model.MenuName);
                            var json = new { type = 1, data = "", msg = "�����ɣ�", backurl = "" };
                            return Json(json);
                        }
                        else
                        {
                            OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "���Ŀ¼��Ϣʧ�ܣ�MenuName=" + model.MenuName);
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
                else
                {
                    var currentmodel = _sysMenuService.GetSysMenuByKey(key);
                    currentmodel.ParentId = model.ParentId;
                    currentmodel.MenuName = model.MenuName;
                    currentmodel.LinkUrl = model.LinkUrl;
                    currentmodel.Icon = model.Icon;
                    currentmodel.IsHeader = model.IsHeader;
                    currentmodel.SortCode = model.SortCode;
                    currentmodel.Status = model.Status;
                    currentmodel.Description = model.Description;
                    currentmodel.ModifyUser = "�����޸���Ա";
                    var res = _sysMenuService.EditMenu(currentmodel);
                    if (res)
                    {
                        OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "�༭Ŀ¼��Ϣ�ɹ���MenuId=" + key);
                        var json = new { type = 1, data = "", msg = "�༭��ɣ�", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "�༭Ŀ¼��Ϣʧ�ܣ�MenuId=" + key);
                        var json = new { type = 0, data = "", msg = "�༭ʧ�ܣ�", backurl = "" };
                        return Json(json);
                    }
                }
            }
            catch (Exception ex)
            {
                OperationLog.ProcessError("������Ա", HttpContext.GetUserIP(), ex.Message, ex);
                throw;
            }
        }

        [HttpGet("sysmenu/getmenu")]
        public IActionResult GetModelByKey(string key)
        {
            try
            {
                var model = _sysMenuService.GetSysMenuByKey(key);
                var data = _mapper.Map<SysMenuViewModel>(model);
                OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "��ѯĿ¼��Ϣ�ɹ���MenuId=" + key);
                return Json(data);
            }
            catch (Exception ex)
            {
                OperationLog.ProcessError("������Ա", HttpContext.GetUserIP(), ex.Message, ex);
                throw;
            }
        }

        #region �ݹ����νṹ

        public void GetMenuTree(int length, List<MenuModel> data, ref List<MenuModel> resultData)
        {
            string prefix = string.Empty;
            if (length > 1)
            {
                string nbsp = "&nbsp;";
                for (var i = 0; i < length; i++)
                {
                    nbsp += "&nbsp;&nbsp;";
                }
                prefix = nbsp + "|";
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
                    node.CreateUser = sysMenu.CreateUser;
                    node.CreateTime = sysMenu.CreateTime;
                    node.ModifyUser = sysMenu.ModifyUser;
                    node.ModifyTime = sysMenu.ModifyTime;
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

        #endregion
    }
}