namespace EduQuiz.Application.UseCases.Class
{
    public class AddTeacerToClassUseCaseInput
    {
        public Guid ClassId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
