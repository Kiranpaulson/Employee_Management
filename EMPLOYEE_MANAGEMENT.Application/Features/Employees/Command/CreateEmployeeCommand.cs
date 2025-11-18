using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command
{
    /// <summary>
    /// Represents a command to create a new employee in the system.
    /// Contains all required fields for employee creation.
    /// </summary>
    public class CreateEmployeeCommand : IRequest<ApiResponse<EmployeeDto>>
    {
        /// <summary>
        /// Gets or sets the full name of the employee to be created.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the employee.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Aadhar number of the employee.
        /// </summary>
        public string AadharNumber { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the role assigned to the employee.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user account associated with the employee.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the department to which the employee belongs.
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp representing when the employee record was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the timestamp representing when the employee record was last updated.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
