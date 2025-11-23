using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.CustomException; // For NotFoundException
using EMPLOYEE_MANAGEMENT.Application.logging;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Departments.Handler
{
    /// <summary>
    /// Handles the <see cref="DeleteDepartmentCommand"/> by deleting an existing department.
    /// Logs operations and throws <see cref="NotFoundException"/> if the department does not exist.
    /// </summary>
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, ApiResponse<string>>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IAppLogger<DeleteDepartmentCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDepartmentCommandHandler"/> class.
        /// </summary>
        /// <param name="repo">Repository used for department data operations.</param>
        /// <param name="logger">Application logger for logging operations.</param>
        public DeleteDepartmentCommandHandler(IDepartmentRepository repo, IAppLogger<DeleteDepartmentCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        /// <summary>
        /// Handles the department deletion request by verifying existence, deleting it from the repository,
        /// and returning a success response.
        /// </summary>
        /// <param name="request">The <see cref="DeleteDepartmentCommand"/> containing the ID of the department to delete.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a success message if deletion succeeded.
        /// </returns>
        /// <exception cref="NotFoundException">Thrown when the department with the specified ID does not exist.</exception>
        public async Task<ApiResponse<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting DeleteDepartmentCommandHandler for Department Id: {0}", request.Id);

            // Retrieve the department by ID
            var dept = await _repo.GetByIdAsync(request.Id, cancellationToken);

            // Throw NotFoundException if department does not exist
            if (dept == null)
            {
                _logger.LogWarning("Department with Id {0} not found", request.Id);
                throw new NotFoundException($"Department with Id {request.Id} not found.");
            }

            // Delete the department
            await _repo.DeleteAsync(dept, cancellationToken);
            _logger.LogInformation("Department with Id {0} deleted successfully", request.Id);

            _logger.LogInformation("DeleteDepartmentCommandHandler completed successfully for Department Id: {0}", request.Id);
            return ApiResponse<string>.Success("Department deleted successfully");
        }
    }
}
