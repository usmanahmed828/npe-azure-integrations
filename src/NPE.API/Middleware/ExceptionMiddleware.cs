using NPE.Core.Common.Responses;
using NPE.Core.Common.Validation;
using System.Net;
using System.Text.Json;

namespace NPE.API.Middleware
{
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            IWebHostEnvironment env,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message,
                    ex);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.NotFound,
                    ex.Message,
                    ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.Unauthorized,
                    ex.Message,
                    ex);
            }
            catch (ValidationException ex)
            {
                await HandleValidationAsync(
                    context,
                    ex.Errors);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.InternalServerError,
                    "Unexpected server error",
                    ex);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            HttpStatusCode statusCode,
            string message,
            Exception ex)
        {
            _logger.LogError(
                ex,
                "Unhandled exception. TraceId:{TraceId}",
                context.TraceIdentifier);

            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                (int)statusCode;

            var response =
                new ApiResponse<object>
                {
                    Success = false,

                    Message = message,

                    Errors =
                        BuildErrors(
                            context,
                            ex)
                };

            var json =
                JsonSerializer.Serialize(
                    response,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy =
                            JsonNamingPolicy.CamelCase
                    });

            await context.Response.WriteAsync(json);
        }

        private async Task HandleValidationAsync(
            HttpContext context,
            IEnumerable<string> errors)
        {
            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                (int)HttpStatusCode.BadRequest;

            var response =
                new ApiResponse<object>
                {
                    Success = false,

                    Message =
                        "Validation failed",

                    Errors =
                        errors.ToList()
                };

            var json =
                JsonSerializer.Serialize(
                    response,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy =
                            JsonNamingPolicy.CamelCase
                    });

            await context.Response.WriteAsync(json);
        }

        private List<string> BuildErrors(
            HttpContext context,
            Exception ex)
        {
            var errors =
                new List<string>
                {
                    $"TraceId: {context.TraceIdentifier}"
                };

            if (_env.IsDevelopment())
            {
                errors.Add(
                    $"Message: {ex.Message}");

                errors.Add(
                    $"Type: {ex.GetType().FullName}");

                if (ex.InnerException != null)
                {
                    errors.Add(
                        $"Inner: {ex.InnerException.Message}");
                }

                if (!string.IsNullOrWhiteSpace(
                    ex.StackTrace))
                {
                    errors.Add(
                        $"StackTrace: {ex.StackTrace}");
                }
            }

            return errors;
        }
    }
}