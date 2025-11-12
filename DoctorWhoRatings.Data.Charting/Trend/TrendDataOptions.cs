namespace DoctorWhoRatings.Data.Charting.Trend;

public class TrendDataOptions
{
    public TrendRange Range { get; set; } = TrendRange.None;

    public bool Sequential { get; set; } = false;
}
