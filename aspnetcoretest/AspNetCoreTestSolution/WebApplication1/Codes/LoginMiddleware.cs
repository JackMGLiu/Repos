using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;

namespace WebApplication1.Codes
{
    /// <summary>
    /// 登陆验证中间件
    /// </summary>
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //var islogin = context.Session.Get("CurrentUser");
            //if (islogin!=null)
            //{
            //    await _next.Invoke(context);
            //}
            //else
            //{
            //    context.Response.Redirect("/Account/Login");
            //}
            //var cookie = context.Request.Cookies["browseweb"];

            await _next.Invoke(context);
        }
    }
}
