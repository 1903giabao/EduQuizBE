using EduQuiz.Application.Common.Responses;
using EduQuiz.Application.Common.UseCaseInvoker;
using EduQuiz.Application.UseCases.Student;
using Microsoft.AspNetCore.Mvc;

namespace EduQuiz.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        public StudentController() { }

        [HttpGet()]
        public async Task<IActionResult> GetStudentsId([FromQuery] GetStudentsUseCaseInput request)
        {
            var result = await UseCaseInvoker.HandleAsync<GetStudentsUseCaseInput, List<GetStudentsUseCaseOutput>>(request);
            var meta = new ApiMeta
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = result.Count,
            };
            return Ok(ApiResponse<List<GetStudentsUseCaseOutput>>.Ok(result, meta));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var request = new GetStudentByIdUseCaseInput { Id = id };
            var result = await UseCaseInvoker.HandleAsync<GetStudentByIdUseCaseInput, GetStudentByIdUseCaseOutput>(request);
            return Ok(ApiResponse<GetStudentByIdUseCaseOutput>.Ok(result));
        }

        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetStudentsByClassId(Guid classId, [FromQuery] GetStudentsByClassIdUseCaseInput request)
        {
            request.ClassId = classId;
            var result = await UseCaseInvoker.HandleAsync<GetStudentsByClassIdUseCaseInput, List<GetStudentsByClassIdUseCaseOutput>>(request);
            var meta = new ApiMeta
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = result.Count,
            };
            return Ok(ApiResponse<List<GetStudentsByClassIdUseCaseOutput>>.Ok(result, meta));
        }
    }
}
