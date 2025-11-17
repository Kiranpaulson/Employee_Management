using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Command;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using EMPLOYEE_MANAGEMENT.Domain.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Handler
{
    public class CreateEmployeeCommandHandler
        : IRequestHandler<CreateEmployeeCommand, ApiResponse<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<EmployeeDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // convert request → entity
            var employee = _mapper.Map<Employee>(request);

            // save to DB
            var savedEmployee = await _employeeRepository.CreateAsync(employee);

            // convert entity → dto
            var dto = _mapper.Map<EmployeeDto>(savedEmployee);

            return ApiResponse<EmployeeDto>.Created(dto, "Employee created successfully");
        }
    }
}
