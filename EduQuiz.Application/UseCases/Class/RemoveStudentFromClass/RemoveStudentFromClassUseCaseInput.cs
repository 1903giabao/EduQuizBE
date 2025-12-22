namespace EduQuiz.Application.UseCases.Class
{
    public class RemoveStudentFromClassUseCaseInput
    {
        public Guid ClassId { get; set; }
        public Guid StudentId { get; set; }
    }
}
