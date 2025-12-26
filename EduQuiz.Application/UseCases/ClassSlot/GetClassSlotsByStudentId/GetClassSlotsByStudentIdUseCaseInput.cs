using static EduQuiz.Share.Enums.Enum;

namespace EduQuiz.Application.UseCases.ClassSlot
{
    public class GetClassSlotsByStudentIdUseCaseInput
    {
        public Guid? TeacherId { get; set; }
        public Guid? StudentId { get; set; }
        public DateTime? Date { get; set; }
    }
}
