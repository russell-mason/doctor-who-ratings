namespace DoctorWhoRatings.Data.Charting.ByEra;

public class ByEraDataPoint
{
    public required string Era { get; init; }

    public int EpisodeCount { get; init; }

    public decimal? CalculatedOvernightRatings { get; set; }

    public decimal? CalculatedConsolidatedRatings { get; set; }
    
    public decimal? CalculatedExtendedRatings { get; set; }

    public decimal CalculatedRatings { get; set; }
}
