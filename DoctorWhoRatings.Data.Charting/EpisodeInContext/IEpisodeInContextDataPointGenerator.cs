namespace DoctorWhoRatings.Data.Charting.EpisodeInContext;

/// <summary>
/// Generates a chart-based data source specifically designed to show an episode in context of its story, season, doctor, etc.
/// </summary>
public interface IEpisodeInContextDataPointGenerator
{
    /// <summary>
    /// Generates the data source.
    /// </summary>
    /// <param name="options">Options that determine which episode is in context.</param>
    /// <returns>A specific structure containing episode data in its broader context.</returns>
    public EpisodeInContextDataPoint Generate(EpisodeInContextDataOptions options);
}