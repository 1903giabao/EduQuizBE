using EduQuiz.Application.Auth.DTOs;

namespace EduQuiz.Application.Auth
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<TokenModel> RefreshAsync(string refreshToken);
    }
}
