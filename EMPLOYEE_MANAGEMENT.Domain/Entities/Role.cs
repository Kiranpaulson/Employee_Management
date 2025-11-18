using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMPLOYEE_MANAGEMENT.Domain.Entities
{
    /// <summary>
    /// Represents a role within the system (Admin, Manager, Staff, etc.).
    /// Defines permissions or job responsibilities attached to an employee.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Primary key — auto-generated role identifier.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Name of the role (e.g., Admin, HR, Manager).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Optional description providing more details about the role.
        /// </summary>
        [MaxLength(250)]
        public string Description { get; set; }

        /// <summary>
        /// Timestamp — when the role record was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Timestamp — when the role record was last updated.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
