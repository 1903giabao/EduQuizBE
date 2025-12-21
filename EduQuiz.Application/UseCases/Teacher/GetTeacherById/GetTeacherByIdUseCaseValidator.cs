using FluentValidation;

namespace EduQuiz.Application.UseCases.Teacher
{
    public class GetTeacherByIdUseCaseValidator : AbstractValidator<GetTeacherByIdUseCaseInput>
    {
        public GetTeacherByIdUseCaseValidator() { }
    }
}
