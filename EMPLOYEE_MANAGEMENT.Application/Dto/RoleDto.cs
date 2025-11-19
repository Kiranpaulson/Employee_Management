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
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional description of the role.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Timestamp when the role was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Timestamp when the role was last updated.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
