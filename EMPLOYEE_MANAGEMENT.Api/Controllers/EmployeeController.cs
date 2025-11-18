using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Api.Controllers
{
    /// <summary>
    /// Controller responsible for handling employee-related operations such as
    /// retrieving, creating, updating, and deleting employee records.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
       
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator dependency for handling requests.</param>
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all employees in the system.
        /// </summary>
        /// <returns>A list of employee DTOs wrapped in an ApiResponse.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<EmployeeDto>>>> GetAllEmployees()
        {
            var query = new GetAllEmployeesQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves an employee by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        /// <returns>An employee DTO wrapped in an ApiResponse.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> GetEmployeeById(int id)
        {
            var query = new GetEmployeeByIdQuery(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves employees belonging to a specific department.
        /// </summary>
        /// <param name="departmentId">The department ID.</param>
        /// <returns>A list of employees in that department wrapped in an ApiResponse.</returns>
        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult<ApiResponse<List<EmployeeDto>>>> GetEmployeesByDepartmentId(int departmentId)
        {
            var query = new GetEmployeesByDepartmentIdQuery(departmentId);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="command">The employee creation command data.</param>
        /// <returns>The created employee wrapped in an ApiResponse.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Deletes an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>A success message wrapped in an ApiResponse.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteEmployee(int id)
        {
            var command = new DeleteEmployeeCommand(id);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="command">The employee update data.</param>
        /// <returns>The updated employee wrapped in an ApiResponse.</returns>
        [HttpPatch]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> UpdateEmployee([FromBody] UpdateEmployeeCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
