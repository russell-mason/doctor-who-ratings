namespace DoctorWhoRatings.Data.Charting.ShareByContent;

public class ShareByContentDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IShareByContentDataPointGenerator
{
    public List<ShareByContentDataPoint> Generate(ShareByContentDataOptions options) =>
        dataProvider.DoctorWhoData.Episodes
                    .GroupBy(episode => episode.Doctor)
                    .Select(group => CreateDataPoint(group, options))
                    .ToList();

    private ShareByContentDataPoint CreateDataPoint(IGrouping<int, Episode> group, ShareByContentDataOptions options)
    {
        const int minutesInHour = 60;

        var totalEpisodeCount = dataProvider.DoctorWhoData.Episodes.Count; 
        var totalRuntimeMinutes = dataProvider.DoctorWhoData.Episodes.Sum(episode => episode.Runtime);

        var episodeCount = group.Count();
        var runtimeHours =  (decimal) group.Sum(episode => episode.Runtime) / minutesInHour;
        var totalRuntimeHours = (decimal) totalRuntimeMinutes / minutesInHour;

        var referenceEpisode = group.First();

        var dataPoint = new ShareByContentDataPoint
        {
            Doctor = group.Key,
            Actor = group.First().Actor,
            UnambiguousActor = referenceEpisode.DisambiguateActor(dataProvider.DoctorWhoData.Doctors),
            EpisodeCount = episodeCount,
            TotalEpisodeHours = runtimeHours,
            CalculatedPercentageShare = options.CalculationMethod switch
            {
                ShareByContentCalculationMethod.Episodes => ((decimal) episodeCount/ totalEpisodeCount) * 100,

                ShareByContentCalculationMethod.Hours => (runtimeHours / totalRuntimeHours) * 100,
                
                _ => throw new ArgumentOutOfRangeException(nameof(options))
            }
        };

        return dataPoint;
    }
}
