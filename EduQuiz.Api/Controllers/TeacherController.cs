using EduQuiz.Application.Common.Responses;
using EduQuiz.Application.Common.UseCaseInvoker;
using EduQuiz.Application.UseCases.Student;
using EduQuiz.Application.UseCases.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace EduQuiz.Api.Controllers
{
    [ApiController]
    [Route("api/teachers")]
    public class TeacherController : ControllerBase
    {
        public TeacherController() { }

        [HttpGet()]
        public async Task<IActionResult> GetTeachers([FromQuery] GetTeachersUseCaseInput request)
        {
            var result = await UseCaseInvoker.HandleAsync<GetTeachersUseCaseInput, List<GetTeachersUseCaseOutput>>(request);
            var meta = new ApiMeta
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = result.Count,
            };
            return Ok(ApiResponse<List<GetTeachersUseCaseOutput>>.Ok(result, meta));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(Guid id)
        {
            var request = new GetTeacherByIdUseCaseInput { Id = id };
            var result = await UseCaseInvoker.HandleAsync<GetTeacherByIdUseCaseInput, GetTeacherByIdUseCaseOutput>(request);
            return Ok(ApiResponse<GetTeacherByIdUseCaseOutput>.Ok(result));
        }
    }
}
