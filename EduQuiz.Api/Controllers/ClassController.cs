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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(Guid id, [FromBody] UpdateClassUseCaseInput request)
        {
            request.Id = id;
            var result = await UseCaseInvoker.HandleAsync<UpdateClassUseCaseInput, UpdateClassUseCaseOutput>(request);
            return Ok(ApiResponse<UpdateClassUseCaseOutput>.Ok(result));
        }

        [HttpPut("{id}/publish")]
        public async Task<IActionResult> PublishClass(Guid id)
        {
            var request = new PublishClassUseCaseInput { Id = id };
            var result = await UseCaseInvoker.HandleAsync<PublishClassUseCaseInput, PublishClassUseCaseOutput>(request);
            return Ok(ApiResponse<PublishClassUseCaseOutput>.Ok(result));
        }

        [HttpPost("{id}/students")]
        public async Task<IActionResult> AddStudentToClass(Guid id, [FromBody] AddStudentToClassUseCaseInput request)
        {
            request.ClassId = id;
            var result = await UseCaseInvoker.HandleAsync<AddStudentToClassUseCaseInput, AddStudentToClassUseCaseOutput>(request);
            return Ok(ApiResponse<AddStudentToClassUseCaseOutput>.Ok(result));
        }

        [HttpPost("{id}/teachers")]
        public async Task<IActionResult> AddTeacherToClass(Guid id, [FromBody] AddTeacerToClassUseCaseInput request)
        {
            request.ClassId = id;
            var result = await UseCaseInvoker.HandleAsync<AddTeacerToClassUseCaseInput, AddTeacerToClassUseCaseOutput>(request);
            return Ok(ApiResponse<AddTeacerToClassUseCaseOutput>.Ok(result));
        }

        [HttpDelete("{id}/students/{studentId}")]
        public async Task<IActionResult> RemoveStudentFromClass(Guid id, Guid studentId)
        {
            var request = new RemoveStudentFromClassUseCaseInput { ClassId = id, StudentId = studentId };
            var result = await UseCaseInvoker.HandleAsync<RemoveStudentFromClassUseCaseInput, RemoveStudentFromClassUseCaseOutput>(request);
            return Ok(ApiResponse<RemoveStudentFromClassUseCaseOutput>.Ok(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(Guid id)
        {
            var request = new DeleteClassUseCaseInput { Id = id };
            var result = await UseCaseInvoker.HandleAsync<DeleteClassUseCaseInput, DeleteClassUseCaseOutput>(request);
            return Ok(ApiResponse<DeleteClassUseCaseOutput>.Ok(result));
        }
    }
}
