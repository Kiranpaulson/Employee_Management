using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.logging;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command
{
    /// <summary>
    /// Handles the <see cref="UpdateRoleCommand"/> by updating an existing role
    /// and returning a response containing the updated role details.
    /// Throws a <see cref="NotFoundException"/> if the role does not exist.
    /// Includes logging at start, operation, and completion/error.
    /// </summary>
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ApiResponse<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<UpdateRoleCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleCommandHandler"/> class.
        /// </summary>
        /// <param name="roleRepository">The repository used for role data operations.</param>
        /// <param name="mapper">The AutoMapper instance used to map between models and DTOs.</param>
        /// <param name="logger">Application logger for logging operations.</param>
        public UpdateRoleCommandHandler(
            IRoleRepository roleRepository,
            IMapper mapper,
            IAppLogger<UpdateRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the role update request by retrieving the role entity,
        /// updating its fields, saving it to the repository, and returning the updated role as a DTO.
        /// </summary>
        /// <param name="request">The <see cref="UpdateRoleCommand"/> containing role update details.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the updated <see cref="RoleDto"/>
        /// and a success message.
        /// </returns>
        /// <exception cref="NotFoundException">Thrown when the role with the specified ID does not exist.</exception>
        public async Task<ApiResponse<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting UpdateRoleCommandHandler for Role Id: {0}", request.Id);

            try
            {
                // Retrieve the role by ID
                var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);
                _logger.LogInformation("Fetched role from repository. Role exists: {0}", role != null);

                // Throw NotFoundException if role does not exist
                if (role == null)
                {
                    _logger.LogWarning("Role with Id {0} not found", request.Id);
                    throw new NotFoundException($"Role with Id {request.Id} not found");
                }

                // Update only the provided fields
                if (!string.IsNullOrEmpty(request.Name)) role.Name = request.Name;
                if (!string.IsNullOrEmpty(request.Description)) role.Description = request.Description;

                role.UpdatedDate = DateTime.UtcNow;
                _logger.LogInformation("Updated role fields for Role Id: {0}", request.Id);

                // Save changes to the repository
                await _roleRepository.UpdateAsync(role, cancellationToken);
                _logger.LogInformation("Role with Id {0} updated successfully", request.Id);

                // Map entity to DTO
                var dto = _mapper.Map<RoleDto>(role);

                _logger.LogInformation("UpdateRoleCommandHandler completed successfully for Role Id: {0}", request.Id);
                return ApiResponse<RoleDto>.Success(dto, "Role updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogErrors("Error occurred while updating Role Id: {0}. Exception: {1}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
