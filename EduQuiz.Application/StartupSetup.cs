using EduQuiz.Application.Auth;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using EduQuiz.Application.Common.IUseCase;
using Scrutor;

namespace EduQuiz.Application
{
    public static class StartupSetup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            _ = services.Scan(s =>
                    s.FromCallingAssembly()
                    .AddClasses(c => c.AssignableTo(typeof(IUseCase<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services.AddScoped<IAuthService, AuthService>();

            //Validator
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(StartupSetup).Assembly);

            return services;
        }
    }
}
