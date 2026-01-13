using EduQuiz.Domain.Entities;

namespace EduQuiz.Application.UseCases.Account
{
    public class GetUserProfileUseCaseOutput
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } 
        public string? Avatar { get; set; }
        public string? Bio { get; set; }
        public string? Department { get; set; }
        public string? ParentPhoneNumber { get; set; }
        public string? School { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? TeachingClassesCount { get; set; }
        public int? StudentsCount { get; set; }
        public int? PublishedClassesCount { get; set; }
        public int? NonPublishedClassesCount { get; set; }
        public List<ClassSchedule>? ClassSchedule { get; set; }
        public int? EnrolledClassesCount { get; set; }
        public int? CompletedQuizzesCount { get; set; }
        public int? ToDoQuizzesCount { get; set; }     
    }
    public class ClassSchedule
    {
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public List<string> Times { get; set; }
        public List<string> Location { get; set; }
    }
}
