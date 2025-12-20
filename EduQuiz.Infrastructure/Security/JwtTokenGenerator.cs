using EduQuiz.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EduQuiz.Infrastructure.Security;

public class JwtTokenGenerator
{
    private readonly JwtSettings _settings;

    public JwtTokenGenerator(IOptions<JwtSettings> options)
    {
        _settings = options.Value;
    }

    public string GenerateAccessToken(Account account)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, account.Email),
            new Claim(ClaimTypes.Role, account.Role.Name)
        };

        return GenerateToken(
            claims,
            DateTime.UtcNow.AddMinutes(_settings.AccessTokenMinutes),
            _settings.AccessTokenKey);
    }

    public string GenerateRefreshToken(Account account)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
            new Claim("type", "refresh")
        };

        return GenerateToken(
            claims,
            DateTime.UtcNow.AddDays(_settings.RefreshTokenDays),
            _settings.RefreshTokenKey);
    }

    public ClaimsPrincipal ValidateRefreshToken(string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _settings.Issuer,
            ValidAudience = _settings.Audience,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_settings.RefreshTokenKey)),
            ClockSkew = TimeSpan.Zero
        };

        return tokenHandler.ValidateToken(
            refreshToken,
            parameters,
            out _);
    }

    private string GenerateToken(
        IEnumerable<Claim> claims,
        DateTime expires,
        string signingKey)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(signingKey));

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

