namespace DoctorWhoRatings.Data.Charting.HighLowEpisodesByDoctor;

public class HighLowEpisodesByDoctorData
{
    public const string Title = "High/Low Episodes (by Doctor)";

    public const string Description = "Shows highest and lowest rated episodes for each doctor.";

    public const string XAxisTitle = "Doctor";

    public const string YAxisTitle = "Ratings (millions)";

    public static readonly string[] SeriesTitles =
    [
        "Overnight viewers (High)",
        "Additional viewers after seven days (High)",
        "Additional viewers on all devices (High)",
        "Overnight viewers (Low)",
        "Additional viewers after seven days (Low)",
        "Additional viewers on all devices (Low)"
    ];

    public IReadOnlyList<HighLowEpisodesByDoctorDataPoint> DataPoints { get; set; } = [];
}