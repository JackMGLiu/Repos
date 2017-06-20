using Microsoft.AspNetCore.Builder;

namespace WebApplication1.Codes
{
    public static class RequestIPExtensions
    {
        public static IApplicationBuilder UseRequestIP(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggerMiddleware>();
        }
    }

    /// <summary>
    /// 登陆验证中间件
    /// </summary>
    public static class LoginValidateExtensions
    {
        public static IApplicationBuilder LoginValidate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
