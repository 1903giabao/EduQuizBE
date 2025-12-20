using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Domain.Entities
{
    public class StudentClass
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid ClassId { get; set; }
        public Class Class { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
