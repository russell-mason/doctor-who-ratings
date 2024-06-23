namespace DoctorWhoRatings.Data.Charting.AllEpisodes;

public class AllEpisodesDataGenerator(IDoctorWhoDataProvider dataProvider) : IAllEpisodesDataGenerator
{
    public AllEpisodesData Generate()
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .Select(CreateDataPoint)
                                     .ToList()
                                     .AsReadOnly();

        var allEpisodesData = new AllEpisodesData()
        {
            DataPoints = dataPoints
        };

        return allEpisodesData;
    }

    private static EpisodeDataPoint CreateDataPoint(Episode episode)
    {
        var dataPoint = new EpisodeDataPoint(episode);

        return dataPoint;
    }
}