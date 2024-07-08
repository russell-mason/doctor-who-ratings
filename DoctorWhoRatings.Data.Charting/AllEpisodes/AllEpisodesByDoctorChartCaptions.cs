namespace DoctorWhoRatings.Data.Charting.AllEpisodes;

public static class AllEpisodesByDoctorChartCaptions
{
    public const string Title = "All Episodes (by Doctor)";

    public const string Description = "Shows ratings for all episodes, including Classic Who, New Who, and the Movie.";

    public const string XAxisTitle = "Episodes";

    public const string YAxisTitle = "Ratings (millions)";

    public static readonly string[] SeriesTitles =
    [
        "Overnight viewers",
        "Additional viewers after seven days",
        "Additional viewers on all devices"
    ];
}