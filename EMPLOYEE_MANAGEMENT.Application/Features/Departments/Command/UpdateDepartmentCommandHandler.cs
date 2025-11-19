using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Departments.Handler
{
    /// <summary>
    /// Handles department update requests.
    /// </summary>
    public class UpdateDepartmentCommandHandler
        : IRequestHandler<UpdateDepartmentCommand, ApiResponse<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(IDepartmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<DepartmentDto>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var dept = await _repo.GetById(request.Id);

            if (dept == null)
                return ApiResponse<DepartmentDto>.Fail("Department not found");

            dept.Name = request.Name;
            dept.Description = request.Description;
            dept.UpdatedDate = DateTime.UtcNow;

            var updated = await _repo.UpdateAsync(dept);

            var dto = _mapper.Map<DepartmentDto>(updated);

            return ApiResponse<DepartmentDto>.Success(dto);
        }
    }
}
