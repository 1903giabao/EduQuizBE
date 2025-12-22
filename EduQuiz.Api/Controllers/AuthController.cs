using EduQuiz.Application.Auth;
using EduQuiz.Application.Auth.DTOs;
using EduQuiz.Application.Auth.UseCases;
using EduQuiz.Application.Auth.UseCases.LoginUseCase;
using EduQuiz.Application.Common.Responses;
using EduQuiz.Application.Common.UseCaseInvoker;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(ApiResponse<LoginUseCaseOutput>.Ok(result));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
        var result = await _authService.RefreshAsync(refreshToken);
        return Ok(ApiResponse<TokenModel>.Ok(result));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUseCaseInput request)
    {
        var result = await UseCaseInvoker.HandleAsync<RegisterUseCaseInput, RegisterUseCaseOutput>(request);
        return Ok(ApiResponse<RegisterUseCaseOutput>.Ok(result));
    }
}