using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using EMPLOYEE_MANAGEMENT.Application.logging;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Departments.Handler
{
    /// <summary>
    /// Handles department creation logic.
    /// Logs key operations and errors.
    /// </summary>
    public class CreateDepartmentCommandHandler
        : IRequestHandler<CreateDepartmentCommand, ApiResponse<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateDepartmentCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="CreateDepartmentCommandHandler"/>.
        /// </summary>
        /// <param name="repo">Repository used for department data operations.</param>
        /// <param name="mapper">AutoMapper instance for mapping between entity and DTO.</param>
        /// <param name="logger">Application logger for logging operations.</param>
        public CreateDepartmentCommandHandler(
            IDepartmentRepository repo,
            IMapper mapper,
            IAppLogger<CreateDepartmentCommandHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new department, saves it to the repository, maps it to DTO, and returns a response.
        /// </summary>
        /// <param name="request">The <see cref="CreateDepartmentCommand"/> containing department details.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the created <see cref="DepartmentDto"/> and success message.
        /// </returns>
        public async Task<ApiResponse<DepartmentDto>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting CreateDepartmentCommandHandler for Department Name: {0}", request.Name);

            try
            {
                var dept = _mapper.Map<Department>(request);
                _logger.LogInformation("Mapped CreateDepartmentCommand to Department entity for Department Name: {0}", request.Name);

                dept.CreatedDate = DateTime.UtcNow;
                dept.UpdatedDate = DateTime.UtcNow;

                var saved = await _repo.CreateAsync(dept, cancellationToken);
                _logger.LogInformation("Department saved to repository with Id: {0}", saved.Id);

                var dto = _mapper.Map<DepartmentDto>(saved);

                _logger.LogInformation("CreateDepartmentCommandHandler completed successfully for Department Id: {0}", saved.Id);
                return ApiResponse<DepartmentDto>.Created(dto, "Department created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogErrors("Error occurred while creating department: {0}. Exception: {1}", request.Name, ex.Message);
                throw;
            }
        }
    }
}
