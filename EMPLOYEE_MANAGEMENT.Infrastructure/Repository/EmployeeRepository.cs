using EMPLOYEE_MANAGEMENT.Domain.Entities;
using EMPLOYEE_MANAGEMENT.Domain.Persistance;
using EMPLOYEE_MANAGEMENT.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Infrastructure.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        //when we create a class of customer then also the generic repostiory dbcontext objec talso initiated which is called dependency injection
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext) {

            _dbContext = dbContext;
        }
        public Task<List<Employee>> GetEmployeesWithRelationsAsync()
        {
            return _dbContext.Employees
           .Include(e => e.Department)
           .Include(e => e.User)
           .Include(e => e.Role) // ⭐ ADD THIS
           .ToListAsync();

        }

        public async Task<Employee> GetEmployeeWithRelationsByIdAsync(int id)
        {
            return await _dbContext.Employees
       .Include(e => e.User)
       .Include(e => e.Department)
       .Include(e => e.Role) // ⭐ ADD THIS
       .FirstOrDefaultAsync(e => e.Id == id);
        }



    }

}
