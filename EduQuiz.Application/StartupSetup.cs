using EduQuiz.Application.Auth;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EduQuiz.Application
{
    public static class StartupSetup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.Scan(s =>
                    s.FromCallingAssembly()
                    .AddClasses(c => c.AssignableTo(typeof(IUseCase<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITemplateService, TemplateService>();

            //Validator
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(StartupSetup).Assembly);

            return services;
        }
    }
}
