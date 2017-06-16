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
        /// ҵ��ִ���
        /// </summary>
        protected ISysUserService _sysUserService;

        protected IMapper _mapper { get; set; }

        /// <summary>
        /// ͨ����Ŀƽ̨���Ʋ�ʵ��
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
                        model.CreateUser = "������Ա";
                        var res = _sysUserService.AddUser(model);
                        if (res)
                        {
                            OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "����û���Ϣ�ɹ���UserName=" + model.UserName);
                            var json = new { type = 1, data = "", msg = "�����ɣ�", backurl = "" };
                            return Json(json);
                        }
                        else
                        {
                            OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "����û���Ϣʧ�ܣ�UserName=" + model.UserName);
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
                        OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "�༭�û���Ϣ�ɹ���UserId=" + key);
                        var json = new { type = 1, data = "", msg = "�༭��ɣ�", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "�༭�û���Ϣʧ�ܣ�UserId=" + key);
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
                        OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "ɾ���û���Ϣ�ɹ���UserId=" + key);
                        var json = new { type = 1, data = "", msg = "ɾ����ɣ�", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "ɾ���û���Ϣʧ�ܣ�UserId=" + key);
                        var json = new { type = 0, data = "", msg = "ɾ��ʧ�ܣ�", backurl = "" };
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

        [HttpGet("sysuser/getuser")]
        public IActionResult GetModelByKey(string key)
        {
            try
            {
                var model = _sysUserService.GetSysUserByKey(key);
                var data = _mapper.Map<SysUserViewModel>(model);
                OperationLog.ProcessInfo("������Ա", HttpContext.GetUserIP(), "��ѯ�û���Ϣ�ɹ���UserId=" + key);
                return Json(data);
            }
            catch (Exception ex)
            {
                OperationLog.ProcessError("������Ա", HttpContext.GetUserIP(), ex.Message, ex);
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