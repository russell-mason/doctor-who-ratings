namespace DoctorWhoRatings.Data.Charting.EpisodeGuide;

public class EpisodeGuideDataPointMetadata
{
    public bool ShowSeasonTitle { get; init; }

    public string? SeasonTitle { get; init; }

    public bool ShowStoryTitle { get; init; }

    public bool ShowStoryNumber { get; init; }

    public bool HasStoryWiki { get; init; }

    public required string EpisodeTitle { get; init; }

    public bool HasEpisodeWiki { get; init; }

    public required string EpisodeIndentIndicator { get; init; }

    public required string ConsecutiveSeasonStoryIndicator { get; init; }
}