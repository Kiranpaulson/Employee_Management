using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmployeeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">Repository used to access employee data.</param>
        /// <param name="mapper">AutoMapper instance used for entity-to-DTO conversion.</param>
        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the request to fetch a single employee by ID.
        /// </summary>
        /// <param name="request">The query request containing the employee ID.</param>
        /// <param name="cancellationToken">Token used to cancel the operation.</param>
        /// <returns>An ApiResponse containing the employee details or an error.</returns>
        /// <exception cref="NotFoundException">
        /// Thrown when an employee with the specified ID does not exist.
        /// </exception>
        public async Task<ApiResponse<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeWithRelationsByIdAsync(request.Id);

            if (employee == null)
            {
                throw new NotFoundException($"Employee with Id {request.Id} not found");
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return ApiResponse<EmployeeDto>.Success(employeeDto);
        }
    }
}
