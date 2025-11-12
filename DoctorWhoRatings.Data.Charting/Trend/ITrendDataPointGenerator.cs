namespace DoctorWhoRatings.Data.Charting.Trend;

/// <summary>
/// Generates a chart-based data source specifically designed to show ratings trendlines.
/// <para>
/// N.B. Can only be used on charts based on an EpisodeDataPoint source, and only uses Overnight ratings.
/// </para>
/// </summary>
public interface ITrendDataPointGenerator
{
    /// <summary>
    /// Generates the data source.
    /// </summary>
    /// <param name="dataPoints">The data to calculate trendlines for.</param>
    /// <param name="trendOptions">Options that determine how trendlines should be created.</param>
    /// <returns>A specific structure containing calculated trendline data.</returns>
    List<TrendDataPoint> Generate(List<EpisodeDataPoint> dataPoints, TrendDataOptions trendOptions);

    /// <summary>
    /// Generates the data source as two alternating data sets.
    /// <para>
    /// Due to charting limitations, it may not be possible to display dozens of trendlines, e.g. for all seasons.
    /// In this case two series can be used that alternate trendline pairs with null values for the missing alternate values.
    /// This means that only two series are required rather than dozens.
    /// </para>
    /// </summary>
    /// <param name="dataPoints">The data to calculate trendlines for.</param>
    /// <param name="trendOptions">Options that determine how trendlines should be created.</param>
    /// <returns>A tuple made up of two alternating structures containing calculated trendline data.</returns>
    (List<TrendDataPoint?> alternating1, List<TrendDataPoint?> alternating2)
        GenerateAlternating(List<EpisodeDataPoint> dataPoints, TrendDataOptions trendOptions);
}