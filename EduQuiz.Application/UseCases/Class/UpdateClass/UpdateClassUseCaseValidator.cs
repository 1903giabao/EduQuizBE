using FluentValidation;

namespace EduQuiz.Application.UseCases.Class
{
    public class UpdateClassUseCaseValidator : AbstractValidator<UpdateClassUseCaseInput>
    {
        public UpdateClassUseCaseValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Class name is required")
                .MaximumLength(100).WithMessage("Class name is invalid");
        }
    }
}
