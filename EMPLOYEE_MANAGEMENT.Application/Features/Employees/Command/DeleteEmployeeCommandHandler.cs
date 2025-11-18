using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command
{
    /// <summary>
    /// Handles the <see cref="DeleteEmployeeCommand"/> by locating the employee,
    /// deleting it from the system, and returning a status message.
    /// </summary>
    public class DeleteEmployeeCommandHandler
        : IRequestHandler<DeleteEmployeeCommand, ApiResponse<string>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEmployeeCommandHandler"/> class.
        /// </summary>
        /// <param name="employeeRepository">The repository used to perform employee data operations.</param>
        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Processes the delete command by validating the employee's existence
        /// and deleting it if found.
        /// </summary>
        /// <param name="request">The command containing the employee ID to delete.</param>
        /// <param name="cancellationToken">A cancellation token for task cancellation.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a success or failure message
        /// depending on the result of the delete operation.
        /// </returns>
        public async Task<ApiResponse<string>> Handle(
            DeleteEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Find employee
            var employee = await _employeeRepository.GetById(request.Id);

            if (employee == null)
            {
                return ApiResponse<string>.Fail("Employee not found");
            }

            // 2. Delete employee
            await _employeeRepository.DeleteAsync(employee);

            return ApiResponse<string>.Success("Employee deleted successfully");
        }
    }
}
