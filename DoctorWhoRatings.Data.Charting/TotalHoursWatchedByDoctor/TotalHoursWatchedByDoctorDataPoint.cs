namespace DoctorWhoRatings.Data.Charting.TotalHoursWatchedByDoctor;

public class TotalHoursWatchedByDoctorDataPoint
{
    public required string Actor { get; init; }

    public required string UnambiguousActor { get; init; }

    public int EpisodeCount { get; init; }

    public decimal TotalEpisodeHours { get; init; }

    public decimal? TotalOvernightHoursWatched { get; init; }

    public decimal? TotalConsolidatedHoursWatched { get; init; }

    public decimal? TotalExtendedHoursWatched { get; init; }

    public decimal CalculatedTotalHoursWatched { get; init; }
}
