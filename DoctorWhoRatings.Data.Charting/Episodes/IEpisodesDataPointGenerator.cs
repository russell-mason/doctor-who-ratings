namespace DoctorWhoRatings.Data.Charting.Episodes;

/// <summary>
/// Generates a chart-based data source specifically designed to show ratings for all episodes.
/// <para>
/// Can be used by multiple charts by specifying different filter options.
/// </para>
/// </summary>
public interface IEpisodesDataPointGenerator
{
    /// <summary>
    /// Generates the data source.
    /// </summary>
    /// <param name="options">Options that determine how to filter the episodes.</param>
    /// <returns>A specific structure containing calculated episode data.</returns>
    List<EpisodeDataPoint> Generate(EpisodesDataOptions options);

    /// <summary>
    /// Generates the data source for a single episode.
    /// </summary>
    /// <param name="episode">The episode to generate the data point for.</param>
    /// <returns>A specific structure containing calculated episode data.</returns>
    EpisodeDataPoint Generate(Episode episode);

    /// <summary>
    /// Filters episodes based on the specified options.
    /// </summary>
    /// <param name="options">Options that determine how to filter the episodes.</param>
    /// <returns>A specific structure containing calculated episode data.</returns>
    List<Episode> Filter(EpisodesDataOptions options);
}