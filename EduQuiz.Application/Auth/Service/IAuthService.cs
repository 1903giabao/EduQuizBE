using EduQuiz.Application.Auth.DTOs;

namespace EduQuiz.Application.Auth
{
    public interface IAuthService
    {
        Task<TokenModel> RefreshAsync(string refreshToken);
    }
}
