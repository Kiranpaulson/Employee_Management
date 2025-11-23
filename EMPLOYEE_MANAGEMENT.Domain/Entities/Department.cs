using System;
using System.Collections.Generic;

namespace EMPLOYEE_MANAGEMENT.Domain.Entities
{
    /// <summary>
    /// Represents a department within the organization.
    /// Each department can have multiple employees.
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Unique identifier for the department.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the department (e.g., HR, Finance, IT).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional description or details about the department.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Collection of employees who belong to this department.
        /// </summary>
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        /// <summary>
        /// Timestamp indicating when the department was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Timestamp indicating when the department record was last updated.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
