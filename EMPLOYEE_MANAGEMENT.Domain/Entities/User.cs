using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMPLOYEE_MANAGEMENT.Domain.Entities
{
    /// <summary>
    /// Represents an application user who can log in and access the system.
    /// Linked to an Employee record through a one-to-one relationship.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Primary key — unique identifier for the user.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Username used for authentication.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        /// <summary>
        /// User's email address.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Hashed password stored securely in the database.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Indicates whether the user account is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// One-to-one relationship: a user is associated with exactly one employee.
        /// </summary>
        public Employee Employee { get; set; }
    }
}
