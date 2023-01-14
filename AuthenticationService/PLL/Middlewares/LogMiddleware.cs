using AuthenticationService.PLL.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AuthenticationService.PLL.Middlewares
{
    public class LogMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var remoteIpAddress = httpContext.Connection.RemoteIpAddress;

            _logger.WriteEvent($"Я твой Middleware, твой ip - {remoteIpAddress}");
            await _next(httpContext);
        }

    }
}
