using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using EMPLOYEE_MANAGEMENT.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves all employees with related Department, User, and Role.
        /// Uses AsQueryable() to allow composition and supports cancellation.
        /// </summary>
        public Task<List<Employee>> GetEmployeesWithRelationsAsync(CancellationToken cancellationToken)
        {
            return _dbContext.Employees
                .AsQueryable()
                .Include(e => e.Department)
                .Include(e => e.User)
                .Include(e => e.Role)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves a single employee by its ID with User, Department, and Role.
        /// Supports cancellation.
        /// </summary>
        public Task<Employee> GetEmployeeWithRelationsByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _dbContext.Employees
                .AsQueryable()
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
    }
}
