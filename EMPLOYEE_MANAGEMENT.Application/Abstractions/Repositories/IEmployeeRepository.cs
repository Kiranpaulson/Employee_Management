using EMPLOYEE_MANAGEMENT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories
{
    /// <summary>
    /// Defines the contract for employee-specific data access operations,
    /// extending the generic repository with additional queries for related data.
    /// </summary>
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        /// <summary>
        /// Retrieves all employees along with their related entities
        /// (e.g., department, roles, or other navigation properties).
        /// </summary>
        /// <returns>A list of employees including related data.</returns>
        Task<List<Employee>> GetEmployeesWithRelationsAsync();

        /// <summary>
        /// Retrieves a single employee by the specified ID,
        /// including all related navigation properties.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>The employee entity with related data, or null if not found.</returns>
        Task<Employee> GetEmployeeWithRelationsByIdAsync(int id);
    }
}
