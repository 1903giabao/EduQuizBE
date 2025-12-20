using EduQuiz.Infrastructure.Context;
using EduQuiz.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            return services;
        }
    }
}
