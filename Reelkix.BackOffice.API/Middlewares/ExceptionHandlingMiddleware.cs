using System.Net;
using System.Text.Json;
using FluentValidation;

namespace Reelkix.BackOffice.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    Message = "Validation failed",
                    Errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    Message = "An unexpected error occurred. Please try again later."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        }

    }
}
