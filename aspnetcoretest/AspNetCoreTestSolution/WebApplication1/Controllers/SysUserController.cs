using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreService;
using NetCoreService.DTO;
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

        protected IMapper _mapper { get; set; }

        /// <summary>
        /// 通用项目平台控制层实例
        /// </summary>
        /// <param name="userService">业务仓储类</param>
        public SysUserController(IMapper mapper, ISysUserService sysUserService)
        {
            _log = LogManager.GetCurrentClassLogger();
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
            return PartialView();
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
                            var json = new {type = 1, data = "", msg = "添加完成！", backurl = ""};
                            return Json(json);
                        }
                        else
                        {
                            var json = new {type = 0, data = "", msg = "添加失败！", backurl = ""};
                            return Json(json);
                        }
                    }
                    else
                    {
                        var json = new {type = 2, data = "", msg = "请填写完整数据！", backurl = ""};
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
                        var json = new { type = 1, data = "", msg = "编辑完成！", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        var json = new { type = 0, data = "", msg = "编辑失败！", backurl = "" };
                        return Json(json);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, ex.Message);
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
                        var json = new { type = 1, data = "", msg = "删除完成！", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        var json = new { type = 0, data = "", msg = "删除失败！", backurl = "" };
                        return Json(json);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, ex.Message);
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
                return Json(data);
            }
            catch (Exception ex)
            {
                _log.Error(ex, ex.Message);
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