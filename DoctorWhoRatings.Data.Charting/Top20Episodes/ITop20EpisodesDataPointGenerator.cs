namespace DoctorWhoRatings.Data.Charting.Top20Episodes;

/// <summary>
/// Generates a chart-based data source specifically designed to show the 20 episodes with the highest ratings.
/// </summary>
public interface ITop20EpisodesDataPointGenerator
{
    /// <summary>
    /// Generates the data source using the specified options.
    /// </summary>
    /// <param name="options">Options that determine how the ratings are calculated.</param>
    /// <returns>A specific structure containing the calculated top 20 episode data.</returns>
    List<Top20EpisodesDataPoint> Generate(Top20EpisodesDataOptions options);
}