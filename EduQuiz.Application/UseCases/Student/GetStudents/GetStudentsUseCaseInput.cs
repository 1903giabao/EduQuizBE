namespace EduQuiz.Application.UseCases.Student
{
    public class GetStudentsUseCaseInput
    {
        public string? Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
