using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using EMPLOYEE_MANAGEMENT.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Infrastructure.Repository
{
    /// <summary>
    /// Repository class responsible for handling Employee-related
    /// database operations with Entity Framework Core.
    /// Extends the <see cref="GenericRepository{Employee}"/> to provide
    /// additional methods for retrieving related entities.
    /// </summary>
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of <see cref="EmployeeRepository"/>.
        /// The DbContext is passed to both this repository and the base generic repository
        /// through dependency injection.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves all employees from the database, including their related
        /// Department, User, and Role entities.
        /// </summary>
        /// <returns>A list of employees with related entity data.</returns>
        public Task<List<Employee>> GetEmployeesWithRelationsAsync()
        {
            return _dbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.User)
                .Include(e => e.Role)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a single employee by its ID, including the associated
        /// User, Department, and Role information.
        /// </summary>
        /// <param name="id">The employee ID to search for.</param>
        /// <returns>
        /// The matching <see cref="Employee"/> with related data,
        /// or null if not found.
        /// </returns>
        public async Task<Employee> GetEmployeeWithRelationsByIdAsync(int id)
        {
            return await _dbContext.Employees
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
