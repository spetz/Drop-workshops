using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Drop.Api
{
    public class DummyMiddleware : IMiddleware
    {
        private readonly ILogger<DummyMiddleware> _logger;

        public DummyMiddleware(ILogger<DummyMiddleware> logger)
        {
            _logger = logger;
        }
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogInformation("I' the dummy middleware.");
            await next(context);
        }
    }
}