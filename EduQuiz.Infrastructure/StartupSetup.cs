using Amazon.S3;
using EduQuiz.Infrastructure.Context;
using EduQuiz.Infrastructure.Security;
using EduQuiz.Infrastructure.Services.FileStorageService;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EduQuiz.Infrastructure
{
    public static class StartupSetup
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<EduQuizDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddScoped<JwtTokenGenerator>();

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            #region S3 storage setup
            services.Configure<StorageSettings>(configuration.GetSection("Storage"));

            services.AddSingleton<IAmazonS3>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<StorageSettings>>().Value;

                var config = new AmazonS3Config
                {
                    ServiceURL = options.Endpoint,
                    ForcePathStyle = true,
                    UseHttp = !options.UseSsl
                };

                return new AmazonS3Client(
                    options.AccessKey,
                    options.SecretKey,
                    config
                );
            });

            services.AddScoped<IFileStorageService, S3FileStorageService>();
            #endregion

            return services;
        }
    }
}
