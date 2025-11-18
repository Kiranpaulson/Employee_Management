using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Command.Employee;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Handler
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ApiResponse<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetById(request.Id);

            if (employee == null)
                throw new NotFoundException($"Employee with Id {request.Id} not found");

            if (request.Name != null) employee.Name = request.Name;
            if (request.PhoneNumber != null) employee.PhoneNumber = request.PhoneNumber;
            if (request.AadharNumber != null) employee.AadharNumber = request.AadharNumber;
            if (request.DepartmentId != null) employee.DepartmentId = request.DepartmentId.Value;
            if (request.UserId != null) employee.UserId = request.UserId.Value;
            if (request.RoleId != null) employee.RoleId = request.RoleId.Value;

            employee.UpdatedDate = DateTime.UtcNow;

            await _employeeRepository.UpdateAsync(employee);

            var dto = _mapper.Map<EmployeeDto>(employee);

            return ApiResponse<EmployeeDto>.Success(dto, "Employee updated successfully");
        }

    }
}
