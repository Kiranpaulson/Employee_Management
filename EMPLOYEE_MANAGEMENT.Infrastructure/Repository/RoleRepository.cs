using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using EMPLOYEE_MANAGEMENT.Infrastructure.Persistance;

namespace EMPLOYEE_MANAGEMENT.Infrastructure.Repository
{
    /// <summary>
    /// Repository implementation for Role entity.
    /// Uses generic repository for basic CRUD operations.
    /// </summary>
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
