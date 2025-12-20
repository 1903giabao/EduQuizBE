using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Domain.Entities
{
    public class StudentQuiz
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public double Score { get; set; }
        public DateTime? SubmittedAt { get; set; } 
        public bool IsCompleted { get; set; } = false;
        public int AttemptNumber { get; set; } = 1;
        public string Answers { get; set; }
        public bool IsPaused { get; set; } = false;
        public TimeSpan ElapsedTime { get; set; } = TimeSpan.Zero;
        public DateTime? LastPausedAt { get; set; }
    }
}
