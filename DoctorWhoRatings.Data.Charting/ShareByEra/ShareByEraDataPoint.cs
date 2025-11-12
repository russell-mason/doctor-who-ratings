namespace DoctorWhoRatings.Data.Charting.ShareByEra;

public class ShareByEraDataPoint
{
    public required string Era { get; init; }

    public int EpisodeCount { get; init; }

    public decimal? CalculatedOvernightRatings { get; init; }

    public decimal? CalculatedConsolidatedRatings { get; init; }
    
    public decimal? CalculatedExtendedRatings { get; init; }

    public decimal CalculatedRatings { get; init; }
}
