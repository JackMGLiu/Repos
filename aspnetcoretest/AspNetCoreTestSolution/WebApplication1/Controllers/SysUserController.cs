using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreService;
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
        /// <summary>
        /// ͨ����Ŀƽ̨���Ʋ�ʵ��
        /// </summary>
        /// <param name="userService">ҵ��ִ���</param>
        public SysUserController(ISysUserService sysUserService)
        {
            _log = LogManager.GetCurrentClassLogger();
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
        //[Route("sysuser/savedata")]
        public IActionResult EditForm([FromForm]SysUser model)
        {
            try
            {
                if (model != null)
                {
                    model.UserId = Guid.NewGuid().ToString();
                    model.PassWord = "123456";
                    model.IsDelete = 0;
                    model.CreateTime=DateTime.Now;
                    model.CreateUser = "������Ա";
                    var res = _sysUserService.AddUser(model);
                    if (res)
                    {
                        var json = new {type = 1, data = "", msg = "�����ɣ�", backurl = ""};
                        return Json(json);
                    }
                    else
                    {
                        var json = new { type = 0, data = "", msg = "���ʧ�ܣ�", backurl = "" };
                        return Json(json);
                    }
                }
                else
                {
                    var json = new { type = 2, data = "", msg = "����д�������ݣ�", backurl = "" };
                    return Json(json);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, ex.Message);
                throw;
            }

        }
    }
}