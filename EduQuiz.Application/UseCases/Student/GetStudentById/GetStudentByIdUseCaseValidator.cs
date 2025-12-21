using FluentValidation;

namespace EduQuiz.Application.UseCases.Student
{
    public class GetStudentByIdUseCaseValidator : AbstractValidator<GetStudentByIdUseCaseInput>
    {
        public GetStudentByIdUseCaseValidator() { }
    }
}
