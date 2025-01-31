namespace DoctorWhoRatings.Data.Charting.Episodes;

public class TrendDataPoint : EpisodeDataPoint
{
    public static TrendDataPoint Create(int id, decimal value) => CreateTrendDataPoint(id, value);

    public static TrendDataPoint Create(int id) => CreateTrendDataPoint(id, null);

    public static List<TrendDataPoint> From(Trendline trendline) =>
    [
        Create((int) trendline.StartX, trendline.StartY),
        Create((int) trendline.EndX, trendline.EndY)
    ];

    // EpisodeDataPoint uses init fields so this can't use a constructor
    private static TrendDataPoint CreateTrendDataPoint(int id, decimal? value) =>
        new TrendDataPoint
        {
            // The TrendDataPoint needs to mimic the EpisodeDataPoint because the chart requires the same data point type for
            // all series. Only requires the matching first/last episode IDs and overnights to create the line 
            Id = id,
            OvernightRatings = value,

            // All other properties are for normal required values
            EraDescription = string.Empty,
            Doctor = 0,
            Actor = string.Empty,
            SeasonFormatDescription = string.Empty,
            SeasonDescription = string.Empty,
            StoryTitle = string.Empty,
            PartInStory = 0,
            FullTitle = string.Empty,
            Runtime = 0,
            Population = 0
        };
}