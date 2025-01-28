namespace DoctorWhoRatings.Data.Charting.EpisodeGuide;

public class EpisodeGuideDataPoint
{
    public int? EpisodeFormatId { get; init; }

    public int? Season { get; init; }

    public required string SeasonFormatDescription { get; init; }

    public required int EpisodeNumber { get; init; }

    public int Story { get; init; }

    public required string StoryTitle { get; init; }

    public int PartInStory { get; init; }

    public string? PartTitle { get; init; }

    public DateTime OriginalAirDate { get; init; }

    public decimal? OvernightRatings { get; init; }

    public decimal? ConsolidatedRatings { get; init; }

    public decimal? ExtendedRatings { get; init; }

    public string? WikiUrl { get; init; }

    public string? Slug { get; init; }

    public required EpisodeGuideDataPointMetadata Metadata { get; set; }
}