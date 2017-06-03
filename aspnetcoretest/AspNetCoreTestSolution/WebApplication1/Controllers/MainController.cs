using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreRepository;
using NetCoreService;
using NLog;

namespace WebApplication1.Controllers
{
    public class MainController : Controller
    {
        /// <summary>
        /// ҵ��ִ���
        /// </summary>99
        //protected IUserRepository _userRepository;
        protected IUserService _userService;
        /// <summary>
        /// ��־��
        /// </summary>
        protected Logger _log;
        /// <summary>
        /// ͨ����Ŀƽ̨���Ʋ�ʵ��
        /// </summary>
        /// <param name="userService">ҵ��ִ���</param>
        public MainController(IUserService userService)
        {
            _log = LogManager.GetCurrentClassLogger();
            _userService = userService;
        }

        public IActionResult SysMain()
        {
            return View();
        }


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
                _log.Error(ex, "�Զ������");
                throw;
            }
            //var count = _userRepository.GetList("select * from UserTwo",null).Count();
        }

        public IActionResult AddUser()
        {
            try
            {
                var model = new UserTwo();
                model.UserName = "����88888";
                model.UserPass = "123123";
                model.RealName = "����189";
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
                _log.Error(ex, "������ˣ���������" + ex.Message);
                throw;
            }
        }
    }
}