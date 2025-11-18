using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Common.Behaviour
{
    /// <summary>
    /// A MediatR pipeline behavior that automatically executes validation
    /// rules for incoming requests before passing them to the next handler.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request being handled.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the request handler.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">
        /// A collection of validators associated with the request type.
        /// </param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Executes all validators for the given request before continuing to the next handler.
        /// Throws a <see cref="ValidationException"/> if any validation errors are found.
        /// </summary>
        /// <param name="request">The incoming MediatR request.</param>
        /// <param name="next">The next delegate in the pipeline.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The response returned by the next request handler in the pipeline.</returns>
        /// <exception cref="ValidationException">
        /// Thrown when one or more validation rules fail.
        /// </exception>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken))
                );

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count > 0)
                {
                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
