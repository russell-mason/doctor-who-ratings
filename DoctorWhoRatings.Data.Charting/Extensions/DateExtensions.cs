namespace DoctorWhoRatings.Data.Charting.Extensions;

public static class DateExtensions
{
    extension(DateTime date)
    {
        public bool IsChristmas()=> date is { Month: 12, Day: 25 };

        public bool IsNewYear() => date is { Month: 1, Day: 1 };

        public bool IsSeasonal() => date.IsChristmas() || date.IsNewYear();

        public (int years, int months, int days) PeriodToNow()
        {
            var to = DateTime.UtcNow.Date;

            var years = 0;
            var months = 0;
            var days = 0;

            while (date.AddYears(years + 1) <= to) years++;
            while (date.AddYears(years).AddMonths(months + 1) <= to) months++;
            while (date.AddYears(years).AddMonths(months).AddDays(days + 1) <= to) days++;

            return (years, months, days);
        }
    }
}
