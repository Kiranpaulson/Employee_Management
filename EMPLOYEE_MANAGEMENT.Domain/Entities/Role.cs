using System;

namespace EMPLOYEE_MANAGEMENT.Domain.Entities
{
    /// <summary>
    /// Represents a role in the system,
    /// defining a set of permissions or responsibilities (e.g., Admin, Manager).
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Unique identifier for the role.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional description providing more details about the role.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Timestamp indicating when the role record was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Timestamp indicating when the role record was last updated.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
