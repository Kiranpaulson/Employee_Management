using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMPLOYEE_MANAGEMENT.Domain.Entities
{
    /// <summary>
    /// Represents an employee within the organization.
    /// Connected to Department, User, and Role entities.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Primary key — auto-generated employee identifier.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Full name of the employee.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Foreign key — department the employee belongs to.
        /// </summary>
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Navigation property — associated department.
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// Foreign key — associated user account (login credentials).
        /// </summary>
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        /// <summary>
        /// Navigation property — user details.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Foreign key — employee role (Admin, Manager, Staff, etc.).
        /// </summary>
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        /// <summary>
        /// Navigation property — role details.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Employee phone number (10 digits).
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Employee Aadhar number (12 digits).
        /// </summary>
        public string AadharNumber { get; set; }

        /// <summary>
        /// Timestamp — when the employee record was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Timestamp — when the employee record was last updated.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
