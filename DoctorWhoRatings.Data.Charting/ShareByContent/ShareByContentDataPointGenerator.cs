namespace DoctorWhoRatings.Data.Charting.ShareByContent;

public class ShareByContentDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IShareByContentDataPointGenerator
{
    public List<ShareByContentDataPoint> Generate(ShareByContentDataOptions options)
    {
        var totals = (dataProvider.DoctorWhoData.Episodes.Count,
                      dataProvider.DoctorWhoData.Episodes.Sum(episode => episode.Runtime));

        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .GroupBy(episode => episode.Doctor)
                                     .Select(group => CreateDataPoint(group, totals, options))
                                     .ToList();

        return dataPoints;
    }

    private static ShareByContentDataPoint CreateDataPoint(IGrouping<int, Episode> group, 
                                                           (int totalEpisodeCount, int totalRuntimeMinutes) totals,
                                                           ShareByContentDataOptions options)
    {
        const int minutesInHour = 60;

        var episodeCount = group.Count();
        var runtimeHours =  (decimal) group.Sum(episode => episode.Runtime) / minutesInHour;
        var totalRuntimeHours = (decimal) totals.totalRuntimeMinutes / minutesInHour;

        var dataPoint = new ShareByContentDataPoint
        {
            Doctor = group.Key,
            Actor = group.First().Actor,
            EpisodeCount = episodeCount,
            TotalEpisodeHours = runtimeHours,
            CalculatedPercentageShare = options.CalculationMethod switch
            {
                ShareByContentCalculationMethod.Episodes => ((decimal) episodeCount/ totals.totalEpisodeCount) * 100,
                ShareByContentCalculationMethod.Hours => (runtimeHours / totalRuntimeHours) * 100,
                _ => throw new InvalidOperationException(nameof(options.CalculationMethod))
            }
        };

        return dataPoint;
    }
}
