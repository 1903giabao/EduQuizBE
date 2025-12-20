using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.Common.Responses
{
    public class ApiMeta
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages =>
            (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
