using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.Common.Responses
{
    public class ApiError
    {
        public string Code { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
