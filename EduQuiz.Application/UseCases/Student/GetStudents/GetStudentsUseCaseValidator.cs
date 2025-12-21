using FluentValidation;

namespace EduQuiz.Application.UseCases.Student
{
    public class GetStudentsUseCaseValidator : AbstractValidator<GetStudentsUseCaseInput>
    {
        public GetStudentsUseCaseValidator() { }
    }
}
