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
    /// Handles department creation logic.
    /// </summary>
    public class CreateDepartmentCommandHandler
        : IRequestHandler<CreateDepartmentCommand, ApiResponse<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(IDepartmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new department and saves it to the database.
        /// </summary>
        public async Task<ApiResponse<DepartmentDto>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var dept = _mapper.Map<Department>(request);

            dept.CreatedDate = DateTime.UtcNow;
            dept.UpdatedDate = DateTime.UtcNow;

            var saved = await _repo.CreateAsync(dept);

            var dto = _mapper.Map<DepartmentDto>(saved);

            return ApiResponse<DepartmentDto>.Created(dto, "Department created successfully");
        }
    }
}
