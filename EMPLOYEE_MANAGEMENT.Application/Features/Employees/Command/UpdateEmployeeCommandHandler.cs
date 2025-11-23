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

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command
{
    /// <summary>
    /// Handles the <see cref="UpdateEmployeeCommand"/> by validating the employee's existence,
    /// updating only the provided fields, saving the changes, and returning the updated employee data.
    /// </summary>
    public class UpdateEmployeeCommandHandler
        : IRequestHandler<UpdateEmployeeCommand, ApiResponse<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<UpdateEmployeeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEmployeeCommandHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">The repository responsible for employee data operations.</param>
        /// <param name="mapper">The AutoMapper instance for converting between entity and DTO.</param>
        /// <param name="logger">Application logger for logging information, warnings, and errors.</param>
        public UpdateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IAppLogger<UpdateEmployeeCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the employee update request by applying only the non-null fields,
        /// updating the modification timestamp, and saving changes.
        /// </summary>
        /// <param name="request">The update command containing new values for the employee.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>Returns an updated <see cref="EmployeeDto"/> wrapped inside an <see cref="ApiResponse{T}"/>.</returns>
        /// <exception cref="NotFoundException">
        /// Thrown when no employee is found with the provided ID.
        /// </exception>
        public async Task<ApiResponse<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting UpdateEmployeeCommandHandler for Employee Id: {0}", request.Id);

            try
            {
                // Step 1: Fetch employee
                var employee = await _employeeRepository.GetByIdAsync(request.Id, cancellationToken);
                _logger.LogInformation("Fetched employee from repository. Employee exists: {0}", employee != null);

                if (employee == null)
                {
                    _logger.LogWarning("Employee with Id {0} not found", request.Id);
                    throw new NotFoundException($"Employee with Id {request.Id} not found.");
                }

                // Step 2: Update only provided fields
                if (request.Name != null) employee.Name = request.Name;
                if (request.PhoneNumber != null) employee.PhoneNumber = request.PhoneNumber;
                if (request.AadharNumber != null) employee.AadharNumber = request.AadharNumber;
                if (request.DepartmentId != null) employee.DepartmentId = request.DepartmentId.Value;
                if (request.UserId != null) employee.UserId = request.UserId.Value;
                if (request.RoleId != null) employee.RoleId = request.RoleId.Value;

                employee.UpdatedDate = DateTime.UtcNow;
                _logger.LogInformation("Updated employee fields for Employee Id: {0}", employee.Id);

                // Step 3: Save changes
                await _employeeRepository.UpdateAsync(employee, cancellationToken);
                _logger.LogInformation("Employee with Id {0} updated successfully in repository", employee.Id);

                // Step 4: Convert to DTO
                var dto = _mapper.Map<EmployeeDto>(employee);
                _logger.LogInformation("UpdateEmployeeCommandHandler completed successfully for Employee Id: {0}", employee.Id);

                return ApiResponse<EmployeeDto>.Success(dto, "Employee updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogErrors("Error occurred while updating Employee Id: {0}. Exception: {1}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
