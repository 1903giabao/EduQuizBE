using EduQuiz.Application.Auth.DTOs;

namespace EduQuiz.Application.Auth
{
    public class LoginUseCaseOutput
    {
        public TokenModel Token { get; set; }
        public LoginResponse Response { get; set; }
    }

    public class LoginResponse
    {
        public string Role { get; set; } = null!;
        public Guid AccountId { get; set; }
    }
}
