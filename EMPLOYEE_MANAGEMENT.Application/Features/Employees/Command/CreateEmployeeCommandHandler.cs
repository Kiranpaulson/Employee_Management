using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command
{
    /// <summary>
    /// Handles the <see cref="CreateEmployeeCommand"/> by creating a new employee,
    /// saving it to the database, and returning a response containing the created employee details.
    /// </summary>
    public class CreateEmployeeCommandHandler
        : IRequestHandler<CreateEmployeeCommand, ApiResponse<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEmployeeCommandHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">The repository used for employee data operations.</param>
        /// <param name="mapper">The AutoMapper instance used to map between models.</param>
        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the employee creation request by mapping the command to an entity,
        /// saving it to the repository, and returning the created employee as a DTO.
        /// </summary>
        /// <param name="request">The command containing employee creation details.</param>
        /// <param name="cancellationToken">A cancellation token for task cancellation.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the created <see cref="EmployeeDto"/>
        /// and a success message.
        /// </returns>
        public async Task<ApiResponse<EmployeeDto>> Handle(
            CreateEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            // convert request → entity
            var employee = _mapper.Map<Employee>(request);

            employee.CreatedDate = DateTime.UtcNow;
            employee.UpdatedDate = DateTime.UtcNow;

            // save to DB
            var savedEmployee = await _employeeRepository.CreateAsync(employee);

            // convert entity → dto
            var dto = _mapper.Map<EmployeeDto>(savedEmployee);

            return ApiResponse<EmployeeDto>.Created(dto, "Employee created successfully");
        }
    }
}
