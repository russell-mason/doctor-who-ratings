namespace DoctorWhoRatings.Data.Charting.Episodes;

public class EpisodeDataPoint
{
    private int? _index;

    public int Index => _index ?? Id;

    public int Id { get; init; }

    public required string EraDescription { get; init; }

    public int Doctor { get; init; }

    public required string Actor { get; init; }

    public int? Season { get; init; }

    public required string SeasonFormatDescription { get; init; }

    public required string SeasonDescription { get; init; }

    public int Story { get; init; }

    public int? StoryInSeason { get; init; }

    public required string StoryTitle { get; init; }

    public int PartInStory { get; init; }

    public string? PartTitle { get; init; }

    public int? EpisodeInSeason { get; init; }

    public required string FullTitle { get; init; }

    public string? Slug { get; init; }

    public DateTime OriginalAirDate { get; init; }

    public int Runtime { get; init; }

    public bool IsMissing { get; init; }

    public string[] Writers { get; init; } = [];

    public string? WriterAlias { get; init; }

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

    public int Population { get; init; }

    public decimal? OvernightPercentageOfPopulation { get; init; }

    public decimal? ConsolidatedPercentageOfPopulation { get; init; }

    public decimal? ExtendedPercentageOfPopulation { get; init; }

    public string? Note { get; init; }

    public string? WikiUrl { get; init; }

    public void SetIndex(int index) => _index = index;
}