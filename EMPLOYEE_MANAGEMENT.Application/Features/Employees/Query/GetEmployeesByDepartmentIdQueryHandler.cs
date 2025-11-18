using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query
{
    /// <summary>
    /// Handles the query to retrieve all employees who belong to a specific department.
    /// </summary>
    public class GetEmployeesByDepartmentIdQueryHandler
        : IRequestHandler<GetEmployeesByDepartmentIdQuery, ApiResponse<List<EmployeeDto>>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmployeesByDepartmentIdQueryHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">Repository for accessing employee data.</param>
        /// <param name="mapper">AutoMapper instance for mapping entities to DTOs.</param>
        public GetEmployeesByDepartmentIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the logic for fetching employees filtered by a specific department ID.
        /// </summary>
        /// <param name="request">The query containing the department ID.</param>
        /// <param name="cancellationToken">Token to cancel the request if needed.</param>
        /// <returns>A list of employees belonging to the given department, wrapped in an ApiResponse.</returns>
        public async Task<ApiResponse<List<EmployeeDto>>> Handle(GetEmployeesByDepartmentIdQuery request, CancellationToken cancellationToken)
        {
            // Fetch all employees
            var employees = await _employeeRepository.GetAllAsync();

            // Filter by DepartmentId
            var filteredEmployees = employees
                .Where(e => e.DepartmentId == request.DepartmentId)
                .ToList();

            // Map result to DTO
            var dtoList = _mapper.Map<List<EmployeeDto>>(filteredEmployees);

            return ApiResponse<List<EmployeeDto>>.Success(dtoList);
        }
    }
}
