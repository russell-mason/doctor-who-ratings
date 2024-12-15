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
}
