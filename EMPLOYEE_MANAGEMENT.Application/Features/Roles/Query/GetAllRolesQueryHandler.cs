using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.logging;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Roles.Query
{
    /// <summary>
    /// Handles the <see cref="GetAllRolesQuery"/> request to fetch all roles from the repository.
    /// Includes logging at start, operation, and completion/error.
    /// </summary>
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, ApiResponse<List<RoleDto>>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetAllRolesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllRolesQueryHandler"/> class.
        /// </summary>
        /// <param name="roleRepository">Repository for accessing role data.</param>
        /// <param name="mapper">AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="logger">Application logger for logging operations.</param>
        public GetAllRolesQueryHandler(
            IRoleRepository roleRepository,
            IMapper mapper,
            IAppLogger<GetAllRolesQueryHandler> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get all roles.
        /// </summary>
        /// <param name="request">The <see cref="GetAllRolesQuery"/> request object.</param>
        /// <param name="cancellationToken">Cancellation token for the async operation.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a list of <see cref="RoleDto"/> objects.
        /// </returns>
        /// <exception cref="NotFoundException">Thrown when no roles are found in the repository.</exception>
        public async Task<ApiResponse<List<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting GetAllRolesQueryHandler...");

            try
            {
                _logger.LogInformation("Fetching all roles from repository...");
                var roles = await _roleRepository.GetAllAsync(cancellationToken);

                if (roles == null || roles.Count == 0)
                {
                    _logger.LogWarning("No roles found in the repository.");
                    throw new NotFoundException("No roles found.");
                }

                _logger.LogInformation("Mapping roles to DTOs...");
                var dtoList = _mapper.Map<List<RoleDto>>(roles);

                _logger.LogInformation("GetAllRolesQueryHandler completed successfully. Total roles: {0}", dtoList.Count);
                return ApiResponse<List<RoleDto>>.Success(dtoList);
            }
            catch (System.Exception ex)
            {
                _logger.LogErrors("Error occurred in GetAllRolesQueryHandler: {0}", ex.Message);
                throw;
            }
        }
    }
}
