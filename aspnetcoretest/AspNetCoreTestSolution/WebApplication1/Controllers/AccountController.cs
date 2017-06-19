using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreService.DTO;
using NetCoreService.Interface;
using NetCoreUtils;
using WebApplication1.Codes;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        protected ISysUserService _sysUserService;

        protected IMapper _mapper { get; set; }

        public AccountController(IMapper mapper, ISysUserService sysUserService)
        {
            _mapper = mapper;
            _sysUserService = sysUserService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("account/userlogin")]
        public IActionResult UserLogin(LoginModel loginModel)
        {
            string returnmsg = string.Empty;

            SysUserViewModel model = null;
            if (loginModel != null)
            {
                var res = _sysUserService.UserLogin(loginModel.UserName, loginModel.UserPass, out returnmsg, out model);
                if (res)
                {
                    OperationLog.ProcessInfo(loginModel.UserName, HttpContext.GetUserIP(),
                        "用户登陆成功！UserName=" + model.UserName);
                    var json = new {type = 1, data = model, msg = returnmsg, backurl = "/Main/SysMain"};
                    return Json(json);
                }
                else
                {
                    OperationLog.ProcessInfo(loginModel.UserName, HttpContext.GetUserIP(),
                        "用户登陆失败！UserName=" + loginModel.UserName);
                    var json = new {type = 0, data = "", msg = returnmsg, backurl = ""};
                    return Json(json);
                }
            }
            else
            {
                OperationLog.ProcessInfo("登陆用户", HttpContext.GetUserIP(),
                    "用户登陆信息有误！");
                var json = new {type = 2, data = "", msg = returnmsg, backurl = ""};
                return Json(json);
            }
        }
    }
}