using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.logging
{
    public interface IAppLogger<T>
    {

        public void LogInformation(String messge, params object[] args);
        public void LogWarning(String message, params object[] args);

        public void LogErrors(String message, params object[] args);

    }
}
