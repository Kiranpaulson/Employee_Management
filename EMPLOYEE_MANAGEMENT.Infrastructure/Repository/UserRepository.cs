using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using EMPLOYEE_MANAGEMENT.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EMPLOYEE_MANAGEMENT.Infrastructure.Repository
{
    /// <summary>
    /// Concrete repository for managing User data.
    /// Inherits all basic CRUD operations from GenericRepository.
    /// </summary>
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;

        }
        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}
