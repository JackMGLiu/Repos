using Microsoft.AspNetCore.Http;

namespace NetCoreUtils
{
    public static class NetHelper
    {
        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetUserIP(this HttpContext context)
        {
            return context.Connection.LocalIpAddress.ToString();
        }
    }
}