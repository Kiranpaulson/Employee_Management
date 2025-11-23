using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.CustomException; // For NotFoundException
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using EMPLOYEE_MANAGEMENT.Application.logging;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command
{
    /// <summary>
    /// Handles the <see cref="DeleteRoleCommand"/> by deleting an existing role
    /// and returning a success message. Throws a <see cref="NotFoundException"/> if the role does not exist.
    /// Includes logging at start, operation, and completion/error.
    /// </summary>
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ApiResponse<string>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IAppLogger<DeleteRoleCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRoleCommandHandler"/> class.
        /// </summary>
        /// <param name="roleRepository">The repository used for role data operations.</param>
        /// <param name="logger">Application logger for logging operations.</param>
        public DeleteRoleCommandHandler(
            IRoleRepository roleRepository,
            IAppLogger<DeleteRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        /// <summary>
        /// Handles the role deletion request by checking if the role exists,
        /// deleting it from the repository, and returning a success response.
        /// </summary>
        /// <param name="request">The <see cref="DeleteRoleCommand"/> containing the ID of the role to delete.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a success message if deletion succeeded.
        /// </returns>
        /// <exception cref="NotFoundException">Thrown when the role with the specified ID does not exist.</exception>
        public async Task<ApiResponse<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting DeleteRoleCommandHandler for Role Id: {0}", request.Id);

            try
            {
                // Retrieve the role by ID
                var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);
                _logger.LogInformation("Fetched role from repository. Role exists: {0}", role != null);

                // Throw NotFoundException if role does not exist
                if (role == null)
                {
                    _logger.LogWarning("Role with Id {0} not found", request.Id);
                    throw new NotFoundException($"Role with Id {request.Id} not found.");
                }

                // Delete the role
                await _roleRepository.DeleteAsync(role, cancellationToken);
                _logger.LogInformation("Role with Id {0} deleted successfully", request.Id);

                _logger.LogInformation("DeleteRoleCommandHandler completed successfully for Role Id: {0}", request.Id);
                return ApiResponse<string>.Success("Role deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogErrors("Error occurred while deleting Role Id: {0}. Exception: {1}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
