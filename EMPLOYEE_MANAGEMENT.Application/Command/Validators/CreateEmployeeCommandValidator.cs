using FluentValidation;
using EMPLOYEE_MANAGEMENT.Application.Command;

namespace EMPLOYEE_MANAGEMENT.Application.Command.Validators
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            // NAME
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Employee name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            // EMAIL
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            // PHONE NUMBER
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Must(p => p.All(char.IsDigit))
                    .WithMessage("Phone number must contain only digits.")
                .Length(10).WithMessage("Phone number must be exactly 10 digits.")
                .Must(p => p.StartsWith("6") || p.StartsWith("7") || p.StartsWith("8") || p.StartsWith("9"))
                    .WithMessage("Phone number must start with 6, 7, 8, or 9.");

            // AADHAR NUMBER
            RuleFor(x => x.AadharNumber)
                .NotEmpty().WithMessage("Aadhar number is required.")
                .Length(12).WithMessage("Aadhar must be exactly 12 digits.")
                .Must(a => a.All(char.IsDigit)).WithMessage("Aadhar must contain only digits.");

            // ROLE
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .MaximumLength(50).WithMessage("Role must not exceed 50 characters.");

            // USER ID
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be a valid positive number.");

            // DEPARTMENT ID
            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("DepartmentId must be a valid positive number.");

            // SALARY
          

            // IS ACTIVE → No need for validation; bool is always valid.
        }
    }
}
