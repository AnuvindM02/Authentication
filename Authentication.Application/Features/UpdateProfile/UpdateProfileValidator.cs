using FluentValidation;

namespace Authentication.Application.Features.UpdateProfile
{
    public class UpdateProfileValidator:AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Matches("^[a-zA-Z]+$").WithMessage("First name must contain only letters.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.MiddleName)
                .MaximumLength(50).WithMessage("Middle name cannot exceed 50 characters.")
                .Matches("^[a-zA-Z]+$").WithMessage("Middle name must contain only letters.")
                .When(x => !string.IsNullOrEmpty(x.MiddleName));

            RuleFor(x => x.LastName)
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.")
                .Matches("^[a-zA-Z]+$").WithMessage("Last name must contain only letters.")
                .When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[!@#$%^&*(),.?{}|<>]").WithMessage("Password must contain at least one special character.");
        }
    }
}
