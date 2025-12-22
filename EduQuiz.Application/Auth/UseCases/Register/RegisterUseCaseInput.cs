namespace EduQuiz.Application.Auth.UseCases
{
    public class RegisterUseCaseInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsTeacherRegistration { get; set; }
        public string? Bio { get; set; }
        public string? Department { get; set; }
        public string? ParentPhoneNumer { get; set; }
        public string? Grade { get; set; }
        public string? School { get; set; }
    }
}