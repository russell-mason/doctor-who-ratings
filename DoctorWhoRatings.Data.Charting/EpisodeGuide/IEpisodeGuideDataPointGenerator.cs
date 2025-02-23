namespace DoctorWhoRatings.Data.Charting.EpisodeGuide;

/// <summary>
/// Generates a chart-based data source specifically designed to show a list of episodes.
/// </summary>
public interface IEpisodeGuideDataPointGenerator
{
    /// <summary>
    /// Generates the data source.
    /// </summary>
    /// <param name="options">Options that determine how the episodes are filtered.</param>
    /// <returns>A specific structure containing a list of episodes organised by doctor, season, story, episode.</returns>
    EpisodeGuideDataPoint Generate(EpisodeGuideDataOptions options);
}