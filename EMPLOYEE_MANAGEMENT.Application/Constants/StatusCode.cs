namespace EMPLOYEE_MANAGEMENT.Application.Constants
{
    /// <summary>
    /// Defines commonly used HTTP status codes for API responses.
    /// </summary>
    public enum StatusCode
    {
        /// <summary>
        /// Request processed successfully.
        /// </summary>
        OK = 200,

        /// <summary>
        /// Resource created successfully.
        /// </summary>
        Created = 201,

        /// <summary>
        /// The request is invalid or malformed.
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// The requested resource was not found.
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// An unexpected server-side error occurred.
        /// </summary>
        InternalServerError = 500
    }
}
