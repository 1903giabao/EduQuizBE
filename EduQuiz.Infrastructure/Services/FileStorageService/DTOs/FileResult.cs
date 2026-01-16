using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Infrastructure.Services.FileStorageService.DTOs
{
    public class FileResult
    {
        public Stream Content { get; set; } = null!;
        public string ContentType { get; set; } = "application/octet-stream";
        public long ContentLength { get; set; }
    }
}
