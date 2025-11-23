using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Users.Query
{
    public class LoginUserQuery : IRequest<ApiResponse<LoginResponseDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
