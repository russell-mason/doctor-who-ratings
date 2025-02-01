namespace DoctorWhoRatings.Data.Charting.AverageByStory;

public class AverageByStoryDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IAverageByStoryDataPointGenerator
{
    public List<AverageByStoryDataPoint> Generate()
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .Where(episode => episode.EraId == Eras.Classic)
                                     .GroupBy(episode => episode.Story)
                                     .Where(group => group.Count() > 1)
                                     .Select((group, index) => CreateDataPoint(index, group))
                                     .ToList();

        return dataPoints;
    }

    private static AverageByStoryDataPoint CreateDataPoint(int index, IGrouping<int, Episode> group)
    {
        var dataPoint = new AverageByStoryDataPoint
        {
            Id = index + 1,
            Actor = group.First().Actor,
            Season = group.First().Season!.Value,
            SeasonFormatDescription = group.First().SeasonFormatDescription,
            StoryTitle = group.First().StoryTitle,
            OriginalAirDate = group.First().OriginalAirDate,
            EpisodeCount = group.Count(),
            CalculatedOvernightRatings = group.Average(e => e.OvernightRatings),
            CalculatedPopulationAdjustedOvernightRatings = group.Average(e => e.PopulationAdjustedOvernightRatings),
        };

        return dataPoint;
    }
}
