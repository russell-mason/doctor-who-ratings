namespace DoctorWhoRatings.Data.Charting.Trend;

public class TrendDataPoint : EpisodeDataPoint
{
    public static TrendDataPoint Create(int id) => CreateTrendDataPoint(id, null);

    public static List<TrendDataPoint> From(Trendline trendline, TrendContext context) =>
    [
        CreateTrendDataPoint((int) trendline.StartX, trendline.StartY, context),
        CreateTrendDataPoint((int) trendline.EndX, trendline.EndY, context)
    ];

    // EpisodeDataPoint uses init fields so this can't use a constructor
    private static TrendDataPoint CreateTrendDataPoint(int id, decimal? value, TrendContext? context = null) =>
        new TrendDataPoint
        {
            // The TrendDataPoint needs to mimic the EpisodeDataPoint because the chart requires the same data point type for
            // all series. Only requires the matching first/last episode IDs and overnights to create the line 
            Id = id,
            OvernightRatings = value,

            // All other properties are for normal required values
            EraDescription = context?.EraDescription ?? string.Empty,
            Doctor = 0,
            Actor = context?.Actor ?? string.Empty,
            Season = context?.Season,
            SeasonFormatDescription = context?.SeasonFormatDescription ?? string.Empty,
            SeasonDescription = string.Empty,
            StoryTitle = string.Empty,
            PartInStory = 0,
            FullTitle = string.Empty,
            Runtime = 0,
            Population = 0
        };
}