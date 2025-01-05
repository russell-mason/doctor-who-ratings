namespace DoctorWhoRatings.Data.Charting.AverageByStory;

/// <summary>
/// Generates a chart-based data source specifically designed to show the average ratings across all classic stories.
/// While there are New Who stories, they're not in the same format so don't compare well, so are excluded.
/// </summary>
public interface IAverageByStoryDataPointGenerator
{
    /// <summary>
    /// Generates the data source.
    /// </summary>
    /// <returns>A specific structure containing calculated average data.</returns>
    List<AverageByStoryDataPoint> Generate();
}