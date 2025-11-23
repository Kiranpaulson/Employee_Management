using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EMPLOYEE_MANAGEMENT.Application.logging;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command
{
    /// <summary>
    /// Handles the <see cref="CreateEmployeeCommand"/> by creating a new employee,
    /// saving it to the database, and returning a response containing the created employee details.
    /// Includes logging at start, during operation, and end.
    /// </summary>
    public class CreateEmployeeCommandHandler
        : IRequestHandler<CreateEmployeeCommand, ApiResponse<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateEmployeeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEmployeeCommandHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">The repository used for employee data operations.</param>
        /// <param name="mapper">The AutoMapper instance used to map between models.</param>
        /// <param name="logger">The application logger for logging information, warnings, and errors.</param>
        public CreateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IAppLogger<CreateEmployeeCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the creation of a new employee.
        /// Maps the command to an employee entity, saves it in the repository, 
        /// maps the result to a DTO, and returns a success response.
        /// Logs the process at key stages.
        /// </summary>
        /// <param name="request">The <see cref="CreateEmployeeCommand"/> containing employee details.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
        /// <returns>An <see cref="ApiResponse{EmployeeDto}"/> containing the created employee.</returns>
        /// <exception cref="Exception">Throws if any error occurs while creating the employee.</exception>
        public async Task<ApiResponse<EmployeeDto>> Handle(
            CreateEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting CreateEmployeeCommandHandler for Employee Name: {0}", request.Name);

            try
            {
                // Map request → entity
                var employee = _mapper.Map<Employee>(request);
                _logger.LogInformation("Mapped CreateEmployeeCommand to Employee entity for Employee Name: {0}", request.Name);

                employee.CreatedDate = DateTime.UtcNow;
                employee.UpdatedDate = DateTime.UtcNow;

                // Save to DB
                var savedEmployee = await _employeeRepository.CreateAsync(employee, cancellationToken);
                _logger.LogInformation("Employee saved to repository with Id: {0}", savedEmployee.Id);

                // Map entity → DTO
                var dto = _mapper.Map<EmployeeDto>(savedEmployee);

                _logger.LogInformation("CreateEmployeeCommandHandler completed successfully for Employee Id: {0}", savedEmployee.Id);
                return ApiResponse<EmployeeDto>.Created(dto, "Employee created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogErrors("Error occurred while creating employee: {0}. Exception: {1}", request.Name, ex.Message);
                throw;
            }
        }
    }
}
