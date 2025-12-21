using FluentValidation;

namespace EduQuiz.Application.UseCases.Student
{
    public class GetStudentsByClassIdUseCaseValidator : AbstractValidator<GetStudentsByClassIdUseCaseInput>
    {
        public GetStudentsByClassIdUseCaseValidator() { }
    }
}
