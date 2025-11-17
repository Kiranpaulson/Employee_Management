using EMPLOYEE_MANAGEMENT.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Wrapper
{
    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public T? Data { get; set; }  // Nullable to handle null
        public string Message { get; set; }

        public ApiResponse(int statusCode, T? data, string message)
        {
            Status = statusCode;
            Data = data;
            Message = message;
        }

        public static ApiResponse<T> Success(T? data, string message = "Request processed successfully")
        {
            return new ApiResponse<T>(StatusCode.OK, data, message);
        }

        public static ApiResponse<T> Created(T? data, string message = "Resource created successfully")
        {
            return new ApiResponse<T>(StatusCode.Created, data, message);
        }

        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T>(StatusCode.BadRequest, default, message);
        }
    }
    
}
