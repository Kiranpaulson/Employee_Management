using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EMPLOYEE_MANAGEMENT.Api.Common;

namespace EMPLOYEE_MANAGEMENT.Api.Controllers
{
    /// <summary>
    /// Controller responsible for handling department-related operations such as
    /// retrieving, creating, updating, and deleting department records.
    /// </summary>
    public class DepartmentController : BaseApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator dependency for handling requests.</param>
        public DepartmentController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Retrieves all departments in the system.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>A list of department DTOs wrapped in an ApiResponse.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<DepartmentDto>>>>
            GetAllDepartments(CancellationToken cancellationToken)
        {
            var query = new GetAllDepartmentsQuery();
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="command">The department creation command data.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The created department wrapped in an ApiResponse.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<DepartmentDto>>>
            CreateDepartment([FromBody] CreateDepartmentCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to delete.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>A success message wrapped in an ApiResponse.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>>
            DeleteDepartment(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteDepartmentCommand(id);
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="command">The department update data.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The updated department wrapped in an ApiResponse.</returns>
        [HttpPatch]
        public async Task<ActionResult<ApiResponse<DepartmentDto>>>
            UpdateDepartment([FromBody] UpdateDepartmentCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }
    }
}
