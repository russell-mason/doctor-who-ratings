namespace DoctorWhoRatings.Data.Charting.Timeline;

public class TimelineDataPoint
{
    public int Number { get; init; }

    public DateTime FirstEpisodeAirDate { get; init; }

    public DateTime? LastEpisodeAirDate { get; init; }

    public required string Actor { get; init; }

    public int EpisodeCount { get; init; }
}
