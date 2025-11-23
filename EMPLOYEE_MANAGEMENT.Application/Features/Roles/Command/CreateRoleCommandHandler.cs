using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.logging;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command
{
    /// <summary>
    /// Handles the <see cref="CreateRoleCommand"/> by creating a new Role,
    /// saving it to the database, and returning a response containing the created Role details.
    /// Includes logging for start, operation, and completion/error.
    /// </summary>
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ApiResponse<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateRoleCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRoleCommandHandler"/> class.
        /// </summary>
        /// <param name="roleRepository">The repository used for role data operations.</param>
        /// <param name="mapper">The AutoMapper instance used to map between models.</param>
        /// <param name="logger">Application logger for logging operations.</param>
        public CreateRoleCommandHandler(
            IRoleRepository roleRepository,
            IMapper mapper,
            IAppLogger<CreateRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the role creation request by mapping the command to an entity,
        /// saving it to the repository, and returning the created role as a DTO.
        /// </summary>
        /// <param name="request">The command containing role creation details.</param>
        /// <param name="cancellationToken">A cancellation token for task cancellation.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the created <see cref="RoleDto"/>
        /// and a success message.
        /// </returns>
        public async Task<ApiResponse<RoleDto>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting CreateRoleCommandHandler for Role Name: {0}", request.Name);

            try
            {
                // Map command → entity
                var role = _mapper.Map<Role>(request);
                role.CreatedDate = DateTime.UtcNow;
                role.UpdatedDate = DateTime.UtcNow;
                _logger.LogInformation("Mapped CreateRoleCommand to Role entity for Role Name: {0}", request.Name);

                // Save to DB
                var savedRole = await _roleRepository.CreateAsync(role, cancellationToken);
                _logger.LogInformation("Role saved to repository with Id: {0}", savedRole.Id);

                // Map entity → DTO
                var dto = _mapper.Map<RoleDto>(savedRole);

                _logger.LogInformation("CreateRoleCommandHandler completed successfully for Role Id: {0}", savedRole.Id);
                return ApiResponse<RoleDto>.Created(dto, "Role created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogErrors("Error occurred while creating role: {0}. Exception: {1}", request.Name, ex.Message);
                throw;
            }
        }
    }
}
