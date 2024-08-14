namespace DoctorWhoRatings.Data.Charting.ByEra;

public class ByEraDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IByEraDataPointGenerator
{
    public List<ByEraDataPoint> Generate(ByEraDataOptions options)
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .GroupBy(episode => episode.EraDescription)
                                     .Select(group => CreateDataPoint(group, options))
                                     .ToList();

        return dataPoints;
    }

    private static ByEraDataPoint CreateDataPoint(IGrouping<string, Episode> group, ByEraDataOptions options)
    {
        var calculatedOvernightRatings = CalculatedRatings(group, options, ByEraOverCalculationMethod.Overnight);
        var calculatedConsolidatedRatings = CalculatedRatings(group, options, ByEraOverCalculationMethod.Consolidated);
        var calculatedExtendedRatings = CalculatedRatings(group, options, ByEraOverCalculationMethod.Extended);

        var calculatedRatings = options.OverCalculationMethod switch
        {
            ByEraOverCalculationMethod.Overnight => calculatedOvernightRatings,
            ByEraOverCalculationMethod.Consolidated => calculatedConsolidatedRatings,
            ByEraOverCalculationMethod.Extended => calculatedExtendedRatings,
            _ => throw new InvalidOperationException(nameof(options.OverCalculationMethod))
        };

        // Normally we can use a null result to naturally eliminate values used by the tooltip, but because
        // there's a mix of those with, and without, the consolidated/extended values, we need to manually
        // determine when this happens and matching values can be nullified
        decimal? useCalculatedConsolidatedRatings = (calculatedConsolidatedRatings == calculatedOvernightRatings) 
            ? null : calculatedConsolidatedRatings;
        
        decimal? useCalculatedExtendedRatings = (calculatedExtendedRatings == calculatedConsolidatedRatings) 
            ? null : calculatedExtendedRatings;

        var dataPoint = new ByEraDataPoint
                        {
                            Era = group.Key,
                            EpisodeCount = group.Count(),
                            CalculatedOvernightRatings = calculatedOvernightRatings,
                            CalculatedConsolidatedRatings = useCalculatedConsolidatedRatings,
                            CalculatedExtendedRatings = useCalculatedExtendedRatings,
                            CalculatedRatings = calculatedRatings
                        };

        return dataPoint;
    }

    private static decimal CalculatedRatings(IGrouping<string, Episode> group, 
                                             ByEraDataOptions options,
                                             ByEraOverCalculationMethod method)
    {
        var calculatedOvernightRatings = options.CalculationMethod switch
        {
            ByEraCalculationMethod.Sum => SumRatings(group, method),
            ByEraCalculationMethod.MeanPerEpisode => MeanPerEpisode(group, method),
            ByEraCalculationMethod.MeanPerHour => MeanPerHour(group, method),
            _ => throw new InvalidOperationException(nameof(options.CalculationMethod))
        };

        return calculatedOvernightRatings;
    }

    private static decimal SumRatings(IGrouping<string, Episode> group, ByEraOverCalculationMethod method) =>
        group.Sum(episode => SelectRating(episode, method));

    private static decimal MeanPerEpisode(IGrouping<string, Episode> group, ByEraOverCalculationMethod method) =>
        group.Average(episode => SelectRating(episode, method));

    private static decimal MeanPerHour(IGrouping<string, Episode> group, ByEraOverCalculationMethod method)
    {
        const decimal minutesInHour = 60m;  // Force decimal division

        var totalRatings = group.Sum(episode => SelectRating(episode, method) * (episode.Runtime / minutesInHour));
        var totalHours = group.Sum(episode => episode.Runtime / minutesInHour);

        return totalRatings / totalHours;
    }

    private static decimal SelectRating(Episode episode, ByEraOverCalculationMethod method) =>
        method switch
        {
            ByEraOverCalculationMethod.Overnight => episode.OvernightRatings ?? 0,
            ByEraOverCalculationMethod.Consolidated => episode.ConsolidatedRatings ?? episode.OvernightRatings ?? 0,
            ByEraOverCalculationMethod.Extended => episode.ExtendedRatings ?? episode.ConsolidatedRatings ?? episode.OvernightRatings ?? 0,
            _ => throw new InvalidOperationException(nameof(method))
        };
}
