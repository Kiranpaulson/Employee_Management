using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System.Collections.Generic;

namespace EMPLOYEE_MANAGEMENT.Application.Query.Employee
{
    public class GetEmployeesByDepartmentIdQuery : IRequest<ApiResponse<List<EmployeeDto>>>
    {
        public int DepartmentId { get; set; }

        public GetEmployeesByDepartmentIdQuery(int departmentId)
        {
            DepartmentId = departmentId;
        }
    }
}
