namespace DoctorWhoRatings.Data.Charting.ShareByEra;

public class ShareByEraDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IShareByEraDataPointGenerator
{
    public List<ShareByEraDataPoint> Generate(ShareByEraDataOptions options)
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .GroupBy(episode => episode.EraDescription)
                                     .Select(group => CreateDataPoint(group, options))
                                     .ToList();

        return dataPoints;
    }

    private static ShareByEraDataPoint CreateDataPoint(IGrouping<string, Episode> group, ShareByEraDataOptions options)
    {
        var calculatedOvernightRatings = CalculatedRatings(group, options, ShareByEraOverCalculationMethod.Overnight);
        var calculatedConsolidatedRatings = CalculatedRatings(group, options, ShareByEraOverCalculationMethod.Consolidated);
        var calculatedExtendedRatings = CalculatedRatings(group, options, ShareByEraOverCalculationMethod.Extended);

        var calculatedRatings = options.OverCalculationMethod switch
        {
            ShareByEraOverCalculationMethod.Overnight => calculatedOvernightRatings,
            ShareByEraOverCalculationMethod.Consolidated => calculatedConsolidatedRatings,
            ShareByEraOverCalculationMethod.Extended => calculatedExtendedRatings,
            _ => throw new InvalidOperationException(nameof(options.OverCalculationMethod))
        };

        // Normally we can use a null result to naturally eliminate values used by the tooltip, but because
        // there's a mix of those with, and without, the consolidated/extended values, we need to manually
        // determine when this happens and matching values can be nullified
        decimal? useCalculatedConsolidatedRatings = (calculatedConsolidatedRatings == calculatedOvernightRatings) 
            ? null : calculatedConsolidatedRatings;
        
        decimal? useCalculatedExtendedRatings = (calculatedExtendedRatings == calculatedConsolidatedRatings) 
            ? null : calculatedExtendedRatings;

        var dataPoint = new ShareByEraDataPoint
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
                                             ShareByEraDataOptions options,
                                             ShareByEraOverCalculationMethod method)
    {
        var calculatedOvernightRatings = options.CalculationMethod switch
        {
            ShareByEraCalculationMethod.Sum => SumRatings(group, method),
            ShareByEraCalculationMethod.MeanPerEpisode => MeanPerEpisode(group, method),
            ShareByEraCalculationMethod.MeanPerHour => MeanPerHour(group, method),
            _ => throw new InvalidOperationException(nameof(options.CalculationMethod))
        };

        return calculatedOvernightRatings;
    }

    private static decimal SumRatings(IGrouping<string, Episode> group, ShareByEraOverCalculationMethod method) =>
        group.Sum(episode => SelectRating(episode, method));

    private static decimal MeanPerEpisode(IGrouping<string, Episode> group, ShareByEraOverCalculationMethod method) =>
        group.Average(episode => SelectRating(episode, method));

    private static decimal MeanPerHour(IGrouping<string, Episode> group, ShareByEraOverCalculationMethod method)
    {
        const decimal minutesInHour = 60m;  // Force decimal division

        var totalRatings = group.Sum(episode => SelectRating(episode, method) * (episode.Runtime / minutesInHour));
        var totalHours = group.Sum(episode => episode.Runtime / minutesInHour);

        return totalRatings / totalHours;
    }

    private static decimal SelectRating(Episode episode, ShareByEraOverCalculationMethod method) =>
        method switch
        {
            ShareByEraOverCalculationMethod.Overnight => episode.OvernightRatings ?? 0,
            ShareByEraOverCalculationMethod.Consolidated => episode.ConsolidatedRatings ?? episode.OvernightRatings ?? 0,
            ShareByEraOverCalculationMethod.Extended => episode.ExtendedRatings ?? episode.ConsolidatedRatings ?? episode.OvernightRatings ?? 0,
            _ => throw new InvalidOperationException(nameof(method))
        };
}
