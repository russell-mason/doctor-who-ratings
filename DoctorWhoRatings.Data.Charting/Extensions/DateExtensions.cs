namespace DoctorWhoRatings.Data.Charting.Extensions;

public static class DateExtensions
{
    public static bool IsChristmas(this DateTime date)
    {
        return date is { Month: 12, Day: 25 };
    }

    public static bool IsNewYear(this DateTime date)
    {
        return date is { Month: 1, Day: 1 };
    }

    public static bool IsSeasonal(this DateTime date) => date.IsChristmas() || date.IsNewYear();

    public static (int years, int months, int days) PeriodToNow(this DateTime from)
    {
        var to = DateTime.UtcNow.Date;

        var years = 0;
        var months = 0;
        var days = 0;

        while (from.AddYears(years + 1) <= to) years++;
        while (from.AddYears(years).AddMonths(months + 1) <= to) months++;
        while (from.AddYears(years).AddMonths(months).AddDays(days + 1) <= to) days++;

        return (years, months, days);
    }
}
