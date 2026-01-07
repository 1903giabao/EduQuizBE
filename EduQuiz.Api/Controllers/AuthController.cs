using EduQuiz.Application.Auth;
using EduQuiz.Application.Auth.UseCases;
using EduQuiz.Application.Common.Responses;
using EduQuiz.Application.Common.UseCaseInvoker;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduQuiz.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUseCaseInput request)
    {
        var result = await UseCaseInvoker.HandleAsync<LoginUseCaseInput, LoginUseCaseOutput>(request);
        SetAuthCookies(result.Token.AccessToken, result.Token.RefreshToken);
        return Ok(ApiResponse<LoginResponse>.Ok(result.Response));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refresh_token"];

        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized();

        var result = await _authService.RefreshAsync(refreshToken);
        SetAuthCookies(result.AccessToken, result.RefreshToken);
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUseCaseInput request)
    {
        var result = await UseCaseInvoker.HandleAsync<RegisterUseCaseInput, RegisterUseCaseOutput>(request);
        return Ok(ApiResponse<RegisterUseCaseOutput>.Ok(result));
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("access_token");
        Response.Cookies.Delete("refresh_token");
        return Ok();
    }

    [HttpGet("me")]
    public async Task<IActionResult> AboutMe()
    {
        var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(accountId))
            return Unauthorized();

        var request = new AboutMeUseCaseInput { AccountId = accountId };
        var result = await UseCaseInvoker.HandleAsync<AboutMeUseCaseInput, AboutMeUseCaseOutput>(request);
        return Ok(ApiResponse<AboutMeUseCaseOutput>.Ok(result));
    }

    private void SetAuthCookies(string accessToken, string refreshToken)
    {
        Response.Cookies.Append(
            "access_token",
            accessToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(15),
                Path = "/"
            }
        );

        Response.Cookies.Append(
            "refresh_token",
            refreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(14),
                Path = "/"
            }
        );
    }
}