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

    public required string StoryTitle { get; set; }

    public string? StoryPartTitle { get; set; }

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

    public decimal? CalculatedRatings { get; set; }
}
