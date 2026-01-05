using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Share.Extensions
{
    public static class DayExtension
    {
        public static List<DateOnly> ExtractDateByDayAndTimeRange(DateTime startDate, DateTime endDate, DayOfWeek day)
        {
            var result = new List<DateOnly>();
            for (DateTime i = startDate; i <= endDate; i = i.AddDays(1))
            {
                if (day == i.DayOfWeek)
                {
                    var dateOnly = DateOnly.FromDateTime(i);
                    result.Add(dateOnly);
                }
            }

            return result;
        }
    }
}
