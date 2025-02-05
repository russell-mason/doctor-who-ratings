namespace DoctorWhoRatings.Data.Charting.ShareByContent;

public class ShareByContentDataPoint
{
    public int Doctor { get; init; }

    public required string Actor { get; init; }

    public int EpisodeCount { get; init; }

    public decimal TotalEpisodeHours { get; init; }

    public decimal CalculatedPercentageShare { get; init; }
}
