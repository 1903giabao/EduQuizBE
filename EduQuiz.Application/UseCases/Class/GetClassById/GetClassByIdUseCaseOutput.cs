using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.UseCases.Class
{
    public class GetClassByIdUseCaseOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string Status { get; set; }
        public int NumOfStudents { get; set; }
        public List<string>? Locations { get; set; }
        public List<string>? Schedules {  get; set; }
        public List<StudentInClass>? StudentInClasses { get; set; }
        public List<QuizOfClass>? QuizOfClasses { get; set; }
        public List<ScheduleOfClass>? ScheduleOfClasses { get; set; }
    }

    public class StudentInClass
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
    }

    public class QuizOfClass
    {
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public int DurationMinutes { get; set; }
    }

    public class ScheduleOfClass
    {
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
