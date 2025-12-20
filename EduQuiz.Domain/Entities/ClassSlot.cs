using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Domain.Entities
{
    public class ClassSlot
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public Class Class { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool IsOffline { get; set; } = true;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Attendance> Attendances { get; set; }
    }
}
