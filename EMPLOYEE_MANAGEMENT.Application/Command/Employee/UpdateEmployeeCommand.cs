using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;

namespace EMPLOYEE_MANAGEMENT.Application.Command.Employee
{
    public class UpdateEmployeeCommand : IRequest<ApiResponse<EmployeeDto>>
    {
        public int Id { get; set; }   // required

        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AadharNumber { get; set; }
        public int? RoleId { get; set; }
        public int? UserId { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime UpdatedDate{ get; set; }


    }
}
