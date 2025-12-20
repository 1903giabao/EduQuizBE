using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public string? ParentPhoneNumer { get; set; }
        public string? Grade { get; set; }
        public string? School { get; set; }
        public ICollection<StudentClass> StudentClasses { get; set; }
        public ICollection<StudentQuiz> StudentQuizzes { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}
