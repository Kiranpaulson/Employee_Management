using System;

namespace EMPLOYEE_MANAGEMENT.Application.Dto
{
    /// <summary>
    /// Data transfer object representing a Role entity.
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Role unique identifier.
        /// Example: 5
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the role.
        /// Example: "Administrator"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional description of the role.
        /// Example: "Has full access to all system features."
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Timestamp when the role was created.
        /// Example: "2025-01-10T14:30:00Z"
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Timestamp when the role was last updated.
        /// Example: "2025-02-01T10:15:00Z"
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
