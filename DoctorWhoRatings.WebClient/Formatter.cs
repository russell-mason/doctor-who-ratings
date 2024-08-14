﻿namespace DoctorWhoRatings.WebClient;

public static class Formatter
{
    // Assumes values in millions
    public static string FormatNumber(decimal? number) => 
        (number is >= 1000 ? (number/1000)?.ToString("0.000") : number?.ToString("0.00") ?? "N/A") ?? "N/A";

    public static string FormatNumber(int number) => number.ToString("00,000,000");

    public static string FormatDate(DateTime? date) => date?.ToString("dd MMMM yyyy") ?? "N/A";

    // Assumes values in millions
    public static string NumberRepresentationText(decimal? number) => number >= 1000 ? "billion" : "million";

    public static TimeSpan ToTimeSpan(decimal? number) => number == null ? TimeSpan.Zero : TimeSpan.FromHours((double) number);

    public static MarkupString FormatNote(string note) =>
        new(string.Join("<br />", note.Split("*", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)));
}
