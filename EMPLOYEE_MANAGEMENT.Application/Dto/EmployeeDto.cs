namespace EMPLOYEE_MANAGEMENT.Application.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        // Include related data for convenience
        public string DepartmentName { get; set; }
        public string Username { get; set; }

        // Newly added fields
        public string PhoneNumber { get; set; }
        public string AadharNumber { get; set; }
    }
}
