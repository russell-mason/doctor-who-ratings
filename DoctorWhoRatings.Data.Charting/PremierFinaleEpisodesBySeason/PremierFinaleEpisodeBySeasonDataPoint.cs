namespace DoctorWhoRatings.Data.Charting.PremierFinaleEpisodesBySeason;

public class PremierFinaleEpisodeBySeasonDataPoint
{
    public bool IsPremier { get; init; }

    public required string EpisodePosition { get; init; }

    public int EpisodeCount { get; init; }

    public required string Actor { get; init; }

    public int? Season { get; init; }

    public required string SeasonFormatDescription { get; init; }

    public required string SeasonDescription { get; init; }

    public required string StoryTitle { get; init; }

    public string? StoryPartTitle { get; init; }

    public string? Slug { get; init; }

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

    public decimal? CalculatedRatings { get; init; }
}
