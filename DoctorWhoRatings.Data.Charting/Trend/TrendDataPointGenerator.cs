namespace DoctorWhoRatings.Data.Charting.Trend;

public class TrendDataPointGenerator : ITrendDataPointGenerator
{
    public List<TrendDataPoint> Generate(List<EpisodeDataPoint> dataPoints, TrendDataOptions trendOptions)
    {
        var groupedByRange = GroupByRange(dataPoints, trendOptions);

        var trendLineDataPoints = groupedByRange
                .SelectMany(group => CreateTrendDataPoints(group, CreateContext(group.First(), trendOptions)))
                .ToList();

        if (trendOptions.Sequential)
        {
            Sequence(trendLineDataPoints);
        }

        return trendLineDataPoints;
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
                alternates.Add(TrendDataPoint.Create(source[index].Id, source[index].Index));
            }
        }

        return alternates;
    }

    private static TrendContext CreateContext(EpisodeDataPoint dataPoint, TrendDataOptions trendOptions) =>
        trendOptions.Range switch
        {
            TrendRange.Season => new TrendContext
            {
                Actor = dataPoint.Actor,
                Season = dataPoint.Season,
                SeasonFormatDescription = dataPoint.SeasonFormatDescription
            },

            TrendRange.Doctor => new TrendContext
            {
                Actor = dataPoint.Actor,
            },

            TrendRange.Era => new TrendContext
            {
                EraDescription = dataPoint.EraDescription
            },

            TrendRange.All => new TrendContext
            {
                EraDescription = "All"
            },

            _ => new TrendContext()
        };

    private static IEnumerable<IGrouping<string, EpisodeDataPoint>> GroupByRange(List<EpisodeDataPoint> dataPoints, TrendDataOptions trendOptions) =>
        trendOptions.Range switch
        {
            TrendRange.Season => dataPoints.Where(dataPoint => dataPoint.Season != null)
                                           .GroupBy(dataPoint => $"#{dataPoint.Doctor}#{dataPoint.Season}"),

            TrendRange.Doctor => dataPoints.GroupBy(dataPoint => dataPoint.Doctor.ToString()),

            TrendRange.Era => dataPoints.GroupBy(dataPoint => dataPoint.EraDescription),

            TrendRange.All => dataPoints.GroupBy(_ => "All"),

            _ => []
        };

    private static List<TrendDataPoint> CreateTrendDataPoints(IGrouping<string, EpisodeDataPoint> group, TrendContext context)
    {
        var points = group.Select(dataPoint => ((decimal) dataPoint.Id, dataPoint.OvernightRatings ?? 1)).ToList();

        return TrendDataPoint.From(new Trendline(points), context);
    }

    private static void Sequence(List<TrendDataPoint> trendLineDataPoints)
    {
        var counter = 1;

        for (var index = 0; index < trendLineDataPoints.Count; index += 2)
        {
            trendLineDataPoints[index].SetIndex(counter);
            trendLineDataPoints[index + 1].SetIndex(counter + 1);

            counter++;
        }
    }
}
