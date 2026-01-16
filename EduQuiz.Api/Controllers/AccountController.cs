using EduQuiz.Application.Common.Responses;
using EduQuiz.Application.Common.UseCaseInvoker;
using EduQuiz.Application.UseCases.Account;
using EduQuiz.Application.UseCases.Class;
using Microsoft.AspNetCore.Mvc;

namespace EduQuiz.Api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        public AccountController() { }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetUserProfile(Guid id)
        {
            var request = new GetUserProfileUseCaseInput { Id = id };
            var result = await UseCaseInvoker.HandleAsync<GetUserProfileUseCaseInput, GetUserProfileUseCaseOutput>(request);
            return Ok(ApiResponse<GetUserProfileUseCaseOutput>.Ok(result));
        }

        [HttpPut("profile/avatar/{id}")]
        public async Task<IActionResult> UpdateAccountAvatar([FromRoute] Guid id, [FromForm] IFormFile file)
        {
            var request = new UpdateAccountAvatarUseCaseInput { Id = id, Avatar = file };
            var result = await UseCaseInvoker.HandleAsync<UpdateAccountAvatarUseCaseInput, UpdateAccountAvatarUseCaseOutput>(request);
            return Ok(ApiResponse<UpdateAccountAvatarUseCaseOutput>.Ok(result));
        }
    }
}
