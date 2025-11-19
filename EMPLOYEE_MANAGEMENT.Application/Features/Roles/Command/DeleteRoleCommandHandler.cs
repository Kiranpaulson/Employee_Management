using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command
{
    /// <summary>
    /// Handles the <see cref="DeleteRoleCommand"/> by deleting an existing Role
    /// and returning a success/failure message.
    /// </summary>
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ApiResponse<string>>
    {
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRoleCommandHandler"/> class.
        /// </summary>
        /// <param name="roleRepository">The repository used for role data operations.</param>
        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Handles the role deletion request by retrieving the entity,
        /// deleting it from the repository, and returning a message.
        /// </summary>
        /// <param name="request">The command containing role deletion details.</param>
        /// <param name="cancellationToken">A cancellation token for task cancellation.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a success/failure message.
        /// </returns>
        public async Task<ApiResponse<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetById(request.Id);
            if (role == null)
                return ApiResponse<string>.Fail("Role not found");

            await _roleRepository.DeleteAsync(role);
            return ApiResponse<string>.Success("Role deleted successfully");
        }
    }
}
