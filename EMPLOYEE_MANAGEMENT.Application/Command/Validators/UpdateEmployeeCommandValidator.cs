using FluentValidation;
using EMPLOYEE_MANAGEMENT.Application.Command;

namespace EMPLOYEE_MANAGEMENT.Application.Command.Validators
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            // ID must always be valid
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Employee Id must be a valid positive number.");

            RuleFor(x => x.Name)
                .MaximumLength(100)
                .When(x => x.Name != null)
                .WithMessage("Name must not exceed 100 characters.");

          

            RuleFor(x => x.PhoneNumber)
                .Length(10)
                .When(x => x.PhoneNumber != null)
                .WithMessage("Phone number must be exactly 10 digits.")
                .Must(p => p!.All(char.IsDigit))
                .When(x => x.PhoneNumber != null)
                .WithMessage("Phone number must contain only digits.");

            RuleFor(x => x.AadharNumber)
                .Length(12)
                .When(x => x.AadharNumber != null)
                .WithMessage("Aadhar number must be 12 digits.")
                .Must(a => a!.All(char.IsDigit))
                .When(x => x.AadharNumber != null)
                .WithMessage("Aadhar must contain only digits.");

            RuleFor(x => x.Role)
                .MaximumLength(50)
                .When(x => x.Role != null)
                .WithMessage("Role must not exceed 50 characters.");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0)
                .When(x => x.DepartmentId != null)
                .WithMessage("DepartmentId must be valid.");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .When(x => x.UserId != null)
                .WithMessage("UserId must be valid.");

           
        }
    }
}
