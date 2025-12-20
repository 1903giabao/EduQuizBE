using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Domain.Entities
{
    public class QuizQuestion
    {
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public int Score { get; set; }
        public int Order { get; set; }
    }
}
