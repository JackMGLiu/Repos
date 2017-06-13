using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Codes
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            //_logger = LogManager.GetCurrentClassLogger();
            _logger = loggerFactory.CreateLogger<RequestLoggerMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            //_logger.LogInformation("Handling request: " + context.Request.Path);
            //_logger.LogInformation("IP="+ context.Connection.RemoteIpAddress.ToString());
            await _next.Invoke(context);
            //_logger.LogInformation("Finished handling request.");
        }
    }
}
