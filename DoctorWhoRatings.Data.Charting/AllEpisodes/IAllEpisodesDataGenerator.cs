namespace DoctorWhoRatings.Data.Charting.AllEpisodes;

/// <summary>
/// Generates a chart-based data source specifically designed to show ratings for all episodes.
/// </summary>
public interface IAllEpisodesDataGenerator
{
    /// <summary>
    /// Generates the data source.
    /// </summary>
    /// <returns>A specific structure containing calculated episode data.</returns>
    AllEpisodesData Generate();
}