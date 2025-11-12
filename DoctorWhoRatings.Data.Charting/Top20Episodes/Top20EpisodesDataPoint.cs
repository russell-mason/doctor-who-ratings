namespace DoctorWhoRatings.Data.Charting.Top20Episodes;

public class Top20EpisodesDataPoint
{
    public int Id { get; init; }

    public required string Actor { get; init; }

    public int? Season { get; init; }

    public required string SeasonFormatDescription { get; init; }

    public required string StoryTitle { get; init; }

    public string? PartTitle { get; init; }

    public string? Slug { get; init; }

    public DateTime OriginalAirDate { get; init; }

    public decimal? OvernightRatings { get; init; }

    public decimal? ConsolidatedRatings { get; init; }

    public decimal? ConsolidatedExcessRatings { get; init; }

    public decimal? ExtendedRatings { get; init; }

    public decimal? ExtendedExcessRatings { get; init; }

    public decimal? PopulationAdjustedOvernightRatings { get; init; }

    public decimal? PopulationAdjustedConsolidatedRatings { get; init; }

    public decimal? PopulationAdjustedExtendedRatings { get; init; }

    public decimal? PopulationAdjustedConsolidatedExcessRatings { get; init; }

    public decimal? PopulationAdjustedExtendedExcessRatings { get; init; }

    public decimal? CalculatedOvernightRatings { get; init; }

    public decimal? CalculatedConsolidatedRatings { get; init; }

    public decimal? CalculatedConsolidatedExcessRatings { get; init; }

    public decimal? CalculatedExtendedRatings { get; init; }

    public decimal? CalculatedExtendedExcessRatings { get; init; }
}
