namespace DoctorWhoRatings.Data.Charting.AllEpisodes;

public class AllEpisodesDataGenerator(IDoctorWhoDataProvider dataProvider) : IAllEpisodesDataGenerator
{
    public AllEpisodesData Generate()
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .Select(episode => new EpisodeDataPoint(episode))
                                     .ToList()
                                     .AsReadOnly();

        var allEpisodesData = new AllEpisodesData()
        {
            DataPoints = dataPoints
        };

        return allEpisodesData;
    }
}