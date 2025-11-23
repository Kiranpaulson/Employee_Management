using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Features.Users.Command;
using EMPLOYEE_MANAGEMENT.Application.logging;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using MediatR;
using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Abstractions.Services;

namespace EMPLOYEE_MANAGEMENT.Application.Handler.Users
{
    public class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, ApiResponse<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAppLogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            IAppLogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task<ApiResponse<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Registering user with email {request.Email}");

            var user = _mapper.Map<User>(request);

            // hash password
            user.PasswordHash = _passwordHasher.HashPassword(request.Password);

            var savedUser = await _userRepository.CreateAsync(user, cancellationToken);

            return ApiResponse<User>.Created(savedUser, "User registered successfully");
        }
    }
}
