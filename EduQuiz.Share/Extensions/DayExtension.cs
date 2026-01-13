using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
        public static List<string> ToListDayFormat(List<DateTime> dates)
        {
            var result = new List<string>();
            foreach (var date in dates)
            {
                var stringDayOfWeek = ToDayOfWeek3Letters(date);
                var newDayString = stringDayOfWeek + " " + date.ToString("HH:mm");
                if (!result.Contains(newDayString))
                    result.Add(newDayString);
            }

            return result;
        }
        public static string ToDayOfWeek(DateTime date)
        {
            return date.DayOfWeek switch
            {
                DayOfWeek.Sunday => "Sunday",
                DayOfWeek.Monday => "Monday",
                DayOfWeek.Tuesday => "Tuesday",
                DayOfWeek.Wednesday => "Wednesday",
                DayOfWeek.Thursday => "Thursday",
                DayOfWeek.Friday => "Friday",
                DayOfWeek.Saturday => "Saturday",
                _ => string.Empty
            };
        }
        public static string ToDayOfWeek3Letters(DateTime date)
        {
            return date.DayOfWeek switch
            {
                DayOfWeek.Sunday => "Sun",
                DayOfWeek.Monday => "Mon",
                DayOfWeek.Tuesday => "Tue",
                DayOfWeek.Wednesday => "Wed",
                DayOfWeek.Thursday => "Thu",
                DayOfWeek.Friday => "Fri",
                DayOfWeek.Saturday => "Sat",
                _ => string.Empty
            };
        }
    }
}
