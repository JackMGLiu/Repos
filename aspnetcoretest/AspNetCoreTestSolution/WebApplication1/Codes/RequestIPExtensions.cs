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
}
