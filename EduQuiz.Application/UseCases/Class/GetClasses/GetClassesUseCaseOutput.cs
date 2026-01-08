using EduQuiz.Application.Common.Responses;
using EduQuiz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.UseCases.Class
{
    public class GetClassesUseCaseOutput
    {
        public List<GetClassesUseCaseResponse> Response { get; set; }
        public ApiMeta Meta { get; set; }
    }
    public class GetClassesUseCaseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string Status { get; set; }
        public int NumOfStudents { get; set; }
    }
}
