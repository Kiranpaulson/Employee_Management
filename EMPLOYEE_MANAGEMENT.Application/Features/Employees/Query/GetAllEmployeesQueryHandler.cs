using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query
{
    /// <summary>
    /// Handles the GetAllEmployeesQuery request.
    /// Fetches all employees along with their related data,
    /// maps them to EmployeeDto, and returns the result wrapped in an ApiResponse.
    /// </summary>
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, ApiResponse<List<EmployeeDto>>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor to inject required dependencies.
        /// </summary>
        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve all employees.
        /// </summary>
        public async Task<ApiResponse<List<EmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            // Fetch all employees with relations
            var employees = await _employeeRepository.GetEmployeesWithRelationsAsync();

            // Map to DTO
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            // Return response
            return ApiResponse<List<EmployeeDto>>.Success(employeeDtos);
        }
    }
}
