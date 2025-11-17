using EMPLOYEE_MANAGEMENT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Domain.Persistance
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<List<Employee>> GetEmployeesWithRelationsAsync();
        Task<Employee> GetEmployeeWithRelationsByIdAsync(int id);
    }
}
