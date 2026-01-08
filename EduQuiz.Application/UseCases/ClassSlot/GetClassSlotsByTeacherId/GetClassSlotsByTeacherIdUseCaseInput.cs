namespace EduQuiz.Application.UseCases
{
    public class GetClassSlotsByTeacherIdUseCaseInput
    {
        public Guid TeacherId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
