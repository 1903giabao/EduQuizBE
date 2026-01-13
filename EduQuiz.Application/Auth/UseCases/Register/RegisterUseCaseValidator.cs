using FluentValidation;

namespace EduQuiz.Application.Auth.UseCases
{
    public class RegisterUseCaseValidator : AbstractValidator<RegisterUseCaseInput>
    {
        public RegisterUseCaseValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50);

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required")
                .Must(g => g == "Male" || g == "Female" || g == "Other")
                .WithMessage("Gender must be Male, Female, or Other");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[0-9]{9,15}$")
                .WithMessage("Invalid phone number format");

            When(x => x.IsTeacherRegistration, () =>
            {
                RuleFor(x => x.Department)
                    .NotEmpty().WithMessage("Department is required for teachers");

                RuleFor(x => x.Bio)
                    .NotEmpty().WithMessage("Bio is required for teachers")
                    .MaximumLength(1000);
            });

            When(x => !x.IsTeacherRegistration, () =>
            {
                RuleFor(x => x.Grade)
                    .NotEmpty().WithMessage("Grade is required for students");

                RuleFor(x => x.School)
                    .NotEmpty().WithMessage("School is required for students");

                RuleFor(x => x.ParentPhoneNumber)
                    .NotEmpty().WithMessage("Parent phone number is required for students")
                    .Matches(@"^\+?[0-9]{9,15}$")
                    .WithMessage("Invalid parent phone number format");
            });
        }
    }

}
