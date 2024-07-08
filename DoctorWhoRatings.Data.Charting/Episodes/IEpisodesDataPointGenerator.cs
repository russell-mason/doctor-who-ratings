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
}