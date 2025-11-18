using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System.Collections.Generic;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query
{
    /// <summary>
    /// Query used to retrieve all employees belonging to a specific department.
    /// Returns a list of EmployeeDto wrapped inside an ApiResponse.
    /// </summary>
    public class GetEmployeesByDepartmentIdQuery : IRequest<ApiResponse<List<EmployeeDto>>>
    {
        /// <summary>
        /// The department identifier whose employees need to be fetched.
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Initializes a new instance of the query with the provided department ID.
        /// </summary>
        public GetEmployeesByDepartmentIdQuery(int departmentId)
        {
            DepartmentId = departmentId;
        }
    }
}
