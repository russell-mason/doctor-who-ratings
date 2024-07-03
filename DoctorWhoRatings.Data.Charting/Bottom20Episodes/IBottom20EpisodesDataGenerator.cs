namespace DoctorWhoRatings.Data.Charting.Bottom20Episodes;

/// <summary>
/// Generates a chart-based data source specifically designed to show the 20 episodes with the lowest ratings.
/// </summary>
public interface IBottom20EpisodesDataGenerator
{
    /// <summary>
    /// Generates the data source using the specified options.
    /// </summary>
    /// <param name="options">Options that determine how the ratings are calculated.</param>
    /// <returns>A specific structure containing calculated bottom 20 data.</returns>
    Bottom20EpisodesData Generate(Bottom20EpisodesDataOptions options);
}