using EduQuiz.Application.Auth.DTOs;
using EduQuiz.Application.Auth.UseCases.LoginUseCase;
using EduQuiz.Application.Common.IUseCase;
using EduQuiz.Infrastructure.Security;
using EduQuiz.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EduQuiz.Application.Auth
{
    public class LoginUseCase : IUseCase<LoginUseCaseInput, LoginUseCaseOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public LoginUseCase(IUnitOfWork unitOfWork, JwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginUseCaseOutput> HandleAsync(LoginUseCaseInput useCaseInput)
        {
            var account = await _unitOfWork.Accounts.Query()
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Email == useCaseInput.Email);

            if (account == null ||
                !PasswordHasher.Verify(useCaseInput.Password, account.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var accessToken = _jwtTokenGenerator.GenerateAccessToken(account);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken(account);

            return new LoginUseCaseOutput
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
    }
}
