using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Domain.Entities;

namespace EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories
{
    /// <summary>
    /// Repository interface for accessing and managing User data.
    /// Extends the generic repository with CRUD operations.
    /// </summary>
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);

    }

}
