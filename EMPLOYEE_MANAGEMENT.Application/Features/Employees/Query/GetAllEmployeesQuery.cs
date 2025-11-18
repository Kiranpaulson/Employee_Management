using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query
{
    /// <summary>
    /// Query request used to retrieve all employees.
    /// Returns a list of EmployeeDto wrapped inside an ApiResponse.
    /// </summary>
    public class GetAllEmployeesQuery : IRequest<ApiResponse<List<EmployeeDto>>>
    {
    }
}
