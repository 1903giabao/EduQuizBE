using FluentValidation;

namespace EduQuiz.Application.UseCases.Teacher
{
    public class GetTeachersUseCaseValidator : AbstractValidator<GetTeachersUseCaseInput>
    {
        public GetTeachersUseCaseValidator() { }
    }
}
