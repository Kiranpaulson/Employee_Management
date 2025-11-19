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

namespace EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command
{
    /// <summary>
    /// Handles the <see cref="CreateRoleCommand"/> by creating a new Role,
    /// saving it to the database, and returning a response containing the created Role details.
    /// </summary>
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ApiResponse<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRoleCommandHandler"/> class.
        /// </summary>
        /// <param name="roleRepository">The repository used for role data operations.</param>
        /// <param name="mapper">The AutoMapper instance used to map between models.</param>
        public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
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
            var role = _mapper.Map<Role>(request);
            role.CreatedDate = DateTime.UtcNow;
            role.UpdatedDate = DateTime.UtcNow;

            var savedRole = await _roleRepository.CreateAsync(role);
            var dto = _mapper.Map<RoleDto>(savedRole);

            return ApiResponse<RoleDto>.Created(dto, "Role created successfully");
        }
    }
}
