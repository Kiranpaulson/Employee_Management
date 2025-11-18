using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command
{
    /// <summary>
    /// Handles the update logic for an existing employee.
    /// Fetches the employee, updates only provided fields (non-null),
    /// sets UpdatedDate, saves changes, and returns an EmployeeDto.
    /// </summary>
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ApiResponse<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for injecting required dependencies.
        /// </summary>
        /// <param name="employeeRepository">Repository to access employee data.</param>
        /// <param name="mapper">AutoMapper instance for mapping entities to DTOs.</param>
        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the update request for an employee.
        /// </summary>
        /// <param name="request">The update command containing new field values.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>ApiResponse containing updated EmployeeDto.</returns>
        /// <exception cref="NotFoundException">Thrown when employee does not exist.</exception>
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
