namespace EduQuiz.Share.Extensions
{
    public static class MonthExtensions
    {
        public static string ToVietnameseMonth(this string month)
        {
            month = month.ToLower();
            return month switch
            {
                "january" => "Tháng 1",
                "february" => "Tháng 2",
                "march" => "Tháng 3",
                "april" => "Tháng 4",
                "May" => "Tháng 5",
                "june" => "Tháng 6",
                "july" => "Tháng 7",
                "august" => "Tháng 8",
                "september" => "Tháng 9",
                "october" => "Tháng 10",
                "november" => "Tháng 11",
                "december" => "Tháng 12",
                _ => month
            };
        }
    }
}
