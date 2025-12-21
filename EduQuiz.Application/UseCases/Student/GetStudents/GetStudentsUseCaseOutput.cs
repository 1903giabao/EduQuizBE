namespace EduQuiz.Application.UseCases.Student
{
    public class GetStudentsUseCaseOutput
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ParentPhoneNumer { get; set; }
        public string? Grade { get; set; }
        public string? School { get; set; }
    }
}
