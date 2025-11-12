namespace DoctorWhoRatings.Data.Charting.ShareByHoursWatched;

public class ShareByHoursWatchedDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IShareByHoursWatchedDataPointGenerator
{
    public List<ShareByHoursWatchedDataPoint> Generate(ShareByHoursWatchedDataOptions options) => dataProvider.DoctorWhoData.Episodes
        .GroupBy(episode => episode.Doctor)
        .Select(group => CreateDataPoint(group, options))
        .ToList();

    private ShareByHoursWatchedDataPoint CreateDataPoint(IGrouping<int, Episode> group,
                                                         ShareByHoursWatchedDataOptions options)
    {
        const int minutesInHour = 60;

        var totalEpisodeMinutes = group.Sum(episode => episode.Runtime);
        var totalOvernightMinutes = group.SumOrDefault(episode => episode.OvernightRatings * episode.Runtime);
        var totalConsolidatedMinutes = group.SumOrDefault(episode => episode.ConsolidatedRatings * episode.Runtime);
        var totalExtendedMinutes = group.SumOrDefault(episode => episode.ExtendedRatings * episode.Runtime);

        var calculatedTotalMinutesWatched = options.CalculationMethod switch
        {
            ShareByHoursWatchedCalculationMethod.Overnight => totalOvernightMinutes,

            ShareByHoursWatchedCalculationMethod.Consolidated => totalConsolidatedMinutes ?? totalOvernightMinutes,

            ShareByHoursWatchedCalculationMethod.Extended => totalExtendedMinutes ?? totalConsolidatedMinutes ?? totalOvernightMinutes,

            _ => throw new ArgumentOutOfRangeException(nameof(options.CalculationMethod))
        };

        var referenceEpisode = group.First();

        var dataPoint = new ShareByHoursWatchedDataPoint
                        {
                            Actor = referenceEpisode.Actor,
                            UnambiguousActor = referenceEpisode.DisambiguateActor(dataProvider.DoctorWhoData.Doctors),
                            EpisodeCount = group.Count(),
                            TotalEpisodeHours = (decimal) totalEpisodeMinutes / minutesInHour,
                            TotalOvernightHoursWatched = totalOvernightMinutes / minutesInHour,
                            TotalConsolidatedHoursWatched = totalConsolidatedMinutes / minutesInHour,
                            TotalExtendedHoursWatched = totalExtendedMinutes / minutesInHour,
                            CalculatedTotalHoursWatched = (calculatedTotalMinutesWatched ?? 0) / minutesInHour
                        };

        return dataPoint;
    }
}
