namespace DoctorWhoRatings.Data.Charting.Trend;

public class TrendDataPointGenerator : ITrendDataPointGenerator
{
    public List<TrendDataPoint> Generate(List<EpisodeDataPoint> dataPoints, TrendDataOptions trendOptions)
    {
        var groupedByRange = GroupByRange(dataPoints, trendOptions);
        var trendLines = groupedByRange.SelectMany(CreateTrendDataPoints).ToList();

        return trendLines;
    }

    public (List<TrendDataPoint?> alternating1, List<TrendDataPoint?> alternating2) 
        GenerateAlternating(List<EpisodeDataPoint> dataPoints, TrendDataOptions trendOptions)
    {
        var fullList = Generate(dataPoints, trendOptions);

        return (AlternatePairs(fullList, 0), AlternatePairs(fullList, 2));
    }

    private static List<TrendDataPoint?> AlternatePairs(List<TrendDataPoint> source, int startIndex)
    {
        var alternates = new List<TrendDataPoint?>();

        for (var index = 0; index < source.Count; index++)
        {
            if (index % 4 == startIndex || index % 4 == startIndex + 1)
            {
                alternates.Add(source[index]);
            }
            else
            {
                alternates.Add(TrendDataPoint.Create(source[index].Id));
            }
        }

        return alternates;
    }

    private static IEnumerable<IGrouping<string, EpisodeDataPoint>> GroupByRange(List<EpisodeDataPoint> dataPoints, TrendDataOptions trendOptions) =>
        trendOptions.Range switch
        {
            TrendRange.Season => dataPoints.Where(dataPoint => dataPoint.Season != null)
                                           .GroupBy(dataPoint => $"{dataPoint.Doctor}{dataPoint.Season}")
                                           .Where(group => group.Count() > 1),

            TrendRange.Doctor => dataPoints.GroupBy(dataPoint => dataPoint.Doctor.ToString()).Where(group => group.Count() > 1),

            TrendRange.Era => dataPoints.GroupBy(dataPoint => dataPoint.EraDescription).Where(group => group.Count() > 1),

            TrendRange.All => dataPoints.GroupBy(_ => "All"),

            _ => []
        };

    private static List<TrendDataPoint> CreateTrendDataPoints(IGrouping<string, EpisodeDataPoint> group)
    {
        var points = group.Select(dataPoint => ((decimal) dataPoint.Id, dataPoint.OvernightRatings ?? 1)).ToList();

        return TrendDataPoint.From(new Trendline(points));
    }
}
