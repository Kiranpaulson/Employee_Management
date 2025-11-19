using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command
{
    /// <summary>
    /// Command used to update an existing department.
    /// </summary>
    public class UpdateDepartmentCommand : IRequest<ApiResponse<DepartmentDto>>
    {
        /// <summary>
        /// ID of the department to update.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Updated name of the department.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Updated description of the department.
        /// </summary>
        public string Description { get; set; }
    }
}
