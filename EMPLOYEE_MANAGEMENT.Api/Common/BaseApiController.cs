using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMPLOYEE_MANAGEMENT.Api.Common
{
    /// <summary>
    /// Base controller providing common functionality for all API controllers.
    /// This class centralizes shared dependencies (like MediatR) so that 
    /// derived controllers do not need to repeat constructor injection code.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Provides access to MediatR for sending commands and queries.
        /// </summary>
        protected readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance injected via DI.</param>
        protected BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
