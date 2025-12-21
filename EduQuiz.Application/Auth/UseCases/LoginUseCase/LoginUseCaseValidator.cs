using FluentValidation;

namespace EduQuiz.Application.Auth.UseCases.LoginUseCase
{
    public class LoginUseCaseValidator : AbstractValidator<LoginUseCaseInput>
    {
        public LoginUseCaseValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is invalid");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
