using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command;
using FluentValidation;

namespace EMPLOYEE_MANAGEMENT.Application.Validators
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Role Id must be valid.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(100).WithMessage("Role name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Description must not exceed 250 characters.");
        }
    }
}
