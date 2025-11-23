using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Abstractions.Services
{
    public interface IAuthService
    {
        public string GenerateToken(int userId, string email, string username);
    }
}
