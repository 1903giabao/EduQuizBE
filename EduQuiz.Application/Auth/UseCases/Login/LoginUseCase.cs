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
                throw new ArgumentException("Invalid email or password");
            }

            var accessToken = _jwtTokenGenerator.GenerateAccessToken(account);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken(account);

            return new LoginUseCaseOutput
            {
                Token = new DTOs.TokenModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                },
                Response = new LoginResponse
                {
                    AccountId = account.Id,
                    Role = account.Role.Name
                }
            };
        }
    }
}
