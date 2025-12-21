using EduQuiz.Application.Auth.DTOs;
using EduQuiz.Infrastructure.Context;
using EduQuiz.Infrastructure.Security;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EduQuiz.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            IUnitOfWork unitOfWork,
            JwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<TokenModel> RefreshAsync(string refreshToken)
        {
            var principal = _jwtTokenGenerator.ValidateRefreshToken(refreshToken);

            if (principal.FindFirst("type")?.Value != "refresh")
                throw new UnauthorizedAccessException("Invalid refresh token");

            var accountId = principal.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var account = await _unitOfWork.Accounts.Query()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(accountId))
                ?? throw new UnauthorizedAccessException("Invalid refresh token");

            return new TokenModel
            {
                AccessToken = _jwtTokenGenerator.GenerateAccessToken(account),
                RefreshToken = _jwtTokenGenerator.GenerateRefreshToken(account)
            };
        }
    }
}
