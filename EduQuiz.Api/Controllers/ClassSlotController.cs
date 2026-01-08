using EduQuiz.Application.Common.Responses;
using EduQuiz.Application.Common.UseCaseInvoker;
using EduQuiz.Application.UseCases;
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
            var result = await UseCaseInvoker.HandleAsync<GetClassSlotsByStudentIdUseCaseInput, GetClassSlotsByStudentIdUseCaseOutput>(request);
            return Ok(ApiResponse<List<GetClassSlotsByStudentIdUseCaseResponse>>.Ok(result.Response, result.Meta));
        }

        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetClassSlotsByStudentId(Guid teacherId, [FromQuery] GetClassSlotsByTeacherIdUseCaseInput request)
        {
            request.TeacherId = teacherId;
            var result = await UseCaseInvoker.HandleAsync<GetClassSlotsByTeacherIdUseCaseInput, GetClassSlotsByTeacherIdUseCaseOutput>(request);
            return Ok(ApiResponse<List<GetClassSlotsByTeacherIdUseCaseResponse>>.Ok(result.Response, result.Meta));
        }
    }
}
