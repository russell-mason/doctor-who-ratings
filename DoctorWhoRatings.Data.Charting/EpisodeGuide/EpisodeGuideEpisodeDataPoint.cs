namespace DoctorWhoRatings.Data.Charting.EpisodeGuide;

public class EpisodeGuideEpisodeDataPoint
{
    public int? EpisodeFormatId { get; init; }

    public required int EpisodeNumber { get; init; }

    public int PartInStory { get; init; }

    public string? PartTitle { get; init; }

    public DateTime OriginalAirDate { get; init; }

    public decimal? OvernightRatings { get; init; }

    public decimal? ConsolidatedRatings { get; init; }

    public decimal? ExtendedRatings { get; init; }

    public string? WikiUrl { get; init; }

    public string? Slug { get; init; }
}