using Amazon.S3;
using Amazon.S3.Model;
using EduQuiz.Infrastructure.Security;
using EduQuiz.Infrastructure.Services.FileStorageService.DTOs;
using Microsoft.Extensions.Options;

namespace EduQuiz.Infrastructure.Services.FileStorageService
{
    public class S3FileStorageService : IFileStorageService
    {
        private readonly IAmazonS3 _s3;
        private readonly StorageSettings _options;

        public S3FileStorageService(
            IAmazonS3 s3,
            IOptions<StorageSettings> options)
        {
            _s3 = s3;
            _options = options.Value;
        }

        public async Task UploadAsync(
            Stream fileStream,
            string fileName,
            string contentType,
            CancellationToken cancellationToken = default)
        {
            var request = new PutObjectRequest
            {
                BucketName = _options.Bucket,
                Key = fileName,
                InputStream = fileStream,
                ContentType = contentType
            };

            await _s3.PutObjectAsync(request, cancellationToken);
        }

        public async Task<FileResult> GetFileAsync(
            string fileName,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _s3.GetObjectAsync(
                    _options.Bucket,
                    fileName,
                    cancellationToken);

                return new FileResult
                {
                    Content = response.ResponseStream,
                    ContentType = response.Headers.ContentType,
                    ContentLength = response.Headers.ContentLength
                };
            }
            catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new FileNotFoundException($"File '{fileName}' not found.");
            }
        }

        public Task<string> GetPresignedUrlAsync(string fileName, TimeSpan expiresIn)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _options.Bucket,
                Key = fileName,
                Expires = DateTime.UtcNow.Add(expiresIn)
            };

            return Task.FromResult(_s3.GetPreSignedURL(request));
        }
    }
}
