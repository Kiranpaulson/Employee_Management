using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Api.Controllers
{
    /// <summary>
    /// Controller responsible for handling department-related operations such as
    /// retrieving, creating, updating, and deleting department records.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator dependency for handling requests.</param>
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all departments in the system.
        /// </summary>
        /// <returns>A list of department DTOs wrapped in an ApiResponse.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<DepartmentDto>>>> GetAllDepartments()
        {
            var query = new GetAllDepartmentsQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }


        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="command">The department creation command data.</param>
        /// <returns>The created department wrapped in an ApiResponse.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<DepartmentDto>>> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to delete.</param>
        /// <returns>A success message wrapped in an ApiResponse.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteDepartment(int id)
        {
            var command = new DeleteDepartmentCommand(id);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="command">The department update data.</param>
        /// <returns>The updated department wrapped in an ApiResponse.</returns>
        [HttpPatch]
        public async Task<ActionResult<ApiResponse<DepartmentDto>>> UpdateDepartment([FromBody] UpdateDepartmentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
