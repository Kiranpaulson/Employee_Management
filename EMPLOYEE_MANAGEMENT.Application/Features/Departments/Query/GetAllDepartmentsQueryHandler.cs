using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.CustomException; // For NotFoundException
using EMPLOYEE_MANAGEMENT.Application.logging;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Departments.Handler
{
    /// <summary>
    /// Handles the <see cref="GetAllDepartmentsQuery"/> by retrieving all departments
    /// from the repository and returning them as a list of <see cref="DepartmentDto"/>.
    /// Logs the operations and throws <see cref="NotFoundException"/> if no departments are found.
    /// </summary>
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, ApiResponse<List<DepartmentDto>>>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetAllDepartmentsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllDepartmentsQueryHandler"/> class.
        /// </summary>
        /// <param name="repo">The repository used for department data operations.</param>
        /// <param name="mapper">The AutoMapper instance used to map entities to DTOs.</param>
        /// <param name="logger">Application logger for logging operations.</param>
        public GetAllDepartmentsQueryHandler(IDepartmentRepository repo, IMapper mapper, IAppLogger<GetAllDepartmentsQueryHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to retrieve all departments.
        /// </summary>
        /// <param name="request">The <see cref="GetAllDepartmentsQuery"/> request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a list of <see cref="DepartmentDto"/>.
        /// </returns>
        /// <exception cref="NotFoundException">Thrown when no departments exist in the repository.</exception>
        public async Task<ApiResponse<List<DepartmentDto>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting GetAllDepartmentsQueryHandler.");

            // Fetch all departments from the repository
            var list = await _repo.GetAllAsync(cancellationToken);

            // Throw NotFoundException if no departments exist
            if (list == null || list.Count == 0)
            {
                _logger.LogWarning("No departments found in the repository.");
                throw new NotFoundException("No departments found.");
            }

            // Map entities to DTOs
            var dto = _mapper.Map<List<DepartmentDto>>(list);

            _logger.LogInformation("GetAllDepartmentsQueryHandler completed successfully. Total departments: {0}", dto.Count);

            // Return a successful API response
            return ApiResponse<List<DepartmentDto>>.Success(dto);
        }
    }
}
