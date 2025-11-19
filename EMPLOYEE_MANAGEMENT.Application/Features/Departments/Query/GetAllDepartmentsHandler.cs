using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Departments.Handler
{
    /// <summary>
    /// Handles retrieval of all departments.
    /// </summary>
    public class GetAllDepartmentsQueryHandler
        : IRequestHandler<GetAllDepartmentsQuery, ApiResponse<List<DepartmentDto>>>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper _mapper;

        public GetAllDepartmentsQueryHandler(IDepartmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<DepartmentDto>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var list = await _repo.GetAllAsync();
            var dto = _mapper.Map<List<DepartmentDto>>(list);

            return ApiResponse<List<DepartmentDto>>.Success(dto);
        }
    }
}
