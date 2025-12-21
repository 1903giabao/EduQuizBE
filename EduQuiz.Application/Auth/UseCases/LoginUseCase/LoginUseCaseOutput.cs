using EduQuiz.Application.Auth.DTOs;

namespace EduQuiz.Application.Auth.UseCases.LoginUseCase
{
    public class LoginUseCaseOutput
    {
        public TokenModel Token { get; set; }
        public string Role { get; set; } = null!;
        public Guid AccountId { get; set; }
    }
}
