using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace EMPLOYEE_MANAGEMENT.Application.Query.Employee
{
    public class GetAllEmployeesQuery : IRequest<ApiResponse<List<EmployeeDto>>>
    {
    }
}
