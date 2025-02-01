namespace DoctorWhoRatings.Data.Charting.AverageBySeason;

/// <summary>
/// Generates a chart-based data source specifically designed to show the average ratings across all seasons.
/// The Movie and Specials are not classed as part of a season, so are excluded.
/// </summary>
public interface IAverageBySeasonDataPointGenerator
{
    /// <summary>
    /// Generates the data source.
    /// </summary>
    /// <param name="options">Options that determine how the average ratings are calculated.</param>
    /// <returns>A specific structure containing calculated average data.</returns>
    List<AverageBySeasonDataPoint> Generate(AverageBySeasonDataOptions options);
}