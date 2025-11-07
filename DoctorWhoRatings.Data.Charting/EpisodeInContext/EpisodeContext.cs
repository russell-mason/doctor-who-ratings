namespace DoctorWhoRatings.Data.Charting.EpisodeInContext;

public class EpisodeContext
{
    public bool IsSpecial { get; set; }

    public int PositionFromTop { get; set; }

    public int PositionFromBottom { get; set; }

    public decimal? RelativeToHighestRatings { get; init; }

    public decimal? RelativeToAverageRatings { get; init; }

    public decimal? RelativeToLowestRatings { get; init; }

    public int EpisodeCount { get; init; }

    public int MissingEpisodeCount { get; init; }

    public int StoryCount { get; init; }

    public int EpisodeIndex { get; set; }

    public required List<EpisodeDataPoint> Episodes { get; init; }
}