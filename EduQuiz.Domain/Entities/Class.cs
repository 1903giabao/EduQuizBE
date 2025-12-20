using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Domain.Entities
{
    public class Class
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<ClassSlot> Slots { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
        public ICollection<StudentClass> StudentClasses { get; set; }
    }
}
