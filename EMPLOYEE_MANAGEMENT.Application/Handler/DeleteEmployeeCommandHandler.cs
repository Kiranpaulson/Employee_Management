using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Command.Employee;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using EMPLOYEE_MANAGEMENT.Domain.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Handler
{
    public class DeleteEmployeeCommandHandler
        : IRequestHandler<DeleteEmployeeCommand, ApiResponse<string>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ApiResponse<string>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
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
    