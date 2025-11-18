using System;
using System.Collections.Generic;

namespace EMPLOYEE_MANAGEMENT.Application.CustomException
{
    /// <summary>
    /// Represents an exception that is thrown when one or more validation
    /// errors occur during request processing.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Gets a collection of validation errors where the key represents
        /// the field or property name and the value contains the associated
        /// validation error messages.
        /// </summary>
        public IDictionary<string, string[]> Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class
        /// with the specified collection of validation errors.
        /// </summary>
        /// <param name="errors">
        /// A dictionary containing validation failures, where each key is the field name
        /// and the value is an array of corresponding error messages.
        /// </param>
        public ValidationException(IDictionary<string, string[]> errors)
            : base("One or more validation failures have occurred.")
        {
            Errors = errors;
        }
    }
}
