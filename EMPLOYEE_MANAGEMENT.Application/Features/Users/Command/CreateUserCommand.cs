using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using MediatR;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Users.Command
{
    public class CreateUserCommand : IRequest<ApiResponse<User>>
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsActive { get; set; } = true;

    }
}
