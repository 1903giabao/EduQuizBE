using EduQuiz.Application.Common.Responses;

namespace EduQuiz.Application.UseCases
{
    public class GetClassSlotsByTeacherIdUseCaseOutput
    {
        public List<GetClassSlotsByTeacherIdUseCaseResponse> Response {  get; set; }
        public ApiMeta Meta { get; set; }
    }

    public class GetClassSlotsByTeacherIdUseCaseResponse
    {
        public Guid Id { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
