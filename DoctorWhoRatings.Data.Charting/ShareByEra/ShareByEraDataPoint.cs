namespace DoctorWhoRatings.Data.Charting.ShareByEra;

public class ShareByEraDataPoint
{
    public required string Era { get; init; }

    public int EpisodeCount { get; init; }

    public decimal? CalculatedOvernightRatings { get; set; }

    public decimal? CalculatedConsolidatedRatings { get; set; }
    
    public decimal? CalculatedExtendedRatings { get; set; }

    public decimal CalculatedRatings { get; set; }
}
