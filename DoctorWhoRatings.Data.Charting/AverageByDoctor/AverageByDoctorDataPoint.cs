namespace DoctorWhoRatings.Data.Charting.AverageByDoctor;

public class AverageByDoctorDataPoint
{
    public required string Actor { get; init; }

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
