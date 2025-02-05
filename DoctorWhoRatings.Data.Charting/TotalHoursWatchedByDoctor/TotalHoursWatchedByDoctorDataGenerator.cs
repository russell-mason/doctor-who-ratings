namespace DoctorWhoRatings.Data.Charting.TotalHoursWatchedByDoctor;

public class TotalHoursWatchedByDoctorDataPointGenerator(IDoctorWhoDataProvider dataProvider) : ITotalHoursWatchedByDoctorDataPointGenerator
{
    public List<TotalHoursWatchedByDoctorDataPoint> Generate(TotalHoursWatchedByDoctorDataOptions options)
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .GroupBy(episode => episode.Doctor)
                                     .Select(group => CreateDataPoint(group, options))
                                     .ToList();

        return dataPoints;
    }

    private TotalHoursWatchedByDoctorDataPoint CreateDataPoint(IGrouping<int, Episode> group, 
                                                               TotalHoursWatchedByDoctorDataOptions options)
    {
        const int minutesInHour = 60;

        var totalEpisodeMinutes = group.Sum(episode => episode.Runtime);
        var totalOvernightMinutes = group.SumOrDefault(episode => episode.OvernightRatings * episode.Runtime);
        var totalConsolidatedMinutes = group.SumOrDefault(episode => episode.ConsolidatedRatings * episode.Runtime);
        var totalExtendedMinutes = group.SumOrDefault(episode => episode.ExtendedRatings * episode.Runtime);

        var calculatedTotalMinutesWatched = options.CalculationMethod switch
        {
            TotalHoursWatchedByDoctorCalculationMethod.Overnight => totalOvernightMinutes,
            TotalHoursWatchedByDoctorCalculationMethod.Consolidated => totalConsolidatedMinutes ?? totalOvernightMinutes,
            TotalHoursWatchedByDoctorCalculationMethod.Extended => totalExtendedMinutes ?? totalConsolidatedMinutes ?? totalOvernightMinutes,
            _ => throw new InvalidOperationException(nameof(options.CalculationMethod))
        };

        var referenceEpisode = group.First();

        var dataPoint = new TotalHoursWatchedByDoctorDataPoint
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
