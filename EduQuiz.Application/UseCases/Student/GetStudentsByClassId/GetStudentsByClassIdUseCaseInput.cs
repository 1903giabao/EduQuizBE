namespace EduQuiz.Application.UseCases.Student
{
    public class GetStudentsByClassIdUseCaseInput
    {
        public Guid ClassId { get; set; }
        public string? Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
