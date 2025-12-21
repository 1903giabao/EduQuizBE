using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Domain.Entities
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public Guid ClassSlotId { get; set; }
        public ClassSlot ClassSlot { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public bool IsAttended { get; set; } = false;
        public DateTime MarkedAt { get; set; }
        public string? Notes { get; set; }
    }
}
