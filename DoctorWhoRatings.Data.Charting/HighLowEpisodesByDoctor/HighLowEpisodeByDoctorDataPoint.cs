namespace DoctorWhoRatings.Data.Charting.HighLowEpisodesByDoctor;

public class HighLowEpisodeByDoctorDataPoint
{
    public int? Season { get; init; }

    public required string SeasonFormatDescription { get; init; }

    public required string StoryTitle { get; set; }

    public string? StoryPartTitle { get; set; }

    public string? Slug { get; init; }

    public decimal? OvernightRatings { get; set; }

    public decimal? ConsolidatedRatings { get; set; }

    public decimal? ConsolidatedExcessRatings { get; init; }

    public decimal? ExtendedRatings { get; set; }

    public decimal? ExtendedExcessRatings { get; set; }

    public decimal? PopulationAdjustedOvernightRatings { get; init; }

    public decimal? PopulationAdjustedConsolidatedRatings { get; init; }

    public decimal? PopulationAdjustedExtendedRatings { get; init; }

    public decimal? PopulationAdjustedConsolidatedExcessRatings { get; init; }

    public decimal? PopulationAdjustedExtendedExcessRatings { get; init; }

    public decimal? CalculatedOvernightRatings { get; set; }

    public decimal? CalculatedConsolidatedRatings { get; set; }

    public decimal? CalculatedConsolidatedExcessRatings { get; init; }

    public decimal? CalculatedExtendedRatings { get; set; }

    public decimal? CalculatedExtendedExcessRatings { get; init; }
}
