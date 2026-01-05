using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.UseCases.Class
{
    public class CreateClassUseCaseInput
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<SlotInDay> SlotInDays { get; set; }
    }

    public class SlotInDay
    {
        public DayOfWeek Day { get; set; }
        public TimeOnly StartSlotTime { get; set; }
        public TimeOnly EndSlotTime { get; set; }
        public string Location { get; set; }
    }
}
