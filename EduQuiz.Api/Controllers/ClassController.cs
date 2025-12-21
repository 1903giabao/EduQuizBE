using EduQuiz.Application.Auth.UseCases.LoginUseCase;
using EduQuiz.Application.Common.Responses;
using EduQuiz.Application.Common.UseCaseInvoker;
using EduQuiz.Application.UseCases.Class;
using Microsoft.AspNetCore.Mvc;

namespace EduQuiz.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        public ClassController() { }

        [HttpPost]
        public async Task<IActionResult> CreateClass(CreateClassUseCaseInput request)
        {
            var result = await UseCaseInvoker.HandleAsync<CreateClassUseCaseInput, CreateClassUseCaseOutput>(request);
            return Ok(ApiResponse<CreateClassUseCaseOutput>.Ok(result));
        }
    }
}
