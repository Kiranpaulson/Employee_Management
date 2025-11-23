namespace EMPLOYEE_MANAGEMENT.Application.Dto
{
    /// <summary>
    /// Represents a data transfer object (DTO) containing essential information
    /// about an employee, including related department and user details.
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee.
        /// Example: 101
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the employee.
        /// Example: "Kiran Paulson"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the role or designation of the employee.
        /// Example: "Senior Developer"
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the name of the department to which the employee belongs.
        /// Example: "IT Department"
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Gets or sets the username associated with the employee's user account.
        /// Example: "kiran.p"
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the contact phone number of the employee.
        /// Example: "+91 9876543210"
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Aadhar number of the employee.
        /// Example: "1234 5678 9012"
        /// </summary>
        public string AadharNumber { get; set; }
    }
}
