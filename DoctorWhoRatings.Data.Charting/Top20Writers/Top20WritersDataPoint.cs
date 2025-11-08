namespace DoctorWhoRatings.Data.Charting.Top20Writers;

public class Top20WritersDataPoint
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public string?[] Aliases { get; init; } = [];

    public int EpisodeCount { get; init; }

    public decimal TotalEpisodeHours { get; init; }

    public decimal? TotalOvernightHoursWatched { get; init; }

    public decimal? TotalConsolidatedHoursWatched { get; init; }

    public decimal? TotalConsolidatedExcessHoursWatched { get; init; }

    public decimal? TotalExtendedHoursWatched { get; init; }

    public decimal? TotalExtendedExcessHoursWatched { get; init; }

    public decimal? CalculatedByOptions { get; init; }

    public decimal HighestOvernightRating { get; init; }

    public decimal LowestOvernightRating { get; init; }
}
