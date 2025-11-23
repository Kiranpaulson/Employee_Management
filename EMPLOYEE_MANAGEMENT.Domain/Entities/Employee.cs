using System;

namespace EMPLOYEE_MANAGEMENT.Domain.Entities
{
    /// <summary>
    /// Represents an employee in the organization.
    /// Each employee is linked to a department, role, and user account.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Unique identifier for the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Full name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Foreign key representing the department the employee belongs to.
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Navigation property for the employee's department.
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// Foreign key representing the associated user account.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Navigation property for the associated user account.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Foreign key representing the employee’s role.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Navigation property for the employee’s role details.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Phone number of the employee (up to 10 digits).
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Aadhar number of the employee (12-digit identifier).
        /// </summary>
        public string AadharNumber { get; set; }

        /// <summary>
        /// Timestamp indicating when the employee record was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Timestamp indicating when the employee record was last updated.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
