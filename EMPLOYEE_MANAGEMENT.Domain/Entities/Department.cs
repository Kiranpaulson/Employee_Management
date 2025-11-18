using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMPLOYEE_MANAGEMENT.Domain.Entities
{
    /// <summary>
    /// Represents a department within the organization.
    /// Each department can have multiple employees.
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Primary key — auto-incremented department identifier.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Name of the department.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Optional description or details about the department.
        /// </summary>
        [MaxLength(250)]
        public string Description { get; set; }

        /// <summary>
        /// Navigation property — all employees that belong to this department.
        /// </summary>
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        /// <summary>
        /// Timestamp for when the department was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Timestamp for when the department record was last updated.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
