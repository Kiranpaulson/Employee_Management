using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Query.Employee;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Handler
{
    public class GetEmployeeByIdQueryHandler
        : IRequestHandler<GetEmployeeByIdQuery, ApiResponse<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeWithRelationsByIdAsync(request.Id);

            if (employee == null)
            {
                // THROW — let middleware handle it
                throw new NotFoundException($"Employee with Id {request.Id} not found");
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return ApiResponse<EmployeeDto>.Success(employeeDto);
        }
    }
}
