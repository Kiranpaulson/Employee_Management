using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Query.Employee;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Persistance;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Handler
{
    public class GetEmployeesByDepartmentIdQueryHandler
        : IRequestHandler<GetEmployeesByDepartmentIdQuery, ApiResponse<List<EmployeeDto>>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeesByDepartmentIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

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
