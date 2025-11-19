using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command
{
    /// <summary>
    /// Command to delete an existing role.
    /// </summary>
    public class DeleteRoleCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }

        public DeleteRoleCommand(int id)
        {
            Id = id;
        }
    }
}
