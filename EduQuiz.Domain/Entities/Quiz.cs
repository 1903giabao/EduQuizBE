using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Domain.Entities
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int DurationMinutes { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid ClassId { get; set; }
        public Class Class { get; set; }
        public ICollection<StudentQuiz> StudentQuizzes { get; set; }
        public ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
