using EduQuiz.Application.Common.Responses;
using EduQuiz.Application.Common.UseCaseInvoker;
using EduQuiz.Application.UseCases.Class;
using Microsoft.AspNetCore.Mvc;

namespace EduQuiz.Api.Controllers
{
    [ApiController]
    [Route("api/classes")]
    public class ClassController : ControllerBase
    {
        public ClassController() { }

        [HttpGet]
        public async Task<IActionResult> GetClasses([FromQuery] GetClassesUseCaseInput request)
        {
            var result = await UseCaseInvoker.HandleAsync<GetClassesUseCaseInput, List<GetClassesUseCaseOutput>>(request);
            var meta = new ApiMeta
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = result.Count,
            };
            return Ok(ApiResponse<List<GetClassesUseCaseOutput>>.Ok(result, meta));
        }

        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetClassesByTeacherId(Guid teacherId)
        {
            var request = new GetClassesByTeacherIdUseCaseInput { TeacherId = teacherId };
            var result = await UseCaseInvoker.HandleAsync<GetClassesByTeacherIdUseCaseInput, List<GetClassesByTeacherIdUseCaseOutput>>(request);
            var meta = new ApiMeta
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = result.Count,
            };
            return Ok(ApiResponse<List<GetClassesByTeacherIdUseCaseOutput>>.Ok(result, meta));
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetClassesByStudentId(Guid studentId)
        {
            var request = new GetClassesByStudentIdUseCaseInput { StudentId = studentId };
            var result = await UseCaseInvoker.HandleAsync<GetClassesByStudentIdUseCaseInput, List<GetClassesByStudentIdUseCaseOutput>>(request);
            var meta = new ApiMeta
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = result.Count,
            };
            return Ok(ApiResponse<List<GetClassesByStudentIdUseCaseOutput>>.Ok(result, meta));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(Guid id)
        {
            var request = new GetClassByIdUseCaseInput { Id = id };
            var result = await UseCaseInvoker.HandleAsync<GetClassByIdUseCaseInput, GetClassByIdUseCaseOutput>(request);
            return Ok(ApiResponse<GetClassByIdUseCaseOutput>.Ok(result));
        }

        [HttpPost]
        public async Task<IActionResult> CreateClass(CreateClassUseCaseInput request)
        {
            var result = await UseCaseInvoker.HandleAsync<CreateClassUseCaseInput, CreateClassUseCaseOutput>(request);
            return Ok(ApiResponse<CreateClassUseCaseOutput>.Ok(result));
        }
    }
}
