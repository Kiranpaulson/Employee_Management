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
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the department.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Department description.
        /// </summary>
        public string Description { get; set; }
    }
}
