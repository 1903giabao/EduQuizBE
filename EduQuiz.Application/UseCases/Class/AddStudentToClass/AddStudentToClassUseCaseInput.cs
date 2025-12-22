namespace EduQuiz.Application.UseCases.Class
{
    public class AddStudentToClassUseCaseInput
    {
        public Guid ClassId { get; set; }
        public Guid StudentId { get; set; }
    }
}
