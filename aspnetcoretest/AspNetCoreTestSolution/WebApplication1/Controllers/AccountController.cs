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
                        "�û���½�ɹ���UserName=" + model.UserName);
                    var json = new {type = 1, data = model, msg = returnmsg, backurl = "/Main/SysMain"};
                    return Json(json);
                }
                else
                {
                    OperationLog.ProcessInfo(loginModel.UserName, HttpContext.GetUserIP(),
                        "�û���½ʧ�ܣ�UserName=" + loginModel.UserName);
                    var json = new {type = 0, data = "", msg = returnmsg, backurl = ""};
                    return Json(json);
                }
            }
            else
            {
                OperationLog.ProcessInfo("��½�û�", HttpContext.GetUserIP(),
                    "�û���½��Ϣ����");
                var json = new {type = 2, data = "", msg = returnmsg, backurl = ""};
                return Json(json);
            }
        }
    }
}