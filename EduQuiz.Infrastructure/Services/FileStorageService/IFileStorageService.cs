using EduQuiz.Infrastructure.Services.FileStorageService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Infrastructure.Services.FileStorageService
{
    public interface IFileStorageService
    {
        Task UploadAsync(
            Stream fileStream,
            string fileName,
            string contentType,
            CancellationToken cancellationToken = default);

        Task<FileResult> GetFileAsync(
            string fileName,
            CancellationToken cancellationToken = default);

        Task<string> GetPresignedUrlAsync(string fileName, TimeSpan expiresIn);
    }
}
