namespace DoctorWhoRatings.Data.Models;

/// <summary>
/// Represents a single Doctor Who episode, special, or movie, denormalized to present a flat structure.
/// </summary>
public record Episode
{
    /// <summary>
    /// Unofficial episode identifier. Matches the order in which the episodes were aired.
    /// </summary>
    public int Id { get; init; }

    public int? EpisodeFormatId { get; init; }

    public string? EpisodeFormatDescription { get; init; }

    public int Doctor { get; init; }

    public required string Actor { get; init; }

    public int? Season { get; init; }

    public int SeasonFormatId { get; init; }

    public required string SeasonFormatDescription { get; init; }

    public int Story { get; init; }

    public int? StoryInSeason { get; init; }

    public required string StoryTitle { get; init; }

    public int PartInStory { get; init; }

    public string? PartTitle { get; init; }

    public DateTime OriginalAirDate { get; init; }

    public int Runtime { get; init; }

    /// <summary>
    /// Overnight viewers (in millions).
    /// </summary>
    public decimal? OvernightRatings { get; init; }

    /// <summary>
    /// Viewers after seven days (in millions).
    /// </summary>
    public decimal? ConsolidatedRatings { get; init; }

    /// <summary>
    /// Difference between the consolidated figure and the overnight figure (in millions).
    /// </summary>
    public decimal? ConsolidatedExcessRatings => ConsolidatedRatings - OvernightRatings;

    /// <summary>
    /// Viewers after seven days on all devices (in millions).
    /// </summary>
    public decimal? ExtendedRatings { get; init; }

    /// <summary>
    /// Difference between the extended figure and the consolidated figure (in millions).
    /// </summary>
    public decimal? ExtendedExcessRatings => ExtendedRatings - (ConsolidatedRatings ?? OvernightRatings);

    /// <summary>
    /// Adjusted relative to the UK population at the time of airing and the current population.
    /// </summary>
    public decimal? PopulationAdjustedOvernightRatings => OvernightRatings * PopulationFactor;

    /// <summary>
    /// Adjusted relative to the UK population at the time of airing and the current population.
    /// </summary>
    public decimal? PopulationAdjustedConsolidatedRatings => ConsolidatedRatings * PopulationFactor;

    /// <summary>
    /// Adjusted relative to the UK population at the time of airing and the current population.
    /// </summary>
    public decimal? PopulationAdjustedExtendedRatings => ExtendedRatings * PopulationFactor;

    /// <summary>
    /// Adjusted relative to the UK population at the time of airing and the current population.
    /// </summary>
    public decimal? PopulationAdjustedConsolidatedExcessRatings => PopulationAdjustedConsolidatedRatings - PopulationAdjustedOvernightRatings;

    /// <summary>
    /// Adjusted relative to the UK population at the time of airing and the current population.
    /// </summary>
    public decimal? PopulationAdjustedExtendedExcessRatings => PopulationAdjustedExtendedRatings - (PopulationAdjustedConsolidatedRatings ?? PopulationAdjustedOvernightRatings);

    /// <summary>
    /// UK population at the time of airing.
    /// </summary>
    public int Population { get; init; }

    /// <summary>
    /// Factor relative to the current UK population.
    /// </summary>
    public decimal PopulationFactor { get; init; }

    public string? Note { get; init; }
}

