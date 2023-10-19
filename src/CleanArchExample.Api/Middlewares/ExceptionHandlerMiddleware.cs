using MongoDB.Bson.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchExample.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsJsonAsync(new
                    {
                        ErrorId = errorId,
                        Messages = new List<string> { "Ocorreu um erro inesperado. Contate o suporte." }
                    });
                }

                var route = context.Request.Path.ToString().ToLower().Trim('/');
                _logger.LogError(ex, $"Id: ${errorId}, Rota: {route}");
            }
        }
    }
}
