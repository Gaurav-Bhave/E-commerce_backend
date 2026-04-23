using System.Net;
using System.Text.Json;

namespace Practiced_E_commerce.Execption
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate delegeate, ILogger<ExceptionMiddleware> logger)
        {
            _next = delegeate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong : {ex.Message}");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var responce = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error . Please try again later"
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(responce));
            }
        }
    }
}
