namespace EMPLOYEE_MANAGEMENT.Application.Dto
{
    /// <summary>
    /// Data Transfer Object for Department entity.
    /// Used to return department information to API responses.
    /// </summary>
    public class DepartmentDto
    {
        /// <summary>
        /// Unique identifier of the department.
        /// Example: 3
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the department.
        /// Example: "Human Resources"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Department description.
        /// Example: "Handles recruitment and employee welfare."
        /// </summary>
        public string Description { get; set; }
    }
}
