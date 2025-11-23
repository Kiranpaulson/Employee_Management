namespace EMPLOYEE_MANAGEMENT.Domain.Entities
{
    /// <summary>
    /// Represents an application user who can log in to the system.
    /// Linked to exactly one employee through a one-to-one relationship.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier for the user account.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Username used for authentication.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hashed password stored securely.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Indicates whether the user account is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Navigation property representing the associated employee record.
        /// </summary>
        public Employee Employee { get; set; }
    }
}
