namespace DoctorWhoRatings.Data.Charting.Top20Episodes;

/// <summary>
/// Generates a chart-based data source specifically designed to show the 20 episodes with the highest ratings.
/// </summary>
public interface ITop20EpisodesDataGenerator
{
    /// <summary>
    /// Generates the data source using the specified options.
    /// </summary>
    /// <param name="options">Options that determine how the ratings are calculated.</param>
    /// <returns>A specific structure containing calculated top 20 data.</returns>
    Top20EpisodesData Generate(Top20EpisodesDataOptions options);
}