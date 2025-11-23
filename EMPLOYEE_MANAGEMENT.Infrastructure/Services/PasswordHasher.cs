using EMPLOYEE_MANAGEMENT.Application.Abstractions.Services;
using System;
using System.Security.Cryptography;
using System.Text;

namespace EMPLOYEE_MANAGEMENT.Application.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            using var hmac = new HMACSHA512();
            var salt = hmac.Key; // 128-bit cryptographic salt
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Add null/empty checks
            if (string.IsNullOrWhiteSpace(password))
            {
                return false; // Don't throw, just return false for security
            }

            if (string.IsNullOrWhiteSpace(hashedPassword))
            {
                return false;
            }

            try
            {
                var parts = hashedPassword.Split('.');

                if (parts.Length != 2)
                    return false;

                // Check if parts are not empty
                if (string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
                    return false;

                var salt = Convert.FromBase64String(parts[0]);
                var storedHash = Convert.FromBase64String(parts[1]);

                using var hmac = new HMACSHA512(salt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return CryptographicOperations.FixedTimeEquals(storedHash, computedHash);
            }
            catch (FormatException)
            {
                // Invalid Base64 format
                return false;
            }
            catch (Exception)
            {
                // Any other exception during verification
                return false;
            }
        }
    }
}