using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Roles.Query
{
    /// <summary>
    /// Handles the <see cref="GetAllRolesQuery"/> by fetching all Roles from the repository.
    /// </summary>
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, ApiResponse<List<RoleDto>>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllRolesQueryHandler"/> class.
        /// </summary>
        /// <param name="roleRepository">The repository used for role data operations.</param>
        /// <param name="mapper">The AutoMapper instance used to map between models.</param>
        public GetAllRolesQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles fetching all roles from the repository and mapping them to DTOs.
        /// </summary>
        /// <param name="request">The query request (empty for fetching all).</param>
        /// <param name="cancellationToken">A cancellation token for task cancellation.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a list of <see cref="RoleDto"/>.
        /// </returns>
        public async Task<ApiResponse<List<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllAsync();
            var dtoList = _mapper.Map<List<RoleDto>>(roles);

            return ApiResponse<List<RoleDto>>.Success(dtoList);
        }
    }
}
