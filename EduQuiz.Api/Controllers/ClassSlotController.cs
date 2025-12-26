using EduQuiz.Application.Common.Responses;
using EduQuiz.Application.Common.UseCaseInvoker;
using EduQuiz.Application.UseCases.Class;
using EduQuiz.Application.UseCases.ClassSlot;
using Microsoft.AspNetCore.Mvc;

namespace EduQuiz.Api.Controllers
{
    [ApiController]
    [Route("api/class-slots")]
    public class ClassSlotController : ControllerBase
    {
        public ClassSlotController() { }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetClassSlotsByStudentId(Guid studentId, [FromQuery] GetClassSlotsByStudentIdUseCaseInput request)
        {
            request.StudentId = studentId;
            var result = await UseCaseInvoker.HandleAsync<GetClassSlotsByStudentIdUseCaseInput, List<GetClassSlotsByStudentIdUseCaseOutput>>(request);
            return Ok(ApiResponse<List<GetClassSlotsByStudentIdUseCaseOutput>>.Ok(result));
        }
    }
}
