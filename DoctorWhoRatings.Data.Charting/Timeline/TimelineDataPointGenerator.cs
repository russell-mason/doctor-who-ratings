namespace DoctorWhoRatings.Data.Charting.Timeline;

public class TimelineDataPointGenerator(IDoctorWhoDataProvider dataProvider) : ITimelineDataPointGenerator
{
    public List<TimelineDataPoint> Generate()
    {
        var dataPoints = dataProvider.DoctorWhoData.Doctors
                                     .Select(doctor =>
                                     {
                                         var episodes = dataProvider.DoctorWhoData.Episodes
                                                                    .Where(episode => episode.Doctor == doctor.Number)
                                                                    .ToList();

                                         return CreateDataPoint(doctor, episodes);
                                     })
                                     .ToList();

        return dataPoints;
    }

    private static TimelineDataPoint CreateDataPoint(Doctor doctor, List<Episode> episodes)
    {
        var dataPoint = new TimelineDataPoint
        {
            Number = doctor.Number,
            Actor = doctor.Actor,
            FirstEpisodeAirDate = episodes.First().OriginalAirDate,
            EpisodeCount = episodes.Count()
        };

        return dataPoint;
    }
}