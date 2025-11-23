using EMPLOYEE_MANAGEMENT.Api.Common;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Api.Controllers
{
    /// <summary>
    /// Controller responsible for handling role-related operations such as
    /// retrieving, creating, updating, and deleting roles.
    /// </summary>
    public class RoleController : BaseApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator dependency for handling requests.</param>
        public RoleController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Retrieves all roles in the system.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>A list of role DTOs wrapped in an ApiResponse.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<RoleDto>>>>
            GetAllRoles(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllRolesQuery(), cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="command">The role creation command data.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The created role wrapped in an ApiResponse.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<RoleDto>>>
            CreateRole([FromBody] CreateRoleCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to delete.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>A success message wrapped in an ApiResponse.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>>
            DeleteRole(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteRoleCommand(id), cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <param name="command">The role update data.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The updated role wrapped in an ApiResponse.</returns>
        [HttpPatch]
        public async Task<ActionResult<ApiResponse<RoleDto>>>
            UpdateRole([FromBody] UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }
    }
}
