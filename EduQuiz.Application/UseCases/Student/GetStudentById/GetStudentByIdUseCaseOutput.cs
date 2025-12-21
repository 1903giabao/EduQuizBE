namespace EduQuiz.Application.UseCases.Student
{
    public class GetStudentByIdUseCaseOutput
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ParentPhoneNumer { get; set; }
        public string? Grade { get; set; }
        public string? School { get; set; }
        public List<ClassOfStudent> Classes { get; set; }
    }

    public class ClassOfStudent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? TeacherId { get; set; }
        public string? TeacherName { get; set; }
    }
}
