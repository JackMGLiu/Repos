using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
