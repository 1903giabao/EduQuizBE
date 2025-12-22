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
    }
}
