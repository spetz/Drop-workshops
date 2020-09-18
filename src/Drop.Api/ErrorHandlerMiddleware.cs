using System;
using System.Threading.Tasks;
using Drop.Application.Exceptions;
using Drop.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Drop.Api
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented
        };

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                switch (exception)
                {
                    case AppException appException:
                        await HandleCustomException(context, appException.Code, appException.Message);
                        return;
                    case DomainException domainException:
                        await HandleCustomException(context, domainException.Code, domainException.Message);
                        return;
                    default:
                        throw;
                }
            }
        }

        private static async Task HandleCustomException(HttpContext context, string code, string message)
        {
            context.Response.StatusCode = 400;
            var response = new
            {
                code, message
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, JsonSettings));
        }
    }
}