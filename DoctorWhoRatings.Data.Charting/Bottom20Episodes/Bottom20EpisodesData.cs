namespace DoctorWhoRatings.Data.Charting.Bottom20Episodes;

public class Bottom20EpisodesData
{
    public const string Title = "Bottom 20 Episodes";

    public const string Description = "Shows the 20 episodes with the lowest ratings.";

    public const string XAxisTitle = "Doctor";

    public const string YAxisTitle = "Ratings (millions)";

    public static readonly string[] SeriesTitles =
    [
        "Overnight viewers",
        "Additional viewers after seven days",
        "Additional viewers on all devices"
    ];

    public IReadOnlyList<Bottom20EpisodesDataPoint> DataPoints { get; set; } = [];
}