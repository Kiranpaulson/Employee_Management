using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.logging;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query
{
    /// <summary>
    /// Handles the retrieval of a single employee by ID, including related entities.
    /// </summary>
    public class GetEmployeeByIdQueryHandler
        : IRequestHandler<GetEmployeeByIdQuery, ApiResponse<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetEmployeeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmployeeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">Repository used to access employee data.</param>
        /// <param name="mapper">AutoMapper instance used for entity-to-DTO conversion.</param>
        /// <param name="logger">Application logger for logging information, warnings, and errors.</param>
        public GetEmployeeByIdQueryHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IAppLogger<GetEmployeeByIdQueryHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the request to fetch a single employee by ID.
        /// Throws <see cref="NotFoundException"/> if the employee does not exist.
        /// </summary>
        /// <param name="request">The query request containing the employee ID.</param>
        /// <param name="cancellationToken">Token used to cancel the operation.</param>
        /// <returns>An <see cref="ApiResponse{EmployeeDto}"/> containing the employee details.</returns>
        public async Task<ApiResponse<EmployeeDto>> Handle(
            GetEmployeeByIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting GetEmployeeByIdQueryHandler for Employee Id: {0}", request.Id);

            try
            {
                _logger.LogInformation("Fetching employee from repository...");
                var employee = await _employeeRepository.GetEmployeeWithRelationsByIdAsync(request.Id, cancellationToken);

                if (employee == null)
                {
                    _logger.LogWarning("Employee with Id {0} not found.", request.Id);
                    throw new NotFoundException($"Employee with Id {request.Id} not found");
                }

                _logger.LogInformation("Mapping employee entity to DTO...");
                var employeeDto = _mapper.Map<EmployeeDto>(employee);

                _logger.LogInformation("GetEmployeeByIdQueryHandler completed successfully for Employee Id: {0}", request.Id);
                return ApiResponse<EmployeeDto>.Success(employeeDto);
            }
            catch (Exception ex)
            {
                _logger.LogErrors("Error in GetEmployeeByIdQueryHandler for Employee Id {0}: {1}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
