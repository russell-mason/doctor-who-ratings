namespace DoctorWhoRatings.Data.Charting.EpisodeInContext;

public static class EpisodeInContextChartCaptions
{
    public const string Title = "Episode In Context";

    public const string Description = "Shows the episode within the context of its story, season, doctor, and all episodes. Based on overnight ratings only.";

    public const string XAxisTitle = "Doctor";

    public const string YAxisTitle = "Ratings (millions)";

    public static readonly string[] SeriesTitles =
    [
        "Overnight viewers",
        "Additional viewers after seven days",
        "Additional viewers on all devices"
    ];
}