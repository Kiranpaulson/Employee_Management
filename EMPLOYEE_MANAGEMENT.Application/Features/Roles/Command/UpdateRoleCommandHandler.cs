using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command
{
    /// <summary>
    /// Handles the <see cref="UpdateRoleCommand"/> by updating an existing Role
    /// and returning a response containing the updated Role details.
    /// </summary>
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ApiResponse<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleCommandHandler"/> class.
        /// </summary>
        /// <param name="roleRepository">The repository used for role data operations.</param>
        /// <param name="mapper">The AutoMapper instance used to map between models.</param>
        public UpdateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the role update request by retrieving the entity,
        /// updating its fields, saving to the repository, and returning the updated role as a DTO.
        /// </summary>
        /// <param name="request">The command containing role update details.</param>
        /// <param name="cancellationToken">A cancellation token for task cancellation.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the updated <see cref="RoleDto"/>
        /// and a success message.
        /// </returns>
        public async Task<ApiResponse<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetById(request.Id);

            if (role == null)
                throw new NotFoundException($"Role with Id {request.Id} not found");

            if (!string.IsNullOrEmpty(request.Name)) role.Name = request.Name;
            if (!string.IsNullOrEmpty(request.Description)) role.Description = request.Description;

            role.UpdatedDate = DateTime.UtcNow;

            await _roleRepository.UpdateAsync(role);
            var dto = _mapper.Map<RoleDto>(role);

            return ApiResponse<RoleDto>.Success(dto, "Role updated successfully");
        }
    }
}
