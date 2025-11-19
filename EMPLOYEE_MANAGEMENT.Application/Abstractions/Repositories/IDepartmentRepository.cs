using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories
{
    /// <summary>
    /// Repository interface for accessing and managing Department data.
    /// Extends the generic repository with CRUD operations.
    /// </summary>
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
    }
}
