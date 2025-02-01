namespace DoctorWhoRatings.Data.Charting.AverageBySeason;

public class AverageBySeasonDataPoint
{
    public int Id { get; init; }

    public required string Actor { get; init; }

    public int Season { get; init; }

    public required string SeasonFormatDescription { get; init; }

    public DateTime OriginalAirDate { get; init; }

    public int EpisodeCount { get; init; }

    public decimal? CalculatedOvernightRatings { get; init; }

    public decimal? CalculatedConsolidatedRatings { get; init; }

    public decimal? CalculatedExtendedRatings { get; init; }

    public decimal? CalculatedConsolidatedExcessRatings { get; init; }

    public decimal? CalculatedExtendedExcessRatings { get; init; }

    public decimal? CalculatedPopulationAdjustedOvernightRatings { get; init; }

    public decimal? CalculatedPopulationAdjustedConsolidatedRatings { get; init; }

    public decimal? CalculatedPopulationAdjustedExtendedRatings { get; init; }

    public decimal? CalculatedPopulationAdjustedConsolidatedExcessRatings { get; init; }

    public decimal? CalculatedPopulationAdjustedExtendedExcessRatings { get; init; }
}
