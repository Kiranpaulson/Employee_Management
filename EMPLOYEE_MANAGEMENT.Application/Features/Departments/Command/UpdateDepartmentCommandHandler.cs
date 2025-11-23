using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.CustomException; // For NotFoundException
using EMPLOYEE_MANAGEMENT.Application.logging;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Departments.Handler
{
    /// <summary>
    /// Handles the <see cref="UpdateDepartmentCommand"/> by updating an existing department.
    /// Logs operations and throws <see cref="NotFoundException"/> if the department does not exist.
    /// </summary>
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, ApiResponse<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IAppLogger<UpdateDepartmentCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDepartmentCommandHandler"/> class.
        /// </summary>
        /// <param name="repo">The repository used for department data operations.</param>
        /// <param name="mapper">The AutoMapper instance used to map between entities and DTOs.</param>
        /// <param name="logger">Application logger for logging operations.</param>
        public UpdateDepartmentCommandHandler(IDepartmentRepository repo, IMapper mapper, IAppLogger<UpdateDepartmentCommandHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the department update request by retrieving the entity,
        /// updating its fields, saving to the repository, and returning the updated department as a DTO.
        /// </summary>
        /// <param name="request">The <see cref="UpdateDepartmentCommand"/> containing the updated department details.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing the updated <see cref="DepartmentDto"/> and a success message.
        /// </returns>
        /// <exception cref="NotFoundException">Thrown when the department with the specified ID does not exist.</exception>
        public async Task<ApiResponse<DepartmentDto>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting UpdateDepartmentCommandHandler for Department Id: {0}", request.Id);

            // Retrieve the department by ID
            var dept = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (dept == null)
            {
                _logger.LogWarning("Department with Id {0} not found", request.Id);
                throw new NotFoundException($"Department with Id {request.Id} not found.");
            }

            // Update department fields
            dept.Name = request.Name;
            dept.Description = request.Description;
            dept.UpdatedDate = DateTime.UtcNow;

            _logger.LogInformation("Updating Department Id: {0}", request.Id);

            // Save the updated department
            var updated = await _repo.UpdateAsync(dept, cancellationToken);

            // Map to DTO
            var dto = _mapper.Map<DepartmentDto>(updated);

            _logger.LogInformation("UpdateDepartmentCommandHandler completed successfully for Department Id: {0}", request.Id);

            // Return success response
            return ApiResponse<DepartmentDto>.Success(dto, "Department updated successfully");
        }
    }
}
