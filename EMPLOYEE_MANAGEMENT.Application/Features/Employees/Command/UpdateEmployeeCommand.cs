using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command
{
    /// <summary>
    /// Represents a command to update an existing employee's details.
    /// Only the fields provided will be updated.
    /// </summary>
    public class UpdateEmployeeCommand : IRequest<ApiResponse<EmployeeDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee to be updated.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the updated name of the employee.
        /// Optional field.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the updated phone number of the employee.
        /// Optional field.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the updated Aadhar number of the employee.
        /// Optional field.
        /// </summary>
        public string? AadharNumber { get; set; }

        /// <summary>
        /// Gets or sets the updated role identifier assigned to the employee.
        /// Optional field.
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// Gets or sets the updated user account identifier associated with the employee.
        /// Optional field.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the updated department identifier for the employee.
        /// Optional field.
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp representing when the employee record was last updated.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
