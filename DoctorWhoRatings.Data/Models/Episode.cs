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

    public string Actor { get; init; } = string.Empty;

    public int? Season { get; init; }

    public int SeasonFormatId { get; init; }

    public string SeasonFormatDescription { get; init; } = string.Empty;

    public int Story { get; init; }

    public int? StoryInSeason { get; init; }

    public string StoryTitle { get; init; } = string.Empty;

    public int PartInStory { get; init; }

    public string? PartTitle { get; init; }

    public DateTime OriginalAirDate { get; init; }

    /// <summary>
    /// Overnight viewers (in millions).
    /// </summary>
    public decimal? OvernightRatings { get; init; }

    /// <summary>
    /// Viewers after seven days (in millions).
    /// </summary>
    public decimal? ConsolidatedRatings { get; init; }

    /// <summary>
    /// Viewers after seven days on all devices (in millions).
    /// </summary>
    public decimal? ExtendedRatings { get; init; }

    /// <summary>
    /// Adjusted relative to the UK population at the time of airing and the current population.
    /// </summary>
    public decimal? PopulationAdjustedOvernightRatings { get; init; }

    /// <summary>
    /// Adjusted relative to the UK population at the time of airing and the current population.
    /// </summary>
    public decimal? PopulationAdjustedConsolidatedRatings { get; init; }

    /// <summary>
    /// Adjusted relative to the UK population at the time of airing and the current population.
    /// </summary>
    public decimal? PopulationAdjustedExtendedRatings { get; init; }

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

