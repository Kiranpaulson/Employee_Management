using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command
{
    /// <summary>
    /// Represents a command to delete an employee based on the specified identifier.
    /// </summary>
    public class DeleteEmployeeCommand : IRequest<ApiResponse<string>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee to be deleted.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEmployeeCommand"/> class
        /// with the specified employee identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        public DeleteEmployeeCommand( int id)
        {
            Id = id;
        }
    }
}
