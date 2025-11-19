using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command
{
    /// <summary>
    /// Command used to create a new department.
    /// </summary>
    public class CreateDepartmentCommand : IRequest<ApiResponse<DepartmentDto>>
    {
        /// <summary>
        /// Name of the new department.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the new department.
        /// </summary>
        public string Description { get; set; }
    }
}
