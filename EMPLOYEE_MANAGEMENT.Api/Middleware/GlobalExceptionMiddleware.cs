using EMPLOYEE_MANAGEMENT.Application.CustomException;
using System.Net;
using System.Text.Json;

namespace EMPLOYEE_MANAGEMENT.Api.Middleware
{
    /// <summary>
    /// Global middleware that intercepts all unhandled exceptions during request processing
    /// and returns a standardized JSON error response.
    /// </summary>
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger">Logger instance used to log exception details.</param>
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Executes the middleware logic and catches any unhandled exceptions.
        /// </summary>
        /// <param name="context">The current HTTP request context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Generates an appropriate JSON response for the given exception, including status codes
        /// and error messages based on the exception type.
        /// </summary>
        /// <param name="context">The HTTP context to write the response to.</param>
        /// <param name="exception">The exception thrown during request processing.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            object apiResponse;
            int statusCode;

            switch (exception)
            {
                case ValidationException validationEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    apiResponse = new
                    {
                        Success = false,
                        Message = validationEx.Message,
                        Errors = validationEx.Errors
                    };
                    break;

                case NotFoundException notFoundEx:
                    statusCode = (int)HttpStatusCode.NotFound;
                    apiResponse = new
                    {
                        Success = false,
                        Message = notFoundEx.Message
                    };
                    break;

                case UnauthorizedAccessException unauthorizedEx:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    apiResponse = new
                    {
                        Success = false,
                        Message = unauthorizedEx.Message
                    };
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    apiResponse = new
                    {
                        Success = false,
                        Message = "An unexpected error occurred.",
                        Details = exception.Message
                    };
                    break;
            }

            context.Response.StatusCode = statusCode;

            var json = JsonSerializer.Serialize(apiResponse);
            await context.Response.WriteAsync(json);
        }
    }
}
