using Deportes.Modelo.ComunModel;
using System.Text.Json;

namespace Deportes.api.Config
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
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
                _logger.LogError(ex, "[Error Handler] An exception occurred - {ErrorMessage}", ex.Message);

                await ManejarErrorDominioAsync(context, ex);
            }
        }

        private static async Task ManejarErrorDominioAsync(HttpContext context, Exception error)
        {
            var errorAMostrar = error as ErrorDeDominioException ?? new ErrorGenericoException();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)MapeadorCodigoStatus.ObtenerCodigoPorCategoria(errorAMostrar.Categoria);

            var json = JsonSerializer.Serialize(new { Message = errorAMostrar.Mensaje });
            await context.Response.WriteAsync(json);
        }
    }
}
