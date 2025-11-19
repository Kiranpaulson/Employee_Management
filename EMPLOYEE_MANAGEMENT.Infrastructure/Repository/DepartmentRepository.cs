using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using EMPLOYEE_MANAGEMENT.Infrastructure.Persistance;

namespace EMPLOYEE_MANAGEMENT.Infrastructure.Repository
{
    /// <summary>
    /// Concrete repository for managing Department entities using EF Core.
    /// </summary>
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext _db;

        /// <summary>
        /// Initializes the repository with a database context.
        /// </summary>
        public DepartmentRepository(AppDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }
    }
}
