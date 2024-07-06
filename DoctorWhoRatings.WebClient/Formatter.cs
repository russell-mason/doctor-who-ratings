namespace DoctorWhoRatings.WebClient;

public static class Formatter
{
    public static string FormatNumber(decimal? number) => number?.ToString("0.00") ?? "N/A";

    public static string FormatNumber(int number) => number.ToString("00,000,000");

    public static string FormatDate(DateTime? date) => date?.ToString("dd MMMM yyyy") ?? "N/A";

    public static TimeSpan ToTimeSpan(decimal? number) => number == null ? TimeSpan.Zero : TimeSpan.FromHours((double) number);

    public static MarkupString FormatNote(string note) =>
        new(string.Join("<br />", note.Split("*", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)));
}
