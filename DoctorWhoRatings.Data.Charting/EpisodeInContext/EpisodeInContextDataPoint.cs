namespace DoctorWhoRatings.Data.Charting.EpisodeInContext;

public class EpisodeInContextDataPoint
{
    public required EpisodeDataPoint Episode { get; init; }

    public EpisodeContext? InStoryContext { get; init; }

    public EpisodeContext? InSeasonContext { get; init; }

    public EpisodeContext? InDoctorContext { get; init; }

    public required EpisodeContext InFullContext { get; init; }
}