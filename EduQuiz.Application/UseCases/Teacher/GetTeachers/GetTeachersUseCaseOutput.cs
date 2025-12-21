
namespace EduQuiz.Application.UseCases.Teacher
{
    public class GetTeachersUseCaseOutput
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public string? Department { get; set; }
    }
}
