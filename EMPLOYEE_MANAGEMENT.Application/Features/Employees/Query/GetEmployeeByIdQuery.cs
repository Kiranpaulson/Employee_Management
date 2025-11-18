using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query
{
    /// <summary>
    /// Query used to retrieve a single employee by their unique identifier.
    /// Returns an EmployeeDto wrapped inside an ApiResponse.
    /// </summary>
    public class GetEmployeeByIdQuery : IRequest<ApiResponse<EmployeeDto>>
    {
        /// <summary>
        /// The unique identifier of the employee to be retrieved.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Creates a new instance of the query with the specified employee ID.
        /// </summary>
        /// <param name="id">The employee's unique identifier.</param>
        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
