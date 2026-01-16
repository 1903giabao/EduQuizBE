using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EduQuiz.Share.Extensions
{
    public static class FileExtension
    {
        public static string BuildFileName(string originalFileName)
        {
            var cleaned = Regex.Replace(originalFileName, @"[^a-zA-Z0-9]", " ");

            var normalized = string.Join("_",
                cleaned.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            var today = DateTime.Now;
            var result = string.Join("_", normalized,
                today.ToString("yyyy-MM-dd"),
                today.Ticks.ToString());

            return result;
        }
    }
}
