using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command
{
    /// <summary>
    /// Command used to delete a department by ID.
    /// </summary>
    public class DeleteDepartmentCommand : IRequest<ApiResponse<string>>
    {
        /// <summary>
        /// ID of the department to delete.
        /// </summary>
        public int Id { get; set; }

        public DeleteDepartmentCommand(int id)
        {
            Id = id;
        }
    }
}
