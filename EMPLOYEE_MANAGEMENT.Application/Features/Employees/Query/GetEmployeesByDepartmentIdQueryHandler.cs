using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query
{
    /// <summary>
    /// Handles the query to retrieve employees belonging to a specific department.
    /// Throws a <see cref="NotFoundException"/> if no employees are found.
    /// </summary>
    public class GetEmployeesByDepartmentIdQueryHandler
        : IRequestHandler<GetEmployeesByDepartmentIdQuery, ApiResponse<List<EmployeeDto>>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetEmployeesByDepartmentIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the handler.
        /// </summary>
        /// <param name="employeeRepository">Repository for accessing employee data.</param>
        /// <param name="mapper">Mapper for converting entities to DTOs.</param>
        /// <param name="logger">Application logger for logging operations.</param>
        public GetEmployeesByDepartmentIdQueryHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IAppLogger<GetEmployeesByDepartmentIdQueryHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all employees that belong to the specified department ID.
        /// </summary>
        /// <param name="request">The query containing the department ID.</param>
        /// <param name="cancellationToken">Token for cancelling the request.</param>
        /// <returns>A list of employees in the department wrapped in an <see cref="ApiResponse{T}"/>.</returns>
        /// <exception cref="NotFoundException">Thrown when no employees are found for this department.</exception>
        public async Task<ApiResponse<List<EmployeeDto>>> Handle(
            GetEmployeesByDepartmentIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting GetEmployeesByDepartmentIdQueryHandler for DepartmentId: {0}", request.DepartmentId);

            try
            {
                _logger.LogInformation("Fetching all employees from repository...");
                var employees = await _employeeRepository.GetEmployeesWithRelationsAsync(cancellationToken);

                _logger.LogInformation("Filtering employees by DepartmentId: {0}", request.DepartmentId);
                var filteredEmployees = employees
                    .Where(e => e.DepartmentId == request.DepartmentId)
                    .ToList();

                if (!filteredEmployees.Any())
                {
                    _logger.LogWarning("No employees found for DepartmentId: {0}", request.DepartmentId);
                    throw new NotFoundException($"No employees found for DepartmentId {request.DepartmentId}");
                }

                _logger.LogInformation("Mapping filtered employees to DTOs...");
                var dtoList = _mapper.Map<List<EmployeeDto>>(filteredEmployees);

                _logger.LogInformation("GetEmployeesByDepartmentIdQueryHandler completed successfully. Total employees: {0}", dtoList.Count);
                return ApiResponse<List<EmployeeDto>>.Success(dtoList);
            }
            catch (Exception ex)
            {
                _logger.LogErrors("Error in GetEmployeesByDepartmentIdQueryHandler for DepartmentId {0}: {1}", request.DepartmentId, ex.Message);
                throw;
            }
        }
    }
}
