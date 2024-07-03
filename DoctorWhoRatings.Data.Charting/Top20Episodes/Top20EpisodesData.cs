namespace DoctorWhoRatings.Data.Charting.Top20Episodes;

public class Top20EpisodesData
{
    public const string Title = "Top 20 Episodes";

    public const string Description = "Shows the 20 episodes with the highest ratings.";

    public const string XAxisTitle = "Doctor";

    public const string YAxisTitle = "Ratings (millions)";

    public static readonly string[] SeriesTitles =
    [
        "Overnight viewers",
        "Additional viewers after seven days",
        "Additional viewers on all devices"
    ];

    public IReadOnlyList<Top20EpisodesDataPoint> DataPoints { get; set; } = [];
}