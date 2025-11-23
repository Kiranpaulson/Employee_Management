using EMPLOYEE_MANAGEMENT.Api.Common;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Api.Controllers
{
    /// <summary>
    /// Controller responsible for handling employee-related operations such as
    /// retrieving, creating, updating, and deleting employee records.
    /// </summary>
    public class EmployeeController : BaseApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator dependency for handling requests.</param>
        public EmployeeController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Retrieves all employees in the system.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<EmployeeDto>>>>
            GetAllEmployees(CancellationToken cancellationToken)
        {
            var query = new GetAllEmployeesQuery();
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves an employee by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        ///// <param name="cancellationToken">Token to cancel the request.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>>
            GetEmployeeById(int id, CancellationToken cancellationToken)
        {
            var query = new GetEmployeeByIdQuery(id);
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }






        /// <summary>
        /// Retrieves employees belonging to a specific department.
        /// </summary>
        /// <param name="departmentId">The department ID.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult<ApiResponse<List<EmployeeDto>>>>
            GetEmployeesByDepartmentId(int departmentId, CancellationToken cancellationToken)
        {
            var query = new GetEmployeesByDepartmentIdQuery(departmentId);
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="command">The employee creation command data.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>>
            CreateEmployee([FromBody] CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Deletes an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>>
            DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteEmployeeCommand(id);
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="command">The employee update data.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        [HttpPatch]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>>
            UpdateEmployee([FromBody] UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }
    }
}
