using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System.Collections.Generic;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Departments.Query
{
    /// <summary>
    /// Query to retrieve all departments.
    /// </summary>
    public class GetAllDepartmentsQuery : IRequest<ApiResponse<List<DepartmentDto>>>
    {
    }
}
