namespace EduQuiz.Application.UseCases.Teacher
{
    public class GetTeacherByIdUseCaseOutput
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public string? Department { get; set; }
        public List<ClassOfTeacher> Classes { get; set; }
    }

    public class ClassOfTeacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
