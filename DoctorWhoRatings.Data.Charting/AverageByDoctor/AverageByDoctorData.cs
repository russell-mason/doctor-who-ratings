namespace DoctorWhoRatings.Data.Charting.AverageByDoctor;

public class AverageByDoctorData
{
    public const string Title = "Average By Doctor";

    public const string Description = "Shows average ratings across all episodes for each doctor.";

    public const string XAxisTitle = "Doctor";

    public const string YAxisTitle = "Ratings (millions)";

    public static readonly string[] SeriesTitles =
    [
        "Overnight viewers",
        "Additional viewers after seven days",
        "Additional viewers on all devices"
    ];

    public IReadOnlyList<AverageByDoctorDataPoint> DataPoints { get; set; } = [];
}