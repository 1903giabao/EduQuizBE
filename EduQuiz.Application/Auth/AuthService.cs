using EduQuiz.Application.Auth.DTOs;
using EduQuiz.Infrastructure.Context;
using EduQuiz.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EduQuiz.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly EduQuizDbContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private static List<(string Email, string RefreshToken)> _refreshTokens = new();

        public AuthService(
            EduQuizDbContext context,
            JwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Email == request.Email);

            if (account == null ||
                !PasswordHasher.Verify(request.Password, account.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var accessToken = _jwtTokenGenerator.GenerateAccessToken(account);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken(account);

            _refreshTokens.Add((account.Email, refreshToken));

            return new LoginResponse
            {
                Token = new TokenModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                },
                Role = account.Role.Name,
                AccountId = account.Id
            };
        }

        public async Task<TokenModel> RefreshAsync(string refreshToken)
        {
            var principal = _jwtTokenGenerator.ValidateRefreshToken(refreshToken);

            if (principal.FindFirst("type")?.Value != "refresh")
                throw new UnauthorizedAccessException("Invalid refresh token");

            var accountId = principal.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var account = await _context.Accounts
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
