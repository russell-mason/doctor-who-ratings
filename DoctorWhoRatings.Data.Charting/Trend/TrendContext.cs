namespace DoctorWhoRatings.Data.Charting.Trend;

public class TrendContext
{
    public string? EraDescription { get; set; } = string.Empty;

    public string? Actor { get; set; } = string.Empty;

    public int? Season { get; set; }

    public string? SeasonFormatDescription { get; set; } = string.Empty;
}