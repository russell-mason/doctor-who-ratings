namespace DoctorWhoRatings.Data.Charting.TotalHoursWatchedByDoctor;

public class TotalHoursWatchedByDoctorChartCaptions
{
    public const string Title = "Total Hours Watched (by Doctor)";

    public const string Description = "Shows the total number of hours watched for each doctor.";

    public const string XAxisTitle = "Doctor";

    public const string YAxisTitle = "Share (%)";

    public static readonly string[] SeriesTitles =
    [
        "Overnight viewers",
        "Additional viewers after seven days",
        "Additional viewers on all devices"
    ];
}