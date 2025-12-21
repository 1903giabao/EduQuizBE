using EduQuiz.Application.Common.IUseCase;
using Microsoft.Extensions.DependencyInjection;

namespace EduQuiz.Application.Common.UseCaseInvoker
{
    public static class UseCaseInvoker
    {
        private static IServiceProvider? _serviceProvider;
        public static void Configure(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
        public static async Task<TUseCaseOutput> HandleAsync<TUseCaseInput, TUseCaseOutput>(TUseCaseInput useCaseInput)
        {
            if (_serviceProvider == null)
                throw new InvalidOperationException("UseCaseInvoker not configured");

            using var scope = _serviceProvider.CreateScope();
            var useCase = scope.ServiceProvider.GetRequiredService<IUseCase<TUseCaseInput, TUseCaseOutput>>();
            return await useCase.HandleAsync(useCaseInput);
        }
    }
}
