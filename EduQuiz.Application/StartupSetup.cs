using EduQuiz.Application.Auth;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace EduQuiz.Application
{
    public static class StartupSetup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            //Validator
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(StartupSetup).Assembly);

            return services;
        }
    }
}
