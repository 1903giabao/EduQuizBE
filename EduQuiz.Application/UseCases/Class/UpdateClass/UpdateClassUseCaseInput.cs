namespace EduQuiz.Application.UseCases.Class
{
    public class UpdateClassUseCaseInput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? TeacherId { get; set; }
    }
}
