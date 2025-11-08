namespace DoctorWhoRatings.Data.Charting.Top20Writers;

/// <summary>
/// Generates a chart-based data source specifically designed to show the 20 writers with the highest number of
/// episodes, script hours, or total ratings.
/// </summary>
public interface ITop20WritersDataPointGenerator
{
    /// <summary>
    /// Generates the data source using the specified options.
    /// </summary>
    /// <param name="options">Options that determine how the totals are calculated.</param>
    /// <returns>A specific structure containing the calculated top 20 writers data.</returns>
    List<Top20WritersDataPoint> Generate(Top20WritersDataOptions options);
}