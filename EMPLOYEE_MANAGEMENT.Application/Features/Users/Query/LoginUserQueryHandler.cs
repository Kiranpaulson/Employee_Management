using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Features.Users.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using MediatR;
using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Abstractions.Services;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using System;

namespace EMPLOYEE_MANAGEMENT.Application.Handler.Users
{
    public class LoginUserQueryHandler
        : IRequestHandler<LoginUserQuery, ApiResponse<LoginResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthService _authService;

        public LoginUserQueryHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IAuthService authService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _authService = authService;
        }

        public async Task<ApiResponse<LoginResponseDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("=== LOGIN ATTEMPT START ===");
                Console.WriteLine($"Request is null: {request == null}");

                if (request != null)
                {
                    Console.WriteLine($"Email: '{request.Email ?? "NULL"}'");
                    Console.WriteLine($"Password is null: {request.Password == null}");
                    Console.WriteLine($"Password is empty: {string.IsNullOrWhiteSpace(request.Password)}");
                    Console.WriteLine($"Password length: {request.Password?.Length ?? 0}");
                }

                // 1. Validate input
                if (string.IsNullOrWhiteSpace(request?.Email))
                {
                    Console.WriteLine("Email validation failed");
                    return ApiResponse<LoginResponseDto>.Fail("Email is required");
                }

                if (string.IsNullOrWhiteSpace(request?.Password))
                {
                    Console.WriteLine("Password validation failed");
                    return ApiResponse<LoginResponseDto>.Fail("Password is required");
                }

                // 2. Find user by email
                Console.WriteLine($"Searching for user with email: {request.Email}");
                var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

                Console.WriteLine($"User found: {user != null}");

                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return ApiResponse<LoginResponseDto>.Fail("Invalid email or password");
                }

                Console.WriteLine($"User ID: {user.Id}");
                Console.WriteLine($"User Email: {user.Email ?? "NULL"}");
                Console.WriteLine($"User Username: {user.Username ?? "NULL"}");
                Console.WriteLine($"User PasswordHash is null: {user.PasswordHash == null}");
                Console.WriteLine($"User PasswordHash length: {user.PasswordHash?.Length ?? 0}");

                // 3. Check if PasswordHash exists
                if (string.IsNullOrWhiteSpace(user.PasswordHash))
                {
                    Console.WriteLine("PasswordHash is null or empty");
                    return ApiResponse<LoginResponseDto>.Fail("Invalid email or password");
                }

                // 4. Verify password
                Console.WriteLine("About to verify password");
                var isValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
                Console.WriteLine($"Password verification result: {isValid}");

                if (!isValid)
                {
                    Console.WriteLine("Password verification failed");
                    return ApiResponse<LoginResponseDto>.Fail("Invalid email or password");
                }

                // 5. Generate JWT token
                Console.WriteLine("About to generate token");
                Console.WriteLine($"Token params - UserId: {user.Id}, Email: '{user.Email}', Username: '{user.Username}'");

                string token = _authService.GenerateToken(user.Id, user.Email, user.Username);

                Console.WriteLine($"Token generated: {!string.IsNullOrEmpty(token)}");
                Console.WriteLine($"Token length: {token?.Length ?? 0}");

                // 6. Return success response
                var dto = new LoginResponseDto
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    AccessToken = token
                };

                Console.WriteLine("=== LOGIN ATTEMPT SUCCESS ===");
                return ApiResponse<LoginResponseDto>.Success(dto, "Login successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine("=== LOGIN ATTEMPT EXCEPTION ===");
                Console.WriteLine($"Exception Type: {ex.GetType().Name}");
                Console.WriteLine($"Exception Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner Stack Trace: {ex.InnerException.StackTrace}");
                }

                throw;
            }
        }
    }
}