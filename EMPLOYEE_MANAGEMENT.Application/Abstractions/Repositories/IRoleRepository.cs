using EMPLOYEE_MANAGEMENT.Domain.Entities;

namespace EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories
{
    /// <summary>
    /// Repository interface for Role entity.
    /// Inherits generic repository methods: Create, Update, Delete, GetById, GetAll.
    /// </summary>
    public interface IRoleRepository : IGenericRepository<Role>
    {
    }
}
