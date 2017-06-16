using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreService.DTO;
using NetCoreService.Interface;
using NetCoreUtils;
using WebApplication1.Codes;

namespace WebApplication1.Controllers
{
    public class SysUserController : Controller
    {
        /// <summary>
        /// 业务仓储类
        /// </summary>
        protected ISysUserService _sysUserService;

        protected IMapper _mapper { get; set; }

        /// <summary>
        /// 通用项目平台控制层实例
        /// </summary>
        public SysUserController(IMapper mapper, ISysUserService sysUserService)
        {
            //_log = LogManager.GetCurrentClassLogger();
            _mapper = mapper;
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
            return View();
        }

        [HttpPost("sysuser/savedata")]
        public IActionResult EditForm(string key, SysUser model)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    if (model != null)
                    {
                        model.PassWord = "123456";
                        model.CreateUser = "测试人员";
                        var res = _sysUserService.AddUser(model);
                        if (res)
                        {
                            OperationLog.ProcessInfo("测试人员", HttpContext.GetUserIP(), "添加用户信息成功！UserName=" + model.UserName);
                            var json = new { type = 1, data = "", msg = "添加完成！", backurl = "" };
                            return Json(json);
                        }
                        else
                        {
                            OperationLog.ProcessInfo("测试人员", HttpContext.GetUserIP(), "添加用户信息失败！UserName=" + model.UserName);
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
                else
                {
                    var currentmodel = _sysUserService.GetSysUserByKey(key);
                    currentmodel.UserName = model.UserName;
                    currentmodel.RealName = model.RealName;
                    currentmodel.NickName = model.NickName;
                    currentmodel.HeadImg = model.HeadImg;
                    currentmodel.Age = model.Age;
                    currentmodel.Gender = model.Gender;
                    currentmodel.Nation = model.Nation;
                    currentmodel.BirthDay = model.BirthDay;
                    currentmodel.CardId = model.CardId;
                    currentmodel.Phone = model.Phone;
                    currentmodel.Mobile = model.Mobile;
                    currentmodel.Email = model.Email;
                    currentmodel.QQ = model.QQ;
                    currentmodel.WeChat = model.WeChat;
                    currentmodel.Status = model.Status;
                    currentmodel.Address = model.Address;
                    currentmodel.Description = model.Description;
                    currentmodel.ModifyUser = "测试修改人员";
                    var res = _sysUserService.EditUser(currentmodel);
                    if (res)
                    {
                        OperationLog.ProcessInfo("测试人员", HttpContext.GetUserIP(), "编辑用户信息成功！UserId=" + key);
                        var json = new { type = 1, data = "", msg = "编辑完成！", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        OperationLog.ProcessInfo("测试人员", HttpContext.GetUserIP(), "编辑用户信息失败！UserId=" + key);
                        var json = new { type = 0, data = "", msg = "编辑失败！", backurl = "" };
                        return Json(json);
                    }
                }
            }
            catch (Exception ex)
            {
                OperationLog.ProcessError("测试人员", HttpContext.GetUserIP(), ex.Message, ex);
                throw;
            }
        }

        [HttpPost("sysuser/delete")]
        public IActionResult DelModelByKey(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    var json = new { type = 0, data = "", msg = "数据是否存在？请刷新页面重试！", backurl = "" };
                    return Json(json);
                }
                else
                {
                    var res = _sysUserService.DeleteUser(key);
                    if (res)
                    {
                        OperationLog.ProcessInfo("测试人员", HttpContext.GetUserIP(), "删除用户信息成功！UserId=" + key);
                        var json = new { type = 1, data = "", msg = "删除完成！", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        OperationLog.ProcessInfo("测试人员", HttpContext.GetUserIP(), "删除用户信息失败！UserId=" + key);
                        var json = new { type = 0, data = "", msg = "删除失败！", backurl = "" };
                        return Json(json);
                    }
                }
            }
            catch (Exception ex)
            {
                OperationLog.ProcessError("测试人员", HttpContext.GetUserIP(), ex.Message, ex);
                throw;
            }
        }

        [HttpGet("sysuser/getuser")]
        public IActionResult GetModelByKey(string key)
        {
            try
            {
                var model = _sysUserService.GetSysUserByKey(key);
                var data = _mapper.Map<SysUserViewModel>(model);
                OperationLog.ProcessInfo("测试人员", HttpContext.GetUserIP(), "查询用户信息成功！UserId=" + key);
                return Json(data);
            }
            catch (Exception ex)
            {
                OperationLog.ProcessError("测试人员", HttpContext.GetUserIP(), ex.Message, ex);
                throw;
            }
        }

        [HttpGet("sysuser/getusers")]
        public IActionResult GetPageData(int page = 1)
        {
            var data = _sysUserService.GetPageList("", page, 10);
            var items = _mapper.Map<List<SysUserViewModel>>(data.Items);
            //var items = data.Items.Select(s => _mapper.Map<SysUserViewModel>(s));
            var json = new { data.TotalNum, Items = items, data.CurrentPage, data.TotalPageCount };
            return Json(json);
        }
    }
}