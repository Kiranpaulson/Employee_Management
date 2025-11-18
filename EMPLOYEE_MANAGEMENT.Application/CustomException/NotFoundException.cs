using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.CustomException
{
    /// <summary>
    /// Represents an exception that is thrown when a requested resource
    /// or entity cannot be found in the system.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class
        /// with a specified error message that describes the missing resource.
        /// </summary>
        /// <param name="message">A descriptive message explaining the cause of the exception.</param>
        public NotFoundException(string message) : base(message) { }
    }
}
