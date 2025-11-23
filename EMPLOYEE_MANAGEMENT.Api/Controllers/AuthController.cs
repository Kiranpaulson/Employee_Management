using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using EMPLOYEE_MANAGEMENT.Api.Common;
using EMPLOYEE_MANAGEMENT.Application.Features.Users.Query;
using EMPLOYEE_MANAGEMENT.Application.Features.Users.Command;
using EMPLOYEE_MANAGEMENT.Domain.Entities;

namespace EMPLOYEE_MANAGEMENT.Api.Controllers
{
    /// <summary>
    /// Controller responsible for user authentication operations such as
    /// registration and login.
    /// </summary>
    public class AuthController : BaseApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator dependency for handling auth requests.</param>
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="command">The registration data containing user details.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The created user details wrapped in an ApiResponse.</returns>
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<User>>>
            Register([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token if successful.
        /// </summary>
        /// <param name="command">The login data containing email and password.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>A login response containing JWT token wrapped in an ApiResponse.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>>
            Login([FromBody] LoginUserQuery query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(query, cancellationToken);
           
            return Ok(response);
        }
    }
}
