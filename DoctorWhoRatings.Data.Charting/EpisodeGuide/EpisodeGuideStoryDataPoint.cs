namespace DoctorWhoRatings.Data.Charting.EpisodeGuide;

public class EpisodeGuideStoryDataPoint
{
    public int Story { get; init; }

    public int? StoryInSeason { get; init; }

    public string? StoryTitle { get; init; }

    public string? WikiUrl { get; init; }

    public required List<EpisodeGuideEpisodeDataPoint> Episodes { get; init; }
}