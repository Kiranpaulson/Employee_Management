using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
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
    /// Handles the <see cref="DeleteEmployeeCommand"/> by validating the employee's existence,
    /// deleting it from the repository, and returning a success message.
    /// Includes logging at start, during operation, and end.
    /// </summary>
    public class DeleteEmployeeCommandHandler
        : IRequestHandler<DeleteEmployeeCommand, ApiResponse<string>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAppLogger<DeleteEmployeeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEmployeeCommandHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">The repository used for employee data operations.</param>
        /// <param name="logger">The application logger for logging information, warnings, and errors.</param>
        public DeleteEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IAppLogger<DeleteEmployeeCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        /// <summary>
        /// Handles the deletion of an employee by ID.
        /// Logs the process at key stages and throws <see cref="NotFoundException"/> if the employee does not exist.
        /// </summary>
        /// <param name="request">The <see cref="DeleteEmployeeCommand"/> containing the employee ID.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> for task cancellation.</param>
        /// <returns>An <see cref="ApiResponse{string}"/> indicating the result of the deletion.</returns>
        /// <exception cref="NotFoundException">Thrown when the employee with the specified ID does not exist.</exception>
        public async Task<ApiResponse<string>> Handle(
            DeleteEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting DeleteEmployeeCommandHandler for Employee Id: {0}", request.Id);

            try
            {
                // 1. Locate employee
                var employee = await _employeeRepository.GetByIdAsync(request.Id, cancellationToken);
                _logger.LogInformation("Fetched employee from repository. Employee exists: {0}", employee != null);

                if (employee == null)
                {
                    _logger.LogWarning("Employee with Id {0} not found", request.Id);
                    throw new NotFoundException($"Employee with Id {request.Id} not found.");
                }

                // 2. Delete employee
                await _employeeRepository.DeleteAsync(employee, cancellationToken);
                _logger.LogInformation("Employee with Id {0} deleted successfully", request.Id);

                _logger.LogInformation("DeleteEmployeeCommandHandler completed successfully for Employee Id: {0}", request.Id);
                return ApiResponse<string>.Success("Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogErrors("Error occurred while deleting Employee Id: {0}. Exception: {1}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
