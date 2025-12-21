using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EduQuiz.Share.Enums.Enum;

namespace EduQuiz.Application.UseCases.Class
{
    public class GetClassesUseCaseInput
    {
        public List<Guid>? TeacherIds {  get; set; }
        public List<Guid>? StudentIds { get; set; }
        public ClassStatus? Status { get; set; }
        public string? Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
