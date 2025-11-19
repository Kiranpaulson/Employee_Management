using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Departments.Handler
{
    /// <summary>
    /// Handles deletion of departments.
    /// </summary>
    public class DeleteDepartmentCommandHandler
        : IRequestHandler<DeleteDepartmentCommand, ApiResponse<string>>
    {
        private readonly IDepartmentRepository _repo;

        public DeleteDepartmentCommandHandler(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var dept = await _repo.GetById(request.Id);

            if (dept == null)
                return ApiResponse<string>.Fail("Department not found");

            await _repo.DeleteAsync(dept);

            return ApiResponse<string>.Success("Department deleted successfully");
        }
    }
}
