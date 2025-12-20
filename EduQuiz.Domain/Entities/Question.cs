using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Domain.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string[] Options { get; set; }
        public string[] Answer { get; set; }
        public string? Explanation { get; set; }
        public string? Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
