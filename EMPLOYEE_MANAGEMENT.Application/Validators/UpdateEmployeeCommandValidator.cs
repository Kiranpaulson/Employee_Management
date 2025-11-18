using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command;
using FluentValidation;

namespace EMPLOYEE_MANAGEMENT.Application.Validators
{
    /// <summary>
    /// Validator for <see cref="UpdateEmployeeCommand"/>.
    /// Ensures all fields supplied during update follow correct formatting rules.
    /// Only fields that are not null will be validated.
    /// </summary>
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            /// <summary>
            /// Employee Id must be a valid positive integer.
            /// </summary>
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Employee Id must be a valid positive number.");

            /// <summary>
            /// Name must not exceed 100 characters (validated only if provided).
            /// </summary>
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .When(x => x.Name != null)
                .WithMessage("Name must not exceed 100 characters.");

            /// <summary>
            /// Phone number must be exactly 10 digits and contain only numbers.
            /// Validation happens only when the phone number is provided.
            /// </summary>
            RuleFor(x => x.PhoneNumber)
                .Length(10)
                .When(x => x.PhoneNumber != null)
                .WithMessage("Phone number must be exactly 10 digits.")
                .Must(p => p!.All(char.IsDigit))
                .When(x => x.PhoneNumber != null)
                .WithMessage("Phone number must contain only digits.");

            /// <summary>
            /// Aadhar number must be 12 digits and numeric.
            /// Validated only when provided.
            /// </summary>
            RuleFor(x => x.AadharNumber)
                .Length(12)
                .When(x => x.AadharNumber != null)
                .WithMessage("Aadhar number must be 12 digits.")
                .Must(a => a!.All(char.IsDigit))
                .When(x => x.AadharNumber != null)
                .WithMessage("Aadhar must contain only digits.");

            /// <summary>
            /// DepartmentId must be valid if provided.
            /// </summary>
            RuleFor(x => x.DepartmentId)
                .GreaterThan(0)
                .When(x => x.DepartmentId != null)
                .WithMessage("DepartmentId must be valid.");

            /// <summary>
            /// UserId must be valid if provided.
            /// </summary>
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .When(x => x.UserId != null)
                .WithMessage("UserId must be valid.");
        }
    }
}
