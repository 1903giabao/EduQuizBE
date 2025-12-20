using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Domain.Entities
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public string? Bio { get; set; }
        public string? Department { get; set; }
        public ICollection<Class> Classes { get; set; }
        public ICollection<Question> Questions{ get; set; }
    }
}
