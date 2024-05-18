using System.Globalization;

namespace Projekt_Avancerad.NET.Helper
{
    public static class DateHelper
    {
        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }

            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }
    }
}
