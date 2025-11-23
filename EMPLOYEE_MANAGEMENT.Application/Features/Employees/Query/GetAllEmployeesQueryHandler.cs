using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query
{
    /// <summary>
    /// Handles the <see cref="GetAllEmployeesQuery"/> request.
    /// Retrieves all employees from the repository along with related data,
    /// maps them to <see cref="EmployeeDto"/>, and returns them wrapped in an <see cref="ApiResponse{T}"/>.
    /// Logs the process at start, during execution, and on completion.
    /// </summary>
    public class GetAllEmployeesQueryHandler
        : IRequestHandler<GetAllEmployeesQuery, ApiResponse<List<EmployeeDto>>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetAllEmployeesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllEmployeesQueryHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">Repository used to fetch employee data.</param>
        /// <param name="mapper">Mapper used to convert employee entities to DTOs.</param>
        /// <param name="logger">Application logger to log information, warnings, and errors.</param>
        public GetAllEmployeesQueryHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IAppLogger<GetAllEmployeesQueryHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the <see cref="GetAllEmployeesQuery"/> by fetching all employees from the repository,
        /// mapping them to DTOs, and returning them wrapped in an <see cref="ApiResponse{T}"/>.
        /// </summary>
        /// <param name="request">The query request to get all employees.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>An <see cref="ApiResponse{T}"/> containing the list of <see cref="EmployeeDto"/> objects.</returns>
        /// <exception cref="NotFoundException">Thrown when no employees are found in the system.</exception>
        public async Task<ApiResponse<List<EmployeeDto>>> Handle(
            GetAllEmployeesQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting GetAllEmployeesQueryHandler.");

            try
            {
                _logger.LogInformation("Fetching employees from repository...");
                var employees = await _employeeRepository
                    .GetEmployeesWithRelationsAsync(cancellationToken);

                if (employees == null || employees.Count == 0)
                {
                    _logger.LogWarning("No employees found in the system.");
                    throw new NotFoundException("No employees found in the system.");
                }

                _logger.LogInformation("Mapping employee entities to DTOs...");
                var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

                _logger.LogInformation("GetAllEmployeesQueryHandler completed successfully. Total employees: {0}", employeeDtos.Count);
                return ApiResponse<List<EmployeeDto>>.Success(employeeDtos);
            }
            catch (Exception ex)
            {
                _logger.LogErrors("Error in GetAllEmployeesQueryHandler: {0}", ex.Message);
                throw;
            }
        }
    }
}
