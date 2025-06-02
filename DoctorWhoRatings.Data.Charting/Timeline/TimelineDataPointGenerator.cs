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

                                         const bool isCurrent = false;
                                         // Until the next doctor is actually confirmed assume there is no current doctor
                                         // var isCurrent = doctor == dataProvider.DoctorWhoData.Doctors.Last();

                                         return CreateDataPoint(doctor, isCurrent, episodes);
                                     })
                                     .ToList();

        return dataPoints;
    }

    private static TimelineDataPoint CreateDataPoint(Doctor doctor, bool isCurrent, List<Episode> episodes)
    {
        var dataPoint = new TimelineDataPoint
        {
            Number = doctor.Number,
            Actor = doctor.Actor,
            FirstEpisodeAirDate = episodes.First().OriginalAirDate,
            LastEpisodeAirDate = isCurrent ? null : episodes.Last().OriginalAirDate,
            EpisodeCount = episodes.Count
        };

        return dataPoint;
    }
}