using EMPLOYEE_MANAGEMENT.Application.Constants;

namespace EMPLOYEE_MANAGEMENT.Application.Wrapper
{
    /// <summary>
    /// Generic wrapper class for API responses.
    /// Encapsulates HTTP-like status, data, and message.
    /// </summary>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Status code representing the response result.
        /// Uses the StatusCode enum.
        /// </summary>
        public StatusCode Status { get; set; }

        /// <summary>
        /// Actual data returned by the API.
        /// Nullable to support empty results.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Descriptive message about the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new ApiResponse.
        /// </summary>
        public ApiResponse(StatusCode status, T? data, string message)
        {
            Status = status;
            Data = data;
            Message = message;
        }

        /// <summary>
        /// Successful response (200 OK).
        /// </summary>
        public static ApiResponse<T> Success(T? data, string message = "Request processed successfully")
        {
            return new ApiResponse<T>(StatusCode.OK, data, message);
        }

        /// <summary>
        /// Successful resource creation (201 Created).
        /// </summary>
        public static ApiResponse<T> Created(T? data, string message = "Resource created successfully")
        {
            return new ApiResponse<T>(StatusCode.Created, data, message);
        }

        /// <summary>
        /// Bad request or validation failure (400 Bad Request).
        /// </summary>
        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T>(StatusCode.BadRequest, default, message);
        }
    }
}
