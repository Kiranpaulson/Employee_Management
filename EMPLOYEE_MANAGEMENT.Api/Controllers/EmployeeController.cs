



using EMPLOYEE_MANAGEMENT.Application.Command;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Query.Employee;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<EmployeeDto>>>> GetAllEmployees()
        {
            var query = new GetAllEmployeesQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        // GET: api/Employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> GetEmployeeById(int id)
        {
            var query = new GetEmployeeByIdQuery(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        // GET: api/Employee/department/{departmentId}
        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult<ApiResponse<List<EmployeeDto>>>> GetEmployeesByDepartmentId(int departmentId)
        {
            var query = new GetEmployeesByDepartmentIdQuery(departmentId);
            var response = await _mediator.Send(query);
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteEmployee(int id)
        {
            var command = new DeleteEmployeeCommand(id);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPatch]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> UpdateEmployee([FromBody] UpdateEmployeeCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}