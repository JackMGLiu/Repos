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
        /// ҵ��ִ���
        /// </summary>
        protected ISysUserService _sysUserService;

        /// <summary>
        /// ��־��
        /// </summary>
        protected Logger _log;

        protected IMapper _mapper { get; set; }

        /// <summary>
        /// ͨ����Ŀƽ̨���Ʋ�ʵ��
        /// </summary>
        /// <param name="userService">ҵ��ִ���</param>
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
                        model.CreateUser = "������Ա";
                        var res = _sysUserService.AddUser(model);
                        if (res)
                        {
                            var json = new {type = 1, data = "", msg = "�����ɣ�", backurl = ""};
                            return Json(json);
                        }
                        else
                        {
                            var json = new {type = 0, data = "", msg = "���ʧ�ܣ�", backurl = ""};
                            return Json(json);
                        }
                    }
                    else
                    {
                        var json = new {type = 2, data = "", msg = "����д�������ݣ�", backurl = ""};
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
                    currentmodel.ModifyUser = "�����޸���Ա";
                    var res = _sysUserService.EditUser(currentmodel);
                    if (res)
                    {
                        var json = new { type = 1, data = "", msg = "�༭��ɣ�", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        var json = new { type = 0, data = "", msg = "�༭ʧ�ܣ�", backurl = "" };
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
                    var json = new { type = 0, data = "", msg = "�����Ƿ���ڣ���ˢ��ҳ�����ԣ�", backurl = "" };
                    return Json(json);
                }
                else
                {
                    var res = _sysUserService.DeleteUser(key);
                    if (res)
                    {
                        var json = new { type = 1, data = "", msg = "ɾ����ɣ�", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        var json = new { type = 0, data = "", msg = "ɾ��ʧ�ܣ�", backurl = "" };
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