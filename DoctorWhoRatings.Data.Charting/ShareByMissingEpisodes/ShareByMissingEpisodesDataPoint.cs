namespace DoctorWhoRatings.Data.Charting.ShareByMissingEpisodes;

public class ShareByMissingEpisodesDataPoint
{
    public required string Description { get; init; }

    public int TotalEpisodeCount { get; init; }

    public int ContextEpisodeCount { get; init; }

    public int EpisodeCount { get; init; }

    public decimal CalculatedTotalPercentageShare { get; init; }

    public decimal CalculatedContextPercentageShare { get; init; }
}
