using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
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

        [AllowAnonymous]
        public IActionResult Login()
        {
            //if (!HttpContext.User.Identity.IsAuthenticated)
            //{
            //    //把返回地址保存在前台的hide表单中
            //    ViewBag.returnUrl = returnUrl;
            //}
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var loginName = HttpContext.User.Identity.Name;
                ViewBag.UserName = loginName;
                ViewBag.Rember = true;
            }
            HttpContext.Authentication.SignOutAsync("member");
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

                    HttpContext.Authentication.SignOutAsync("member");
                    if (loginModel.IsRember)  //保存cookie
                    {
                        var identity = new ClaimsIdentity("Forms");     // 指定身份认证类型
                        identity.AddClaim(new Claim(ClaimTypes.Sid, model.UserId));  // 用户Id
                        identity.AddClaim(new Claim(ClaimTypes.Name, model.UserName));       // 用户名称
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.Authentication.SignInAsync("member", principal, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddMinutes(60) });  //过期时间60分钟
                    }
                    //记录Session
                    HttpContext.Session.Set("CurrentUser", ByteConVertHelper.Object2Bytes(model.UserId));
                    OperationLog.ProcessInfo(loginModel.UserName, HttpContext.GetUserIP(),
                        "用户登陆成功！UserName=" + model.UserName);
                    var json = new { type = 1, data = model, msg = returnmsg, backurl = "/Main/SysMain" };
                    return Json(json);
                }
                else
                {
                    OperationLog.ProcessInfo(loginModel.UserName, HttpContext.GetUserIP(),
                        "用户登陆失败！UserName=" + loginModel.UserName);
                    var json = new { type = 0, data = "", msg = returnmsg, backurl = "" };
                    return Json(json);
                }
            }
            else
            {
                OperationLog.ProcessInfo("登陆用户", HttpContext.GetUserIP(),
                    "用户登陆信息有误！");
                var json = new { type = 2, data = "", msg = returnmsg, backurl = "" };
                return Json(json);
            }
        }
    }
}